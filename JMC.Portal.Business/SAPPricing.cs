using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using HSSPortalSales;
//using WheatlandPortal;
//using AtlasSAPPortal;
using JMC.Portal.Business.HSSPortalSales;
using JMC.Portal.Business.WheatlandPortal;
using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using log4net;

namespace JMC.Portal.Business
{
    public class SAPPricing : IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DateTime EffectiveDate { get; private set; }
        public SAPSoldTo SAPSoldTo { get; private set; }
        public SAPShipTo SAPShipTo { get; private set; }
        public List<SAPCharacteristicOption> AllSAPMaterialGroups { get; private set; }
        public List<SAPCharacteristicOption> SAPMaterialPricingGroups { get; private set; }
        public List<SAPCharacteristicOption> SAPTubeStandards { get; private set; }
        public List<Zr00> corePrices { get; private set; }
        public List<Zr00> todeleteListZR00 { get; private set; }
        public List<Zr01> Adders { get; private set; }
        public List<Zr01> todeleteListZR01 { get; private set; }
        public List<Zr04> materialGroupAdders { get; private set; }
        public List<Zr04> todeleteListZR04 { get; private set; }
        public List<Zep1> EpoxyPrices { get; private set; }
        public List<Zep1> todeleteListZEP1 { get; private set; }
        public List<Zg01> GradeXtras { get; private set; }
        public List<Zg01> todeleteListZG01 { get; private set; }
        public List<FreightandFsc> FreightFSCcharges { get; private set; }
        public List<FreightandFsc> todeleteListFreightFSCcharges { get; private set; }
        public List<SAPCharacteristicOption> SAPPricingGroups { get; private set; }
        public List<SAPCharacteristicOption> SAPMaterialGroups { get; private set; }
        PortalEntities db;

        public SAPPricing()
        {
            if (db == null)
            {
                db = new PortalEntities();
            }
            var sapPricing = SSAPPricingHelper.getInstance();
            this.SAPMaterialPricingGroups = sapPricing.SAPMaterialPricingGroups;
            this.SAPPricingGroups = sapPricing.SAPPricingGroups;
            this.AllSAPMaterialGroups = sapPricing.AllSAPMaterialGroups;
            this.SAPTubeStandards = sapPricing.SAPTubeStandards;
            this.EffectiveDate = DateTime.Today;
        }

