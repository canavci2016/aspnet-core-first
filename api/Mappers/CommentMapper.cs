using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
  public static class CommentMapper
  {
    public static CommentDto ToCommentDto(this Comment comment)
    {
      return new CommentDto
      {
        Id = comment.Id,
        Title = comment.Title,
        Content = comment.Content,
        StockId = comment.StockId
      };
    }

    public static Comment ToCommentFromCreateCommentRequestDto(this CreateCommentRequestDto createCommentRequestDto, int stockId)
    {
      return new Comment
      {
        Id = createCommentRequestDto.Id,
        Title = createCommentRequestDto.Title,
        Content = createCommentRequestDto.Content,
        StockId = stockId,
      };
    }

    public static Comment ToCommentFromUpdateRequestDto(this UpdateCommentRequestDto updateCommentRequestDto)
    {
      return new Comment
      {
        Title = updateCommentRequestDto.Title,
        Content = updateCommentRequestDto.Content,
      };
    }
  }
}