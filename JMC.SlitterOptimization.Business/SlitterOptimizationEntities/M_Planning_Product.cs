using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using System.Collections;
using JMC.Portal.Business.HSSPortalAPOSlitter;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business {
	public partial class M_Planning_Product {

        public static DbSet<M_Planning_Product> MPlanningProduct { get; set; } = null!;

        public static void RefreshFromAtlasSAP(string email) {
			SlitterOptimizationContext db = new SlitterOptimizationContext();

			int deletedCount = (from rsi in db.MPlanningProducts select rsi).Count();
			int returnedCount = 0;
			int insertedCount = 0;

			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			StringBuilder emailStringBuilder = new StringBuilder();

			ZWS_HSS_PORTAL_APO_SLITTERClient sapPortalService = new ZWS_HSS_PORTAL_APO_SLITTERClient("HSS_PORTAL_APO_SLITTER");
			sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPAPOUserName"];
			sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPAPOPassword"];

			
			ZfmPlanningProductExtract getplanningProucts = new ZfmPlanningProductExtract();
			getplanningProucts.ItPlant = new List<ZST_PLANT>().ToArray();
			/*
			List<ZstPlant> plants = new List<ZstPlant>();
			
			ZstPlant plant = new ZstPlant();
			plant.Werks = "CHIC";
			plants.Add(plant);
			
			getSosapoSlitPattern.ItPlant = plants.ToArray();
			*/
			sapPortalService.Open();
			ZfmPlanningProductExtractResponse getPlanningProductsResponse = sapPortalService.ZfmPlanningProductExtractAsync(getplanningProucts);
			sapPortalService.Close();

            MPlanningProduct.FromSqlRaw("DELETE FROM [M_Planning_Product]");

            //db.ExecuteStoreCommand("DELETE FROM [M_Planning_Product]");

			returnedCount = getPlanningProductsResponse.EtPlanningprod.Count();

			List<M_Planning_Product> planningproducts = (from e in db.MPlanningProducts select e).ToList();

			foreach (Zplanningproduct zplanningproduct in getPlanningProductsResponse.EtPlanningprod) {
					M_Planning_Product planningproduct= new M_Planning_Product();

					planningproduct.Plant = zplanningproduct.Plant;
					planningproduct.ResourceName = zplanningproduct.ResourceName;
					planningproduct.PlanningMaterial = zplanningproduct.PlanningMaterial;
					planningproduct.Family = zplanningproduct.Family;
					planningproduct.Diameter = zplanningproduct.Diameter;
					planningproduct.TubeSize1 = zplanningproduct.TubeSize1;
					planningproduct.TubeSize2 = zplanningproduct.TubeSize2;
					planningproduct.Shape = zplanningproduct.Shape;
					planningproduct.Gauge = zplanningproduct.Gauge;
					planningproduct.GradeGroup = zplanningproduct.GradeGroup;
					planningproduct.RunRate = zplanningproduct.RunRate;
					planningproduct.PrimSlitcoil = zplanningproduct.PrimSlitcoil;
					planningproduct.Loss = zplanningproduct.Loss;
					planningproduct.PrimMastercoil = zplanningproduct.PrimMastercoil;
					planningproduct.LastBlockAllowed = zplanningproduct.LastBlockAllowed;
					planningproduct.Cycletime = zplanningproduct.Cycletime.ToDecimal();

				planningproducts.Add(planningproduct);
				db.MPlanningProducts.Add(planningproduct);

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

			Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Planning Products Extract Results", emailStringBuilder.ToString());
		}

	}
}
