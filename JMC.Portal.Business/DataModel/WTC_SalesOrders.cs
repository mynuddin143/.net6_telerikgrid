using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Configuration;
using JMC.Portal.Business.WheatlandPortal;

namespace JMC.Portal.Business.DataModel
{
    public class WTC_SalesOrders
    {
        public string PurchaseOrder { get; set; }
        public string CustPart { get; set; }
        public string Item { get; set; }
        public string OrderedPcsLbs  { get; set; }
        public string NotReadyPcsLbs { get; set; }
        public string ReadyPcsLbs { get; set; }
        public string ReleasedPcsLbs { get; set; }
        public string OnBOLPcsLbs { get; set; }
        public string Plant { get; set; }
        public string DueDate { get; set; }
        public string RollDate { get; set; }
        public int SalesOrder { get; set; }
        public int PoS    { get; set; }
        public string CreditBlock { get; set; }
        public string DeliveryBlock { get; set; }
        public string BolNumbers { get; set; }
				public string ShipmentNumbers { get; set; }
        public string Order_Quantity { get; set; }
        public string CreatedDate { get; set; }
        public string PurchaseOrderDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        

    }
}
