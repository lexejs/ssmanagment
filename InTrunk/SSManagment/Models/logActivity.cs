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

        public static void Info(string message, int buyerId)
        {
            var db = new ssmDataContext();
            var logActivity = new logActivity {action = message, buyerId = buyerId, informAdmin = false};
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

        public static void Warning(string message, int buyerId)
        {
            var db = new ssmDataContext();
            var logActivity = new logActivity {action = message, buyerId = buyerId, informAdmin = true};
            db.logActivities.InsertOnSubmit(logActivity);
            db.SubmitChanges();
        }
    }
}