using System;

namespace ItemsBasket.AuthenticationService.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message)
            : base(message)
        {
        }
    }
}