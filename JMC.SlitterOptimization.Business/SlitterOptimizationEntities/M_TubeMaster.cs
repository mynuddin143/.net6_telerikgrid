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
    public partial class M_TubeMaster
    {
        public static DbSet<M_TubeMaster> MTubeMaster { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bi in db.MTubeMasters select bi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicatePlanningMaterials = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubeMaster getSosTubeMaster = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubeMaster();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubeMasterResponse getSosTubeMasterResponse = sapPortalService.ZGetSosTubeMasterAsync(getSosTubeMaster);
            sapPortalService.Close();

            MTubeMaster.FromSqlRaw("DELETE FROM [M_TubeMaster]");

            //db.ExecuteStoreCommand("DELETE FROM [M_TubeMaster]");

            returnedCount = getSosTubeMasterResponse.EtSosTubeMaster.Count();

            List<M_TubeMaster> tubeMasters = (from im in db.MTubeMasters select im).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosTubeMaster zstSosTubeMaster in getSosTubeMasterResponse.EtSosTubeMaster)
            {
                M_TubeMaster tubeMaster = (from tm in tubeMasters where tm.TubeItem == zstSosTubeMaster.PlanningMaterial select tm).FirstOrDefault();

                if (tubeMaster.IsNull())
                {
                    tubeMaster = new M_TubeMaster();

                    tubeMaster.TubeItem = zstSosTubeMaster.PlanningMaterial;
                    tubeMaster.TubePerimeter = zstSosTubeMaster.Perimeter.ToDecimal();
                    tubeMaster.TubeFamily = zstSosTubeMaster.Family;
                    tubeMaster.TubeGaugeCode = zstSosTubeMaster.GaugeCode.ToString();
                    tubeMaster.TubeGrade = zstSosTubeMaster.Grade;
                    tubeMaster.TubeWeightPerFoot = zstSosTubeMaster.WeightPerFoot.ToDecimal();
                    tubeMaster.TubeDesc = zstSosTubeMaster.Description;

                    tubeMasters.Add(tubeMaster);
                    db.MTubeMasters.Add(tubeMaster);

                    insertedCount++;
                }
                else
                {
                    duplicatePlanningMaterials.Add(zstSosTubeMaster.PlanningMaterial);
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
            emailStringBuilder.Append("<br /><br /><br />Duplicate Planning Materials:<br /><br />");

            foreach (string planningMaterial in duplicatePlanningMaterials)
            {
                emailStringBuilder.Append(planningMaterial + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Tube Master Results", emailStringBuilder.ToString());
        }
    }
}
