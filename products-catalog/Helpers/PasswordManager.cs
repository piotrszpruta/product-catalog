using System.Text;

namespace products_catalog.Helpers
{
	public class PasswordManager
	{
		public static string EncryptPass(string pass)
		{
			try
			{
				if (!string.IsNullOrEmpty(pass))
				{
					byte[] dataByte = new byte[pass.Length];
					dataByte = Encoding.UTF8.GetBytes(pass);
					string encryptedPass = Convert.ToBase64String(dataByte);

					return encryptedPass;
				}
				return null;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static string DecryptPassword(string pass)
		{
			UTF8Encoding utf8Encode = new();
			Decoder utf8Decode = utf8Encode.GetDecoder();
			byte[] todecode_byte = Convert.FromBase64String(pass);
			int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
			char[] decoded_char = new char[charCount];
			utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
			string result = decoded_char.ToString();
			return result;
		}
	}
}