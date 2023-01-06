namespace products_catalog.Models
{
	public class UserModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}

	public class SinkModel
	{
		public int Id { get; set; }
		public string Model { get; set; }
		public string Descr { get; set; }
		public string Base64Img { get; set; }
		public float Width { get; set; }
		public float Length { get; set; }
		public float Weight { get; set; }
		public string ChamberCount { get; set; }
	}
}
