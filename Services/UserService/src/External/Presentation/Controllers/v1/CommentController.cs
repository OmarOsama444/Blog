using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CommentResponseDto>> CreateComment([FromBody] CreateCommentRequestDto request, CancellationToken cancellationToken)
        {
            var userid = User.GetUserId();
            var result = await commentService.CreateComment(userid, request, cancellationToken);
            return Ok(result);
        }
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<ICollection<CommentResponseDto>>> GetCommentsByPostId([FromRoute] Guid postId, CancellationToken cancellationToken)
        {
            var result = await commentService.GetCommentsByPostId(postId, cancellationToken);
            return Ok(result);
        }
        [HttpGet("replies/{commentId}")]
        public async Task<ActionResult<ICollection<CommentResponseDto>>> GetRepliesByCommentId([FromRoute] string commentId, CancellationToken cancellationToken)
        {
            var result = await commentService.GetRepliesByCommentId(commentId, cancellationToken);
            return Ok(result);
        }
    }
}