using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
  public static class StockMapper
  {
    public static StockDto ToStockDto(this Stock stock)
    {
      return new StockDto
      {
        Id = stock.Id,
        Symbol = stock.Symbol,
        CompanyName = stock.CompanyName,
        Price = stock.Price,
        LastDiv = stock.LastDiv,
        Industry = stock.Industry,
        MarketCap = stock.MarketCap
      };
    }

    public static Stock StockFromCreateRequestDto(this CreateStockRequestDto dto)
    {
      return new Stock
      {
        Symbol = dto.Symbol,
        CompanyName = dto.CompanyName,
        Price = dto.Price,
        LastDiv = dto.LastDiv,
        Industry = dto.Industry,
        MarketCap = dto.MarketCap
      };
    }
  }
}