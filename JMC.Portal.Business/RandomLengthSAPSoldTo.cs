using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class RandomLengthSAPSoldTo {
		public long ID {
			get { return this.RandomLengthSapsoldToID; }
		}

		public string Name {
			get { return this.SapsoldTo.TrimmedNumber + " " + this.SapsoldTo.Name; }
		}
	}
}
