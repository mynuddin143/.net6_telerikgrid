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
    public partial class M_ItemMasterOld
    {
        public static DbSet<M_ItemMasterOld> MItemMasterOld { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from tsg in db.MItemMasterOlds select tsg).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosItemMasterOld getSosItemMasterOld = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosItemMasterOld();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosItemMasterOldResponse getSosItemMasterOldResponse = sapPortalService.ZGetSosItemMasterOldAsync(getSosItemMasterOld);
            sapPortalService.Close();

            MItemMasterOld.FromSqlRaw("DELETE FROM [M_ItemMasterOld]");

            //db.ExecuteStoreCommand("DELETE FROM [M_ItemMasterOld]");

            returnedCount = getSosItemMasterOldResponse.EtSosItemstrOld.Count();

            List<M_ItemMasterOld> itemMasterOlds = (from tsg in db.MItemMasterOlds select tsg).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosItemMasterOld zstSosItemMasterOld in getSosItemMasterOldResponse.EtSosItemstrOld)
            {
                M_ItemMasterOld itemMasterOld = new M_ItemMasterOld();

                itemMasterOld.Plant = zstSosItemMasterOld.Plant;
                itemMasterOld.CurrentBOMCoilItem = zstSosItemMasterOld.CrntBomCoilItem;
                itemMasterOld.PreviousBOMCoilItem = zstSosItemMasterOld.PrevBomCoilItem;

                itemMasterOlds.Add(itemMasterOld);
                db.MItemMasterOlds.Add(itemMasterOld);

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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Item Master Old Results", emailStringBuilder.ToString());
        }
    }
}
