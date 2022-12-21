using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("Paperwork")]
    public partial class Paperwork
    {
        public Paperwork()
        {
            CustomerServiceComplaintIncorrectPaperworks = new HashSet<CustomerServiceComplaint>();
            CustomerServiceComplaintMissingPaperworks = new HashSet<CustomerServiceComplaint>();
        }

        [Key]
        [Column("PaperworkID")]
        public int PaperworkId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

        [InverseProperty("IncorrectPaperwork")]
        public virtual ICollection<CustomerServiceComplaint> CustomerServiceComplaintIncorrectPaperworks { get; set; }
        [InverseProperty("MissingPaperwork")]
        public virtual ICollection<CustomerServiceComplaint> CustomerServiceComplaintMissingPaperworks { get; set; }
    }
}
