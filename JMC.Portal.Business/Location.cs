using JMC.Portal.Business.PortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMC.Portal.Business
{
	public partial class Location
	{
		public static IQueryable<Business.PortalModels.Location> GetAllActive(ref PortalContext db)
		{
			return GetAllActive(ref db, null);
		}

		public static IQueryable<JMC.Portal.Business.PortalModels.Location> GetAllActive(ref PortalContext db, long? divisionID)
		{
			if (divisionID > 0)
			{
				return db.Locations.Where(d => d.Active == true && d.DivisionId == divisionID);
			}

			return db.Locations.Where(d => d.Active == true);
		}

		public static Dictionary<string, string> GetAllActiveDictionary(ref PortalContext db, bool addBlankOption)
		{
			return GetAllActiveDictionary(ref db, addBlankOption, null);
		}

		public static Dictionary<string, string> GetAllActiveDictionary(ref PortalContext db, bool addBlankOption, long? divisionID)
		{
			Dictionary<string, string> dictionary = Location.GetAllActive(ref db, divisionID).ToDictionary(d => d.LocationId.ToString(), d => d.Name);

			if (addBlankOption)
			{
				dictionary.Add(string.Empty, string.Empty);
			}

			return dictionary;
		}
	}
}
