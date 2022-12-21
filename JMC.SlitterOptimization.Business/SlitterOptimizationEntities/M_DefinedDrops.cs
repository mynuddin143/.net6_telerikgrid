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
    public partial class M_DefinedDrops
    {
        public static DbSet<M_DefinedDrops> MDefinedDrops { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from tsg in db.MDefinedDrops select tsg).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosDefinedDrops getSosDefinedDrops = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosDefinedDrops();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosDefinedDropsResponse getSosDefinedDropsResponse = sapPortalService.ZGetSosDefinedDropsAsync(getSosDefinedDrops);
            sapPortalService.Close();

            MDefinedDrops.FromSqlRaw("DELETE FROM [M_DefinedDrops]");
            
            //db.ExecuteStoreCommand("DELETE FROM [M_DefinedDrops]");

            returnedCount = getSosDefinedDropsResponse.EtSosDefinedDrops.Count();

            List<M_DefinedDrops> definedDropses = (from tsg in db.MDefinedDrops select tsg).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosDefinedDrops zstSosDefinedDrops in getSosDefinedDropsResponse.EtSosDefinedDrops)
            {
                M_DefinedDrops definedDrops = new M_DefinedDrops();

                definedDrops.Plant = zstSosDefinedDrops.Werks;
                definedDrops.PrimaryFamily = zstSosDefinedDrops.PrimaryFamily;
                definedDrops.SecondaryFamily = zstSosDefinedDrops.SecondaryFamily;
                definedDrops.PrimaryCuts = zstSosDefinedDrops.PrimaryCuts;
                definedDrops.SecondaryCuts = zstSosDefinedDrops.SecondaryCuts;

                definedDropses.Add(definedDrops);
                db.MDefinedDrops.Add(definedDrops);

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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Defined Drops Results", emailStringBuilder.ToString());
        }
    }
}
