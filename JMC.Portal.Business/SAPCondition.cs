using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using AtlasSAPPortal;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Collections;
using System.Configuration;

namespace JMC.Portal.Business
{
    public partial class SAPCondition
    {
        public Plant Plant { get; set; }
        public string FreightIndicator { get; set; }
        public decimal? FSCPercent { get; set; }
        public decimal? FSC { get; set; }
        public decimal? Total { get; set; }

        public decimal Rate { get; set; }
        public string RateUnit { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int PricePer { get; set; }
        public string PricePerUnit { get; set; }
        public bool RegionalFreight { get; set; }

        public string Currency { get; set; }
        public string MaterialGroup { get; set; }

        public bool InfinityValidTo
        {
            get { return this.ValidTo == new DateTime(9999, 12, 31) ? true : false; }
        }

        public bool NullValidTo
        {
            get { return this.ValidTo.IsMin(); }
        }

        public string Description
        {
            get { return (this.RateString.jIsEmpty() && this.PricePerString.jIsEmpty()) ? " " : this.RateString + this.PricePerString; }
        }

        public string DescriptionHTML
        {
            get { return (this.RateStringHTML.jIsEmpty() && this.PricePerStringHTML.jIsEmpty()) ? "&nbsp;" : "<span style=\"white-space:nowrap;\">" + this.RateStringHTML + this.PricePerStringHTML + "</span>"; }
        }

        public string TwoLineDescriptionHTML
        {
            get { return (string.IsNullOrEmpty(this.RateStringHTML) && string.IsNullOrEmpty(this.PricePerStringHTML)) ? "&nbsp;" : this.RateStringHTML + "<br />" + this.PricePerStringHTML; }
        }

        public string RateString
        {
            get { return this.Rate != 0 ? (this.RateUnit.Equals("%") ? (this.Rate.ToDecimal() / 10).ToString() + this.RateUnit : string.Format("{0:c}", this.Rate) + " " + this.RateUnit) : string.Empty; }
        }

        public string RateStringHTML
        {
            get { return this.Rate != 0 ? (this.RateUnit.Equals("%") ? (this.Rate / 10).ToString() + this.RateUnit : "<span style=\"white-space:nowrap;\">" + string.Format("{0:c}", this.Rate) + "&nbsp;<span style=\"font-size:9px;\">" + this.RateUnit + "</span></span>") : string.Empty; }
        }

        public string PricePerString
        {
            get { return (this.PricePer != 0 && !this.RateUnit.Equals("%")) ? " /" + this.PricePer + " " + this.PricePerUnit + (this.Sapcode == "ZF02" ? (this.RegionalFreight ? " RF" : " EQ") : string.Empty) : string.Empty; }
        }

        public string PricePerStringHTML
        {
            get { return (this.PricePer != 0 && !this.RateUnit.Equals("%")) ? " <span style=\"font-size:9px;white-space:nowrap;\">/" + this.PricePer + " " + this.PricePerUnit + (this.Sapcode == "ZF02" ? (this.RegionalFreight ? " <span style=\"font-weight:bold;\">RF</span>" : " <span style=\"font-weight:bold;\">EQ</span>") : string.Empty) + "</span>" : string.Empty; }
        }

        public string ValidityString
        {
            get { return !this.ValidTo.IsMin() ? (this.InfinityValidTo ? this.ValidFrom.ToString("MMM dd, yyyy") : this.ValidFrom.ToString("MMM dd, yyyy") + " - " + this.ValidTo.ToString("MMM dd, yyyy") + "*") : string.Empty; }
        }

        public string ValidityStringHTML
        {
            get { return !this.ValidTo.IsMin() ? (this.InfinityValidTo ? this.ValidFrom.ToString("MMM dd, yyyy") : this.ValidFrom.ToString("MMM dd, yyyy") + " - <span style=\"color:Red;\">" + this.ValidTo.ToString("MMM dd, yyyy") + "</span><span style=\"color:Green;\">*</span>") : string.Empty; }
        }

        public string ApprovalValidityStringHTML
        {
            get { return !this.NullValidTo ? (this.InfinityValidTo ? this.ValidFrom.ToString("MMM dd, yyyy") : this.ValidFrom.ToString("MMM dd, yyyy") + " - " + this.ValidTo.ToString("MMM dd, yyyy")) : string.Empty; }
        }

        public string RateAndValidityStringHTML
        {
            get { return !this.NullValidTo ? "<span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span> " + this.PricePerStringHTML + " <span style=\"font-size:9px;white-space:nowrap;\">" + this.ValidityStringHTML + "</span>" : "none"; }
        }

        public string ApprovalRateAndValidityStringHTML
        {
            get { return !this.NullValidTo ? "<span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span> " + this.PricePerStringHTML + " <span style=\"font-size:9px;white-space:nowrap;\">" + this.ApprovalValidityStringHTML + "</span>" : "<span style=\"font-size:9px;white-space:nowrap;\">none.</span>"; }
        }

        public string EmailApprovalRateAndValidityStringHTML
        {
            get { return !this.NullValidTo ? "<span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span> " + this.PricePerStringHTML + " " + this.ApprovalValidityStringHTML : "none."; }
        }

        public static SAPCondition FindAndInsertIfMissing(ref PortalEntities db, string sapCode)
        {
            if (!sapCode.jIsEmpty())
            {
                SAPCondition SAPCondition = (from c in db.Sapconditions where c.Sapcode == sapCode select c).FirstOrDefault();

                if (SAPCondition.IsNull())
                {
                    SAPCondition = new SAPCondition();
                    SAPCondition.Name = sapCode;
                    SAPCondition.Sapcode = sapCode;
                    SAPCondition.Application = "V";

                    db.Sapconditions.Add(SAPCondition);
                    db.SaveChanges();
                }

                return SAPCondition;
            }

            return new SAPCondition();
        }

        public static List<SAPCondition> GetFreightRates(string incoTerms2, long? plantID)
        {
            PortalEntities db = new PortalEntities();

            List<SAPCondition> returnSAPConditions = new List<SAPCondition>();
            decimal? truckFuelSurcharge = null;
            decimal? railFuelSurcharge = null;
            IQueryable<Plant> plants = Plant.GetAllActive(ref db, (long)Enums.Divisions.Atlas);

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            ZGetPricingConditions getPricingConditions = new ZGetPricingConditions();

            ArrayList pricingConditionArrayList = new ArrayList();

            ZstGetPricingConditions truckFSC = new ZstGetPricingConditions();
            truckFSC.ConditionType = "ZFSC";
            truckFSC.FreightIndicator = "TR";
            truckFSC.FuelSurcharge = "YF";
            truckFSC.EffectiveDate = DateTime.Today;
            pricingConditionArrayList.Add(truckFSC);

            ZstGetPricingConditions railFSC = new ZstGetPricingConditions();
            railFSC.ConditionType = "ZFSC";
            railFSC.FreightIndicator = "RL";
            railFSC.FuelSurcharge = "YF";
            railFSC.EffectiveDate = DateTime.Today;
            pricingConditionArrayList.Add(railFSC);

            if (plantID > 0)
            {
                Plant plant = (from p in plants where p.LocationID == plantID select p).FirstOrDefault();

                if (!plant.IsNull())
                {
                    ZstGetPricingConditions usdTruck = new ZstGetPricingConditions();
                    usdTruck.ConditionType = "ZF00";
                    usdTruck.FreightIndicator = "TR";
                    usdTruck.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    usdTruck.IncoTerms2 = incoTerms2;
                    usdTruck.Currency = "USD";
                    usdTruck.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(usdTruck);

                    ZstGetPricingConditions usdRail = new ZstGetPricingConditions();
                    usdRail.ConditionType = "ZF00";
                    usdRail.FreightIndicator = "RL";
                    usdRail.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    usdRail.IncoTerms2 = incoTerms2;
                    usdRail.Currency = "USD";
                    usdRail.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(usdRail);

                    ZstGetPricingConditions cadTruck = new ZstGetPricingConditions();
                    cadTruck.ConditionType = "ZF00";
                    cadTruck.FreightIndicator = "TR";
                    cadTruck.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    cadTruck.IncoTerms2 = incoTerms2;
                    cadTruck.Currency = "CAD";
                    cadTruck.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(cadTruck);

                    ZstGetPricingConditions cadRail = new ZstGetPricingConditions();
                    cadRail.ConditionType = "ZF00";
                    cadRail.FreightIndicator = "RL";
                    cadRail.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    cadRail.IncoTerms2 = incoTerms2;
                    cadRail.Currency = "CAD";
                    cadRail.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(cadRail);
                }
            }
            else
            {
                foreach (Plant plant in plants)
                {
                    ZstGetPricingConditions usdTruck = new ZstGetPricingConditions();
                    usdTruck.ConditionType = "ZF00";
                    usdTruck.FreightIndicator = "TR";
                    usdTruck.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    usdTruck.IncoTerms2 = incoTerms2;
                    usdTruck.Currency = "USD";
                    usdTruck.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(usdTruck);

                    ZstGetPricingConditions usdRail = new ZstGetPricingConditions();
                    usdRail.ConditionType = "ZF00";
                    usdRail.FreightIndicator = "RL";
                    usdRail.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    usdRail.IncoTerms2 = incoTerms2;
                    usdRail.Currency = "USD";
                    usdRail.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(usdRail);

                    ZstGetPricingConditions cadTruck = new ZstGetPricingConditions();
                    cadTruck.ConditionType = "ZF00";
                    cadTruck.FreightIndicator = "TR";
                    cadTruck.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    cadTruck.IncoTerms2 = incoTerms2;
                    cadTruck.Currency = "CAD";
                    cadTruck.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(cadTruck);

                    ZstGetPricingConditions cadRail = new ZstGetPricingConditions();
                    cadRail.ConditionType = "ZF00";
                    cadRail.FreightIndicator = "RL";
                    cadRail.HomeMill = plant.HomeMillSapconditionGroup.Sapcode;
                    cadRail.IncoTerms2 = incoTerms2;
                    cadRail.Currency = "CAD";
                    cadRail.EffectiveDate = DateTime.Today;
                    pricingConditionArrayList.Add(cadRail);
                }
            }

            getPricingConditions.PricingConditions = (ZstGetPricingConditions[])pricingConditionArrayList.ToArray(typeof(ZstGetPricingConditions));

            sapPortalService.Open();
            AtlasSAPPortal.ZGetPricingConditionsResponse getPricingConditionsResponse = sapPortalService.ZGetPricingConditionsAsync(getPricingConditions);
            sapPortalService.Close();

            List<SAPCondition> SAPConditions = (from c in db.Sapconditions select c).ToList();

            foreach (ZstGetPricingConditions zstGetPricingCondition in getPricingConditionsResponse.PricingConditions)
            {
                Plant plant = (from p in plants where p.HomeMillSapconditionGroup.Sapcode == zstGetPricingCondition.HomeMill.Trim() select p).FirstOrDefault();
                SAPCondition SAPCondition = (from c in SAPConditions where c.Sapcode == zstGetPricingCondition.ConditionType.Trim() select c).FirstOrDefault();

                if (!SAPCondition.IsNull())
                {
                    if (zstGetPricingCondition.Rate > 0)
                    {
                        SAPCondition returnSAPCondition = new SAPCondition();
                        returnSAPCondition.Sapcode = SAPCondition.Sapcode;

                        switch (returnSAPCondition.Sapcode)
                        {
                            case "ZFSC":
                                switch (zstGetPricingCondition.FreightIndicator)
                                {
                                    case "TR":
                                        truckFuelSurcharge = zstGetPricingCondition.Rate / 10;
                                        break;

                                    case "RL":
                                        railFuelSurcharge = zstGetPricingCondition.Rate / 10;
                                        break;
                                }
                                break;

                            default:
                                returnSAPCondition.Plant = plant;
                                returnSAPCondition.FreightIndicator = zstGetPricingCondition.FreightIndicator;
                                returnSAPCondition.Rate = zstGetPricingCondition.Rate;
                                returnSAPCondition.RateUnit = zstGetPricingCondition.RateUnit;
                                returnSAPCondition.ValidFrom = zstGetPricingCondition.ValidFrom.ToDate();
                                returnSAPCondition.ValidTo = zstGetPricingCondition.ValidTo.ToDate();
                                returnSAPCondition.PricePer = zstGetPricingCondition.PricePer.ToInt();
                                returnSAPCondition.PricePerUnit = zstGetPricingCondition.PricePerUnit;

                                switch (zstGetPricingCondition.FreightIndicator)
                                {
                                    case "TR":
                                        returnSAPCondition.FSCPercent = truckFuelSurcharge;
                                        returnSAPCondition.FSC = returnSAPCondition.Rate * (returnSAPCondition.FSCPercent / 100);
                                        returnSAPCondition.Total = returnSAPCondition.Rate + returnSAPCondition.FSC;
                                        break;

                                    case "RL":
                                        returnSAPCondition.FSCPercent = railFuelSurcharge;
                                        returnSAPCondition.FSC = returnSAPCondition.Rate * (returnSAPCondition.FSCPercent / 100);
                                        returnSAPCondition.Total = returnSAPCondition.Rate + returnSAPCondition.FSC;
                                        break;
                                }

                                returnSAPConditions.Add(returnSAPCondition);
                                break;
                        }
                    }
                }
            }

            return returnSAPConditions;
        }

        public static IOrderedEnumerable<SAPCondition> GetPriceChangeHistory(string soldToNumber, DateTime validFrom, DateTime validTo)
        {
            PortalEntities db = new PortalEntities();

            List<SAPCondition> returnSAPConditions = new List<SAPCondition>();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            //sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            //sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            sapPortalService.ClientCredentials.UserName.UserName = "WEBUSER";
            sapPortalService.ClientCredentials.UserName.Password = "APO2018";


            ZGetPriceConditionHistory getPricingConditionsHistory = new ZGetPriceConditionHistory();

            getPricingConditionsHistory.IvSoldTo = soldToNumber;
            getPricingConditionsHistory.IvDateFrom = validFrom;
            getPricingConditionsHistory.IvDateTo = validTo;

            sapPortalService.Open();
            AtlasSAPPortal.ZGetPriceConditionHistoryResponse getPricingConditionsHistoryResponse = sapPortalService.ZGetPriceConditionHistoryAsync(getPricingConditionsHistory);
            sapPortalService.Close();

            foreach (ZstPricingCondition zstPricingCondition in getPricingConditionsHistoryResponse.EtPricingHistory)
            {
                SAPCondition returnSAPCondition = new SAPCondition();

                returnSAPCondition.Rate = zstPricingCondition.Price;
                returnSAPCondition.Currency = zstPricingCondition.Currency;

                switch (zstPricingCondition.MaterialGroup)
                {
                    case "Z1":
                        returnSAPCondition.MaterialGroup = "(Z1) Core Price";
                        break;

                    case "Z2":
                        returnSAPCondition.MaterialGroup = "(Z2) Prop Price";
                        break;
                    case "Z3":
                        returnSAPCondition.MaterialGroup = "(Z3) Jumbo Price";
                        break;
                    case "Z4":
                        returnSAPCondition.MaterialGroup = "(Z4) EpoxZ Price";
                        break;
                    case "Z5":
                        returnSAPCondition.MaterialGroup = "(Z5) LG Pipe Price";
                        break;
                }

                returnSAPCondition.ValidFrom = zstPricingCondition.EffectiveFrom.ToDate();
                returnSAPCondition.ValidTo = zstPricingCondition.EffectiveTo.ToDate();

                returnSAPConditions.Add(returnSAPCondition);
            }

            return returnSAPConditions.OrderByDescending(c => c.ValidFrom);
        }
    }
}
