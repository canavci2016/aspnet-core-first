using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
  public class UpdateCommentRequestDto
  {
    [Required]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = String.Empty;
    [Required]
    [MinLength(3, ErrorMessage = "Content must be at least 3 characters long.")]
    [MaxLength(100, ErrorMessage = "Content cannot be longer than 100 characters.")]
    public string Content { get; set; } = String.Empty;
  }
}