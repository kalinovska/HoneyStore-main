using AutoMapper;
using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoneyStore.Api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();

            if (comments == null)
            {
                return NoContent();
            }

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentService.GetCommentAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = _mapper.Map<CommentDto>(model);
            await _commentService.AddCommentAsync(comment);
            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentDto comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _commentService.AddCommentAsync(comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetCommentAsync(id);

            if (comment == null)
            {
                return BadRequest();
            }

            await _commentService.RemoveCommentAsync(id);

            return Ok(comment);
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetCommentsByProductId(int id)
        {
            var comments = await _commentService.GetCommentsByProductIdAsync(id);

            if (comments == null)
            {
                return NoContent();
            }

            return Ok(comments);
        }
    }
}
