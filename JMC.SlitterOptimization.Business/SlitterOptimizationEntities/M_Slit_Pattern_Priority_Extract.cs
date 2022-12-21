using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.HSSPortalAPOSlitter;
using System.Configuration;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using JMC.Portal.Business;

namespace JMC.SlitterOptimization.Business {
	public partial class M_Slit_Pattern_Priority_Extract {
        public static DbSet<M_Slit_Pattern_Priority_Extract> MSlitPatternPriorityExtract { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email) {
			SlitterOptimizationContext db = new SlitterOptimizationContext();

			int deletedCount = (from e in db.MSlitPatternPriorityExtracts select e).Count();
			int returnedCount = 0;
			int insertedCount = 0;

			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			StringBuilder emailStringBuilder = new StringBuilder();

			ZWS_HSS_PORTAL_APO_SLITTERClient sapPortalService = new ZWS_HSS_PORTAL_APO_SLITTERClient("HSS_PORTAL_APO_SLITTER");
			sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPAPOUserName"];
			sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPAPOPassword"];

			ZfmGetSosapoSlitPattern getSosapoSlitPattern = new ZfmGetSosapoSlitPattern();
			getSosapoSlitPattern.ItPlant = new List<ZstPlant>().ToArray();
			/*
			List<ZstPlant> plants = new List<ZstPlant>();
			
			ZstPlant plant = new ZstPlant();
			plant.Werks = "CHIC";
			plants.Add(plant);
			
			getSosapoSlitPattern.ItPlant = plants.ToArray();
			*/
			sapPortalService.Open();
			ZfmGetSosapoSlitPatternResponse getSosapoSlitPatternResponse = sapPortalService.ZfmGetSosapoSlitPatternAsync(getSosapoSlitPattern);
			sapPortalService.Close();

            MSlitPatternPriorityExtract.FromSqlRaw("DELETE FROM [M_Slit_Pattern_Priority_Extract]");

            //db.ExecuteStoreCommand("DELETE FROM [M_Slit_Pattern_Priority_Extract]");

			returnedCount = getSosapoSlitPatternResponse.EtSosapoSlitPattern.Count();

			List<M_Slit_Pattern_Priority_Extract> slitPatterns = (from e in db.MSlitPatternPriorityExtracts select e).ToList();

			foreach (ZatesSlitPatt zatesSlitPatt in getSosapoSlitPatternResponse.EtSosapoSlitPattern) {
				M_Slit_Pattern_Priority_Extract slitPattern = new M_Slit_Pattern_Priority_Extract();

				slitPattern.PrimSlitCoil = zatesSlitPatt.PrimSlitCoil;
				slitPattern.Plant = zatesSlitPatt.Plant;
				slitPattern.Priority = (Int16)zatesSlitPatt.Priority.ToInt();
				slitPattern.MasterCoil = zatesSlitPatt.MasterCoil;
				slitPattern.NumberOfCuts = zatesSlitPatt.NumberOfCuts.IsNull() || zatesSlitPatt.NumberOfCuts.jIsEmpty() ? "0" : zatesSlitPatt.NumberOfCuts;
				slitPattern.DropFamily = zatesSlitPatt.DropFamily;
				slitPattern.DropFamilyGauge = zatesSlitPatt.DropFamilyGaug;
				slitPattern.CutsOnDrop = zatesSlitPatt.CutsOnDrop.IsNull() || zatesSlitPatt.CutsOnDrop.jIsEmpty() ? "0" : zatesSlitPatt.CutsOnDrop;
				slitPattern.SlabInd = zatesSlitPatt.SlabInd;
				slitPattern.SlabGroup = zatesSlitPatt.SlabGrpInd;
				slitPattern.SlabGroupDesc = zatesSlitPatt.GroupDesc;
				slitPattern.OptimizerFamily = zatesSlitPatt.OptimizerFamily;

				slitPatterns.Add(slitPattern);
				db.MSlitPatternPriorityExtracts.Add(slitPattern);

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

			Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas SOS Slit Pattern Priority Results", emailStringBuilder.ToString());
		}
	}
}
