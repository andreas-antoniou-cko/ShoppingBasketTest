using ItemsBasket.AuthenticationService.Models;
using System.Collections.Generic;

namespace ItemsBasket.AuthenticationService.Services.Interfaces
{
    /// <summary>
    /// Validation logic for user account information.
    /// </summary>
    public interface IUsersValidator
    {
        /// <summary>
        /// Check if the given username is unique in the current context.
        /// </summary>
        /// <param name="userName">The username to check for.</param>
        /// <param name="context">The current context.</param>
        /// <param name = "errorMessage" > The error message if one occurred.</param>
        /// <returns>True if the username is unique in the current context, otherwise false.</returns>
        bool IsUsernameUnique(string userName, IDictionary<int, User> context, out string errorMessage);

        /// <summary>
        /// Check if the given password conforms to the required rules
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="errorMessage">The error message if one occurred.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        bool IsPasswordValid(string password, out string errorMessage);
    }
}