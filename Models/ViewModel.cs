using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace Otello.Models
{
    public class ViewModel
    { 
        public List<Product> ProductModel { get; set; }
        public List<ProductImages> ProductImagesModel { get; set; }
        public List<ShoppingCart> ShoppingCartModel { get; set; }
        public List<ProductReviews> ProductReviewsModel { get; set; }

        public List<CheckboxOptions> Checkboxes = new List<CheckboxOptions>();
        public readonly int MaxPrice = 300;
        public int chosenMax { get; set; } = 300;
        public int chosenMin { get; set; } = 1;
        public readonly int MinPrice = 1;
        public List<string> UniqueName { get; set; }
    }
}
