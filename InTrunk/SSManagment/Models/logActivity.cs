using System;
using System.Collections.Generic;
using System.Linq;

namespace SSManagment.Models
{
    public partial class logActivity
    {
        public static void Info(string message)
        {
            var db = new ssmDataContext();
            var logActivity = new logActivity {action = message, informAdmin = false};
            db.logActivities.InsertOnSubmit(logActivity);
            db.SubmitChanges();
        }

		public static void Info(string message, int sellerId)
        {
            var db = new ssmDataContext();
			var logActivity = new logActivity { action = message, SellerId = sellerId, informAdmin = false };
            db.logActivities.InsertOnSubmit(logActivity);
            db.SubmitChanges();
        }

        public static void Warning(string message)
        {
            var db = new ssmDataContext();
            var logActivity = new logActivity {action = message, informAdmin = true};
            db.logActivities.InsertOnSubmit(logActivity);
            db.SubmitChanges();
        }

		public static void Warning(string message, int sellerId)
        {
            var db = new ssmDataContext();
			var logActivity = new logActivity { action = message, SellerId = sellerId, informAdmin = true, date = DateTime.Now };
            db.logActivities.InsertOnSubmit(logActivity);
            db.SubmitChanges();
        }

		private static object GetLogActivityList(IQueryable<logActivity> rootQuery, ssmDataContext db)
		{
			var logActivityJoinBuyer = rootQuery.Join(seller.Cache, d => d.SellerId, c => c.id, (d, c) => new
			{
				sellerName = c.fullName,
				d.SellerId,
				d.action,
				d.date,
				d.id,
				d.informAdmin
				
			});

			return logActivityJoinBuyer.OrderBy(g => g.date).ToList();
		}

		public static object GetHotLogActivityList()
		{
			ssmDataContext db = new ssmDataContext();
			return GetLogActivityList(db.logActivities.Where(gdf => gdf.informAdmin == true), db);
		}


		public static object GetMsgLogActivityList()
		{
			ssmDataContext db = new ssmDataContext();
			return GetLogActivityList(db.logActivities.Where(gdf => gdf.informAdmin == false), db);
		}
    }
}