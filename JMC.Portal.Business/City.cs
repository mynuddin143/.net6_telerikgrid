using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class City {
		//public static City FindAndInsertIfMissing(ref PortalEntities db, string cityName, string stateAbbreviation, string countryAbbreviation) {
		//  if (cityName.jIsEmpty() || stateAbbreviation.jIsEmpty() || countryAbbreviation.jIsEmpty()) {
		//    return null;
		//  }

		//  State state = State.FindAndInsertIfMissing(ref db, stateAbbreviation,	countryAbbreviation);

		//  City foundCity = (from city in db.City where city.Name.ToLower() == cityName && city.StateID == state.StateID select city).FirstOrDefault();

		//  if (foundCity == null) {
		//    foundCity = new City();
		//    foundCity.Name = cityName;
		//    foundCity.State = state;
		//    foundCity.Active = true;
		//    db.City.Add(foundCity);
		//    db.SaveChanges();
		//  }

		//  return foundCity;
		//}

	}
}
