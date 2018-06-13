using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Responses;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Services
{
    /// <summary>
    /// The implementation of this class assumes only a single instance of the service will 
    /// be running. Most of the code would be redundant with a SQL (or any) db, specially
    /// for key creation.
    /// </summary>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public class UsersRepository : IUsersRepository
    {
        private static readonly object _newUserLock = new object();

        private readonly IUsersValidator _usersValidator;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="usersValidator">The users validator dependency.</param>
        public UsersRepository(IUsersValidator usersValidator)
        {
            _usersValidator = usersValidator;
        }

        private int _currentMaxUserId = 1;
        private readonly IDictionary<int, User> _context = new Dictionary<int, User>
        {
            { 1, new User(1, UsersValidator.AdminUsername, "pass") }
        };

        /// <summary>
        /// Return a list of user names registered in the data store.
        /// </summary>
        /// <returns>List of usernames.</returns>
        public async Task<IEnumerable<string>> List()
        {
            return _context.Select(c => c.Value.Username);
        }

        /// <summary>
        /// Check if a username exists and if the password matches.
        /// </summary>
        /// <param name="username">The username of the user to check.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// A tuple containing a flag denoting if the user was authenticated, the user id and a string with 
        /// a response if an error occurred or if the authentication failed.
        /// </returns>
        public async Task<(bool, int, string)> IsAuthenticated(string username, string password)
        {
            var user = _context.Values.FirstOrDefault(c => string.Equals(c.Username, username));

            if (user == null)
            {
                return (false, -1, $"No user found with username = {username}");
            }

            if (!string.Equals(user.Password, password))
            {
                return (false, -1, $"The password provided for user with username = {username} is wrong. Please try again.");
            }

            return (true, user.UserId, "");
        }

        /// <summary>
        /// Create a new user account. Some crude validation will take place to ensure it is valid.
        /// </summary>
        /// <param name="userName">The username of the account.</param>
        /// <param name="password">The password of the account.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<UserResponse> Create(string userName, string password)
        {
            int nextUserId;
            User newUser;

            lock(_newUserLock)
            {
                if (!_usersValidator.IsPasswordValid(password, out string errorMessage))
                {
                    return UserResponse.CreateFailedResult(errorMessage);
                }
                if (!_usersValidator.IsUsernameUnique(password, _context, out errorMessage))
                {
                    return UserResponse.CreateFailedResult(errorMessage);
                }

                _currentMaxUserId++;
                nextUserId = _currentMaxUserId;
            }

            newUser = new User(nextUserId, userName, password);

            _context.Add(newUser.UserId, newUser);

            return UserResponse.CreateSuccessfulResult(newUser);
        }

        /// <summary>
        /// Update the details of a registered user. The properties that can be modified are the username
        /// and the password.
        /// </summary>
        /// <param name="user">The user details of the account to be modified.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<UserResponse> Update(User user)
        {
            if (!_context.TryGetValue(user.UserId, out User existingUser))
            {
                return UserResponse.CreateFailedResult($"Could not find user with ID = {user.UserId} to update.");
            }

            if (!_usersValidator.IsUsernameUnique(user.Password, _context, out string errorMessage))
            {
                return UserResponse.CreateFailedResult(errorMessage);
            }

            _context[user.UserId] = user;

            return UserResponse.CreateSuccessfulResult(user);
        }

        /// <summary>
        /// Delete a user account.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        public async Task<UserResponse> Delete(int userId)
        {
            if (!_context.TryGetValue(userId, out User existingUser))
            {
                return UserResponse.CreateFailedResult($"Could not find user with ID = {userId} to delete.");
            }

            _context.Remove(userId);

            return UserResponse.CreateSuccessfulResult(User.Empty);
        }
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}