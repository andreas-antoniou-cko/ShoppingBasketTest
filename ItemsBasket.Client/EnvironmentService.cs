using ItemsBasket.Client.Interfaces;
using System.Collections.Generic;

namespace ItemsBasket.Client
{
    public class EnvironmentService : IEnvironmentService
    {
        /// <summary>
        /// A dictionary of:
        ///     -   Key: A known service.
        ///     - Value: The endpoint of the service
        /// This is a crude implementation for the sake of the prototype. In the real world service discovery
        /// (like Consul) would be used. This would mean only 1 endpoint would be required (the load balanced
        /// service discovery endpoint).
        /// </summary>
        public IDictionary<KnownService, string> ServiceEndpoints { get; }

        public EnvironmentService()
        {
            ServiceEndpoints = new Dictionary<KnownService, string>
            {
                { KnownService.AuthenticationService, "http://localhost:8001/authentication/Authentication" },
                { KnownService.UserService, "http://localhost:8001/authentication/Users" },
                { KnownService.BasketService, "http://localhost:8002/basket/BasketItems" },
                { KnownService.ItemsService, "http://localhost:8003/items/Items" },
            };
        }
    }
}