using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapshipTo
    {
        public SapshipTo()
        {
            DealsBySoldToShipTos = new HashSet<DealsBySoldToShipTo>();
            FreightandFscs = new HashSet<FreightandFsc>();
            PriceChangeRequestItems = new HashSet<PriceChangeRequestItem>();
            Quotes = new HashSet<Quote>();
            SapcustomerTexts = new HashSet<SapcustomerText>();
            SapsalesDeliveries = new HashSet<SapsalesDelivery>();
            SapsalesOrderItems = new HashSet<SapsalesOrderItem>();
            SapsalesOrders = new HashSet<SapsalesOrder>();
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
            SapsoldToDefaultSapshipTos = new HashSet<SapsoldTo>();
            Zr00s = new HashSet<Zr00>();
            Zr01s = new HashSet<Zr01>();
            Users = new HashSet<User>();
        }

        public long SapshipToId { get; set; }
        public long DivisionId { get; set; }
        public string Number { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public long CityId { get; set; }
        public string PostalCode { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Fax { get; set; } = null!;
        public string IncoTerms2 { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public long? FreightIndicatorSapconditionGroupId { get; set; }
        public long? FuelSurchargeSapconditionGroupId { get; set; }
        public bool Active { get; set; }
        public long? SapcustomerGroupId { get; set; }
        public long? SapsalesGroupId { get; set; }
        public string? Name2 { get; set; }
        public int? SortOrder { get; set; }
        public bool? AllowPrice { get; set; }
        public bool? Out { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Division Division { get; set; } = null!;
        public virtual SapconditionGroup? FreightIndicatorSapconditionGroup { get; set; }
        public virtual SapconditionGroup? FuelSurchargeSapconditionGroup { get; set; }
        public virtual SapcustomerGroup? SapcustomerGroup { get; set; }
        public virtual SapsalesGroup? SapsalesGroup { get; set; }
        public virtual SapsoldTo SapsoldToSapshipTo { get; set; } = null!;
        public virtual ICollection<DealsBySoldToShipTo> DealsBySoldToShipTos { get; set; }
        public virtual ICollection<FreightandFsc> FreightandFscs { get; set; }
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItems { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual ICollection<SapcustomerText> SapcustomerTexts { get; set; }
        public virtual ICollection<SapsalesDelivery> SapsalesDeliveries { get; set; }
        public virtual ICollection<SapsalesOrderItem> SapsalesOrderItems { get; set; }
        public virtual ICollection<SapsalesOrder> SapsalesOrders { get; set; }
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
        public virtual ICollection<SapsoldTo> SapsoldToDefaultSapshipTos { get; set; }
        public virtual ICollection<Zr00> Zr00s { get; set; }
        public virtual ICollection<Zr01> Zr01s { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
