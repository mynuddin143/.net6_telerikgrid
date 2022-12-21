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
    public partial class M_CoilMaster
    {
        public static DbSet<M_CoilMaster> MCoilMaster { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from bi in db.MCoilMasters select bi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();
            ArrayList duplicateSlitCoils = new ArrayList();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilMaster zGetSosCoilMaster = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilMaster();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosCoilMasterResponse getSosCoilMasterResponse = sapPortalService.ZGetSosCoilMasterAsync(zGetSosCoilMaster);
            sapPortalService.Close();

            MCoilMaster.FromSqlRaw("DELETE FROM [M_CoilMaster]");

            //db.ExecuteStoreCommand("DELETE FROM [M_CoilMaster]");

            returnedCount = getSosCoilMasterResponse.EtSosCoilMaster.Count();

            List<M_CoilMaster> coilMasters = (from cm in db.MCoilMasters select cm).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosCoilMaster zstSosCoilMaster in getSosCoilMasterResponse.EtSosCoilMaster)
            {
                M_CoilMaster coilMaster = (from cm in coilMasters where cm.CoilItem == zstSosCoilMaster.SlitCoil && cm.TubeGrade == zstSosCoilMaster.Grade && cm.CoilGaugeCode == zstSosCoilMaster.GaugeCode.ToString() && cm.TubeFamily == zstSosCoilMaster.Family select cm).FirstOrDefault();

                if (coilMaster.IsNull())
                {
                    coilMaster = new M_CoilMaster();

                    coilMaster.CoilItem = zstSosCoilMaster.SlitCoil;
                    coilMaster.TubeGrade = zstSosCoilMaster.Grade;
                    coilMaster.CoilGaugeCode = zstSosCoilMaster.GaugeCode.ToString();
                    coilMaster.TubeFamily = zstSosCoilMaster.Family;

                    coilMasters.Add(coilMaster);
                    db.MCoilMasters.Add(coilMaster);

                    insertedCount++;
                }
                else
                {
                    duplicateSlitCoils.Add(zstSosCoilMaster.SlitCoil + " " + zstSosCoilMaster.Grade);
                }

                coilMaster.CoilGauge = coilMaster.CoilGauge <= 0 ? zstSosCoilMaster.Gauge.ToDecimal() : coilMaster.CoilGauge;
                coilMaster.CoilWidth = coilMaster.CoilWidth <= 0 ? zstSosCoilMaster.Width.ToDecimal() : coilMaster.CoilWidth;
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
            emailStringBuilder.Append("<br /><br /><br />Duplicate Slit Coils:<br /><br />");

            foreach (string slitCoil in duplicateSlitCoils)
            {
                emailStringBuilder.Append(slitCoil + "<br />");
            }

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Coil Master Results", emailStringBuilder.ToString());
        }
    }
}
