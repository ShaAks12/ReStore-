using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : BaseApiController
    {
        private readonly StoreContext context;
        public ProductsController(StoreContext context)
        {
            this.context = context;  
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var products =  await context.Products.ToListAsync();

            return Ok(products);// when we use async method then we dont use this line 

            //or 

            //return await context.Products.ToListAsync();
        }

        [HttpGet("{id}")] //api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id){

            var product = await context.Products.FindAsync(id);

            if(product == null) return NotFound();

            return product;

        }
    }
}