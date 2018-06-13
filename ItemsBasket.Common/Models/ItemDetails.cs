using System.Collections.Generic;

namespace ItemsBasket.Common.Models
{
    /// <summary>
    /// The details of an item which can be added to a basket.
    /// </summary>
    public class ItemDetails
    {
        /// <summary>
        /// An empy item details object.
        /// </summary>
        public static readonly ItemDetails Empty = new ItemDetails(-1, "", 0);

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

        public ItemDetails(int itemId, string description, decimal price)
        {
            ItemId = itemId;
            Description = description;
            Price = price;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ItemDetails;
            return details != null &&
                   ItemId == details.ItemId &&
                   Description == details.Description &&
                   Price == details.Price;
        }

        public override int GetHashCode()
        {
            var hashCode = -212936922;
            hashCode = hashCode * -1521134295 + ItemId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }
    }
}