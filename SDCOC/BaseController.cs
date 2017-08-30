using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SDCOC
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["userid"])))
            {
                
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                return;
            }

        }

    }
}
