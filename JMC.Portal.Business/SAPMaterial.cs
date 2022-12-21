using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMC.Portal.Business.AtlasMasterData;
using System.Configuration;
using JMC.Portal.Business.WheatlandPortal;
using System.Net;
using JMC.Portal.Business.HSSPortalSales;

namespace JMC.Portal.Business
{
    public partial class SAPMaterial
    {
        public decimal? AreaPerFoot
        {
            get
            {
                if (this.SaptubeShapeID == (long)Enums.AtlasTubeShapes.Round)
                {
                    return ((22 * this.Diameter) / 7) / 12;
                }
                else
                {
                    return (2 * (this.Size + this.Size2)) / 12;
                }
            }
        }

        public int PiecesPerBundle
        {
            get
            {
                int factor = 1;

                if (Bundling1.HasValue && Bundling1.Value != 0)
                {
                    factor *= _Bundling1.Value;
                }

                if (Bundling2.HasValue && Bundling1.Value != 0)
                {
                    factor *= _Bundling2.Value;
                }

                if (factor == 1 && (BundlingRound ?? 0) != 0)
                {
                    factor *= BundlingRound.Value;
                }
                return factor;
            }
        }

        public bool IsRound
        {
            get
            {
                return (this.Diameter.HasValue && this.Diameter.Value > 0);
            }
        }

        public decimal? ConvertSQFRateInCWT(decimal sqfRate)
        {
            decimal? cwtRate = null;

            if (sqfRate > 0 && this.AreaPerFoot > 0 && this.WeightPerFoot > 0)
            {
                cwtRate = (sqfRate / this.WeightPerFoot) * this.AreaPerFoot * 100;
            }

            return cwtRate;
        }

        public static SAPMaterial Find(decimal[] sizes, decimal[] size2s, decimal gauge)
        {
            SAPMaterial foundSAPMaterial = new SAPMaterial();

            for (int i = 0; i < sizes.Length; i++)
            {
                decimal size = sizes[i];
                decimal size2 = size2s[i];

                PortalEntities db = new PortalEntities();

                foundSAPMaterial = (from m in db.Sapmaterials where m.Size == size && m.Size2 == size2 && m.GaugeRestrictable == gauge select m).FirstOrDefault();

                if (!foundSAPMaterial.IsNull())
                {
                    return foundSAPMaterial;
                }
            }

            return foundSAPMaterial ?? new SAPMaterial();
        }

        public static SAPMaterial Find(decimal[] diameters, decimal gauge)
        {
            SAPMaterial foundSAPMaterial = new SAPMaterial();

            for (int i = 0; i < diameters.Length; i++)
            {
                decimal diameter = diameters[i];

                PortalEntities db = new PortalEntities();

                foundSAPMaterial = (from m in db.Sapmaterials where m.Diameter == diameter && m.GaugeRestrictable == gauge select m).FirstOrDefault();

                if (!foundSAPMaterial.IsNull())
                {
                    return foundSAPMaterial;
                }
            }

            return foundSAPMaterial ?? new SAPMaterial();
        }

        public static string GetNPSDescription(Round round, Gauge gauge)
        {
            if (round.IsNull() || gauge.IsNull()) return "";

            SAPMaterial SAPMaterial = (from x in DB.GlobalCache.SAPMaterials where x.DivisionID == (long)Enums.Divisions.Atlas && x.Diameter.Value == round.Size && x.Npsdescription != null && x.Npsdescription != "" && x.GaugeRestrictable.HasValue && x.GaugeRestrictable.Value == gauge.Gauge1 && x.Configurable select x).FirstOrDefault();

            if (!SAPMaterial.IsNull())
            {
                return SAPMaterial.Npsdescription;
            }

            return "";
        }

        public static void RefreshFromAtlasSAP(string email)
        {
            RefreshCharacteristicsFromAtlasSAP();
            RefreshMaterialsFromAtlasSAP(email);
            RefreshBundlingFromAtlasSAP(email);
        }

