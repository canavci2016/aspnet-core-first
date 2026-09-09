using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StockController : ControllerBase
  {

    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepository;

    public StockController(ApplicationDbContext context, IStockRepository stockRepository)
    {
      _context = context;
      _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StockDto>>> Get()
    {
      var stocks = await _stockRepository.GetAllStocksAsync();
      var stockDtos = stocks.Select(s => s.ToStockDto());
      return Ok(stockDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StockDto>> GetById(int id)
    {
      var stock = await _context.Stocks.FindAsync(id);
      if (stock == null)
      {
        return NotFound();
      }
      return Ok(stock.ToStockDto());
    }
    [HttpPost]
    public async Task<ActionResult<StockDto>> Create(CreateStockRequestDto stock)
    {
      var newStock = stock.StockFromCreateRequestDto();

      await _context.Stocks.AddAsync(newStock);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetById), new { id = newStock.Id }, newStock.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateStockRequestDto stock)
    {
      await _stockRepository.UpdateStockAsync(id, stock);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      await _stockRepository.DeleteStockAsync(id);
      return NoContent();
    }
  }
}