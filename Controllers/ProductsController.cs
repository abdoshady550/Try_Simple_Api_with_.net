﻿using Asp.net_Web_Api.Data;
using Asp.net_Web_Api.Entities;
using Asp.net_Web_Api.Interface;
using Asp.net_Web_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Asp.net_Web_Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class ProductsController : ControllerBase
    {

        private readonly IProductService _iproductservice;

        public ProductsController(AppDbContext dbContext, IProductService iproductservice)
        {

            _iproductservice = iproductservice;

        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllProducts()
        {
            var username = User.Identity.Name;
            var all = await _iproductservice.GetAllProductsService();
            return Ok(all);

        }
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult> CreateProduct(Product product)
        {

            var created = await _iproductservice.CreateProductService(product);

            return Ok(created.ToString());

        }

        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<ActionResult> UpdateProductById(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _iproductservice.UpdateProductByIdService(product);

            if (!updated) return NotFound();

            return Ok($"Product with ID {product.Id} Update successfully.");
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteProductById(int id)
        {

            var deletedItem = await _iproductservice.DeleteProductByIdService(id);
            if (!deletedItem) return NotFound();
            return Ok($"Product with ID {id} deleted successfully.");
        }


    }
}
