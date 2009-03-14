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
    			return item.GetById(itemId.Value).name;
    		}
    	}

		public int? logBprice
		{
			get
			{
				return item.GetById(itemId.Value).bprice;
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
        }

        public static void GiveBack(int buyerId, int sellerId, int itemId, int itemsCount, float cash, int sid)
        {

            var db = new ssmDataContext();

			logSale sale = db.logSales.FirstOrDefault(
        		b => b.buyerId == buyerId && b.sellerId == sellerId && b.sid == sid && b.isGiveBack == false);
			if (sale!=null)
			{
				sale.cash = sale.cash - cash;
				sale.itemsCount = sale.itemsCount - itemsCount;
			}

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
#warning При подтверждении возврата товара админом в sellerId ставbтся id админа. Колличество вернутого товара добавляется к имеющемуся на складе
#warning  Грида с из logActivity и logSale в админке
			db.logSales.InsertOnSubmit(logSale);
            db.SubmitChanges();
        }

		public static IList<logSale> GetSalesForGiveBackList(string GUID, string buyTime)
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
				list = db.logSales.Where(b => b.isGiveBack == false).ToList();
			}
			return list;
		}

		public static logSale GetLogSalesById(int id)
		{
			ssmDataContext db = new ssmDataContext();
			return db.logSales.FirstOrDefault(b => b.id == id);
		}
		
		private static object GetLogSalesList(IQueryable<logSale> rootQuery, ssmDataContext db)
		{
			var logSalesJoinSeller = rootQuery.Join(db.sellers, d => d.sellerId, c => c.id, (d, c) => new
			{
				sellerName = c.fullName,
				d.buyerId,
				d.cash,
				d.date,
				d.id,
				d.isGiveBack,
				d.itemId,
				d.itemsCount,
				d.sellerId
			});
			var logSalesJoinSellerBuyer = logSalesJoinSeller.Join(db.buyers, d => d.buyerId, c => c.id, (d, c) => new
			{
				buerName = c.name,
				d.buyerId,
				d.cash,
				d.date,
				d.id,
				d.isGiveBack,
				d.itemId,
				d.itemsCount,
				d.sellerId,
				d.sellerName
			});
			var res = logSalesJoinSellerBuyer.Join(db.items, d => d.itemId, c => c.id, (d, c) => new 
			                                                                     	{
																						SellerName = d.sellerName,
																						BuyerName = d.buerName,
																						ItemName = c.name,
			                                                                     		LogCash = d.cash,
																						Logdate = d.date,
																						ItemsCount = d.itemsCount,
																						d.isGiveBack,
																						d.id
																						
			                                                                     	});

			return res.OrderBy(f => f.Logdate).ToList();
		}

		public static object GetSalesList()
		{
			ssmDataContext db = new ssmDataContext();
			return GetLogSalesList(db.logSales.Where(g => g.isGiveBack == false), db);
		}


		public static object GetGiveBackListForApprove()
		{
			if (AppHelper.CurrentUser.isAdmin.Value)
			{
				ssmDataContext db = new ssmDataContext();
				return
					GetLogSalesList(db.logSales.Where(g => g.isGiveBack == true && g.sellerId != AppHelper.CurrentUser.id), db);
			}
			return null;
		}

		public static void ApproveGiveBack(int id)
		{
			ssmDataContext db = new ssmDataContext();
			logSale log = db.logSales.FirstOrDefault(b => b.id == id);
			if (log != null && log.isGiveBack.Value && AppHelper.CurrentUser.isAdmin.Value)
			{
				item itm = db.items.FirstOrDefault(c => c.id == log.itemId);
				if (itm!=null)
				{
					log.sellerId = AppHelper.CurrentUser.id;
					itm.count += log.itemsCount;
				}
			}

		}
    }
}