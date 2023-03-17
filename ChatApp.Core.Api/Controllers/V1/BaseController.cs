using System;
using ChatApp.Core.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Core.Api.Controllers.V1
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [UserIdentityActionFIlter]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController() { }
        public Guid CurrentUserID { get; set; } = Guid.Empty;
        public string CurrentUserEmail { get; set; } = string.Empty;
    }
}




