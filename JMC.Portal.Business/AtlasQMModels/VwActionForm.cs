using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwActionForm
    {
        [Column("ActionFormID")]
        public int ActionFormId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column("ActionFormTypeID")]
        public int? ActionFormTypeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ActionFormTypeName { get; set; } = null!;
        [Column("PlantID")]
        public int PlantId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PlantName { get; set; } = null!;
        [Column("DepartmentID")]
        public int? DepartmentId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DepartmentName { get; set; } = null!;
        [Column("CustomerID")]
        public int? CustomerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Status { get; set; }
    }
}
