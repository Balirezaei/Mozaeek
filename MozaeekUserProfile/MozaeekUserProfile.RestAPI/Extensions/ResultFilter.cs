using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MozaeekUserProfile.Core.Core.ResponseMessages;

namespace MozaeekUserProfile.RestAPI.Extensions
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null && context.Controller.ToString() != "MozaeekUserProfile.RestAPI.Controllers.ErrorController")
            {
                if (context.Result is ObjectResult objectResult)
                {
                    objectResult.Value = new Result { Data = objectResult.Value };
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }

}