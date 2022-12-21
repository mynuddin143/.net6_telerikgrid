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
    public partial class M_ItemMaster
    {
        public static DbSet<M_ItemMaster> MItemMaster { get; set; } = null!;


        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bi in db.MItemMasters select bi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicatePlanningMaterials = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosItemMaster getSosItemMaster = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosItemMaster();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosItemMasterResponse getSosItemMasterResponse = sapPortalService.ZGetSosItemMasterAsync(getSosItemMaster);
            sapPortalService.Close();

            MItemMaster.FromSqlRaw("DELETE FROM [M_ItemMaster]");

            //db.ExecuteStoreCommand("DELETE FROM [M_ItemMaster]");

            returnedCount = getSosItemMasterResponse.EtSosItemMaster.Count();

            List<M_ItemMaster> itemMasters = (from im in db.MItemMasters select im).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosItemMaster zstSosItemMaster in getSosItemMasterResponse.EtSosItemMaster)
            {
                M_ItemMaster itemMaster = (from im in itemMasters where im.Plant == zstSosItemMaster.Plant && im.Routing == zstSosItemMaster.BundlingWorkCenter && im.TubeItem == zstSosItemMaster.PlanningMaterial && im.BOMCoilItem == zstSosItemMaster.SlitCoil select im).FirstOrDefault();

                if (itemMaster.IsNull())
                {
                    itemMaster = new M_ItemMaster();

                    itemMaster.TubeSurface = string.Empty;

                    itemMaster.Plant = zstSosItemMaster.Plant;
                    itemMaster.Routing = zstSosItemMaster.BundlingWorkCenter;
                    itemMaster.TubeItem = zstSosItemMaster.PlanningMaterial;
                    itemMaster.BOMCoilItem = zstSosItemMaster.SlitCoil;

                    itemMasters.Add(itemMaster);
                    db.MItemMasters.Add(itemMaster);

                    insertedCount++;
                }
                else
                {
                    duplicatePlanningMaterials.Add(zstSosItemMaster.Plant + " " + zstSosItemMaster.BundlingWorkCenter + " " + zstSosItemMaster.PlanningMaterial + " " + zstSosItemMaster.SlitCoil);
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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Item Master Results", emailStringBuilder.ToString());
        }
    }
}
