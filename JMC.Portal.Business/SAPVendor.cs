using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using JMC.Portal.Business.AtlasMasterData;
using System.Configuration;
using JMC.Portal.Business.WheatlandPortal;
using System.Net;
using JMC.Portal.Business.AtlasSAPPortal;

namespace JMC.Portal.Business {
	public partial class SAPVendor {
		public int IntegerNumber {
			get { return this.Number.ToInt(); }
		}

		public string TrimmedNumber {
			get { return this.IntegerNumber > 0 ? this.IntegerNumber.ToString() : this.Number; }
		}

		public static void RefreshFromAtlasSAP(string email) {
			PortalEntities db = new PortalEntities();

			int insertedCount = 0;
			int checkedForUpdatesCount = 0;
			int disabledCount = 0;
			int duplicateCount = 0;

			ArrayList SAPDeliveryNumbers = new ArrayList();
			ArrayList duplicateSAPDeliveryNumbers = new ArrayList();

			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			StringBuilder emailStringBuilder = new StringBuilder();
			StringBuilder disabledStringBuilder = new StringBuilder();

			ZWS_MASTER_DATAClient masterDataService = new ZWS_MASTER_DATAClient("ATLAS_ZWS_MASTER_DATA");
			masterDataService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
			masterDataService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

			AtlasMasterData.ZGetAllVendors getAllVendors = new AtlasMasterData.ZGetAllVendors();
			getAllVendors.Vendors = new AtlasMasterData.ZstVendor[] { new AtlasMasterData.ZstVendor() };

			masterDataService.Open();
			AtlasMasterData.ZGetAllVendorsResponse getAllVendorsResponse = masterDataService.ZGetAllVendorsAsync(getAllVendors);
			masterDataService.Close();

			List<City> cities = (from c in db.Cities select c).ToList();
			List<State> states = (from s in db.States select s).ToList();
			List<Country> countries = (from c in db.Countries select c).ToList();
			List<SAPDelivery> vendors = (from v in db.Sapdeliveries where v.DivisionID == (long)Enums.Divisions.Atlas select v).ToList();

			foreach (AtlasMasterData.ZstVendor zstVendor in getAllVendorsResponse.Vendors) {
				if (SAPDeliveryNumbers.Contains(zstVendor.VendorNumber)) {
					duplicateCount++;
					duplicateSAPDeliveryNumbers.Add(zstVendor.VendorNumber);
				} else if (!zstVendor.VendorNumber.Trim().jIsEmpty()) {
					SAPDeliveryNumbers.Add(zstVendor.VendorNumber);

					string countryAbbr = zstVendor.Country.Trim();
					string stateAbbr = zstVendor.State.Trim();
					string cityName = zstVendor.City.Trim();

					if (stateAbbr.jIsEmpty()) {
						stateAbbr = ".";
					}

					if (cityName.jIsEmpty()) {
						cityName = ".";
					}

					SAPDelivery SAPDelivery = (from v in vendors where v.Number == zstVendor.VendorNumber.Trim() select v).FirstOrDefault();

					if (!countryAbbr.jIsEmpty() && !stateAbbr.jIsEmpty() && !cityName.jIsEmpty()) {
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
							if (SAPDelivery.IsNull()) {
								SAPDelivery = new SAPDelivery();
								SAPDelivery.DivisionID = (long)Enums.Divisions.Atlas;
								SAPDelivery.Number = zstVendor.VendorNumber.Trim();
								vendors.Add(SAPDelivery);
								db.Sapdeliveries.Add(SAPDelivery);
								insertedCount++;
							} else {
								checkedForUpdatesCount++;
							}

							SAPDelivery.Name = zstVendor.Name;
							SAPDelivery.Address = zstVendor.Address;
							SAPDelivery.City = city;
							SAPDelivery.PostalCode = zstVendor.PostalCode;
							SAPDelivery.Phone = zstVendor.Phone;
							SAPDelivery.Fax = zstVendor.Fax;
							SAPDelivery.Active = true;
							SAPDelivery.Rail = zstVendor.Rail.ToBool();
							SAPDelivery.Express = false;
							SAPDelivery.Intermodal = false;
						}
					}
				}
			}

			//foreach (SAPDelivery SAPDelivery in SAPDelivery.ActiveList) {
			//  if (!SAPDeliveryNumbers.Contains(SAPDelivery.Number)) {
			//    disabledStringBuilder.Append("<span style=\"font-weight:bold;\">");
			//    disabledStringBuilder.Append(SAPDelivery.Number + " " + SAPDelivery.Name);
			//    disabledStringBuilder.Append("</span> is active on the Intranet, but not found in SAP download, disabling.<br />");

