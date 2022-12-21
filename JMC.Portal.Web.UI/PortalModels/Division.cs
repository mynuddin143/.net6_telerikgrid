using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Division
    {
        public Division()
        {
            Employees = new HashSet<Employee>();
            Locations = new HashSet<Location>();
            PendingShipmentCosts = new HashSet<PendingShipmentCost>();
            PriceChangeRequests = new HashSet<PriceChangeRequest>();
            ProductLines = new HashSet<ProductLine>();
            Quotes = new HashSet<Quote>();
            SapbundlingOptions = new HashSet<SapbundlingOption>();
            SapcharacteristicTypes = new HashSet<SapcharacteristicType>();
            SapconditionGroups = new HashSet<SapconditionGroup>();
            Sapconditions = new HashSet<Sapcondition>();
            SapcustomerGroups = new HashSet<SapcustomerGroup>();
            SapcustomerServiceReps = new HashSet<SapcustomerServiceRep>();
            Sapdeliveries = new HashSet<Sapdelivery>();
            Sapmaterials = new HashSet<Sapmaterial>();
            Sapregions = new HashSet<Sapregion>();
            SapsalesGroups = new HashSet<SapsalesGroup>();
            SapsalesOrders = new HashSet<SapsalesOrder>();
            SapsalesOrganizations = new HashSet<SapsalesOrganization>();
            SapshipTos = new HashSet<SapshipTo>();
            Sapshipments = new HashSet<Sapshipment>();
            Saptiers = new HashSet<Saptier>();
            Sapvendors = new HashSet<Sapvendor>();
            ScrapSales = new HashSet<ScrapSale>();
        }

        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<PendingShipmentCost> PendingShipmentCosts { get; set; }
        public virtual ICollection<PriceChangeRequest> PriceChangeRequests { get; set; }
        public virtual ICollection<ProductLine> ProductLines { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<SapbundlingOption> SapbundlingOptions { get; set; }
        public virtual ICollection<SapcharacteristicType> SapcharacteristicTypes { get; set; }
        public virtual ICollection<SapconditionGroup> SapconditionGroups { get; set; }
        public virtual ICollection<Sapcondition> Sapconditions { get; set; }
        public virtual ICollection<SapcustomerGroup> SapcustomerGroups { get; set; }
        public virtual ICollection<SapcustomerServiceRep> SapcustomerServiceReps { get; set; }
        public virtual ICollection<Sapdelivery> Sapdeliveries { get; set; }
        public virtual ICollection<Sapmaterial> Sapmaterials { get; set; }
        public virtual ICollection<Sapregion> Sapregions { get; set; }
        public virtual ICollection<SapsalesGroup> SapsalesGroups { get; set; }
        public virtual ICollection<SapsalesOrder> SapsalesOrders { get; set; }
        public virtual ICollection<SapsalesOrganization> SapsalesOrganizations { get; set; }
        public virtual ICollection<SapshipTo> SapshipTos { get; set; }
        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
        public virtual ICollection<Saptier> Saptiers { get; set; }
        public virtual ICollection<Sapvendor> Sapvendors { get; set; }
        public virtual ICollection<ScrapSale> ScrapSales { get; set; }
    }
}
