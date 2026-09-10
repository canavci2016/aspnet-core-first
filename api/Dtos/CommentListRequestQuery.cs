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
    public string? SortBy { get; set; } = null;
    public bool? SortByDesc { get; set; } = true;

    // Pagination
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

  }
}