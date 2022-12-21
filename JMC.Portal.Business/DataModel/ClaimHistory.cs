using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business.DataModel
{
    public class ClaimHistory : CustomerComplaint
    {
        public string ClaimNumber { get; set; }
        public string BatchNumber { get; set; }
        public string Description { get; set; }
        public string HeatNumber { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
