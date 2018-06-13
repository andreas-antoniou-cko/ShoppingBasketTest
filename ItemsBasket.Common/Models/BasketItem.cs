namespace ItemsBasket.BasketService.Models
{
    public class BasketItem
    {
        public int ItemId { get; }
        public int Quantity { get; }

        public BasketItem(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}