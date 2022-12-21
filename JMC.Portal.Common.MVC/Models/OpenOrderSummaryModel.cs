using JMC.Portal.Business;
using JMC.Portal.Business.PortalModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JMC.Portal.Business.PortalModels.PortalContext;

namespace JMC.Portal.Common.MVC.Models
{
	public class OpenOrderSummaryModel
	{
		public List<OpenOrderItem> BackLogItems;
		IQueryable<SapsoldTo> sapSoldTos;
		//IQueryable<SAPShipTo> sapShipTos;
		//IQueryable<Plant> plants;

		//public bool updateneeded = false;
		public DateTime? BackLogAge = null;

		public long? sapSoldToId;
		public long? sapShipToId;
		//public long? sapSalesOrgId { get; set; }
		//public string sapSalesOfficeId { get; set; }
		//public long? sapCustomerGroupId { get; set; }
		public long? sapSalesGroupId { get; set; }
		//public long? sapCustomerGroupId { get; set; }

		//public SAPSoldTo sapSoldTo { get; set; }
		public SapsalesOrganization sapSalesOrg { get; set; }
		//public string sapSalesOffice { get; set; }
		public SapcustomerGroup sapCustomerGroup { get; set; }
		public SapsalesGroup sapSalesGroup { get; set; }

		HttpContext _httpcontext;

		public bool wasquick = false;
		public int soldToCount = 0;
		//private DBCache dbcache;
		private PortalContext db;
		public User user= new User();
		public User ImpersonateUser;

		public OpenOrderSummaryModel(HttpContext httpcontext)
		{
			_httpcontext = httpcontext;
			this.PreLoad();


            //if (user != null && !user.SapsoldToes.Any(x => !x.LastBacklogRefresh.HasValue) && user.UserProfiles.SAPSoldToes.Any())
            //{
            //    this.BackLogAge = user.SAPSoldToes.Min(x => x.LastBacklogRefresh.Value);
            //}
        }

		public OpenOrderSummaryModel(HttpContext httpcontext, string _userName)
		{
			_httpcontext = httpcontext;
			//Session = session;

			this.PreLoad();

			//if (user != null && !user.SAPSoldToes.Any(x => !x.LastBacklogRefresh.HasValue) && user.SAPSoldToes.Any())
			//{
			//	this.BackLogAge = user.SAPSoldToes.Min(x => x.LastBacklogRefresh.Value);
			//}
		}

