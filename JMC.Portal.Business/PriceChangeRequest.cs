using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;

namespace JMC.Portal.Business
{
    public partial class PriceChangeRequest
    {
        public string TitleHTML
        {
            get
            {
                if (this.SapsoldToID > 0)
                {
                    return "<span style=\"font-weight:bold;\">" + this.SapsoldTo.TrimmedNumber + " " + this.SapsoldTo.Name + "</span><br />" + this.SapsoldTo.Address;
                }
                else if (this.RegionSapconditionGroupID > 0 && this.TierSapconditionGroupID > 0)
                {
                    return "<span style=\"font-weight:bold;\">" + this.RegionSapconditionGroup.Name + "</span><br />" + this.TierSapconditionGroup.Name + " " + this.Currency;
                }

                return string.Empty;
            }
        }


        public string TitleExcel
        {
            get
            {
                if (this.SapsoldToID > 0)
                {
                    return this.SapsoldTo.TrimmedNumber + " " + this.SapsoldTo.Name + " " + this.SapsoldTo.Address;
                }
                else if (this.RegionSapconditionGroupID > 0 && this.TierSapconditionGroupID > 0)
                {
                    return this.RegionSapconditionGroup.Name + "" + this.TierSapconditionGroup.Name + " " + this.Currency;
                }

                return string.Empty;
            }
        }


        public List<PriceChangeRequestItem> OpenPriceChangeRequestItems { get; set; }
        public List<PriceChangeRequestItem> ClosedPriceChangeRequestItems { get; set; }

        public bool IsOpen
        {
            get
            {
                return (this.OpenPriceChangeRequestItems.Count() > 0);
            }
        }

        public int UserCanApprove(Employee employee, decimal difference)
        {
            PortalEntities db = new PortalEntities();

            decimal approvalThreshold = ConfigurationManager.AppSettings["PriceChangeApprovalThreshold"].ToInt();
            //decimal approvalThreshold = 100;

            if (!employee.IsNull())
            {
                if (employee.HasRole(ref db, (long)Enums.ApplicationRoles.PriceChangeAdmin))
                {
                    return (int)PriceChangeRequestItem.ApprovalCheckValues.PriceChangeAdmin;
                }
                else if (employee.HasRole(ref db, (long)Enums.ApplicationRoles.PriceChangeApprover))
                {
                    return Math.Abs(difference) <= approvalThreshold ? (int)PriceChangeRequestItem.ApprovalCheckValues.CanApprove : (int)PriceChangeRequestItem.ApprovalCheckValues.CouldApproveButExceedsLimit;
                }
                else if (employee.HasRole(ref db, (long)Enums.ApplicationRoles.RegionalManager))
                {
                    return Math.Abs(difference) <= approvalThreshold ? (int)PriceChangeRequestItem.ApprovalCheckValues.CanApprove : (int)PriceChangeRequestItem.ApprovalCheckValues.CouldApproveButExceedsLimit;
                }
            }

            return (int)PriceChangeRequestItem.ApprovalCheckValues.CanNotApprove;
        }