        private static void RefreshCharacteristicsFromAtlasSAP()
        {
            PortalEntities db = new PortalEntities();

            ZWS_MASTER_DATAClient masterDataService = new ZWS_MASTER_DATAClient("ATLAS_ZWS_MASTER_DATA");
            masterDataService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            masterDataService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasMasterData.ZGetMaterialCharacteristics getMaterialCharacteristics = new JMC.Portal.Business.AtlasMasterData.ZGetMaterialCharacteristics();
            getMaterialCharacteristics.AlternateCoilInds = new ZstAlternateCoilInd[] { new ZstAlternateCoilInd() };
            getMaterialCharacteristics.EpoxycoatColors = new ZstEpoxycoatColor[] { new ZstEpoxycoatColor() };
            getMaterialCharacteristics.KleenkoteColors = new ZstKleenkoteColor[] { new ZstKleenkoteColor() };
            getMaterialCharacteristics.SalesInstructions = new ZstSalesInstruction[] { new ZstSalesInstruction() };
            getMaterialCharacteristics.Specifications = new JMC.Portal.Business.AtlasMasterData.ZstSpecification[] { new JMC.Portal.Business.AtlasMasterData.ZstSpecification() };
            getMaterialCharacteristics.TubeShapes = new ZstTubeShape[] { new ZstTubeShape() };
            getMaterialCharacteristics.TubeStandards = new ZstTubeStandard[] { new ZstTubeStandard() };
            getMaterialCharacteristics.MaterialPricingGroups = new ZstMaterialPricingGroup[] { new ZstMaterialPricingGroup() };
            getMaterialCharacteristics.PricingGroups = new ZstPricingGroup[] { new ZstPricingGroup() };
            getMaterialCharacteristics.MaterialGroups = new JMC.Portal.Business.AtlasMasterData.ZstMaterialGroup[] { new JMC.Portal.Business.AtlasMasterData.ZstMaterialGroup() };
            getMaterialCharacteristics.MaterialTypes = new JMC.Portal.Business.AtlasMasterData.ZstMaterialType[] { new JMC.Portal.Business.AtlasMasterData.ZstMaterialType() };

            masterDataService.Open();
            JMC.Portal.Business.AtlasMasterData.ZGetMaterialCharacteristicsResponse getMaterialCharacteristicsResponse = masterDataService.ZGetMaterialCharacteristicsAsync(getMaterialCharacteristics);
            masterDataService.Close();

            List<SAPCharacteristicOption> alternateCoilIndicators = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.AlternateCoilIndicator select co).ToList();
            List<SAPCharacteristicOption> epoxycoatColors = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.EpoxycoatColor select co).ToList();
            List<SAPCharacteristicOption> kleenkoteColors = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.KleenkoteColor select co).ToList();
            List<SAPCharacteristicOption> salesInstructions = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.SalesInstruction select co).ToList();
            List<SAPCharacteristicOption> specifications = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.Specification select co).ToList();
            List<SAPCharacteristicOption> tubeShapes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.TubeShape select co).ToList();
            List<SAPCharacteristicOption> tubeStandards = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.TubeStandard select co).ToList();
            List<SAPCharacteristicOption> materialPricingGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup select co).ToList();
            List<SAPCharacteristicOption> pricingGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.PricingGroup select co).ToList();
            List<SAPCharacteristicOption> materialGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup select co).ToList();
            List<SAPCharacteristicOption> materialTypes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialType select co).ToList();

            foreach (ZstAlternateCoilInd zstAlternateCoilInd in getMaterialCharacteristicsResponse.AlternateCoilInds)
            {
                if (!string.IsNullOrEmpty(zstAlternateCoilInd.Code) && !string.IsNullOrEmpty(zstAlternateCoilInd.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in alternateCoilIndicators where co.Sapcode == zstAlternateCoilInd.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.AlternateCoilIndicator;
                        alternateCoilIndicators.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstAlternateCoilInd.Code;
                    SAPCharacteristicOption.Name = zstAlternateCoilInd.Name;
                }
            }

            foreach (ZstEpoxycoatColor zstEpoxycoatColor in getMaterialCharacteristicsResponse.EpoxycoatColors)
            {
                if (!string.IsNullOrEmpty(zstEpoxycoatColor.Code) && !string.IsNullOrEmpty(zstEpoxycoatColor.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in epoxycoatColors where co.Sapcode == zstEpoxycoatColor.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.EpoxycoatColor;
                        epoxycoatColors.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstEpoxycoatColor.Code;
                    SAPCharacteristicOption.Name = zstEpoxycoatColor.Name;
                }
            }

            foreach (ZstKleenkoteColor zstKleenkoteColor in getMaterialCharacteristicsResponse.KleenkoteColors)
            {
                if (!string.IsNullOrEmpty(zstKleenkoteColor.Code) && !string.IsNullOrEmpty(zstKleenkoteColor.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in kleenkoteColors where co.Sapcode == zstKleenkoteColor.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.KleenkoteColor;
                        kleenkoteColors.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstKleenkoteColor.Code;
                    SAPCharacteristicOption.Name = zstKleenkoteColor.Name;
                }
            }

            foreach (ZstSalesInstruction zstSalesInstruction in getMaterialCharacteristicsResponse.SalesInstructions)
            {
                if (!string.IsNullOrEmpty(zstSalesInstruction.Code) && !string.IsNullOrEmpty(zstSalesInstruction.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in salesInstructions where co.Sapcode == zstSalesInstruction.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.SalesInstruction;
                        salesInstructions.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstSalesInstruction.Code;
                    SAPCharacteristicOption.Name = zstSalesInstruction.Name;
                }
            }

            foreach (JMC.Portal.Business.AtlasMasterData.ZstSpecification zstSpecification in getMaterialCharacteristicsResponse.Specifications)
            {
                if (!string.IsNullOrEmpty(zstSpecification.Code) && !string.IsNullOrEmpty(zstSpecification.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in specifications where co.Sapcode == zstSpecification.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.Specification;
                        specifications.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstSpecification.Code;
                    SAPCharacteristicOption.Name = zstSpecification.Name;
                }
            }

            foreach (ZstTubeShape zstTubeShape in getMaterialCharacteristicsResponse.TubeShapes)
            {
                if (!string.IsNullOrEmpty(zstTubeShape.Code) && !string.IsNullOrEmpty(zstTubeShape.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in tubeShapes where co.Sapcode == zstTubeShape.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.TubeShape;
                        tubeShapes.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstTubeShape.Code;
                    SAPCharacteristicOption.Name = zstTubeShape.Name;
                }
            }

            foreach (ZstTubeStandard zstTubeStandard in getMaterialCharacteristicsResponse.TubeStandards)
            {
                if (!string.IsNullOrEmpty(zstTubeStandard.Code) && !string.IsNullOrEmpty(zstTubeStandard.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in tubeStandards where co.Sapcode == zstTubeStandard.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.TubeStandard;
                        tubeStandards.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstTubeStandard.Code;
                    SAPCharacteristicOption.Name = zstTubeStandard.Name;
                }
            }

            foreach (ZstMaterialPricingGroup zstMaterialPricingGroup in getMaterialCharacteristicsResponse.MaterialPricingGroups)
            {
                if (!string.IsNullOrEmpty(zstMaterialPricingGroup.MaterialPricingGroupNumber) && !string.IsNullOrEmpty(zstMaterialPricingGroup.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in materialPricingGroups where co.Sapcode == zstMaterialPricingGroup.MaterialPricingGroupNumber select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup;
                        materialPricingGroups.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstMaterialPricingGroup.MaterialPricingGroupNumber;
                    SAPCharacteristicOption.Name = zstMaterialPricingGroup.Name;
                }
            }

            foreach (ZstPricingGroup zstPricingGroup in getMaterialCharacteristicsResponse.PricingGroups)
            {
                if (!string.IsNullOrEmpty(zstPricingGroup.PricingGroupNumber) && !string.IsNullOrEmpty(zstPricingGroup.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in pricingGroups where co.Sapcode == zstPricingGroup.PricingGroupNumber select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.PricingGroup;
                        SAPCharacteristicOption.Active = true;
                        SAPCharacteristicOption.SortOrder = 0;
                        pricingGroups.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstPricingGroup.PricingGroupNumber;
                    SAPCharacteristicOption.Name = zstPricingGroup.Name;
                }
            }

            foreach (JMC.Portal.Business.AtlasMasterData.ZstMaterialGroup zstMaterialGroup in getMaterialCharacteristicsResponse.MaterialGroups)
            {
                if (!string.IsNullOrEmpty(zstMaterialGroup.Code) && !string.IsNullOrEmpty(zstMaterialGroup.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in materialGroups where co.Sapcode == zstMaterialGroup.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup;
                        materialGroups.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstMaterialGroup.Code;
                    SAPCharacteristicOption.Name = zstMaterialGroup.Name;
                }
            }

            foreach (JMC.Portal.Business.AtlasMasterData.ZstMaterialType zstMaterialType in getMaterialCharacteristicsResponse.MaterialTypes)
            {
                if (!string.IsNullOrEmpty(zstMaterialType.Code) && !string.IsNullOrEmpty(zstMaterialType.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in materialTypes where co.Sapcode == zstMaterialType.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.AtlasSAPCharacteristicTypes.MaterialType;
                        materialTypes.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstMaterialType.Code;
                    SAPCharacteristicOption.Name = zstMaterialType.Name;
                }
            }

            db.SaveChanges();
        }

        private static void RefreshMaterialsFromAtlasSAP(string email)
        {
            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_MASTER_DATAClient masterDataService = new ZWS_MASTER_DATAClient("ATLAS_ZWS_MASTER_DATA");
            masterDataService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            masterDataService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            JMC.Portal.Business.AtlasMasterData.ZGetAllMaterials getAllMaterials = new JMC.Portal.Business.AtlasMasterData.ZGetAllMaterials();
            getAllMaterials.Materials = new JMC.Portal.Business.AtlasMasterData.ZstMaterial[] { new JMC.Portal.Business.AtlasMasterData.ZstMaterial() };

            masterDataService.Open();
            JMC.Portal.Business.AtlasMasterData.ZGetAllMaterialsResponse getAllMaterialsForSaleResponse = masterDataService.ZGetAllMaterialsAsync(getAllMaterials);
            masterDataService.Close();

            List<SAPCharacteristicOption> alternateCoilIndicators = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.AlternateCoilIndicator select co).ToList();
            List<SAPCharacteristicOption> epoxycoatColors = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.EpoxycoatColor select co).ToList();
            List<SAPCharacteristicOption> kleenkoteColors = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.KleenkoteColor select co).ToList();
            List<SAPCharacteristicOption> salesInstructions = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.SalesInstruction select co).ToList();
            List<SAPCharacteristicOption> specifications = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.Specification select co).ToList();
            List<SAPCharacteristicOption> tubeShapes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.TubeShape select co).ToList();
            List<SAPCharacteristicOption> tubeStandards = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.TubeStandard select co).ToList();
            List<SAPCharacteristicOption> materialPricingGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup select co).ToList();
            List<SAPCharacteristicOption> pricingGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.PricingGroup select co).ToList();
            List<SAPCharacteristicOption> materialGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup select co).ToList();
            List<SAPCharacteristicOption> materialTypes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialType select co).ToList();
            List<SAPMaterial> materials = (from m in db.Sapmaterials where m.DivisionID == (long)Enums.Divisions.Atlas select m).ToList();

            foreach (JMC.Portal.Business.AtlasMasterData.ZstMaterial zstMaterial in getAllMaterialsForSaleResponse.Materials)
            {
                SAPMaterial SAPMaterial = (from m in materials where m.Number == zstMaterial.MaterialNumber select m).FirstOrDefault();
                SAPCharacteristicOption SAPMaterialGroup = (from co in materialGroups where co.Sapcode == zstMaterial.MaterialGroup select co).FirstOrDefault();

                if (!SAPMaterialGroup.IsNull())
                {
                    SAPCharacteristicOption sapAlternateCoilIndicator = (from co in alternateCoilIndicators where co.Sapcode == zstMaterial.AlternateCoilInd select co).FirstOrDefault();
                    SAPCharacteristicOption sapEpoxycoatColor = (from co in epoxycoatColors where co.Sapcode == zstMaterial.EpoxyColor select co).FirstOrDefault();
                    SAPCharacteristicOption sapKleenkoteColor = (from co in kleenkoteColors where co.Sapcode == zstMaterial.KleenkoteColor select co).FirstOrDefault();
                    SAPCharacteristicOption sapSalesInstruction = (from co in salesInstructions where co.Sapcode == zstMaterial.SalesInstructions select co).FirstOrDefault();
                    SAPCharacteristicOption sapSpecification = (from co in specifications where co.Sapcode == zstMaterial.Specifications select co).FirstOrDefault();
                    SAPCharacteristicOption sapTubeShape = (from co in tubeShapes where co.Sapcode == zstMaterial.Shape select co).FirstOrDefault();
                    SAPCharacteristicOption sapTubeStandard = (from co in tubeStandards where co.Sapcode == zstMaterial.Grade select co).FirstOrDefault();
                    SAPCharacteristicOption SAPMaterialPricingGroup = (from co in materialPricingGroups where co.Sapcode == zstMaterial.MaterialPricingGroup select co).FirstOrDefault();
                    SAPCharacteristicOption sapPricingGroup = (from co in pricingGroups where co.Sapcode == zstMaterial.PricingGroup select co).FirstOrDefault();
                    SAPCharacteristicOption SAPMaterialType = (from co in materialTypes where co.Sapcode == zstMaterial.MaterialType select co).FirstOrDefault();

                    if (!string.IsNullOrEmpty(zstMaterial.MaterialNumber))
                    {
                        if (SAPMaterial.IsNull())
                        {
                            SAPMaterial = new SAPMaterial();
                            SAPMaterial.DivisionID = (long)Enums.Divisions.Atlas;
                            SAPMaterial.Number = zstMaterial.MaterialNumber;
                            materials.Add(SAPMaterial);
                            db.Sapmaterials.Add(SAPMaterial);
                            insertedCount++;
                        }
                        else
                        {
                            checkedForUpdatesCount++;
                        }

                        SAPMaterial.Name = zstMaterial.Description;
                        SAPMaterial.SapmaterialGroup = SAPMaterialGroup;
                        SAPMaterial.SapmaterialType = SAPMaterialType;
                        SAPMaterial.SapalternateCoilIndicator = sapAlternateCoilIndicator;
                        SAPMaterial.Cutting = zstMaterial.Cutting.ToBool();
                        SAPMaterial.SapkleenkoteColor = sapKleenkoteColor;
                        SAPMaterial.SapepoxycoatColor = sapEpoxycoatColor;
                        SAPMaterial.SaptubeShape = sapTubeShape;
                        SAPMaterial.Size = zstMaterial.Size.ToNullableDecimal();
                        SAPMaterial.Size2 = zstMaterial.Size2.ToNullableDecimal();
                        SAPMaterial.Diameter = zstMaterial.Diameter.ToNullableDecimal();
                        SAPMaterial.Length = zstMaterial.Length.ToNullableDecimal();
                        SAPMaterial.LengthFeet = zstMaterial.LengthFt.ToNullableInt();
                        SAPMaterial.LengthInches = zstMaterial.LengthIn.ToNullableInt();
                        SAPMaterial.LengthFractionalInches = zstMaterial.LengthFracIn;
                        SAPMaterial.PieceWeight = zstMaterial.PieceWeight.ToNullableDecimal();
                        SAPMaterial.WeightPerFoot = zstMaterial.WeigthPerFoot.ToNullableDecimal();
                        SAPMaterial.SaptubeStandard = sapTubeStandard;
                        SAPMaterial.GaugeRestrictable = zstMaterial.GaugeRestrictable.ToNullableDecimal();
                        SAPMaterial.Bundling1 = zstMaterial.Bundling1.ToNullableInt();
                        SAPMaterial.Bundling2 = zstMaterial.Bundling2.ToNullableInt();
                        SAPMaterial.BundlingRound = zstMaterial.BundlingRound.ToNullableInt();
                        SAPMaterial.Sapspecification = sapSpecification;
                        SAPMaterial.SapsalesInstruction = sapSalesInstruction;
                        SAPMaterial.Configurable = zstMaterial.IsKmat.ToBool();
                        SAPMaterial.SapmaterialPricingGroup = SAPMaterialPricingGroup;
                        SAPMaterial.SappricingGroup = sapPricingGroup;
                        SAPMaterial.OldMaterialNumber = zstMaterial.OldMaterial.Trim();
                    }
                }
            }

            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(checkedForUpdatesCount);
            emailStringBuilder.Append(" checked for updates.<br />");

            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds<br /><br />");

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Material Download Results", emailStringBuilder.ToString());
        }

        private static void RefreshBundlingFromAtlasSAP(string email)
        {
            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int joinsCount = 0;
            int joinsRemovedCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            ZWS_MASTER_DATAClient masterDataService = new ZWS_MASTER_DATAClient("ATLAS_ZWS_MASTER_DATA");
            masterDataService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            masterDataService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            ZGetMaterialBundling getMaterialBundling = new ZGetMaterialBundling();
            getMaterialBundling.BundlingOptions = new ZstBundlingOption[] { new ZstBundlingOption() };

            masterDataService.Open();
            ZGetMaterialBundlingResponse getMaterialBundlingResponse = masterDataService.ZGetMaterialBundlingAsync(getMaterialBundling);
            masterDataService.Close();

            List<SAPMaterial> materials = (from m in db.Sapmaterials where m.DivisionID == (long)Enums.Divisions.Atlas select m).ToList();
            IQueryable<Plant> plants = (from l in Location.GetAllActive(ref db).OfType<Plant>() where l.DivisionID == (long)Enums.Divisions.Atlas select l);
            List<SAPBundlingOption> bundlingOptions = (from bo in db.SapbundlingOptions where bo.DivisionID == (long)Enums.Divisions.Atlas select bo).ToList();

            foreach (ZstBundlingOption zstBundlingOption in getMaterialBundlingResponse.BundlingOptions)
            {
                decimal gauge = zstBundlingOption.Gauge.ToDecimal();
                decimal lengthLow = zstBundlingOption.LengthLow.ToDecimal();
                decimal lengthHigh = zstBundlingOption.LengthHigh.ToDecimal();
                int bundling1 = zstBundlingOption.Bundling1.ToInt();
                int bundling2 = zstBundlingOption.Bundling2.ToInt();

                decimal size = zstBundlingOption.Size.ToDecimal();
                decimal size2 = zstBundlingOption.Size2.ToDecimal();
                decimal diameter = zstBundlingOption.Diameter.ToDecimal();
                // Plant specific Bundling - 10/10/2017 -Zahir Kapadia
                string plant = zstBundlingOption.Plant;
                SAPBundlingOption SAPBundlingOption;

                if (bundling2 <= 0)
                {
                    bundling2 = 1;
                }

                if (gauge > 0 && lengthLow > 0 && lengthHigh > 0 && bundling1 > 0)
                {
                    if (plant.jIsEmpty())
                    {
                        SAPBundlingOption = (from bo in bundlingOptions where bo.LengthLow == lengthLow && bo.LengthHigh == lengthHigh && bo.Bundling1 == bundling1 && bo.Bundling2 == bundling2 select bo).FirstOrDefault();
                    }
                    else
                    {
                        Plant pl = (from l in db.Locations.OfType<Plant>() where l.Code == plant select l).FirstOrDefault();
                        SAPBundlingOption = (from bo in bundlingOptions where bo.LengthLow == lengthLow && bo.LengthHigh == lengthHigh && bo.Bundling1 == bundling1 && bo.Bundling2 == bundling2 && bo.Plant == pl.Code select bo).FirstOrDefault();
                    }

                    if (SAPBundlingOption.IsNull())
                    {
                        SAPBundlingOption = new SAPBundlingOption();
                        SAPBundlingOption.DivisionID = (long)Enums.Divisions.Atlas;
                        SAPBundlingOption.LengthLow = lengthLow;
                        SAPBundlingOption.LengthHigh = lengthHigh;
                        SAPBundlingOption.Bundling1 = bundling1;
                        SAPBundlingOption.Bundling2 = bundling2;
                        SAPBundlingOption.Plant = plant;
                        bundlingOptions.Add(SAPBundlingOption);
                        db.SapbundlingOptions.Add(SAPBundlingOption);
                        insertedCount++;
                    }
                    else
                    {

                    }

                    List<SAPMaterial> matchingMaterials = null;

                    if (size > 0 && size2 > 0 && bundling1 > 0 && bundling2 > 0)
                    {
                        matchingMaterials = (from m in materials where m.Size == size && m.Size2 == size2 && m.GaugeRestrictable == gauge select m).ToList();
                    }
                    else if (zstBundlingOption.Diameter > 0 && zstBundlingOption.Bundling1 > 0)
                    {
                        matchingMaterials = (from m in materials where m.Diameter == diameter && m.GaugeRestrictable == gauge select m).ToList();
                    }

                    foreach (SAPMaterial matchingMaterial in matchingMaterials)
                    {
                        if (!(from bo in matchingMaterial.SapbundlingOptions select bo.SapbundlingOptionID).Contains(SAPBundlingOption.SapbundlingOptionID))
                        {
                            matchingMaterial.SapbundlingOptions.Add(SAPBundlingOption);
                        }
                    }
                }
            }

            db.SaveChanges();

            foreach (SAPMaterial SAPMaterial in materials)
            {
                List<SAPBundlingOption> SAPBundlingOptionsToRemove = new List<SAPBundlingOption>();

                foreach (SAPBundlingOption SAPBundlingOption in SAPMaterial.SapbundlingOptions)
                {
                    float gauge = SAPMaterial.GaugeRestrictable.ToFloat();
                    float lengthLow = SAPBundlingOption.LengthLow.ToFloat();
                    float lengthHigh = SAPBundlingOption.LengthHigh.ToFloat();
                    int bundling1 = SAPBundlingOption.Bundling1.ToInt();
                    int bundling2 = SAPBundlingOption.Bundling2.ToInt();
                    string plant = SAPBundlingOption.Plant;
                    float size = SAPMaterial.Size.ToFloat();
                    float size2 = SAPMaterial.Size2.ToFloat();
                    float diameter = SAPMaterial.Diameter.ToFloat();
                    ZstBundlingOption zstBundlingOption;

                    if (size > 0 && size2 > 0 && bundling1 > 0 && bundling2 > 0)
                    {
                        if (plant.jIsEmpty())
                        {
                            zstBundlingOption = (from bo in getMaterialBundlingResponse.BundlingOptions where bo.Size == size && bo.Size2 == size2 && bo.Gauge == gauge && bo.LengthLow == lengthLow && bo.LengthHigh == lengthHigh && bo.Bundling1 == bundling1 && bo.Bundling2 == bundling2 select bo).FirstOrDefault();
                        }
                        else
                        {
                            Plant pl = (from l in db.Locations.OfType<Plant>() where l.Code == plant select l).FirstOrDefault();
                            zstBundlingOption = (from bo in getMaterialBundlingResponse.BundlingOptions where bo.Size == size && bo.Size2 == size2 && bo.Gauge == gauge && bo.LengthLow == lengthLow && bo.LengthHigh == lengthHigh && bo.Bundling1 == bundling1 && bo.Bundling2 == bundling2 && bo.Plant == pl.Code select bo).FirstOrDefault();
                        }

                        if (zstBundlingOption.IsNull())
                        {
                            SAPBundlingOptionsToRemove.Add(SAPBundlingOption);
                        }
                    }
                    else if (diameter > 0 && bundling1 > 0)
                    {
                        if (plant.jIsEmpty())
                        {
                            zstBundlingOption = (from bo in getMaterialBundlingResponse.BundlingOptions where bo.Diameter == diameter && bo.Gauge == gauge && bo.LengthLow == lengthLow && bo.LengthHigh == lengthHigh && bo.Bundling1 == bundling1 select bo).FirstOrDefault();
                        }
                        else
                        {
                            Plant pl = (from l in db.Locations.OfType<Plant>() where l.Code == plant select l).FirstOrDefault();
                            zstBundlingOption = (from bo in getMaterialBundlingResponse.BundlingOptions where bo.Diameter == diameter && bo.Gauge == gauge && bo.LengthLow == lengthLow && bo.LengthHigh == lengthHigh && bo.Bundling1 == bundling1 && bo.Plant == pl.Code select bo).FirstOrDefault();
                        }

                        if (zstBundlingOption.IsNull())
                        {
                            SAPBundlingOptionsToRemove.Add(SAPBundlingOption);
                        }
                    }
                }

                foreach (SAPBundlingOption SAPBundlingOption in SAPBundlingOptionsToRemove)
                {
                    SAPMaterial.SapbundlingOptions.Remove(SAPBundlingOption);
                    joinsRemovedCount++;
                }
            }

            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(joinsCount);
            emailStringBuilder.Append(" joins created between SAP Materials and SAP Bundling Options.<br />");
            emailStringBuilder.Append(joinsRemovedCount);
            emailStringBuilder.Append(" joins removed between SAP Materials and SAP Bundling Options.<br />");

            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds<br /><br />");

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Atlas Material Bundling Download Results", emailStringBuilder.ToString());
        }

        public static void RefreshFromWheatlandSAP(string email)
        {
            RefreshCharacteristicsFromWheatlandSAP();
            RefreshMaterialsFromWheatlandSAP(email);
        }

        private static void RefreshCharacteristicsFromWheatlandSAP()
        {
            PortalEntities db = new PortalEntities();

            zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

            JMC.Portal.Business.WheatlandPortal.ZGetMaterialCharacteristics getMaterialCharacteristics = new JMC.Portal.Business.WheatlandPortal.ZGetMaterialCharacteristics();
            getMaterialCharacteristics.MaterialGroups = new JMC.Portal.Business.WheatlandPortal.ZstMaterialGroup[] { new JMC.Portal.Business.WheatlandPortal.ZstMaterialGroup() };
            getMaterialCharacteristics.MaterialTypes = new JMC.Portal.Business.WheatlandPortal.ZstMaterialType[] { new JMC.Portal.Business.WheatlandPortal.ZstMaterialType() };
            getMaterialCharacteristics.ProductLines = new JMC.Portal.Business.WheatlandPortal.ZstProductLine[] { new JMC.Portal.Business.WheatlandPortal.ZstProductLine() };
            getMaterialCharacteristics.ProductGroups = new JMC.Portal.Business.WheatlandPortal.ZstProductGroup[] { new JMC.Portal.Business.WheatlandPortal.ZstProductGroup() };
            getMaterialCharacteristics.ProductTypes = new JMC.Portal.Business.WheatlandPortal.ZstProductType[] { new JMC.Portal.Business.WheatlandPortal.ZstProductType() };
            getMaterialCharacteristics.ProductColorFinishes = new JMC.Portal.Business.WheatlandPortal.ZstProductColorFinish[] { new JMC.Portal.Business.WheatlandPortal.ZstProductColorFinish() };
            getMaterialCharacteristics.ProductEndFinish = new JMC.Portal.Business.WheatlandPortal.ZstProductEndFinish[] { new JMC.Portal.Business.WheatlandPortal.ZstProductEndFinish() };
            getMaterialCharacteristics.Specifications = new JMC.Portal.Business.WheatlandPortal.ZstSpecification[] { new JMC.Portal.Business.WheatlandPortal.ZstSpecification() };


            portalService.Open();
            JMC.Portal.Business.WheatlandPortal.ZGetMaterialCharacteristicsResponse getMaterialCharacteristicsResponse = portalService.ZGetMaterialCharacteristicsAsync(getMaterialCharacteristics);
            portalService.Close();

            List<SAPCharacteristicOption> materialGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.MaterialGroup select co).ToList();
            List<SAPCharacteristicOption> materialTypes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.MaterialType select co).ToList();
            List<SAPCharacteristicOption> productLines = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductLine select co).ToList();
            List<SAPCharacteristicOption> productGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductGroup select co).ToList();
            List<SAPCharacteristicOption> productTypes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductType select co).ToList();
            List<SAPCharacteristicOption> productColorFinishes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductColorFinish select co).ToList();
            List<SAPCharacteristicOption> productEndFinishes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductEndFinish select co).ToList();
            List<SAPCharacteristicOption> specifications = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.Specification select co).ToList();

            foreach (JMC.Portal.Business.WheatlandPortal.ZstMaterialGroup zstMaterialGroup in getMaterialCharacteristicsResponse.MaterialGroups)
            {
                if (!string.IsNullOrEmpty(zstMaterialGroup.Code) && !string.IsNullOrEmpty(zstMaterialGroup.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in materialGroups where co.Sapcode == zstMaterialGroup.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.MaterialGroup;
                        materialGroups.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstMaterialGroup.Code;
                    SAPCharacteristicOption.Name = zstMaterialGroup.Name;
                }
            }

            foreach (JMC.Portal.Business.WheatlandPortal.ZstMaterialType zstMaterialType in getMaterialCharacteristicsResponse.MaterialTypes)
            {
                if (!string.IsNullOrEmpty(zstMaterialType.Code) && !string.IsNullOrEmpty(zstMaterialType.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in materialTypes where co.Sapcode == zstMaterialType.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.MaterialType;
                        materialTypes.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstMaterialType.Code;
                    SAPCharacteristicOption.Name = zstMaterialType.Name;
                }
            }

            foreach (JMC.Portal.Business.WheatlandPortal.ZstProductLine zstProductLine in getMaterialCharacteristicsResponse.ProductLines)
            {
                if (!string.IsNullOrEmpty(zstProductLine.Code) && !string.IsNullOrEmpty(zstProductLine.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in productLines where co.Sapcode == zstProductLine.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.ProductLine;
                        productLines.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstProductLine.Code;
                    SAPCharacteristicOption.Name = zstProductLine.Name;
                }
            }

            foreach (JMC.Portal.Business.WheatlandPortal.ZstProductGroup zstProductGroup in getMaterialCharacteristicsResponse.ProductGroups)
            {
                if (!string.IsNullOrEmpty(zstProductGroup.Code) && !string.IsNullOrEmpty(zstProductGroup.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in productGroups where co.Sapcode == zstProductGroup.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.ProductGroup;
                        productGroups.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstProductGroup.Code;
                    SAPCharacteristicOption.Name = zstProductGroup.Name;
                }
            }

            foreach (JMC.Portal.Business.WheatlandPortal.ZstProductType zstProductType in getMaterialCharacteristicsResponse.ProductTypes)
            {
                if (!string.IsNullOrEmpty(zstProductType.Code) && !string.IsNullOrEmpty(zstProductType.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in productTypes where co.Sapcode == zstProductType.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.ProductType;
                        productTypes.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstProductType.Code;
                    SAPCharacteristicOption.Name = zstProductType.Name;
                }
            }

            foreach (JMC.Portal.Business.WheatlandPortal.ZstProductColorFinish zstProductColorFinish in getMaterialCharacteristicsResponse.ProductColorFinishes)
            {
                if (!string.IsNullOrEmpty(zstProductColorFinish.Code) && !string.IsNullOrEmpty(zstProductColorFinish.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in productColorFinishes where co.Sapcode == zstProductColorFinish.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.ProductColorFinish;
                        productColorFinishes.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstProductColorFinish.Code;
                    SAPCharacteristicOption.Name = zstProductColorFinish.Name;
                }
            }

            foreach (JMC.Portal.Business.WheatlandPortal.ZstProductEndFinish zstProductEndFinish in getMaterialCharacteristicsResponse.ProductEndFinish)
            {
                if (!string.IsNullOrEmpty(zstProductEndFinish.Code) && !string.IsNullOrEmpty(zstProductEndFinish.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in productEndFinishes where co.Sapcode == zstProductEndFinish.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.ProductEndFinish;
                        productEndFinishes.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstProductEndFinish.Code;
                    SAPCharacteristicOption.Name = zstProductEndFinish.Name;
                }
            }

            foreach (JMC.Portal.Business.WheatlandPortal.ZstSpecification zstSpecification in getMaterialCharacteristicsResponse.Specifications)
            {
                if (!string.IsNullOrEmpty(zstSpecification.Code) && !string.IsNullOrEmpty(zstSpecification.Name))
                {
                    SAPCharacteristicOption SAPCharacteristicOption = (from co in specifications where co.Sapcode == zstSpecification.Code select co).FirstOrDefault();

                    if (SAPCharacteristicOption.IsNull())
                    {
                        SAPCharacteristicOption = new SAPCharacteristicOption();
                        SAPCharacteristicOption.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.Specification;
                        specifications.Add(SAPCharacteristicOption);
                        db.SapcharacteristicOptions.Add(SAPCharacteristicOption);
                    }

                    SAPCharacteristicOption.Sapcode = zstSpecification.Code;
                    SAPCharacteristicOption.Name = zstSpecification.Name;
                }
            }
            db.SaveChanges();
        }

        private static void RefreshMaterialsFromWheatlandSAP(string email)
        {
            PortalEntities db = new PortalEntities();

            int insertedCount = 0;
            int checkedForUpdatesCount = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

            JMC.Portal.Business.WheatlandPortal.ZGetAllMaterials getAllMaterials = new JMC.Portal.Business.WheatlandPortal.ZGetAllMaterials();
            JMC.Portal.Business.WheatlandPortal.ZGetMaterialPlants getMaterialPlants = new JMC.Portal.Business.WheatlandPortal.ZGetMaterialPlants();

            getAllMaterials.Materials = new JMC.Portal.Business.WheatlandPortal.ZstMaterial[] { };
            getAllMaterials.SalesViews = new JMC.Portal.Business.WheatlandPortal.ZstSalesViews[] { };
            getMaterialPlants.MaterialsPlants = new JMC.Portal.Business.WheatlandPortal.ZstMaterialPlant[] { };

            portalService.Open();
            JMC.Portal.Business.WheatlandPortal.ZGetAllMaterialsResponse getAllMaterialsResponse = portalService.ZGetAllMaterialsAsync(getAllMaterials);
            JMC.Portal.Business.WheatlandPortal.ZGetMaterialPlantsResponse getMaterialPlantsResponse = portalService.ZGetMaterialPlantsAsync(getMaterialPlants);
            portalService.Close();

            List<SAPCharacteristicOption> materialGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.MaterialGroup select co).ToList();
            List<SAPCharacteristicOption> materialTypes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.MaterialType select co).ToList();
            List<SAPCharacteristicOption> productLines = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductLine select co).ToList();
            List<SAPCharacteristicOption> productGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductGroup select co).ToList();
            List<SAPCharacteristicOption> productTypes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductType select co).ToList();
            List<SAPCharacteristicOption> productColorFinishes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductColorFinish select co).ToList();
            List<SAPCharacteristicOption> productEndFinishes = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductEndFinish select co).ToList();
            List<SAPCharacteristicOption> productEndFinishesEnergex = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.ProductEndFinishEnergex select co).ToList();
            List<SAPCharacteristicOption> metalGrades = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.MetalGrade select co).ToList();
            List<SAPMaterial> materials = (from m in db.Sapmaterials where m.DivisionID == (long)Enums.Divisions.Wheatland select m).ToList();
            List<SAPCharacteristicOption> specifications = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.WheatlandSAPCharacteristicTypes.Specification select co).ToList();

            int count = 0;

            foreach (JMC.Portal.Business.WheatlandPortal.ZstMaterial zstMaterial in getAllMaterialsResponse.Materials)
            {
                SAPMaterial SAPMaterial = (from m in materials where m.Number == zstMaterial.MaterialNumber select m).FirstOrDefault();
                SAPCharacteristicOption SAPMaterialGroup = (from co in materialGroups where co.Sapcode == zstMaterial.MaterialGroup select co).FirstOrDefault();

                if (!SAPMaterialGroup.IsNull())
                {
                    SAPCharacteristicOption SAPMaterialType = (from co in materialTypes where co.Sapcode == zstMaterial.MaterialType select co).FirstOrDefault();
                    SAPCharacteristicOption sapProductLine = (from co in productLines where co.Sapcode == zstMaterial.ProductLine select co).FirstOrDefault();
                    SAPCharacteristicOption sapProductGroup = (from co in productGroups where co.Sapcode == zstMaterial.ProductGroup select co).FirstOrDefault();
                    SAPCharacteristicOption sapProductType = (from co in productTypes where co.Sapcode == zstMaterial.ProductType select co).FirstOrDefault();
                    SAPCharacteristicOption sapProductColorFinish = (from co in productColorFinishes where co.Sapcode == zstMaterial.ProductColorFinish select co).FirstOrDefault();
                    SAPCharacteristicOption sapProductEndFinish = (from co in productEndFinishes where co.Sapcode == zstMaterial.ProductEndFinish select co).FirstOrDefault();
                    SAPCharacteristicOption sapProductEndFinishEnergex = (from co in productEndFinishesEnergex where co.Sapcode == zstMaterial.ProductEndFinishEnergex select co).FirstOrDefault();
                    SAPCharacteristicOption sapMetalGrade = (from co in metalGrades where co.Sapcode == zstMaterial.MetalGrade select co).FirstOrDefault();
                    SAPCharacteristicOption sapSpecification = (from co in specifications where co.Sapcode == zstMaterial.Specifications select co).FirstOrDefault();
                    //TODO
                    if (!zstMaterial.ProductEndFinishEnergex.jIsEmpty() && sapProductEndFinishEnergex.IsNull())
                    {
                        sapProductEndFinishEnergex = new SAPCharacteristicOption();
                        sapProductEndFinishEnergex.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.ProductEndFinishEnergex;
                        sapProductEndFinishEnergex.Sapcode = zstMaterial.ProductEndFinishEnergex;
                        sapProductEndFinishEnergex.Name = zstMaterial.ProductEndFinishEnergex;

                        productEndFinishesEnergex.Add(sapProductEndFinishEnergex);
                        db.SapcharacteristicOptions.Add(sapProductEndFinishEnergex);
                    }

                    if (!zstMaterial.MetalGrade.jIsEmpty() && sapMetalGrade.IsNull())
                    {
                        sapMetalGrade = new SAPCharacteristicOption();
                        sapMetalGrade.SapcharacteristicTypeID = (long)Enums.WheatlandSAPCharacteristicTypes.MetalGrade;
                        sapMetalGrade.Sapcode = zstMaterial.MetalGrade;
                        sapMetalGrade.Name = zstMaterial.MetalGrade;

                        metalGrades.Add(sapMetalGrade);
                        db.SapcharacteristicOptions.Add(sapMetalGrade);
                    }

                    if (!string.IsNullOrEmpty(zstMaterial.MaterialNumber))
                    {
                        if (SAPMaterial.IsNull())
                        {
                            SAPMaterial = new SAPMaterial();
                            SAPMaterial.DivisionID = (long)Enums.Divisions.Wheatland;
                            SAPMaterial.Number = zstMaterial.MaterialNumber;
                            materials.Add(SAPMaterial);
                            db.Sapmaterials.Add(SAPMaterial);
                            insertedCount++;
                        }
                        else
                        {
                            checkedForUpdatesCount++;
                        }

                        SAPMaterial.Name = zstMaterial.Description;
                        SAPMaterial.SapmaterialGroup = SAPMaterialGroup;
                        SAPMaterial.SapmaterialType = SAPMaterialType;
                        SAPMaterial.SapproductLine = sapProductLine;
                        SAPMaterial.SapproductGroup = sapProductGroup;
                        SAPMaterial.SapproductType = sapProductType;
                        SAPMaterial.SapproductColorFinish = sapProductColorFinish;
                        SAPMaterial.SapproductEndFinish = sapProductEndFinish;
                        SAPMaterial.SapproductEndFinishEnergex = sapProductEndFinishEnergex;
                        SAPMaterial.SapmetalGrade = sapMetalGrade;
                        SAPMaterial.LengthFractionalInches = string.Empty;
                        SAPMaterial.ProductSize = zstMaterial.ProductSize;
                        SAPMaterial.ProductGauge = zstMaterial.ProductGauge;
                        SAPMaterial.GrossWeight = zstMaterial.GrossWeight;
                        SAPMaterial.NetWeight = zstMaterial.NetWeight;
                        SAPMaterial.CommissionGroup = zstMaterial.CommissionGroup;
                        SAPMaterial.VolumeRebateGroup = zstMaterial.VolumeRebateGroup;
                        SAPMaterial.Sapspecification = sapSpecification;
                        SAPMaterial.Length = zstMaterial.Length.ToDecimal();
                        SAPMaterial.Diameter = zstMaterial.OutsideDiameter;
                        SAPMaterial.GaugeRestrictable = zstMaterial.Gauge;
                        SAPMaterial.PlanningMaterial = zstMaterial.PlanningMaterial;
                        SAPMaterial.PieceWeight = zstMaterial.PieceWeight.ToDecimal();
                        SAPMaterial.BundleWeight = zstMaterial.BundleWeight.ToDecimal();
                        SAPMaterial.TotalBundleLength = zstMaterial.TotLenBun.ToDecimal();
                        SAPMaterial.PieceperBundle = zstMaterial.TotPcQtyBun.ToDecimal().ToInt();


                    }
                }

                count++;

                if (count % 100 == 0)
                {
                    db.SaveChanges();
                }
            }

            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append(insertedCount);
            emailStringBuilder.Append(" inserted.<br />");
            emailStringBuilder.Append(checkedForUpdatesCount);
            emailStringBuilder.Append(" checked for updates.<br />");

            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds<br /><br />");

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Wheatland Material Download Results", emailStringBuilder.ToString());
        }

        public static void RefreshUnitOfMeasureFromWheatlandSAP(string email)
        {
            PortalEntities db = new PortalEntities();

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            StringBuilder emailStringBuilder = new StringBuilder();

            zws_portalClient portalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            portalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            portalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];

            JMC.Portal.Business.WheatlandPortal.ZGetMaterialUom getMaterialUom = new JMC.Portal.Business.WheatlandPortal.ZGetMaterialUom();

            portalService.Open();
            JMC.Portal.Business.WheatlandPortal.ZGetMaterialUomResponse getMaterialUomResponse = portalService.ZGetMaterialUomAsync(getMaterialUom);
            portalService.Close();

            List<SAPMaterial> SAPMaterials = (from m in db.Sapmaterials where m.DivisionID == (long)Enums.Divisions.Wheatland select m).ToList();
            List<SAPUnitOfMeasure> SAPUnitOfMeasures = (from uom in db.SapunitOfMeasures select uom).ToList();
            int count = 0;

            foreach (JMC.Portal.Business.WheatlandPortal.ZstMaterialUom zstMaterialUom in getMaterialUomResponse.ExUomConversions)
            {
                SAPUnitOfMeasure baseSAPUnitOfMeasure = (from uom in SAPUnitOfMeasures where uom.Sapcode == zstMaterialUom.BaseUom.Trim() select uom).FirstOrDefault();

                if (baseSAPUnitOfMeasure.IsNull())
                {
                    baseSAPUnitOfMeasure = new SAPUnitOfMeasure();

                    baseSAPUnitOfMeasure.Sapcode = zstMaterialUom.BaseUom.Trim();

                    SAPUnitOfMeasures.Add(baseSAPUnitOfMeasure);
                    db.SapunitOfMeasures.Add(baseSAPUnitOfMeasure);
                }

                SAPUnitOfMeasure alternateSAPUnitOfMeasure = (from uom in SAPUnitOfMeasures where uom.Sapcode == zstMaterialUom.AltUom.Trim() select uom).FirstOrDefault();

                if (alternateSAPUnitOfMeasure.IsNull())
                {
                    alternateSAPUnitOfMeasure = new SAPUnitOfMeasure();

                    alternateSAPUnitOfMeasure.Sapcode = zstMaterialUom.AltUom.Trim();

                    SAPUnitOfMeasures.Add(alternateSAPUnitOfMeasure);
                    db.SapunitOfMeasures.Add(alternateSAPUnitOfMeasure);
                }

                if (!baseSAPUnitOfMeasure.IsNull() && !alternateSAPUnitOfMeasure.IsNull())
                {
                    SAPMaterial SAPMaterial = (from m in SAPMaterials where m.DivisionID == (long)Enums.Divisions.Wheatland && m.Number == zstMaterialUom.MaterialNumber.Trim() select m).FirstOrDefault();

                    if (!SAPMaterial.IsNull())
                    {
                        SAPMaterialUnitOfMeasure SAPMaterialUnitOfMeasure = (from muom in SAPMaterial.SapmaterialUnitOfMeasures where muom.BaseUnitOfMeasureID == baseSAPUnitOfMeasure.SapunitOfMeasureID && muom.AlternateUnitOfMeasureID == alternateSAPUnitOfMeasure.SapunitOfMeasureID select muom).FirstOrDefault();

                        if (SAPMaterialUnitOfMeasure.IsNull())
                        {
                            SAPMaterialUnitOfMeasure = new SAPMaterialUnitOfMeasure();

                            SAPMaterialUnitOfMeasure.BaseUnitOfMeasure = baseSAPUnitOfMeasure;     //.BaseSAPUnitOfMeasure = baseSAPUnitOfMeasure;
                            SAPMaterialUnitOfMeasure.AlternateUnitOfMeasure = alternateSAPUnitOfMeasure;     //.AlternateSAPUnitOfMeasure = alternateSAPUnitOfMeasure;

                            SAPMaterial.SapmaterialUnitOfMeasures.Add(SAPMaterialUnitOfMeasure);
                            db.SapmaterialUnitOfMeasures.Add(SAPMaterialUnitOfMeasure);
                        }

                        SAPMaterialUnitOfMeasure.Numerator = zstMaterialUom.Numerator;
                        SAPMaterialUnitOfMeasure.Denominator = zstMaterialUom.Denominator;

                        count++;

                        if (count % 100 == 0)
                        {
                            db.SaveChanges();
                        }
                    }
                }
            }

            db.SaveChanges();

            endTime = DateTime.Now;

            TimeSpan runTime = endTime.Subtract(startTime);

            emailStringBuilder.Append("Run Time " + runTime.Days);
            emailStringBuilder.Append("days " + runTime.Hours);
            emailStringBuilder.Append("hours " + runTime.Minutes);
            emailStringBuilder.Append("minutes " + runTime.Seconds);
            emailStringBuilder.Append("seconds<br /><br />");

            Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], email, string.Empty, "Wheatland Material UOM Download Results", emailStringBuilder.ToString());
        }
    }

    public class SquareGaugeSize
    {
        public long squareID;
        public Gauge gauge;
    }

    public class RectGaugeSize
    {
        public long rectangleID;
        public Gauge gauge;
    }

    public class RoundGaugeSize
    {
        public long roundID;
        public Gauge gauge;
    }

    public class NPSGaugeSize
    {
        public long roundID;
        public Gauge gauge;
        public string Description;
    }
    public class SqRecGaugeSize
    {
        public long sizeID;
        public Gauge gauge;
    }
    public class SpecialGaugeSize
    {
        public long sizeID;
        public Gauge gauge;
    }
    public class JumboGaugeSize
    {
        public long sizeID;
        public Gauge gauge;
    }


    public class SRSize
    {
        public long sizeID;
        public decimal size1;
        public decimal size2;
        public string shape;
    }
}