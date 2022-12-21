using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    public partial class M_BandDeliveries
    {
        public static DbSet<M_BandDeliveries> MBandDeliveries { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bd in db.MBandDeliveries select bd).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandDeliveries getSosBandDeliveries = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandDeliveries();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandDeliveriesResponse getSosBandDeliveriesResponse = sapPortalService.ZGetSosBandDeliveriesAsync(getSosBandDeliveries);
            sapPortalService.Close();

            MBandDeliveries.FromSqlRaw("DELETE FROM [M_BandDeliveries]");

            //db.ExecuteStoreCommand("DELETE FROM [M_BandDeliveries]");

            returnedCount = getSosBandDeliveriesResponse.EtSosBandDeliveries.Count();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosBandDeliveries zstSosBandDelivery in getSosBandDeliveriesResponse.EtSosBandDeliveries)
            {
                M_BandDeliveries bandDelivery = new M_BandDeliveries();

                bandDelivery.DeliveryNo = zstSosBandDelivery.DeliveryNumber.ToInt();
                bandDelivery.Item = zstSosBandDelivery.MaterialNumber;
                bandDelivery.AvailableDate = zstSosBandDelivery.DeliveryDate.ToDate();
                bandDelivery.Vendor = zstSosBandDelivery.Plant;
                bandDelivery.OpenTons = zstSosBandDelivery.GrossWeight * (decimal)0.0005;

                switch (zstSosBandDelivery.SoldTo)
                {
                    case "0000002478":
                        bandDelivery.DestWarehouse = "CHIC";
                        break;

                    case "0000002831":
                        bandDelivery.DestWarehouse = "HARR";
                        break;

                    case "0000003056":
                        bandDelivery.DestWarehouse = "BLYT";
                        break;

                    case "0000006421":
                        bandDelivery.DestWarehouse = "DETR";
                        break;
                }

                db.MBandDeliveries.Add(bandDelivery);

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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Band Delivery Results", emailStringBuilder.ToString());
        }
    }
}
