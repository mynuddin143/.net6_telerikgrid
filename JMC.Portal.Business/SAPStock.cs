using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.HSSPortalSales;
using JMC.Portal.Business.WheatlandPortal;
using System.Configuration;
using System.Diagnostics;

namespace JMC.Portal.Business
{
    public partial class SAPStock
    {
        public int PiecesPerBundle
        {
            get
            {
                int factor = 1;

                if (Bundling1.HasValue && Bundling1.Value != 0)
                {
                    factor *= _Bundling1.Value;
                }

                if (Bundling2.HasValue && Bundling1.Value != 0)
                {
                    factor *= _Bundling2.Value;
                }

                if (factor == 1 && (BundlingRound ?? 0) != 0)
                {
                    factor *= BundlingRound.Value;
                }
                return factor;
            }
        }

        public decimal WeightPerPiece
        {
            get
            {
                return (this.Weight / this.AvailablePieces);
            }
        }

        public int AvailableBundles
        {
            get
            {
                return AvailablePieces / PiecesPerBundle;
            }
        }

        public decimal WeightPerFoot
        {
            get
            {
                return (this.TubeLength == 0) ? 1 : (this.WeightPerPiece / this.TubeLength);
            }
        }

        public decimal DeliveredPrice { get; private set; }
        public string PartNumber { get; private set; }

