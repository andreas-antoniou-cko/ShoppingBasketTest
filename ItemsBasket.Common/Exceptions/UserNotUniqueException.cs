using System;

namespace ItemsBasket.AuthenticationService.Exceptions
{
    public class UserNotUniqueException : Exception
    {
        public UserNotUniqueException(string message)
            : base(message)
        {

        }
    }
}