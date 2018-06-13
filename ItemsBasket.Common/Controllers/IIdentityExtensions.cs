using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Controllers
{
    public static class IIdentityExtensions
    {
        public static async Task<T> ExecuteAuthorizedAction<T>(this IIdentity identity,
            Func<int, Task<T>> func, 
            Func<string, T> errorFunc,
            string genericErrorMessage,
            ILogger logger)
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
            catch(Exception e)
            {
                logger.LogError(e, e.Message);
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

            foreach(var claim in claimsIdentity.Claims)
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