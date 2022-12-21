using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;

namespace JMC.Portal.Business
{
    public partial class PriceChangeRequestItem
    {
        public enum ApprovalCheckValues
        {
            CanNotApprove = 0,
            CanApprove = 1,
            CouldApproveButExceedsLimit = 2,
            PriceChangeAdmin = 3
        }

        public enum EffectiveDateStatusValues
        {
            OK = 0,
            NotTodayOrEarlier = 1,
            TooSoonInfinityValidTo = 2,
            TooSoonFutureCondition = 3
        }

        public SAPCondition OldSapcondition { get; set; }

        public string ApprovalEffectiveDateHTML
        {
            get { return this.EffectiveDateStatus > 0 ? "<span style=\"color:Red;\">" + this.EffectiveDate.ToString("MMM dd, yyyy") + "</span>" : this.EffectiveDate.ToString("MMM dd, yyyy"); }
        }

        public string EffectiveDateStatusStringHTML
        {
            get
            {
                switch (this.EffectiveDateStatus)
                {
                    case (int)PriceChangeRequestItem.EffectiveDateStatusValues.NotTodayOrEarlier:
                        return "<span style=\"font-size:9px;white-space:nowrap;color:red;\">No current pricing condition found.  Effective date must be today or earlier.*</span>";

                    case (int)PriceChangeRequestItem.EffectiveDateStatusValues.TooSoonInfinityValidTo:
                        return "<span style=\"font-size:9px;white-space:nowrap;color:red;\">Effective date must be " + this.OldSapcondition.ValidFrom.AddDays(1).ToString("MMM dd, yyyy") + " or later.*</span>";

                    case (int)PriceChangeRequestItem.EffectiveDateStatusValues.TooSoonFutureCondition:
                        return "<span style=\"font-size:9px;white-space:nowrap;color:red;\">Future price change exists.  Effective date must be " + this.OldSapcondition.ValidTo.AddDays(2).ToString("MMM dd, yyyy") + " or later.*</span>";
                }

                return string.Empty;
            }
        }

        public string EmailEffectiveDateStatusStringHTML
        {
            get
            {
                switch (this.EffectiveDateStatus)
                {
                    case (int)PriceChangeRequestItem.EffectiveDateStatusValues.NotTodayOrEarlier:
                        return "<span style=\"color:red;\">No current pricing condition found.  Effective date must be today or earlier.*</span>";

                    case (int)PriceChangeRequestItem.EffectiveDateStatusValues.TooSoonInfinityValidTo:
                        return "<span style=\"color:red;\">Effective date must be " + this.OldSapcondition.ValidFrom.AddDays(1).ToString("MMM dd, yyyy") + " or later.*</span>";

                    case (int)PriceChangeRequestItem.EffectiveDateStatusValues.TooSoonFutureCondition:
                        return "<span style=\"color:red;\">Future price change exists.  Effective date must be " + this.OldSapcondition.ValidTo.AddDays(2).ToString("MMM dd, yyyy") + " or later.*</span>";
                }

                return string.Empty;
            }
        }

