using Microsoft.AspNetCore.Mvc;

namespace Otello.Controllers
{
	public class BrandsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
