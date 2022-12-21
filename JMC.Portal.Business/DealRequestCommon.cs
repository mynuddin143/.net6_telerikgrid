using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using AtlasSAPPortal;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace JMC.Portal.Business
{
    public partial class DealRequestCommon
    {
        public List<PriceChangeRequestItem> OpenPriceChangeRequestItems { get; set; }
        public List<PriceChangeRequestItem> ClosedPriceChangeRequestItems { get; set; }

        public List<DealsDetail> dealDetails = new List<DealsDetail>();
        public List<DealsBySoldToShipTo> dealsBySoldToShipTo = new List<DealsBySoldToShipTo>();
        public List<DealsMaterialPricingGroup> dealsMaterialPricingGroup = new List<DealsMaterialPricingGroup>();
        public List<DealsPricingGroup> dealsPricingGroup = new List<DealsPricingGroup>();

        public SAPSoldTo SAPSoldTo { get; private set; }
        //public SAPShipTo SAPShipTo { get; private set; }
        public IEnumerable<SAPSoldTo> SAPSoldTos = null;
        public IEnumerable<SAPShipTo> SAPShipTos = null;
        public SAPSoldTo sapSoldTo;
        public SAPShipTo sapShipTo;

        public List<SAPCharacteristicOption> SAPMaterialPricingGroups { get; private set; }
        public List<SAPCharacteristicOption> SAPPricingGroups { get; private set; }
        public List<DealsPlant> dealPlant = new List<DealsPlant>();
        public List<Plant> ValidPlants = new List<Portal.Business.Plant>();

        public class SAPShipToSelectedClass
        {
            public int? selectedSoldToIDColumn { get; set; }
            public string selectedSoldToNameColumn { get; set; }
            public string selectedShipToIDColumn { get; set; }
            public string selectedShipToColumn { get; set; }
            public string selectedShipToNameColumn { get; set; }
        }
        public List<SAPShipToSelectedClass> SAPShipToSelected { get; private set; }

        //public HttpSessionStateBase Session;
        public HttpContext Session;

        public PortalEntities db;
        public Employee CurrentEmployee { get; private set; }
        public User user = null;

        public string strDealType = string.Empty;

        public int? isSubmittedApprovedDenied = 0;

        public User CreatedUser;
        public DealsDetail dealInformation = new DealsDetail();

        public void GetEmailSendToObjects(long? intDealID, User userObject, int? SubmittedApprovedDenied)
        {
            isSubmittedApprovedDenied = SubmittedApprovedDenied;

            this.SAPShipToSelected = new List<JMC.Portal.Business.DealRequestCommon.SAPShipToSelectedClass>();

            db = new PortalEntities();
            if (db.IsNull()) db = new PortalEntities();

            dealDetails = db.DealsDetails.Where(o => o.DealID == intDealID).ToList();
            //dealsBySoldToShipTo = db.DealsBySoldToShipToes.Where(o => o.DealID == intDealID).ToList();
            dealsMaterialPricingGroup = db.DealsMaterialPricingGroups.Where(o => o.DealID == intDealID).ToList();
            dealsPricingGroup = db.DealsPricingGroups.Where(o => o.DealID == intDealID).OrderBy(x => x.Zr01).ToList();

            dealsBySoldToShipTo = db.DealsBySoldToShipTos.Where(o => o.DealID == intDealID).ToList();

            dealPlant = db.DealsPlants.Where(o => o.DealID == intDealID).ToList();

            foreach (DealsBySoldToShipTo dealsBySoldToShipToItem in dealsBySoldToShipTo)
            {
                this.sapSoldTo = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.SapshipToID == dealsBySoldToShipToItem.SapsoldToID select s).FirstOrDefault();

                this.sapShipTo = this.SAPSoldTo.sapshipTos.Where(x => x.SapshipToID == dealsBySoldToShipToItem.SapshipToID).FirstOrDefault();

                if (this.sapShipTo != null && this.SAPSoldTo != null)
                {
                    string strShipToName = this.sapShipTo.TrimmedNumber + " " + this.sapShipTo.Name + " (" + (this.sapShipTo.City.Name + ", " + this.sapShipTo.City.State.Name) + ")";
                    string strSoldToName = this.sapSoldTo.TrimmedNumber + " " + this.sapSoldTo.Name + " (" + (this.sapSoldTo.City.Name + ", " + this.sapSoldTo.City.State.Name) + ")";

                    this.SAPShipToSelected.Add(new JMC.Portal.Business.DealRequestCommon.SAPShipToSelectedClass { selectedSoldToIDColumn = dealsBySoldToShipToItem.SapsoldToID.ToInt(), selectedSoldToNameColumn = strSoldToName.ToString(), selectedShipToIDColumn = dealsBySoldToShipToItem.SapshipToID.ToString(), selectedShipToColumn = dealsBySoldToShipToItem.SapshipTo.TrimmedNumber.ToString(), selectedShipToNameColumn = strShipToName.ToString() });
                }
            }
            this.SAPShipToSelected.OrderBy(o => o.selectedSoldToIDColumn).ToList();

            dealInformation = (from x in db.DealsDetails where x.DealID == intDealID select x).FirstOrDefault();
            CreatedUser = (from u in db.Users where u.Active == true && u.UserID == dealInformation.CreatedUserID select u).FirstOrDefault();

        }

        public string GetEmailSendTo(long? intDealID, User userObject, int? isSubmitOrApproved)
        {
            string emailSendTo = string.Empty;

            List<User> users = new List<User>();

            ArrayList userIDs = new ArrayList();


            if (userObject.UserID > 0)
            {
                userIDs.Add(userObject.UserID);
                users.Add(userObject);
            }

            db = new PortalEntities();
            dealsBySoldToShipTo = db.DealsBySoldToShipTos.Where(o => o.DealID == intDealID).ToList();
            for (int i = 0; i < dealsBySoldToShipTo.Count; i++)
            {
                long? intSAPSoldTo = this.dealsBySoldToShipTo[i].SapsoldToID.ToLong();

                List<SAPSoldTo> SAPSoldTos = SAPSoldTo.FindRegionSAPConditionGroupID(ref db, this.dealsBySoldToShipTo[i].SapsoldToID.ToLong());

                foreach (SAPSoldTo SAPSoldTo in SAPSoldTos)
                {
                    if (SAPSoldTo.SapsalesGroup.UserID > 0 && SAPSoldTo.SapsalesGroup.UserID != userObject.UserID && !userIDs.Contains(SAPSoldTo.SapsalesGroup.UserID))
                    {
                        userIDs.Add(SAPSoldTo.SapsalesGroup.UserID);
                        users.Add(SAPSoldTo.SapsalesGroup.User);
                    }

                    if (SAPSoldTo.SapcustomerGroup.UserID > 0 && SAPSoldTo.SapcustomerGroup.UserID != userObject.UserID && !userIDs.Contains(SAPSoldTo.SapcustomerGroup.UserID))
                    {
                        userIDs.Add(SAPSoldTo.SapcustomerGroup.UserID);
                        users.Add(SAPSoldTo.SapcustomerGroup.User);
                    }

                    List<User> additionalUsers = (from cgu in SAPSoldTo.SapcustomerGroup.SapcustomerGroupUsers select cgu.User).ToList();

                    foreach (User user in additionalUsers)
                    {
                        if (user.UserID > 0 && user.UserID != userObject.UserID && !userIDs.Contains(user.UserID))
                        {
                            userIDs.Add(user.UserID);
                            users.Add(user);
                        }
                    }

                    if (SAPSoldTo.SapcustomerGroup.RegionalManagerUserID > 0 && SAPSoldTo.SapcustomerGroup.RegionalManagerUserID != userObject.UserID && !userIDs.Contains(SAPSoldTo.SapcustomerGroup.RegionalManagerUserID))
                    {
                        userIDs.Add(SAPSoldTo.SapcustomerGroup.RegionalManagerUserID);
                        users.Add(SAPSoldTo.SapcustomerGroup.RegionalManagerUser);
                    }

                    bool EnableCustomerEmailForDeal = System.Configuration.ConfigurationManager.AppSettings["EnableCustomerEmailForDeal"].ToBool();
                    if (EnableCustomerEmailForDeal && isSubmitOrApproved == 2) //isSubmitOrApproved = 2 means approved
                    {
                        List<User> customerUsers = (from u in db.Users.OfType<User>()
                                                    from st in u.SapsoldTos.DefaultIfEmpty()
                                                    from ar in db.ApplicationRoles
                                                    where ((intSAPSoldTo == null || st.SapshipToID == intSAPSoldTo || u.PrimarySapsoldToID == intSAPSoldTo) && st.DivisionID == (long)Enums.Divisions.Atlas) && u.Active == true && !(u is Employee) && u.ApplicationRoles.Any(o => o.Name == "ViewPricing")
                                                    select u).Distinct().ToList();
                        foreach (User user in customerUsers)
                        {
                            if (user.UserID > 0 && user.UserID != userObject.UserID && !userIDs.Contains(user.UserID))
                            {
                                userIDs.Add(user.UserID);
                                users.Add(user);
                            }
                        }
                    }
                }
            }

            var PriceChangeReceiver = (from p in db.ApplicationRoles where p.ApplicationRoleID == (long)Enums.ApplicationRoles.PriceChangeReceiver select p.ApplicationRoleID).FirstOrDefault();
            var usrlist = (from u in db.Users.OfType<Employee>() where u.Active == true && u.DivisionID == (long)Enums.Divisions.Atlas && u.ApplicationRoles.Any(o => o.ApplicationRoleID == PriceChangeReceiver) select u).ToList();

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

        public void getUserInfo(User userObject)
        {
            string userName = ((JMC.Portal.Business.Employee)(userObject)).Domain + "\\" + ((JMC.Portal.Business.Employee)(userObject)).SamaccountName;
            this.CurrentEmployee = Employee.FindByDomainAndSAMAccountName(ref db, userName);

            if (db.IsNull()) db = new PortalEntities();
            if (user.IsNull())
            {
                user = db.Users.FirstOrDefault(x => x.UserName == userName);
                if (user.IsNull()) user = Employee.FindByDomainAndSAMAccountName(ref db, userName);
            }
        }

        public StringBuilder GetSubmittedEmailHeader(long? intDealID, User userObject)
        {
            StringBuilder emailSB = new StringBuilder();
            foreach (DealsDetail dealDetailsItem in dealDetails)
            {
                emailSB.Append("This deal has been Submitted By : ");
                emailSB.Append(userObject.FullName);
                //emailSB.Append("<br />");
                //emailSB.Append("<br />Regional Manager – please review for approval.");
                //emailSB.Append("<br /><br /><div><span style=\"font-weight: bold;\">Deal Number:&nbsp;</span>");
                //emailSB.Append(intDealID);
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Deal Name:&nbsp;</span> ");
                //emailSB.Append(dealDetailsItem.Name);
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Created Date:&nbsp;</span>");
                //emailSB.Append(Convert.ToDateTime(dealDetailsItem.CreatedDate));
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Requested By:&nbsp;</span>");
                //emailSB.Append(userObject.FullName);
                //emailSB.Append("<br />");
                //emailSB.Append("</div>");
            }
            return emailSB;
        }

        public StringBuilder GetModifedEmailHeader(long? intDealID, User userObject)
        {
            StringBuilder emailSB = new StringBuilder();
            foreach (DealsDetail dealDetailsItem in dealDetails)
            {
                emailSB.Append("This deal has been Modified By : ");
                emailSB.Append(userObject.FullName);
                //emailSB.Append("<br />");
                //emailSB.Append("<br />Regional Manager – please review for approval.");
                //emailSB.Append("<br /><br /><div><span style=\"font-weight: bold;\">Deal Number:&nbsp;</span>");
                //emailSB.Append(intDealID);
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Deal Name:&nbsp;</span> ");
                //emailSB.Append(dealDetailsItem.Name);
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Created Date:&nbsp;</span>");
                //emailSB.Append(Convert.ToDateTime(dealDetailsItem.CreatedDate));
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Requested By:&nbsp;</span>");
                //emailSB.Append(userObject.FullName);
                //emailSB.Append("<br />");
                //emailSB.Append("</div>");
            }
            return emailSB;
        }

        public StringBuilder GetApprovedEmailHeader(long? intDealID, User userObject, string strCreatedBy)
        {
            StringBuilder emailSB = new StringBuilder();
            foreach (DealsDetail dealDetailsItem in dealDetails)
            {
                emailSB.Append("This deal has been Approved / Denied By : ");
                emailSB.Append(userObject.FullName);
                //emailSB.Append("<br />");
                //emailSB.Append("<br /><br /><div><span style=\"font-weight: bold;\">Deal Number:&nbsp;</span>");
                //emailSB.Append(intDealID);
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Deal Name:&nbsp;</span> ");
                //emailSB.Append(dealDetailsItem.Name);
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Date:&nbsp;</span> ");
                //emailSB.Append(Convert.ToDateTime(DateTime.Now.ToDate().ToShortDateString()));
                //emailSB.Append("<br /><span style=\"font-weight: bold;\">Requested By:&nbsp;</span>");
                //emailSB.Append(strCreatedBy);
                //emailSB.Append("<br />");
                //emailSB.Append("</div>");
            }
            return emailSB;
        }

        public void GetPlants()
        {
            if (this.user is Employee)
            {
                ValidPlants = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionID == this.CurrentEmployee.DivisionID select l).ToList();
            }
            else
            {
                ValidPlants = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionID == this.user.SapsoldTo.DivisionID select l).ToList();
            }
        }

        public void SendCreatedEmail(int? intDealID, User userObject, string emailSB, int isSubmitOrApproved, string strCreatedBy)
        {
            GetEmailSendToObjects(intDealID, userObject, isSubmitOrApproved);

            string sendTo = GetEmailSendTo(intDealID, userObject, 2);

            try
            {
                StringBuilder emailSB1 = new StringBuilder();
                if (isSubmitOrApproved == 1) //isSubmitOrApproved = 1 means submitted, 2 means approved
                    emailSB1 = GetSubmittedEmailHeader(intDealID, userObject);
                else if (isSubmitOrApproved == 5) //isSubmitOrApproved = 5 means modified
                    emailSB1 = GetModifedEmailHeader(intDealID, userObject);
                else
                    emailSB1 = GetApprovedEmailHeader(intDealID, userObject, strCreatedBy);

                emailSB = emailSB1 + emailSB;

                string emailSubject = "Deal has been ";
                if (isSubmitOrApproved.ToString() == "1")
                    emailSubject += JMC.Portal.Business.Enums.SubmittedApprovedDenied.Submitted.ToString();
                else if (isSubmitOrApproved.ToString() == "2")
                    emailSubject += JMC.Portal.Business.Enums.SubmittedApprovedDenied.Approved.ToString();
                else if (isSubmitOrApproved.ToString() == "3")
                    emailSubject += JMC.Portal.Business.Enums.SubmittedApprovedDenied.Denied.ToString();
                else if (isSubmitOrApproved.ToString() == "5")
                    emailSubject += JMC.Portal.Business.Enums.SubmittedApprovedDenied.Modified.ToString();
                else
                    emailSubject += JMC.Portal.Business.Enums.SubmittedApprovedDenied.Submitted.ToString();

                Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], sendTo, string.Empty, string.Empty, emailSubject, emailSB.ToString());
            }
            catch (Exception ex)
            {
                Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["ErrorEmailAddress"], string.Empty, string.Empty, "Error Sending: Deal Submitted for Approval", emailSB.ToString());
            }
        }

        //public void SendCreatedEmail(long? intDealID, User userObject, int? isSubmitOrApproved, string strCreatedBy)
        //{
        //    try
        //    {
        //        GetEmailSendToObjects(intDealID, userObject, (int)Enums.SubmittedApprovedDenied.Approved);

        //        //StringBuilder emailSB1 = new StringBuilder();
        //        //if (isSubmitOrApproved == 1) //isSubmitOrApproved = 1 means submitted, 2 means approved
        //        //    emailSB1 = this.GetSubmittedEmailHeader(intDealID, userObject);
        //        //else
        //        //    emailSB1 = this.GetApprovedEmailHeader(intDealID, userObject, strCreatedBy);

        //        //string emailSB = this.generateHTML(emailSB1, intDealID, userObject);

        //        this.getUserInfo(userObject);

        //        string emailSB = string.Empty;
        //        if (isSubmitOrApproved == 1) //isSubmitOrApproved = 1 means submitted, 2 means approved
        //            emailSB = this.generateHTML(intDealID, userObject, 1);
        //        else
        //            emailSB = this.generateHTML(intDealID, userObject, 0);


        //        string sendTo = this.GetEmailSendTo(intDealID, userObject, isSubmitOrApproved);

        //        try
        //        {
        //            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], sendTo, string.Empty, "Deal Submitted for Approval", emailSB.ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["ErrorEmailAddress"], string.Empty, "Error Sending: Deal Submitted for Approval", emailSB.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string generateHTML(long? intDealID, User userObject, int isSubmitOrApproved)
        //{

        //    //GetEmailSendTo(intDealID, userObject);
        //    StringBuilder emailSB = new StringBuilder();

        //    if (isSubmitOrApproved == 1)
        //    {
        //        emailSB.Append("The following deal has been submitted for approval:");
        //        emailSB.Append("<br />Regional Manager – please review for approval.");
        //        //emailSB.Append("<br />");
        //    }
        //    else
        //    {
        //        emailSB.Append("This following deal has been Approved / Denied By : ");
        //        emailSB.Append(userObject.FullName);
        //        //emailSB.Append("<br />");
        //    }

        //    emailSB.Append("<style type=\"text/css\">.float-container { } .float-child { width: 50%; float: left; } </style>");
        //    emailSB.Append("<div class=\"float-container\">");

        //    emailSB.Append("<div class=\"float-child\">");

        //    foreach (DealsDetail dealDetailsItem in dealDetails)
        //    {
        //        emailSB.Append("<table id=\"tblContent2\" style=\"width: 390px; font-family: Arial, Tahoma, Verdana, Helvetica, sans-serif;font-size: 12px; margin-top: 20px; border: 1px solid;\">");
        //        emailSB.Append("<tbody>");

        //        emailSB.Append("<tr>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">");
        //        emailSB.Append("<b>Deal Number</b>");
        //        emailSB.Append("</td>");
        //        emailSB.Append("<td style=\"text-align: right; text-align: left;\">");
        //        emailSB.Append(intDealID);
        //        emailSB.Append("</td>");
        //        emailSB.Append("</tr>");

        //        emailSB.Append("<tr>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">");
        //        emailSB.Append("<b>Deal Number</b>");
        //        emailSB.Append("</td>");
        //        emailSB.Append("<td style=\"text-align: right; text-align: left;\" colspan=\"6\">");
        //        emailSB.Append(dealDetailsItem.Name);
        //        emailSB.Append("</td>");
        //        emailSB.Append("</tr>");

        //        this.GetPlants();
        //        List<long?> intPLantIDs = (from s in dealDetailsItem.DealsPlants where s.DealID == dealDetailsItem.DealID select s.PlantID).ToList();
        //        string strPlantName = string.Empty;

        //        for (int i = 0; i < intPLantIDs.Count; i++)
        //            strPlantName = strPlantName + ", " + (from s in this.ValidPlants where s.LocationID == intPLantIDs[i] select s.Name).FirstOrDefault();

        //        emailSB.Append("<tr>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\"><b>Deal Number</b></td>");
        //        emailSB.Append("<td style=\"text-align: right; text-align: left;\" colspan=\"6\">" + strPlantName.TrimStart(',').TrimEnd(')') + ")</td>");
        //        emailSB.Append("</tr>");


        //        emailSB.Append("<tr>");
        //        emailSB.Append("<th style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3; text-align: left;\" rowspan=\"2\">Order By Date</th>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">From</td>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">To</td>");
        //        emailSB.Append("</tr>");

        //        emailSB.Append("<tr>");
        //        emailSB.Append("<td>" + dealDetailsItem.OrderFromDate.ToDate().ToString("MMM dd, yyyy") + "</td>");
        //        emailSB.Append("<td>" + dealDetailsItem.OrderToDate.ToDate().ToString("MMM dd, yyyy") + "</td>");
        //        emailSB.Append("</tr>");


        //        emailSB.Append("<tr>");
        //        emailSB.Append("<th style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3; text-align: left;\" rowspan=\"2\">Ship By Date</th>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">From</td>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">To</td>");
        //        emailSB.Append("</tr>");

        //        emailSB.Append("<tr>");
        //        emailSB.Append("<td>" + dealDetailsItem.ShipFromDate.ToDate().ToString("MMM dd, yyyy") + "</td>");
        //        emailSB.Append("<td>" + dealDetailsItem.ShipToDate.ToDate().ToString("MMM dd, yyyy") + "</td>");
        //        emailSB.Append("</tr>");


        //        emailSB.Append("<tr>");
        //        emailSB.Append("<th style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3; text-align: left;\" rowspan=\"2\">Tons</th>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">Max Tons</td>");
        //        emailSB.Append("<td style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3;\">Threshold Limit</td>");
        //        emailSB.Append("</tr>");


        //        decimal maxTons = this.user is Employee ? ConfigurationManager.AppSettings["MAXTons"].ToDecimal() : (ConfigurationManager.AppSettings["MAXTons"].ToDecimal() + dealDetailsItem.ThresholdLimit.ToDecimal());
        //        emailSB.Append("<tr>");
        //        emailSB.Append("<td>" + maxTons.ToString() + "</td>");
        //        if (this.user is Employee)
        //        {
        //            emailSB.Append("<td>" + dealDetailsItem.ThresholdLimit.ToString() + "</td>");
        //        }               
        //        emailSB.Append("</tr>");


        //        emailSB.Append("<tr>");
        //        emailSB.Append("<th style=\"padding: 4px; min-width: 45px; background-color: #E3E3E3; text-align: left;\" rowspan=\"2\">Created by</th>");
        //        emailSB.Append("<td style=\"vertical-align: middle;\" colspan=\"4\"><b>Name: </b>" + CreatedUser.FirstName + " " + CreatedUser.LastName + "<br /><b> Phone: </b>" + CreatedUser.PhoneNumber + "<br /><b> Email: </b>" + CreatedUser.Email + "</td>");
        //        emailSB.Append("</tr>");

        //        strDealType = dealDetailsItem.DealType;
        //    }

        //    emailSB.Append("<br />");
        //    emailSB.Append("<table id=\"Table1\" style=\"font-family: Arial, Tahoma, Verdana, Helvetica, sans-serif; font-size: 12px; width: 390px; border: 1px solid;\">");
        //    emailSB.Append("<tbody>");
        //    emailSB.Append("<tr>");
        //    emailSB.Append("<td colspan=\"4\">");
        //    emailSB.Append("<table id=\"mySelectionTable\" style=\"font-family: Arial, Tahoma, Verdana, Helvetica, sans-serif; font-size: 12px; width: 380px;\">");
        //    emailSB.Append("<thead>");
        //    emailSB.Append("<tr style=\"background-color: #941800; color: #FFFFFF;\">");
        //    emailSB.Append("<th>Sold To / Ship To Name</th>");
        //    emailSB.Append("</tr>");
        //    emailSB.Append("</thead>");
        //    emailSB.Append("<tbody>");
        //    string strHTML = generateSoldToShipToHTML(SAPShipToSelected);
        //    emailSB.Append(strHTML);
        //    emailSB.Append("</tbody>");
        //    emailSB.Append("</table>");
        //    emailSB.Append("</td>");
        //    emailSB.Append("</tr>");
        //    emailSB.Append("</tbody>");
        //    emailSB.Append("</table>");

        //    emailSB.Append("<br />");
        //    emailSB.Append("<p>You will be notified when the deal has been approved or denied.</p><p>Thank you for using the Atlas Tube Portal.</p>");

        //    emailSB.Append("</div>");


        //    emailSB.Append("<div class=\"float-child\">");
        //    emailSB.Append("<table id=\"myPriceTable1\" style=\"width: 350px;\">");
        //    emailSB.Append("<tbody>");
        //    emailSB.Append("<tr>");
        //    emailSB.Append("<td>");
        //    emailSB.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"font-family: Arial, Tahoma, Verdana, Helvetica, sans-serif;font-size: 12px; margin-top: 20px; width: 350px; border: 1px solid;\">");
        //    emailSB.Append("<tbody>");
        //    emailSB.Append("<tr>");
        //    emailSB.Append("<td style=\"padding: 8px; min-width: 45px; background-color: #E3E3E3;\"><b>Notes:</b></td>");
        //    foreach (DealsDetail dealDetailsItem in dealDetails)
        //    {
        //        emailSB.Append("<td style=\"vertical-align: middle;\" colspan=\"4\">" + dealDetailsItem.Notes  + "</td>");
        //    }
        //    emailSB.Append("</tr>");
        //    emailSB.Append("</tbody>");
        //    emailSB.Append("</table>");

        //    emailSB.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"font-family: Arial, Tahoma, Verdana, Helvetica, sans-serif;font-size: 12px; margin-top: 20px; width: 350px; border: 1px solid;\">");
        //    emailSB.Append("<tbody>");
        //    emailSB.Append("<tr>");
        //    emailSB.Append("<th style=\"width: 140px; border-bottom: solid 1px #D3D5C7;background-color: #941800; color: #FFFFFF;\">Material Pricing Group</th>");
        //    if (strDealType == Enums.DealType.Override.ToString())
        //    {
        //        emailSB.Append("<th style=\"width: 140px; border-bottom: solid 1px #D3D5C7; border-left: solid 1px #D3D5C7; background-color: #941800; color: #FFFFFF;\">Override (CWT) ZR02 ($)</th>");
        //    }
        //    else
        //    { 
        //        emailSB.Append("<th style=\"width: 140px; border-bottom: solid 1px #D3D5C7; border-left: solid 1px #D3D5C7; background-color: #941800; color: #FFFFFF;\">Discount (CWT) ZKA0 ($)</th>");
        //    }
        //    emailSB.Append("</tr>");

        //    this.SAPMaterialPricingGroups = (from co in db.SAPCharacteristicOption where co.SAPCharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup select co).ToList();

        //    int count = 0;
        //    foreach (DealsMaterialPricingGroup dealsMaterialPricingGroupItem in dealsMaterialPricingGroup)
        //    {
        //        var selectedSAPMaterialPricingGroups = SAPMaterialPricingGroups.Where(p => p.SAPCharacteristicOptionID.ToInt() == dealsMaterialPricingGroupItem.ZR00.ToInt()).FirstOrDefault();

        //        if (count % 2 == 0)
        //        {
        //            emailSB.Append("<tr>");
        //            emailSB.Append("<td>");
        //            emailSB.Append(selectedSAPMaterialPricingGroups.Name);
        //            emailSB.Append("</td>");
        //            emailSB.Append("<td style='height: 20px; border-left: solid 1px #D3D5C7; text-align: center;'>");
        //            emailSB.Append(dealsMaterialPricingGroupItem.Discount);
        //            emailSB.Append("</td>");
        //            emailSB.Append("</tr>");
        //        }
        //        else
        //        {
        //            emailSB.Append("<tr style='background-color: #E3E3E3;'>");
        //            emailSB.Append("<td>");
        //            emailSB.Append(selectedSAPMaterialPricingGroups.Name);
        //            emailSB.Append("</td>");
        //            emailSB.Append("<td style='height: 20px; border-left: solid 1px #D3D5C7; text-align: center;'>");
        //            emailSB.Append(dealsMaterialPricingGroupItem.Discount);
        //            emailSB.Append("</td>");
        //            emailSB.Append("</tr>");
        //        }
        //        count++;
        //    }
        //    emailSB.Append("</tbody></table>");

        //    emailSB.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"font-family: Arial, Tahoma, Verdana, Helvetica, sans-serif;font-size: 12px; margin-top: 20px; width: 350px; border: 1px solid;\">");
        //    emailSB.Append("<tbody>");
        //    emailSB.Append("<tr>");
        //    emailSB.Append("<th style=\"width: 140px; border-bottom: solid 1px #D3D5C7; background-color: #941800; color: #FFFFFF;\">Pricing Group</th>");
        //    emailSB.Append("<th style=\"width: 140px; border-bottom: solid 1px #D3D5C7; border-left: solid 1px #D3D5C7; background-color: #941800; color: #FFFFFF;\">Update ($)</th>");
        //    emailSB.Append("</tr>");


        //    this.SAPPricingGroups = (from co in db.SAPCharacteristicOption where co.SAPCharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.PricingGroup select co).ToList();

        //    int count1 = 0;
        //    foreach (DealsPricingGroup dealsPricingGroupItem in dealsPricingGroup)
        //    {
        //        var selectedSAPPricingGroups = SAPPricingGroups.Where(p => p.SAPCharacteristicOptionID.ToInt() == dealsPricingGroupItem.ZR01.ToInt()).FirstOrDefault();

        //        if (count1 % 2 == 0)
        //        {
        //            emailSB.Append("<tr>");
        //            emailSB.Append("<td>");
        //            emailSB.Append(selectedSAPPricingGroups.Name);
        //            emailSB.Append("</td>");
        //            emailSB.Append("<td style='height: 20px; border-left: solid 1px #D3D5C7; text-align: center;'>");
        //            if (strDealType == Enums.DealType.Override.ToString())
        //            {
        //                emailSB.Append("<input name=" + dealsPricingGroupItem.SAPCharacteristicOption.SAPCode + "Check id=" + dealsPricingGroupItem.SAPCharacteristicOption.SAPCode + "Check value=\"CK\" type=\"checkbox\" class=\"pricingCheckbox\" checked=\"checked\" disabled=\"disabled\" />");
        //            }
        //            else
        //            {
        //                emailSB.Append(dealsPricingGroupItem.Discount);
        //            }
        //            emailSB.Append("</td>");
        //            emailSB.Append("</tr>");
        //        }
        //        else
        //        {
        //            emailSB.Append("<tr style='background-color: #E3E3E3;'>");
        //            emailSB.Append("<td>");
        //            emailSB.Append(selectedSAPPricingGroups.Name);
        //            emailSB.Append("</td>");
        //            emailSB.Append("<td style='height: 20px; border-left: solid 1px #D3D5C7; text-align: center;'>");
        //            if (strDealType == Enums.DealType.Override.ToString())
        //            {
        //                emailSB.Append("<input name=" + dealsPricingGroupItem.SAPCharacteristicOption.SAPCode + "Check id=" + dealsPricingGroupItem.SAPCharacteristicOption.SAPCode + "Check value=\"CK\" type=\"checkbox\" class=\"pricingCheckbox\" checked=\"checked\" disabled=\"disabled\" />");
        //            }
        //            else
        //            {
        //                emailSB.Append(dealsPricingGroupItem.Discount);
        //            }
        //            emailSB.Append("</td>");
        //            emailSB.Append("</tr>");
        //        }
        //        count1++;
        //    }

        //    emailSB.Append("</tbody></table></td></tr></tbody></table>");
        //    emailSB.Append("</div>");

        //    emailSB.Append("</div>");

        //    return emailSB.ToString();
        //}

        //public string generateSoldToShipToHTML(List<SAPShipToSelectedClass> SAPShipToSelected)
        //{
        //    string strHTML = "";
        //    int? tempSoldToID = 0;
        //    int? intSoldToIDAlreadyDisplayeed = 0;
        //    int? intCheckOddEvenNumber = 0;

        //    if (SAPShipToSelected != null && SAPShipToSelected.Count > 0)
        //    {
        //        for (int i = 0; i < SAPShipToSelected.Count; i++)
        //        {
        //            if (tempSoldToID == SAPShipToSelected[i].selectedSoldToIDColumn)
        //            {
        //                if (intCheckOddEvenNumber % 2 == 0)
        //                {
        //                    intCheckOddEvenNumber += 1;
        //                    strHTML = strHTML + "<tr><td>" + SAPShipToSelected[i].selectedShipToNameColumn + "</td></tr>";
        //                }
        //                else
        //                {
        //                    intCheckOddEvenNumber += 1;
        //                    strHTML = strHTML + "<tr style='background-color: #E3E3E3;'><td>" + SAPShipToSelected[i].selectedShipToNameColumn + "</td></tr>";
        //                }
        //            }
        //            else
        //            {
        //                intSoldToIDAlreadyDisplayeed = 0;
        //                if (intSoldToIDAlreadyDisplayeed == 0)
        //                {
        //                    intCheckOddEvenNumber += 2;

        //                    if (intCheckOddEvenNumber % 2 == 0)
        //                    {
        //                        intCheckOddEvenNumber += 1;
        //                        strHTML = strHTML + "<tr style='background-color: #A4D1FF;'><td>" + SAPShipToSelected[i].selectedSoldToNameColumn + "</td></tr><tr><td>" + SAPShipToSelected[i].selectedShipToNameColumn + "</td></tr>";
        //                    }
        //                    else
        //                    {
        //                        intCheckOddEvenNumber += 1;
        //                        strHTML = strHTML + "<tr style='background-color: #A4D1FF;'><td>" + SAPShipToSelected[i].selectedSoldToNameColumn + "</td></tr><tr style='background-color: #E3E3E3;'><td>" + SAPShipToSelected[i].selectedShipToNameColumn + "</td></tr>";
        //                    }

        //                    intSoldToIDAlreadyDisplayeed = 1;
        //                }
        //            }

        //            tempSoldToID = SAPShipToSelected[i].selectedSoldToIDColumn;
        //        }
        //    }
        //    return SAPShipToSelected == null ? string.Empty : strHTML;

        //}
    }

    //public class DealPricingConditionList
    //{
    //    public string itemNumberField;
    //    public string conditionTypeField;
    //    public float rateField;
    //    public float pricePerField;
    //    public string pricePerUnitField;
    //}
}
