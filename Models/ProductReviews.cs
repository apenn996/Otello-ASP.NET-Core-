using System.Reflection.Metadata.Ecma335;

namespace Otello.Models
{
    public class ProductReviews
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Message { get; set; }
        public float Rating { get; set; }

        public ProductReviews()
        {
            
        }
    }
}
