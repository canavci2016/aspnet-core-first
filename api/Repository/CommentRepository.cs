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
  public class CommentRepository : ICommentRepository
  {

    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public Task<bool> CommentExists(int id)
    {
      return _context.Comments.AnyAsync(r => r.Id == id);
    }

    public async Task<Comment?> CreateAsync(Comment comment)
    {
      await _context.Comments.AddAsync(comment);
      await _context.SaveChangesAsync();
      return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
      var existingComment = await _context.Comments.FirstOrDefaultAsync(r => r.Id == id);
      if (existingComment == null)
      {
        return null;
      }
      _context.Comments.Remove(existingComment);
      await _context.SaveChangesAsync();
      return existingComment;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
      return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
      return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment?> UpdateAsync(int id, Comment commentDto)
    {
      var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
      if (existingComment == null)
      {
        return null;
      }
      existingComment.Title = commentDto.Title;
      existingComment.Content = commentDto.Content;

      _context.Comments.Update(existingComment);
      await _context.SaveChangesAsync();
      return existingComment;
    }
  }
}