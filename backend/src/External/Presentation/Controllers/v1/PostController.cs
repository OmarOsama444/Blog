using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class PostController(IPostService postService, IELasticService eLasticService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostResponseDto>> CreatePost([FromBody] CreatePostRequestDto requestDto)
        {
            var userid = User.GetUserId();
            return Ok(await postService.CreatePostAsync(userid, requestDto));
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<PostResponseDto>>> SearchPost([FromQuery] SearchPostRequestDto requestDto)
        {
            return Ok(await postService.SearchPost(requestDto));
        }

        [HttpGet("semantic")]
        public async Task<ActionResult<ICollection<PostResponseDto>>> GetAllPosts([FromQuery] SearchPostRequestDto requestDto)
        {
            return Ok(await eLasticService.SearchPostSemantic(requestDto));
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeletePost([FromBody] ICollection<Guid> PostIds)
        {
            var userid = User.GetUserId();
            await postService.DeletePost(userid, PostIds);
            return NoContent();
        }
    }
}