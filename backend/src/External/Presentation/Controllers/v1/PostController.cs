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
        public async Task<ActionResult<ICollection<PostResponseDto>>> CreatePost([FromBody] ICollection<CreatePostRequestDto> requestDtos)
        {
            var userid = User.GetUserId();
            ICollection<PostResponseDto> results = [];
            foreach (var requestDto in requestDtos)
                results.Add(await postService.CreatePostAsync(userid, requestDto));
            return Ok(results);
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