		private void PreLoad()
		{
			
			if (db==null) db = new PortalContext();

			if (this.user == null)
			{
				user = db.Users.FirstOrDefault(x => x.UserName == _httpcontext.User.Identity.Name);
			}

			if (this.user == null) user.Employee = Employee.FindByDomainAndSAMAccountName(ref db, _httpcontext.User.Identity.Name.ToString());
			//this.user = new User();

			if (user is Employee)
			{
				
				long? ImpId = Convert.ToInt64(_httpcontext.Session.GetString("UserID"));
				if (ImpId.HasValue)
				{
					ImpersonateUser = db.Users.FirstOrDefault(x => x.UserId == ImpId.Value);
					if (ImpersonateUser != null)
					{
						user = ImpersonateUser;
					}
				}
			}

			if (user is Employee)
			{
				//sapSoldTos = (from s in db.SapshipTos.OfType<JMC.Portal.Business.PortalModels.SapsoldTo>() where s.DivisionId == (long)Enums.Divisions.Atlas select s).OrderBy(x => x.Number);

				if (user.IsIsr())
				{
					this.sapSalesGroup = (from sg in db.SapsalesGroups where sg.UserId == user.UserId select sg).FirstOrDefault();
					this.sapSalesGroupId = this.sapSalesGroup == null ? 0 : this.sapSalesGroup.SapsalesGroupId;
				}
				//if (user.IsOsr()) {
				//  this.sapCustomerGroup = (from sg in db.SAPCustomerGroups where sg.UserID == user.UserID select sg).FirstOrDefault();
				//  this.sapCustomerGroupId = this.sapCustomerGroup.IsNull() ? 0 : this.sapCustomerGroup.SAPCustomerGroupID;
				//}
			}
			else
			{
				var listsapSoldTo = user.GetSapSoldTos();
				sapSoldTos = listsapSoldTo.AsQueryable();
			}
			
			long soldToID = (_httpcontext.Session == null || _httpcontext.Session.GetString("SoldToID") == null ? 0 : Convert.ToInt64(_httpcontext.Session.GetString("SoldToID")));
			SapsoldTo sapSoldTo = null;

			if (soldToID > 0)
			{
				sapSoldTo = sapSoldTos.FirstOrDefault(x => x.SapshipToId == soldToID);
			}
			else
			{
				//sapSoldTo = user.SAPSoldTo;
				//if (sapSoldTo == null)
				//{
				//	sapSoldTo = user.SAPSoldToes.FirstOrDefault();
				//}
				//if (sapSoldTo != null && _httpcontext.Session != null)
				//{
				//	_httpcontext.Session.SetString("SoldToID", sapSoldTo.SapshipToId.ToString());
				//	_httpcontext.Session.SetString("SoldTo", sapSoldTo.ToString() + " " + sapSoldTo.Name + " (" + sapSoldTo.IncoTerms2 + ")");
				//}
			}

			sapSoldToId = 0;
			if (sapSoldTo!= null)
			{
				sapSoldToId = sapSoldTo.SapshipToId;
				sapShipToId = Convert.ToInt64((_httpcontext.Session == null || _httpcontext.Session.GetString("ShipToID") == null ? 0 : _httpcontext.Session.GetString("ShipToID")));
			}

			//if (dbcache.IsNull()) dbcache = new DBCache(ref db);
			if (BackLogItems == null) BackLogItems = new List<OpenOrderItem>();
		}

		public string Status(long soldToId, int refresh)
		{
			this.PreLoad();
			bool inqueue = false;
			DateTime? refreshDate = null;
			SapsoldTo sapSoldTo = this.sapSoldTos.FirstOrDefault(x => x.SapshipToId == soldToId);
			if (sapSoldTo == null)
			{
				return @"{""inqueue"":" + (inqueue ? "true" : "false") + @",""date"":""" + (refreshDate.HasValue ? refreshDate.Value.ToShortDateString() : "") + "\"," + @"""datetime"":""" + (refreshDate.HasValue ? refreshDate.Value.ToString("g") : "") + "\"}";
			}
			if (refresh == 1)
			{
				BackgroundProcessor.Push(new BackgroundProcessor.ProcessingTask(500, "BL", sapSoldTo.PricingNotes, true));
			}
			inqueue = BackgroundProcessor.IsInqueue("BL", sapSoldTo.PricingNotes);

			if (inqueue == false)
			{
				refreshDate = sapSoldTo.LastBacklogRefresh;
			}

			return @"{""inqueue"":" + (inqueue ? "true" : "false") + @",""date"":""" + (refreshDate.HasValue ? refreshDate.Value.ToShortDateString() : "") + "\"," + @"""datetime"":""" + (refreshDate.HasValue ? refreshDate.Value.ToString("g") : "") + "\"}";
		}

		public string GetLastBacklogRefresh(long soldToId)
		{
			DateTime? refreshDate = null;

			SapsoldTo sapSoldTo = this.sapSoldTos.FirstOrDefault(x => x.SapshipToId == soldToId);

			if (sapSoldTo != null)
			{
				refreshDate = sapSoldTo.LastBacklogRefresh;
			}

			return @"{""date"":""" + (refreshDate.HasValue ? refreshDate.Value.ToShortDateString() : "") + "\"," + @"""datetime"":""" + (refreshDate.HasValue ? refreshDate.Value.ToString("g") : "") + "\"}";
		}

