using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
	public class AppHelper
	{
		public static seller CurrentUser
		{
			get
			{
				return HttpContext.Current.Session["CurrentUser"] as seller;
			}
			set
			{
				HttpContext.Current.Session["CurrentUser"] = value;
			}
		}

	}
}
