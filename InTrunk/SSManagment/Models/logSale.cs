﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SSManagment.Models
{
	public class logType
	{
		public string ProductName { get; set; }
		public string SellerName { get; set; }
		public string BuyerName { get; set; }
		public double? LogCash { get; set; }
		public System.DateTime? Logdate { get; set; }
		public float? ItemsCount { get; set; }
		public bool? LogIsGiveBack { get; set; }
		public int LogId { get; set; }
	}

	public partial class logSale
	{


		public string logName
		{
			get
			{
				var itm = item.GetById(itemId.Value);
				return itm != null ? itm.name : null;
			}
		}

		public decimal? logBprice
		{
			get
			{
				var itm = item.GetById(itemId.Value);
				return itm != null ? itm.bprice : null;
			}
		}

		public static void Sale(int buyerId, int sellerId, int itemId, float itemsCount, decimal? cash, int sid)
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
									cash = (double?) cash,
									sid = sid
								};
			db.logSales.InsertOnSubmit(logSale);
			db.items.First(i => i.id == itemId).count -= itemsCount;
			db.SubmitChanges();
		}

		public static void GiveBack(int buyerId, int sellerId, int itemId, float itemsCount, float cash, int sid)
		{

			var db = new ssmDataContext();

			logSale sale = db.logSales.FirstOrDefault(
				b => b.buyerId == buyerId && b.sellerId == sellerId && b.sid == sid && b.isGiveBack == false);
			if (sale != null)
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
			db.logSales.InsertOnSubmit(logSale);
			db.SubmitChanges();
		}

		public static object GetSalesForGiveBackList(string GUID, DateTime? buyDate)
		{
			IList<logSale> list = null;
			ssmDataContext db = new ssmDataContext();
			int sid;
			list = (!string.IsNullOrEmpty(GUID)) && (int.TryParse(GUID, out sid)) ?
					(buyDate != null ?
						db.logSales.Where(b => b.isGiveBack == false && b.date.Value.Year == buyDate.Value.Year && b.date.Value.Month == buyDate.Value.Month && b.date.Value.Day == buyDate.Value.Day).ToList()
									: db.logSales.Where(b => b.isGiveBack == false && b.sid == sid).ToList())
							: (buyDate != null ?
						db.logSales.Where(b => b.isGiveBack == false && b.date.Value.Year == buyDate.Value.Year && b.date.Value.Month == buyDate.Value.Month && b.date.Value.Day == buyDate.Value.Day).ToList()
									: db.logSales.Where(b => b.isGiveBack == false).ToList());

			var resJoinBuers1 = list.Join(buyer.Cache, d => d.buyerId, c => c.id, (d, c) => new
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
				d.logName,
				d.logBprice
			});

			return resJoinBuers1.OrderByDescending(ord => ord.date).ToList();
		}

		public static logSale GetLogSalesById(int id)
		{
			ssmDataContext db = new ssmDataContext();
			return db.logSales.FirstOrDefault(b => b.id == id);
		}

		private static object GetLogSalesList(IQueryable<logSale> rootQuery, ssmDataContext db)
		{
			var logSalesJoinSeller = rootQuery
				.ToList()
				.Join(seller.Cache, d => d.sellerId, c => c.id, (d, c) => new
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

			var logSalesJoinSellerBuyer = logSalesJoinSeller
				.Join(buyer.Cache, d => d.buyerId, c => c.id, (d, c) => new
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

			var res = logSalesJoinSellerBuyer
				.Join(db.items, d => d.itemId, c => c.id, (d, c) => new
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


		public static IList<logType> GetGiveBackListForApprove()
		{
			if (AppHelper.CurrentUser.isAdmin.Value)
			{
				ssmDataContext db = new ssmDataContext();

				var logSalesJoinSeller = db.logSales
					.Where(g => g.isGiveBack == true
						/// будем апрувить все возвраты
						/// && g.sellerId != AppHelper.CurrentUser.id
						&& g.itemsCount != 0
						)
					.ToList()
					.Join(seller.Cache, d => d.sellerId, c => c.id, (d, c) => new
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
				var logSalesJoinSellerBuyer = logSalesJoinSeller.Join(buyer.Cache, d => d.buyerId, c => c.id, (d, c) => new
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
				var res = logSalesJoinSellerBuyer.Join(db.items, d => d.itemId, c => c.id, (d, c) => new logType
																						{
																							SellerName = d.sellerName,
																							BuyerName = d.buerName,
																							ProductName = c.name,
																							LogCash = d.cash,
																							Logdate = d.date,
																							ItemsCount = d.itemsCount,
																							LogIsGiveBack = d.isGiveBack,
																							LogId = d.id

																						});

				return res.OrderBy(f => f.Logdate).ToList();
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
				if (itm != null && itm.count != null)
				{
					log.sellerId = AppHelper.CurrentUser.id;
					itm.count += log.itemsCount;
					log.itemsCount = 0;
					db.SubmitChanges();
				}
			}

		}
	}
}