			//    SAPDelivery.Active = false;
			//    SAPDelivery.Save();

			//    disabledCount++;
			//  }
			//}

			db.SaveChanges();

			emailStringBuilder.Append(insertedCount);
			emailStringBuilder.Append(" inserted.<br />");
			emailStringBuilder.Append(checkedForUpdatesCount);
			emailStringBuilder.Append(" checked for updates.<br />");
			emailStringBuilder.Append(disabledCount);
			emailStringBuilder.Append(" disabled.<br />");
			emailStringBuilder.Append(duplicateCount);
			emailStringBuilder.Append(" duplicate numbers.<br />");

			endTime = DateTime.Now;

			TimeSpan runTime = endTime.Subtract(startTime);

			emailStringBuilder.Append("Run Time " + runTime.Days);
			emailStringBuilder.Append("days " + runTime.Hours);
			emailStringBuilder.Append("hours " + runTime.Minutes);
			emailStringBuilder.Append("minutes " + runTime.Seconds);
			emailStringBuilder.Append("seconds<br /><br />");

      ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
			sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
			sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

			AtlasSAPPortal.ZGetFiscalCalendar getTwoYearsAgoFiscalCalendar = new AtlasSAPPortal.ZGetFiscalCalendar();
			getTwoYearsAgoFiscalCalendar.ImFiscalYear = (DateTime.Today.Year - 2).ToString();
			getTwoYearsAgoFiscalCalendar.FiscalPeriods = new AtlasSAPPortal.ZstFiscalPeriod[] { };

			AtlasSAPPortal.ZGetFiscalCalendar getLastYearFiscalCalendar = new AtlasSAPPortal.ZGetFiscalCalendar();
			getLastYearFiscalCalendar.ImFiscalYear = (DateTime.Today.Year - 1).ToString();
			getLastYearFiscalCalendar.FiscalPeriods = new AtlasSAPPortal.ZstFiscalPeriod[] { };

			AtlasSAPPortal.ZGetFiscalCalendar getThisYearFiscalCalendar = new AtlasSAPPortal.ZGetFiscalCalendar();
			getThisYearFiscalCalendar.ImFiscalYear = DateTime.Today.Year.ToString();
			getThisYearFiscalCalendar.FiscalPeriods = new AtlasSAPPortal.ZstFiscalPeriod[] { };

			AtlasSAPPortal.ZGetFiscalCalendar getNextYearFiscalCalendar = new AtlasSAPPortal.ZGetFiscalCalendar();
			getNextYearFiscalCalendar.ImFiscalYear = (DateTime.Today.Year + 1).ToString();
			getNextYearFiscalCalendar.FiscalPeriods = new AtlasSAPPortal.ZstFiscalPeriod[] { };

			sapPortalService.Open();
			AtlasSAPPortal.ZGetFiscalCalendarResponse getTwoYearsAgoFiscalCalendarResponse = sapPortalService.ZGetFiscalCalendarAsync(getTwoYearsAgoFiscalCalendar);
			AtlasSAPPortal.ZGetFiscalCalendarResponse getLastYearFiscalCalendarResponse = sapPortalService.ZGetFiscalCalendarAsync(getLastYearFiscalCalendar);
			AtlasSAPPortal.ZGetFiscalCalendarResponse getThisYearFiscalCalendarResponse = sapPortalService.ZGetFiscalCalendarAsync(getThisYearFiscalCalendar);
			AtlasSAPPortal.ZGetFiscalCalendarResponse getNextYearFiscalCalendarResponse = sapPortalService.ZGetFiscalCalendarAsync(getNextYearFiscalCalendar);
			sapPortalService.Close();

			List<FiscalYear> fiscalYears = (from fy in db.FiscalYears select fy).ToList();
			FiscalYear twoFiscalYearsAgo = (from fy in fiscalYears where fy.TheFiscalYear == DateTime.Today.Year - 2 select fy).FirstOrDefault();

			if (twoFiscalYearsAgo.IsNull()) {
				twoFiscalYearsAgo = new FiscalYear();
				twoFiscalYearsAgo.TheFiscalYear = DateTime.Today.Year - 2;

				fiscalYears.Add(twoFiscalYearsAgo);
				db.FiscalYears.Add(twoFiscalYearsAgo);
			}

