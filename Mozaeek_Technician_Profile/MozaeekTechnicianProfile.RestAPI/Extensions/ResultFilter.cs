using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MozaeekTechnicianProfile.Core.Core.ResponseMessages;

namespace MozaeekTechnicianProfile.RestAPI.Extensions
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null && context.Controller.ToString() != "MozaeekTechnicianProfile.RestAPI.Controllers.ErrorController")
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