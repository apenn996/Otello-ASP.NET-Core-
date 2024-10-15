namespace Otello.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }
        public string? Image6 { get; set; }
        public int colorId { get; set; }
        public bool variant {  get; set; }
    }
}
