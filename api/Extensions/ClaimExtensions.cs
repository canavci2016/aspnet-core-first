using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
  public static class ClaimExtensions
  {

    public static string GetUserName(this ClaimsPrincipal user)
    {
      return user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
    }
  }
}