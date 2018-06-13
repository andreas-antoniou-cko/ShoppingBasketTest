using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ItemsBasket.AuthenticationService.Services
{
    public class AuthorizationLayer : IAuthorizationLayer
    {
        private readonly ILogger<AuthorizationLayer> _logger;

        public AuthorizationLayer(ILogger<AuthorizationLayer> logger)
        {
            _logger = logger;
        }

        public async Task<T> ExecuteAuthorizedAction<T>(IIdentity identity, 
            Func<int, Task<T>> func, 
            Func<string, T> errorFunc, 
            string genericErrorMessage)
        {
            try
            {
                bool isAuthorized = TryGetAuthorizedUserId(identity, out int nameIdentifier);

                if (!isAuthorized)
                {
                    return errorFunc.Invoke("The user is not authorized to execute this action.");
                }

                return await func(nameIdentifier);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return errorFunc.Invoke(genericErrorMessage);
            }
        }

        private static bool TryGetAuthorizedUserId(IIdentity identity,
            out int nameIdentifier)
        {
            var claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                nameIdentifier = -1;
                return false;
            }

            foreach (var claim in claimsIdentity.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    return int.TryParse(claim.Value, out nameIdentifier);
                }
            }

            nameIdentifier = -1;
            return false;
        }
    }
}