using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Dtos.Comment;
using learn_dotnet.Interfaces;
using learn_dotnet.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace learn_dotnet.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(comment => comment.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment(
            [FromRoute] int stockId,
            [FromBody] CreateCommentRequestDto createCommentDto)
        {
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var newComment = createCommentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(newComment);

            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id },
            newComment.ToCommentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(
            [FromRoute] int id,
            [FromBody] UpdateCommentRequestDto updateCommentDto)
        {
            var comment = await _commentRepo.UpdateAsync(id, updateCommentDto.ToCommentFromUpdateDto());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }
    }
}