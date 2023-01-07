using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using products_catalog.Controllers;
using products_catalog.Helpers;
using products_catalog.Models;
using System.Diagnostics;

namespace products_catalog.Pages
{
	public class RegisterUserModel : PageModel
	{
		[BindProperty]
		public UserModel User { get; set; }

		public IActionResult OnPost()
		{
			RegisterUser();
			int isLogged = Convert.ToInt32(HttpContext.Session.GetInt32("isLogged"));
			return isLogged == 1 ? RedirectToPage("LoginUser") : null;
		}

		public void RegisterUser()
		{
			string login = string.Format("{0}", Request.Form["User.UserName"]);
			string password = string.Format("{0}", Request.Form["User.Password"]);
			string encryptedPassword = PasswordManager.EncryptPass(password);
			string email = string.Format("{0}", Request.Form["User.Email"]);

			using MySqlConnection conn = new(ConnectionHelper.ConnectionString);
			conn.Open();
			using MySqlCommand cmd = new()
			{
				Connection = conn,
				CommandText = "INSERT INTO users SET username=@userLogin, password=@userPassword, email=@userEmail",
			};
			cmd.Parameters.AddWithValue("@userLogin", login);
			cmd.Parameters.AddWithValue("@userPassword", encryptedPassword);
			cmd.Parameters.AddWithValue("@userEmail", email);
			cmd.ExecuteNonQuery();
			conn.Close();
		}
	}
}