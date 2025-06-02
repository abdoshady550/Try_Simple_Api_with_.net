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


    }
}
