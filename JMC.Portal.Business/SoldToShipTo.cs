using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using JMC.Portal.Business.IntranetModel;
using System.Data.Linq;

namespace JMC.Portal.Business {
	public static class SoldToShipToTable {
		public static List<SoldToShipTo> GetSoldToShipTos(this PortalEntities db, Enums.Divisions? division = null) {
			string query = "SELECT [dbo].[SAPSoldToSAPShipTo].[SAPSoldToID],[dbo].[SAPSoldToSAPShipTo].[SAPShipToID] FROM [dbo].[SAPSoldToSAPShipTo]";
			if (division.HasValue) {
				query += " INNER JOIN [dbo].[SAPShipTo] ON [dbo].[SAPShipTo].[SAPShipToID] = [dbo].[SAPSoldToSAPShipTo].[SAPSoldToID] WHERE [dbo].[SAPShipTo].[DivisionID] = " + ((long)division.Value);
			}

            var soldToShipTo = db.SapsoldToSapshipTos.FromSqlRaw<SAPSoldToSAPShipTo>(query);
			return (List<SoldToShipTo>)soldToShipTo;

            //return db.ExecuteStoreQuery<SoldToShipTo>(query).ToList();
        }
    }
	public class SoldToShipTo {
		public long SAPSoldToID { get; set; }
		public long SAPShipToID { get; set; }
	}
}
