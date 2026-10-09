using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
  public class NewUserDto
  {
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string Token { get; set; } = String.Empty;
  }
}