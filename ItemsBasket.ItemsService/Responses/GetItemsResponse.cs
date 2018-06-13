using ItemsBasket.AuthenticationService.Controllers;
using ItemsBasket.Common.Models;
using System.Collections.Generic;

namespace ItemsBasket.ItemsService.Responses
{
    /// <summary>
    /// The response object when querying for items.
    /// </summary>
    public class GetItemsResponse : BaseResponse<List<ItemDetails>>
    {
        /// <summary>
        /// Default constructor. It is marked as public since Json.Net requires a public constructor.
        /// You can use this or for convinience (and consistency to avoid null user objects if none
        /// is applicable) prefer the factory methods provided.
        /// </summary>
        /// <param name="item">The list of item details.</param>
        /// <param name = "isSuccessful">A flag to denote success/failure.</param>
        /// <param name = "errorMessage">The error message if one occurred.</param>
        public GetItemsResponse(List<ItemDetails> item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        /// <summary>
        /// Create a successful response to a request to list items.
        /// </summary>
        /// <param name="basketItems">The list of requested items.</param>
        /// <returns>The response object with the payload set and success flag set to true.</returns>
        public static GetItemsResponse CreateSuccessfulResult(List<ItemDetails> basketItems)
        {
            return new GetItemsResponse(basketItems, true, "");
        }

        /// <summary>
        /// Create a failed response to to a request to list items.
        /// </summary>
        /// <param name="errorMessage">The error message related to the failure.</param>
        /// <returns>The response object with the payload set to an empty list and success flag set to false.</returns>
        public static GetItemsResponse CreateFailedResult(string errorMessage)
        {
            return new GetItemsResponse(new List<ItemDetails>(), false, errorMessage);
        }
    }
}