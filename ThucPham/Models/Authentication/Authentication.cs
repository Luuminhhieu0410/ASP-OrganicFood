using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ThucPham.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public Authentication() { }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetString("Email") == null)
            {
                context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                       {"Controller","Access"},
                       {"Action","Login"}
                   }
                   );
            }
        }
    }
}
