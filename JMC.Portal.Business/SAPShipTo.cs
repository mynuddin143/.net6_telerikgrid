using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using JMC.Portal.Business.AtlasMasterData;
using JMC.Portal.Business.WheatlandPortal;
using System.Net;

namespace JMC.Portal.Business {
	public partial class SAPShipTo
    {
		public int IntegerNumber {
			get { return this.Number.ToInt(); }
		}

		public string TrimmedNumber {
			get { return this.IntegerNumber > 0 ? this.IntegerNumber.ToString() : string.Empty; }
		}

		public string AlphaNumericIncoTerms2Name {
			get { return Regex.Replace(this.IncoTerms2, "[^A-Za-z0-9]", string.Empty); }
		}

		public string CityAndState {
			get {
				return this.City.IsNull() ? string.Empty : this.City.Name + ", " + this.City.State.Name;
			}
		}

		public string SalesOrganizationString {
			get {
				string[] salesOrganizations = (from stso in this.SapshipToSapsalesOrganizations orderby stso.SapsalesOrganization.Sapcode select stso.SapsalesOrganization.Sapcode).Distinct().ToArray<string>();

				return string.Join(", ", salesOrganizations);
			}
		}

		public bool EqualizedFreight {
			get {
				if (!this.FreightSAPPricingCondition.IsNull()) {
					if (this.FreightSAPPricingCondition.Sapcode == "ZF02") {
						return true;
					}
				}

				return false;
			}
		}

		public string CoreSAPPricingConditionDescription {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.CoreSAPPricingCondition.Description; }
		}

