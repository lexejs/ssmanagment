using System;
using System.Collections;
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

			IList<buyer> buy = buyer.Cache;
			if (buy.Count > 0)
			{
				Loger.Info("buyer cache successful Loaded");
			}
			else
			{
				Loger.Info("buyer cache not Loaded or buyer.count = 0");
			}
			buy.Clear();

			IList<seller> sel = seller.Cache;
			if (sel.Count > 0)
			{
				Loger.Info("seller cache successful Loaded");
			}
			else
			{
				Loger.Info("seller cache not Loaded or seller.count = 0");
			}
			sel.Clear();
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