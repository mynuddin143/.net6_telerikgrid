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
    public partial class M_FamilySchedule
    {
        public static DbSet<M_FamilySchedule> MFamilySchedule { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from fs in db.MFamilySchedules select fs).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosFamilySchedule getSosFamilySchedule = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosFamilySchedule();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosFamilyScheduleResponse getSosFamilyScheduleResponse = sapPortalService.ZGetSosFamilyScheduleAsync(getSosFamilySchedule);
            sapPortalService.Close();

            MFamilySchedule.FromSqlRaw("DELETE FROM [M_FamilySchedule]");

            //db.ExecuteStoreCommand("DELETE FROM [M_FamilySchedule]");

            returnedCount = getSosFamilyScheduleResponse.EtSosFamilySchedule.Count();

            List<M_FamilySchedule> familySchedules = (from fs in db.MFamilySchedules select fs).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosTubeSubgrade zstSosTubeSubgrade in getSosFamilyScheduleResponse.EtSosFamilySchedule)
            {
                M_FamilySchedule familySchedule = new M_FamilySchedule();

                familySchedule.Plant = zstSosTubeSubgrade.Plant;
                familySchedule.BundlingWorkCenter = zstSosTubeSubgrade.BundlingWorkCenter;
                familySchedule.Family = zstSosTubeSubgrade.Family;
                familySchedule.FinishDate = zstSosTubeSubgrade.FinishDate;
                familySchedule.Quantity = zstSosTubeSubgrade.Quantity;
                familySchedule.UnitOfMeasure = zstSosTubeSubgrade.UnitMeasure;
                familySchedule.SlitComplete = zstSosTubeSubgrade.SlitComplete.ToBool();
                familySchedule.RollComplete = zstSosTubeSubgrade.RollComplete.ToBool();

                familySchedules.Add(familySchedule);
                db.MFamilySchedules.Add(familySchedule);

                insertedCount++;
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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Family Schedule Results", emailStringBuilder.ToString());
        }
    }
}
