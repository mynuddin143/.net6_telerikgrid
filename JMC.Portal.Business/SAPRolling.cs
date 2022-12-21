using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.HSSPortalSales;
using System.Configuration;
using System.Collections;
using JMC.Portal.Business.HSSPortalAPOSales;
using ZstMaterialNumber = JMC.Portal.Business.HSSPortalSales.ZstMaterialNumber;
using ZstPlantNumber = JMC.Portal.Business.HSSPortalSales.ZstPlantNumber;

namespace JMC.Portal.Business
{
    public partial class SAPRolling
    {
        public static IHSSPortalRolling[] GetByMaterialGroupFromAtlasSAP(string SAPMaterialGroupCode)
        {
            return SAPRolling.GetByMaterialGroupFromAtlasSAP(SAPMaterialGroupCode, new string[] { });
        }

        public static IHSSPortalRolling[] GetByMaterialGroupFromAtlasSAP(string SAPMaterialGroupCode, string[] plantCodes, string grade = "")
        {
            ZWS_HSS_PORTAL_SALESClient portalSalesService = new ZWS_HSS_PORTAL_SALESClient("HSS_PORTAL_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            ZstMaterialNumber[] materialNumbers = new ZstMaterialNumber[] { };
            //ArrayList plantNumberArrayList = new ArrayList();
            ArrayList additionalGradesArrayList = new ArrayList();

            //foreach (var plantCode in plantCodes) {
            //  ZstPlantNumber zstPlantNumber = new ZstPlantNumber();
            //  zstPlantNumber.PlantNumber = plantCode;
            //  plantNumberArrayList.Add(zstPlantNumber);

            //  if (zstPlantNumber.PlantNumber == "HARR") {
            //    ZstPlantNumber detroitZSTPlantNumber = new ZstPlantNumber();
            //    detroitZSTPlantNumber.PlantNumber = "DETR";
            //    plantNumberArrayList.Add(detroitZSTPlantNumber);
            //  }
            //}

            List<string> plantCodes2 = plantCodes.ToList();
            if (plantCodes2.Contains("HARR") && !plantCodes2.Contains("DETR"))
            {
                plantCodes2.Add("DETR");
            }

            grade = grade.TrimNull().ToUpper();
            if (grade == "CSA" || grade == "C") grade = "CSA";

            if (grade == "0")
            { //ALL
                additionalGradesArrayList.Add("CSA");  // These are the original
                additionalGradesArrayList.Add("A513"); // 
                additionalGradesArrayList.Add("AY50"); //
            }
            else
                if (grade != string.Empty && grade != "A")
                {
                    additionalGradesArrayList.Add(grade);
                }

            ZfmGetHssPortalRollings getHssPortalRollings = new ZfmGetHssPortalRollings();
            getHssPortalRollings.ImMaterialGroup = SAPMaterialGroupCode;
            getHssPortalRollings.ItPlantNumbers = (from x in plantCodes2.Distinct() select new ZstPlantNumber() { PlantNumber = x }).ToArray();
            getHssPortalRollings.ItAdditionalGrades = (string[])additionalGradesArrayList.ToArray(typeof(string));
            getHssPortalRollings.ImIncludeClosed = "X";

            portalSalesService.Open();
            ZfmGetHssPortalRollingsResponse getHssPortalRollingsResponse = portalSalesService.ZfmGetHssPortalRollingsAsync(getHssPortalRollings);
            portalSalesService.Close();

            if (grade == "A")
            {
                getHssPortalRollingsResponse.EtHssPortalRollings = getHssPortalRollingsResponse.EtHssPortalRollings
                    .Where(x => !x.MaterialNumber.Contains("CSA"))
                    .Where(x => !x.MaterialNumber.Contains("AY50"))
                    .Where(x => !x.MaterialNumber.Contains("A513"))
                    .ToArray();
            }

            if (grade == "AY50" || grade == "A513" || grade == "CSA")
            {
                getHssPortalRollingsResponse.EtHssPortalRollings = getHssPortalRollingsResponse.EtHssPortalRollings.Where(x => x.MaterialNumber.Contains(grade)).ToArray();
            }

            return (IHSSPortalRolling[])getHssPortalRollingsResponse.EtHssPortalRollings;
        }

        public static IHSSPortalAPORolling[] GetByMaterialGroupFromAtlasSAPAPO(string SAPMaterialGroupCode)
        {
            return SAPRolling.GetByMaterialGroupFromAtlasSAPAPO(SAPMaterialGroupCode, new string[] { });
        }

        public static IHSSPortalAPORolling[] GetByMaterialGroupFromAtlasSAPAPO(string SAPMaterialGroupCode, string[] plantCodes, string grade = "")
        {
            HSSPortalAPOSales.ZWS_HSS_PORTAL_APO_SALESClient portalSalesService = new HSSPortalAPOSales.ZWS_HSS_PORTAL_APO_SALESClient("HSS_PORTAL_APO_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPAPOUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPAPOPassword"];

            HSSPortalAPOSales.ZstMaterialNumber[] materialNumbers = new HSSPortalAPOSales.ZstMaterialNumber[] { };
            //ArrayList plantNumberArrayList = new ArrayList();
            ArrayList additionalGradesArrayList = new ArrayList();

            //foreach (var plantCode in plantCodes) {
            //  ZstPlantNumber zstPlantNumber = new ZstPlantNumber();
            //  zstPlantNumber.PlantNumber = plantCode;
            //  plantNumberArrayList.Add(zstPlantNumber);

            //  if (zstPlantNumber.PlantNumber == "HARR") {
            //    ZstPlantNumber detroitZSTPlantNumber = new ZstPlantNumber();
            //    detroitZSTPlantNumber.PlantNumber = "DETR";
            //    plantNumberArrayList.Add(detroitZSTPlantNumber);
            //  }
            //}

            List<string> plantCodes2 = plantCodes.ToList();
            // Comment this code for PLYM Reorg
            //if (plantCodes2.Contains("HARR") && !plantCodes2.Contains("DETR")) {
            //  plantCodes2.Add("DETR");
            //}
            //End
            grade = grade.TrimNull().ToUpper();
            if (grade == "C") grade = "CSA";

            if (grade == "0")
            { //ALL
                string[] rollingGrades = ConfigurationManager.AppSettings["RollingGrades"].Split(',');

                foreach (string rollingGrade in rollingGrades)
                {
                    HSSPortalAPOSales.ZatasGradeGrp zatasGradeGrp = new HSSPortalAPOSales.ZatasGradeGrp();
                    zatasGradeGrp.Grade = rollingGrade;
                    additionalGradesArrayList.Add(zatasGradeGrp);
                }
            }
            else if (grade != string.Empty)
            {
                HSSPortalAPOSales.ZatasGradeGrp zatasGradeGrp = new HSSPortalAPOSales.ZatasGradeGrp();
                zatasGradeGrp.Grade = grade;
                additionalGradesArrayList.Add(zatasGradeGrp);
            }

            HSSPortalAPOSales.ZatasGradeGrp[] grades = (HSSPortalAPOSales.ZatasGradeGrp[])additionalGradesArrayList.ToArray(typeof(HSSPortalAPOSales.ZatasGradeGrp));

            HSSPortalAPOSales.ZfmGetHssPortalapoRollings getHssPortalRollings = new HSSPortalAPOSales.ZfmGetHssPortalapoRollings();
            getHssPortalRollings.ImMaterialGroup = SAPMaterialGroupCode;
            getHssPortalRollings.ItPlantNumbers = (from x in plantCodes2.Distinct() select new HSSPortalAPOSales.ZstPlantNumber() { PlantNumber = x }).ToArray();
            getHssPortalRollings.ItGrades = grades;
            //getHssPortalRollings.ImIncludeClosed = "X";

            portalSalesService.Open();
            HSSPortalAPOSales.ZfmGetHssPortalapoRollingsResponse getHssPortalRollingsResponse = portalSalesService.ZfmGetHssPortalapoRollingsAsync(getHssPortalRollings);
            portalSalesService.Close();

            if (grade != "0")
            {
                //getHssPortalRollingsResponse.EtHssPortalRollings = getHssPortalRollingsResponse.EtHssPortalRollings.Where(x => x.MaterialNumber.Contains(grade)).ToArray();
            }

            return (IHSSPortalAPORolling[])getHssPortalRollingsResponse.EtHssPortalRollings;
        }

        public static HSSPortalAPOSales.ZppPortalRolling[] GetWTCSAPRollings(List<string> planningMaterial, List<string> plantCodes)
        {
            HSSPortalAPOSales.ZWS_HSS_PORTAL_APO_SALESClient portalSalesService = new HSSPortalAPOSales.ZWS_HSS_PORTAL_APO_SALESClient("HSS_PORTAL_APO_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPAPOUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPAPOPassword"];


            HSSPortalAPOSales.ZfmGetWhRolling getWTPortalRollings = new HSSPortalAPOSales.ZfmGetWhRolling();
            getWTPortalRollings.ItLocno = plantCodes.ToArray();

            //List<string> listShapeR = new List<string>();
            //HSSPortalAPOSales.ZppShapeR shapeR = new HSSPortalAPOSales.ZppShapeR() {Low="Round",Option="EQ",Sign="I" };
            //listShapeR.Add("ROUND");
            //getWTPortalRollings.ItShape = listShapeR.ToArray();

            List<string> listPlanMaterial = new List<string>();
            // listPlanMaterial.Add("R02375135-C1012H");            
            getWTPortalRollings.ItPlanmat = planningMaterial.ToArray();
            //List<HSSPortalAPOSales.ZppShapeR> listShapeR = new List<HSSPortalAPOSales.ZppShapeR>();
            //HSSPortalAPOSales.ZppShapeR shapeR;
            HSSPortalAPOSales.ZfmGetWhRollingRequest rollingrequest = new HSSPortalAPOSales.ZfmGetWhRollingRequest(getWTPortalRollings);
            portalSalesService.Open();
            HSSPortalAPOSales.ZfmGetWhRollingResponse zfmRollingResponse = portalSalesService.ZfmGetWhRollingAsync(getWTPortalRollings);
            HSSPortalAPOSales.ZppPortalRolling[] rollings = zfmRollingResponse.EtRollingTab;
            //return testRollings.ToArray();
            //getWTPortalRollings.ItShape 
            portalSalesService.Close();
            return rollings;
        }

        //new method based on material numbers.
        public static IHSSPortalAPORolling[] GetByMaterialNumbersFromAtlasSAPAPO(string[] materialNumbers, string[] plantCodes, string[] grade)
        {
            var portalSalesService = new HSSPortalAPOSales.ZWS_HSS_PORTAL_APO_SALESClient("HSS_PORTAL_APO_SALES");
            portalSalesService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPAPOUserName"];
            portalSalesService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPAPOPassword"];

            var zstMaterialNumbers = new List<HSSPortalAPOSales.ZstMaterialNumber> { };
            zstMaterialNumbers.AddRange(materialNumbers.Select(materialNumber => new HSSPortalAPOSales.ZstMaterialNumber
            {
                MaterialNumber =  materialNumber
            }));
            var additionalGradesArrayList = new List<HSSPortalAPOSales.ZatasGradeGrp>();
            var rollingGrades = ConfigurationManager.AppSettings["RollingGrades"].Split(',');
            if (grade.Any())
            {
                rollingGrades = grade;
            }
            additionalGradesArrayList.AddRange(rollingGrades.Select(rollingGrade => new HSSPortalAPOSales.ZatasGradeGrp {Grade = rollingGrade}));
            var getHssPortalRollings = new HSSPortalAPOSales.ZfmGetHssPortalapoRollings
            {
                ItPlantNumbers = (from x in plantCodes.Distinct() select new HSSPortalAPOSales.ZstPlantNumber() { PlantNumber = x }).ToArray(),
                ItMaterialNumbers = (from x in zstMaterialNumbers.Distinct() select x).ToArray(),
                ItGrades = (from x in additionalGradesArrayList.Distinct() select x).ToArray(),
            };
            portalSalesService.Open();
            var getHssPortalRollingsResponse = portalSalesService.ZfmGetHssPortalapoRollingsAsync(getHssPortalRollings);
            portalSalesService.Close();
            return (IHSSPortalAPORolling[])getHssPortalRollingsResponse.EtHssPortalRollings;
        }
    }
}
