using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Core.Api.Controllers.V1
{
    [Authorize]
    public class ChatController : BaseController
    {
        [HttpGet, Route("inboxes")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetInboxes()
        {
            return Ok();
        }
    }
}
