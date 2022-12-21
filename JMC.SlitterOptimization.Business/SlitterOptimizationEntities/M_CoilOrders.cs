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
    public partial class M_CoilOrders
    {
        public static DbSet<M_CoilOrders> MCoilOrders { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from co in db.MCoilOrders select co).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateMaterialNumbers = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilOrders getSosCoilOrders = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilOrders();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilOrdersResponse getSosCoilOrdersResponse = sapPortalService.ZGetSosCoilOrdersAsync(getSosCoilOrders);
            sapPortalService.Close();

            MCoilOrders.FromSqlRaw("DELETE FROM [M_CoilOrders]");

            //db.ExecuteStoreCommand("DELETE FROM [M_CoilOrders]");

            returnedCount = getSosCoilOrdersResponse.EtSosCoilOrders.Count();

            List<M_CoilOrders> coilOrders = (from so in db.MCoilOrders select so).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosCoilOrders zstSosCoilOrders in getSosCoilOrdersResponse.EtSosCoilOrders)
            {
                M_CoilOrders coilOrder = (from so in coilOrders where so.CoilItem == zstSosCoilOrders.MaterialNumber && so.PONum == zstSosCoilOrders.PoNumber && so.CurReqDate == zstSosCoilOrders.DeliveryDate select so).FirstOrDefault();

                if (coilOrder.IsNull())
                {
                    coilOrder = new M_CoilOrders();

                    coilOrder.CoilItem = zstSosCoilOrders.MaterialNumber;
                    coilOrder.PONum = zstSosCoilOrders.PoNumber;
                    coilOrder.CurReqDate = zstSosCoilOrders.DeliveryDate.ToDate();

                    coilOrders.Add(coilOrder);
                    db.MCoilOrders.Add(coilOrder);

                    insertedCount++;
                }
                else
                {
                    duplicateMaterialNumbers.Add(zstSosCoilOrders.MaterialNumber + " " + zstSosCoilOrders.PoNumber + " " + zstSosCoilOrders.DeliveryDate);
                }

                coilOrder.Plant = coilOrder.Plant.jIsEmpty() ? zstSosCoilOrders.Plant : coilOrder.Plant;
                coilOrder.Vendor = coilOrder.Vendor.jIsEmpty() ? zstSosCoilOrders.Vendor : coilOrder.Vendor;
                coilOrder.Domestic = coilOrder.Domestic.jIsEmpty() ? (zstSosCoilOrders.CertificateOfOrigin == zstSosCoilOrders.CountryOfIssue) ? "Y" : string.Empty : coilOrder.Domestic;
                coilOrder.AvailableDate = coilOrder.AvailableDate.IsNullOrMin() ? zstSosCoilOrders.DeliveryDate.ToDate() : coilOrder.AvailableDate;
                coilOrder.Tons = coilOrder.Tons <= 0 ? (zstSosCoilOrders.ScheduledQuantity - zstSosCoilOrders.QuantityOfGoodsReceived) * (decimal)0.0005 : coilOrder.Tons;
                coilOrder.Warehouse = coilOrder.Warehouse.jIsEmpty() ? zstSosCoilOrders.StorageLocation : coilOrder.Warehouse;
                coilOrder.OrigReqDate = coilOrder.OrigReqDate.IsNullOrMin() ? zstSosCoilOrders.OriginalRequestedDate.ToNullableDate() : coilOrder.OrigReqDate;
                coilOrder.OrderTons = coilOrder.OrderTons <= 0 ? zstSosCoilOrders.ScheduledQuantity * (decimal)0.0005 : coilOrder.OrderTons;
                coilOrder.Component = zstSosCoilOrders.Component.jIsEmpty() ? "" : zstSosCoilOrders.Component;
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

            foreach (string materialNumber in duplicateMaterialNumbers)
            {
                emailStringBuilder.Append(materialNumber + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Coil Orders Results", emailStringBuilder.ToString());
        }
    }
}
