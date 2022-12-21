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
    public partial class M_MillWarehouse
    {
        public static DbSet<M_MillWarehouse> MMillWarehouse { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from mw in db.MMillWarehouses select mw).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateWarehouses = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosMillWarehouse getSosMillWarehouse = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosMillWarehouse();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosMillWarehouseResponse getSosMillWarehouseResponse = sapPortalService.ZGetSosMillWarehouseAsync(getSosMillWarehouse);
            sapPortalService.Close();

            MMillWarehouse.FromSqlRaw("DELETE FROM [M_MillWarehouse]");

            //db.ExecuteStoreCommand("DELETE FROM [M_MillWarehouse]");

            returnedCount = getSosMillWarehouseResponse.EtSosMillWarehouse.Count();

            List<M_MillWarehouse> millWarehouses = (from mw in db.MMillWarehouses select mw).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosMillWarehouse zstSosMillWarehouse in getSosMillWarehouseResponse.EtSosMillWarehouse)
            {
                M_MillWarehouse millWarehouse = (from mw in millWarehouses where mw.Plant == zstSosMillWarehouse.Plant && mw.Dept == zstSosMillWarehouse.WorkCenter && mw.Warehouse == zstSosMillWarehouse.StorageLocation select mw).FirstOrDefault();

                if (millWarehouse.IsNull())
                {
                    millWarehouse = new M_MillWarehouse();

                    millWarehouse.Plant = zstSosMillWarehouse.Plant;
                    millWarehouse.Dept = zstSosMillWarehouse.WorkCenter;
                    millWarehouse.Warehouse = zstSosMillWarehouse.StorageLocation;

                    millWarehouses.Add(millWarehouse);
                    db.MMillWarehouses.Add(millWarehouse);

                    insertedCount++;
                }
                else
                {
                    duplicateWarehouses.Add(zstSosMillWarehouse.Plant + " " + zstSosMillWarehouse.WorkCenter + " " + zstSosMillWarehouse.StorageLocation);
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
            emailStringBuilder.Append("<br /><br /><br />Duplicate Warehouses:<br /><br />");

            foreach (string warehouse in duplicateWarehouses)
            {
                emailStringBuilder.Append(warehouse + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Mill Warehouse Results", emailStringBuilder.ToString());
        }
    }
}
