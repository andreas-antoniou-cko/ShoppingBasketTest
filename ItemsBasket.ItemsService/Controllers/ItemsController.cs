using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemsBasket.Common.Models;
using ItemsBasket.ItemsService.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ItemsBasket.ItemsService.Controllers
{
    /// <summary>
    /// Simple controller returning a list of hard-coded items.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Items")]
    public class ItemsController : Controller
    {
        private static readonly List<ItemDetails> _allItems = new List<ItemDetails>
        {
            new ItemDetails(1, "Item 1", 10),
            new ItemDetails(2, "Item 2", 20),
            new ItemDetails(3, "Item 3", 30),
            new ItemDetails(4, "Item 4", 40),
            new ItemDetails(5, "Item 5", 50)
        };

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Returns all available items.
        /// </summary>
        /// <returns>A response object containing all the available items.</returns>
        [HttpGet]
        public async Task<GetItemsResponse> Get()
        {
            return GetItemsResponse.CreateSuccessfulResult(_allItems);
        }

        /// <summary>
        /// Returns the details of all item IDs.
        /// </summary>
        /// <returns>A response object containing all the requested item details.</returns>
        [HttpGet("GetByIds")]
        public async Task<GetItemsResponse> GetByIds([FromQuery] List<int> itemIds)
        {
            return GetItemsResponse.CreateSuccessfulResult(
                _allItems.Where(i => itemIds.Contains(i.ItemId)).ToList());
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}