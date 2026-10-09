using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
  public class TokenService : ITokenService
  {
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration configuration)
    {
      _configuration = configuration;
      _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    }

    public string CreateToken(AppUser appUser)
    {
      var claims = new List<Claim>
      {
        new(ClaimTypes.NameIdentifier, appUser.UserName ?? string.Empty)
      };

      var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = credentials
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}