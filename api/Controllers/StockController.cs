using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StockController : ControllerBase
  {

    private readonly IStockRepository _stockRepository;

    public StockController(IStockRepository stockRepository)
    {
      _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StockDto>>> Get([FromQuery] CommentListRequestQuery query)
    {
      var stocks = await _stockRepository.GetAllStocksAsync(query);
      var stockDtos = stocks.Select(s => s.ToStockDto());
      return Ok(stockDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StockDto>> GetById(int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var stock = await _stockRepository.GetStockByIdAsync(id);
      if (stock == null)
      {
        return NotFound();
      }
      return Ok(stock.ToStockDto());
    }
    [HttpPost]
    public async Task<ActionResult<StockDto>> Create(CreateStockRequestDto stock)
    {
      var newStock= await _stockRepository.CreateStockAsync(stock.StockFromCreateRequestDto());

      return CreatedAtAction(nameof(GetById), new { id = newStock.Id }, newStock.ToStockDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateStockRequestDto stock)
    {
      await _stockRepository.UpdateStockAsync(id, stock);
      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      await _stockRepository.DeleteStockAsync(id);
      return NoContent();
    }
  }
}