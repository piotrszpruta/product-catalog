using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace products_catalog.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			Debug.WriteLine("testestteststsetes121212");
			Response.WriteAsync("test");
			return View();
		}
	}
}
