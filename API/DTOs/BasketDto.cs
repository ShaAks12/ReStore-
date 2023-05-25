namespace API.DTOs
{
    public class BasketDto
    {
        public int Id { get; set; }

        public string BuyerId { get; set; }

        public List<BasketItemDto> Items {get;set;}  //right click and generate method in new file from Quick fix

        //so instead of returning basket from our controller we gonna retrieve BasketDto
    }
}