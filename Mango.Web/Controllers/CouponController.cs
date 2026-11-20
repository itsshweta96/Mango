using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? coupons = new();
            ResponseDto? responseDto = await _couponService.GetAllCouponsAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                //handle the success response
                // List<CouponDto>? coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
                coupons = responseDto.GetResult<List<CouponDto>>();
            }
            else
            {
                TempData["success"] = responseDto?.ErrorMessage;
            }
            return View(coupons);
        }

        public async Task<IActionResult> CouponCreate() //get endpoint
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if(ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.CreateCouponsAsync(model);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["success"] = responseDto?.ErrorMessage;
                }
            }
            
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId )
        {
                ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(couponId);
                if (responseDto != null && responseDto.IsSuccess)
                {
                CouponDto? model = responseDto.GetResult<CouponDto>();
                return View(model);
                }
                else
                {
                    TempData["success"] = responseDto?.ErrorMessage;
                }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? responseDto = await _couponService.DeleteCouponsAsync(couponDto.CouponId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                // Handle the successful response
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = responseDto?.ErrorMessage;
            }
            return View(couponDto);
        }
    }
}
