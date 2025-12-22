using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class RelationController(IRelationService relationService) : ControllerBase
    {
        [HttpPost("friend/{id}")]
        public async Task<ActionResult> SendFriend([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            await relationService.SendFriendRequest(userId, id);
            return NoContent();
        }
        [HttpPost("friend/{id}/approve")]
        public async Task<ActionResult> ApproveFriend([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            await relationService.ApproveFriendRequest(userId, id);
            return NoContent();
        }
    }
}