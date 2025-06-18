using Asp.net_Web_Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_Web_Api.Interface
{
    public interface IProductService
    {
        Task<object> CreateProductService(Product product);
        Task<ActionResult<IEnumerable<object>>> GetAllProductsService();

        Task<bool> UpdateProductByIdService(Product product);
        Task<bool> DeleteProductByIdService(int id);
    }
}
