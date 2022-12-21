using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public string Title { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string FileType { get; set; } = null!;
    }
}
