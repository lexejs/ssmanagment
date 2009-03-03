using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
	public partial class buyer
	{
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



	}
}