        public void GetPricingFromSAP()
        {
            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            ZGetPricingConditions getPricingConditions = new ZGetPricingConditions();

            ArrayList pricingConditionArrayList = new ArrayList();

            foreach (PriceChangeRequestItem priceChangeRequestItem in this.OpenPriceChangeRequestItems)
            {
                ZstGetPricingConditions zstGetPricingConditions = new ZstGetPricingConditions();
                zstGetPricingConditions.WebPricechangerequestitemid = priceChangeRequestItem.PriceChangeRequestItemID.ToInt();
                zstGetPricingConditions.ConditionType = priceChangeRequestItem.Sapcondition.Sapcode;
                zstGetPricingConditions.SoldToNumber = !priceChangeRequestItem.PriceChangeRequest.SapsoldTo.IsNull() ? priceChangeRequestItem.PriceChangeRequest.SapsoldTo.Number : string.Empty;
                zstGetPricingConditions.MaterialPricingGroup = !priceChangeRequestItem.SapmaterialPricingGroup.IsNull() ? priceChangeRequestItem.SapmaterialPricingGroup.Sapcode : string.Empty;
                zstGetPricingConditions.PricingGroup = !priceChangeRequestItem.SappricingGroup.IsNull() ? priceChangeRequestItem.SappricingGroup.Sapcode : string.Empty;
                zstGetPricingConditions.MaterialGroup = !priceChangeRequestItem.SapmaterialGroup.IsNull() ? priceChangeRequestItem.SapmaterialGroup.Sapcode : string.Empty;
                zstGetPricingConditions.ShipToNumber = !priceChangeRequestItem.SapshipTo.IsNull() ? priceChangeRequestItem.SapshipTo.Number : string.Empty;
                zstGetPricingConditions.Region = !priceChangeRequestItem.PriceChangeRequest.RegionSapconditionGroup.IsNull() ? priceChangeRequestItem.PriceChangeRequest.RegionSapconditionGroup.Sapcode : string.Empty;
                zstGetPricingConditions.Tier = !priceChangeRequestItem.PriceChangeRequest.TierSapconditionGroup.IsNull() ? priceChangeRequestItem.PriceChangeRequest.TierSapconditionGroup.Sapcode : string.Empty;
                zstGetPricingConditions.Currency = priceChangeRequestItem.PriceChangeRequest.Currency;
                zstGetPricingConditions.EffectiveDate = priceChangeRequestItem.EffectiveDate;

                pricingConditionArrayList.Add(zstGetPricingConditions);
            }

            getPricingConditions.PricingConditions = (ZstGetPricingConditions[])pricingConditionArrayList.ToArray(typeof(ZstGetPricingConditions));

            sapPortalService.Open();
            ZGetPricingConditionsResponse getPricingConditionsResponse = sapPortalService.ZGetPricingConditionsAsync(getPricingConditions);
            sapPortalService.Close();

            foreach (ZstGetPricingConditions zstGetPricingCondition in getPricingConditionsResponse.PricingConditions)
            {
                if (!zstGetPricingCondition.ConditionType.jIsEmpty())
                {
                    int priceChangeRequestItemID = zstGetPricingCondition.WebPricechangerequestitemid;

                    if (priceChangeRequestItemID > 0)
                    {
                        PriceChangeRequestItem priceChangeRequestItem = (from pcri in this.OpenPriceChangeRequestItems where pcri.PriceChangeRequestItemID == priceChangeRequestItemID select pcri).FirstOrDefault();

                        if (!priceChangeRequestItem.IsNull())
                        {
                            SAPCondition SAPCondition = new SAPCondition();

                            SAPCondition.Sapcode = zstGetPricingCondition.ConditionType;
                            SAPCondition.Rate = zstGetPricingCondition.Rate;
                            SAPCondition.RateUnit = zstGetPricingCondition.RateUnit;
                            SAPCondition.ValidFrom = zstGetPricingCondition.ValidFrom.ToDate();
                            SAPCondition.ValidTo = zstGetPricingCondition.ValidTo.ToDate();
                            SAPCondition.PricePer = zstGetPricingCondition.PricePer.ToInt();
                            SAPCondition.PricePerUnit = zstGetPricingCondition.PricePerUnit;

                            priceChangeRequestItem.OldSapcondition = SAPCondition;
                        }
                    }
                }
            }
        }

        public string GetEmailTitleHTML()
        {
            if (this.SapsoldToID > 0)
            {
                return "<span style=\"font-weight:bold;\">" + this.SapsoldTo.TrimmedNumber + " " + this.SapsoldTo.Name + "</span><br />" + this.SapsoldTo.Address;
            }
            else if (this.RegionSapconditionGroupID > 0 && this.TierSapconditionGroupID > 0)
            {
                return "<span style=\"font-weight:bold;\">" + this.RegionSapconditionGroup.Name + "</span><br />" + this.TierSapconditionGroup.Name + " " + this.Currency;
            }

            return string.Empty;
        }

