using Microsoft.AspNetCore.Mvc;
using Otello.Data;
using Otello.Models;
using System.Diagnostics;

namespace Otello.Controllers
{
    public class HomeController : Controller
    {
         private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        //public HomeController(ILogger<HomeController> logger)
       // {
        //    _logger = logger;
       // }
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewModel model = new ViewModel();
           
            model.ProductModel = db.Product.ToList();
            model.ProductImagesModel = db.ProductImages.ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
