namespace API.Entities
{
    public class Product
    {
        //protected acesss specifier mean a class that is derived from this class can only able to access this class
        public int Id { get; set; }
        public string Name{get; set;}

        public string Description{ get; set; }
        public long Price{get;set;}
        public string PictureUrl{get;set;}
        public string Type{get;set;}
        public string Brand{get;set;}
        public int QuantityInStock{ get; set; }
    }
}