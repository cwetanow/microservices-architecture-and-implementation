using Discount.Domain.Data;
using Discount.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountData data;

        public DiscountController(IDiscountData data)
        {
            this.data = data;
        }

        [HttpGet("{productId}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productId) => Ok(await data.GetDiscount(productId));

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await data.CreateDiscount(coupon);
            return CreatedAtRoute(nameof(GetDiscount), new { coupon.ProductId }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateBasket([FromBody] Coupon coupon)
        {
            await data.UpdateDiscount(coupon);
            return Ok(coupon);
        }

        [HttpDelete("{productId}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteDiscount(string productId)
        {
            await data.DeleteDiscount(productId);
            return NoContent();
        }
    }
}
