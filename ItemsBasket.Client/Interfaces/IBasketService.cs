using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemsBasket.Client.Interfaces
{
    public interface IBasketService
    {
        /// <summary>
        /// List all the items currently in the user's basket.
        /// </summary>
        /// <returns>
        /// A list of items in the user's basket and a flag to denote if the operation 
        /// was successful or not - in which case an error message will be included.
        /// </returns>
        Task<GetUserBasketItemsResponse> ListBasketItems();

        /// <summary>
        /// Add an item to the user's basket.
        /// </summary>
        /// <param name="item">The item to add. This includes the item ID as well as the quantity.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> AddItemToBasket(BasketItem item);

        /// <summary>
        /// Updates the given item from the user's basket.
        ///     - If the item does not exist it will be added.
        ///     - If the item quantity is set to zero it will be removed.
        /// </summary>
        /// <param name="update">A single basket update.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> UpdateBasketItem(BasketItem item);

        /// <summary>
        /// Updates multiple items from the user's basket.
        ///     - If the item does not exist it will be added.
        ///     - If the item quantity is set to zero it will be removed.
        /// </summary>
        /// <param name="updates">A list of basket updates.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> UpdateBasketItem(List<BasketItem> items);

        /// <summary>
        /// Remove an item to the user's basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to remove.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> RemoveItemFromBasket(BasketItem item);

        /// <summary>
        /// Clears all the contents from the user's basket.
        /// </summary>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> ClearBasket();
    }
}
