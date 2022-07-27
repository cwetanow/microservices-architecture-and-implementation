using Discount.Domain.Data;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : Protos.DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountData data;

        public DiscountService(IDiscountData data)
        {
            this.data = data;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = new Domain.Entities.Coupon
            {
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                ProductId = request.Coupon.ProductId,
                Id = request.Coupon.Id
            };

            await data.CreateDiscount(coupon);

            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            await data.DeleteDiscount(request.ProductId);
            return new DeleteDiscountResponse
            {
                Success = true
            };
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var discount = await data.GetDiscount(request.ProductId);
            return new CouponModel
            {
                ProductId = discount.ProductId,
                Amount = (int)discount.Amount,
                Description = discount.Description,
                Id = discount.Id
            };
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var updatedCoupon = new Domain.Entities.Coupon
            {
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                ProductId = request.Coupon.ProductId,
                Id = request.Coupon.Id
            };

            await data.UpdateDiscount(updatedCoupon);

            return request.Coupon;
        }
    }
}
