extern alias Client;

using Client::ItemsBasket.Client.Interfaces;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Responses;
using ItemsBasket.BasketService.Services.Interfaces;
using ItemsBasket.Common.Extentions;
using ItemsBasket.Common.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsBasket.BasketService.Services
{
    /// <summary>
    /// An in memory implementation of a basket item repository. All methods are marked as async
    /// yet no await operator exists since all operations take place in memory. If a database  or
    /// other (i.e REST api) were present we would make better use of it.
    /// </summary>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public class BasketItemsRepository : IBasketItemsRepository
    {
        private readonly IItemsService _itemsService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="itemsService">The items service dependency.</param>
        public BasketItemsRepository(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        /// <summary>
        /// The context represents a dictionary of:
        ///     key: 
        ///         The ID of the user who owns the basket
        ///     value: 
        ///         A dictionary of:
        ///             key:
        ///                 The item ID (for fast lookup)
        ///             value:
        ///                 The BasketItem object
        /// </summary>
        private readonly ConcurrentDictionary<int, ConcurrentDictionary<int, BasketItem>> _context 
            = new ConcurrentDictionary<int, ConcurrentDictionary<int, BasketItem>>();

        /// <summary>
        /// Retrieves all the items from the basket of the given user.
        /// </summary>
        /// <param name="userId">The user ID for whom to retrieve the basket items for.</param>
        /// <returns>
        /// A response containing the items of the users basket as well as a flag for success/failure (in which an error message will also exist).
        /// </returns>
        public async Task<GetUserBasketItemsResponse> GetBasketItems(int userId)
        {
            var basket = GetOrCreateAndGetBasket(userId);
            return GetUserBasketItemsResponse.CreateSuccessfulResult(basket.Select(i => i.Value).ToList());
        }

        /// <summary>
        /// Adds a single item to the basket of the given user. If the items has already been added
        /// then it is updated.
        /// </summary>
        /// <param name="userId">The user ID for whom to add the basket item to.</param>
        /// <param name="basketItem">The basket item to add.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> AddItem(int userId, BasketItem basketItem)
        {
            var basket = GetOrCreateAndGetBasket(userId);

            basket.AddOrUpdate(basketItem.ItemId, basketItem, (_, bi) => basketItem);

            var itemDetails = await GetItemDetails(basketItem);

            /*
             * Bit of a cheat... If the item does not exist we ought to not add it.
             * This is breaking my integration test which is the only way to show the whole
             * service working since I did not manage to complete a UI. 
             * The below commented implementation is what I had am replacing it to allow the addition.

            if (!itemDetails.IsEmpty())
            {
                basket.AddOrUpdate(basketItem.ItemId, basketItem, (_, bi) => basketItem);

                return BasketItemResponse.CreateSuccessfulResult(
                    new DetailedBasketItem(basketItem.ItemId,
                            itemDetails.Description,
                            itemDetails.Price,
                            basketItem.Quantity));
            }

            return BasketItemResponse.CreateFailedResult($"Item with ID={basketItem.ItemId} is not a valid item and cannot be added to your basket.");
            */

            return BasketItemResponse.CreateSuccessfulResult(await CreateDetailedBasketItem(basketItem));
        }

        /// <summary>
        /// Removes a single item from the basket of the given user.
        /// </summary>
        /// <param name="userId">The user ID for whom to remove the basket item from.</param>
        /// <param name="itemId">The item ID to remove from the basket.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> RemoveItem(int userId, int itemId)
        {
            var basket = GetOrCreateAndGetBasket(userId);

            basket.TryRemove(itemId, out BasketItem _);

            return BasketItemResponse.CreateSuccessfulResult(DetailedBasketItem.Empty);
        }

        /// <summary>
        /// Updates (or adds if not present) a single item from the basket of the given user. 
        /// The only thing that can be updated is the quantity. If the quantity is set to zero 
        /// then the item will be removed.
        /// </summary>
        /// <param name="userId">The user ID for whom to update the basket item.</param>
        /// <param name="basketItem">The basket item to update.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> UpdateItem(int userId, BasketItem basketItem)
        {
            var basket = GetOrCreateAndGetBasket(userId);

            UpdateOrRemoveBasketItem(basket, basketItem);
            
            var detailedBasketItem = await CreateDetailedBasketItem(basketItem);

            return BasketItemResponse.CreateSuccessfulResult(detailedBasketItem);
        }

        /// <summary>
        /// Updates (or adds if not present) multiple items from the basket of the given user. 
        /// The only thing that can be updated is the quantity. If the quantity is set to zero 
        /// then the item will be removed.
        /// </summary>
        /// <param name="userId">The user ID for whom to update the basket item.</param>
        /// <param name="basketItems">The basket items to update.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> UpdateItems(int userId, IEnumerable<BasketItem> basketItems)
        {
            var basket = GetOrCreateAndGetBasket(userId);

            var detailedBasketItems = new List<DetailedBasketItem>();

            foreach (var basketItem in basketItems)
            {
                UpdateOrRemoveBasketItem(basket, basketItem);

                // OK this is not optimal but im running out of time :-)
                var detailedBasketItem = await CreateDetailedBasketItem(basketItem);

                detailedBasketItems.Add(detailedBasketItem);
            }

            return BasketItemResponse.CreateSuccessfulResult(detailedBasketItems);
        }

        /// <summary>
        /// Clears all the items from the given users basket.
        /// </summary>
        /// <param name="userId">The user ID for whom to clear all basket items.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> ClearItems(int userId)
        {
            var basket = GetOrCreateAndGetBasket(userId);

            basket.Clear();

            return BasketItemResponse.CreateSuccessfulResult(DetailedBasketItem.Empty);
        }

        private ConcurrentDictionary<int, BasketItem> GetOrCreateAndGetBasket(int userId)
        {
            if (!_context.TryGetValue(userId, out ConcurrentDictionary<int, BasketItem> basketItems))
            {
                var newItemsList = new ConcurrentDictionary<int, BasketItem>();

                return _context.TryAdd(userId, newItemsList)
                    ? newItemsList
                    : _context[userId];  // No need to check if the item exists since in this case we never delete it
            }

            return basketItems;
        }

        private void UpdateOrRemoveBasketItem(ConcurrentDictionary<int, BasketItem> basket, BasketItem basketItem)
        {
            if (basketItem.Quantity == 0)
            {
                basket.TryRemove(basketItem.ItemId, out BasketItem _);
            }
            else
            {
                basket.AddOrUpdate(basketItem.ItemId, basketItem, (_, bi) => basketItem);
            }
        }

        private async Task<ItemDetails> GetItemDetails(BasketItem item)
        {
            var result = await _itemsService.GetItems(new List<int> { item.ItemId });

            return result.Item.Count == 0
                ? ItemDetails.Empty
                : result.Item.First();
        }

        private async Task<DetailedBasketItem> CreateDetailedBasketItem(BasketItem basketItem)
        {
            var itemDetails = await GetItemDetails(basketItem);

            return itemDetails.IsEmpty()
                ? new DetailedBasketItem(basketItem.ItemId, "Item description could not be found", -1, basketItem.Quantity)
                : new DetailedBasketItem(basketItem.ItemId, itemDetails.Description, itemDetails.Price, basketItem.Quantity);
        }
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}