        public string ShortDescriptionHTML
        {
            get
            {
                if (this.SapmaterialPricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>";
                }
                else if (this.SappricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>";
                }
                else if (this.SapmaterialGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR04")
                {
                    return "(" + this.Sapcondition.Sapcode + ") Material Group " + this.SapmaterialGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>";
                }
                else if (this.SapshipToID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapshipTo.TrimmedNumber + " " + this.SapshipTo.Name + " (" + this.SapshipTo.IncoTerms2 + ") adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>";
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SapmaterialPricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>";
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SappricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>";
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && !string.IsNullOrEmpty(this.FreightIndicator) && !string.IsNullOrEmpty(this.IncoTerms2) && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.FreightIndicator + " " + this.IncoTerms2 + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>";
                }

                return string.Empty;
            }
        }

        public string ShortDescriptionExcel
        {
            get
            {
                if (this.SapmaterialPricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to " + string.Format("{0:c}", this.Rate);
                }
                else if (this.SappricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to " + string.Format("{0:c}", this.Rate);
                }
                else if (this.SapmaterialGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR04")
                {
                    return "(" + this.Sapcondition.Sapcode + ") Material Group " + this.SapmaterialGroup.Name + " adder change to " + string.Format("{0:c}", this.Rate);
                }
                else if (this.SapshipToID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapshipTo.TrimmedNumber + " " + this.SapshipTo.Name + " (" + this.SapshipTo.IncoTerms2 + ") adder change to " + string.Format("{0:c}", this.Rate);
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SapmaterialPricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to" + string.Format("{0:c}", this.Rate);
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SappricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to " + string.Format("{0:c}", this.Rate);
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && !string.IsNullOrEmpty(this.FreightIndicator) && !string.IsNullOrEmpty(this.IncoTerms2) && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.FreightIndicator + " " + this.IncoTerms2 + " adder change to " + string.Format("{0:c}", this.Rate);
                }

                return string.Empty;
            }
        }

        public string DescriptionHTML
        {
            get
            {
                if (this.SapmaterialPricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br /><span style=\"font-size:9px;white-space:nowrap;\">Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ":</span> " + this.OldSapcondition.ApprovalRateAndValidityStringHTML + "<br />" + this.EffectiveDateStatusStringHTML;
                }
                else if (this.SappricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br /><span style=\"font-size:9px;white-space:nowrap;\">Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ":</span> " + this.OldSapcondition.ApprovalRateAndValidityStringHTML + "<br />" + this.EffectiveDateStatusStringHTML;
                }
                else if (this.SapmaterialGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR04")
                {
                    return "(" + this.Sapcondition.Sapcode + ") Material Group " + this.SapmaterialGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br /><span style=\"font-size:9px;white-space:nowrap;\">Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ":</span> " + this.OldSapcondition.ApprovalRateAndValidityStringHTML + "<br />" + this.EffectiveDateStatusStringHTML;
                }
                else if (this.SapshipToID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapshipTo.TrimmedNumber + " " + this.SapshipTo.Name + " (" + this.SapshipTo.IncoTerms2 + ") adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br /><span style=\"font-size:9px;white-space:nowrap;\">Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ":</span> " + this.OldSapcondition.ApprovalRateAndValidityStringHTML + "<br />" + this.EffectiveDateStatusStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SapmaterialPricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br /><span style=\"font-size:9px;white-space:nowrap;\">Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ":</span> " + this.OldSapcondition.ApprovalRateAndValidityStringHTML + "<br />" + this.EffectiveDateStatusStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SappricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br /><span style=\"font-size:9px;white-space:nowrap;\">Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ":</span> " + this.OldSapcondition.ApprovalRateAndValidityStringHTML + "<br />" + this.EffectiveDateStatusStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && !string.IsNullOrEmpty(this.FreightIndicator) && !string.IsNullOrEmpty(this.IncoTerms2) && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.FreightIndicator + " " + this.IncoTerms2 + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br /><span style=\"font-size:9px;white-space:nowrap;\">Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ":</span> " + this.OldSapcondition.ApprovalRateAndValidityStringHTML + "<br />" + this.EffectiveDateStatusStringHTML;
                }

                return string.Empty;
            }
        }

        public string EmailDescriptionHTML
        {
            get
            {
                if (this.SapmaterialPricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML + "<br />" + this.EmailEffectiveDateStatusStringHTML;
                }
                else if (this.SappricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML + "<br />" + this.EmailEffectiveDateStatusStringHTML;
                }
                else if (this.SapmaterialGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR04")
                {
                    return "(" + this.Sapcondition.Sapcode + ") Material Group " + this.SapmaterialGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML + "<br />" + this.EmailEffectiveDateStatusStringHTML;
                }
                else if (this.SapshipToID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapshipTo.TrimmedNumber + " " + this.SapshipTo.Name + " (" + this.SapshipTo.IncoTerms2 + ") adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML + "<br />" + this.EmailEffectiveDateStatusStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SapmaterialPricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML + "<br />" + this.EmailEffectiveDateStatusStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SappricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML + "<br />" + this.EmailEffectiveDateStatusStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && !string.IsNullOrEmpty(this.FreightIndicator) && !string.IsNullOrEmpty(this.IncoTerms2) && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.FreightIndicator + " " + this.IncoTerms2 + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML + "<br />" + this.EmailEffectiveDateStatusStringHTML;
                }

                return string.Empty;
            }
        }

        public string EmailResponseDescriptionHTML
        {
            get
            {
                if (this.SapmaterialPricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML;
                }
                else if (this.SappricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML;
                }
                else if (this.SapmaterialGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR04")
                {
                    return "(" + this.Sapcondition.Sapcode + ") Material Group " + this.SapmaterialGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML;
                }
                else if (this.SapshipToID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapshipTo.TrimmedNumber + " " + this.SapshipTo.Name + " (" + this.SapshipTo.IncoTerms2 + ") adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SapmaterialPricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SapmaterialPricingGroup.Name + " change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SappricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.SappricingGroup.Name + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML;
                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && !string.IsNullOrEmpty(this.FreightIndicator) && !string.IsNullOrEmpty(this.IncoTerms2) && this.Sapcondition.Sapcode == "ZF02")
                {
                    return "(" + this.Sapcondition.Sapcode + ") " + this.FreightIndicator + " " + this.IncoTerms2 + " adder change to <span style=\"font-weight:bold;\">" + string.Format("{0:c}", this.Rate) + "</span>" + "<br />Price on " + this.EffectiveDate.ToString("MMM dd, yyyy") + ": " + this.OldSapcondition.EmailApprovalRateAndValidityStringHTML;
                }

                return string.Empty;
            }
        }

        public int EffectiveDateStatus
        {
            get
            {
                if (this.SapmaterialPricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {

                }
                else if (this.SappricingGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {

                }
                else if (this.SapmaterialGroupID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZR04")
                {

                }
                else if (this.SapshipToID > 0 && this.PriceChangeRequest.SapsoldToID > 0 && this.Sapcondition.Sapcode == "ZF02")
                {

                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SapmaterialPricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR00")
                {

                }
                else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && this.SappricingGroupID > 0 && this.Sapcondition.Sapcode == "ZR01")
                {

                }

                if (this.OldSapcondition.NullValidTo && this.EffectiveDate > DateTime.Today)
                {
                    return (int)PriceChangeRequestItem.EffectiveDateStatusValues.NotTodayOrEarlier;
                }

                if (this.OldSapcondition.InfinityValidTo && this.EffectiveDate < this.OldSapcondition.ValidFrom.AddDays(1))
                {
                    return (int)PriceChangeRequestItem.EffectiveDateStatusValues.TooSoonInfinityValidTo;
                }

                if (!this.OldSapcondition.NullValidTo && !this.OldSapcondition.InfinityValidTo && this.EffectiveDate < this.OldSapcondition.ValidTo.AddDays(2))
                {
                    return (int)PriceChangeRequestItem.EffectiveDateStatusValues.TooSoonFutureCondition;
                }

                return (int)PriceChangeRequestItem.EffectiveDateStatusValues.OK;
            }
        }

        public decimal Difference
        {
            get { return this.Rate.ToDecimal() - this.OldSapcondition.Rate; }
        }

        public string ApprovedStringExcel
        {
            get { return this.Approved.ToBool() ? "Approved by " + this.ApprovedUser.FullName + " on " + string.Format("{0:MMM dd, yyyy h:mmtt}", this.ApprovedDate) : this.Approved != null ? "Denied by " + this.ApprovedUser.FullName + " on " + string.Format("{0:MMM dd, yyyy h:mmtt}", this.ApprovedDate) : "Pending"; }
        }

        public string ApprovedStringHTML
        {
            get { return this.Approved.ToBool() ? "<span style=\"color:Green;\">Approved</span> by " + this.ApprovedUser.FullName + " on " + string.Format("{0:MMM dd, yyyy h:mmtt}", this.ApprovedDate) : this.Approved != null ? "<span style=\"color:Red;\">Denied</span> by " + this.ApprovedUser.FullName + " on " + string.Format("{0:MMM dd, yyyy h:mmtt}", this.ApprovedDate) : "Pending"; }
        }

        public string GetApprovedRadioHTML(Employee employee)
        {
            int approvalCheck = this.PriceChangeRequest.UserCanApprove(employee, this.Difference);
            decimal approvalThreshold = ConfigurationManager.AppSettings["PriceChangeApprovalThreshold"].ToInt();

            switch (approvalCheck)
            {
                case (int)PriceChangeRequestItem.ApprovalCheckValues.PriceChangeAdmin:
                    if (this.EffectiveDateStatus == 0)
                    {
                        if (Math.Abs(this.Difference) > approvalThreshold)
                        {
                            return "<input name=\"ApprovedDenied" + this.PriceChangeRequestItemID + "\" value=\"1\" type=\"radio\" class=\"radioButton\" title=\"Approved\" /><br /><span style=\"\">> " + string.Format("{0:c}", approvalThreshold) + "</span>";
                        }
                        else
                        {
                            return "<input name=\"ApprovedDenied" + this.PriceChangeRequestItemID + "\" value=\"1\" type=\"radio\" class=\"radioButton\" title=\"Approved\" />";
                        }
                    }
                    break;

                case (int)PriceChangeRequestItem.ApprovalCheckValues.CanApprove:
                    if (this.EffectiveDateStatus == 0)
                    {
                        return "<input name=\"ApprovedDenied" + this.PriceChangeRequestItemID + "\" value=\"1\" type=\"radio\" class=\"radioButton\" title=\"Approved\" />";
                    }
                    break;

                case (int)PriceChangeRequestItem.ApprovalCheckValues.CouldApproveButExceedsLimit:
                    return "> " + string.Format("{0:c}", approvalThreshold);

                case (int)PriceChangeRequestItem.ApprovalCheckValues.CanNotApprove:
                    break;
            }

            return string.Empty;
        }

        public string GetDeniedRadioHTML(Employee employee)
        {
            int approvalCheck = this.PriceChangeRequest.UserCanApprove(employee, this.Difference);

            switch (approvalCheck)
            {
                case (int)PriceChangeRequestItem.ApprovalCheckValues.PriceChangeAdmin:
                    return "<input name=\"ApprovedDenied" + this.PriceChangeRequestItemID + "\" value=\"0\" type=\"radio\" class=\"radioButton\" title=\"Denied\" />";

                case (int)PriceChangeRequestItem.ApprovalCheckValues.CanApprove:
                    return "<input name=\"ApprovedDenied" + this.PriceChangeRequestItemID + "\" value=\"0\" type=\"radio\" class=\"radioButton\" title=\"Denied\" />";

                case (int)PriceChangeRequestItem.ApprovalCheckValues.CouldApproveButExceedsLimit:
                    break;

                case (int)PriceChangeRequestItem.ApprovalCheckValues.CanNotApprove:
                    break;
            }

            return string.Empty;
        }

        public void SaveToSAP()
        {
            if (this.PriceChangeRequest.SapsoldToID > 0)
            {
                ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
                sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

                ZInsertPricingCondition insertPricingCondition = new ZInsertPricingCondition();
                insertPricingCondition.ImConditionType = this.Sapcondition.Sapcode;
                insertPricingCondition.ImPrice = this.Rate.ToDecimal();
                insertPricingCondition.ImValidFrom = this.EffectiveDate;
                insertPricingCondition.ImSoldToNumber = this.PriceChangeRequest.SapsoldTo.Number;
                insertPricingCondition.ImShipToNumber = !this.SapshipTo.IsNull() ? this.SapshipTo.Number : string.Empty;
                insertPricingCondition.ImMaterialPricingGroup = !this.SapmaterialPricingGroup.IsNull() ? this.SapmaterialPricingGroup.Sapcode : string.Empty;
                insertPricingCondition.ImPricingGroup = !this.SappricingGroup.IsNull() ? this.SappricingGroup.Sapcode : string.Empty;
                insertPricingCondition.ImMaterialGroup = !this.SapmaterialGroup.IsNull() ? this.SapmaterialGroup.Sapcode : string.Empty;

                sapPortalService.Open();
                ZInsertPricingConditionResponse insertPricingConditionResponse = sapPortalService.ZInsertPricingConditionAsync(insertPricingCondition);
                sapPortalService.Close();
            }
            else if (this.PriceChangeRequest.RegionSapconditionGroupID > 0 && this.PriceChangeRequest.TierSapconditionGroupID > 0 && !this.PriceChangeRequest.Currency.jIsEmpty())
            {
                ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
                sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

                ZInsertPricingCondition insertPricingCondition = new ZInsertPricingCondition();
                insertPricingCondition.ImConditionType = this.Sapcondition.Sapcode;
                insertPricingCondition.ImPrice = this.Rate.ToDecimal();
                insertPricingCondition.ImValidFrom = this.EffectiveDate;
                insertPricingCondition.ImMaterialPricingGroup = !this.SapmaterialPricingGroup.IsNull() ? this.SapmaterialPricingGroup.Sapcode : string.Empty;
                insertPricingCondition.ImPricingGroup = !this.SappricingGroup.IsNull() ? this.SappricingGroup.Sapcode : string.Empty;
                insertPricingCondition.ImRegion = this.PriceChangeRequest.RegionSapconditionGroup.Sapcode;
                insertPricingCondition.ImTier = this.PriceChangeRequest.TierSapconditionGroup.Sapcode;
                insertPricingCondition.ImCurrency = this.PriceChangeRequest.Currency;
                insertPricingCondition.ImFreightIndicator = this.FreightIndicator;
                insertPricingCondition.ImIncoTerms2 = this.IncoTerms2;

                sapPortalService.Open();
                ZInsertPricingConditionResponse insertPricingConditionResponse = sapPortalService.ZInsertPricingConditionAsync(insertPricingCondition);
                sapPortalService.Close();
            }
        }

        public static PriceChangeRequestItem Find(ref List<PriceChangeRequestItem> priceChangeRequestItems, string conditionType, long? SapsoldToID, long? regionSapconditionGroupID, long? tierSapconditionGroupID, string currency, long? SapmaterialPricingGroupID, long? SappricingGroupID, long? SapshipToID, long? SapmaterialGroupID, string freightIndicator, string incoTerms2)
        {
            if (SapsoldToID != null)
            {
                PriceChangeRequestItem priceChangeRequestItem = (from pcri in priceChangeRequestItems
                                                                 where pcri.Sapcondition.Sapcode.Equals(conditionType)
                                                                    && pcri.PriceChangeRequest.SapsoldToID.Equals(SapsoldToID)
                                                                    && pcri.SapmaterialPricingGroupID.Equals(SapmaterialPricingGroupID)
                                                                    && pcri.SappricingGroupID.Equals(SappricingGroupID)
                                                                    && pcri.SapshipToID.Equals(SapshipToID)
                                                                    && pcri.SapmaterialGroupID.Equals(SapmaterialGroupID)
                                                                 select pcri).FirstOrDefault();

                return priceChangeRequestItem ?? new PriceChangeRequestItem();
            }
            else if (regionSapconditionGroupID != null && tierSapconditionGroupID != null && !string.IsNullOrEmpty(currency))
            {
                PriceChangeRequestItem priceChangeRequestItem = (from pcri in priceChangeRequestItems
                                                                 where pcri.Sapcondition.Sapcode.Equals(conditionType)
                                                                    && pcri.PriceChangeRequest.RegionSapconditionGroupID.Equals(regionSapconditionGroupID)
                                                                    && pcri.PriceChangeRequest.TierSapconditionGroupID.Equals(tierSapconditionGroupID)
                                                                    && pcri.PriceChangeRequest.Currency.Equals(currency)
                                                                    && pcri.SapmaterialPricingGroupID.Equals(SapmaterialPricingGroupID)
                                                                    && pcri.SappricingGroupID.Equals(SappricingGroupID)
                                                                 select pcri).FirstOrDefault();

                return priceChangeRequestItem ?? new PriceChangeRequestItem();
            }
            else if (regionSapconditionGroupID != null && !string.IsNullOrEmpty(currency) && !string.IsNullOrEmpty(freightIndicator) && !string.IsNullOrEmpty(incoTerms2))
            {
                PriceChangeRequestItem priceChangeRequestItem = (from pcri in priceChangeRequestItems
                                                                 where pcri.Sapcondition.Sapcode.Equals(conditionType)
                                                                    && pcri.PriceChangeRequest.RegionSapconditionGroupID.Equals(regionSapconditionGroupID)
                                                                    && pcri.PriceChangeRequest.Currency.Equals(currency)
                                                                    && pcri.FreightIndicator.Equals(freightIndicator)
                                                                    && pcri.IncoTerms2.Equals(incoTerms2)
                                                                 select pcri).FirstOrDefault();

                return priceChangeRequestItem ?? new PriceChangeRequestItem();
            }

            return new PriceChangeRequestItem();
        }
    }
}
