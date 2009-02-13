using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
	public partial class item
	{
		public static IList<item> GetAll()
		{
			var db = new ssmDataContext();
			var result = AppHelper.CurrentUser.login.ToLower().Contains("admin") ? Flood() : db.items.ToList();
			return result;
		}

		public static IList<item> GetAllByGroupId(int groupId)
		{
			return GetAll();
		}
    
		private static IList<item> Flood()
		{
			IList<item> result = new List<item>();
			var db = new ssmDataContext();
			var r = new Random();
			IList<int> groups = db.groups.Select(g => g.id).ToArray();

			IList<string> names = db.items.Select(i => i.name).ToList();
			if (names.Count < 10)
			{
				int namesCount = r.Next(300, 400);
				for (int i = 0; i < namesCount; i++)
					names.Add("Товар № " + i);
			}

			for (int i = 0; i < r.Next(30, 100); i++)
			{
				result.Add(
					new item
						{
							count = r.Next(1, 1000),
							adminPrice = r.Next(500, 10000) * 10,
							pct = r.Next(3, 50),
							price = r.Next(500, 10000) * 10,
							canGiveBack = r.Next(10) < 7,
							measure = r.Next(10) < 5 ? "1 шт" : (r.Next(1, 10) * 100) + " шт",
							countToOrder = r.Next(1, 20) * 10,
							order = r.Next(10) < 3,
							name = names[r.Next(names.Count - 1)],
							groupId = groups[r.Next(groups.Count - 1)]
						}
									);
			}
			return result;
		}
	}

}
