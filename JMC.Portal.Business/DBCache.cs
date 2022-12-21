using JMC.Portal.Business.PortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMC.Portal.Business
{
	public class DBCache
	{
		public PortalContext db;

		public IQueryable<SapdeliveryType> deliveryTypes;// = (from dt in db.SAPDeliveryTypes select dt);
		public IQueryable<Sapshipment> shipments;// = (from s in db.Sapshipments where s.DivisionId == (long)Enums.Divisions.Atlas select s);
		public IQueryable<Sapdelivery> deliveries;// = (from d in db.SAPDeliveries where d.DivisionId == (long)Enums.Divisions.Atlas select d);
		public IQueryable<SapshipTo> shipTos;// = (from s in db.SapshipTos where s.DivisionId == (long)Enums.Divisions.Atlas select s);
		public IQueryable<SapshipTo> shipTosWTC;
		public IQueryable<SapsoldTo> soldTosWTC;
		public IQueryable<SapsoldTo> soldTos;// = (from s in db.SapshipTos.OfType<SapsoldTo>() where s.DivisionId == (long)Enums.Divisions.Atlas select s);
		public IQueryable<Sapvendor> vendors;// = (from v in db.SAPVendors where v.DivisionId == (long)Enums.Divisions.Atlas select v);
		public IQueryable<Plant> plants;
		public IQueryable<Plant> plantsWTC;
		public IQueryable<SapstorageLocation> storageLocations;// = (from sl in db.SAPStorageLocations where sl.Plant.DivisionId == (long)Enums.Divisions.Atlas select sl);
		public IQueryable<ScrapSapsoldTo> scrapSapsoldTos;// = (from sst in db.ScrapSapsoldToes where sst.SapsoldTo.DivisionId == (long)Enums.Divisions.Atlas select sst);
		public IQueryable<RandomLengthSapsoldTo> randomLengthSapsoldTos;// = (from rlst in db.RandomLengthSapsoldToes where rlst.SapsoldTo.DivisionId == (long)Enums.Divisions.Atlas select rlst);
		public IQueryable<SapsalesOrder> SapSalesOrders;
		public IQueryable<Sapstock> sapStocks;
		public IQueryable<Sapmaterial> SAPMaterials;
		public IQueryable<Sapmaterial> SAPMaterialsWTC;
		public IQueryable<SapcharacteristicOption>SapcharacteristicOptions;
		private List<SapstorageLocation> cacheSAPStorageLocations;
		public List<Sapdelivery> cacheSAPDeliveries;
		public List<SapdeliveryItem> cacheSAPDeliveryItems;
		public List<Sapshipment> cacheSapshipments;
		public List<SapsalesOrder> cacheSAPSalesOrders;
		private List<Sapstock> cacheSAPStocks;
		private List<SapcharacteristicOption> cacheSAPCharacteristicOptions;
		public List<SapsalesOrderItem> cacheSAPSalesOrderItems;
		private List<SapsoldTo> cacheSapsoldTos;
		private List<SapsoldTo> cacheSapsoldTosWTC;
		public List<Sapmaterial> cacheSAPMaterials;
		public List<Sapmaterial> cacheSAPMaterialsWTC;
		private List<Plant> cachePlants;
		private List<Plant> cachePlantsWTC;
		private List<SapshipTo> cacheSapshipTos;
		private List<SapshipTo> cacheSapshipTosWTC;
		private List<SapdeliveryType> cacheSAPDeliveryTypes;
		private List<Sapvendor> cacheSAPVendors;
		public List<SapsoldTo> PendingBacklogRefresh;
		public List<string> Metrics;
		public List<string> MissingSalesOrderNumbers;

		public void AddMetrics(string s)
		{
			if (Metrics == null) Metrics = new List<string>();
			Metrics.Add(s);
		}

		//public void PrescanAndCache(ZstHssDelivery[] zstHssDeliveries)
		//{
		//	List<string> oldSAPDeliveryNumbers = (from x in this.cacheSAPDeliveries select x.Number.Trim()).Distinct().ToList();
		//	List<string> newSAPDeliveryNumbers = (from x in zstHssDeliveries where !x.DeliveryNumber.Trim().jIsEmpty() && !oldSAPDeliveryNumbers.Contains(x.DeliveryNumber.Trim()) select x.DeliveryNumber.Trim()).Distinct().ToList();
		//	List<Sapdelivery> deliveries = (from x in this.db.Sapdeliveries where newSAPDeliveryNumbers.Contains(x.Number) select x).ToList();
		//	this.AddToCache(deliveries.ToList());
		//	oldSAPDeliveryNumbers = (from x in this.cacheSapshipments where !string.IsNullOrEmpty(x.Number.Trim()) select x.Number.Trim()).Distinct().ToList();
		//	newSAPDeliveryNumbers = (from x in zstHssDeliveries where !x.DeliveryNumber.Trim().jIsEmpty() && !oldSAPDeliveryNumbers.Contains(x.DeliveryNumber.Trim()) select x.DeliveryNumber.Trim()).Distinct().ToList();
		//	List<Sapshipment> Sapshipments = (from x in this.db.Sapshipments where newSAPDeliveryNumbers.Contains(x.DeliveryNumber) select x).ToList();
		//	this.AddToCache(Sapshipments);

		//	List<string> soldToNumbers = (from x in zstHssDeliveries select x.SoldToNumber.Trim()).Distinct().ToList();
		//	List<SapsoldTo> SapsoldTos = (from x in this.db.SapshipTos.OfType<SapsoldTo>().Where(x => x.DivisionId == (long)Enums.Divisions.Atlas) where soldToNumbers.Contains(x.Number) select x).ToList();
		//	this.AddToCache(SapsoldTos);

		//	List<string> shipToNumbers = (from x in zstHssDeliveries select x.ShipToNumber.Trim()).Distinct().ToList();
		//	List<SapshipTo> SapshipTos = (from x in this.db.SapshipTos.Where(x => x.DivisionId == (long)Enums.Divisions.Atlas) where shipToNumbers.Contains(x.Number) select x).ToList();
		//	this.AddToCache(SapshipTos);
		//}
		//public void PrescanAndCache(ZstHssDeliveryItem[] zstHssDeliveryItems)
		//{
		//	List<string> oldSAPDeliveryNumbers = (from x in this.cacheSAPDeliveries select x.Number.Trim()).Distinct().ToList();
		//	List<string> newSAPDeliveryNumbers = (from x in zstHssDeliveryItems where !x.DeliveryNumber.Trim().jIsEmpty() && !oldSAPDeliveryNumbers.Contains(x.DeliveryNumber.Trim()) select x.DeliveryNumber.Trim()).Distinct().ToList();
		//	List<Sapdelivery> deliveries = (from x in this.db.Sapdeliveries where newSAPDeliveryNumbers.Contains(x.Number) select x).ToList();
		//	this.AddToCache(deliveries);
		//	oldSAPDeliveryNumbers = (from x in this.cacheSapshipments where !string.IsNullOrEmpty(x.Number.Trim()) select x.Number.Trim()).Distinct().ToList();
		//	newSAPDeliveryNumbers = (from x in zstHssDeliveryItems where !x.DeliveryNumber.Trim().jIsEmpty() && !oldSAPDeliveryNumbers.Contains(x.DeliveryNumber.Trim()) select x.DeliveryNumber.Trim()).Distinct().ToList();
		//	List<Sapshipment> Sapshipments = (from x in this.db.Sapshipments where newSAPDeliveryNumbers.Contains(x.DeliveryNumber) select x).ToList();
		//	this.AddToCache(Sapshipments);
		//	List<long> sapDeliveryIDS = (from x in this.db.Sapdeliveries select x.SapdeliveryId).Distinct().ToList();
		//	List<SapdeliveryItem> items = (from x in this.db.SapdeliveryItems where sapDeliveryIDS.Contains(x.SapdeliveryId) select x).ToList();
		//	this.AddToCache(items);
		//}
		//public void PrescanAndCache(ZstHssSalesOrder[] zstHssSalesOrders)
		//{
		//	List<string> oldSalesOrderNumbers = (from x in this.cacheSAPSalesOrders select x.Number.Trim()).Distinct().ToList();
		//	List<string> newSAPSalesOrderNumbers = (from x in zstHssSalesOrders where !oldSalesOrderNumbers.Contains(x.SalesOrderNumber) select x.SalesOrderNumber.Trim()).Distinct().ToList();
		//	List<SAPSalesOrder> sapSalesOrders = (from x in this.db.SAPSalesOrders where newSAPSalesOrderNumbers.Contains(x.Number) select x).ToList();
		//	this.AddToCache(sapSalesOrders);
		//}
		//public void PrescanAndCache(ZstHssSalesOrderItem[] zstHssSalesOrderItems)
		//{
		//	List<string> oldSAPSalesOrderNumbers = (from x in this.cacheSAPSalesOrders select x.Number.Trim()).Distinct().ToList();
		//	List<string> newSAPSalesOrderNumbers = (from x in zstHssSalesOrderItems where !oldSAPSalesOrderNumbers.Contains(x.SalesOrderNumber) select x.SalesOrderNumber.Trim()).Distinct().ToList();
		//	List<SAPSalesOrder> sapSalesOrders = (from x in this.db.SAPSalesOrders where newSAPSalesOrderNumbers.Contains(x.Number) select x).ToList();
		//	this.AddToCache(sapSalesOrders);
		//	List<long> SAPSalesOrderIDS = (from x in this.cacheSAPSalesOrders select x.SAPSalesOrderID).Distinct().ToList();
		//	List<SAPSalesOrderItem> sapSalesOrderItems = (from x in this.db.SAPSalesOrderItems where SAPSalesOrderIDS.Contains(x.SAPSalesOrderID) select x).ToList();
		//	this.AddToCache(sapSalesOrderItems);
		//	List<string> alreadyCachedMaterialNumbers = (from x in this.cacheSAPMaterials select x.Number.Trim()).ToList();
		//	List<string> sapmaterialnumbers = (from x in zstHssSalesOrderItems where !alreadyCachedMaterialNumbers.Contains(x.MaterialNumber.Trim()) select x.MaterialNumber.Trim()).Distinct().ToList();
		//	List<SAPMaterial> sapMaterials = (from x in this.db.SAPMaterials where sapmaterialnumbers.Contains(x.Number) select x).ToList();
		//	this.AddToCache(sapMaterials);
		//}

		public void AddToCache(List<SapsalesOrderItem> salesorderitems)
		{
			foreach (var soi in salesorderitems)
			{
				if (!cacheSAPSalesOrderItems.Contains(soi))
				{
					cacheSAPSalesOrderItems.Add(soi);
				}
			}
		}
		public void AddToCache(List<SapsalesOrder> SAPSalesOrders)
		{
			foreach (SapsalesOrder sapSalesOrder in SAPSalesOrders)
			{
				if (!cacheSAPSalesOrders.Contains(sapSalesOrder))
				{
					cacheSAPSalesOrders.Add(sapSalesOrder);
				}
			}
		}
		public void AddToCache(List<Sapstock> sapStocks)
		{
			foreach (Sapstock stock in sapStocks)
			{
				if (!cacheSAPStocks.Contains(stock))
				{
					cacheSAPStocks.Add(stock);
				}
			}
		}
		public void AddToCache(List<Sapshipment> Sapshipments)
		{
			foreach (var sm in Sapshipments)
			{
				if (!cacheSapshipments.Contains(sm))
				{
					cacheSapshipments.Add(sm);
				}
			}
		}
		public void AddToCache(List<Sapdelivery> sapDeliveries)
		{
			foreach (Sapdelivery sapDelivery in sapDeliveries)
			{
				if (!cacheSAPDeliveries.Contains(sapDelivery))
				{
					cacheSAPDeliveries.Add(sapDelivery);
				}
			}
		}
		public void AddToCache(List<SapdeliveryItem> sapDeliveryItems)
		{
			foreach (SapdeliveryItem sapDeliveryItem in sapDeliveryItems)
			{
				if (!cacheSAPDeliveryItems.Contains(sapDeliveryItem))
				{
					cacheSAPDeliveryItems.Add(sapDeliveryItem);
				}
			}
		}
		public void AddToCache(List<Sapmaterial> sapMaterials)
		{
			foreach (Sapmaterial sapMaterial in sapMaterials)
			{
				if (!cacheSAPMaterials.Contains(sapMaterial))
				{
					cacheSAPMaterials.Add(sapMaterial);
				}
			}
		}
		public void AddToCache(List<SapsoldTo> SapsoldTos)
		{
			foreach (SapsoldTo SapsoldTo in SapsoldTos)
			{
				if (!cacheSapsoldTos.Contains(SapsoldTo))
				{
					cacheSapsoldTos.Add(SapsoldTo);
				}
			}
		}
		public void AddToCache(List<SapshipTo> SapshipTos)
		{
			foreach (SapshipTo SapshipTo in SapshipTos)
			{
				if (!cacheSapshipTos.Contains(SapshipTo))
				{
					cacheSapshipTos.Add(SapshipTo);
				}
			}
		}

		public Sapmaterial getSAPMaterial(string MaterialNumber)
		{
			MaterialNumber = MaterialNumber.Trim();
			var a = (from x in cacheSAPMaterials where x.Number == MaterialNumber select x).FirstOrDefault();
			if (a != null) return a;
			//a = (from x in DB.GlobalCache.sapMaterials where x.Number == MaterialNumber select x).FirstOrDefault();
			//if (a == null) { return a; } 
			a = (from x in SAPMaterials where x.Number == MaterialNumber select x).FirstOrDefault();
			if (a != null) cacheSAPMaterials.Add(a);
			return a;
		}

		public Sapmaterial getSAPMaterialWTC(string MaterialNumber)
		{
			MaterialNumber = MaterialNumber.Trim();
			var a = (from x in cacheSAPMaterialsWTC where x.Number == MaterialNumber select x).FirstOrDefault();
			if (a != null) return a;
			//a = (from x in DB.GlobalCache.sapMaterials where x.Number == MaterialNumber select x).FirstOrDefault();
			//if (a == null) { return a; } 
			a = (from x in SAPMaterialsWTC where x.Number == MaterialNumber select x).FirstOrDefault();
			if (a != null) cacheSAPMaterialsWTC.Add(a);
			return a;
		}

		public DBCache()
		{

		}

		public DBCache(ref PortalContext _db)
		{
			db = _db;
			MissingSalesOrderNumbers = new List<string>();
			cacheSAPDeliveries = new List<Sapdelivery>();
			cacheSapshipments = new List<Sapshipment>();
			cacheSAPStorageLocations = new List<SapstorageLocation>();
			cacheSAPSalesOrders = new List<SapsalesOrder>();
			cacheSapsoldTos = new List<SapsoldTo>();
			PendingBacklogRefresh = new List<SapsoldTo>();
			cacheSAPSalesOrderItems = new List<SapsalesOrderItem>();
			cacheSAPMaterials = new List<Sapmaterial>();
			cacheSAPDeliveryItems = new List<SapdeliveryItem>();
			cachePlants = new List<Plant>();
			cacheSapshipTos = new List<SapshipTo>();
			cacheSAPDeliveryTypes = new List<SapdeliveryType>();
			cacheSAPVendors = new List<Sapvendor>();
			cacheSAPStocks = new List<Sapstock>();
			cacheSAPCharacteristicOptions = new List<SapcharacteristicOption>();
			cacheSAPMaterialsWTC = new List<Sapmaterial>();
			cachePlantsWTC = new List<Plant>();
			cacheSapshipTosWTC = new List<SapshipTo>();
			cacheSapsoldTosWTC = new List<SapsoldTo>();
			Load();
			WTCLoad();
		}
		public DBCache(PortalContext _db)
		{
			db = _db;
			MissingSalesOrderNumbers = new List<string>();
			cacheSAPDeliveries = new List<Sapdelivery>();
			cacheSapshipments = new List<Sapshipment>();
			cacheSAPStorageLocations = new List<SapstorageLocation>();
			cacheSAPSalesOrders = new List<SapsalesOrder>();
			cacheSapsoldTos = new List<SapsoldTo>();
			PendingBacklogRefresh = new List<SapsoldTo>();
			cacheSAPSalesOrderItems = new List<SapsalesOrderItem>();
			cacheSAPMaterials = new List<Sapmaterial>();
			cacheSAPDeliveryItems = new List<SapdeliveryItem>();
			cachePlants = new List<Plant>();
			cacheSapshipTos = new List<SapshipTo>();
			cacheSAPDeliveryTypes = new List<SapdeliveryType>();
			cacheSAPVendors = new List<Sapvendor>();
			cacheSAPStocks = new List<Sapstock>();
			cacheSAPCharacteristicOptions = new List<SapcharacteristicOption>();
			cachePlantsWTC = new List<Plant>();
			cacheSapshipTosWTC = new List<SapshipTo>();
			cacheSapsoldTosWTC = new List<SapsoldTo>();
			cacheSAPMaterialsWTC = new List<Sapmaterial>();
			Metrics = new List<string>();

			Load();
			WTCLoad();
		}
		private void Load()
		{
			deliveryTypes = (from dt in db.SapdeliveryTypes select dt);
			shipments = (from s in db.Sapshipments where s.DivisionId == (long)Enums.Divisions.Atlas select s);
			deliveries = (from d in db.Sapdeliveries where d.DivisionId == (long)Enums.Divisions.Atlas select d);
			shipTos = (from s in db.SapshipTos where s.DivisionId == (long)Enums.Divisions.Atlas select s);
			//soldTos = (from s in db.SapshipTos.OfType<SapsoldTo>() where s.DivisionId == (long)Enums.Divisions.Atlas select s);
			vendors = (from v in db.Sapvendors where v.DivisionId == (long)Enums.Divisions.Atlas select v);
			//plants = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionId == (long)Enums.Divisions.Atlas select l);
			//storageLocations = (from sl in db.SapstorageLocations where sl.Plant.DivisionId == (long)Enums.Divisions.Atlas select sl);
			//scrapSapsoldTos = (from sst in db.ScrapSapsoldTos where sst.SapsoldTo.DivisionId == (long)Enums.Divisions.Atlas select sst);
			//randomLengthSapsoldTos = (from rlst in db.RandomLengthSapsoldTos where rlst.SapsoldTo.DivisionId == (long)Enums.Divisions.Atlas select rlst);
			//SapSalesOrders = (from sso in db.SapsalesOrderItems where sso.DivisionId == (long)Enums.Divisions.Atlas select sso);
			sapStocks = (from stock in db.Sapstocks select stock);
			SAPMaterials = (from x in db.Sapmaterials select x);
			SapcharacteristicOptions = (from x in db.SapcharacteristicOptions select x);
		}
		public void WTCLoad()
		{
			//deliveryTypes = (from dt in db.SAPDeliveryTypes select dt);
			//shipments = (from s in db.Sapshipments where s.DivisionId == (long)Enums.Divisions.Wheatland select s);
			//deliveries = (from d in db.SAPDeliveries where d.DivisionId == (long)Enums.Divisions.Atlas select d);
			shipTosWTC = (from s in db.SapshipTos where s.DivisionId == (long)Enums.Divisions.Wheatland select s);
			//soldTosWTC = (from s in db.SapshipTos.OfType<SapsoldTo>() where s.DivisionId == (long)Enums.Divisions.Wheatland select s);
			//vendors = (from v in db.SAPVendors where v.DivisionId == (long)Enums.Divisions.Atlas select v);
			//plantsWTC = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionId == (long)Enums.Divisions.Wheatland select l);
			//storageLocations = (from sl in db.SAPStorageLocations where sl.Plant.DivisionId == (long)Enums.Divisions.Atlas select sl);
			//scrapSapsoldTos = (from sst in db.ScrapSapsoldToes where sst.SapsoldTo.DivisionId == (long)Enums.Divisions.Atlas select sst);
			//randomLengthSapsoldTos = (from rlst in db.RandomLengthSapsoldToes where rlst.SapsoldTo.DivisionId == (long)Enums.Divisions.Atlas select rlst);
			//SapSalesOrders = (from sso in db.SAPSalesOrders where sso.DivisionId == (long)Enums.Divisions.Atlas select sso);
			//sapStocks = (from stock in db.SAPStocks select stock);
			SAPMaterialsWTC = (from x in db.Sapmaterials select x);
			//SAPCharacteristicOptions = (from x in db.SAPCharacteristicOptions select x);
		}
		public void QueueUpBacklogRefresh(SapsoldTo SapsoldTo)
		{
			if (SapsoldTo == null) return;
			if (PendingBacklogRefresh.Where(x => x.SapshipToId == SapsoldTo.SapshipToId).Any())
				return;
			PendingBacklogRefresh.Add(SapsoldTo);
		}

		public SapdeliveryType getSAPDeliveryType(string sapdeliverytype)
		{
			sapdeliverytype = (sapdeliverytype).Trim();
			var a = (from x in cacheSAPDeliveryTypes where x.Sapcode == sapdeliverytype select x).FirstOrDefault();
			if (a != null) return a;
			a = (from x in db.SapdeliveryTypes where x.Sapcode == sapdeliverytype select x).FirstOrDefault();
			if (a != null) cacheSAPDeliveryTypes.Add(a);
			return a;
		}

		public void AddShipment(Sapshipment Sapshipment)
		{
			cacheSapshipments.Add(Sapshipment);
			db.Sapshipments.Add(Sapshipment);
		}
		public void AddDelivery(Sapdelivery delivery)
		{
			cacheSAPDeliveries.Add(delivery);
			db.Sapdeliveries.Add(delivery);
		}
		public void addStorageLocation(SapstorageLocation storagelocation)
		{
			cacheSAPStorageLocations.Add(storagelocation);
			db.SapstorageLocations.Add(storagelocation);
		}
		public void AddSAPDeliveryType(SapdeliveryType sdt)
		{
			db.SapdeliveryTypes.Add(sdt);
			cacheSAPDeliveryTypes.Add(sdt);
		}
		public void AddSAPDeliveryItem(SapdeliveryItem sdi)
		{
			db.SapdeliveryItems.Add(sdi);
			cacheSAPDeliveryItems.Add(sdi);
		}
		public void AddSAPSalesOrder(SapsalesOrder sso)
		{
			cacheSAPSalesOrders.Add(sso);
			db.SapsalesOrders.Add(sso);
		}
		public void AddSAPSalesOrderItem(SapsalesOrderItem ssoi)
		{
			cacheSAPSalesOrderItems.Add(ssoi);
			db.SapsalesOrderItems.Add(ssoi);
		}
		public void AddSAPStock(Sapstock ss)
		{
			db.Sapstocks.Add(ss);
			cacheSAPStocks.Add(ss);
		}
		public void DeleteSAPStock(Sapstock sapStock)
		{
			if (sapStock == null) return;
			cacheSAPStocks.Remove(sapStock);
			db.Sapstocks.Remove(sapStock);
		}
		public void DeleteDeliveryItem(SapdeliveryItem sapdeliveryitem)
		{
			if (sapdeliveryitem == null) return;
			//List<WebReleasePlantSapsalesOrderItem> webReleasePlantSAPSalesOrderItems = sapdeliveryitem.WebReleasePlantSapsalesOrderItems.ToList();
			//foreach (WebReleasePlantSapsalesOrderItem webReleasePlantSAPSalesOrderItem in webReleasePlantSAPSalesOrderItems)
			//{
			//	//sapdeliveryitem.WebReleasePlantSAPSalesOrderItems.Remove(webReleasePlantSAPSalesOrderItem);
			//}
			cacheSAPDeliveryItems.Remove(sapdeliveryitem);
			db.SapdeliveryItems.Remove(sapdeliveryitem);
		}

		public void DestroyDeliveryByNumber(string DeliveryNumber)
		{
			Sapdelivery sapDelivery = this.getDeliveryByNumber(DeliveryNumber);
			if (sapDelivery == null) return;
			//List<SapdeliveryItem> detroyItems = sapDelivery.SapdeliveryItems.ToList();
			//this.AddToCache(detroyItems);

			//foreach (SapdeliveryItem sapDeliveryItem in detroyItems)
			//{
			//	this.DeleteDeliveryItem(sapDeliveryItem);
			//}
			cacheSAPDeliveries.Remove(sapDelivery);
			db.Sapdeliveries.Remove(sapDelivery);

		}

		public Plant getPlantByID(long id)
		{
			var a = (from x in cachePlants where x.LocationId == id select x).FirstOrDefault();
			if (a != null) return a;
			a = (from x in plants where x.LocationId == id select x).FirstOrDefault();
			if (a != null) cachePlants.Add(a);
			return a;
		}
		public Plant getPlantBySalesOrg(string salesOrg)
		{
			salesOrg = (salesOrg).Trim();
			var a = (from x in cachePlants where x.SalesOrganization == salesOrg select x).FirstOrDefault();
			if (a == null) return a;
			a = (from x in plants where x.SalesOrganization == salesOrg select x).FirstOrDefault();
			if (a == null) cachePlants.Add(a);
			return a;
		}
		public Plant getPlantBySalesOrgWTC(string salesOrg)
		{
			salesOrg = (salesOrg).Trim();
			var a = (from x in cachePlantsWTC where x.SalesOrganization == salesOrg select x).FirstOrDefault();
			if (a == null) return a;
			a = (from x in plantsWTC where x.SalesOrganization == salesOrg select x).FirstOrDefault();
			if (a == null) cachePlantsWTC.Add(a);
			return a;
		}
		public Plant getPlantBySapCode(string sapCode)
		{
			sapCode = (sapCode).Trim();
			var a = (from x in cachePlants where x.Code == sapCode select x).FirstOrDefault();
			if (a == null) return a;
			a = (from x in plants where x.Code == sapCode select x).FirstOrDefault();
			if (a == null) cachePlants.Add(a);
			return a;
		}
		public Plant getPlantBySapCodeWTC(string sapCode)
		{
			sapCode = (sapCode).Trim();
			var a = (from x in cachePlantsWTC where x.Code == sapCode select x).FirstOrDefault();
			if (a == null) return a;
			a = (from x in plantsWTC where x.Code == sapCode select x).FirstOrDefault();
			if (a == null) cachePlantsWTC.Add(a);
			return a;
		}

		public SapstorageLocation getStorageLocation(long PlantID, string StorageLocation)
		{
			StorageLocation = (StorageLocation).Trim();
			var findStorageLocation = (from sl in cacheSAPStorageLocations where sl.SapstorageLocationId == PlantID && sl.Sapcode == StorageLocation select sl).FirstOrDefault();
			if (findStorageLocation != null) return findStorageLocation;
			findStorageLocation = (from sl in storageLocations where sl.SapstorageLocationId == PlantID && sl.Sapcode == StorageLocation select sl).FirstOrDefault();
			if (findStorageLocation != null)
				cacheSAPStorageLocations.Add(findStorageLocation);
			return findStorageLocation;
		}
		public SapshipTo getShipToByNumber(string ShipToNumber)
		{
			ShipToNumber = (ShipToNumber).Trim();
			var a = (from s in cacheSapshipTos where s.Number == ShipToNumber select s).FirstOrDefault();
			if (a == null) return a;
			a = (from s in shipTos where s.Number == ShipToNumber select s).FirstOrDefault();
			if (a == null) cacheSapshipTos.Add(a);
			return a;
		}
		public SapshipTo getShipToByNumberWTC(string ShipToNumber)
		{
			ShipToNumber = (ShipToNumber).Trim();
			var a = (from s in cacheSapshipTosWTC where s.Number == ShipToNumber select s).FirstOrDefault();
			if (a == null) return a;
			a = (from s in shipTosWTC where s.Number == ShipToNumber select s).FirstOrDefault();
			if (a == null) cacheSapshipTosWTC.Add(a);
			return a;
		}
		public SapsoldTo getSoldToByNumber(string soldToNumber)
		{
			soldToNumber = (soldToNumber).Trim();
			var a = (from x in cacheSapsoldTos where x.PricingNotes == soldToNumber select x).FirstOrDefault();
			if (a == null) return a;
			a = (from s in soldTos where s.PricingNotes == soldToNumber select s).FirstOrDefault();
			if (a == null) cacheSapsoldTos.Add(a);
			return a;
		}
		public SapsoldTo getSoldToByNumberWTC(string soldToNumber)
		{
			soldToNumber = (soldToNumber).Trim();
			var a = (from x in cacheSapsoldTosWTC where x.PricingNotes == soldToNumber select x).FirstOrDefault();
			if (a == null) return a;
			a = (from s in soldTosWTC where s.PricingNotes == soldToNumber select s).FirstOrDefault();
			if (a == null) cacheSapsoldTosWTC.Add(a);
			return a;
		}
		public SapsoldTo getSoldToByID(long soldToID)
		{
			var a = (from x in cacheSapsoldTos where x.SapshipToId == soldToID select x).FirstOrDefault();
			if (a == null) return a;
			a = (from s in soldTos where s.SapshipToId == soldToID select s).FirstOrDefault();
			if (a == null) cacheSapsoldTos.Add(a);
			return a;
		}
		public SapshipTo getShipToByID(long ShipToID)
		{
			var a = (from s in cacheSapshipTos where s.SapshipToId == ShipToID select s).FirstOrDefault();
			if (a == null) return a;
			a = (from s in shipTos where s.SapshipToId == ShipToID select s).FirstOrDefault();
			if (a == null) cacheSapshipTos.Add(a);
			return a;
		}
		public Sapvendor getVendorByNumber(string VendorNumber)
		{
			VendorNumber = (VendorNumber).Trim();
			var a = (from s in cacheSAPVendors where s.Number == VendorNumber select s).FirstOrDefault();
			if (a == null) return a;
			a = (from s in vendors where s.Number == VendorNumber select s).FirstOrDefault();
			if (a == null) cacheSAPVendors.Add(a);
			return a;
		}
		public SapsalesOrder getSAPSalesOrderByNumber(string SalesOrderNumber)
		{
			SapsalesOrder sso = null;
			SalesOrderNumber = (SalesOrderNumber).Trim();
			sso = (from x in cacheSAPSalesOrders where x.Number.EndsWith(SalesOrderNumber) select x).FirstOrDefault();
			if (sso != null) return sso;
			sso = (from x in SapSalesOrders where x.Number.EndsWith(SalesOrderNumber) select x).FirstOrDefault();
			if (sso != null)
				cacheSAPSalesOrders.Add(sso);
			return sso;
		}
		public SapsalesOrder getSAPSalesOrderByNumberWTC(string SalesOrderNumber)
		{
			SapsalesOrder sso = null;
			SalesOrderNumber = (SalesOrderNumber).Trim();
			sso = (from x in cacheSAPSalesOrders where x.Number.EndsWith(SalesOrderNumber) && (x.DivisionId == (long)Enums.Divisions.Wheatland) select x).FirstOrDefault();
			if (sso != null) return sso;
			sso = (from x in SapSalesOrders where x.Number.EndsWith(SalesOrderNumber) && (x.DivisionId == (long)Enums.Divisions.Wheatland) select x).FirstOrDefault();
			if (sso != null)
				cacheSAPSalesOrders.Add(sso);
			return sso;
		}
		//public Sapstock getSAPStock(ZstHssPortalStock zstHssPortalStock)
		//{
		//	Sapstock stock = null;
		//	string sapCode = zstHssPortalStock.StockId;
		//	string salesOrder = zstHssPortalStock.SalesOrder.Trim();
		//	int salesOrderItem = zstHssPortalStock.SalesOrderItem.Trim().ToInt();
		//	if (string.IsNullOrEmpty(salesOrder) && !(salesOrderItem == 0))
		//	{
		//		//KMAT
		//		stock = (from x in cacheSAPStocks where x.SapsalesOrderNumber == salesOrder && (x.SapsalesOrderItemNumber ?? 0) == salesOrderItem select x).FirstOrDefault();
		//		if (stock != null) return stock;
		//		stock = (from x in sapStocks where x.SapsalesOrderNumber == salesOrder && (x.SapsalesOrderItemNumber ?? 0) == salesOrderItem select x).FirstOrDefault();
		//	}
		//	else
		//	{
		//		//FERT
		//		stock = (from x in cacheSAPStocks where x.Sapcode == zstHssPortalStock.StockId select x).FirstOrDefault();
		//		if (stock != null) return stock;
		//		stock = (from x in sapStocks where x.Sapcode == zstHssPortalStock.StockId select x).FirstOrDefault();
		//	}
		//	if (stock != null)
		//	{
		//		cacheSAPStocks.Add(stock);
		//	}
		//	return stock;
		//}

		//public Sapstock getSAPStock(ZstHssCreateSalesOrderIn zstHssCreateSalesOrderIn)
		//{
		//	Sapstock stock = null;
		//	bool KMAT = zstHssCreateSalesOrderIn.IsKmat.Trim().ToUpper() == "X";
		//	string sapCode = zstHssCreateSalesOrderIn.ConfigurationNumber.Trim();
		//	string salesOrder = zstHssCreateSalesOrderIn.SalesOrderNumber.Trim();
		//	int salesOrderItem = zstHssCreateSalesOrderIn.SalesOrderItemNumber.Trim().ToInt();

		//	if (KMAT)
		//	{
		//		stock = (from x in cacheSAPStocks where x.SapsalesOrderNumber == salesOrder && (x.SapsalesOrderItemNumber ?? 0) == salesOrderItem select x).FirstOrDefault();
		//		if (stock != null) return stock;
		//		stock = (from x in sapStocks where x.SapsalesOrderNumber == salesOrder && (x.SapsalesOrderItemNumber ?? 0) == salesOrderItem select x).FirstOrDefault();
		//	}
		//	else
		//	{   //FERT
		//		stock = (from x in cacheSAPStocks where x.Sapcode == sapCode select x).FirstOrDefault();
		//		if (stock != null) return stock;
		//		stock = (from x in sapStocks where x.Sapcode == sapCode select x).FirstOrDefault();
		//	}
		//	if (stock != null)
		//	{
		//		cacheSAPStocks.Add(stock);
		//	}
		//	return stock;
		//}
		//public Sapstock getSAPStock(Portal.Business.WheatlandPortal.ZstHssCreateSalesOrderIn zstHssCreateSalesOrderIn)
		//{
		//	Sapstock stock = null;
		//	bool KMAT = zstHssCreateSalesOrderIn.IsKmat.Trim().ToUpper() == "X";
		//	string sapCode = zstHssCreateSalesOrderIn.ConfigurationNumber.Trim();
		//	string salesOrder = zstHssCreateSalesOrderIn.SalesOrderNumber.Trim();
		//	int salesOrderItem = zstHssCreateSalesOrderIn.SalesOrderItemNumber.Trim().ToInt();

		//	if (KMAT)
		//	{
		//		stock = (from x in cacheSAPStocks where x.SapsalesOrderNumber == salesOrder && (x.SapsalesOrderItemNumber ?? 0) == salesOrderItem select x).FirstOrDefault();
		//		if (stock != null) return stock;
		//		stock = (from x in sapStocks where x.SapsalesOrderNumber == salesOrder && (x.SapsalesOrderItemNumber ?? 0) == salesOrderItem select x).FirstOrDefault();
		//	}
		//	else
		//	{   //FERT
		//		stock = (from x in cacheSAPStocks where x.Sapcode == sapCode select x).FirstOrDefault();
		//		if (stock != null) return stock;
		//		stock = (from x in sapStocks where x.Sapcode == sapCode select x).FirstOrDefault();
		//	}
		//	if (stock != null)
		//	{
		//		cacheSAPStocks.Add(stock);
		//	}
		//	return stock;
		//}
		public Sapstock getSAPStockOld(string sapCode, string batchNumber)
		{
			Sapstock stock = null;
			sapCode = (sapCode).Trim();

			if (!string.IsNullOrEmpty(batchNumber))
			{
				stock = (from x in cacheSAPStocks where x.BatchNumber == batchNumber select x).FirstOrDefault();
				if (stock != null) return stock;
				stock = (from x in sapStocks where x.BatchNumber == batchNumber select x).FirstOrDefault();
			}
			else
			{
				stock = (from x in cacheSAPStocks where x.Sapcode == sapCode select x).FirstOrDefault();
				if (stock != null) return stock;
				stock = (from x in sapStocks where x.Sapcode == sapCode select x).FirstOrDefault();
			}

			if (stock != null)
				cacheSAPStocks.Add(stock);

			return stock;
		}
		public Sapstock getSAPStockByID(long sapStockID)
		{
			Sapstock stock = null;
			if (sapStockID == 0) return null;
			stock = (from x in cacheSAPStocks where x.SapstockId == sapStockID select x).FirstOrDefault();
			if (stock != null) return stock;
			stock = (from x in sapStocks where x.SapstockId == sapStockID select x).FirstOrDefault();
			if (stock != null)
				cacheSAPStocks.Add(stock);
			return stock;
		}
		public SapcharacteristicOption getSAPTubeStandard(string grade)
		{
			SapcharacteristicOption sapCharacteristicOption = null;
			grade = (grade).Trim();
			sapCharacteristicOption = (from x in cacheSAPCharacteristicOptions where x.SapcharacteristicTypeId == (long)Enums.AtlasSAPCharacteristicTypes.TubeStandard && x.Sapcode == grade select x).FirstOrDefault();
			if (sapCharacteristicOption != null) return sapCharacteristicOption;
			sapCharacteristicOption = (from x in SapcharacteristicOptions where x.SapcharacteristicTypeId == (long)Enums.AtlasSAPCharacteristicTypes.TubeStandard && x.Sapcode == grade select x).FirstOrDefault();
			if (sapCharacteristicOption != null)
				cacheSAPCharacteristicOptions.Add(sapCharacteristicOption);
			return sapCharacteristicOption;
		}
		//public SapsalesOrderItem getSAPSalesOrderItem(string SalesOrderNumber, string Position)
		//{
		//	int positionInt = Convert.ToInt32((Position).Trim());
		//	SalesOrderNumber = SalesOrderNumber.Trim();

		//	SapsalesOrderItem soi = (from x in cacheSAPSalesOrderItems where x.SapsalesOrderId.DivisionId == (long)Enums.Divisions.Atlas && x.SAPSalesOrder.Number == SalesOrderNumber && x.Position == positionInt select x).FirstOrDefault();
		//	if (soi != null) return soi;
		//	//if (!Size.jIsEmpty())
		//	//   soi = (from x in db.SAPSalesOrderItems where x.SAPSalesOrder.DivisionId == (long)Enums.Divisions.Atlas && x.SAPSalesOrder.Number == SalesOrderNumber && x.Position == positionInt && x.SAPMaterial.SAPMaterialGroup.SAPCode.Contains(Size) select x).FirstOrDefault();
		//	//else
		//	soi = (from x in db.SapsalesOrderItems where x.SAPSalesOrder.DivisionId == (long)Enums.Divisions.Atlas && x.SAPSalesOrder.Number == SalesOrderNumber && x.Position == positionInt select x).FirstOrDefault();
		//	if (soi != null) cacheSAPSalesOrderItems.Add(soi);
		//	return soi;
		//}
		//public SapsalesOrderItem getSAPSalesOrderItemWTC(string SalesOrderNumber, string Position)
		//{
		//	int positionInt = Convert.ToInt32((Position).Trim());
		//	SalesOrderNumber = SalesOrderNumber.Trim();

		//	SapsalesOrderItem soi = (from x in cacheSAPSalesOrderItems where x.SAPSalesOrder.DivisionId == (long)Enums.Divisions.Wheatland && x.SAPSalesOrder.Number == SalesOrderNumber && x.Position == positionInt select x).FirstOrDefault();
		//	if (soi != null) return soi;
		//	//if (!Size.jIsEmpty())
		//	//   soi = (from x in db.SAPSalesOrderItems where x.SAPSalesOrder.DivisionId == (long)Enums.Divisions.Atlas && x.SAPSalesOrder.Number == SalesOrderNumber && x.Position == positionInt && x.SAPMaterial.SAPMaterialGroup.SAPCode.Contains(Size) select x).FirstOrDefault();
		//	//else
		//	soi = (from x in db.SapsalesOrderItems where x.SAPSalesOrder.DivisionId == (long)Enums.Divisions.Wheatland && x.SAPSalesOrder.Number == SalesOrderNumber && x.Position == positionInt select x).FirstOrDefault();
		//	if (soi != null) cacheSAPSalesOrderItems.Add(soi);
		//	return soi;
		//}
		public Sapdelivery getDeliveryByNumber(string DeliveryNumber)
		{
			DeliveryNumber = (DeliveryNumber).Trim();
			Sapdelivery sapDelivery = null;
			sapDelivery = (from d in cacheSAPDeliveries where d.Number == DeliveryNumber select d).FirstOrDefault();
			if (sapDelivery != null) return sapDelivery;
			if (sapDelivery == null)
			{
				sapDelivery = (from d in deliveries where d.Number == DeliveryNumber select d).FirstOrDefault();
			}
			if (sapDelivery != null) cacheSAPDeliveries.Add(sapDelivery);
			return sapDelivery;
		}
		public SapdeliveryItem getDeliveryItem(string DeliveryNumber, string Position)
		{
			int positionInt = Convert.ToInt32((Position).Trim());
			Sapdelivery sapDelivery = getDeliveryByNumber(DeliveryNumber);
			if (sapDelivery == null) return null;
			//SAPDeliveryItem sapDeliveryItem = (from x in cacheSAPDeliveryItems where x.Position == PositionInt && x.SAPDeliveryID == sapDelivery.SAPDeliveryID select x).FirstOrDefault();
			//if (!sapDeliveryItem.IsNull()) return sapDeliveryItem;
			SapdeliveryItem sapDeliveryItem = null; //(from x in sapDelivery.SAPDeliveryItems where x.Position == positionInt select x).FirstOrDefault();
			if (sapDeliveryItem!=null) cacheSAPDeliveryItems.Add(sapDeliveryItem);
			return sapDeliveryItem;
		}
		public Sapshipment getShipment(string ShipmentNumber, string DeliveryNumber)
		{
			Sapshipment Sapshipment = null;

			ShipmentNumber = (ShipmentNumber).Trim();
			DeliveryNumber = (DeliveryNumber).Trim();

			if (Sapshipment == null)
			{ // if not found look in cache of new shipments
				if (!string.IsNullOrEmpty(ShipmentNumber))
				{
					Sapshipment = (from s in cacheSapshipments where s.Number == ShipmentNumber select s).FirstOrDefault();
				}
				else
				{
					Sapshipment = (from s in cacheSapshipments where s.DeliveryNumber == DeliveryNumber select s).FirstOrDefault();
				}
			}
			if (Sapshipment == null)
			{
				if (!string.IsNullOrEmpty(ShipmentNumber))
				{
					Sapshipment = db.Sapshipments.Where(x => x.Number == ShipmentNumber).FirstOrDefault();
				}
				else
				{
					Sapshipment = db.Sapshipments.Where(x => x.DeliveryNumber == DeliveryNumber).FirstOrDefault();
				}
				if (Sapshipment != null && !cacheSapshipments.Contains(Sapshipment))
				{
					cacheSapshipments.Add(Sapshipment);
				}
			}
			return Sapshipment;
		}
		public void CleanUp()
		{
			DBCache dbcache = this;
			//if (!this.MissingSalesOrderNumbers.IsNull() && this.MissingSalesOrderNumbers.Any()) {
			//	foreach (string soinumber in this.MissingSalesOrderNumbers) {
			//		var a = SAPSalesOrder.SAPSearch(null, null, new List<SapshipTo>(), new List<SapsoldTo>(), new List<Plant>(), soinumber, true);
			//		SAPSalesOrder.StoreSalesOrders(ref dbcache, a.EtHssSalesOrders);
			//		SAPSalesOrder.StoreSalesOrderItems(ref dbcache, a.EtHssSalesOrderItems, new List<SAPSalesOrderItem>(), false, false);
			//	}
			//}
			if (PendingBacklogRefresh.Any())
			{
				db.SaveChanges();
				var soldToNumbers = (from x in PendingBacklogRefresh.Distinct() select x.PricingNotes).ToList();
				//SapsoldTo.RefreshBacklog(ref dbcache, soldToNumbers);
			}
			PendingBacklogRefresh.Clear();
			db.SaveChanges();
		}
	}
}

