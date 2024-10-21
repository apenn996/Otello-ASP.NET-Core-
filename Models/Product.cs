using System.Diagnostics.Contracts;

namespace Otello.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Brand { get; set; }   
        public string? Size { get; set; }    
        public int? colorId { get; set; }
        public string Description { get; set; }
        public float? Rating { get; set; }
        public int Stock { get; set; }
       
        public double Price { get; set; }
        public int CategoryId{ get; set; }
        public string Type { get; set; }
        public char Sex { get; set; }

        public string? Grouping { get; set; }
    }
}
