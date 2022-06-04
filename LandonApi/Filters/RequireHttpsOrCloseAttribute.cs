using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LandonApi.Filters
{
    public class RequireHttpsOrCloseAttribute:RequireHttpsAttribute //inherit require https class atribute
    {
        //overiding HandelNonHttpsRequest Method
        protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new StatusCodeResult(400);
        }
    }
}
