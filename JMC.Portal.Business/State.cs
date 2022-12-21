using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public partial class State {
		//public static State FindAndInsertIfMissing(ref PortalEntities db, string stateAbbreviation, string countryAbbreviation) {
		//  if (stateAbbreviation.jIsEmpty() || countryAbbreviation.jIsEmpty()) {
		//    return null;
		//  }

		//  Country country = Country.FindAndInsertIfMissing(ref db, countryAbbreviation);

		//  State foundState = (from state in db.State where state.Abbreviation == stateAbbreviation && state.CountryID == country.CountryID select state).FirstOrDefault();

		//  if (foundState == null) {
		//      foundState = new State();
		//      foundState.Abbreviation = stateAbbreviation;
		//      foundState.Country = country;
		//      foundState.Name = stateAbbreviation;
		//      foundState.Active = true;
		//      db.State.Add(foundState);
		//      db.SaveChanges();
		//    }

		//  return foundState;
		//}
	}
}
