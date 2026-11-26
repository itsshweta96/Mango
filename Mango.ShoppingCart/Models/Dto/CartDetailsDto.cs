using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.ShoppingCart.Models.Dto
{
    public class CartDetailsDto
    {
       [Required]
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeader? CartHeader { get; set; } // ?
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
 
        //[Range(1, 100, ErrorMessage = "Count must be between 1 and 100.")] for negative counts gives error
        public int Count { get; set; }
    }
}
