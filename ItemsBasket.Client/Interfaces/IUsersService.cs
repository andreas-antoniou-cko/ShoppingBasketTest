using ItemsBasket.AuthenticationService.Models;
using System.Threading.Tasks;

namespace ItemsBasket.Client.Interfaces
{
    /// <summary>
    /// Allows CRUD operations for a user.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Create a new user account.
        /// </summary>
        /// <param name="user">The user to create the account for.</param>
        /// <returns>The new instance of the user object.</returns>
        Task<User> CreateUser(User user);

        /// <summary>
        /// Update an existing user account. The only properties that can be modified are the 
        /// username and/or password of the account.
        /// </summary>
        /// <param name="user">The user details to be modified.</param>
        /// <returns>The updated user object.</returns>
        Task<User> UpdateUser(User user);

        /// <summary>
        /// Delete an existing user account with the given ID.
        /// </summary>
        /// <param name="userId">The user ID to delete the account for.</param>
        /// <returns>An empty user instance.</returns>
        Task<User> DeleteUser(User user);
    }
}