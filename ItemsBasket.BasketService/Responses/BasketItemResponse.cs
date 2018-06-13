using ItemsBasket.AuthenticationService.Controllers;
using ItemsBasket.Common.Models;
using System.Collections.Generic;

namespace ItemsBasket.BasketService.Responses
{
    /// <summary>
    /// The response object when performing actions on basket items
    /// </summary>
    public class BasketItemResponse : BaseResponse<List<DetailedBasketItem>>
    {
        /// <summary>
        /// Default constructor. It is marked as public since Json.Net requires a public constructor.
        /// You can use this or for convinience (and consistency to avoid null user objects if none
        /// is applicable) prefer the factory methods provided.
        /// </summary>
        /// <param name="item">The response item - a list of detailed basket items.</param>
        /// <param name="isSuccessful">A flag to denote success/failure.</param>
        /// <param name="errorMessage">The error message if one occurred.</param>
        public BasketItemResponse(List<DetailedBasketItem> item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        /// <summary>
        /// Create a successful response to an update request of one basket item.
        /// </summary>
        /// <param name="basketItem">The detailed basket item.</param>
        /// <returns>The response object with the payload set and success flag set to true.</returns>
        public static BasketItemResponse CreateSuccessfulResult(DetailedBasketItem basketItem)
        {
            return new BasketItemResponse(new List<DetailedBasketItem> { basketItem }, true, "");
        }

        /// <summary>
        /// Create a successful response to an update request of one or more basket items.
        /// </summary>
        /// <param name="basketItems">The list of detailed basket items.</param>
        /// <returns>The response object with the payload set and success flag set to true.</returns>
        public static BasketItemResponse CreateSuccessfulResult(List<DetailedBasketItem> basketItems)
        {
            return new BasketItemResponse(basketItems, true, "");
        }

        /// <summary>
        /// Create a failed response to an update request of one or more basket items.
        /// </summary>
        /// <param name="errorMessage">The error message related to the failure.</param>
        /// <returns>The response object with the payload set to an empty list and success flag set to false.</returns>
        public static BasketItemResponse CreateFailedResult(string errorMessage)
        {
            return new BasketItemResponse(new List<DetailedBasketItem>(), false, errorMessage);
        }
    }
}