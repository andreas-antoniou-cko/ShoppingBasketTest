namespace ItemsBasket.AuthenticationService.Models
{
    /// <summary>
    /// The contents of a user authentication request.
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The password associated with the username.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The password associated with the username.</param>
        public AuthenticationRequest(string userName, string password)
        {
            Username = userName;
            Password = password;
        }

        /// <summary>
        /// String representation of object.
        /// </summary>
        /// <returns>String representation of object.</returns>
        public override string ToString()
        {
            return Username;
        }
    }
}