using System;
using System.Collections.Generic;
using api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface  IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(AppUser user);
    }
}