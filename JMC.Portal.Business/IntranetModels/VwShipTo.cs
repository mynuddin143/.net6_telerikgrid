using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Keyless]
    public partial class VwShipTo
    {
        [Column("ShipToID")]
        public int ShipToId { get; set; }
        [Column("SAPCustomerID")]
        [StringLength(10)]
        [Unicode(false)]
        public string SapcustomerId { get; set; } = null!;
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CustomerName { get; set; } = null!;
        [Column("SAPShipToID")]
        [StringLength(10)]
        [Unicode(false)]
        public string SAPShipToId { get; set; } = null!;
        [StringLength(150)]
        [Unicode(false)]
        public string ShipToName { get; set; } = null!;
        [StringLength(150)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [Column("CityID")]
        public int CityId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CityName { get; set; } = null!;
        [Column("StateID")]
        public int StateId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string StateName { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string PostalCode { get; set; } = null!;
    }
}
