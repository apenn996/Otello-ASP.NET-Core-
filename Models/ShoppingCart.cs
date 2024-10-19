namespace Otello.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public int VariationId { get; set; }
        public double TotalCost { get; set; }

        public ShoppingCart(string userId, int quantity, int variationId, double totalCost)
        {
            this.UserId = userId;
            this.Quantity = quantity;
            this.VariationId = variationId;
            this.TotalCost = totalCost;
        }
        public ShoppingCart()
        {
            
        }
    }
}
