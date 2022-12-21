using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using System.Collections;
using JMC.Portal.Business.HSSPortalAPOSlitter;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    public partial class M_APO_Rolling_Schedule_Import
    {
        public static DbSet<M_APO_Rolling_Schedule_Import> MAPORollingScheduleImport { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email)
        {
            SlitterOptimizationContext db = new SlitterOptimizationContext();

            int deletedCount = (from rsi in db.MApoRollingScheduleImports select rsi).Count();
            int returnedCount = 0;
            int insertedCount = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_HSS_PORTAL_APO_SLITTERClient sapPortalService = new ZWS_HSS_PORTAL_APO_SLITTERClient("HSS_PORTAL_APO_SLITTER");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPAPOUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPAPOPassword"];

            ZfmGetSosapoTandemSchedule getSosapoTandemSchedule = new ZfmGetSosapoTandemSchedule();
            getSosapoTandemSchedule.ItPlant = new List<ZST_PLANT>().ToArray();
            /*
			List<ZstPlant> plants = new List<ZstPlant>();
			
			ZstPlant plant = new ZstPlant();
			plant.Werks = "CHIC";
			plants.Add(plant);
			
			getSosapoSlitPattern.ItPlant = plants.ToArray();
			*/
            sapPortalService.Open();
            ZfmGetSosapoTandemScheduleResponse getSosapoTandemScheduleResponse = sapPortalService.ZfmGetSosapoTandemScheduleAsync(getSosapoTandemSchedule);
            sapPortalService.Close();

            MAPORollingScheduleImport.FromSqlRaw("DELETE FROM [M_APO_Rolling_Schedule_Import]");

            //db.ExecuteStoreCommand("DELETE FROM [M_APO_Rolling_Schedule_Import]");

            returnedCount = getSosapoTandemScheduleResponse.EtSosapoTandemSchedule.Count();

            List<M_APO_Rolling_Schedule_Import> rollings = (from e in db.MApoRollingScheduleImports select e).ToList();

            foreach (ZatasSosTandemSchedule zatasSosTandemSchedule in getSosapoTandemScheduleResponse.EtSosapoTandemSchedule)
            {
                M_APO_Rolling_Schedule_Import rolling = new M_APO_Rolling_Schedule_Import();

                rolling.Plant = zatasSosTandemSchedule.Plant;
                rolling.BundlingW = zatasSosTandemSchedule.BundlingWorkCenter;
                rolling.MaterialNumber = zatasSosTandemSchedule.MaterialNumber;
                rolling.PlannedQuantity = zatasSosTandemSchedule.PlannedQuantity;
                rolling.FinishDat = zatasSosTandemSchedule.FinishDate.ToDate();
                rolling.SlitCoil = zatasSosTandemSchedule.SlitCoil;
                rolling.ReceivedQuantity = zatasSosTandemSchedule.ReceivedQuantity;
                rolling.Source = zatasSosTandemSchedule.Source;
                rolling.Family = zatasSosTandemSchedule.Family;
                rolling.BlockNumber = zatasSosTandemSchedule.BlockNumber;
                rolling.BFixed = zatasSosTandemSchedule.BFixed;
                rolling.Gauge = zatasSosTandemSchedule.Gauge;
                rolling.GradeGroup = zatasSosTandemSchedule.GradeGroup;
                rolling.CombFreeCap = zatasSosTandemSchedule.CombFreeCap;
                rolling.StockOrd = zatasSosTandemSchedule.StockOrd;
                rolling.StockReq = zatasSosTandemSchedule.StockReq;
                rolling.BlkHrs = zatasSosTandemSchedule.BlkHrs;
                rolling.StkLevel = zatasSosTandemSchedule.StkLevel.ToInt();
                rolling.PlanHrs = zatasSosTandemSchedule.PlanHrs;
                rolling.StkOrdHrs = zatasSosTandemSchedule.StkordHrs;
                rolling.BookHrs = zatasSosTandemSchedule.BookHrs;
                rolling.FamilyZpp01 = zatasSosTandemSchedule.Zpp01Fam;


                rollings.Add(rolling);
                db.MApoRollingScheduleImports.Add(rolling);

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

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS APO Rolling Schedule Import Results", emailStringBuilder.ToString());
        }
    }
}
