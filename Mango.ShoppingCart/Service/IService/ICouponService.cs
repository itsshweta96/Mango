using Mango.ShoppingCart.Models.Dto;


namespace Mango.ShoppingCart.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