        public static void RefreshAllFromAtlasSAP(string email)
        {
            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            Stopwatch timeLoadCache = new Stopwatch();
            Stopwatch timeInSAP = new Stopwatch();
            Stopwatch timeSaving = new Stopwatch();
            Stopwatch timeProcessing = new Stopwatch();

            timeLoadCache.Start();
            List<SAPStock> SAPStocks = (from s in db.Sapstocks select s).ToList();
            Dictionary<string, SAPCharacteristicOption> sapTubeStandards = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.TubeStandard select co).ToDictionary(x => x.Sapcode, y => y);
            Dictionary<string, SAPCharacteristicOption> sapSpecifications = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.Specification select co).ToDictionary(x => x.Sapcode, y => y);
            Dictionary<string, Plant> plants = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionID == (long)Enums.Divisions.Atlas select l).ToDictionary(x => x.Code, y => y);
            Dictionary<string, SAPMaterial> SAPMaterials = (from m in db.Sapmaterials where m.DivisionID == (long)Enums.Divisions.Atlas select m).ToDictionary(x => x.Number, y => y);

            //List<SAPCharacteristicOption> SAPMaterialGroups = (from co in db.SAPCharacteristicOption where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup select co).ToList();
            timeLoadCache.Stop();

            timeProcessing.Start();

            //foreach (SAPCharacteristicOption materialGroup in SAPMaterialGroups) {
            ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            ZfmGetHssPortalStock getHssPortalStock = new ZfmGetHssPortalStock();
            //getHssPortalStock.ImMaterialGroup = materialGroup.SAPCode;

            timeProcessing.Stop();
            timeInSAP.Start();

            portalSalesService.Open();
            ZfmGetHssPortalStockResponse getHssPortalStockResponse = portalSalesService.ZfmGetHssPortalStockAsync(getHssPortalStock);
            portalSalesService.Close();

            timeInSAP.Stop();
            timeProcessing.Start();

            SAPStock.RefreshFromAtlasSAP(ref insertedCount, ref db, ref SAPStocks, ref sapTubeStandards, ref sapSpecifications, null, ref plants, ref SAPMaterials, getHssPortalStockResponse.EtHssPortalStock);

            //timeProcessing.Stop();
            //}

            timeProcessing.Stop();
            timeSaving.Start();

            db.SaveChanges();

            timeSaving.Stop();
            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append("Cache Load Time " + timeLoadCache.ElapsedMilliseconds + " ms<br />");
            emailStringBuilder.Append("db.SaveChanges() " + timeSaving.ElapsedMilliseconds + " ms<br />");
            emailStringBuilder.Append("Processing Time " + timeProcessing.ElapsedMilliseconds + " ms<br />");
            emailStringBuilder.Append("SAP Processing Time " + timeInSAP.ElapsedMilliseconds + " ms<br />");

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");

            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds<br /><br />");

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Stock Download Results", emailStringBuilder.ToString());
        }

        public static void RefreshFromAtlasSAP(ref DBCache dbcache, string SAPMaterialGroupCode)
        {
            if (dbcache.Metrics.IsNull()) dbcache.Metrics = new List<string>();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            SAPCharacteristicOption materialGroup = (from co in dbcache.db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup && co.Sapcode == SAPMaterialGroupCode select co).FirstOrDefault();
            if (!materialGroup.IsNull())
            {
                ZfmGetHssPortalStockResponse getHssPortalStockResponse = GetStockFromSAP(ref dbcache, SAPMaterialGroupCode);
                sw.Stop();

                dbcache.Metrics.Add("Time IN SAP " + sw.ElapsedMilliseconds);
                sw.Stop();
                sw.Reset();
                sw.Start();
                List<SAPStock> oldStocks = (from x in dbcache.db.Sapstocks where x.Sapmaterial.DivisionID == (long)Enums.Divisions.Atlas && x.Sapmaterial.SapmaterialGroupID == materialGroup.SapcharacteristicOptionID select x).ToList();
                dbcache.AddToCache(oldStocks);
                sw.Stop();
                dbcache.Metrics.Add("Load Old Stock " + sw.ElapsedMilliseconds);
                sw.Stop();
                sw.Reset();
                sw.Start();
                SAPStock.StoreStockItems(ref dbcache, getHssPortalStockResponse.EtHssPortalStock, oldStocks);
                sw.Stop();
                dbcache.Metrics.Add("Store New Stock " + sw.ElapsedMilliseconds);
            }
        }

        public static ZfmGetHssPortalStockResponse GetStockFromSAP(ref DBCache dbcache, string SAPMaterialGroupCode)
        {
            SAPCharacteristicOption materialGroup = (from co in dbcache.db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup && co.Sapcode == SAPMaterialGroupCode select co).FirstOrDefault();
            if (!materialGroup.IsNull())
            {
                ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
                portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

                ZfmGetHssPortalStock getHssPortalStock = new ZfmGetHssPortalStock();
                getHssPortalStock.ImMaterialGroup = materialGroup.Sapcode;
                //getHssPortalStock.ItClsedRollingInd = "Y";

                portalSalesService.Open();
                ZfmGetHssPortalStockResponse getHssPortalStockResponse = portalSalesService.ZfmGetHssPortalStockAsync(getHssPortalStock);
                portalSalesService.Close();

                return getHssPortalStockResponse;
            }
            return null;
        }


        public static ZfmGetHssPortalStocksResponse GetWheatlandPipeStockFromSAP(List<SAPMaterial> materials, List<Plant> plants, string grades = "", string materialGroup = "")
        {
            //SAPCharacteristicOption materialGroup = (from co in dbcache.db.SAPCharacteristicOption where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup && co.SAPCode == SAPMaterialGroupCode select co).FirstOrDefault();
            if (!materials.IsNull())
            {
                zws_portalClient portalSalesService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
                portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
                portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];
                //string[] mt = {"128621000"};
                ZfmGetHssPortalStocks getHssPortalStock = new ZfmGetHssPortalStocks();
                //getHssPortalStock.ImMaterialGroup = materialGroup.SAPCode;
                getHssPortalStock.ItMaterialNumbers = (from m in materials select m.Number).ToArray();
                //getHssPortalStock.ItMaterialNumbers = mt;
                getHssPortalStock.ItPlantNumbers = (from p in plants select p.Code).ToArray();

                portalSalesService.Open();
                ZfmGetHssPortalStocksResponse getHssPortalStockResponse = portalSalesService.ZfmGetHssPortalStocksAsync(getHssPortalStock);
                portalSalesService.Close();

                return getHssPortalStockResponse;
            }
            return null;
        }

        public static void RefreshFromAtlasSAP(ref int insertedCount, ref PortalEntities db, ref List<SAPStock> SAPStocks, ref Dictionary<string, SAPCharacteristicOption> sapTubeStandards, ref Dictionary<string, SAPCharacteristicOption> sapSpecifications, SAPCharacteristicOption materialGroup, ref Dictionary<string, Plant> plants, ref Dictionary<string, SAPMaterial> SAPMaterials, ZstHssPortalStock[] zstHssPortalStocks)
        {
            List<SAPStock> oldStock = null;
            int count = 0;

            if (materialGroup.IsNull())
            {
                oldStock = (from s in db.Sapstocks select s).ToList();
            }
            else
            {
                oldStock = (from s in db.Sapstocks where s.Sapmaterial.SapmaterialGroupID == materialGroup.SapcharacteristicOptionID select s).ToList();
            }

            foreach (ZstHssPortalStock hssPortalStock in zstHssPortalStocks)
            {
                hssPortalStock.StockId = (hssPortalStock.StockId).TrimNull();

                if (hssPortalStock.StockId.ToInt() > 0)
                {
                    SAPStock SAPStock = null;
                    SAPCharacteristicOption sapTubeStandard = null;
                    SAPCharacteristicOption sapSpecification = null;
                    Plant plant = null;
                    SAPMaterial SAPMaterial = null;

                    SAPStock = (from s in SAPStocks where s.Sapcode == hssPortalStock.StockId && s.BatchNumber == hssPortalStock.BatchNumber select s).FirstOrDefault();
                    sapTubeStandards.TryGetValue(hssPortalStock.Grade, out sapTubeStandard);
                    sapSpecifications.TryGetValue(hssPortalStock.TubeSpi, out sapSpecification);
                    plants.TryGetValue(hssPortalStock.Plant, out plant);
                    SAPMaterials.TryGetValue(hssPortalStock.MaterialNumber, out SAPMaterial);

                    if (!SAPMaterial.IsNull() && !plant.IsNull())
                    {
                        if (SAPStock.IsNull())
                        {
                            SAPStock = new SAPStock();
                            SAPStock.Sapcode = hssPortalStock.StockId;
                            SAPStock.BatchNumber = hssPortalStock.BatchNumber;
                            SAPStock.BatchDate = hssPortalStock.BatchDate.IsMin() ? (DateTime?)null : hssPortalStock.BatchDate;

                            SAPStocks.Add(SAPStock);
                            db.Sapstocks.Add(SAPStock);
                            insertedCount++;
                        }
                        if (oldStock.Contains(SAPStock))
                        {
                            oldStock.Remove(SAPStock);
                        }

                        SAPStock.Sapmaterial = SAPMaterial;
                        SAPStock.Plant = plant;
                        SAPStock.TubeLength = hssPortalStock.Length.ToDecimal();
                        SAPStock.AvailablePieces = hssPortalStock.Pieces.ToInt();
                        SAPStock.Weight = hssPortalStock.Weight;
                        SAPStock.Uom = hssPortalStock.Uom;
                        SAPStock.Name = !hssPortalStock.MaterialDescription.jIsEmpty() ? hssPortalStock.MaterialDescription : SAPMaterial.Name;

                        if (SAPMaterial.Configurable)
                        {
                            SAPStock.Bundling1 = hssPortalStock.Bundling1;
                            SAPStock.Bundling2 = hssPortalStock.Bundling2;
                            SAPStock.BundlingRound = hssPortalStock.BundlingRound.ToNullableInt();
                            SAPStock.SaptubeStandard = sapTubeStandard;
                            SAPStock.Sapspecification = sapSpecification;
                            SAPStock.Grade = hssPortalStock.SteelGrade;
                        }
                        else
                        {
                            SAPStock.Bundling1 = SAPMaterial.Bundling1;
                            SAPStock.Bundling2 = SAPMaterial.Bundling2;
                            SAPStock.BundlingRound = SAPMaterial.BundlingRound;
                            SAPStock.SaptubeStandard = SAPMaterial.SaptubeStandard;
                            SAPStock.Grade = SAPMaterial.SaptubeStandard.IsNull() ? hssPortalStock.SteelGrade : SAPMaterial.SaptubeStandard.Name;

                            //if (SAPStock.TubeLength <= 0) {
                            //  SAPStock.TubeLength = SAPMaterial.Length.ToDecimal();
                            //}
                        }

                        SAPStock.SapsalesOrderNumber = hssPortalStock.SalesOrder;
                        SAPStock.SapsalesOrderItemNumber = hssPortalStock.SalesOrderItem.ToInt();
                        SAPStock.SapsalesOrderItemID = null;
                        // 03-11-2019 to implement stocking items tab in the portal
                        SAPStock.MaxStockLevel = hssPortalStock.MaxStockLevel > 0 ? true : false;

                        if (SAPMaterial.PieceWeight > 0)
                        {
                            SAPStock.AvailablePieces = Math.Round((SAPStock.Weight.ToDecimal() / SAPMaterial.PieceWeight.ToDecimal()), 0, MidpointRounding.AwayFromZero).ToInt();
                        }
                    }

                    count++;

                    if (count % 100 == 0)
                    {
                        db.SaveChanges();
                    }
                }
            }

            db.SaveChanges();

            foreach (SAPStock SAPStock in oldStock)
            {
                List<ShoppingCartSapstock> shoppingCartSAPStocks = SAPStock.ShoppingCartSapstocks.ToList();

                foreach (ShoppingCartSapstock shoppingCartSAPStock in shoppingCartSAPStocks)
                {
                    db.ShoppingCartSapstocks.Remove(shoppingCartSAPStock);
                }

                SAPStocks.Remove(SAPStock);
                db.Sapstocks.Remove(SAPStock);
            }

            db.SaveChanges();
        }

        public static void StoreStockItems(ref DBCache dbcache, ZstHssPortalStock[] zstHssPortalStocks, List<SAPStock> oldStock)
        {
            Dictionary<string, SAPCharacteristicOption> sapSpecifications = (from co in dbcache.db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.Specification select co).ToDictionary(x => x.Sapcode, y => y);

            foreach (ZstHssPortalStock zstHssPortalStock in zstHssPortalStocks)
            {
                SAPCharacteristicOption sapSpecification = null;

                zstHssPortalStock.StockId = (zstHssPortalStock.StockId).TrimNull();

                if (zstHssPortalStock.StockId.ToInt() > 0)
                {
                    SAPStock SAPStock = dbcache.getSAPStock(zstHssPortalStock);

                    SAPCharacteristicOption sapTubeStandard = dbcache.getSAPTubeStandard(zstHssPortalStock.Grade);
                    sapSpecifications.TryGetValue(zstHssPortalStock.TubeSpi, out sapSpecification);
                    Plant plant = dbcache.getPlantBySapCode(zstHssPortalStock.Plant);
                    zstHssPortalStock.MaterialNumber = (zstHssPortalStock.MaterialNumber).TrimNull();
                    SAPMaterial SAPMaterial = DB.GlobalCache.SAPMaterials.Where(x => x.Number == zstHssPortalStock.MaterialNumber).FirstOrDefault();

                    if (SAPMaterial.IsNull())
                    {
                        SAPMaterial = dbcache.getSAPMaterial(zstHssPortalStock.MaterialNumber);
                    }

                    if (!SAPMaterial.IsNull() && !plant.IsNull())
                    {
                        if (SAPStock.IsNull())
                        {
                            SAPStock = new SAPStock();
                            SAPStock.Sapcode = zstHssPortalStock.StockId;
                            SAPStock.BatchNumber = zstHssPortalStock.BatchNumber;
                            SAPStock.BatchDate = zstHssPortalStock.BatchDate.IsMin() ? (DateTime?)null : zstHssPortalStock.BatchDate;

                            dbcache.AddSAPStock(SAPStock);
                        }
                        else
                        {
                            oldStock.Remove(SAPStock);
                        }

                        SAPStock.SapmaterialID = SAPMaterial.SapmaterialID;
                        SAPStock.Plant = plant;
                        SAPStock.TubeLength = zstHssPortalStock.Length.ToDecimal();
                        SAPStock.AvailablePieces = zstHssPortalStock.Pieces.ToInt();
                        SAPStock.Weight = zstHssPortalStock.Weight;
                        SAPStock.Uom = zstHssPortalStock.Uom;

                        if (SAPMaterial.Configurable)
                        {
                            SAPStock.Name = zstHssPortalStock.MaterialDescription;
                            SAPStock.Bundling1 = zstHssPortalStock.Bundling1;
                            SAPStock.Bundling2 = zstHssPortalStock.Bundling2;
                            SAPStock.BundlingRound = zstHssPortalStock.BundlingRound.ToNullableInt();
                            SAPStock.SaptubeStandard = sapTubeStandard;
                            SAPStock.Sapspecification = sapSpecification;
                            SAPStock.Grade = zstHssPortalStock.SteelGrade;
                        }
                        else
                        {
                            SAPStock.Name = SAPMaterial.Name;
                            SAPStock.Bundling1 = SAPMaterial.Bundling1;
                            SAPStock.Bundling2 = SAPMaterial.Bundling2;
                            SAPStock.BundlingRound = SAPMaterial.BundlingRound;
                            SAPStock.SaptubeStandardID = SAPMaterial.SaptubeStandardID;
                            SAPStock.Grade = SAPMaterial.SaptubeStandard.IsNull() ? zstHssPortalStock.SteelGrade : SAPMaterial.SaptubeStandard.Name;
                        }

                        SAPStock.SapsalesOrderNumber = zstHssPortalStock.SalesOrder.TrimNull();
                        SAPStock.SapsalesOrderItemNumber = zstHssPortalStock.SalesOrderItem.ToInt();
                        SAPStock.SapsalesOrderItemID = null;
                        // 03-11-2019 to implement stocking items tab in the portal
                        SAPStock.MaxStockLevel = zstHssPortalStock.MaxStockLevel > 0 ? true : false;

                        if (SAPMaterial.PieceWeight > 0)
                        {
                            SAPStock.AvailablePieces = Math.Round((SAPStock.Weight.ToDecimal() / SAPMaterial.PieceWeight.ToDecimal()), 0, MidpointRounding.AwayFromZero).ToInt();
                        }
                    }
                }
            }
            foreach (SAPStock SAPStock in oldStock)
            {
                List<ShoppingCartSapstock> shoppingCartSAPStocks = dbcache.db.ShoppingCartSapstocks.Where(x => x.SapstockID == SAPStock.SapstockID).ToList();
                foreach (ShoppingCartSapstock shoppingCartSAPStock in shoppingCartSAPStocks)
                {
                    ShoppingCart aUsersShoppingCart = shoppingCartSAPStock.ShoppingCart;
                    if (!aUsersShoppingCart.IsNull())
                    {
                        aUsersShoppingCart.ShoppingCartSapstocks.Remove(shoppingCartSAPStock);
                    }
                    dbcache.db.Remove(shoppingCartSAPStock);
                }
                dbcache.DeleteSAPStock(SAPStock);
            }
            dbcache.db.SaveChanges();
            dbcache.CleanUp();
        }

        public void RefeshDeliveredPrice(decimal deliveredPrice)
        {
            this.DeliveredPrice = deliveredPrice;
        }


        public void RefeshPartNumber(string partNumber)
        {
            this.PartNumber = partNumber;
        }
    }
}
