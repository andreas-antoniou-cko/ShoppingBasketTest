using ItemsBasket.Common.Models;

namespace ItemsBasket.Common.Extentions
{
    public static class ItemDetailsExtensions
    {
        public static bool IsEmpty(this ItemDetails itemDetails)
        {
            return itemDetails == ItemDetails.Empty;
        }
    }
}