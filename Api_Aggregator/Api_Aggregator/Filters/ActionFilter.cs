using Api_Aggregator.Infrastructure.ResponseMessages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api_Aggregator.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null && context.Controller.ToString() != "Api_Aggregator.Controllers.ErrorController")
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