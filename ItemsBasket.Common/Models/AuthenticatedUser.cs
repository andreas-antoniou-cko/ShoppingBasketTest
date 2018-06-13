namespace ItemsBasket.AuthenticationService.Models
{
    /// <summary>
    /// The details of an authenticated user.
    /// </summary>
    public class AuthenticatedUser
    {
        /// <summary>
        /// An empty instance of an authenticated user. To be used where no valid instance exists, instead of null.
        /// </summary>
        public static readonly AuthenticatedUser Empty = new AuthenticatedUser(-1, "", null);

        /// <summary>
        /// The user ID of the authenticated user.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The user name of the authenticated user.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The authentication token.
        /// </summary>
        public string Token { get; }

        public AuthenticatedUser(int userId, string userName, string token)
        {
            UserId = userId;
            Username = userName;
            Token = token;
        }
    }
}