using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.DbContexts;
using ProductMicroservice.Dto;
using ProductMicroservice.Models;
using ProductMicroservice.Services;

namespace ProductMicroservice.Controllers
{
    [Route("api/rest/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductMicroserviceDbContext _context;
        public readonly IApiService _api;

        public ProductController(ProductMicroserviceDbContext context, IApiService api)
        {
            _context = context;
            _api = api ??
                throw new ArgumentNullException(nameof(api));
        }




        [HttpPost("register")]
        public async Task<IActionResult> RegisterProduct(ProductDto data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve the JWT token from the Authorization header
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Replace("Bearer ", "");

            HttpResponseMessage authresponse = await _api.isAuthorized(token);

            if (!authresponse.IsSuccessStatusCode)
            {
                return Unauthorized();
            }


            bool hasConflitname = _context.Products.Where(x => x.Name==data.Name).Any();

            if (hasConflitname)
            {
                return Conflict();
            }


            Product new_record = new Product()
            {
                PId = Guid.NewGuid(),
                Name = data.Name,
                Brand = data.Brand,
                Category = data.Category,
                Price = data.Price,
                Description = data.Description,
                Image = data.Image

            };
            await _context.Products.AddAsync(new_record);
            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id , [FromBody] ProductDto data)
        {
            // Retrieve the JWT token from the Authorization header
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Replace("Bearer ", "");

            HttpResponseMessage authresponse = await _api.isAuthorized(token);

            if (!authresponse.IsSuccessStatusCode)
            {
                return Unauthorized();
            }

            var record = await _context.Products.FindAsync(id);

            if (record ==null)
            {
                return NotFound();
            }

            bool hasConflitname = _context.Products.Where(x => x.Name == data.Name && x.PId != id).Any();

            if (hasConflitname)
            {
                return Conflict();
            }

            record.Name = data.Name;
            record.Brand = data.Brand;
            record.Category = data.Category;
            record.Price = data.Price;
            record.Description = data.Description;
            record.Image = data.Image;


            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {

            // Retrieve the JWT token from the Authorization header
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Replace("Bearer ", "");

            HttpResponseMessage authresponse = await _api.isAuthorized(token);

            if (!authresponse.IsSuccessStatusCode)
            {
                return Unauthorized();
            }


            var record = await _context.Products.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }



            _context.Products.Remove(record);
            await _context.SaveChangesAsync();

            return Ok();
        }




    }
}
