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
			string sid = string.Format("{0}{1}{2}{3}{4}{5}", DateTime.Now.Year.ToString().Last(), DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second.ToString().Last());
			return Convert.ToInt32(sid);
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

		public static string TreeSelectedNodePathSession
		{
			get
			{
				if (HttpContext.Current.Session["TreeSelectedNode"] == null)
				{
					HttpContext.Current.Session["TreeSelectedNode"] = "";
				}
				return (HttpContext.Current.Session["TreeSelectedNode"].ToString());

			}
			set { HttpContext.Current.Session["TreeSelectedNode"] = value; }
		}

		#endregion

		public static int RoundTo10(double? val)
		{
			int value = 0;

			if (val != null)
			{
				value = (int)Math.Round(val.Value, 0);

				if (value <= 10)
				{
					return 10;
				}

				int last = Convert.ToInt32(value.ToString()[value.ToString().Length - 1].ToString());
				if (last >= 1)
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

		public static void CalcShopingCartSum(Label lblTmp, HtmlGenericControl spanTmp, DropDownList drpTmp, CheckBox chk)
		{
			if (ShopingCartSession != null && ShopingCartSession.Count > 0)
			{
				spanTmp.Visible = true;
				ssmDataContext db = new ssmDataContext();
				buyer br = db.buyers.FirstOrDefault(b => b.isActive.HasValue && b.isActive.Value && b.id == Convert.ToInt32(drpTmp.SelectedValue));
				if (br != null)
				{
					double? sum = ShopingCartSession.Sum(b => b.ResultPrice);
					lblTmp.Text = RoundTo10(sum - ((sum / 100) * br.pct)).ToString("0р.");
					chk.Visible = br.canBuyOnTick.GetValueOrDefault(false);
					return;
				}
			}
			spanTmp.Visible = false;
			lblTmp.Text = "";
		}

		public static void CalcShopingCartSum(Label lblTmp, HtmlGenericControl spanTmp, DropDownList drpTmp)
		{
			if (ShopingCartSession != null && ShopingCartSession.Count > 0)
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
			lblTmp.Text = "";
		}
	}
}
