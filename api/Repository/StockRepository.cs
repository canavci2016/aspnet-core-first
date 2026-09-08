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
  public class StockRepository : IStockRepository
  {

    private readonly ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Stock> CreateStockAsync(Stock stock)
    {
      await _context.Stocks.AddAsync(stock);
      await _context.SaveChangesAsync();
      return stock;
    }

    public async Task DeleteStockAsync(int id)
    {
      var stock = await _context.Stocks.FindAsync(id);
      if (stock != null)
      {
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
      }
    }

    public Task<List<Stock>> GetAllStocksAsync()
    {
      return _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetStockByIdAsync(int id)
    {
      return await _context.Stocks.FindAsync(id);
    }

    public async Task UpdateStockAsync(Stock stock)
    {
      _context.Stocks.Update(stock);
      await _context.SaveChangesAsync();
    }
  }
}