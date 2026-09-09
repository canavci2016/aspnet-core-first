using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
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

    public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stock)
    {
      var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
      if (existingStock == null)
      {
        return null;
      }
      existingStock.Symbol = stock.Symbol;
      existingStock.CompanyName = stock.CompanyName;
      existingStock.Price = stock.Price;
      existingStock.LastDiv = stock.LastDiv;
      existingStock.Industry = stock.Industry;
      existingStock.MarketCap = stock.MarketCap;

      _context.Stocks.Update(existingStock);
      await _context.SaveChangesAsync();
      return existingStock;
    }
  }
}