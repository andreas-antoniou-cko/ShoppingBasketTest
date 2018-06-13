using System.Net.Http;

namespace ItemsBasket.Client.Interfaces
{
    public interface IHttpClientProvider
    {
        HttpClient NonAuthenticatedClient { get; }
        HttpClient AuthenticatedClient { get; }
        void SetAuthentication(string token);
    }
}