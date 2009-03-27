using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment.Models
{
    public partial class item
    {
		public int? bprice
        {
            get
            {
				if (adminPrice != null && pct != null && isActive.HasValue && isActive.Value)
					return AppHelper.RoundTo10(adminPrice + adminPrice*(pct/100));
            	return null;
            }
            set { }
        }

        public static IList<item> GetAll()
        {
            var db = new ssmDataContext();
            var result = AppHelper.CurrentUser.login.ToLower().Contains("admin") ? Flood() : db.items.ToList();
            return result;
        }

        public static IList<item> GetAllByGroupId(int groupId)
        {
            var db = new ssmDataContext();
            var groupIDs = db.groups.Where(g => g.id == groupId || g.parent == groupId).Select(g => g.id).ToList();
            var result = db.items.Where(itm => groupIDs.Contains(itm.groupId.Value) && (itm.isActive.HasValue && itm.isActive.Value || (AppHelper.CurrentUser.isAdmin.HasValue && AppHelper.CurrentUser.isAdmin.Value))).ToList();
			return result;
        }

        public static item GetById(int id)
        {
            return new ssmDataContext().items.FirstOrDefault(itm => itm.id == id);
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

        public static IList<item> FindByName(string name)
        {
            return new ssmDataContext().items.Where(i => i.name.Contains(name) && (i.isActive.HasValue && i.isActive.Value || (AppHelper.CurrentUser.isAdmin.HasValue && AppHelper.CurrentUser.isAdmin.Value))).ToList();
        }


        public static bool ReservItem(int id, int count, DateTime endDate)
        {
            ssmDataContext cont = new ssmDataContext();
            item resvItem = cont.items.FirstOrDefault(b => b.id == id);
            if (resvItem != null)
            {
                if (resvItem.reserveCount != null && resvItem.reserveCount > 0)
                {
                    if ((resvItem.reserveCount + count) <= resvItem.count)
                    {
                        resvItem.reserveCount = count + resvItem.reserveCount;
                        if (resvItem.reserveEndDate < endDate)
                        {
                            resvItem.reserveEndDate = endDate;
                        }
                    }
                }
                else
                {
                    resvItem.reserveCount = count;
                    resvItem.reserveEndDate = endDate;
                }

                cont.SubmitChanges();
                return true;
            }
            return false;
        }

        public static void UnReservForItemId(int id)
        {
            ssmDataContext cont = new ssmDataContext();
            item resvItem = cont.items.FirstOrDefault(b => b.id == id);
            if (resvItem != null)
            {
                resvItem.reserveCount = null;
                resvItem.reserveEndDate = null;
                cont.SubmitChanges();
            }

        }

        public static void CheckForExpiredItems()
        {
            var db = new ssmDataContext();
            var result = db.items.Where(i => i.reserveEndDate<=DateTime.Now).ToList();
            foreach (var item in result)
            {
                item.reserveCount = null;
                item.reserveEndDate = null;
            }
            db.SubmitChanges();
        }

        public static void Order(int id)
        {
            ssmDataContext db = new ssmDataContext();
            item orderItem = db.items.FirstOrDefault(b => b.id == id);
            orderItem.order = true;
            db.SubmitChanges();
        }

        public static void CheckForOrder(int id)
        {
            ssmDataContext db = new ssmDataContext();
            item orderItem = db.items.FirstOrDefault(b => b.id == id);
            if (orderItem.count.HasValue && orderItem.countToOrder.HasValue)
                orderItem.order = orderItem.count <= orderItem.countToOrder;
            db.SubmitChanges();
        }

        public static void BuyShopingCart(IList<ShopingCart> shop, IList<item> products, int sellerId, int buyerId, bool isDebt)
        {
        	int sid = AppHelper.GetSID();
			buyer br = buyer.Cache.FirstOrDefault(b => b.isActive.HasValue && b.isActive.Value && b.id == buyerId);

            foreach (ShopingCart shpProduct in shop)
			{
				int sum = AppHelper.RoundTo10(shpProduct.ResultPrice.Value - ((shpProduct.ResultPrice.Value/100)*br.pct));

				if (isDebt)
				{
					buyer.RunIntoDebt(buyerId, sum);
					logSale.Sale(
						buyerId,
						sellerId,
						shpProduct.id,
						shpProduct.BuyCount,
						0,
						sid);
				}
				else
				{
					logSale.Sale(
						buyerId,
						sellerId,
						shpProduct.id,
						shpProduct.BuyCount,
						sum,
						sid);
				}
				item itm = products.FirstOrDefault(b => b.id == shpProduct.id);
				if (itm != null)
				{
					if (itm.count.Value - shpProduct.BuyCount <= 0)
					{
						products.Remove(itm);
					}
					else
					{
						itm.count = itm.count.Value - shpProduct.BuyCount;
					}
				}
            	CheckForOrder(shpProduct.id);
            	
            }

        }

		public static IList<item> GetOrderList()
		{
			ssmDataContext db = new ssmDataContext();
			return db.items.Where(b => b.order.Value && b.count > b.countToOrder).ToList();
		}

		public static IList<item> GetEndItemList()
		{
			ssmDataContext db = new ssmDataContext();
			return db.items.Where(b => b.order.Value && b.count <= b.countToOrder).ToList();
		}


    }

}
