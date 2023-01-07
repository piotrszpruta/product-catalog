using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using products_catalog.Controllers;
using products_catalog.Models;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace products_catalog.Pages
{
	public class IndexModel : PageModel
	{
		public IndexModel()
		{
		}

		public List<SinkModel> LoadData()
		{
			List<SinkModel> models = new();
			using MySqlConnection conn = new(ConnectionHelper.ConnectionString);
			conn.Open();

			using MySqlCommand cmd = new()
			{
				Connection = conn,
				CommandText = "SELECT * FROM products_data",
			};

			using MySqlDataReader dtr = cmd.ExecuteReader();
			while (dtr.Read())
			{
				models.Add(new SinkModel
				{
					Model = dtr.GetString("model"),
					Id = dtr.GetInt32("id"),
					Descr = dtr.GetString("description"),
					Base64Img = dtr.GetString("base64img"),
					Width = dtr.GetFloat("width"),
					Length = dtr.GetFloat("length"),
					Weight = dtr.GetFloat("weight"),
					ChamberCount = dtr.GetString("chamber_variant"),
				});
			}
			conn.Close();
			return models;
		}
	}
}