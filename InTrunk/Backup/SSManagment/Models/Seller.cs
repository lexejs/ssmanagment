using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
	public partial class seller
	{
		private static IList<seller> cache;

		public static IList<seller> Cache
		{
			get
			{
				if (cache == null || cache.Count <= 0)
				{
					ssmDataContext db = new ssmDataContext();
					cache = db.sellers.ToList();
				}
				return cache;
			}
		}

		public static void Refresh()
		{
			ssmDataContext db = new ssmDataContext();
			cache = db.sellers.ToList();
		}
	}
}
