using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using products_catalog.Controllers;

namespace products_catalog.Pages
{
	public class RemoveProductModel : PageModel
	{
		public void Remove(string id)
		{
			using MySqlConnection conn = new(ConnectionHelper.ConnectionString);
			conn.Open();

			using MySqlCommand cmd = new()
			{
				Connection = conn,
				CommandText = "DELETE FROM products_data WHERE id = @id",
			};
			cmd.Parameters.AddWithValue("id", id);
			cmd.ExecuteNonQuery();
		}
	}
}
