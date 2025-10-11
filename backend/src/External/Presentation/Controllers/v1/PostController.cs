using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Requests;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Extensions;

namespace Presentation.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class PostController(IPostService postService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(CreatePostRequestDto requestDto)
        {
            var userid = User.GetUserId();
            return await postService.CreatePostAsync(userid, requestDto);
        }
    }
}