using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using products_catalog.Controllers;
using products_catalog.Helpers;
using products_catalog.Models;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace products_catalog.Pages
{
	public class LoginUserModel : PageModel
	{
		public UserModel User { get; set; }

		public IActionResult? Index()
		{
			int isLogged = Convert.ToInt32(HttpContext.Session.GetInt32("isLogged"));
			return isLogged == 1 ? RedirectToPage("Index") : null;
		}

		public IActionResult? OnGet()
		{
			int isLogged = Convert.ToInt32(HttpContext.Session.GetInt32("isLogged"));
			return isLogged == 1 ? RedirectToPage("Index") : null;
		}

		public IActionResult? OnPost()
		{
			LoginUser();
			int isLogged = Convert.ToInt32(HttpContext.Session.GetInt32("isLogged"));
			return isLogged == 1 ? RedirectToPage("Index") : null;
		}

		public async Task LoginUser()
		{
			string login = string.Format("{0}", Request.Form["User.UserName"]);
			string password = PasswordManager.EncryptPass(string.Format("{0}", Request.Form["User.Password"]));
			int i = 0;
			string name = "";
			if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(login))
			{
				await using MySqlConnection conn = new(ConnectionHelper.ConnectionString);
				await conn.OpenAsync();

				await using MySqlCommand cmd = new()
				{
					Connection = conn,
					CommandText = "SELECT * FROM users WHERE username=@userLogin AND password=@userPassword",
				};
				cmd.Parameters.AddWithValue("@userLogin", login);
				cmd.Parameters.AddWithValue("@userPassword", password);

				using MySqlDataReader dtr = await cmd.ExecuteReaderAsync();
				if (await dtr.ReadAsync())
				{
					i = dtr.FieldCount > 0 ? 1 : 0;
					if (i == 0)
					{
						HttpContext.Session.SetString("ErrorMsg", "Wrong username or/and password.");
					}
					else
					{
						name = dtr.GetString("username");
						HttpContext.Session.SetInt32("isLogged", i);
						HttpContext.Session.SetString("username", name);
					}
				}
				else
				{
					HttpContext.Session.SetString("ErrorMsg", "Wrong username or/and password.");
				}

				await conn.CloseAsync();
			}

		}
	}
}
