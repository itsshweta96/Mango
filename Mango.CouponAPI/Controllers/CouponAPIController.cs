using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mango.CouponAPI.Models;
using Mango.CouponAPI.Data;
using Mango.Services.Shared.Models;
using AutoMapper;
using Mango.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;


//[Authorize] // using Microsoft.AspNetCore.Authorization;
//[Route("api/[controller]")]

[Route("api/coupons")]
[ApiController]
[Authorize(Roles ="ADMIN")]
public class CouponAPIController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    private ResponseDto _response;// using Mango.Services.Shared.Models;
    private IMapper _mapper;
    public CouponAPIController(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    // GET: api/Coupon
    // [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ResponseDto>>GetCoupon()
    {
        try
        {
            IEnumerable<Coupon> objList = await _db.Coupons.ToListAsync();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList); //obj type then destination type
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.Message;
        }
        if(!_response.IsSuccess) return BadRequest(_response);
        return Ok(_response);
    }

    // GET: api/Coupon/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Coupon>> GetCouponById(int id)
    {
        try
        {
            var obj = await _db.Coupons.FindAsync(id);
            if (obj == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "Coupon not found";
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.Message;
        }
        if (!_response.IsSuccess)
        {
            return BadRequest(_response);
        }
        return Ok(_response);     
    }

    [AllowAnonymous]
    [HttpGet("GetByCode/{code}")]
    public async Task<ActionResult<Coupon>> GetCouponByCode(string code)
    {
        try
        {
            var obj = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponCode.ToLower() == code.ToLower());
            if (obj == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "Coupon not found";
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.Message;
        }
        if (!_response.IsSuccess)
        {
            return BadRequest(_response);
        }
        return Ok(_response);
    }

    // PUT: api/Coupon/5
    [HttpPut]
    public async Task<IActionResult> UpdateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);
            if(obj.CouponId == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "Coupon Id is required";
                return BadRequest(_response);
            }
            _db.Coupons.Update(obj);
            await _db.SaveChangesAsync();
            _response.Result = _mapper.Map<CouponDto>(couponDto);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.Message;
        }
        if (!_response.IsSuccess)
        {
            return BadRequest(_response);
        }
        return Ok(_response);
    }

    // POST: api/Coupon
    [HttpPost]
    public async Task<ActionResult<ResponseDto>> CreateCoupon([FromBody] CouponDto couponDto)
    {
        try
        {
            if(couponDto.CouponId > 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "Coupon Id must be empty for creating coupon";
                return BadRequest(_response);
            }
           Coupon obj =  _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(obj);
            await _db.SaveChangesAsync();
            _response.Result = _mapper.Map<CouponDto>(couponDto);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.Message;
        }
        if (!_response.IsSuccess) return BadRequest(_response);
        return Ok(_response);       
    }

    // DELETE: api/Coupon/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCoupon(int id)
    {
        try
        {
            var coupon = await _db.Coupons.FindAsync(id);
            if(coupon == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "Coupon not found";
                return NotFound(_response);
            }
            _db.Coupons.Remove(coupon);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.Message;
        }
        if (!_response.IsSuccess) return BadRequest(_response);
        return Ok(_response);
    }
}
