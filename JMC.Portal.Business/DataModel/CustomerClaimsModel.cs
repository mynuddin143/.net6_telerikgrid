using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business.DataModel
{
    public class CustomerClaimsModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? ChargeDate { get; set; }
        public string ActionApprovedBy { get; set; }
        public DateTime? ActionDate { get; set; }
    }
}
