using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  public class CommentController : ControllerBase
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
      _commentRepository = commentRepository;
      _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll()
    {
      var comments = await _commentRepository.GetAllAsync();
      var commentDtos = comments.Select(cmd => cmd.ToCommentDto());
      return Ok(commentDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDto>> GetById(int id)
    {
      var comment = await _commentRepository.GetByIdAsync(id);
      if (comment == null)
      {
        return NotFound();
      }
      return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<ActionResult<CommentDto>> Create(int stockId, CreateCommentRequestDto createCommentRequestDto)
    {
      if (!await _stockRepository.StockExists(stockId))
      {
        return BadRequest("Stock does not exist.");
      }
      var commentModel = createCommentRequestDto.ToCommentFromCreateCommentRequestDto(stockId);
      await _commentRepository.CreateAsync(commentModel);
      return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> Update(int id, UpdateCommentRequestDto updateCommentRequestDto)
    {
      var updatedComment = await _commentRepository.UpdateAsync(id, updateCommentRequestDto.ToCommentFromUpdateRequestDto());
      if (updatedComment == null)
      {
        return NotFound("Comment not found.");
      }
      return Ok(updatedComment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      var comment = await _commentRepository.DeleteAsync(id);
      if (comment == null)
      {
        return NotFound("Comment not found.");
      }
      return Ok(comment.ToCommentDto());
    }

  }
}