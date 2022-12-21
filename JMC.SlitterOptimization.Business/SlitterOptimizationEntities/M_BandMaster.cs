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
    public partial class M_BandMaster
    {
        public static DbSet<M_BandMaster> MBandMaster { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bi in db.MBandMasters select bi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateMaterialNumbers = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandMaster zGetSosBandMaster = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandMaster();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosBandMasterResponse getSosBandMasterResponse = sapPortalService.ZGetSosBandMasterAsync(zGetSosBandMaster);
            sapPortalService.Close();

            MBandMaster.FromSqlRaw("DELETE FROM [M_BandMaster]");

            //db.ExecuteStoreCommand("DELETE FROM [M_BandMaster]");

            returnedCount = getSosBandMasterResponse.EtSosBandMaster.Count();

            List<M_BandMaster> bandMasters = (from bm in db.MBandMasters select bm).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosBandMaster zstSosBandMaster in getSosBandMasterResponse.EtSosBandMaster)
            {
                //TODO -- FIX LOGIC IN SAP TO TAKE EVERYTHING AFTER THE '-'.
                string bandGrade = zstSosBandMaster.MaterialNumber.Contains('-') ? zstSosBandMaster.MaterialNumber.Substring(zstSosBandMaster.MaterialNumber.IndexOf('-') + 1) : string.Empty;

                M_BandMaster bandMaster = (from bm in bandMasters where bm.BandItem == zstSosBandMaster.MaterialNumber select bm).FirstOrDefault();

                if (bandMaster.IsNull())
                {
                    bandMaster = new M_BandMaster();

                    bandMaster.BandItem = zstSosBandMaster.MaterialNumber;

                    bandMasters.Add(bandMaster);
                    db.MBandMasters.Add(bandMaster);

                    insertedCount++;
                }
                else
                {
                    duplicateMaterialNumbers.Add(zstSosBandMaster.MaterialNumber + " " + zstSosBandMaster.Grade);
                }

                bandMaster.BandGrade = bandMaster.BandGrade.jIsEmpty() ? bandGrade : bandMaster.BandGrade;
                bandMaster.BandGaugeCode = bandMaster.BandGaugeCode.jIsEmpty() ? zstSosBandMaster.GaugeCode.ToString() : bandMaster.BandGaugeCode;
                bandMaster.BandGauge = bandMaster.BandGauge <= 0 ? zstSosBandMaster.Gauge.ToDecimal() : bandMaster.BandGauge;
                bandMaster.BandWidth = bandMaster.BandWidth <= 0 ? zstSosBandMaster.Width.ToDecimal() : bandMaster.BandWidth;
                bandMaster.MinNom = bandMaster.MinNom.jIsEmpty() ? (zstSosBandMaster.MaterialNumber.ToLower().Contains("min") ? "MIN" : (zstSosBandMaster.MaterialNumber.ToLower().Contains("nom") ? "NOM" : string.Empty)) : bandMaster.MinNom;
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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Band Master Results", emailStringBuilder.ToString());
        }
    }
}
