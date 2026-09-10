using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
  public class CreateStockRequestDto
  {

    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot be longer than 10 characters.")]
    public string Symbol { get; set; } = String.Empty;


    [Required]
    [MaxLength(10, ErrorMessage = "CompanyName cannot be longer than 10 characters.")]
    public string CompanyName { get; set; } = String.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }

    public decimal LastDiv { get; set; }

    public string Industry { get; set; } = String.Empty;

    [Range(0, 5000000, ErrorMessage = "MarketCap must be a positive value.")]
    public long MarketCap { get; set; }
  }
}