			foreach (AtlasSAPPortal.ZstFiscalPeriod zstFiscalPeriod in getTwoYearsAgoFiscalCalendarResponse.FiscalPeriods) {
				int postingPeriod = zstFiscalPeriod.PostingPeriod.ToInt();

				FiscalPeriod fiscalPeriod = (from fp in twoFiscalYearsAgo.FiscalPeriods where fp.PeriodNumber == postingPeriod select fp).FirstOrDefault();

				if (fiscalPeriod.IsNull()) {
					fiscalPeriod = new FiscalPeriod();
					fiscalPeriod.PeriodNumber = zstFiscalPeriod.PostingPeriod.ToInt();
					
					twoFiscalYearsAgo.FiscalPeriods.Add(fiscalPeriod);
				}

				if (zstFiscalPeriod.PostingPeriod == "001") {
					fiscalPeriod.StartDate = DateTime.Today.AddYears(-3);
				} else {
					string previousPostingPeriod = (zstFiscalPeriod.PostingPeriod.ToInt() - 1).ToString();

					FiscalPeriod previousFiscalPeriod = (from fp in twoFiscalYearsAgo.FiscalPeriods where fp.PeriodNumber.ToString() == previousPostingPeriod select fp).FirstOrDefault();

					if (!previousFiscalPeriod.IsNull()) {
						fiscalPeriod.StartDate = previousFiscalPeriod.EndDate.AddDays(1);
					}
				}

				fiscalPeriod.EndDate = zstFiscalPeriod.EndDate.ToDate();
			}

			FiscalYear lastFiscalYear = (from fy in fiscalYears where fy.TheFiscalYear == DateTime.Today.Year - 1 select fy).FirstOrDefault();

			if (lastFiscalYear.IsNull()) {
				lastFiscalYear = new FiscalYear();
				lastFiscalYear.TheFiscalYear = DateTime.Today.Year - 1;

				fiscalYears.Add(lastFiscalYear);
				db.FiscalYears.Add(lastFiscalYear);
			}

			foreach (AtlasSAPPortal.ZstFiscalPeriod zstFiscalPeriod in getLastYearFiscalCalendarResponse.FiscalPeriods) {
				int postingPeriod = zstFiscalPeriod.PostingPeriod.ToInt();

				FiscalPeriod fiscalPeriod = (from fp in lastFiscalYear.FiscalPeriods where fp.PeriodNumber == postingPeriod select fp).FirstOrDefault();

				if (fiscalPeriod.IsNull()) {
					fiscalPeriod = new FiscalPeriod();
					fiscalPeriod.PeriodNumber = zstFiscalPeriod.PostingPeriod.ToInt();

					lastFiscalYear.FiscalPeriods.Add(fiscalPeriod);
				}

				if (zstFiscalPeriod.PostingPeriod == "001") {
					FiscalPeriod previousFiscalPeriod = twoFiscalYearsAgo.FiscalPeriods.OrderByDescending(fp => fp.PeriodNumber).Take(1).FirstOrDefault();

					if (!previousFiscalPeriod.IsNull()) {
						fiscalPeriod.StartDate = previousFiscalPeriod.EndDate.AddDays(1);
					}
				} else {
					string previousPostingPeriod = (zstFiscalPeriod.PostingPeriod.ToInt() - 1).ToString();

					FiscalPeriod previousFiscalPeriod = (from fp in lastFiscalYear.FiscalPeriods where fp.PeriodNumber.ToString() == previousPostingPeriod select fp).FirstOrDefault();

					if (!previousFiscalPeriod.IsNull()) {
						fiscalPeriod.StartDate = previousFiscalPeriod.EndDate.AddDays(1);
					}
				}

				fiscalPeriod.EndDate = zstFiscalPeriod.EndDate.ToDate();
			}

			FiscalYear thisFiscalYear = (from fy in fiscalYears where fy.TheFiscalYear == DateTime.Today.Year select fy).FirstOrDefault();

			if (thisFiscalYear.IsNull()) {
				thisFiscalYear = new FiscalYear();
				thisFiscalYear.TheFiscalYear = DateTime.Today.Year;

				fiscalYears.Add(thisFiscalYear);
				db.FiscalYears.Add(thisFiscalYear);
			}

