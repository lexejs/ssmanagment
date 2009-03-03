using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
	public partial class buyer
	{
		/// <summary>
		/// Buyer run into debt 
		/// </summary>
		/// <param name="buyerId">Buyer's ID</param>
		/// <param name="cash">Sum of Sale</param>
		public void RunIntoDebt(int buyerId, float cash)
		{
			var db = new ssmDataContext();
			buyer buyer = db.buyers.First(b => b.id == buyerId);
			if (buyer.debt != null)
			{
				buyer.debt += cash;
			}
			else
			{
				buyer.debt = cash;
			}
			db.SubmitChanges();
		}

		/// <summary>
		/// Buyer refund our money
		/// </summary>
		/// <param name="buyerId">Buyer's ID</param>
		/// <param name="cash">Sum of refund</param>
		public void Refund(int buyerId, float cash)
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
		}
	}
}
