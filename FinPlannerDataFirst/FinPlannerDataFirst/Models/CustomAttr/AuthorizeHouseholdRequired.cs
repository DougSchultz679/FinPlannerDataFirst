using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinPlannerDataFirst.Models.CustomAttr
{
    public class AuthorizeHouseholdRequired:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            // fix this - where does this method go?
            return httpContext.User.Identity.IsInHousehold();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            } else
            {
                filterContext.Result = new RedirectToRouteResult(new 
                    RouteValueDictionary(new 
                    {
                    controller = "Home",
                    //This action doesn't exist yet
                    action = "CreateJoinHousehold"
                }));
            }
        }
    }

    public class ValidateHouseholdId
    {
    }
}