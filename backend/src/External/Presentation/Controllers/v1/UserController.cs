using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserRequestDto requestDto)
        {
            logger.LogInformation("CreateUser called with {RequestDto}", JsonSerializer.Serialize(requestDto));
            var userid = await userService.CreateUserAsync(requestDto);
            logger.LogInformation("CreateUser succeeded with UserId: {UserId}", userid);
            return Ok(userid);
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginUserResponse>> LoginUser([FromBody] LoginUserRequestDto requestDto)
        {
            logger.LogInformation("LoginUser called with {RequestDto}", JsonSerializer.Serialize(requestDto));
            var loginUserResponse = await userService.LoginUserAsync(requestDto.Email, requestDto.Password);
            logger.LogInformation("LoginUser succeeded for Email: {Email}", requestDto.Email);
            return Ok(loginUserResponse);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginUserResponse>> RefreshToken([FromBody] RefreshTokenRequestDto requestDto)
        {
            logger.LogInformation("LoginUser called with {token}", JsonSerializer.Serialize(requestDto));
            var loginUserResponse = await userService.RefreshUserAsnc(requestDto);
            logger.LogInformation("LoginUser succeeded for token: {token}", requestDto.Token);
            return Ok(loginUserResponse);
        }
    }
}