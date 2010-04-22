using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSManagment.Models;

namespace SSManagment
{
	public partial class RemReports : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void RemoveReports(object sender, EventArgs e)
		{
			var db = new ssmDataContext();
			DateTime fromDate;// = DateTime.Now.AddDays(-1);
			DateTime toDate;// = DateTime.Now;
			try
			{

				if (!string.IsNullOrEmpty(txtDateFrom.Text.Trim()))
				{
					if (DateTime.TryParse(txtDateFrom.Text, out fromDate))
					{

						if (!string.IsNullOrEmpty(txtDateTo.Text.Trim()))
						{
							if (DateTime.TryParse(txtDateTo.Text, out toDate))
							{
								IQueryable<logSale> list = db.logSales.Where(ls => ls.date < toDate && ls.date > fromDate);
								int count = list.Count();

								db.logSales.DeleteAllOnSubmit(list);
								db.SubmitChanges();
								lblMessage.Text = "Выполнено за даты с " + fromDate.ToString("yyyy-MM-dd")
									+ " по " + toDate.ToString("yyyy-MM-dd")
									+ string.Format("\n\r Удалено {0} записей ", count);
								return;
							}

						}
					}
				}
				lblMessage.Text = "Введите корректно даты";
			}
			catch (Exception ex)
			{
				Global.Loger.Error("Ошибка при удалении отчётов", ex);
				lblMessage.Text = "произошла ошибка при удалении отчётов! попробуйте ещё раз. Если ошибка повторится, обратитесь к разработчику.";

			}


		}
	}
}
