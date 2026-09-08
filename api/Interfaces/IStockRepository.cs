using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {

      Task<List<Stock>> GetAllStocksAsync();
      Task<Stock?> GetStockByIdAsync(int id);
      Task<Stock> CreateStockAsync(Stock stock);
      Task UpdateStockAsync(Stock stock);
      Task DeleteStockAsync(int id);
    }
}