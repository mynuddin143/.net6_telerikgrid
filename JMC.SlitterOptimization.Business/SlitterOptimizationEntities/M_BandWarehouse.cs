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
    public partial class M_BandWarehouse
    {
        public static DbSet<M_BandWarehouse> MBandWarehouse { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bw in db.MBandWarehouses select bw).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateWarehouses = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandWarehouse getSosBandWarehouse = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandWarehouse();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandWarehouseResponse getSosBandWarehouseResponse = sapPortalService.ZGetSosBandWarehouseAsync(getSosBandWarehouse);
            sapPortalService.Close();

            MBandWarehouse.FromSqlRaw("DELETE FROM [M_BandWarehouse]");

            //db.ExecuteStoreCommand("DELETE FROM [M_BandWarehouse]");

            returnedCount = getSosBandWarehouseResponse.EtSosBandWarehouse.Count();

            List<M_BandWarehouse> bandWarehouses = (from bw in db.MBandWarehouses select bw).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosBandWarehouse zstSosBandWarehouse in getSosBandWarehouseResponse.EtSosBandWarehouse)
            {
                M_BandWarehouse bandWarehouse = (from bw in bandWarehouses where bw.Plant == zstSosBandWarehouse.Plant && bw.Warehouse == zstSosBandWarehouse.StorageLocation select bw).FirstOrDefault();

                if (bandWarehouse.IsNull())
                {
                    bandWarehouse = new M_BandWarehouse();

                    bandWarehouse.Plant = zstSosBandWarehouse.Plant;
                    bandWarehouse.Warehouse = zstSosBandWarehouse.StorageLocation;

                    bandWarehouses.Add(bandWarehouse);
                    db.MBandWarehouses.Add(bandWarehouse);

                    insertedCount++;
                }
                else
                {
                    duplicateWarehouses.Add(zstSosBandWarehouse.Plant + " " + zstSosBandWarehouse.StorageLocation);
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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Band Warehouse Results", emailStringBuilder.ToString());
        }
    }
}
