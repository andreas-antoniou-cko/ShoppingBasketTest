using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Responses;
using ItemsBasket.Client.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemsBasket.Client
{
    public class BasketService : BaseServiceCaller, IBasketService
    {
        /// <summary>
        /// Default constructor. To be used in scenarios where Dependency Injection 
        /// is not available.
        /// </summary>
        public BasketService()
            : base(new EnvironmentService(), new HttpClientProvider(), true)
        {
        }

        /// <summary>
        /// Dependency Injection friendly constructor. 
        /// </summary>
        /// <param name="environmentService">The environment service containing endpoint information.</param>
        /// /// <param name="httpClientProvider">The http client provider for authenticatead and non authenticated clients.</param>
        public BasketService(IEnvironmentService environmentService, IHttpClientProvider httpClientProvider, bool authenticate)
            : base(environmentService, httpClientProvider, authenticate)
        {
        }

        /// <summary>
        /// List all the items currently in the user's basket.
        /// </summary>
        /// <returns>
        /// A list of items in the user's basket and a flag to denote if the operation 
        /// was successful or not - in which case an error message will be included.
        /// </returns>
        public async Task<GetUserBasketItemsResponse> ListBasketItems()
        {
            return await GetCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.BasketService]}",
                e => { return GetUserBasketItemsResponse.CreateFailedResult(e.Message); });
        }

        /// <summary>
        /// Add an item to the user's basket.
        /// </summary>
        /// <param name="item">The item to add. This includes the item ID as well as the quantity.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> AddItemToBasket(BasketItem item)
        {
            return await PutCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.BasketService]}",
                item,
                e => { return BasketItemResponse.CreateFailedResult(e.Message); });
        }

        /// <summary>
        /// Updates the given item from the user's basket.
        ///     - If the item does not exist it will be added.
        ///     - If the item quantity is set to zero it will be removed.
        /// </summary>
        /// <param name="update">A single basket update.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> UpdateBasketItem(BasketItem item)
        {
            return await PostCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.BasketService]}",
                item,
                e => { return BasketItemResponse.CreateFailedResult(e.Message); });
        }

        /// <summary>
        /// Updates multiple items from the user's basket.
        ///     - If the item does not exist it will be added.
        ///     - If the item quantity is set to zero it will be removed.
        /// </summary>
        /// <param name="updates">A list of basket updates.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> UpdateBasketItem(List<BasketItem> items)
        {
            return await PostCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.BasketService]}",
                items,
                e => { return BasketItemResponse.CreateFailedResult(e.Message); });
        }

        /// <summary>
        /// Remove an item to the user's basket.
        /// </summary>
        /// <param name="itemId">The ID of the item to remove.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> RemoveItemFromBasket(BasketItem item)
        {
            return await DeleteCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.BasketService]}/{item.ItemId}",
                item,
                e => { return BasketItemResponse.CreateFailedResult(e.Message); });
        }

        /// <summary>
        /// Clears all the contents from the user's basket.
        /// </summary>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<BasketItemResponse> ClearBasket()
        {
            return await DeleteCall<object, BasketItemResponse>(
                $"{EnvironmentService.ServiceEndpoints[KnownService.BasketService]}",
                null,
                e => { return BasketItemResponse.CreateFailedResult(e.Message); });
        }
    }
}