using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMicroservice.DbContexts;

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
    }
}
