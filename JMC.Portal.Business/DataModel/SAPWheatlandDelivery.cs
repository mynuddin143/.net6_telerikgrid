using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business;
using JMC.Portal.Business.PortalModels;

namespace JMC.Portal.Business.DataModel
{
    public class SAPWheatlandDeliveryEx: SapwheatlandDelivery
    {
     

        //public string BatchNumber { get; set; }
        
        //public string CustomerPO { get; set; }
        
        //public int DeliveryID { get; set; }
        
        //public string DeliveryNumber { get; set; }
        
        //public string HeatNumber { get; set; }
        
        //public string ItemDesc { get; set; }
        
        //public string MaterialNumber { get; set; }
        
        //public DateTime? PGIDate { get; set; }
        
        //public string RunNumber { get; set; }
        
        //public string SalesOrderNumber { get; set; }
        
        //public string ShipToNumber { get; set; }
        
        //public string SoldToNumber { get; set; }

        //public string ShipToName 
        //{
        //    get{
        //    string ShipToName = "";
        //    using (PortalContext db = new PortalContext()) 
        //    {
        //        var row = from r in db.SAPShipTo where r.Number == "0000011275" select r.Name;
        //        ShipToName = row.FirstOrDefault();
        //    }
        //    return ShipToName;
        //    }
           
        //}


        public string ShipToName
        {
            get;
            set;
        }

        public string SoldToName
        {
            get;
            set;
        }
        
        //public static SAPWheatlandDelivery CreateSAPWheatlandDelivery(int deliveryID);
    }
}
