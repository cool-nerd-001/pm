using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMicroservice.DbContexts;
using ProductMicroservice.Dto;
using ProductMicroservice.Extensions;
using ProductMicroservice.Models;
using ProductMicroservice.Pagination;
using System.Collections;

namespace ProductMicroservice.Controllers
{
    [Route("api/rest/v1/")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly ProductMicroserviceDbContext _context;

        public ProductDetailsController(ProductMicroserviceDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllItems()
        {
            var records = _context.Products.Select(x => new { x.PId, x.Name, x.Price, x.Image });

            return Ok(records);
        }


        [HttpGet("products")]
        public async Task<IActionResult> GetItems([FromQuery] ProductParamsDto productParams)
        {
     

            var query = _context.Products
                            .Sort(productParams.OrderBy)
                            .Search(productParams.SearchTerm)
                            .Filter(productParams.Brands, productParams.Types)
                            .AsQueryable();

            var products = await PagedList<Product>.ToPagedList(query, 
                                                                productParams.PageNumber,
                                                                productParams.PageSize);

            Response.AddPaginationHeader(products.MetaData);
      

            return Ok(products);
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


        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(p => p.Category).Distinct().ToListAsync();

            return Ok(new { brands, types });
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
