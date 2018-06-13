namespace ItemsBasket.AuthenticationService.Controllers
{
    public class BaseResponse
    {
        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }

        protected BaseResponse(bool isSuccessful, string errorMessage)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Item { get; }
        
        protected BaseResponse(T item, bool isSuccessful, string errorMessage)
            : base(isSuccessful, errorMessage)
        {
            Item = item;
        }
    }
}