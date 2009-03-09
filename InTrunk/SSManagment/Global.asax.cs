using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using log4net;
using SSManagment.Models;

namespace SSManagment
{
	public class Global : System.Web.HttpApplication
	{
		public static ILog Loger = LogManager.GetLogger(typeof(Global));

		protected void Application_Start(object sender, EventArgs e)
		{
			log4net.Config.DOMConfigurator.Configure();
			Loger.Info("Application Start");
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Loger.Fatal(((System.Web.HttpApplication)(sender)).Context.AllErrors[0].Message,((System.Web.HttpApplication)(sender)).Context.AllErrors[0]);
		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{
			Loger.Info("Application End");
		}

	}
}