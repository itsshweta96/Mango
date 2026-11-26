using Mango.ShoppingCart.Models.Dto;

namespace Mango.ShoppingCart.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
