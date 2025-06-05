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
        public async Task<ActionResult> CreateProduct(Product product)
        {

            _dbcontext.Products.Add(product);
            await _dbcontext.SaveChangesAsync();
            return Ok($"product {product.Name} added successfully");

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
        [Route("{id}")]
        public async Task<ActionResult> DeleteProductById(int id)
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
        public async Task<ActionResult> UpdateProductById(Product product)
        {

            var updatedItem = await _dbcontext.Products.
                FindAsync(product.Id);

            if (updatedItem == null)
            {

                return NotFound("cann't find this item");

            }
            else

                updatedItem.Name = product.Name;
            updatedItem.Sku = product.Sku;

            _dbcontext.Products.Update(updatedItem);
            await _dbcontext.SaveChangesAsync();

            return Ok($"Product with ID {product.Id} Updated successfully.");
        }

    }
}
