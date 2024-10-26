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
        public readonly double MaxPrice = 300;
        public double chosenMax { get; set; } = 300f;
        public double chosenMin { get; set; } = 0f;
        public readonly double MinPrice = 0;
        public List<string> UniqueName { get; set; }

       
    }
}
