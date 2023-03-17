using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Core.Api.Controllers.V1
{
    public class AuthController : BaseController
    {
        //[HttpPost, Route("login")]
        //[ProducesResponseType(typeof(AuthDTO), 200)]
        ////[PayloadValidator]
        //public async Task<IActionResult> Login([FromBody] LoginPayload payload)
        //{
        //    var dto = await AuthDTOBuilder.Login(payload, _userService);
        //    return Ok(dto);
        //}

        [HttpGet, Route("email-exist")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> EmailExist()
        {
            return Ok();
        }
    }
}
