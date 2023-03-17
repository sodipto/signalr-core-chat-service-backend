using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Core.Api.Controllers.V1
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpGet, Route("profile")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetUserProfile()
        {
            return Ok();
        }
    }
}
