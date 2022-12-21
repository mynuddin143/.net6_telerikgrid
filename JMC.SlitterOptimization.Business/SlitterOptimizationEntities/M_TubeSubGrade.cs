using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using System.Collections;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    public partial class M_TubeSubGrade
    {
        public static DbSet<M_TubeSubGrade> MTubeSubGrade { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from tsg in db.MTubeSubGrades select tsg).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubeSubGrade getSosTubeSubGrade = new JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubeSubGrade();

            sapPortalService.Open();
            JMC.Portal.Business.AtlasSAPPortal.ZGetSosTubeSubGradeResponse getSosTubeSubGradeResponse = sapPortalService.ZGetSosTubeSubGradeAsync(getSosTubeSubGrade);
            sapPortalService.Close();

            MTubeSubGrade.FromSqlRaw("DELETE FROM [M_TubeSubGrade]");

            //db.ExecuteStoreCommand("DELETE FROM [M_TubeSubGrade]");

            returnedCount = getSosTubeSubGradeResponse.EtSosTubeSubGrade.Count();

            List<M_TubeSubGrade> tubeSubGrades = (from tsg in db.MTubeSubGrades select tsg).ToList();

            foreach (JMC.Portal.Business.AtlasSAPPortal.ZstSosTubeSubGrade zstSosTubeSubGrade in getSosTubeSubGradeResponse.EtSosTubeSubGrade)
            {
                M_TubeSubGrade tubeSubGrade = new M_TubeSubGrade();

                tubeSubGrade.TubeGrade = zstSosTubeSubGrade.TubeGrade;
                tubeSubGrade.TubeSubGrade = zstSosTubeSubGrade.TubeSubGrade;

                tubeSubGrades.Add(tubeSubGrade);
                db.MTubeSubGrades.Add(tubeSubGrade);

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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Tube Sub Grade Results", emailStringBuilder.ToString());
        }
    }
}
