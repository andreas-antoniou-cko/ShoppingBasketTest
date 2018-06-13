using ItemsBasket.ItemsService.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemsBasket.Client.Interfaces
{
    /// <summary>
    /// Service related to fetching items availbe to be updated on a user's basket.
    /// </summary>
    public interface IItemsService
    {
        /// <summary>
        /// Get details for all items currently available.
        /// </summary>
        /// <returns></returns>
        Task<GetItemsResponse> ListAllAvailabeItems();

        /// <summary>
        /// Get details for the items with the requested IDs.
        /// </summary>
        /// <param name="itemIds">List of item IDs to fetch details for.</param>
        /// <returns>
        /// A list of items item details and a flag to denote if the operation 
        /// was successful or not - in which case an error message will be included.
        /// </returns>
        Task<GetItemsResponse> GetItems(List<int> itemIds);
    }
}