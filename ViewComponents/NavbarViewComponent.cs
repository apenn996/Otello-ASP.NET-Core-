using Microsoft.AspNetCore.Mvc;
using Otello.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Otello.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public NavbarViewComponent(ApplicationDbContext context)
        {
            db = context;
        }
       
        public IViewComponentResult Invoke()
        {
            ViewBag.ShoppingCartModel = db.ShoppingCart.ToList();
            ViewBag.ProductImagesModel = db.ProductImages.ToList();
            ViewBag.ProductModel = db.Product.ToList();
            return View("Navbar.cshtml");
        }
    }
}
