using System.Collections.Generic;

namespace ItemsBasket.Client.Interfaces
{
    public interface IEnvironmentService
    {
        /// <summary>
        /// A dictionary of:
        ///     -   Key: A known service.
        ///     - Value: The endpoint of the service
        /// </summary>
        IDictionary<KnownService, string> ServiceEndpoints { get; }
    }
}