			foreach (AtlasSAPPortal.ZstFiscalPeriod zstFiscalPeriod in getThisYearFiscalCalendarResponse.FiscalPeriods) {
				int postingPeriod = zstFiscalPeriod.PostingPeriod.ToInt();

				FiscalPeriod fiscalPeriod = (from fp in thisFiscalYear.FiscalPeriods where fp.PeriodNumber == postingPeriod select fp).FirstOrDefault();

				if (fiscalPeriod.IsNull()) {
					fiscalPeriod = new FiscalPeriod();
					fiscalPeriod.PeriodNumber = zstFiscalPeriod.PostingPeriod.ToInt();

					thisFiscalYear.FiscalPeriods.Add(fiscalPeriod);
				}

				if (zstFiscalPeriod.PostingPeriod == "001") {
					FiscalPeriod previousFiscalPeriod = lastFiscalYear.FiscalPeriods.OrderByDescending(fp => fp.PeriodNumber).Take(1).FirstOrDefault();

					if (!previousFiscalPeriod.IsNull()) {
						fiscalPeriod.StartDate = previousFiscalPeriod.EndDate.AddDays(1);
					}
				} else {
					string previousPostingPeriod = (zstFiscalPeriod.PostingPeriod.ToInt() - 1).ToString();

					FiscalPeriod previousFiscalPeriod = (from fp in thisFiscalYear.FiscalPeriods where fp.PeriodNumber.ToString() == previousPostingPeriod select fp).FirstOrDefault();

					if (!previousFiscalPeriod.IsNull()) {
						fiscalPeriod.StartDate = previousFiscalPeriod.EndDate.AddDays(1);
					}
				}

				fiscalPeriod.EndDate = zstFiscalPeriod.EndDate.ToDate();
			}

			FiscalYear nextFiscalYear = (from fy in fiscalYears where fy.TheFiscalYear == DateTime.Today.Year + 1 select fy).FirstOrDefault();

			if (nextFiscalYear.IsNull()) {
				nextFiscalYear = new FiscalYear();
				nextFiscalYear.TheFiscalYear = DateTime.Today.Year + 1;

				fiscalYears.Add(nextFiscalYear);
				db.FiscalYears.Add(nextFiscalYear);
			}

			foreach (AtlasSAPPortal.ZstFiscalPeriod zstFiscalPeriod in getNextYearFiscalCalendarResponse.FiscalPeriods) {
				int postingPeriod = zstFiscalPeriod.PostingPeriod.ToInt();

				FiscalPeriod fiscalPeriod = (from fp in nextFiscalYear.FiscalPeriods where fp.PeriodNumber == postingPeriod select fp).FirstOrDefault();

				if (fiscalPeriod.IsNull()) {
					fiscalPeriod = new FiscalPeriod();
					fiscalPeriod.PeriodNumber = zstFiscalPeriod.PostingPeriod.ToInt();

					nextFiscalYear.FiscalPeriods.Add(fiscalPeriod);
				}

				if (zstFiscalPeriod.PostingPeriod == "001") {
					FiscalPeriod previousFiscalPeriod = thisFiscalYear.FiscalPeriods.OrderByDescending(fp => fp.PeriodNumber).Take(1).FirstOrDefault();

					if (!previousFiscalPeriod.IsNull()) {
						fiscalPeriod.StartDate = previousFiscalPeriod.EndDate.AddDays(1);
					}
				} else {
					string previousPostingPeriod = (zstFiscalPeriod.PostingPeriod.ToInt() - 1).ToString();

					FiscalPeriod previousFiscalPeriod = (from fp in nextFiscalYear.FiscalPeriods where fp.PeriodNumber.ToString() == previousPostingPeriod select fp).FirstOrDefault();

					if (!previousFiscalPeriod.IsNull()) {
						fiscalPeriod.StartDate = previousFiscalPeriod.EndDate.AddDays(1);
					}
				}

				fiscalPeriod.EndDate = zstFiscalPeriod.EndDate.ToDate();
			}

			db.SaveChanges();

			Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Vendor Download Results", emailStringBuilder.ToString());
		}

