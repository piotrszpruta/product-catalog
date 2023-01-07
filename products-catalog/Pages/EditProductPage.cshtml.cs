using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using products_catalog.Controllers;
using products_catalog.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace products_catalog.Pages
{
	public class EditProductPageModel : PageModel
	{
		public SinkModel Product { get; set; }
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

		public static SinkModel LoadData(string id)
		{
			SinkModel model = new();
			MySqlConnection conn = new(ConnectionHelper.ConnectionString);
			conn.Open();

			MySqlCommand cmd = new()
			{
				CommandText = "SELECT * FROM products_data WHERE id=@id",
				Connection = conn
			};
			cmd.Parameters.AddWithValue("@id", id);
			MySqlDataReader dtr = cmd.ExecuteReader();
			while (dtr.Read())
			{
				model.Model = dtr.GetString("model");
				model.Descr = dtr.GetString("description");
				model.Weight = dtr.GetFloat("weight");
				model.Width = dtr.GetFloat("width");
				model.Length = dtr.GetFloat("length");
			}

			return model;
		}
	}
}
