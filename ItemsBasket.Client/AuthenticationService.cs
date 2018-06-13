using ItemsBasket.Client.Interfaces;
using ItemsBasket.AuthenticationService.Models;
using System.Threading.Tasks;
using ItemsBasket.AuthenticationService.Responses;

namespace ItemsBasket.Client
{
    public class AuthenticationService : BaseServiceCaller, IAuthenticationService
    {
        /// <summary>
        /// Default constructor. To be used in scenarios where Dependency Injection 
        /// is not available.
        /// </summary>
        public AuthenticationService()
            : base(new EnvironmentService(), new HttpClientProvider(), false)
        {
        }

        /// <summary>
        /// Dependency Injection friendly constructor. 
        /// </summary>
        /// <param name="environmentService">The environment service containing endpoint information.</param>
        /// /// <param name="httpClientProvider">The http client provider for authenticatead and non authenticated clients.</param>
        public AuthenticationService(IEnvironmentService environmentService, IHttpClientProvider httpClientProvider)
            : base(environmentService, httpClientProvider, false)
        {
        }

        public async Task<AuthenticationResponse> TryLogin(string username, string password)
        {
            return await PostCall(
                $"{EnvironmentService.ServiceEndpoints[KnownService.AuthenticationService]}",
                new AuthenticationRequest(username, password),
                e =>
                {
                    return AuthenticationResponse.CreateFailedResult(e.Message);
                });
        }
    }
}