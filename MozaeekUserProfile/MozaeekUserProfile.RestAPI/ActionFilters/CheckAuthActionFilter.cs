using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MozaeekUserProfile.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MozaeekUserProfile.RestAPI.ActionFilters
{
    public class CheckAuthActionFilter : IActionFilter
    {
        private readonly CurrentUser currentUser;

        public CheckAuthActionFilter(CurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var mobileNoClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            currentUser.UserName = mobileNoClaim.Value;
        }
    }
}
