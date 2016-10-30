using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
	public partial class group
	{
		public static int GetItemsCountByGroupId(int id)
		{
			return new ssmDataContext().items.Count(itm => itm.groupId == id);
		}
	}
}
