using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
	public partial class buyer
	{
		private static IList<buyer> cache;

		public static IList<buyer> Cache
		{
			get
			{
				if (cache == null || cache.Count <= 0)
				{
					ssmDataContext db = new ssmDataContext();
					cache = db.buyers.ToList();
				}
				return cache;
			}
		}

		public static void Refresh()
		{
			ssmDataContext db = new ssmDataContext();
			cache = db.buyers.ToList();
		}

		/// <summary>
		/// Buyer run into debt 
		/// </summary>
		/// <param name="buyerId">Buyer's ID</param>
		/// <param name="cash">Sum of Sale</param>
		public static void RunIntoDebt(int buyerId, decimal cash)
		{
			var db = new ssmDataContext();
			buyer buyer = db.buyers.First(b => b.id == buyerId);
			if (buyer.debt != null)
			{
				buyer.debt += (double?) cash;
			}
			else
			{
				buyer.debt = (double?) cash;
			}
			db.SubmitChanges();
			Refresh();
		}

		/// <summary>
		/// Buyer refund our money
		/// </summary>
		/// <param name="buyerId">Buyer's ID</param>
		/// <param name="cash">Sum of refund</param>
		public static void Refund(int buyerId, float cash)
		{
			var db = new ssmDataContext();
			buyer buyer = db.buyers.First(b => b.id == buyerId);
			if (buyer.debt != null)
			{
				buyer.debt -= cash;
			}
			else
			{
				buyer.debt = -cash;
			}
			db.SubmitChanges();
			Refresh();
		}
	}
}
