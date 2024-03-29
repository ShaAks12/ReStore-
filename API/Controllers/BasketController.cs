using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController:BaseApiController
    {
        private readonly StoreContext context;

        public BasketController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "GetBasket")]

        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            Basket basket = await RetrieveBasket();

            if (basket == null) return NotFound();
            return MapBasketToDto(basket);
        }

        
        [HttpPost] //QueryString request api/basket?productId=3&quantity=2

        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            // get basket || create basket if basket dont exist

            var basket = await RetrieveBasket(); 
            //if basket is null then we gonna create a new basket
            if(basket == null) basket = CreateBasket();
            
            // get product
            var product = await context.Products.FindAsync(productId);

            if(product == null) return NotFound();
            
            // add item
            basket.AddItem(product, quantity);

            // save changes
            var result = await context.SaveChangesAsync() > 0;

            if(result) return CreatedAtRoute("GetBasket",MapBasketToDto(basket));

            return BadRequest(new ProblemDetails{Title = "Problem Saving item to basket"});
        }

        [HttpDelete]

        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // get basket
            var basket = await RetrieveBasket(); 

            if (basket==null) return NotFound();
            // remove item or reduce quantity
            var item = basket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return NotFound();

            item.Quantity -= quantity;
            if (item.Quantity == 0) basket.Items.Remove(item);

            //or


            // save changes
            var result = await context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails{Title = "Problem removing item from the basket"});
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(basket => basket.BuyerId == Request.Cookies["buyerId"]);

            // return await context.Baskets
            //     .Include(i => i.Items)
            //     .ThenInclude(p => p.Product)
            //     .FirstOrDefaultAsync(basket => basket.BuyerId == Request.Cookies["buyerId"]);
        }

        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();//here by using Guid it create a new unique identifier
            var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};
            Response.Cookies.Append("buyerId" , buyerId, cookieOptions);
            var basket = new Basket{BuyerId = buyerId};
            context.Baskets.Add(basket);
            return basket;

            // var buyerId = Guid.NewGuid().ToString();
            // var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            // Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            // var basket = new Basket { BuyerId = buyerId };
            // _context.Baskets.Add(basket);
            // return basket;
        }

        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };

            // return new BasketDto
            // {
            //     Id = basket.Id,
            //     BuyerId = basket.BuyerId,
            //     Items = basket.Items.Select(item => new BasketItemDto
            //     {
            //         ProductId = item.ProductId,
            //         Name = item.Product.Name,
            //         Price = item.Product.Price,
            //         PictureUrl = item.Product.PictureUrl,
            //         Type = item.Product.Type,
            //         Brand = item.Product.Brand,
            //         Quantity = item.Quantity
            //     }).ToList()
            // };
        }

    }
}