		public static void RefreshFromWheatlandSAP(string email) {
			PortalEntities db = new PortalEntities();

			int insertedCount = 0;
			int checkedForUpdatesCount = 0;
			int disabledCount = 0;
			int duplicateCount = 0;

			ArrayList SAPDeliveryNumbers = new ArrayList();
			ArrayList duplicateSAPDeliveryNumbers = new ArrayList();

			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			StringBuilder emailStringBuilder = new StringBuilder();
			StringBuilder disabledStringBuilder = new StringBuilder();

			zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
			portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
			portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

			WheatlandPortal.ZGetAllVendors getAllVendors = new WheatlandPortal.ZGetAllVendors();
			getAllVendors.Vendors = new WheatlandPortal.ZstVendor[] { };

			portalService.Open();
			WheatlandPortal.ZGetAllVendorsResponse getAllVendorsResponse = portalService.ZGetAllVendorsAsync(getAllVendors);
			portalService.Close();

			List<City> cities = (from c in db.Cities select c).ToList();
			List<State> states = (from s in db.States select s).ToList();
			List<Country> countries = (from c in db.Countries select c).ToList();
			List<SAPDelivery> vendors = (from v in db.Sapdeliveries where v.DivisionID == (long)Enums.Divisions.Wheatland select v).ToList();

			foreach (WheatlandPortal.ZstVendor zstVendor in getAllVendorsResponse.Vendors) {
				if (SAPDeliveryNumbers.Contains(zstVendor.VendorNumber)) {
					duplicateCount++;
					duplicateSAPDeliveryNumbers.Add(zstVendor.VendorNumber);
				} else if (!zstVendor.VendorNumber.Trim().jIsEmpty()) {
					SAPDeliveryNumbers.Add(zstVendor.VendorNumber);

					string countryAbbr = zstVendor.Country.Trim();
					string stateAbbr = zstVendor.State.Trim();
					string cityName = zstVendor.City.Trim();

					if (stateAbbr.jIsEmpty()) {
						stateAbbr = ".";
					}

					if (cityName.jIsEmpty()) {
						cityName = ".";
					}

					SAPDelivery SAPDelivery = (from v in vendors where v.Number == zstVendor.VendorNumber.Trim() select v).FirstOrDefault();

					if (!countryAbbr.jIsEmpty() && !stateAbbr.jIsEmpty() && !cityName.jIsEmpty()) {
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
							if (SAPDelivery.IsNull()) {
								SAPDelivery = new SAPDelivery();
								SAPDelivery.DivisionID = (long)Enums.Divisions.Wheatland;
								SAPDelivery.Number = zstVendor.VendorNumber.Trim();
								vendors.Add(SAPDelivery);
								db.Sapdeliveries.Add(SAPDelivery);
								insertedCount++;
							} else {
								checkedForUpdatesCount++;
							}

							SAPDelivery.Name = zstVendor.Name;
							SAPDelivery.Address = zstVendor.Address;
							SAPDelivery.City = city;
							SAPDelivery.PostalCode = zstVendor.PostalCode;
							SAPDelivery.Phone = zstVendor.Phone;
							SAPDelivery.Fax = zstVendor.Fax;
							SAPDelivery.Active = true;
							SAPDelivery.Rail = zstVendor.Rail.ToBool();
							SAPDelivery.Express = zstVendor.Express.ToBool();
							SAPDelivery.Intermodal = zstVendor.Intermodal.ToBool();
						}
					}
				}
			}

			//foreach (SAPDelivery SAPDelivery in SAPDelivery.ActiveList) {
			//  if (!SAPDeliveryNumbers.Contains(SAPDelivery.Number)) {
			//    disabledStringBuilder.Append("<span style=\"font-weight:bold;\">");
			//    disabledStringBuilder.Append(SAPDelivery.Number + " " + SAPDelivery.Name);
			//    disabledStringBuilder.Append("</span> is active on the Intranet, but not found in SAP download, disabling.<br />");

			//    SAPDelivery.Active = false;
			//    SAPDelivery.Save();

			//    disabledCount++;
			//  }
			//}

			db.SaveChanges();

			emailStringBuilder.Append(insertedCount);
			emailStringBuilder.Append(" inserted.<br />");
			emailStringBuilder.Append(checkedForUpdatesCount);
			emailStringBuilder.Append(" checked for updates.<br />");
			emailStringBuilder.Append(disabledCount);
			emailStringBuilder.Append(" disabled.<br />");
			emailStringBuilder.Append(duplicateCount);
			emailStringBuilder.Append(" duplicate numbers.<br />");

			endTime = DateTime.Now;

			TimeSpan runTime = endTime.Subtract(startTime);

			emailStringBuilder.Append("Run Time " + runTime.Days);
			emailStringBuilder.Append("days " + runTime.Hours);
			emailStringBuilder.Append("hours " + runTime.Minutes);
			emailStringBuilder.Append("minutes " + runTime.Seconds);
			emailStringBuilder.Append("seconds<br /><br />");

			Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Wheatland Vendor Download Results", emailStringBuilder.ToString());
		}
	}
}
