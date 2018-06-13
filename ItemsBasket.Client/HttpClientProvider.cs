using ItemsBasket.Client.Extensions;
using ItemsBasket.Client.Interfaces;
using System;
using System.Net.Http;

namespace ItemsBasket.Client
{
    public class HttpClientProvider : IHttpClientProvider, IDisposable
    {
        public HttpClient NonAuthenticatedClient { get; private set; }

        public HttpClient AuthenticatedClient { get; private set; }
        
        public void SetAuthentication(string token)
        {
            AuthenticatedClient.AddAuthorizationHeader(token);
        }

        public void Dispose()
        {
            NonAuthenticatedClient.Dispose();
            AuthenticatedClient.Dispose();
        }
    }
}