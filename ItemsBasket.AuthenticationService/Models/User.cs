using System.Collections.Generic;

namespace ItemsBasket.AuthenticationService.Models
{
    /// <summary>
    /// The details of a user as stored in the datastore.
    /// </summary>
    public class User
    {
        /// <summary>
        /// An empty instance of a user. To be used where no valid instance exists, instead of null.
        /// </summary>
        public static readonly User Empty = new User(-1, "", "");

        /// <summary>
        /// The ID of the user.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The password of the user's account.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password of the user's account.</param>
        public User(int userId, string userName, string password)
        {
            UserId = userId;
            Username = userName;
            Password = password;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override string ToString()
        {
            return Username;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   UserId == user.UserId &&
                   Username == user.Username &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            var hashCode = -1846303576;
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}