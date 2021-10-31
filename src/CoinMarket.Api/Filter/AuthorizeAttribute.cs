using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoinMarket.Api.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var attributes = descriptor.MethodInfo.CustomAttributes;

            if (context.HttpContext.User.Identity.IsAuthenticated
                && attributes.All(a => a.AttributeType != typeof(IgnoreAuthorizationCheckAttribute)))
            {
                var token = context.HttpContext.Request.Headers["TokenParibu"].FirstOrDefault();
                if (token == null)
                {
                    context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
                }
            }
            else if (!context.HttpContext.User.Identity.IsAuthenticated && 
                     attributes.All(a => a.AttributeType != typeof(IgnoreAuthorizationCheckAttribute)))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }
        }
    }
}

public class IgnoreAuthorizationCheckAttribute : ActionFilterAttribute
{

}
