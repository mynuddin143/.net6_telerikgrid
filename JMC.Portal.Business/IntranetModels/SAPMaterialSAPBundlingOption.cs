using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPMaterialSAPBundlingOption")]
    public partial class SAPMaterialSAPBundlingOption
    {
        [Key]
        [Column("SAPMaterialSAPBundlingOptionID")]
        public int SAPMaterialSAPBundlingOptionID { get; set; }
        [Column("SAPMaterialID")]
        public int SAPMaterialID { get; set; }
        [Column("SAPBundlingOptionID")]
        public int SAPBundlingOptionID { get; set; }
    }
}
