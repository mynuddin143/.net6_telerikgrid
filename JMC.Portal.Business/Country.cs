using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class Country {
		//public static Country FindAndInsertIfMissing(ref PortalEntities db, string abbreviation) {
		//  if (abbreviation.jIsEmpty()) {
		//    return null;
		//  }
			
		//  Country foundCountry = (from country in db.Country where country.Abbreviation == abbreviation select country).FirstOrDefault();

		//  if (foundCountry == null) {
		//    foundCountry = new Country();
		//    foundCountry.Abbreviation = abbreviation;
		//    foundCountry.Name = abbreviation;
		//    foundCountry.Active = true;
		//    db.Country.Add(foundCountry);
		//    db.SaveChanges();
		//  }

		//  return foundCountry;
		//}
	}
}
