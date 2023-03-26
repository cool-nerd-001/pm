using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using ProductMicroservice.DbContexts;
using ProductMicroservice.Dto;
using System.Collections;
using System.Collections.Specialized;

namespace ProductMicroservice.Controllers
{
    [Route("api/rest/v1/productdetails")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly ProductMicroserviceDbContext _context;

        public ProductDetailsController(ProductMicroserviceDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetItems()
        {
            var records = _context.Products.Select(x => new { x.PId, x.Name, x.Price, x.Image });

            return Ok(records);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            var record = await _context.Products.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }


        [HttpPost("cartitems")]
        public async Task<IActionResult> GetItemsForCart([FromBody] ProductIdList obj)
        {
            ArrayList list = new ArrayList();


            foreach (var i in obj.array)
            {
                var record = await _context.Products.FindAsync(i);
                if (record == null)
                {
                    return NotFound();
                }
                ProductListForCartDto temp = new ProductListForCartDto();
                list.Add(new {PId = i ,Name = record.Name,Price=record.Price,Image=record.Image});
                
            }

            return Ok(list);
        }


    }
}
