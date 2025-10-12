using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Presentation.Fitlers;

namespace Presentation.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserRequestDto requestDto)
        {
            var userid = await userService.CreateUserAsync(requestDto);
            return Ok(userid);
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginUserResponse>> LoginUser([FromBody] LoginUserRequestDto requestDto)
        {
            var loginUserResponse = await userService.LoginUserAsync(requestDto);
            return Ok(loginUserResponse);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginUserResponse>> RefreshToken([FromBody] RefreshTokenRequestDto requestDto)
        {
            var loginUserResponse = await userService.RefreshUserAsnc(requestDto);
            return Ok(loginUserResponse);
        }
    }
}