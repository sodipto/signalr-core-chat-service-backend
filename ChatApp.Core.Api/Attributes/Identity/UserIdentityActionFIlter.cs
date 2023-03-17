using ChatApp.Core.Api.Controllers.V1;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;

namespace ChatApp.Core.Api.Attributes
{
    public class UserIdentityActionFIlter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as BaseController;
            var userIDClaim = context.HttpContext?.User?.Claims?.FirstOrDefault(s => s.Type == "UserID")?.Value;
            var userEmailClaim = context.HttpContext?.User?.Claims?.FirstOrDefault(s => s.Type == "CurrentUserEmail")?.Value;

            if (!string.IsNullOrEmpty(userIDClaim))
            {
                var userID = new Guid(userIDClaim);
                if (userID != Guid.Empty)
                    controller!.CurrentUserID = userID;
            }
            if (!string.IsNullOrEmpty(userEmailClaim))
                controller!.CurrentUserEmail = userEmailClaim;

        }
    }
}