		public string CoreSAPPricingConditionValidityString {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.CoreSAPPricingCondition.ValidityString; }
		}

		public string PropSAPPricingConditionDescription {
			get { return this.PropSAPPricingCondition.IsNull() ? string.Empty : this.PropSAPPricingCondition.Description; }
		}

		public string PropSAPPricingConditionValidityString {
			get { return this.PropSAPPricingCondition.IsNull() ? string.Empty : this.PropSAPPricingCondition.ValidityString; }
		}
		public string JumboSAPPricingConditionDescription {
			get { return this.JumboSAPPricingCondition.IsNull() ? string.Empty : this.JumboSAPPricingCondition.Description; }
		}

		public string JumboSAPPricingConditionValidityString {
			get { return this.JumboSAPPricingCondition.IsNull() ? string.Empty : this.JumboSAPPricingCondition.ValidityString; }
		}
		public string EpoxZSAPPricingConditionDescription {
			get { return this.EpoxZSAPPricingCondition.IsNull() ? string.Empty : this.EpoxZSAPPricingCondition.Description; }
		}
		public string LGPipeSAPPricingConditionDescription {
			get { return this.LGPipeSAPPricingCondition.IsNull() ? string.Empty : this.LGPipeSAPPricingCondition.Description; }
		}
		
		public string EpoxZSAPPricingConditionValidityString {
			get { return this.EpoxZSAPPricingCondition.IsNull() ? string.Empty : this.EpoxZSAPPricingCondition.ValidityString; }
		}
		public string LGPipeSAPPricingConditionValidityString {
			get { return this.LGPipeSAPPricingCondition.IsNull() ? string.Empty : this.LGPipeSAPPricingCondition.ValidityString; }
		}

		public string CoreSAPPricingConditionFullDescription {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.CoreSAPPricingCondition.Description + " " + this.CoreSAPPricingCondition.ValidityString; }
		}



		public string CoreSAPPricingConditionFullDescriptionHTML {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.CoreSAPPricingCondition.DescriptionHTML + "<br /><span style=\"font-size: 9px; white-space: nowrap;\">" + this.CoreSAPPricingCondition.ValidityStringHTML + "</span>"; }
		}

		public string PropSAPPricingConditionFullDescriptionHTML {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.PropSAPPricingCondition.DescriptionHTML + "<br /><span style=\"font-size: 9px; white-space: nowrap;\">" + this.CoreSAPPricingCondition.ValidityStringHTML + "</span>"; }
		}
		public string JumboSAPPricingConditionFullDescriptionHTML {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.JumboSAPPricingCondition.DescriptionHTML + "<br /><span style=\"font-size: 9px; white-space: nowrap;\">" + this.CoreSAPPricingCondition.ValidityStringHTML + "</span>"; }
		}
		public string EpoxZSAPPricingConditionFullDescriptionHTML {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.EpoxZSAPPricingCondition.DescriptionHTML + "<br /><span style=\"font-size: 9px; white-space: nowrap;\">" + this.CoreSAPPricingCondition.ValidityStringHTML + "</span>"; }
		}

		public string LGPipeSAPPricingConditionFullDescriptionHTML {
			get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.LGPipeSAPPricingCondition.DescriptionHTML + "<br /><span style=\"font-size: 9px; white-space: nowrap;\">" + this.CoreSAPPricingCondition.ValidityStringHTML + "</span>"; }
		}

		//public string CoreSAPPricingConditionFullDescriptionEXCEL {
		//  get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.CoreSAPPricingCondition.Description + "\r\n" + this.CoreSAPPricingCondition.ValidityString; }
		//}

		//public string PropSAPPricingConditionFullDescriptionEXCEL {
		//  get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.PropSAPPricingCondition.Description + "\r\n" + this.CoreSAPPricingCondition.ValidityString; }
		//}
		//public string JumboSAPPricingConditionFullDescriptionEXCEL {
		//  get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.JumboSAPPricingCondition.Description + "\r\n" + this.CoreSAPPricingCondition.ValidityString; }
		//}
		//public string EpoxZSAPPricingConditionFullDescriptionEXCEL {
		//  get { return this.CoreSAPPricingCondition.IsNull() ? string.Empty : this.EpoxZSAPPricingCondition.Description + "\r\n" + this.CoreSAPPricingCondition.ValidityString; }
		//}

		public SAPSoldTo ParentSAPSoldTo {
			get { return this.ParentSAPSoldToes.FirstOrDefault() ?? new SAPSoldTo(); }
		}

		public SAPCondition CoreSAPPricingCondition { get; set; }
		public SAPCondition JumboSAPPricingCondition { get; set; }
		public SAPCondition EpoxZSAPPricingCondition { get; set; }
		public SAPCondition LGPipeSAPPricingCondition { get; set; }
		public SAPCondition PropSAPPricingCondition { get; set; }
		public SAPCondition FreightSAPPricingCondition { get; set; }
		public SAPCondition PropFreightSAPPricingCondition { get; set; }
		public SAPCondition FuelSurchargeSAPPricingCondition { get; set; }
        public ICollection<SAPSoldTo> ParentSAPSoldToes { get; set; }   

        public decimal FuelSurcharge { get; set; }
		public decimal PropFuelSurcharge { get; set; }
		public decimal AllInPrice { get; set; }
		public decimal PropAllInPrice { get; set; }
		public decimal? RequestedRate { get; set; }
		public bool RegionalFreight { get; set; }

		public List<SAPCondition> PricingConditions { get; set; }

		public bool CPU {
			get { return this.IncoTerms2.ToLower() == "mill" ? true : false; }
		}

		public string FreightHTML {
			get { return (this.FreightSAPPricingCondition.Rate != 0 && !this.CPU) ? (this.FreightIndicatorSapconditionGroup.IsNull() ? string.Empty : this.FreightIndicatorSapconditionGroup.Sapcode) + "&nbsp;" + this.FreightSAPPricingCondition.TwoLineDescriptionHTML : (this.CPU) ? "CPU" : (this.FreightIndicatorSapconditionGroup.IsNull() ? string.Empty : this.FreightIndicatorSapconditionGroup.Sapcode); }
		}

		public string PropFreightHTML {
			get { return (this.PropFreightSAPPricingCondition.Rate != 0 && !this.CPU) ? this.FreightIndicatorSapconditionGroup.Sapcode + " " + this.PropFreightSAPPricingCondition.DescriptionHTML : (this.CPU) ? "CPU" : this.FreightIndicatorSapconditionGroup.Sapcode; }
		}

		public decimal CorePrice {
			get { return this.CoreSAPPricingCondition.Rate; }
		}

		public decimal JumboPrice {
			get { return this.JumboSAPPricingCondition.Rate; }
		}
		public decimal EpoxZPrice {
			get { return this.EpoxZSAPPricingCondition.Rate; }
		}
		public decimal LGPipePrice {
			get { return this.LGPipeSAPPricingCondition.Rate; }
		}
		
		public string FuelSurchargeHTML {
			get { return (this.FuelSurcharge != 0 && !this.CPU) ? this.FuelSurchargeSAPPricingCondition.DescriptionHTML + "<br />&nbsp;<span style=\"font-size:9px;\">" + string.Format("{0:c}", this.FuelSurcharge) + "</span>" : "&nbsp;"; }
		}

		public string AllInPriceHTML {
			get { return (this.AllInPrice > 0 && !this.CPU) ? string.Format("{0:c}", this.AllInPrice) : "&nbsp;"; }
		}

		public string PropAllInPriceHTML {
			get { return (this.PropAllInPrice > 0 && !this.CPU) ? string.Format("{0:c}", this.PropAllInPrice) : "&nbsp;"; }
		}

		public string RequestedRateHTML {
			get { return this.RequestedRate == null ? string.Empty : string.Format("{0:c}", this.RequestedRate) + "<span class=\"requiredStar\">*</span>"; }
		}

		protected static void RefreshFromAtlasSAP(string email) {
			PortalEntities db = new PortalEntities();

			int insertedCount = 0;
			int checkedForUpdatesCount = 0;
			int conflictedCount = 0;
			int disabledCount = 0;

			ArrayList SAPSoldToNumbers = new ArrayList();

			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			StringBuilder emailStringBuilder = new StringBuilder();
			StringBuilder conflictedStringBuilder = new StringBuilder();
			StringBuilder disabledStringBuilder = new StringBuilder();

			ZWS_MASTER_DATAClient masterDataService = new ZWS_MASTER_DATAClient("ATLAS_ZWS_MASTER_DATA");
			masterDataService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
			masterDataService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

			AtlasMasterData.ZGetAllShipTos getAllShipTos = new AtlasMasterData.ZGetAllShipTos();
			getAllShipTos.ShipTos = new AtlasMasterData.ZstShipTo[] { new AtlasMasterData.ZstShipTo() };

			masterDataService.Open();
			AtlasMasterData.ZGetAllShipTosResponse getAllShipTosResponse = masterDataService.ZGetAllShipTosAsync(getAllShipTos);
			masterDataService.Close();

			List<City> cities = (from c in db.Cities select c).ToList();
			List<State> states = (from s in db.States select s).ToList();
			List<Country> countries = (from c in db.Countries select c).ToList();
			List<SAPShipTo> shipTos = (from s in db.SapshipTos where s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
			List<SAPConditionGroup> conditionGroups = (from cg in db.SapconditionGroups where cg.DivisionID == (long)Enums.Divisions.Atlas select cg).ToList();

			foreach (AtlasMasterData.ZstShipTo zstShipTo in getAllShipTosResponse.ShipTos) {
				string countryAbbr = zstShipTo.Country.Trim();
				string stateAbbr = zstShipTo.State.Trim();
				string cityName = zstShipTo.City.Trim();

				if (!countryAbbr.jIsEmpty() && !stateAbbr.jIsEmpty() && !cityName.jIsEmpty()) {
					SAPSoldToNumbers.Add(zstShipTo.ShipToNumber);

					List<SAPShipTo> conflictingShipTos = new List<SAPShipTo>();
					string alphaNumbericIncoTerms2 = Regex.Replace(zstShipTo.IncoTerms2, "[^A-Za-z0-9]", string.Empty);

					foreach (SAPShipTo SAPShipTo in shipTos) {
						if (zstShipTo.ShipToNumber != SAPShipTo.Number && alphaNumbericIncoTerms2 == SAPShipTo.AlphaNumericIncoTerms2Name && zstShipTo.IncoTerms2 != SAPShipTo.IncoTerms2) {
							conflictingShipTos.Add(SAPShipTo);
						}
					}

					if (conflictingShipTos.Count > 0) {
						conflictedStringBuilder.Append(zstShipTo.ShipToNumber);
						conflictedStringBuilder.Append(" <span style=\"font-weight: bold;\">");
						conflictedStringBuilder.Append(zstShipTo.IncoTerms2);
						conflictedStringBuilder.Append("</span> not added/checked for updates.  The following Ship To's IncoTerms2 were in conflict:<br />");

						foreach (SAPShipTo SAPShipTo in conflictingShipTos) {
							conflictedStringBuilder.Append(SAPShipTo.TrimmedNumber);
							conflictedStringBuilder.Append(" <span style=\"font-weight: bold;\">");
							conflictedStringBuilder.Append(SAPShipTo.IncoTerms2);
							conflictedStringBuilder.Append("</span><br />");
						}

						conflictedStringBuilder.Append("<br />");
						conflictedCount++;
					} else {
						Country country = (from c in countries where c.Abbreviation.ToLower() == countryAbbr.ToLower() select c).FirstOrDefault();

						if (country.IsNull()) {
							country = new Country();
							country.Abbreviation = countryAbbr;
							country.Name = countryAbbr;
							country.Active = true;
							countries.Add(country);
							db.Countries.Add(country);
						}

						State state = (from s in states where s.Abbreviation.ToLower() == stateAbbr.ToLower() && s.Country == country select s).FirstOrDefault();

						if (state.IsNull()) {
							state = new State();
							state.Abbreviation = stateAbbr;
							state.Name = stateAbbr;
							state.Country = country;
							state.Active = true;
							states.Add(state);
							db.States.Add(state);
						}

						City city = (from c in cities where c.Name.ToLower() == cityName.ToLower() && c.State == state select c).FirstOrDefault();

						if (city.IsNull()) {
							city = new City();
							city.Name = cityName;
							city.State = state;
							city.Active = true;
							cities.Add(city);
							db.Cities.Add(city);
						}

						if (!city.IsNull()) {
							SAPShipTo SAPShipTo = (from s in shipTos where s.Number == zstShipTo.ShipToNumber.Trim() select s).FirstOrDefault();
							SAPConditionGroup freightIndicatorSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstShipTo.FreightIndicator select cg).FirstOrDefault();
							SAPConditionGroup fuelSurchargeSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstShipTo.FuelSurcharge select cg).FirstOrDefault();

							if (!zstShipTo.ShipToNumber.jIsEmpty()) {
								if (SAPShipTo.IsNull()) {
									SAPShipTo = new SAPShipTo();
									SAPShipTo.DivisionID = (long)Enums.Divisions.Atlas;
									SAPShipTo.Number = zstShipTo.ShipToNumber.Trim();
									shipTos.Add(SAPShipTo);
									db.SapshipTos.Add(SAPShipTo);
									insertedCount++;
								} else {
									checkedForUpdatesCount++;
								}

								SAPShipTo.Name = zstShipTo.Name;
								SAPShipTo.Address = zstShipTo.Address;
								SAPShipTo.City = city;
								SAPShipTo.PostalCode = zstShipTo.PostalCode;
								SAPShipTo.Phone = zstShipTo.Phone;
								SAPShipTo.Fax = zstShipTo.Fax;
								SAPShipTo.IncoTerms2 = zstShipTo.IncoTerms2;
								SAPShipTo.Currency = zstShipTo.Currency;
								SAPShipTo.FreightIndicatorSapconditionGroup = freightIndicatorSAPConditionGroup;
								SAPShipTo.FuelSurchargeSapconditionGroup = fuelSurchargeSAPConditionGroup;
								//SAPShipTo.Active = true;
								SAPShipTo.Active = zstShipTo.Loevm == "X" ? false : true;
							}
						}
					}
				}
			}

			//foreach (SAPShipTo SAPShipTo in db.SapshipToses) {
			//  if (!SAPSoldToNumbers.Contains(SAPShipTo.Number)) {
			//    SAPSoldTo SAPSoldTo = SAPSoldTo.Find(SAPShipTo.Number);

			//    if (!SAPSoldTo.HasData) {
			//      disabledStringBuilder.Append("<span style=\"font-weight:bold;\">");
			//      disabledStringBuilder.Append(SAPShipTo.TrimmedNumber + " " + SAPShipTo.Name);
			//      disabledStringBuilder.Append("</span> is active on the Intranet, but not found in SAP download, disabling.<br />");

			//      SAPShipTo.Active = false;

			//      disabledCount++;
			//    }
			//  }
			//}

			db.SaveChanges();

			emailStringBuilder.Append(insertedCount);
			emailStringBuilder.Append(" inserted.<br />");
			emailStringBuilder.Append(checkedForUpdatesCount);
			emailStringBuilder.Append(" checked for updates.<br />");
			emailStringBuilder.Append(conflictedCount);
			emailStringBuilder.Append(" not inserted/updated due to IncoTerms2 conflicts.<br />");
			emailStringBuilder.Append(disabledCount);
			emailStringBuilder.Append(" disabled.<br />");

			endTime = DateTime.Now;

			TimeSpan runTime = endTime.Subtract(startTime);

			emailStringBuilder.Append("Run Time " + runTime.Days);
			emailStringBuilder.Append("days " + runTime.Hours);
			emailStringBuilder.Append("hours " + runTime.Minutes);
			emailStringBuilder.Append("minutes " + runTime.Seconds);
			emailStringBuilder.Append("seconds<br /><br />");

			Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Ship To Download Results", emailStringBuilder.ToString());
		}

		protected static void RefreshFromWheatlandSAP(string email) {
			PortalEntities db = new PortalEntities();

			int insertedCount = 0;
			int checkedForUpdatesCount = 0;
			int conflictedCount = 0;
			int disabledCount = 0;
			int shipToSalesOrgJoin = 0;

			ArrayList SAPSoldToNumbers = new ArrayList();

			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			StringBuilder emailStringBuilder = new StringBuilder();
			StringBuilder conflictedStringBuilder = new StringBuilder();
			StringBuilder disabledStringBuilder = new StringBuilder();

			zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
			portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
			portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

			WheatlandPortal.ZGetAllShipTos getAllShipTos = new WheatlandPortal.ZGetAllShipTos();
			getAllShipTos.ShipTos = new WheatlandPortal.ZstShipTo[] { };
			getAllShipTos.SalesOrgs = new WheatlandPortal.ZstSalesOrg[] { };
			getAllShipTos.ShipTosSalesOrgs = new WheatlandPortal.ZstShipToSalesOrg[] { };

			portalService.Open();
			WheatlandPortal.ZGetAllShipTosResponse getAllShipTosResponse = portalService.ZGetAllShipTosAsync(getAllShipTos);
			portalService.Close();

			List<City> cities = (from c in db.Cities select c).ToList();
			List<State> states = (from s in db.States select s).ToList();
			List<Country> countries = (from c in db.Countries select c).ToList();
			List<SAPShipTo> shipTos = (from s in db.SapshipTos where s.DivisionID == (long)Enums.Divisions.Wheatland select s).ToList();
			List<SAPCustomerGroup> customerGroups = (from cg in db.SapcustomerGroups where cg.DivisionID == (long)Enums.Divisions.Wheatland select cg).ToList();
			List<SAPSalesGroup> salesGroups = (from sg in db.SapsalesGroups where sg.DivisionID == (long)Enums.Divisions.Wheatland select sg).ToList();
			List<SAPCustomerServiceRep> customerSalesReps = (from csr in db.SapcustomerServiceReps where csr.DivisionID == (long)Enums.Divisions.Wheatland select csr).ToList();
			List<SAPRegion> regions = (from r in db.Sapregions where r.DivisionID == (long)Enums.Divisions.Wheatland select r).ToList();
			List<SAPTier> tiers = (from t in db.Saptiers where t.DivisionID == (long)Enums.Divisions.Wheatland select t).ToList();
			List<SAPSalesOrganization> salesOrganizations = (from so in db.SapsalesOrganizations where so.DivisionID == (long)Enums.Divisions.Wheatland select so).ToList();
			List<SAPShipToSAPSalesOrganization> SAPShipToSAPSalesOrganizations = (from sso in db.SapshipToSapsalesOrganizations where sso.SapsalesOrganization.DivisionID == (long)Enums.Divisions.Wheatland select sso).ToList();

			foreach (WheatlandPortal.ZstSalesOrg zstSalesOrg in getAllShipTosResponse.SalesOrgs) {
				SAPSalesOrganization salesOrganization = (from so in salesOrganizations where so.Sapcode.ToLower() == zstSalesOrg.SalesOrg.Trim().ToLower() select so).FirstOrDefault();

				if (salesOrganization.IsNull()) {
					salesOrganization = new SAPSalesOrganization();
					salesOrganization.DivisionID = (long)Enums.Divisions.Wheatland;
					salesOrganization.Sapcode = zstSalesOrg.SalesOrg.Trim();
					salesOrganization.Name = zstSalesOrg.SalesOrg.Trim();

					salesOrganizations.Add(salesOrganization);
					db.SapsalesOrganizations.Add(salesOrganization);
				}
			}

			int count = 0;

			foreach (WheatlandPortal.ZstShipTo zstShipTo in getAllShipTosResponse.ShipTos) {
				string countryAbbr = zstShipTo.Country.Trim();
				string stateAbbr = (countryAbbr.ToLower() == "pr" && zstShipTo.State.Trim().jIsEmpty()) ? "PR" : zstShipTo.State.Trim(); ;
				string cityName = zstShipTo.City.Trim();

				if (!countryAbbr.jIsEmpty() && !stateAbbr.jIsEmpty() && !cityName.jIsEmpty()) {
					SAPSoldToNumbers.Add(zstShipTo.ShipToNumber);

					//List<SAPShipTo> conflictingShipTos = new List<SAPShipTo>();
					//string alphaNumbericIncoTerms2 = Regex.Replace(zstShipTo.IncoTerms2, "[^A-Za-z0-9]", string.Empty);

					//foreach (SAPShipTo SAPShipTo in shipTos) {
					//  if (zstShipTo.ShipToNumber != SAPShipTo.Number && alphaNumbericIncoTerms2 == SAPShipTo.AlphaNumericIncoTerms2Name && zstShipTo.IncoTerms2 != SAPShipTo.IncoTerms2) {
					//    conflictingShipTos.Add(SAPShipTo);
					//  }
					//}

					/*if (conflictingShipTos.Count > 0) {
						conflictedStringBuilder.Append(zstShipTo.ShipToNumber);
						conflictedStringBuilder.Append(" <span style=\"font-weight: bold;\">");
						conflictedStringBuilder.Append(zstShipTo.IncoTerms2);
						conflictedStringBuilder.Append("</span> not added/checked for updates.  The following Ship To's IncoTerms2 were in conflict:<br />");

						foreach (SAPShipTo SAPShipTo in conflictingShipTos) {
							conflictedStringBuilder.Append(SAPShipTo.Number);
							conflictedStringBuilder.Append(" <span style=\"font-weight: bold;\">");
							conflictedStringBuilder.Append(SAPShipTo.IncoTerms2);
							conflictedStringBuilder.Append("</span><br />");
						}

						conflictedStringBuilder.Append("<br />");
						conflictedCount++;
					} else {*/
					Country country = (from c in countries where c.Abbreviation.ToLower() == countryAbbr.ToLower() select c).FirstOrDefault();

					if (country.IsNull()) {
						country = new Country();
						country.Abbreviation = countryAbbr;
						country.Name = countryAbbr;
						country.Active = true;
						countries.Add(country);
						db.Countries.Add(country);
					}

					State state = (from s in states where s.Abbreviation.ToLower() == stateAbbr.ToLower() && s.Country == country select s).FirstOrDefault();

					if (state.IsNull()) {
						state = new State();
						state.Abbreviation = stateAbbr;
						state.Name = stateAbbr;
						state.Country = country;
						state.Active = true;
						states.Add(state);
						db.States.Add(state);
					}

					City city = (from c in cities where c.Name.ToLower() == cityName.ToLower() && c.State == state select c).FirstOrDefault();

					if (city.IsNull()) {
						city = new City();
						city.Name = cityName;
						city.State = state;
						city.Active = true;
						cities.Add(city);
						db.Cities.Add(city);
					}

					if (!city.IsNull()) {
						SAPShipTo SAPShipTo = (from s in shipTos where s.Number == zstShipTo.ShipToNumber.Trim() select s).FirstOrDefault();
						//SAPCustomerGroup SAPCustomerGroup = (from cg in customerGroups where cg.Sapcode == zstShipTo.Osr select cg).FirstOrDefault();
						//SAPSalesGroup SAPSalesGroup = (from sg in salesGroups where sg.Sapcode == zstShipTo.Isr select sg).FirstOrDefault();

						if (!zstShipTo.ShipToNumber.jIsEmpty()) {
							if (SAPShipTo.IsNull()) {
								SAPShipTo = new SAPShipTo();
								SAPShipTo.DivisionID = (long)Enums.Divisions.Wheatland;
								SAPShipTo.Number = zstShipTo.ShipToNumber.Trim();
								shipTos.Add(SAPShipTo);
								db.SapshipTos.Add(SAPShipTo);
								insertedCount++;
							} else {
								checkedForUpdatesCount++;
							}

							SAPShipTo.Name = zstShipTo.Name;
							SAPShipTo.Name2 = zstShipTo.Name2;
							SAPShipTo.Address = zstShipTo.Address;
							SAPShipTo.City = city;
							SAPShipTo.PostalCode = zstShipTo.PostalCode;
							SAPShipTo.Phone = zstShipTo.Phone;
							SAPShipTo.Fax = zstShipTo.Fax;
							SAPShipTo.IncoTerms2 = string.Empty;
							SAPShipTo.Currency = string.Empty;
							SAPShipTo.Active = true;
						}
					}
					//}
				}

				count++;

				if (count % 100 == 0) {
					db.SaveChanges();
				}
			}

			db.SaveChanges();

			count = 0;

			foreach (WheatlandPortal.ZstShipToSalesOrg zstShipToSalesOrg in getAllShipTosResponse.ShipTosSalesOrgs) {
				SAPShipTo SAPShipTo = (from s in shipTos where s.Number == zstShipToSalesOrg.ShipToNumber.Trim() select s).FirstOrDefault();
				SAPSalesOrganization SAPSalesOrganization = (from so in salesOrganizations where so.Sapcode == zstShipToSalesOrg.SalesOrg.Trim() select so).FirstOrDefault();

				if (!SAPShipTo.IsNull() && !SAPSalesOrganization.IsNull()) {
					SAPShipToSAPSalesOrganization SAPShipToSAPSalesOrganization = (from sso in SAPShipToSAPSalesOrganizations where sso.SapshipToID == SAPShipTo.SapshipToID && sso.SapsalesOrganizationID == SAPSalesOrganization.SapsalesOrganizationID select sso).FirstOrDefault();

					if (SAPShipToSAPSalesOrganization.IsNull()) {
						SAPShipToSAPSalesOrganization = new SAPShipToSAPSalesOrganization();

						SAPShipToSAPSalesOrganization.SapshipTo = SAPShipTo;
						SAPShipToSAPSalesOrganization.SapsalesOrganization = SAPSalesOrganization;

						SAPShipToSAPSalesOrganizations.Add(SAPShipToSAPSalesOrganization);
						db.SapshipToSapsalesOrganizations.Add(SAPShipToSAPSalesOrganization);
					}

					SAPShipToSAPSalesOrganization.IncoTerms1 = zstShipToSalesOrg.IncoTerms1;
					SAPShipToSAPSalesOrganization.IncoTerms2 = zstShipToSalesOrg.IncoTerms2;
					SAPShipToSAPSalesOrganization.Currency = zstShipToSalesOrg.Currency;
					SAPShipToSAPSalesOrganization.PricingProcedure = zstShipToSalesOrg.PricingProcedure;
					SAPShipToSAPSalesOrganization.SapcustomerGroup = (from cg in customerGroups where cg.Sapcode == zstShipToSalesOrg.Osr select cg).FirstOrDefault();
					SAPShipToSAPSalesOrganization.SapsalesGroup = (from sg in salesGroups where sg.Sapcode == zstShipToSalesOrg.Isr select sg).FirstOrDefault();
					SAPShipToSAPSalesOrganization.SapcustomerServiceRep = (from csr in customerSalesReps where csr.Sapcode == zstShipToSalesOrg.Csr select csr).FirstOrDefault();
					SAPShipToSAPSalesOrganization.Sapregion = (from r in regions where r.Sapcode == zstShipToSalesOrg.Region select r).FirstOrDefault();
					SAPShipToSAPSalesOrganization.Saptier = (from t in tiers where t.Sapcode == zstShipToSalesOrg.Tier select t).FirstOrDefault();

					shipToSalesOrgJoin++;
				}

				count++;

				if (count % 100 == 0) {
					db.SaveChanges();
				}
			}

			db.SaveChanges();

			//foreach (SAPShipTo SAPShipTo in db.SapshipToses) {
			//  if (!SAPSoldToNumbers.Contains(SAPShipTo.Number)) {
			//    SAPSoldTo SAPSoldTo = SAPSoldTo.Find(SAPShipTo.Number);

			//    if (!SAPSoldTo.HasData) {
			//      disabledStringBuilder.Append("<span style=\"font-weight:bold;\">");
			//      disabledStringBuilder.Append(SAPShipTo.TrimmedNumber + " " + SAPShipTo.Name);
			//      disabledStringBuilder.Append("</span> is active on the Intranet, but not found in SAP download, disabling.<br />");

			//      SAPShipTo.Active = false;

			//      disabledCount++;
			//    }
			//  }
			//}

			//db.SaveChanges();

			emailStringBuilder.Append(insertedCount);
			emailStringBuilder.Append(" inserted.<br />");
			emailStringBuilder.Append(checkedForUpdatesCount);
			emailStringBuilder.Append(" checked for updates.<br />");
			emailStringBuilder.Append(conflictedCount);
			emailStringBuilder.Append(" not inserted/updated due to IncoTerms2 conflicts.<br />");
			emailStringBuilder.Append(disabledCount);
			emailStringBuilder.Append(" disabled.<br />");
			emailStringBuilder.Append(shipToSalesOrgJoin);
			emailStringBuilder.Append(" joins between Ship Tos and Sales Organizations.<br />");

			endTime = DateTime.Now;

			TimeSpan runTime = endTime.Subtract(startTime);

			emailStringBuilder.Append("Run Time " + runTime.Days);
			emailStringBuilder.Append("days " + runTime.Hours);
			emailStringBuilder.Append("hours " + runTime.Minutes);
			emailStringBuilder.Append("minutes " + runTime.Seconds);
			emailStringBuilder.Append("seconds<br /><br />");

			Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Wheatland Ship To Download Results", emailStringBuilder.ToString());
		}
	}
}
