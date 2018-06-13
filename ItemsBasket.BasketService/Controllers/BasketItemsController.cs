using System.Collections.Generic;
using System.Threading.Tasks;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Responses;
using ItemsBasket.BasketService.Services.Interfaces;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItemsBasket.BasketService.Controllers
{
    /// <summary>
    /// Performs operations on the basket items of the requesting user's basket.
    /// The following operations are supported:
    ///     - Get (list basket items)
    ///     - Put (add a single item to the basket)
    ///     - Post (update a single or multiple basket items)
    ///     - Delete (delete a single or multiple basket items)
    /// The users identity will be extracted from the claims so ensure the ID is always included.
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/BasketItems")]
    public class BasketItemsController : Controller
    {
        private readonly IBasketItemsRepository _basketItemsRepository;
        private readonly IAuthorizationLayer _authorizationLayer;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="basketItemsRepository">The basket items repository dependency.</param>
        /// <param name="authorizationLayer">The authorization layer dependency.</param>
        public BasketItemsController(IBasketItemsRepository basketItemsRepository,
            IAuthorizationLayer authorizationLayer)
        {
            _basketItemsRepository = basketItemsRepository;
            _authorizationLayer = authorizationLayer;
        }
        
        /// <summary>
        /// Retrieves all the items currently in the basket of the user who initiated the request.
        /// </summary>
        /// <returns>
        /// The items of the user's basket as well as a response containing 
        /// success/failure of the operation and an error message if one occurs.
        /// </returns>
        [HttpGet]
        public async Task<GetUserBasketItemsResponse> Get()
        {
            return await _authorizationLayer.ExecuteAuthorizedAction(User.Identity,
                id => _basketItemsRepository.GetBasketItems(id),
                e => GetUserBasketItemsResponse.CreateFailedResult(e),
                "An error occurred while trying to fetch the users basket items.");
        }

        /// <summary>
        /// Adds a single item to the basket of the user who initiated the request. If the item 
        /// has already been added then it is updated.
        /// </summary>
        /// <param name="update">The update object containing the basket item details.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpPut]
        public async Task<BasketItemResponse> Put([FromBody]BasketItem update)
        {
            return await _authorizationLayer.ExecuteAuthorizedAction(User.Identity,
                id => _basketItemsRepository.AddItem(id, update),
                e => BasketItemResponse.CreateFailedResult(e),
                "An error occurred while trying to add a new item in your basket.");
        }

        /// <summary>
        /// Updates the given item from the basket of the user who initiated the request.
        ///     - If the item does not exist it will be added.
        ///     - If the item quantity is set to zero it will be removed.
        /// </summary>
        /// <param name="update">A single basket update.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpPost]
        public async Task<BasketItemResponse> Post([FromBody]BasketItem update)
        {
            return await _authorizationLayer.ExecuteAuthorizedAction(User.Identity,
                id => _basketItemsRepository.UpdateItem(id, update),
                e => BasketItemResponse.CreateFailedResult(e),
                "An error occurred while trying to update your basket item.");
        }

        /// <summary>
        /// Updates the given items from the basket of the user who initiated the request.
        ///     - If an item does not exist it will be added.
        ///     - If an item quantity is set to zero it will be removed.
        /// </summary>
        /// <param name="updates">A list of basket updates.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpPost("PostMany")]
        public async Task<BasketItemResponse> Post([FromBody]List<BasketItem> updates)
        {
            return await _authorizationLayer.ExecuteAuthorizedAction(User.Identity,
                id => _basketItemsRepository.UpdateItems(id, updates),
                e => BasketItemResponse.CreateFailedResult(e),
                "An error occurred while trying to fetch the users basket items.");
        }

        /// <summary>
        /// Deletes all the contents of the basket for the user who initiated the request.
        /// </summary>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpDelete]
        public async Task<BasketItemResponse> Delete()
        {
            return await _authorizationLayer.ExecuteAuthorizedAction(User.Identity,
                id => _basketItemsRepository.ClearItems(id),
                e => BasketItemResponse.CreateFailedResult(e),
                "An error occurred while trying to clear your basket items.");
        }

        /// <summary>
        /// Deletes a single item from the basket of the user who initiated the request
        /// </summary>
        /// <param name="itemId">The ID of the item to remove from the basket.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpDelete("{itemId}")]
        public async Task<BasketItemResponse> Delete([FromRoute] int itemId)
        {
            return await _authorizationLayer.ExecuteAuthorizedAction(User.Identity,
                id => _basketItemsRepository.RemoveItem(id, itemId),
                e => BasketItemResponse.CreateFailedResult(e),
                "An error occurred while trying to clear your basket items.");
        }
    }
}