using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.DbContexts;
using ProductMicroservice.Dto;
using ProductMicroservice.Models;

namespace ProductMicroservice.Controllers
{
    [Route("api/rest/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductMicroserviceDbContext _context;

        public ProductController(ProductMicroserviceDbContext context)
        {
            _context = context;
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register(ProductDto data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] ProductDto data)
        {


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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {


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
