using BusinessLayer.Services.Interfaces;
using System.Security.Cryptography;

namespace BusinessLayer.Services.Implementations
{
	public class PasswordService : IPasswordService
	{
		public string HashPassword(string password)
		{
			byte[] salt = new byte[16];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}
			byte[] hash = new byte[32];
			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
			{
				hash = pbkdf2.GetBytes(32);
			}
			byte[] result = salt.Concat(hash).ToArray();
			return Convert.ToBase64String(result);
		}

		public bool VerifyPassword(string hashedPassword, string password)
		{
			byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
			byte[] salt = hashedPasswordBytes[..16];
			byte[] hash = hashedPasswordBytes[16..];
			byte[] computedHash = new byte[32];
			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
			{
				computedHash = pbkdf2.GetBytes(32);
			}
			return Enumerable.SequenceEqual(hash, computedHash);
		}
	}
}
