using ItemsBasket.BasketService.Models;
using ItemsBasket.AuthenticationService.Controllers;
using System.Collections.Generic;

namespace ItemsBasket.BasketService.Responses
{
    /// <summary>
    /// The response object when querying user basket items.
    /// </summary>
    public class GetUserBasketItemsResponse : BaseResponse<IEnumerable<BasketItem>>
    {
        private static readonly List<BasketItem> EmptyList = new List<BasketItem>();

        /// <summary>
        /// Default constructor. It is marked as public since Json.Net requires a public constructor.
        /// You can use this or for convinience (and consistency to avoid null user objects if none
        /// is applicable) prefer the factory methods provided.
        /// </summary>
        /// <param name="item">The list of items in the user's basket.</param>
        /// <param name = "isSuccessful" > A flag to denote success/failure.</param>
        /// <param name = "errorMessage" > The error message if one occurred.</param>
        public GetUserBasketItemsResponse(List<BasketItem> item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        /// <summary>
        /// Create a successful response to a request to list user basket items.
        /// </summary>
        /// <param name="basketItems">The list of the user's basket items.</param>
        /// <returns>The response object with the payload set and success flag set to true.</returns>
        public static GetUserBasketItemsResponse CreateSuccessfulResult(List<BasketItem> basketItems)
        {
            return new GetUserBasketItemsResponse(basketItems, true, "");
        }

        /// <summary>
        /// Create a failed response to to a request to list user basket items.
        /// </summary>
        /// <param name="errorMessage">The error message related to the failure.</param>
        /// <returns>The response object with the payload set to an empty authentication object and success flag set to false.</returns>
        public static GetUserBasketItemsResponse CreateFailedResult(string errorMessage)
        {
            return new GetUserBasketItemsResponse(EmptyList, false, errorMessage);
        }
    }
}