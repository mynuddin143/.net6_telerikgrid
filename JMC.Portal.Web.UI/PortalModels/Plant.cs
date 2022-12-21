using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Plant
    {
        public Plant()
        {
            Mills = new HashSet<Mill>();
            Quotes = new HashSet<Quote>();
            Rollings = new HashSet<Rolling>();
            Sapdeliveries = new HashSet<Sapdelivery>();
            SapmaterialPlants = new HashSet<SapmaterialPlant>();
            SapsalesOrderItems = new HashSet<SapsalesOrderItem>();
            SapsalesOrders = new HashSet<SapsalesOrder>();
            Sapshipments = new HashSet<Sapshipment>();
            SapsoldToPlantExclusions = new HashSet<SapsoldToPlantExclusion>();
            Sapstocks = new HashSet<Sapstock>();
            SapstorageLocations = new HashSet<SapstorageLocation>();
            ScrapSales = new HashSet<ScrapSale>();
            ShoppingCartSaprollings = new HashSet<ShoppingCartSaprolling>();
            Stocks = new HashSet<Stock>();
            WebReleasePlants = new HashSet<WebReleasePlant>();
        }

        public long LocationId { get; set; }
        public string Code { get; set; } = null!;
        public string SalesOrganization { get; set; } = null!;
        public long? HomeMillSapconditionGroupId { get; set; }

        public virtual SapconditionGroup? HomeMillSapconditionGroup { get; set; }
        public virtual Location Location { get; set; } = null!;
        public virtual ICollection<Mill> Mills { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<Rolling> Rollings { get; set; }
        public virtual ICollection<Sapdelivery> Sapdeliveries { get; set; }
        public virtual ICollection<SapmaterialPlant> SapmaterialPlants { get; set; }
        public virtual ICollection<SapsalesOrderItem> SapsalesOrderItems { get; set; }
        public virtual ICollection<SapsalesOrder> SapsalesOrders { get; set; }
        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
        public virtual ICollection<SapsoldToPlantExclusion> SapsoldToPlantExclusions { get; set; }
        public virtual ICollection<Sapstock> Sapstocks { get; set; }
        public virtual ICollection<SapstorageLocation> SapstorageLocations { get; set; }
        public virtual ICollection<ScrapSale> ScrapSales { get; set; }
        public virtual ICollection<ShoppingCartSaprolling> ShoppingCartSaprollings { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<WebReleasePlant> WebReleasePlants { get; set; }
    }
}
