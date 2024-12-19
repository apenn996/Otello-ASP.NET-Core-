using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Otello.Models;

namespace Otello.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Otello.Models.Product>? Product { get; set; }
        public DbSet<Otello.Models.ProductImages>? ProductImages { get; set; }
        public DbSet<Otello.Models.ProductReviews>? ProductReviews { get; set; }
        
        public DbSet<Otello.Models.ShoppingCart>? ShoppingCart { get; set; }
        public DbSet<Otello.Models.ProductCategories>? ProductCategories { get; set; }
        public DbSet<Otello.Models.ProductVariations>? ProductVariations { get; set; }
        public DbSet<Otello.Models.ProductColors>? ProductColors { get; set; }
        public DbSet<Otello.Models.ProductSizes>? ProductSizes { get; set; }
		public object Brands { get; internal set; }
		public DbSet<Otello.Models.ProductBrands>? ProductBrands { get; set; }
	}
}
