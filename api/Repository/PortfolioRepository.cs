using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
  public class PortfolioRepository : IPortfolioRepository
  {
    private readonly ApplicationDbContext _context;

    public PortfolioRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Stock>> GetUserPortfolioAsync(AppUser user)
    {
      return await _context.Portfolios.Where(s => s.AppUserId == user.Id).Select(s => new Stock
      {
        Id = s.StockId,
        Symbol = s.Stock.Symbol,
        CompanyName = s.Stock.CompanyName,
        LastDiv = s.Stock.LastDiv,
        Industry = s.Stock.Industry,
        Price = s.Stock.Price
      }).ToListAsync();
    }
  }
}