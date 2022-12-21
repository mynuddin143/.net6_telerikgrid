using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    public partial class M_TandemSchedule
    {
        public static DbSet<M_TandemSchedule> MTandemSchedule { get; set; } = null!;


        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from ts in db.MTandemSchedules select ts).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateMaterialNumbers = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTandemSchedule getSosTandemSchedule = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosTandemSchedule();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTandemScheduleResponse getSosTandemScheduleResponse = sapPortalService.ZGetSosTandemScheduleAsync(getSosTandemSchedule);
            sapPortalService.Close();

            MTandemSchedule.FromSqlRaw("DELETE FROM [M_TandemSchedule]");
            
            //db.ExecuteStoreCommand("DELETE FROM [M_TandemSchedule]");

            returnedCount = getSosTandemScheduleResponse.EtSosTandemSchedule.Count();

            List<M_TandemSchedule> tandemSchedules = (from ts in db.MTandemSchedules select ts).ToList();

            bool insert = false;

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosTandemSchedule zstSosTandemSchedule in getSosTandemScheduleResponse.EtSosTandemSchedule)
            {
                if (!zstSosTandemSchedule.SlitCoil.IsNull() && !(zstSosTandemSchedule.SlitCoil.ToUpper() == "TUBINGSCRAP"))
                {
                    insert = false;
                    M_TandemSchedule tandemSchedule = (from ts in tandemSchedules where ts.Plant == zstSosTandemSchedule.Plant && ts.Dept == zstSosTandemSchedule.BundlingWorkCenter && ts.TubeItem == zstSosTandemSchedule.MaterialNumber && ts.FirstStartDate.ToString() == zstSosTandemSchedule.FinishDate select ts).FirstOrDefault();

                    if (tandemSchedule.IsNull())
                    {
                        tandemSchedule = new M_TandemSchedule();

                        tandemSchedule.Plant = zstSosTandemSchedule.Plant;
                        tandemSchedule.Dept = zstSosTandemSchedule.BundlingWorkCenter;
                        tandemSchedule.TubeItem = zstSosTandemSchedule.MaterialNumber;
                       // tandemSchedule.FirstStartDate = zstSosTandemSchedule.FinishDate;
                        tandemSchedule.Family = zstSosTandemSchedule.Family;

                        insert = true;

                        insertedCount++;
                    }
                    else
                    {
                        duplicateMaterialNumbers.Add(zstSosTandemSchedule.MaterialNumber + " " + zstSosTandemSchedule.Plant + " " + zstSosTandemSchedule.BundlingWorkCenter + " " + zstSosTandemSchedule.FinishDate);
                    }

                    tandemSchedule.Lbs = tandemSchedule.Lbs <= 0 ? (zstSosTandemSchedule.Source == "PLN" ? zstSosTandemSchedule.PlannedQuantity : zstSosTandemSchedule.PlannedQuantity - zstSosTandemSchedule.ReceivedQuantity) : tandemSchedule.Lbs;
                    tandemSchedule.Tons = tandemSchedule.Tons <= 0 ? (zstSosTandemSchedule.Source == "PLN" ? zstSosTandemSchedule.PlannedQuantity * (decimal)0.0005 : (zstSosTandemSchedule.PlannedQuantity - zstSosTandemSchedule.ReceivedQuantity) * (decimal)0.0005) : tandemSchedule.Tons;

                    //if (tandemSchedule.Lbs >= 9999999999) {
                    //	insert = false;
                    //}

                    if (insert)
                    {
                        tandemSchedules.Add(tandemSchedule);
                        db.MTandemSchedules.Add(tandemSchedule);
                    }

                    tandemSchedule.Source = tandemSchedule.Source.jIsEmpty() ? zstSosTandemSchedule.Source : tandemSchedule.Source;
                    tandemSchedule.OverrideCoil = tandemSchedule.OverrideCoil.jIsEmpty() ? zstSosTandemSchedule.SlitCoil : tandemSchedule.OverrideCoil;
                }
            }

            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append(deletedCount);
            emailStringBuilder.Append(" deleted.<br />");
            emailStringBuilder.Append(returnedCount);
            emailStringBuilder.Append(" returned from SAP.<br />");
            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");

            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds");
            emailStringBuilder.Append("<br /><br /><br />Duplicate Material Numbers:<br /><br />");

            foreach (string materialNumber in duplicateMaterialNumbers)
            {
                emailStringBuilder.Append(materialNumber + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Tandem Schedule Results", emailStringBuilder.ToString());
        }
    }
}
