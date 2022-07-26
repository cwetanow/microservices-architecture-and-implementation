using Discount.Domain.Entities;

namespace Discount.Domain.Data
{
    public interface IDiscountData
    {
        Task<Coupon> GetDiscount(string productId);

        Task CreateDiscount(Coupon coupon);
        Task UpdateDiscount(Coupon coupon);
        Task DeleteDiscount(string productId);
    }
}