		public void ForceUpdateSoldTo(List<long> SoldToIDs)
		{
			this.PreLoad();
			if (SoldToIDs == null) SoldToIDs = new List<long>();
			this.wasquick = false;

			IQueryable<SapsoldTo> currentsapSoldTos = null;

			if (SoldToIDs.Count == 0)
			{
				if (this.user is Employee)
				{
					//var listsapSoldTo = user.SAPSoldToes.ToList();
					//SapsoldTo primarySAPSoldTo = null;

					//if (user.PrimarySapsoldToId.HasValue) primarySAPSoldTo = db.SapshipTos.OfType<SapsoldTo>().FirstOrDefault(x => x.SapshipToId == user.PrimarySapsoldToId.Value);
					//if (primarySAPSoldTo != null && !listsapSoldTo.Contains(primarySAPSoldTo))
					//{
					//	listsapSoldTo.Add(primarySAPSoldTo);
					//}

					//sapSoldTos = listsapSoldTo.AsQueryable();
					//currentsapSoldTos = sapSoldTos;
					//var listshipto = new List<SapshipTo>();

					//foreach (var soldto in sapSoldTos)
					//{
					//	//foreach (var shipto in soldto.SAPShipToes)
					//	//{
					//	//	listshipto.Add(shipto);
					//	//}
					//	//if (!listshipto.Contains(soldto.DefaultSapshipTo))
					//	//{
					//	//	listshipto.Add(soldto.DefaultSapshipTo);
					//	//}
					//	//if (!listshipto.Contains(soldto))
					//	//{
					//	//	listshipto.Add(soldto);
					//	//}
					//}
				}
				else
				{
					currentsapSoldTos = sapSoldTos.Take(10);
				}
			}
			else
			{
				currentsapSoldTos = (from x in sapSoldTos where SoldToIDs.Contains(x.SapshipToId) select x);
			}

			DateTime anHourAgo = DateTime.Now.AddHours(-1);
			List<SapsoldTo> expiredSAPSoldTos = (from x in currentsapSoldTos where ((x.RefreshingBacklog ?? false) == false) select x).ToList();
			List<SapsoldTo> uptoDateSAPSoldTos = (from x in currentsapSoldTos where (x.RefreshingBacklog ?? false) == true select x).ToList();

			if (expiredSAPSoldTos.Any())
			{
				this.BackLogAge = null;
			}
			else
			{
				this.BackLogAge = (from x in uptoDateSAPSoldTos select x.LastBacklogRefresh).Min();
			}

			db.SaveChanges();

			if (expiredSAPSoldTos.Count > 0)
			{
				//SapsoldTo.RefreshBacklog(ref dbcache, (from x in expiredSAPSoldTos select x.Number).Distinct().ToList());
			}

			//dbcache.db.SaveChanges();
			//dbcache.CleanUp();

			foreach (SapsoldTo soldto in expiredSAPSoldTos)
			{
				soldto.LastBacklogRefresh = DateTime.Now;
				uptoDateSAPSoldTos.Add(soldto);
			}

			this.BackLogAge = (from x in uptoDateSAPSoldTos where x.LastBacklogRefresh.HasValue select x.LastBacklogRefresh).Min();
			expiredSAPSoldTos.Clear();

			this.soldToCount += uptoDateSAPSoldTos.Count;

			foreach (SapsoldTo sapSoldTo in uptoDateSAPSoldTos)
			{
				//var backlogItems = db.GetBacklog(sapSoldTo.SapshipToId, null, null, null, null, null, (!(this.user is Employee) ? this.user.UserId : (long?)null)).ToList();
				//foreach (GetBacklog_Result backlogItem in backlogItems)
				//{
				//	OpenOrderItem item = new OpenOrderItem();

				//	item.SAPSoldToID = backlogItem.SAPSoldToID ?? 0;
				//	item.SAPShipToID = backlogItem.SAPShipToID ?? 0;
				//	item.PlantID = backlogItem.PlantID ?? 0;

				//	item.SoldTo = backlogItem.SAPSoldToNumber.TrimNull().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPSoldToName.TrimNull();
				//	item.ShipTo = backlogItem.SAPShipToNumber.TrimNull().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPShipToName.TrimNull();
				//	item.City = backlogItem.SAPShipToIncoTerms2.TrimNull();

				//	item.Plant = backlogItem.PlantName;
				//	item.NotReady = backlogItem.DisplayNotReadyWeight ?? 0;
				//	item.Ready = backlogItem.DisplayReadyWeight ?? 0;
				//	item.Released = backlogItem.DisplayReleasedWeight ?? 0;
				//	item.BOL = backlogItem.DeliveryWeight ?? 0;
				//	item.Open = Convert.ToDecimal(item.BOL) + Convert.ToDecimal(item.Ready) + Convert.ToDecimal(item.NotReady)+ item.Released;

				//	BackLogItems.Add(item);
				//}
			}
		}

