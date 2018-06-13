using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemsBasket.BasketService.Services.Interfaces
{
    /// <summary>
    /// A repository class for performing CRUD actions on user basket items.
    /// </summary>
    public interface IBasketItemsRepository
    {
        /// <summary>
        /// Retrieves all the items from the basket of the given user.
        /// </summary>
        /// <param name="userId">The user ID for whom to retrieve the basket items for.</param>
        /// <returns>
        /// A response containing the items of the users basket as well as a flag for success/failure (in which an error message will also exist).
        /// </returns>
        Task<GetUserBasketItemsResponse> GetBasketItems(int userId);

        /// <summary>
        /// Adds a single item to the basket of the given user. If the items has already been added
        /// then it is updated.
        /// </summary>
        /// <param name="userId">The user ID for whom to add the basket item to.</param>
        /// <param name="basketItem">The basket item to add.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> AddItem(int userId, BasketItem basketItem);

        /// <summary>
        /// Removes a single item from the basket of the given user.
        /// </summary>
        /// <param name="userId">The user ID for whom to remove the basket item from.</param>
        /// <param name="itemId">The item ID to remove from the basket.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> RemoveItem(int userId, int itemId);

        /// <summary>
        /// Updates (or adds if not present) a single item from the basket of the given user. 
        /// The only thing that can be updated is the quantity. If the quantity is set to zero 
        /// then the item will be removed.
        /// </summary>
        /// <param name="userId">The user ID for whom to update the basket item.</param>
        /// <param name="basketItem">The basket item to update.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> UpdateItem(int userId, BasketItem basketItem);

        /// <summary>
        /// Updates (or adds if not present) multiple items from the basket of the given user. 
        /// The only thing that can be updated is the quantity. If the quantity is set to zero 
        /// then the item will be removed.
        /// </summary>
        /// <param name="userId">The user ID for whom to update the basket item.</param>
        /// <param name="basketItems">The basket items to update.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> UpdateItems(int userId, IEnumerable<BasketItem> basketItems);

        /// <summary>
        /// Clears all the items from the given users basket.
        /// </summary>
        /// <param name="userId">The user ID for whom to clear all basket items.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<BasketItemResponse> ClearItems(int userId);
    }
}