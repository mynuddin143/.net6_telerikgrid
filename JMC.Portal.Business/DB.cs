using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business;
using System.Collections;
using JMC.Portal.Business.DataModel;

namespace JMC.Portal.Business
{

    public static class DB
    {
        public static GlobalCache GlobalCache { get; set; }
        public static List<LocalCache> LocalCaches { get; set; }

        public static LocalCache FindOrInsertLocalCache(long userID)
        {
            LocalCache localCache = DB.LocalCaches.Where(lc => lc.UserID == userID).FirstOrDefault();

            if (localCache.IsNull() || localCache.UserID <= 0)
            {
                localCache = new LocalCache(userID);
                DB.LocalCaches.Add(localCache);
            }

            return localCache;
        }
    }
    public class GlobalCache
    {
        public List<SAPMaterial> SAPMaterials { get; set; }
        public List<Square> squares { get; set; }
        public List<Rectangle> rectangles { get; set; }
        public List<Round> rounds { get; set; }
        public List<WtcpipeSize> WTCRounds { get; set; }
        public List<Gauge> gauges { get; set; }
        public List<DataView> dataViews { get; set; }
        public List<PipeSize> pipeSizes { get; set; }
    }

    public class LocalCache
    {
        public long UserID { get; set; }
        public List<SAPSalesDeliveryModel> DeliveryHistoryResults { get; set; }
        public List<SAPSalesOrderItemViewModel> OrderHistoryResults { get; set; }
        public List<PersonDataViewTemplate> PersonalDataViewTemplates { get; set; }
        public List<ClaimDeliveryItem> ClaimDeliveryHistoryResults { get; set; }
        public List<CustomerClaimsModel> CustomerClaimsResults { get; set; }
        public List<ClaimHistory> ClaimHistoryResults { get; set; }

        public LocalCache(long userID)
        {
            this.UserID = userID;

            this.DeliveryHistoryResults = new List<SAPSalesDeliveryModel>();
            this.OrderHistoryResults = new List<SAPSalesOrderItemViewModel>();
            this.PersonalDataViewTemplates = new List<PersonDataViewTemplate>();
            this.ClaimDeliveryHistoryResults = new List<ClaimDeliveryItem>();
            this.CustomerClaimsResults = new List<CustomerClaimsModel>();
            this.ClaimHistoryResults = new List<ClaimHistory>();
        }
    }
}