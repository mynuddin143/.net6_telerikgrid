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
    public partial class M_SlitOrders
    {
        public static DbSet<M_SlitOrders> MSlitOrders { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bi in db.MSlitOrders select bi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList nullMaterialNumbers = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosSlitOrders getSosSlitOrders = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosSlitOrders();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosSlitOrdersResponse getSosSlitOrdersResponse = sapPortalService.ZGetSosSlitOrdersAsync(getSosSlitOrders);
            sapPortalService.Close();

            MSlitOrders.FromSqlRaw("DELETE FROM [M_SlitOrders]");

            //db.ExecuteStoreCommand("DELETE FROM [M_SlitOrders]");

            List<M_SlitOrders> slitOrders = (from so in db.MSlitOrders select so).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosSlitOrders zstSosSlitOrders in getSosSlitOrdersResponse.EtSosSlitOrders)
            {
                M_SlitOrders slitOrder = null;

                int orderNumber = zstSosSlitOrders.OrderNumber.ToInt();

                slitOrder = (from so in slitOrders where so.MONum == orderNumber && so.BandItem == zstSosSlitOrders.BandItem && so.CoilItem == zstSosSlitOrders.CoilItem select so).FirstOrDefault();

                if (slitOrder.IsNull())
                {
                    slitOrder = new M_SlitOrders();

                    slitOrder.MOrder = (insertedCount + 1).ToString();
                    slitOrder.MONum = orderNumber;

                    slitOrder.BandItem = zstSosSlitOrders.BandItem;
                    slitOrder.CoilItem = zstSosSlitOrders.CoilItem;
                    slitOrder.Cuts = 1;
                    slitOrder.OrderTons = (zstSosSlitOrders.OrderItemQuantity - zstSosSlitOrders.QuantityReceived) * (decimal)0.0005;

                    slitOrders.Add(slitOrder);
                    db.MSlitOrders.Add(slitOrder);

                    insertedCount++;
                }
                else
                {
                    slitOrder.Cuts++;
                    slitOrder.OrderTons += (zstSosSlitOrders.OrderItemQuantity - zstSosSlitOrders.QuantityReceived) * (decimal)0.0005;
                }

                slitOrder.Plant = slitOrder.Plant.jIsEmpty() ? zstSosSlitOrders.Plant : slitOrder.Plant;
                slitOrder.Workcenter = slitOrder.Workcenter.jIsEmpty() ? zstSosSlitOrders.WorkCenter : slitOrder.Workcenter;
                slitOrder.ReceivingLocation = slitOrder.ReceivingLocation.jIsEmpty() ? zstSosSlitOrders.StorageLocation : slitOrder.ReceivingLocation;
                slitOrder.DueDate = slitOrder.DueDate.IsNullOrMin() ? zstSosSlitOrders.ScheduledFinish.ToDate() : slitOrder.DueDate;
                slitOrder.MOStatus = "ORD";
            }

            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            returnedCount = insertedCount;

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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Slit Orders Results", emailStringBuilder.ToString());
        }
    }
}
