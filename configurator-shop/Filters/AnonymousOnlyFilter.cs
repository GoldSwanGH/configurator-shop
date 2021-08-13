﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace configurator_shop.Filters
{
    public class AnonymousOnlyFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity is {IsAuthenticated: true})
            {
                context.Result = new RedirectToActionResult("Index", "Home","");
            }
        }
    }
}