using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Services.Interfaces
{
    /// <summary>
    /// Repository class for user account records.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Return a list of user names registered in the data store.
        /// </summary>
        /// <returns>List of usernames.</returns>
        Task<IEnumerable<string>> List();

        /// <summary>
        /// Check if a username exists and if the password matches.
        /// </summary>
        /// <param name="username">The username of the user to check.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// A tuple containing a flag denoting if the user was authenticated, the user id and a string with 
        /// a response if an error occurred or if the authentication failed.
        /// </returns>
        Task<(bool, int, string)> IsAuthenticated(string username, string password);

        /// <summary>
        /// Create a new user account. Some crude validation will take place to ensure it is valid.
        /// </summary>
        /// <param name="userName">The username of the account.</param>
        /// <param name="password">The password of the account.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<UserResponse> Create(string userName, string password);

        /// <summary>
        /// Update the details of a registered user. The properties that can be modified are the username
        /// and the password.
        /// </summary>
        /// <param name="user">The user details of the account to be modified.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<UserResponse> Update(User user);

        /// <summary>
        /// Delete a user account.
        /// </summary>
        /// <param name="userId">The user ID to delete.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        Task<UserResponse> Delete(int userId);
    }
}