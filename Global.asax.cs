using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Globalization;


namespace WebApplication73
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

		protected void Application_AcquireRequestState(Object sender, EventArgs e)
		{
			HttpContext context = HttpContext.Current;
			var languageSession = "en";
			if (context != null && context.Session != null)
			{
				languageSession = context.Session["lang"] != null ? context.Session["lang"].ToString() : "en";
			}
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSession);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(languageSession);
		}
	}
}
