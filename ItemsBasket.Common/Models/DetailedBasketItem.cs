using System.Collections.Generic;

namespace ItemsBasket.Common.Models
{
    /// <summary>
    /// A detailed basket item, which includes all the details of the item
    /// as well as the quantity of it in the basket.
    /// </summary>
    public class DetailedBasketItem
    {
        /// <summary>
        /// The empty instance of this object to be used instead of null.
        /// </summary>
        public static readonly DetailedBasketItem Empty = new DetailedBasketItem(-1, "", 0, 0);

        /// <summary>
        /// The item ID.
        /// </summary>
        public int ItemId { get; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The item's price.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// The quantity of this item in the basket.
        /// </summary>
        public int Quantity { get; }

        public DetailedBasketItem(int itemId, string description, decimal price, int quantity)
        {
            ItemId = itemId;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            var item = obj as DetailedBasketItem;
            return item != null &&
                   ItemId == item.ItemId &&
                   Description == item.Description &&
                   Price == item.Price &&
                   Quantity == item.Quantity;
        }

        public override int GetHashCode()
        {
            var hashCode = 1051018766;
            hashCode = hashCode * -1521134295 + ItemId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }
    }
}