        public string GetEmailSendTo(ref PortalEntities db)
        {
            string emailSendTo = string.Empty;

            List<User> users = new List<User>();

            ArrayList userIDs = new ArrayList();

            if (this.UserID > 0)
            {
                userIDs.Add(this.UserID);
                users.Add(this.User);
            }

            if (this.SapsoldToID > 0)
            {
                if (this.SapsoldTo.SapsalesGroup.UserID > 0 && this.SapsoldTo.SapsalesGroup.UserID != this.UserID && !userIDs.Contains(this.SapsoldTo.SapsalesGroup.UserID))
                {
                    userIDs.Add(this.SapsoldTo.SapsalesGroup.UserID);
                    users.Add(this.SapsoldTo.SapsalesGroup.User);
                }

                if (this.SapsoldTo.SapcustomerGroup.UserID > 0 && this.SapsoldTo.SapcustomerGroup.UserID != this.UserID && !userIDs.Contains(this.SapsoldTo.SapcustomerGroup.UserID))
                {
                    userIDs.Add(this.SapsoldTo.SapcustomerGroup.UserID);
                    users.Add(this.SapsoldTo.SapcustomerGroup.User);
                }

                if (this.SapsoldTo.SapcustomerGroup.RegionalManagerUserID > 0 && this.SapsoldTo.SapcustomerGroup.RegionalManagerUserID != this.UserID && !userIDs.Contains(this.SapsoldTo.SapcustomerGroup.RegionalManagerUserID))
                {
                    userIDs.Add(this.SapsoldTo.SapcustomerGroup.RegionalManagerUserID);
                    users.Add(this.SapsoldTo.SapcustomerGroup.RegionalManagerUser);
                }
            }
            else if (this.RegionSapconditionGroupID > 0 && this.TierSapconditionGroupID > 0)
            {
                List<SAPSoldTo> SAPSoldTos = SAPSoldTo.FindAllByRegion(ref db, this.RegionSapconditionGroupID.ToLong());

                foreach (SAPSoldTo SAPSoldTo in SAPSoldTos)
                {
                    if (SapsoldTo.SapsalesGroup.UserID > 0 && SapsoldTo.SapsalesGroup.UserID != this.UserID && !userIDs.Contains(SapsoldTo.SapsalesGroup.UserID))
                    {
                        userIDs.Add(SapsoldTo.SapsalesGroup.UserID);
                        users.Add(SapsoldTo.SapsalesGroup.User);
                    }

                    if (SapsoldTo.SapcustomerGroup.UserID > 0 && SapsoldTo.SapcustomerGroup.UserID != this.UserID && !userIDs.Contains(SapsoldTo.SapcustomerGroup.UserID))
                    {
                        userIDs.Add(SapsoldTo.SapcustomerGroup.UserID);
                        users.Add(SapsoldTo.SapcustomerGroup.User);
                    }

                    List<User> additionalUsers = (from cgu in SapsoldTo.SapcustomerGroup.SapcustomerGroupUsers select cgu.User).ToList();

                    foreach (User user in additionalUsers)
                    {
                        if (user.UserID > 0 && user.UserID != this.UserID && !userIDs.Contains(user.UserID))
                        {
                            userIDs.Add(user.UserID);
                            users.Add(user);
                        }
                    }

                    if (SapsoldTo.SapcustomerGroup.RegionalManagerUserID > 0 && SapsoldTo.SapcustomerGroup.RegionalManagerUserID != this.UserID && !userIDs.Contains(SapsoldTo.SapcustomerGroup.RegionalManagerUserID))
                    {
                        userIDs.Add(SapsoldTo.SapcustomerGroup.RegionalManagerUserID);
                        users.Add(SapsoldTo.SapcustomerGroup.RegionalManagerUser);
                    }
                }
            }

            //User mattMcMahon = (from u in db.User where u.UserID == 20 select u).FirstOrDefault();
            // var usrlist = (from u in db.User.OfType<Employee>() where u.HasRole((long)Enums.ApplicationRoles.PriceChangeReceiver) select u).ToList();

            //	ApplicationRoles PriceChangeReceiver = (from p in db.ApplicationRole where p.ApplicationRoleID == (long)Enums.ApplicationRoles.PriceChangeReceiver select p);
            //var usrlist = (from u in db.User.OfType<Employee>() where u.ApplicationRole.Contains(PriceChangeReceiver) select u).ToList();

            var PriceChangeReceiver = (from p in db.ApplicationRoles where p.ApplicationRoleID == (long)Enums.ApplicationRoles.PriceChangeReceiver select p.ApplicationRoleID).FirstOrDefault();
            var usrlist = (from u in db.Users.OfType<Employee>() select u).ToList().Where(m => m.ApplicationRoles.Any(o => o.ApplicationRoleID == PriceChangeReceiver)).ToList();

            foreach (User usr in usrlist)
            {
                if (!usr.IsNull() && !users.Contains(usr))
                {
                    users.Add(usr);
                }
            }

            foreach (User user in users)
            {
                if (!user.Email.jIsEmpty())
                {
                    emailSendTo += user.Email + ";";
                }
            }

            return emailSendTo;
        }

        public void SendCreatedEmail(ref PortalEntities db)
        {
            this.OpenPriceChangeRequestItems = (from pcri in this.PriceChangeRequestItems where pcri.Approved.Equals(null) select pcri).ToList();
            this.GetPricingFromSAP();

            StringBuilder emailSB = new StringBuilder();

            emailSB.Append("The following price change has been submitted for approval:<br /><br />");
            //emailSB.Append("<a href=\"http://" + url + "/Sales/PriceChangeRequests.aspx\">Click here to view the pending price change request.</a><br />");
            emailSB.Append("<br />Regional Manager – please review for approval.<br />");
            emailSB.Append("<br /><span style=\"font-weight: bold;\">Date:</span> ");
            emailSB.Append(string.Format("{0 :MMM dd, yyyy h:mmtt}", this.Date));
            emailSB.Append("<br /><span style=\"font-weight: bold;\">Requested By:</span>");
            emailSB.Append(this.User.FullName);
            emailSB.Append("<br /><br />");
            emailSB.Append(this.GetEmailTitleHTML());
            emailSB.Append("<br />");
            emailSB.Append("<ul>");

            foreach (PriceChangeRequestItem priceChangeRequestItem in this.PriceChangeRequestItems)
            {
                emailSB.Append("<li><span style=\"font-weight:bold;\">Effective ");
                emailSB.Append(priceChangeRequestItem.EffectiveDate.ToString("MMM dd, yyyy"));
                emailSB.Append("</span> ");
                emailSB.Append(priceChangeRequestItem.EmailResponseDescriptionHTML);
                emailSB.Append("</li>");
            }

            emailSB.Append("</ul>");

            emailSB.Append("<br /><br />You will be notified when the price change has been approved or denied.<br /><br />Thank you for using the Atlas Tube Portal.");

            string sendTo = this.GetEmailSendTo(ref db);
            //string sendTo = "christian.cooper@jmcsteel.com";

            try
            {
                Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], sendTo, string.Empty, "Price Change Submitted for Approval", emailSB.ToString());
            }
            catch (Exception ex)
            {
                Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["ErrorEmailAddress"], string.Empty, "Error Sending: Price Change Submitted for Approval", emailSB.ToString());
            }
        }
    }
}
