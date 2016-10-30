using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSManagment
{
    public class SalesDataSource
    {
        public string Seller { get; set; }
        public double Cash { get; set; }
        public DateTime Date { get; set; }
        public int SID { get; set; }
        public string Buyer { get; set; }
        public string Item { get; set; }
        public float ItemsCount { get; set; }
        public bool IsGiveBack { get; set; }
    }
}
