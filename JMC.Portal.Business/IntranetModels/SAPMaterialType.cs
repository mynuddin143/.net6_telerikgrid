using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPMaterialType")]
    public partial class SAPMaterialType
    {
        public SAPMaterialType()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPMaterialTypeID")]
        public int SAPMaterialTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("SAPMaterialType")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
