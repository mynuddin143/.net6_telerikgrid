using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Data.Kendo {
	public static class Default {
		public static int PageSize {
			get {
				return 100;
			}
		}

		public static int[] PageSizes {
			get {
				return new int[] { 10, 25, 50, 100, 250, 500 };
			}
		}
	}
}