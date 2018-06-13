using System.Collections.Generic;
using System.Linq;
using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Services.Interfaces;

namespace ItemsBasket.AuthenticationService.Services
{
    /// <summary>
    /// Validation logic for user account information.
    /// </summary>
    public class UsersValidator : IUsersValidator
    {
        /// <summary>
        /// The hard-coded pre-existing admin account username.
        /// </summary>
        public const string AdminUsername = "admin";

        /// <summary>
        /// Check if the given username is unique in the current context.
        /// </summary>
        /// <param name="userName">The username to check for.</param>
        /// <param name="context">The current context.</param>
        /// <param name = "errorMessage" > The error message if one occurred.</param>
        /// <returns>True if the username is unique in the current context, otherwise false.</returns>
        public bool IsUsernameUnique(string userName, IDictionary<int, User> context, out string errorMessage)
        {
            if (context.Values.Any(v => string.Equals(v.Username, userName)))
            {
                errorMessage = $"Username {userName} is not unique. Please select a unique username.";
                return false;
            }

            errorMessage = "";
            return true;
        }

        /// <summary>
        /// Check if the given password conforms to the required rules
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="errorMessage">The error message if one occurred.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        public bool IsPasswordValid(string password, out string errorMessage)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 4)
            {
                errorMessage = "A valid password of minimum 4 characters long needs to be supplied.";
                return false;
            }

            errorMessage = "";
            return true;
        }
    }
}