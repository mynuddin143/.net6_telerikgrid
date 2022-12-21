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
    public partial class M_SteelOrders
    {
        public static DbSet<M_SteelOrders> MSteelOrders { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from so in db.MSteelOrders select so).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateMaterialNumbers = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosSteelOrders getSosSteelOrders = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosSteelOrders();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosSteelOrdersResponse getSosSteelOrdersResponse = sapPortalService.ZGetSosSteelOrdersAsync(getSosSteelOrders);
            sapPortalService.Close();

            MSteelOrders.FromSqlRaw("DELETE FROM [M_SteelOrders]");

            //db.ExecuteStoreCommand("DELETE FROM [M_SteelOrders]");

            returnedCount = getSosSteelOrdersResponse.EtSosSteelOrders.Count();

            List<M_SteelOrders> steelOrders = (from so in db.MSteelOrders select so).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosSteelOrders zstSosSteelOrders in getSosSteelOrdersResponse.EtSosSteelOrders)
            {
                M_SteelOrders steelOrder = (from so in steelOrders where so.BandItem == zstSosSteelOrders.MaterialNumber && so.PONum == zstSosSteelOrders.PoNumber && so.CurReqDate == zstSosSteelOrders.DeliveryDate select so).FirstOrDefault();

                if (steelOrder.IsNull())
                {
                    steelOrder = new M_SteelOrders();

                    steelOrder.BandItem = zstSosSteelOrders.MaterialNumber;
                    steelOrder.PONum = zstSosSteelOrders.PoNumber;
                    steelOrder.CurReqDate = zstSosSteelOrders.DeliveryDate;
                    // INC320910
                    steelOrder.Tons = steelOrder.Tons <= 0 ? (zstSosSteelOrders.ScheduledQuantity - zstSosSteelOrders.QuantityOfGoodsReceived) * (decimal)0.0005 : steelOrder.Tons;
                    steelOrder.OrderTons = zstSosSteelOrders.ScheduledQuantity * (decimal)0.0005;
                    //ends
                    steelOrders.Add(steelOrder);
                    db.MSteelOrders.Add(steelOrder);

                    insertedCount++;
                }
                else
                {

                    // INC320910
                    steelOrder.Tons += (zstSosSteelOrders.ScheduledQuantity - zstSosSteelOrders.QuantityOfGoodsReceived) * (decimal)0.0005;
                    steelOrder.OrderTons += zstSosSteelOrders.ScheduledQuantity * (decimal)0.0005;
                    //Ends
                    duplicateMaterialNumbers.Add(zstSosSteelOrders.MaterialNumber + " " + zstSosSteelOrders.PoNumber + " " + zstSosSteelOrders.DeliveryDate);
                }

                steelOrder.Plant = steelOrder.Plant.jIsEmpty() ? zstSosSteelOrders.Plant : steelOrder.Plant;
                steelOrder.Vendor = steelOrder.Vendor.jIsEmpty() ? zstSosSteelOrders.Vendor : steelOrder.Vendor;
                steelOrder.Domestic = steelOrder.Domestic.jIsEmpty() ? (zstSosSteelOrders.CertificateOfOrigin == zstSosSteelOrders.CountryOfIssue) ? "Y" : string.Empty : steelOrder.Domestic;
                steelOrder.AvailableDate = steelOrder.AvailableDate.IsNullOrMin() ? zstSosSteelOrders.DeliveryDate.ToDate() : steelOrder.AvailableDate;
                //steelOrder.Tons = steelOrder.Tons <= 0 ? (zstSosSteelOrders.ScheduledQuantity - zstSosSteelOrders.QuantityOfGoodsReceived) * (decimal)0.0005 : steelOrder.Tons;
                steelOrder.Warehouse = steelOrder.Warehouse.jIsEmpty() ? zstSosSteelOrders.StorageLocation : steelOrder.Warehouse;
                steelOrder.OrigReqDate = steelOrder.OrigReqDate.IsNullOrMin() ? zstSosSteelOrders.OriginalRequestedDate.ToNullableDate() : steelOrder.OrigReqDate;
                //steelOrder.OrderTons = steelOrder.OrderTons <= 0 ? zstSosSteelOrders.ScheduledQuantity * (decimal)0.0005 : steelOrder.OrderTons;
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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Steel Orders Results", emailStringBuilder.ToString());
        }
    }
}