		public void AddCurrentUserSapSoldTos()
		{
			if (this.user != null)
			{
				List<SapsoldTo> sapSoldTos = this.user.GetSapSoldTos();

				this.AddSoldTo((from x in sapSoldTos select x.SapshipToId).ToList());
			}
		}

		public void AddSoldTo(List<long> SoldToIDs, bool quick = false)
		{
			this.PreLoad();
			if (SoldToIDs == null) SoldToIDs = new List<long>();
			this.wasquick = quick;
			IQueryable<SapsoldTo> currentsapSoldTos = null;
			if (SoldToIDs.Count == 0)
			{
				//if (this.user is Employee)
				//{
				//	var listsapSoldTo = user.SAPSoldToes.ToList();
				//	SapsoldTo primarySAPSoldTo = null;
				//	if (user.PrimarySapsoldToId.HasValue) primarySAPSoldTo = db.SapshipTos.OfType<SapsoldTo>().FirstOrDefault(x => x.SapshipToId == user.PrimarySapsoldToId.Value);
				//	if (primarySAPSoldTo != null && !listsapSoldTo.Contains(primarySAPSoldTo))
				//	{
				//		listsapSoldTo.Add(primarySAPSoldTo);
				//	}
				//	sapSoldTos = listsapSoldTo.AsQueryable();
				//	currentsapSoldTos = sapSoldTos;
				//	var listshipto = new List<SapshipTo>();
				//	foreach (var soldto in sapSoldTos)
				//	{
				//		foreach (var shipto in soldto.SAPShipToes)
				//		{
				//			listshipto.Add(shipto);
				//		}
				//		if (!listshipto.Contains(soldto.DefaultSapshipTo))
				//		{
				//			listshipto.Add(soldto.DefaultSapshipTo);
				//		}
				//		if (!listshipto.Contains(soldto))
				//		{
				//			listshipto.Add(soldto);
				//		}
				//	}
				//}
				//else
				//{
				//	currentsapSoldTos = new List<SapsoldTo>().AsQueryable();
				//}
			}
			else
			{
				currentsapSoldTos = (from x in sapSoldTos where SoldToIDs.Contains(x.SapshipToId) select x);
			}
			DateTime anHourAgo = DateTime.Now.AddHours(-1);

			List<SapsoldTo> uptoDateSAPSoldTos = (from x in currentsapSoldTos select x).ToList();

			this.BackLogAge = (from x in uptoDateSAPSoldTos select x.LastBacklogRefresh).Min();

			//dbcache.db.SaveChanges();

			this.soldToCount += uptoDateSAPSoldTos.Count;

			foreach (SapsoldTo sapSoldTo in uptoDateSAPSoldTos)
			{
			//	List<GetBacklog_Result> backlogItems = db.GetBacklog(sapSoldTo.SapshipToId, null, null, null, null, null, (!(this.user is Employee) ? this.user.UserId : (long?)null)).ToList();

			//	foreach (GetBacklog_Result backlogItem in backlogItems)
			//	{
			//		OpenOrderItem item = new OpenOrderItem();

			//		item.SAPSoldToID = backlogItem.SAPSoldToID ?? 0;
			//		item.SAPShipToID = backlogItem.SAPShipToID ?? 0;
			//		item.PlantID = backlogItem.PlantID ?? 0;

			//		item.SoldTo = backlogItem.SAPSoldToNumber.Trim().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPSoldToName.Trim();
			//		item.ShipTo = backlogItem.SAPShipToNumber.Trim().TrimStart('0').PadLeft(5, '0') + " " + backlogItem.SAPShipToName.Trim();
			//		item.City = backlogItem.SAPShipToIncoTerms2.Trim();

			//		item.Plant = backlogItem.PlantName;
			//		item.NotReady = backlogItem.DisplayNotReadyWeight ?? 0;
			//		item.Ready = backlogItem.DisplayReadyWeight ?? 0;
			//		item.Released = backlogItem.DisplayReleasedWeight ?? 0;
			//		item.BOL = backlogItem.DeliveryWeight ?? 0;
			//		item.Open = Convert.ToDecimal(item.BOL) + Convert.ToDecimal(item.Ready) + Convert.ToDecimal(item.NotReady) + item.Released;
			//		BackLogItems.Add(item);
			//	}
			}
		}
	}

