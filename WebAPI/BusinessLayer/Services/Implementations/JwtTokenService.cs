using BusinessLayer.Constants;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services.Implementations
{
	public class JwtTokenService : ITokenService
	{
		private readonly IConfigurationService configurationService;

		public JwtTokenService(IConfigurationService configurationService)
		{
			this.configurationService = configurationService;
		}

		public string CreateToken(AuthenticatedUserDTO user)
		{
			var key = configurationService.GetSetting("JwtSettings:Key");
			var issuer = configurationService.GetSetting("JwtSettings:ValidIssuer");
			var audience = configurationService.GetSetting("JwtSettings:ValidAudience");
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, user.Role),
				new Claim(CustomClaimTypes.Id, user.Id.ToString()),
			};
			var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
			var tokenOptions = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.Now,
				signingCredentials: signingCredentials
			);
			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}
	}
}
