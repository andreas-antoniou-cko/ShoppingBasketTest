namespace ItemsBasket.Client.Models
{
    public class AuthenticationResponse
    {
        public string Token { get; }
        public bool IsAuthenticated { get; }
        public string ErrorMessage { get; }

        public AuthenticationResponse(string token, bool isAuthenticated, string errorMessage)
        {
            Token = token;
            IsAuthenticated = isAuthenticated;
            ErrorMessage = errorMessage;
        }
    }
}