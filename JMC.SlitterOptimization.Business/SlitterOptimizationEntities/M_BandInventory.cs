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
    public partial class M_BandInventory
    {
        public static DbSet<M_BandInventory> MBandInventory { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bi in db.MBandInventories select bi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateBatchNumbers = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandInventory getSosBandInventory = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandInventory();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandInventoryResponse getSosBandInventoryResponse = sapPortalService.ZGetSosBandInventoryAsync(getSosBandInventory);
            sapPortalService.Close();

            MBandInventory.FromSqlRaw("DELETE FROM [M_BandInventory]");

            //db.ExecuteStoreCommand("DELETE FROM [M_BandInventory]");

            returnedCount = getSosBandInventoryResponse.EtSosBandInventory.Count();

            List<M_BandInventory> bandInventories = (from bi in db.MBandInventories select bi).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosBandInventory zstSosBandInventory in getSosBandInventoryResponse.EtSosBandInventory)
            {
                string batchNumber = zstSosBandInventory.BatchNumber.Trim();

                if (!batchNumber.jIsEmpty())
                {
                    M_BandInventory bandInventory = (from bi in bandInventories where bi.TrackID == batchNumber select bi).FirstOrDefault();

                    if (bandInventory.IsNull())
                    {
                        bandInventory = new M_BandInventory();

                        bandInventory.TrackID = batchNumber;
                        bandInventory.OriginalSource = string.Empty;
                        bandInventory.AllocSequence = 0;
                        bandInventory.SlitOrder = string.Empty;
                        bandInventory.ReservedBand = false;
                        bandInventory.InStock = 0;
                        bandInventory.OnOrder = 0;
                        bandInventory.AvailableLeadTimeDays = 0;
                        bandInventory.WarehouseLeadTimeDays = 0;
                        bandInventory.SlitterLeadTimeDays = 0;
                        bandInventory.LeadTimeDays = 0;

                        bandInventory.Plant = zstSosBandInventory.Plant;
                        bandInventory.BandItem = zstSosBandInventory.MaterialNumber;
                        bandInventory.Vendor = zstSosBandInventory.Vendor;
                        bandInventory.Tons = zstSosBandInventory.Weight * (decimal)0.0005;
                        bandInventory.Warehouse = zstSosBandInventory.StorageLocation;
                        bandInventory.Domestic = zstSosBandInventory.CountryOfOrigin;
                        bandInventory.HeatNumber = zstSosBandInventory.HeatNumber;

                        bandInventories.Add(bandInventory);
                        db.MBandInventories.Add(bandInventory);

                        insertedCount++;
                    }
                    else
                    {
                        duplicateBatchNumbers.Add(batchNumber);
                        bandInventory.Tons += zstSosBandInventory.Weight * (decimal)0.0005;

                        if (!zstSosBandInventory.HeatNumber.IsNull() && !zstSosBandInventory.HeatNumber.jIsEmpty() && (bandInventory.HeatNumber.IsNull() || bandInventory.HeatNumber.jIsEmpty()))
                        {
                            bandInventory.HeatNumber = zstSosBandInventory.HeatNumber;
                        }
                    }
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

            foreach (string materialNumber in duplicateBatchNumbers)
            {
                emailStringBuilder.Append(materialNumber + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Band Inventory Results", emailStringBuilder.ToString());
        }
    }
}
