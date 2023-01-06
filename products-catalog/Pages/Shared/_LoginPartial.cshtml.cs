using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace products_catalog.Pages.Shared
{
	public class _LoginPartialModel : PageModel
	{
		public IActionResult? OnPost()
		{
			HttpContext.Session.SetInt32("isLogged", 0);
			HttpContext.Session.SetString("username", null);
			return RedirectToPage("Index");
		}
	}
}
