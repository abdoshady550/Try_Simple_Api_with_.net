using Asp.net_Web_Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_Web_Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;
        public ProductsController(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> CreateProduct(Product product)
        {

            _dbcontext.Products.Add(product);
            await _dbcontext.SaveChangesAsync();
            return Ok(product.Id);

        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllProducts()
        {
            var all = await _dbcontext.Products.Select(x => new
            {
                x.Id,
                x.Name,
                x.Sku
            }).ToListAsync();
            return Ok(all);

        }
        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<int>> DeleteProductById(int id)
        {

            var deletedItem = await _dbcontext.Products.
                FirstOrDefaultAsync(x => x.Id == id);
            if (deletedItem == null)
            {

                return NotFound("cann't find this item");
            }
            else
                _dbcontext.Products.Remove(deletedItem);
            await _dbcontext.SaveChangesAsync();

            return Ok($"Product with ID {id} deleted successfully.");
        }
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<int>> UpdateProductById(int id, string name, string sku)
        {

            var updatedItem = await _dbcontext.Products.
                FirstOrDefaultAsync(x => x.Id == id);
            if (updatedItem == null)
            {

                return NotFound("cann't find this item");

            }
            else

                updatedItem.Name = name;
            updatedItem.Sku = sku;


            await _dbcontext.SaveChangesAsync();

            return Ok($"Product with ID {id} Updated successfully.");
        }

    }
}
