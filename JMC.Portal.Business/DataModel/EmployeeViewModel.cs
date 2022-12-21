using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business.DataModel {
	public class EmployeeViewModel {

		public long EmployeeID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Domain { get; set; }
		public string Phone { get; set; }
		public string Division { get; set; }
		public string Department { get; set; }
		public string EmployeePosition { get; set; }
		public string Fax { get; set; }
	}

	public class UserViewModel {
		public long UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Division { get; set; }
		public string Department { get; set; }
		public string EmployeePosition { get; set; }
		public string Fax { get; set; }
		public string Primary { get; set; }
		public string UserName { get; set; }
	}

	public class PortalUsageViewModel {
		public string SoldToName { get; set; }
		public string SoldToNumber { get; set; }
		public long SAPSoldToID { get; set; }
		public decimal TonsReleased { get; set; }
		public decimal TonsOrdered { get; set; }
		public string UserName { get; set; }
		public long UserId { get; set; }
	}

    public class PortalUsageDetailViewModel
    {
        public long Number { get; set; }
        public long Position { get; set; }
        public string SoldToNumber { get; set; }
        public DateTime Date { get; set; }
        public string PONumber { get; set; }
        public string ShipTo { get; set; }
        public string MaterialDescription { get; set; }
        public string OrderedQty { get; set; }
        public string PieceWeight { get; set; }
        public string RecordSource { get; set; }
        public long? GrossWeight { get; set; }

    }
    public class PortalUsageChartViewModel
    {
        public string category { get; set; }
        public string value { get; set; }
        public string color { get; set; }
    }
}
