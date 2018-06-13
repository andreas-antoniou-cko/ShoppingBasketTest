using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Services.Interfaces
{
    public interface IAuthorizationLayer
    {
        Task<T> ExecuteAuthorizedAction<T>(IIdentity identity,
            Func<int, Task<T>> func,
            Func<string, T> errorFunc,
            string genericErrorMessage);
    }
}