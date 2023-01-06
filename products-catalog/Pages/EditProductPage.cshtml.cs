using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace products_catalog.Pages
{
	public class EditProductPageModel : PageModel
	{
		public void OnGet(string id)
		{
			ViewData["productId"] = id ?? "";
			DetectIfLogged();
		}

		public IActionResult DetectIfLogged()
		{
			int isLogged = Convert.ToInt32(HttpContext.Session.GetInt32("isLogged"));
			return isLogged == 1 ? null : RedirectToPage("Index");
		}
	}
}