        public void GetPricingFromAtlasSAP(string email)
        {
            Log.Info("Start GetPricingFromAtlasSAP...");
            int i = 0;
						var soltoList = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.Active == true && s.DivisionID == (long)Enums.Divisions.Atlas select s).ToList();
            foreach (SAPSoldTo SAPSoldTo in soltoList)
            {
                this.SAPSoldTo = SAPSoldTo;
                //if (i == 1) break;
                string gradXtras = (i == 0) ? "X" : "";
                i++;
                if (!this.SAPSoldTo.IsNull())
                {
                    Log.Info("Start GetPricingFromAtlasSAP for SoldTo:" + this.SAPSoldTo.Number);
                    //if (this.SAPSoldTo.Number != "0000001696")
                    //    continue;
                    this.SAPSoldTo.CoreSAPPricingCondition = new SAPCondition();
                    this.SAPSoldTo.PropSAPPricingCondition = new SAPCondition();
                    this.SAPMaterialGroups = new List<SAPCharacteristicOption>();

                    this.corePrices = (from core in db.Zr00s where core.SapsoldtoID == this.SAPSoldTo.SapshipToID select core).ToList();
                    this.todeleteListZR00 = new List<ZR00>();
                    this.todeleteListZR00.AddRange(this.corePrices);

                    this.Adders = (from adder in db.Zr01s where adder.SapsoldtoID == this.SAPSoldTo.SapshipToID select adder).ToList();
                    this.todeleteListZR01 = new List<ZR01>();
                    this.todeleteListZR01.AddRange(this.Adders);

                    this.materialGroupAdders = (from mgAdder in db.Zr04s where mgAdder.SapsoldtoID == this.SAPSoldTo.SapshipToID select mgAdder).ToList();
                    this.todeleteListZR04 = new List<ZR04>();
                    this.todeleteListZR04.AddRange(this.materialGroupAdders);

                    if (gradXtras == "X") //run for first customer only.
                    {
                        this.EpoxyPrices = (from epoxy in db.Zep1s select epoxy).ToList();
                        this.todeleteListZEP1 = new List<ZEP1>();
                        this.todeleteListZEP1.AddRange(this.EpoxyPrices);
				Log.Info("End GetPricingFromAtlasSAP.....");
                        this.GradeXtras = (from gxtra in db.Zg01s select gxtra).ToList();
                        this.todeleteListZG01 = new List<ZG01>();
                        this.todeleteListZG01.AddRange(this.GradeXtras);
                    }
                    var shipids = this.SAPSoldTo.sapshipTos.Select(x => x.SapshipToID).ToList();
                    this.FreightFSCcharges = db.FreightandFscs.Where(ff => ff.SapsoldtoID == this.SAPSoldTo.SapshipToID && shipids.Contains(ff.SapshiptoID)).ToList();
                    this.todeleteListFreightFSCcharges = new List<FreightandFsc>();
                    this.todeleteListFreightFSCcharges.AddRange(this.FreightFSCcharges);
                    this.GetAllPricefromAtlasSAP(gradXtras);
                }
            }
            Log.Info("END GetPricingFromAtlasSAP...");
        }

        private void GetAllPricefromAtlasSAP(string gradeXtra)
        {
            ZstSoldToPricing[] ZG01Response = null;
            ZstSoldToPricing[] ZEP1Response = null;
            ZstSoldToPricing[] pricingResponse = null;
            using (ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL"))
            {
                sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
                sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

                AtlasSAPPortal.ZGetSoldToPricing getSoldToPricing = new AtlasSAPPortal.ZGetSoldToPricing();
                getSoldToPricing.ImSoldToNumber = this.SAPSoldTo.Number;
                getSoldToPricing.ImPricingDate = this.EffectiveDate;
                getSoldToPricing.ImGetAddtl = gradeXtra;
                getSoldToPricing.Pricing = new ZstSoldToPricing[] { };
                getSoldToPricing.ZG01 = new ZstSoldToPricing[] { };
                getSoldToPricing.ZEP1 = new ZstSoldToPricing[] { };
                AtlasSAPPortal.ZGetSoldToPricingResponse getSoldToPricingResponse = null;

                if (sapPortalService.State == System.ServiceModel.CommunicationState.Created
                    || sapPortalService.State == System.ServiceModel.CommunicationState.Closed)
                {
                    sapPortalService.Open();
                    getSoldToPricingResponse = sapPortalService.ZGetSoldToPricingAsync(getSoldToPricing);
                    sapPortalService.Close();

                    ZG01Response = getSoldToPricingResponse.ZG01;
                    ZEP1Response = getSoldToPricingResponse.ZEP1;
                    pricingResponse = getSoldToPricingResponse.Pricing;
                }
            }

            #region run for first customer only.
            if (gradeXtra == "X") 
            {
                #region ZG01
                if (ZG01Response != null)
                {
                    foreach (ZstSoldToPricing zstSoldToPricing in ZG01Response.Where(x => !string.IsNullOrEmpty(x.ConditionType)))
                    {
                        SAPCondition gradXtra = new SAPCondition();
                        gradXtra.Sapcode = zstSoldToPricing.ConditionType;
                        gradXtra.Rate = zstSoldToPricing.Rate;
                        gradXtra.Currency = zstSoldToPricing.RateUnit;
                        gradXtra.RateUnit = zstSoldToPricing.RateUnit;
                        gradXtra.ValidFrom = zstSoldToPricing.ValidFrom.ToDate();
                        gradXtra.ValidTo = zstSoldToPricing.ValidTo.ToDate();
                        gradXtra.PricePer = zstSoldToPricing.PricePer.ToInt();
                        gradXtra.PricePerUnit = zstSoldToPricing.PricePerUnit;

                        InsertZG01(zstSoldToPricing, gradXtra);
                    }
                }
                #endregion

                #region ZEP1
                if (ZEP1Response != null)
                {
                    foreach (ZstSoldToPricing zstSoldToPricing in ZEP1Response.Where(x => !string.IsNullOrEmpty(x.ConditionType)))
                    {
                        SAPCondition ZEP1condition = new SAPCondition();
                        ZEP1condition.Sapcode = zstSoldToPricing.ConditionType;
                        ZEP1condition.Rate = zstSoldToPricing.Rate;
                        ZEP1condition.Currency = zstSoldToPricing.RateUnit;
                        ZEP1condition.RateUnit = zstSoldToPricing.RateUnit;
                        ZEP1condition.ValidFrom = zstSoldToPricing.ValidFrom.ToDate();
                        ZEP1condition.ValidTo = zstSoldToPricing.ValidTo.ToDate();
                        ZEP1condition.PricePer = zstSoldToPricing.PricePer.ToInt();
                        ZEP1condition.PricePerUnit = zstSoldToPricing.PricePerUnit;

                        InsertZEP1(zstSoldToPricing, ZEP1condition);
                    }
                }
                #endregion

                foreach (ZG01 record in todeleteListZG01)
                {
                    ZG01 isDeleted = (from r in db.Zg01s
                                      where r.Currency == record.Currency && r.SapsoldtoID == record.SapsoldtoID && r.SaptubeStandardID == record.SaptubeStandardID
                                      select r).FirstOrDefault();
                    if (isDeleted != null)
                    {
                        db.Zg01s.Remove(isDeleted);
                    }
                }
                foreach (ZEP1 record in todeleteListZEP1)
                {
                    ZEP1 isDeleted = (from r in db.Zep1s
                                      where r.SappricingGroupID == record.sapcharacteristicOption.SapcharacteristicOptionID && r.Currency == record.Currency
                                      select r).FirstOrDefault();
                    if (isDeleted != null)
                    {
                        db.Zep1s.Remove(isDeleted);
                    }
                }
                db.SaveChanges();
            }
            #endregion            

            #region Pricing
            if (pricingResponse != null)
            {
                #region Pricing ZR01
                foreach (ZstSoldToPricing zstSoldToPricing in pricingResponse.Where(x => !string.IsNullOrEmpty(x.ConditionType) && x.ConditionType == "ZR01"))
                {
                    SAPCondition SAPCondition = CreateSAPCondition(zstSoldToPricing);                 
                    InsertZR01(zstSoldToPricing, this.SAPSoldTo.SapshipToID, SAPCondition);                   
                }
                foreach (ZR01 record in todeleteListZR01)
                {
                    ZR01 isDeleted = (from r in db.Zr01s
                                      where r.SapsoldtoID == record.SapsoldtoID && r.SappricingGroupID == record.sapcharacteristicOption.SapcharacteristicOptionID
                                      select r).FirstOrDefault();
                    if (isDeleted != null)
                    {
                        db.Zr01s.Remove(isDeleted);
                    }
                }                
                db.SaveChanges();
                #endregion

                #region Pricing ZR00
                foreach (ZstSoldToPricing zstSoldToPricing in pricingResponse.Where(x => !string.IsNullOrEmpty(x.ConditionType) && x.ConditionType == "ZR00"))
                {
                    SAPCondition SAPCondition = CreateSAPCondition(zstSoldToPricing);
                    SAPCharacteristicOption SAPMaterialPricingGroup = (from co in this.SAPMaterialPricingGroups where co.Sapcode == zstSoldToPricing.MaterialPricingGroup select co).FirstOrDefault();
                    if (!SAPMaterialPricingGroup.IsNull())
                    {
                        SAPMaterialPricingGroup.SAPCondition = SAPCondition;
                        if (SAPMaterialPricingGroup.Sapcode == "Z1")
                        {
                            this.SAPSoldTo.CoreSAPPricingCondition = SAPCondition;
                            InsertZR00(SAPMaterialPricingGroup, this.SAPSoldTo.SapshipToID, SAPCondition);
                        }

                        if (SAPMaterialPricingGroup.Sapcode == "Z2")
                        {
                            this.SAPSoldTo.PropSAPPricingCondition = SAPCondition;
                            InsertZR00(SAPMaterialPricingGroup, this.SAPSoldTo.SapshipToID, SAPCondition);
                        }
                        if (SAPMaterialPricingGroup.Sapcode == "Z3")
                        {
                            this.SAPSoldTo.JumboSAPPricingCondition = SAPCondition;
                            InsertZR00(SAPMaterialPricingGroup, this.SAPSoldTo.SapshipToID, SAPCondition);
                        }

                        if (SAPMaterialPricingGroup.Sapcode == "Z4")
                        {
                            this.SAPSoldTo.EpoxZSAPPricingCondition = SAPCondition;
                            InsertZR00(SAPMaterialPricingGroup, this.SAPSoldTo.SapshipToID, SAPCondition);
                        }
												if (SAPMaterialPricingGroup.Sapcode == "Z5") {
													this.SAPSoldTo.LGPipeSAPPricingCondition = SAPCondition;
													InsertZR00(SAPMaterialPricingGroup, this.SAPSoldTo.SapshipToID, SAPCondition);
												}
                    }

                }
                foreach (ZR00 record in todeleteListZR00)
                {
                    ZR00 isDeleted = (from r in db.Zr00s
                                      where r.SapsoldtoID == record.SapsoldtoID && r.SapmaterialPricingGroupID == record.SapmaterialPricingGroupID
                                      select r).FirstOrDefault();
                    if (isDeleted != null)
                    {
                        db.Zr00s.Remove(isDeleted);
                    }
                }
                db.SaveChanges();
                #endregion

                #region Pricing Rest ALL
                foreach (ZstSoldToPricing zstSoldToPricing in pricingResponse.Where(x => !string.IsNullOrEmpty(x.ConditionType)
                    && x.ConditionType != "ZR00" && x.ConditionType != "ZR01" && x.ConditionType != "ZEP1"))
                {
                    SAPCondition SAPCondition = CreateSAPCondition(zstSoldToPricing);
                    Log.Info(String.Format("Loop for SAPShipToNumber:{0}, SoldTo : {4}, SAPCode:{1}, SapCurrency:{2} SapRate: {3}", zstSoldToPricing.ShipToNumber,
                           SAPCondition.Sapcode, SAPCondition.Currency, SAPCondition.Rate, this.SAPSoldTo.SapshipToID));

                    SAPShipTo SAPShipTo = (from s in this.SAPSoldTo.sapshipTos where s.Number == zstSoldToPricing.ShipToNumber select s).FirstOrDefault();
                    if (!SAPShipTo.IsNull())
                    {
                        if (SAPCondition.Sapcode == "ZF02" || SAPCondition.Sapcode == "ZF00" || SAPCondition.Sapcode == "PROP" || SAPCondition.Sapcode == "ZFSC")
                        {
                            InsertFreightandFSC(zstSoldToPricing, this.SAPSoldTo.SapshipToID, SAPShipTo.SapshipToID, SAPCondition);
                        }
                    }
                    else
                    {
                        if (SAPCondition.Sapcode == "ZR04")
                        {
                            InsertZR04(zstSoldToPricing, this.SAPSoldTo.SapshipToID, SAPCondition);
                        }
                    }
                }
                foreach (FreightandFsc record in todeleteListFreightFSCcharges.Where(x=>x.SapsoldtoID != null))
                {
                    FreightandFsc isDeleted = (from r in db.FreightandFscs where r.SapshiptoID == record.SapshiptoID 
                                               && r.SapconditionID == record.SapconditionID
                                               && r.SapsoldtoID == this.SAPSoldTo.SapshipToID
                                               select r).FirstOrDefault();
                    if (isDeleted != null)
                    {
                        db.FreightandFscs.Remove(isDeleted);
                    }
                }
                foreach (FreightandFsc record in todeleteListFreightFSCcharges.Where(x => x.SapsoldtoID == null))
                {
                    FreightandFsc isDeleted = (from r in db.FreightandFscs
                                               where r.SapshiptoID == record.SapshiptoID && r.SapsoldtoID == null
                                               && r.SapconditionID == record.SapconditionID
                                               select r).FirstOrDefault();
                    if (isDeleted != null)
                    {
                        db.FreightandFscs.Remove(isDeleted);
                    }
                }
                foreach (ZR04 record in todeleteListZR04)
                {
                    ZR04 isDeleted = (from r in db.Zr04s
                                      where r.SapsoldtoID == record.SapsoldtoID && r.SapmaterialGroupID == record.SapmaterialGroupID
                                      select r).FirstOrDefault();
                    if (isDeleted != null)
                    {
                        db.Zr04s.Remove(isDeleted);
                    }
                }
                db.SaveChanges();
                #endregion

            }
            #endregion

            //to delete from SQL table if SAP doesn't return pricing record.             
            bool isSaving = true;
            try
            {
                db.SaveChanges();
            }
            finally
            {
                isSaving = false;
            }
        }

        private SAPCondition CreateSAPCondition(ZstSoldToPricing zstSoldToPricing)
        {
            SAPCondition SAPCondition = (from c in db.Sapconditions where c.Sapcode == zstSoldToPricing.ConditionType select c).FirstOrDefault();
            SAPCondition.Sapcode = zstSoldToPricing.ConditionType;
            SAPCondition.Rate = zstSoldToPricing.Rate;
            SAPCondition.Currency = zstSoldToPricing.RateUnit;
            SAPCondition.RateUnit = zstSoldToPricing.RateUnit;
            SAPCondition.ValidFrom = zstSoldToPricing.ValidFrom.ToDate();
            SAPCondition.ValidTo = zstSoldToPricing.ValidTo.ToDate();
            SAPCondition.PricePer = zstSoldToPricing.PricePer.ToInt();
            SAPCondition.PricePerUnit = zstSoldToPricing.PricePerUnit;
            return SAPCondition;
        }

        private void InsertZEP1(ZstSoldToPricing zstSoldToPricing, SAPCondition SAPCondition)
        {
            SAPCharacteristicOption sapPricingGroup = (from co in this.SAPPricingGroups where co.Sapcode == zstSoldToPricing.PricingGroup select co).FirstOrDefault();
            ZEP1 ZEP1 = (from z in this.EpoxyPrices where z.SappricingGroupID == sapPricingGroup.SapcharacteristicOptionID && z.Currency == SAPCondition.Currency select z).FirstOrDefault();
            if (!ZEP1.IsNull())
            {
                todeleteListZEP1.Remove(ZEP1);
            }
            if (ZEP1.IsNull())
            {
                ZEP1 = new ZEP1();
                ZEP1.SappricingGroupID = sapPricingGroup.SapcharacteristicOptionID;
                db.Zep1s.Add(ZEP1);
            }
            ZEP1.Rate = SAPCondition.Rate;
            ZEP1.Currency = SAPCondition.Currency;
            ZEP1.Per = SAPCondition.PricePer;
            ZEP1.Unit = SAPCondition.PricePerUnit;
            ZEP1.ValidFrom = SAPCondition.ValidFrom;
            ZEP1.ValidTo = SAPCondition.ValidTo;
        }

        private void InsertZG01(ZstSoldToPricing zstSoldToPricing, SAPCondition SAPCondition)
        {
            ZG01 ZG01;
            SAPShipTo SAPShipTo = (from s in db.SapshipTos where s.Number == zstSoldToPricing.ShipToNumber select s).FirstOrDefault();
            SAPCharacteristicOption gXtra = (from t in this.SAPTubeStandards where t.Sapcode == zstSoldToPricing.MaterialGroup select t).FirstOrDefault();
            if (!SAPShipTo.IsNull())
            {
                ZG01 = (from g in this.GradeXtras
                        where g.SapsoldtoID == SAPShipTo.SapshipToID && g.Currency == SAPCondition.Currency
                            && g.SaptubeStandardID == gXtra.SapcharacteristicOptionID
                        select g).FirstOrDefault();
                if (!ZG01.IsNull())
                {
                    this.todeleteListZG01.Remove(ZG01);
                }
            }
            else
            {
                ZG01 = (from g in this.GradeXtras where g.SaptubeStandardID == gXtra.SapcharacteristicOptionID && g.Currency == SAPCondition.Currency select g).FirstOrDefault();
                if (!ZG01.IsNull())
                {
                    this.todeleteListZG01.Remove(ZG01);
                }
            }
            if (ZG01.IsNull())
            {
                ZG01 = new ZG01();
                ZG01.SapsoldtoID = !SAPShipTo.IsNull() ? SAPShipTo.SapshipToID : 0;
                ZG01.SaptubeStandardID = gXtra.SapcharacteristicOptionID;
                db.Zg01s.Add(ZG01);
            }
            ZG01.Rate = SAPCondition.Rate;
            ZG01.Currency = SAPCondition.Currency;
            ZG01.Per = SAPCondition.PricePer;
            ZG01.Unit = SAPCondition.PricePerUnit;
            ZG01.ValidFrom = SAPCondition.ValidFrom;
            ZG01.ValidTo = SAPCondition.ValidTo;
        }

        private void InsertZR01(ZstSoldToPricing zstSoldToPricing, long SAPShipToID, SAPCondition SAPCondition)
        {
            SAPCharacteristicOption sapPricingGroup = (from co in this.SAPPricingGroups where co.Sapcode == zstSoldToPricing.PricingGroup select co).FirstOrDefault();
            if (!sapPricingGroup.IsNull())
            {
                sapPricingGroup.SAPCondition = SAPCondition;
                ZR01 zr = (from a in this.Adders where a.sapcharacteristicOption.Sapcode == sapPricingGroup.Sapcode select a).FirstOrDefault();
                if (!zr.IsNull())
                {
                    todeleteListZR01.Remove(zr);
                }
                if (zr.IsNull())
                {
                    zr = new ZR01();
                    zr.SapsoldtoID = SAPShipToID;
                    db.Zr01s.Add(zr);
                }

                zr.SappricingGroupID = sapPricingGroup.SapcharacteristicOptionID;
                zr.Rate = SAPCondition.Rate;
                zr.Currency = SAPCondition.Currency;
                zr.Per = SAPCondition.PricePer;
                zr.Unit = SAPCondition.PricePerUnit;
                zr.ValidFrom = SAPCondition.ValidFrom;
                zr.ValidTo = SAPCondition.ValidTo;
            }
        }

        private void InsertZR00(SAPCharacteristicOption SAPMaterialPricingGroup, long SAPShipToID, SAPCondition SAPCondition)
        {
            ZR00 zr = (from c in this.corePrices where c.sapcharacteristicOption.Sapcode == SAPMaterialPricingGroup.Sapcode select c).FirstOrDefault();
            if (!zr.IsNull())
            {
                todeleteListZR00.Remove(zr);
            }
            if (zr.IsNull())
            {
                zr = new ZR00();
                zr.SapsoldtoID = SAPShipToID;
                db.Zr00s.Add(zr);
            }

            zr.SapmaterialPricingGroupID = SAPMaterialPricingGroup.SapcharacteristicOptionID;
            zr.Rate = SAPCondition.Rate;
            zr.Currency = SAPCondition.Currency;
            zr.Per = SAPCondition.PricePer;
            zr.Unit = SAPCondition.PricePerUnit;
            zr.ValidFrom = SAPCondition.ValidFrom;
            zr.ValidTo = SAPCondition.ValidTo;
        }

        private void InsertZR04(ZstSoldToPricing zstSoldToPricing, long SAPShipToID, SAPCondition SAPCondition)
        {
            SAPCharacteristicOption SAPMaterialGroup = (from co in this.AllSAPMaterialGroups where co.Sapcode == zstSoldToPricing.MaterialGroup select co).FirstOrDefault();
            ZR04 zr = (from m in this.materialGroupAdders where m.SapsoldtoID == SAPShipToID && m.sapcharacteristicOption.Sapcode == SAPMaterialGroup.Sapcode select m).FirstOrDefault();
            if (!zr.IsNull())
            {
                todeleteListZR04.Remove(zr);
            }
            if (zr.IsNull())
            {
                zr = new ZR04();
                zr.SapsoldtoID = SAPShipToID;
                zr.SapmaterialGroupID = SAPMaterialGroup.SapcharacteristicOptionID;
                db.Zr04s.Add(zr);
            }
            zr.Rate = SAPCondition.Rate;
            zr.Currency = SAPCondition.Currency;
            zr.Per = SAPCondition.PricePer;
            zr.Unit = SAPCondition.PricePerUnit;
            zr.ValidFrom = SAPCondition.ValidFrom;
            zr.ValidTo = SAPCondition.ValidTo;
        }

        private void InsertFreightandFSC(ZstSoldToPricing zstSoldToPricing, long SAPSoldToID, long SAPShipToID, SAPCondition SAPCondition)
        {
            FreightandFsc fsc = (from f in this.FreightFSCcharges
                                 where f.SapshiptoID == SAPShipToID
                                     && f.SapsoldtoID == SAPSoldToID 
                                     && f.Sapcondition.Sapcode == SAPCondition.Sapcode
                                 select f).FirstOrDefault();
            if (!fsc.IsNull())
            {
                this.todeleteListFreightFSCcharges.Remove(fsc);
            }
            if (fsc.IsNull())
            {
                fsc = new FreightandFsc();
                fsc.SapshiptoID = SAPShipToID;
                fsc.SapsoldtoID = SAPSoldToID;
                fsc.SapconditionID = SAPCondition.SapconditionID;
                db.FreightandFscs.Add(fsc);
            }
            fsc.Rate = SAPCondition.Rate;
            fsc.Currency = SAPCondition.Currency;
            fsc.Per = SAPCondition.PricePer;
            fsc.Unit = SAPCondition.PricePerUnit;
            fsc.ValidFrom = SAPCondition.ValidFrom;
            fsc.ValidTo = SAPCondition.ValidTo;
            if (zstSoldToPricing.RegionalFreight == "X" && SAPCondition.Sapcode == "ZF02")
            {
                fsc.RegionalFreight = true;
            }
        }

        public void GetDeltaPricechangesFromAtlasSAP()
        {
            var conditionGroups = (from cg in db.SapconditionGroups where cg.DivisionID == (long)Enums.Divisions.Atlas select cg).ToList();
            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            AtlasSAPPortal.ZPricingPortalAtl getDeltaPricing = new AtlasSAPPortal.ZPricingPortalAtl();
            getDeltaPricing.ImConditions = new KschlSt[] { };
            getDeltaPricing.ImPricingDate = DateTime.Today;

            sapPortalService.Open();
            AtlasSAPPortal.ZPricingPortalAtlResponse getSoldToPricingResponse = sapPortalService.ZPricingPortalAtlAsync(getDeltaPricing);
            sapPortalService.Close();

            foreach (ZstPricingPortal zstDeltaPricing in getSoldToPricingResponse.ExPricingData.Where(x => !string.IsNullOrEmpty(x.ConditionType)))
            {
                SAPCondition SAPCondition = (from c in db.Sapconditions where c.Sapcode == zstDeltaPricing.ConditionType select c).FirstOrDefault();
                SAPCondition.Sapcode = zstDeltaPricing.ConditionType;
                SAPCondition.Rate = zstDeltaPricing.Rate;
                SAPCondition.Currency = zstDeltaPricing.RateUnit;
                SAPCondition.RateUnit = zstDeltaPricing.RateUnit;
                SAPCondition.ValidFrom = zstDeltaPricing.ValidFrom.ToDate();
                SAPCondition.ValidTo = zstDeltaPricing.ValidTo.ToDate();
                SAPCondition.PricePer = zstDeltaPricing.PricePer.ToInt();
                SAPCondition.PricePerUnit = zstDeltaPricing.PricePerUnit;

                SAPSoldTo SAPSoldTo = (from s in db.SapshipTos.OfType<SAPSoldTo>() where s.Number == zstDeltaPricing.SoldTo select s).FirstOrDefault();
                SAPConditionGroup regionSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstDeltaPricing.PricingRegion select cg).FirstOrDefault();
                SAPConditionGroup tierSAPConditionGroup = (from cg in conditionGroups where cg.Sapcode == zstDeltaPricing.Tier select cg).FirstOrDefault();
                SAPCharacteristicOption SAPMaterialPricingGroup = (from co in this.SAPMaterialPricingGroups where co.Sapcode == zstDeltaPricing.MaterialPricingGroup select co).FirstOrDefault();
                if (!SAPMaterialPricingGroup.IsNull() && SAPCondition.Sapcode == "ZR00")
                {
                    SAPMaterialPricingGroup.SAPCondition = SAPCondition;
                    if (!SAPSoldTo.IsNull())
                    {
                        InsertDeltaZR00(SAPCondition, SAPSoldTo, SAPMaterialPricingGroup);
                    }
                    if (!regionSAPConditionGroup.IsNull() || !tierSAPConditionGroup.IsNull())
                    {
                        var soltoList = (from s in db.SapshipTos.OfType<SAPSoldTo>()
                                         where s.Active == true && s.DivisionID == (long)Enums.Divisions.Atlas
                                         && (s.RegionSapconditionGroupID == regionSAPConditionGroup.SapconditionGroupID
                                         || s.TierSapconditionGroupID == tierSAPConditionGroup.SapconditionGroupID)
                                         && !s.CustomerSpecificPricing
                                         select s).ToList();
                        foreach (SAPSoldTo soldTo in soltoList)
                        {
                            InsertDeltaZR00(SAPCondition, soldTo, SAPMaterialPricingGroup);
                        }
                    }
                }

                SAPShipTo SAPShipTo = (from s in db.SapshipTos where s.Number == zstDeltaPricing.ShipToNumber select s).FirstOrDefault();
                if (!SAPShipTo.IsNull())
                {
                    if (SAPCondition.Sapcode == "ZF02" || SAPCondition.Sapcode == "ZF00" || SAPCondition.Sapcode == "PROP" || SAPCondition.Sapcode == "ZFSC")
                    {
                        InsertDeltaFreightandFSC(zstDeltaPricing, SAPCondition, SAPShipTo, zstDeltaPricing.ShipToNumber);
                    }
                }
                else
                {
                    switch (SAPCondition.Sapcode)
                    {
                        case "ZR04":
                            InsertDeltaZR04(zstDeltaPricing, SAPCondition, SAPSoldTo);
                            break;
                        case "ZF02":
                            if (!SAPSoldTo.IsNull())
                            {
                                foreach (SAPShipTo shipto in SAPSoldTo.sapshipTos)
                                {
                                    InsertDeltaFreightandFSC(zstDeltaPricing, SAPCondition, shipto, shipto.Number);
                                }
                            }
                            if (!regionSAPConditionGroup.IsNull() || !tierSAPConditionGroup.IsNull())
                            {
                                var soltoList = (from s in db.SapshipTos.OfType<SAPSoldTo>()
                                                 where s.Active == true && s.DivisionID == (long)Enums.Divisions.Atlas
                                                 && (s.RegionSapconditionGroupID == regionSAPConditionGroup.SapconditionGroupID
                                                 || s.TierSapconditionGroupID == tierSAPConditionGroup.SapconditionGroupID)
                                                 && !s.CustomerSpecificPricing
                                                 select s).ToList();
                                foreach (SAPSoldTo soldTo in soltoList)
                                {
                                    foreach (SAPShipTo shipto in soldTo.sapshipTos)
                                    {
                                        InsertDeltaFreightandFSC(zstDeltaPricing, SAPCondition, shipto, shipto.Number);
                                    }
                                }
                            }
                            break;
                    }
                }

                SAPCharacteristicOption sapPricingGroup = (from co in this.SAPPricingGroups where co.Sapcode == zstDeltaPricing.PricingGroup select co).FirstOrDefault();
                if (!sapPricingGroup.IsNull() && SAPCondition.Sapcode == "ZR01")
                {
                    sapPricingGroup.SAPCondition = SAPCondition;
                    if (!SAPSoldTo.IsNull())
                    {
                        InsertDeltaZR01(SAPCondition, SAPSoldTo, sapPricingGroup);
                    }
                    if (!regionSAPConditionGroup.IsNull() || !tierSAPConditionGroup.IsNull())
                    {
                        var soltoList = (from s in db.SapshipTos.OfType<SAPSoldTo>()
                                         where s.Active == true && s.DivisionID == (long)Enums.Divisions.Atlas
                                         && (s.RegionSapconditionGroupID == regionSAPConditionGroup.SapconditionGroupID
                                         || s.TierSapconditionGroupID == tierSAPConditionGroup.SapconditionGroupID)
                                         && !s.CustomerSpecificPricing
                                         select s).ToList();
                        foreach (SAPSoldTo soldTo in soltoList)
                        {
                            InsertDeltaZR01(SAPCondition, soldTo, sapPricingGroup);
                        }
                    }
                }
            }
            db.SaveChanges();
        }

        private void InsertDeltaZR04(ZstPricingPortal zstDeltaPricing, SAPCondition SAPCondition, SAPSoldTo SAPSoldTo)
        {
            SAPCharacteristicOption SAPMaterialGroup = (from co in this.AllSAPMaterialGroups where co.Sapcode == zstDeltaPricing.MaterialGroup select co).FirstOrDefault();
            ZR04 mgAdder = (from m in db.Zr04s where m.SapsoldtoID == SAPSoldTo.SapshipToID && m.sapcharacteristicOption.Sapcode == SAPMaterialGroup.Sapcode select m).FirstOrDefault();
            if (mgAdder.IsNull())
            {
                mgAdder = new ZR04();
								mgAdder.SapsoldtoID = SAPSoldTo.SapshipToID;
                mgAdder.SapmaterialGroupID = SAPMaterialGroup.SapcharacteristicOptionID;
                db.Zr04s.Add(mgAdder);
            }
            mgAdder.Rate = SAPCondition.Rate;
            mgAdder.Currency = SAPCondition.Currency;
            mgAdder.Per = SAPCondition.PricePer;
            mgAdder.Unit = SAPCondition.PricePerUnit;
            mgAdder.ValidFrom = SAPCondition.ValidFrom;
            mgAdder.ValidTo = SAPCondition.ValidTo;
        }

        private void InsertDeltaZR00(SAPCondition SAPCondition, SAPSoldTo SAPSoldTo, SAPCharacteristicOption SAPMaterialPricingGroup)
        {
            var Core = (from c in db.Zr00s where c.SapsoldtoID == SAPSoldTo.SapshipToID && c.sapcharacteristicOption.Sapcode == SAPMaterialPricingGroup.Sapcode select c).FirstOrDefault();
            if (Core.IsNull())
            {
                Core = new ZR00();
								Core.SapsoldtoID = SAPSoldTo.SapshipToID;
                db.Zr00s.Add(Core);
            }
            Core.SapmaterialPricingGroupID = SAPMaterialPricingGroup.SapcharacteristicOptionID;
            Core.Rate = SAPCondition.Rate;
            Core.Currency = SAPCondition.Currency;
            Core.Per = SAPCondition.PricePer;
            Core.Unit = SAPCondition.PricePerUnit;
            Core.ValidFrom = SAPCondition.ValidFrom;
            Core.ValidTo = SAPCondition.ValidTo;
       
		}
			
        private void InsertDeltaZR01(SAPCondition SAPCondition, SAPSoldTo SAPSoldTo, SAPCharacteristicOption sapPricingGroup)
        {
            ZR01 Adder = (from a in db.Zr01s where a.SapsoldtoID == SAPSoldTo.SapshipToID && a.sapcharacteristicOption.Sapcode == sapPricingGroup.Sapcode select a).FirstOrDefault();

            if (Adder.IsNull())
            {
                Adder = new ZR01();
                Adder.SapsoldtoID = SAPSoldTo.SapshipToID;
                db.Zr01s.Add(Adder);
            }

            Adder.SappricingGroupID = sapPricingGroup.SapcharacteristicOptionID;
            Adder.Rate = SAPCondition.Rate;
            Adder.Currency = SAPCondition.Currency;
            Adder.Per = SAPCondition.PricePer;
            Adder.Unit = SAPCondition.PricePerUnit;
            Adder.ValidFrom = SAPCondition.ValidFrom;
            Adder.ValidTo = SAPCondition.ValidTo;
        }

        private void InsertDeltaFreightandFSC(ZstPricingPortal zstDeltaPricing, SAPCondition SAPCondition, SAPShipTo SAPShipTo, string shipNumber)
        {
            FreightandFsc fsc = (from f in db.FreightandFscs where f.Sapshipto.Number.TrimStart('0') == shipNumber.TrimStart('0') && f.Sapcondition.Sapcode == SAPCondition.Sapcode select f).FirstOrDefault();
            if (fsc.IsNull())
            {
                fsc = new FreightandFsc();
                fsc.SapshiptoID = SAPShipTo.SapshipToID;
                fsc.SapconditionID = SAPCondition.SapconditionID;
                db.FreightandFscs.Add(fsc);
            }
            fsc.Rate = SAPCondition.Rate;
            fsc.Currency = SAPCondition.Currency;
            fsc.Per = SAPCondition.PricePer;
            fsc.Unit = SAPCondition.PricePerUnit;
            fsc.ValidFrom = SAPCondition.ValidFrom;
            fsc.ValidTo = SAPCondition.ValidTo;
            if (zstDeltaPricing.RegionalFreight == "X")
            {
                fsc.RegionalFreight = true;
            }
        }

        //Sridhar Override Method for GetPricingFromAtlasSAP 
        //using Parallel Task execution for performance and 
        public void GetPricingFromAtlasSAPMultiProcess(string email)
        {
            var data = db.SapshipTos.OfType<SAPSoldTo>().Where(s => s.Active == true && s.DivisionID == (long)Enums.Divisions.Atlas);
            var soltoList = data.AsEnumerable().Select((a, index) => new { SoldTo = a }).ToList();
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 4
            };
            Parallel.ForEach(soltoList, options, (soldtoItem, pls, index) =>
            {
                var sapPricingObj = new SAPPricing() { SAPSoldTo = soldtoItem.SoldTo };
                UpdatePricefromAtlasSAP(index, sapPricingObj);
                index++;
            });
        }

        private static void UpdatePricefromAtlasSAP(long counter, SAPPricing sapPricingObj)
        {
            sapPricingObj.SAPMaterialGroups = new List<SAPCharacteristicOption>();
            sapPricingObj.todeleteListZR00 = new List<ZR00>();
            sapPricingObj.todeleteListZR01 = new List<ZR01>();
            sapPricingObj.todeleteListZR04 = new List<ZR04>();
            sapPricingObj.todeleteListZEP1 = new List<ZEP1>();
            sapPricingObj.todeleteListZG01 = new List<ZG01>();
            sapPricingObj.todeleteListFreightFSCcharges = new List<FreightandFsc>();

            string gradXtras = (counter == 0) ? "X" : "";
            if (!sapPricingObj.SAPSoldTo.IsNull())
            {
                sapPricingObj.SAPSoldTo.CoreSAPPricingCondition = new SAPCondition();
                sapPricingObj.SAPSoldTo.PropSAPPricingCondition = new SAPCondition();
                sapPricingObj.corePrices = (from core in sapPricingObj.db.Zr00s where core.SapsoldtoID == sapPricingObj.SAPSoldTo.SapshipToID select core).ToList();
                sapPricingObj.todeleteListZR00.AddRange(sapPricingObj.corePrices);

                sapPricingObj.Adders = (from adder in sapPricingObj.db.Zr01s where adder.SapsoldtoID == sapPricingObj.SAPSoldTo.SapshipToID select adder).ToList();
                sapPricingObj.todeleteListZR01.AddRange(sapPricingObj.Adders);

                sapPricingObj.materialGroupAdders = (from mgAdder in sapPricingObj.db.Zr04s where mgAdder.SapsoldtoID == sapPricingObj.SAPSoldTo.SapshipToID select mgAdder).ToList();
                sapPricingObj.todeleteListZR04.AddRange(sapPricingObj.materialGroupAdders);

                sapPricingObj.EpoxyPrices = (from epoxy in sapPricingObj.db.Zep1s select epoxy).ToList();
                sapPricingObj.todeleteListZEP1.AddRange(sapPricingObj.EpoxyPrices);

                sapPricingObj.GradeXtras = (from gxtra in sapPricingObj.db.Zg01s select gxtra).ToList();
                sapPricingObj.todeleteListZG01.AddRange(sapPricingObj.GradeXtras);

                var shipids = sapPricingObj.SAPSoldTo.sapshipTos.Select(x => x.SapshipToID).ToList();
                sapPricingObj.FreightFSCcharges = sapPricingObj.db.FreightandFscs.Where(ff => ff.SapsoldtoID == sapPricingObj.SAPSoldTo.SapshipToID &&  shipids.Contains(ff.SapshiptoID)).ToList();
                sapPricingObj.todeleteListFreightFSCcharges.AddRange(sapPricingObj.FreightFSCcharges);

                sapPricingObj.GetAllPricefromAtlasSAP(gradXtras);
            }
        }

        public void Dispose()
        {
            SAPSoldTo = null;
            SAPShipTo = null;
            AllSAPMaterialGroups = null;
            SAPMaterialPricingGroups = null;
            SAPTubeStandards = null;
            corePrices = null;
            todeleteListZR00 = null;
            Adders = null;
            todeleteListZR01 = null;
            materialGroupAdders = null;
            todeleteListZR04 = null;
            EpoxyPrices = null;
            todeleteListZEP1 = null;
            GradeXtras = null;
            todeleteListZG01 = null;
            FreightFSCcharges = null;
            todeleteListFreightFSCcharges = null;
            SAPPricingGroups = null;
            SAPMaterialGroups = null;
            db = null;
        }
    }
}
