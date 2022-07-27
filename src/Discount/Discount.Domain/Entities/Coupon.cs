namespace Discount.Domain.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public static Coupon CreateWithNoDiscount(string productId) => new Coupon { ProductId = productId, Amount = 0, Description = "No Discount" };

    }
}
