using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
  public class CommentListRequestQuery
  {

    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;

  }
}