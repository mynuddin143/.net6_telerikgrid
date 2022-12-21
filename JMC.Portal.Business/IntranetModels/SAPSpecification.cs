using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPSpecification")]
    [Index("Sapcode", Name = "IX_SAPSpecification", IsUnique = true)]
    public partial class SAPSpecification
    {
        public SAPSpecification()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPSpecificationID")]
        public int SAPSpecificationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("SAPSpecification")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
