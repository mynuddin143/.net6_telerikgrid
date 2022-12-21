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
    public partial class M_CoilInventory
    {
        public static DbSet<M_CoilInventory> MCoilInventory { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bi in db.MCoilInventories select bi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateBatchNumbers = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilInventory getSosCoilInventory = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilInventory();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilInventoryResponse getSosCoilInventoryResponse = sapPortalService.ZGetSosCoilInventoryAsync(getSosCoilInventory);
            sapPortalService.Close();

            MCoilInventory.FromSqlRaw("DELETE FROM [M_CoilInventory]");

            //db.ExecuteStoreCommand("DELETE FROM [M_CoilInventory]");

            returnedCount = getSosCoilInventoryResponse.EtSosCoilInventory.Count();

            List<M_CoilInventory> coilInventories = (from ci in db.MCoilInventories select ci).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosCoilInventory zstSosCoilInventory in getSosCoilInventoryResponse.EtSosCoilInventory)
            {
                M_CoilInventory coilInventory = (from ci in coilInventories where ci.TrackID == zstSosCoilInventory.BatchNumber select ci).FirstOrDefault();

                if (coilInventory.IsNull())
                {
                    coilInventory = new M_CoilInventory();

                    coilInventory.TrackID = zstSosCoilInventory.BatchNumber;
                    coilInventory.OriginalSource = string.Empty;
                    coilInventory.AllocSequence = 0;

                    coilInventories.Add(coilInventory);
                    db.MCoilInventories.Add(coilInventory);

                    insertedCount++;
                }
                else
                {
                    duplicateBatchNumbers.Add(zstSosCoilInventory.BatchNumber);
                }

                coilInventory.Plant = coilInventory.Plant.jIsEmpty() ? zstSosCoilInventory.Plant : coilInventory.Plant;
                coilInventory.CoilItem = coilInventory.CoilItem.jIsEmpty() ? zstSosCoilInventory.MaterialNumber : coilInventory.CoilItem;
                coilInventory.Vendor = coilInventory.Vendor.jIsEmpty() ? zstSosCoilInventory.Vendor : coilInventory.Vendor;
                coilInventory.Tons = coilInventory.Tons <= 0 ? zstSosCoilInventory.Weight * (decimal)0.0005 : coilInventory.Tons;
                coilInventory.Warehouse = coilInventory.Warehouse.jIsEmpty() ? zstSosCoilInventory.StorageLocation : coilInventory.Warehouse;
                coilInventory.Domestic = coilInventory.Domestic.jIsEmpty() ? zstSosCoilInventory.CountryOfOrigin : coilInventory.Domestic;
                coilInventory.HeatNumber = coilInventory.HeatNumber.jIsEmpty() ? zstSosCoilInventory.HeatNumber : coilInventory.HeatNumber;
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
            emailStringBuilder.Append("<br /><br /><br />Duplicate Batch Numbers:<br /><br />");

            foreach (string batchNumber in duplicateBatchNumbers)
            {
                emailStringBuilder.Append(batchNumber + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Coil Inventory Results", emailStringBuilder.ToString());
        }
    }
}
