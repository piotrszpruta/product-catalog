using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using products_catalog.Controllers;
using products_catalog.Helpers;
using products_catalog.Models;

namespace products_catalog.Pages
{
	public class AddProductModel : PageModel
	{
		public SinkModel Product { get; set; }

		public IActionResult OnPost()
		{
			AddProduct();
			int isLogged = Convert.ToInt32(HttpContext.Session.GetInt32("isLogged"));
			return isLogged == 1 ? RedirectToPage("Index") : null;
		}

		public void AddProduct()
		{
			string model = string.Format("{0}", Request.Form["Product.Model"]);
			string descr = string.Format("{0}", Request.Form["Product.Descr"]);
			string width = string.Format("{0}", Request.Form["Product.Width"]);
			string length = string.Format("{0}", Request.Form["Product.Length"]);
			string weight = string.Format("{0}", Request.Form["Product.Weight"]);
			string chamberCount = string.Format("{0}", Request.Form["Product.ChamberCount"]);
			string imageBase64 = string.Format("{0}", Request.Form["Product.Base64Img"]);

			using MySqlConnection conn = new(ConnectionHelper.ConnectionString);
			conn.Open();
			using MySqlCommand cmd = new()
			{
				Connection = conn,
				CommandText = "INSERT INTO products_data SET model=@model, description=@descr, width=@width, length=@length, weight=@weight, chamber_variant=@chamberCount, base64img=@imageBase64",
			};
			cmd.Parameters.AddWithValue("@model", model);
			cmd.Parameters.AddWithValue("@descr", descr);
			cmd.Parameters.AddWithValue("@width", width);
			cmd.Parameters.AddWithValue("@length", length);
			cmd.Parameters.AddWithValue("@weight", weight);
			cmd.Parameters.AddWithValue("@chamberCount", chamberCount);
			cmd.Parameters.AddWithValue("@imageBase64", imageBase64);
			cmd.ExecuteNonQuery();
			conn.Close();
		}
	}
}
