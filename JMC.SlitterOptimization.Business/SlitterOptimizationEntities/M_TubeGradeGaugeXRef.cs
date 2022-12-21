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
    public partial class M_TubeGradeGaugeXRef
    {
        public static DbSet<M_TubeGradeGaugeXRef> MTubeGradeGaugeXRef { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from tsg in db.MTubeGradeGaugeXrefs select tsg).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubegradeGaugeXref getSosTubegradeGaugeXref = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubegradeGaugeXref();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubegradeGaugeXrefResponse getSosTubegradeGaugeXrefResponse = sapPortalService.ZGetSosTubegradeGaugeXrefAsync(getSosTubegradeGaugeXref);
            sapPortalService.Close();

            MTubeGradeGaugeXRef.FromSqlRaw("DELETE FROM [M_TubeGradeGaugeXRef]");

            //db.ExecuteStoreCommand("DELETE FROM [M_TubeGradeGaugeXRef]");

            returnedCount = getSosTubegradeGaugeXrefResponse.EtSosTubegradeGaugeXref.Count();

            List<M_TubeGradeGaugeXRef> tubeGradeGaugeXRefs = (from tsg in db.MTubeGradeGaugeXrefs select tsg).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosTubegradeGaugeXref zstSosTubegradeGaugeXref in getSosTubegradeGaugeXrefResponse.EtSosTubegradeGaugeXref)
            {
                M_TubeGradeGaugeXRef tubeGradeGaugeXRef = new M_TubeGradeGaugeXRef();

                tubeGradeGaugeXRef.TubeGrade = zstSosTubegradeGaugeXref.TubeGrade;
                tubeGradeGaugeXRef.GaugeCode = zstSosTubegradeGaugeXref.GaugeCode;
                tubeGradeGaugeXRef.BandGrade = zstSosTubegradeGaugeXref.BandGrade;
                tubeGradeGaugeXRef.BandMinNom = zstSosTubegradeGaugeXref.BandMinNom;
                tubeGradeGaugeXRef.BandMinGauge = zstSosTubegradeGaugeXref.BandMinGauge;
                tubeGradeGaugeXRef.BandMaxGauge = zstSosTubegradeGaugeXref.BandMaxGauge;

                tubeGradeGaugeXRefs.Add(tubeGradeGaugeXRef);
                db.MTubeGradeGaugeXrefs.Add(tubeGradeGaugeXRef);

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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Tube Grade Gauge XRef Results", emailStringBuilder.ToString());
        }
    }
}
