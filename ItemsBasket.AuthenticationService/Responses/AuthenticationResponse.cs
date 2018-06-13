using ItemsBasket.AuthenticationService.Controllers;
using ItemsBasket.AuthenticationService.Models;

namespace ItemsBasket.AuthenticationService.Responses
{
    /// <summary>
    /// The response object to be returned to an autrhentication request.
    /// </summary>
    public class AuthenticationResponse : BaseResponse<AuthenticatedUser>
    {
        /// <summary>
        /// Default constructor. It is marked as public since Json.Net requires a public constructor.
        /// You can use this or for convinience (and consistency to avoid null user objects if none
        /// is applicable) prefer the factory methods provided.
        /// </summary>
        /// <param name="item">The authenticated user object.</param>
        /// <param name="isSuccessful">A flag to denote success/failure.</param>
        /// <param name="errorMessage">The error message if one occurred.</param>
        public AuthenticationResponse(AuthenticatedUser item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        /// <summary>
        /// Create a successful response to an authentication request.
        /// </summary>
        /// <param name="item">The authenticated user details to be included in the response.</param>
        /// <returns>The response object with the payload set and success flag set to true.</returns>
        public static AuthenticationResponse CreateSuccessfulResult(AuthenticatedUser item)
        {
            return new AuthenticationResponse(item, true, "");
        }

        /// <summary>
        /// Create a failed response to an authentication request.
        /// </summary>
        /// <param name="errorMessage">The error message related to the failure.</param>
        /// <returns>The response object with the payload set to an empty authentication object and success flag set to false.</returns>
        public static AuthenticationResponse CreateFailedResult(string errorMessage)
        {
            return new AuthenticationResponse(AuthenticatedUser.Empty, false, errorMessage);
        }
    }
}
