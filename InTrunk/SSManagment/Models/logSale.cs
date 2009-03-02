using System;
using System.Collections.Generic;
using System.Linq;

namespace SSManagment.Models
{
    public partial class logSale
    {
    	public string logName
    	{
    		get
    		{
    			return this.item.name;
    		}
    	}

		public int? logBprice
		{
			get
			{
				return this.item.bprice;
			}
		}

        public static void Sale(int buyerId, int sellerId, int itemId, int itemsCount, float cash, int sid)
        {
            var db = new ssmDataContext();
            var logSale = new logSale
                              {
                                  buyerId = buyerId,
                                  sellerId = sellerId,
                                  itemId = itemId,
                                  itemsCount = itemsCount,
                                  date = DateTime.Now,
                                  isGiveBack = false,
                                  cash = cash,
                                  sid = sid
                              };
            db.logSales.InsertOnSubmit(logSale);
            db.items.First(i => i.id == itemId).count -= itemsCount;
            db.SubmitChanges();
#warning Violation of PRIMARY KEY constraint 'PK_logSales'. Cannot insert duplicate key in object 'dbo.logSales'. The statement has been terminated.
        }

        public static void GiveBack(int buyerId, int sellerId, int itemId, int itemsCount, float cash, int sid)
        {
            var db = new ssmDataContext();
            var logSale = new logSale
                              {
                                  buyerId = buyerId,
                                  sellerId = sellerId,
                                  itemId = itemId,
                                  itemsCount = itemsCount,
                                  date = DateTime.Now,
                                  isGiveBack = true,
                                  cash = cash,
                                  sid = sid
                              };
            db.logSales.InsertOnSubmit(logSale);
            db.SubmitChanges();
        }

		public static IList<logSale> GetGiveBackList(string GUID, string buyTime)
		{
			IList<logSale> list = null;
			ssmDataContext db = new ssmDataContext();
			int sid;

			if (!string.IsNullOrEmpty(buyTime))
			{
				DateTime buyDate;
				if (DateTime.TryParse(buyTime, out buyDate))
				{
					if ((!string.IsNullOrEmpty(GUID)) && (int.TryParse(GUID, out sid)))
					{
						list = db.logSales.Where(b => b.isGiveBack == false && b.sid == sid && b.date == buyDate).ToList();
					}
					else
					{
						list = db.logSales.Where(b => b.isGiveBack == false && b.date == buyDate).ToList();
					}
					return list;
				}
			}
			if ((!string.IsNullOrEmpty(GUID)) && (int.TryParse(GUID, out sid)))
			{
				list = db.logSales.Where(b => b.isGiveBack == false && b.sid == sid).ToList();
			}
			else
			{
#warning Specified cast is not valid.
				list = db.logSales.Where(b => b.isGiveBack == false).ToList();
			}
			return list;
		}

		public static logSale GetLogSalesById(int id)
		{
			ssmDataContext db = new ssmDataContext();
			return db.logSales.FirstOrDefault(b => b.id == id);
		}
    }
}