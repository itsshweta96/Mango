using System.ComponentModel.DataAnnotations;

namespace Mango.CouponAPI.Models
{
    public class Coupon
    {
        [Key] // using System.ComponentModel.DataAnnotations;
        public int CouponId { get; set; }

        [Required]
        public string CouponCode { get; set; }

        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
