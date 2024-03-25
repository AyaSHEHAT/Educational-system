using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo1_Day2.Controllers.CustomFilters
{
    public class AuthFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated==false)
            {
                context.Result=new RedirectToActionResult("Login","Account",null);
            }
            base.OnActionExecuting(context);
        }
    }
}
