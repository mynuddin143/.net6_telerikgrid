using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("NewsStory")]
    public partial class NewsStory
    {
        [Key]
        [Column("NewsStoryID")]
        public int NewsStoryId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Headline { get; set; } = null!;
        [StringLength(2000)]
        [Unicode(false)]
        public string Body { get; set; } = null!;
    }
}
