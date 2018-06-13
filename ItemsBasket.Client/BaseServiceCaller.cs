using ItemsBasket.Client.Extensions;
using ItemsBasket.Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ItemsBasket.Client
{
    public abstract class BaseServiceCaller
    {
        private readonly IEnvironmentService _environmentService;
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly bool _authenticate;

        protected IEnvironmentService EnvironmentService { get { return _environmentService; } }
        protected IHttpClientProvider HttpClientProvider { get { return _httpClientProvider; } }

        protected BaseServiceCaller(IEnvironmentService environmentService, 
            IHttpClientProvider httpClientProvider, 
            bool authenticate)
        {
            _environmentService = environmentService;
            _httpClientProvider = httpClientProvider;
            _authenticate = authenticate;
        }

        /// <summary>
        /// Performs a GET call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> GetCall<TResponse>(string endpoint,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall<object, TResponse, TResponse>(endpoint,
                null,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.GetAsync(uri); },
                response => response,
                exceptionFunc);
        }

        /// <summary>
        /// Performs a POST call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the POST body that will be sent to the API.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PostCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.PostAsync(uri, content); },
                responseFunc,
                exceptionFunc);
        }

        /// <summary>
        /// Performs a POST call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the POST body that will be sent to the API.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PostCall<TRequest, TResponse>(string endpoint,
            TRequest requestObject,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall<TRequest, TResponse, TResponse>(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.PostAsync(uri, content); },
                response => response,
                exceptionFunc);
        }

        /// <summary>
        /// Performs a PUT call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the PUT body that will be sent to the API.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PutCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.PutAsync(uri, content); },
                responseFunc,
                exceptionFunc);
        }

        /// <summary>
        /// Performs a PUT call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the PUT body that will be sent to the API.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PutCall<TRequest, TResponse>(string endpoint,
            TRequest requestObject,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall<TRequest, TResponse, TResponse>(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.PutAsync(uri, content); },
                response => response,
                exceptionFunc);
        }

        /// <summary>
        /// Performs a DELETE call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the DELETE body that will be sent to the API.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> DeleteCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.DeleteAsync(uri); },
                responseFunc,
                exceptionFunc);
        }

        /// <summary>
        /// Performs a DELETE call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the DELETE body that will be sent to the API.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> DeleteCall<TRequest, TResponse>(string endpoint,
            TRequest requestObject,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall<TRequest, TResponse, TResponse>(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.DeleteAsync(uri); },
                response => response,
                exceptionFunc);
        }

        /// <summary>
        /// Performs an HTTP call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the POST body that will be sent to the API.</param>
        /// <param name="requestFunc">The func that will execute the HTTP call. This could be GET/POST/PUT/DELETE etc.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <param name="authenticate">A flag to denote if the request should be authenticated If set to true a bearer value 
        /// will be added to the authentication header.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PerformHttpCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<HttpClient, string, StringContent, Task<HttpResponseMessage>> requestFunc,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            var httpClient = _authenticate 
                ? _httpClientProvider.AuthenticatedClient 
                : _httpClientProvider.NonAuthenticatedClient;

            var postData = requestObject != null
                ? new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, "application/json")
                : new StringContent("", Encoding.UTF8, "application/text");

            try
            {
                var response = await requestFunc(httpClient, endpoint, postData);

                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<TRequestResponse>(responseString);

                return responseFunc(responseObject);
            }
            catch (Exception e)
            {
                return exceptionFunc(e);
            }
        }
    }
}