	public class OpenOrderItem
	{

		public long SAPSoldToID;
		public long SAPShipToID;
		public long PlantID;

		public string SoldTo { get; set; } = "";
		public string ShipTo { get; set; } = "";
		public string City { get; set; } = "";
		public string Plant { get; set; } = "";

		public decimal NotReady { get; set; } = 0;
		//public string NotReadyString
		//{
		//	get
		//	{
		//		return this.NotReady.ToString("#,##0.###");
		//	}
		//}
		public decimal Ready { get; set; } = 0;
		//public string ReadyString
		//{
		//	get
		//	{
		//		return this.Ready.ToString("#,##0.###");
		//	}
		//}
		public decimal BOL { get; set; } = 0;
		//public string BOLString
		//{
		//	get
		//	{
		//		return this.BOL.ToString("#,##0.###");
		//	}
		//}
		public decimal Released { get; set; } = 0;
		//public string ReleasedString
		//{
		//	get
		//	{
		//		return this.Released.ToString("#,##0.###");
		//	}
		//}
		public decimal Open { get; set; } = 0;
		//public string OpenString
		//{
		//	get
		//	{
		//		return this.Open.ToString("#,##0.###");
		//	}
		//}
		public string Blank { get; set; } = "";

	//	public static IQueryable<OpenOrderItem> GetOpenOrderItems(IQueryable<GetBacklog_Result> query)
	//	{
	//		return (from x in query
	//				select new OpenOrderItem()
	//				{
	//					SAPSoldToID = x.SAPSoldToID ?? 0,
	//					SoldTo = x.SAPSoldToNumber.Trim().TrimStart('0') + " " + x.SAPSoldToName.Trim(),
	//					SAPShipToID = x.SAPShipToID ?? 0,
	//					ShipTo = x.SAPShipToNumber.Trim().TrimStart('0') + " " + x.SAPShipToName.Trim(),
	//					City = x.SAPShipToIncoTerms2.Trim(),
	//					PlantID = x.PlantID ?? 0,
	//					Plant = x.PlantName,
	//					NotReady = x.DisplayNotReadyWeight ?? 0,
	//					Ready = x.DisplayReadyWeight ?? 0,
	//					Released = x.DisplayReleasedWeight ?? 0,
	//					BOL = x.DeliveryWeight ?? 0,
	//					Open = (x.DeliveryWeight ?? 0) + (x.DisplayReadyWeight ?? 0) + (x.DisplayNotReadyWeight ?? 0) + (x.DisplayReleasedWeight ?? 0)
	//				}).AsQueryable();
	//	}
	}
}
