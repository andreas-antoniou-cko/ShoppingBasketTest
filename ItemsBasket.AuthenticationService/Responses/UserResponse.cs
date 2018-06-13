using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Controllers;

namespace ItemsBasket.AuthenticationService.Responses
{
    /// <summary>
    /// The response object returned when performing user operations on the API.
    /// </summary>
    public class UserResponse : BaseResponse<User>
    {
        /// <summary>
        /// Default constructor. It is marked as public since Json.Net requires a public constructor.
        /// You can use this or for convinience (and consistency to avoid null user objects if none
        /// is applicable) prefer the factory methods provided.
        /// </summary>
        /// <param name="item">The User item.</param>
        /// <param name="isSuccessful">A flag to denote success/failure.</param>
        /// <param name = "errorMessage" > The error message if one occurred.</param>
        public UserResponse(User item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        /// <summary>
        /// Creates a successful response object.
        /// </summary>
        /// <param name="user">The user that forms the payload of the response.</param>
        /// <returns>A successful response containing the user that was operated on.</returns>
        public static UserResponse CreateSuccessfulResult(User user)
        {
            return new UserResponse(user, true, "");
        }

        /// <summary>
        /// Create a failed response object.
        /// </summary>
        /// <param name="errorMessage">The error message describing the failure.</param>
        /// <returns>A unsuccesssful response containing the error and an empty user object.</returns>
        public static UserResponse CreateFailedResult(string errorMessage)
        {
            return new UserResponse(User.Empty, false, errorMessage);
        }
    }
}