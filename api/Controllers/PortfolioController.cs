using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

  [Route("api/portfolio")]
  [ApiController]
  public class PortfolioController : ControllerBase
  {

    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
    {
      _userManager = userManager;
      _stockRepository = stockRepository;
      _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPortfolio()
    {
      var username = User.GetUserName();
      Console.WriteLine(username);
      var appUser = await _userManager.FindByNameAsync(username);

      var portfolio = await _portfolioRepository.GetUserPortfolioAsync(appUser);
      return Ok(portfolio); 
    }

  }
}