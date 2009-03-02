using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SSManagment.Models
{
	public class AppHelper
	{
		public static int GetSID()
		{
			//DateTime.Now
			return 0;
		}

		#region Properties

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

		public static IList<ShopingCart> ShopingCartSession
		{
			get
			{
				if (HttpContext.Current.Session["ShopingCartItems"] == null)
				{
					HttpContext.Current.Session["ShopingCartItems"] = new List<ShopingCart>();
				}
				return ((IList<ShopingCart>)HttpContext.Current.Session["ShopingCartItems"]);

			}
			set { HttpContext.Current.Session["ShopingCartItems"] = value; }
		}

		public static IList<item> ProductsSession
		{
			get
			{
				if (HttpContext.Current.Session["Products"] == null)
				{
					HttpContext.Current.Session["Products"] = new List<item>();
				}
				return ((IList<item>)HttpContext.Current.Session["Products"]);

			}
			set { HttpContext.Current.Session["Products"] = value; }
		}

		#endregion

		public static int RoundTo10(double? val)
		{
			int value = 0;

			if (val != null)
			{
				value = (int)Math.Round(val.Value, 0);
				int last = Convert.ToInt32(value.ToString()[value.ToString().Length-1].ToString());
				if (last >= 5)
				{
					value = value + (10 - last);
				}
				else
				{
					value = value - last;
				}
			}

			return value;
		}


		public static void CalcShopingCartSum(Label lblTmp, HtmlGenericControl spanTmp, DropDownList drpTmp)
		{
			if (ShopingCartSession != null || ShopingCartSession.Count > 0)
			{
				spanTmp.Visible = true;
				ssmDataContext db = new ssmDataContext();
				buyer br = db.buyers.FirstOrDefault(b => b.isActive.HasValue && b.isActive.Value && b.id == Convert.ToInt32(drpTmp.SelectedValue));
				if (br != null)
				{
					double? sum = ShopingCartSession.Sum(b => b.ResultPrice);
					lblTmp.Text = RoundTo10(sum - ((sum / 100) * br.pct)).ToString("0р.");
					return;
				}
			}

			spanTmp.Visible = false;
			lblTmp.Text = "0";

		}
	}
}
