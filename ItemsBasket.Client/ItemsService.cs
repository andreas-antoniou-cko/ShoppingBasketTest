using ItemsBasket.Client.Interfaces;
using ItemsBasket.ItemsService.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsBasket.Client
{
    public class ItemsService : BaseServiceCaller, IItemsService
    {
        /// <summary>
        /// Default constructor. To be used in scenarios where Dependency Injection 
        /// is not available.
        /// </summary>
        public ItemsService()
            : base(new EnvironmentService(), new HttpClientProvider(), false)
        {
        }

        /// <summary>
        /// Dependency Injection friendly constructor. 
        /// </summary>
        /// <param name="environmentService">The environment service containing endpoint information.</param>
        /// <param name="httpClientProvider">The http client provider for authenticatead and non authenticated clients.</param>
        protected ItemsService(IEnvironmentService environmentService, IHttpClientProvider httpClientProvider, bool authenticate) 
            : base(environmentService, httpClientProvider, authenticate)
        {
        }

        /// <summary>
        /// Get details for all items currently available.
        /// </summary>
        /// <returns></returns>
        public async Task<GetItemsResponse> ListAllAvailabeItems()
        {
            return await GetCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.ItemsService]}",
                e => { return GetItemsResponse.CreateFailedResult(e.Message); });
        }

        /// <summary>
        /// Get details for the items with the requested IDs.
        /// </summary>
        /// <param name="itemIds">List of item IDs to fetch details for.</param>
        /// <returns>
        /// A list of items item details and a flag to denote if the operation 
        /// was successful or not - in which case an error message will be included.
        /// </returns>
        public async Task<GetItemsResponse> GetItems(List<int> itemIds)
        {
            string queryString = "?" + string.Join("&", itemIds.Select(i => $"itemIds={i}"));

            return await GetCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.ItemsService]}/GetByIds?{queryString}",
                e => { return GetItemsResponse.CreateFailedResult(e.Message); });
        }
    }
}
