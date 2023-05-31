using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.DbContexts;
using ProductMicroservice.Dto;

namespace ProductMicroservice.Controllers
{
    [Route("api/rest/v1/verify")]
    [ApiController]
    public class DataVerifyController : ControllerBase
    {

        private readonly ProductMicroserviceDbContext _context;

        public DataVerifyController(ProductMicroserviceDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> CheckIfProductExist([FromRoute] Guid id)
        {
            var record = await _context.Products.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("products")]
        public async Task<IActionResult> CheckIfProductsExist([FromBody] ProductIdList obj)
        {
            foreach (var i in obj.array)
            {
                var record = await _context.Products.FindAsync(i);
                if (record == null)
                {
                    return NotFound();
                }

            }

            return Ok();
        }
    }
}
