using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.HSSPortalSales;
//using HSSPortalSales;
using System.IO;

namespace JMC.Portal.Business.DataModel
{

    public class ClaimDeliveryItem : ZstHssDeliveryItem
    {
        public bool Selected { get; set; }

        public DateTime? PODate { get; set; }

        public DateTime? ShipDate { get; set; }

        public string Customer { get; set; }

        public string shippedFrom { get; set; }
				public string ProducingPlant { get; set; }

        public long ShippedFromID { get; set; }
				public long ProducingPlantId { get; set; }

        public string LineNumber { get; set; }

        public string HeatNumber { get; set; }

        public string BatchNumber { get; set; }
    }
}
