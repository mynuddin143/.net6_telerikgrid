using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JMC.Portal.Business
{
    public class Enums
    {
        public enum Applications
        {
            Portal = 1,
            ATCCustomerPortal = 2,
            WTCPipeportal = 3
        }

        public enum ApplicationRoles
        {
            Admin = 1,
            Sales = 2,
            Traffic = 3,
            Scrap = 4,
            Executive = 5,
            MasterData = 6,
            PriceChangeAdmin = 7,
            PriceChangeApprover = 8,
            RegionalManager = 9,
            ISRSupervisor = 10,
            OSR = 11,
            ISR = 12,
            ScrapAdmin = 13,
            CreateScrapDelivery = 14,
            JMCPortalCreateScrapSale = 15,
            ATCPortalCreateOrders = 16,
            ATCPortalViewDelivery = 17,
            ATCPortalViewOrder = 18,
            ATCPortalCreateRelease = 19,
            ATCPortalViewRelease = 20,
            ATCPortalViewActivityLog = 22,
            ATCProductInquiry = 25,
            ATCSAPBacklogRefresh = 29,
            JMCPortalATCEcommerceAdmin = 26,
            JMCPortalATCEcommerce = 27,
            WTCPipeEcommerce = 36,
            WTCMTRSearch = 37,
            PriceChangeReceiver = 45
        }

        public enum Divisions
        {
            JMC = 1,
            Atlas = 2,
            Wheatland = 3,
            EnergeX = 4
        }

        public enum Plants
        {
            Harrow = 1,
            ChicagoAtlas = 2,
            Blytheville = 3,
            Plymouth = 4,
            Winnipeg = 5,
            Council = 7,
            Warren = 10,
            Mill = 11,
            Cambridge = 12,
            Church = 13,
            ChicagoWheatland = 8,
            EnergeXCasing = 71,
            EnergeXFinishing = 72,
            Welland = 73,
            PicomaCambridge = 74,
            //LongBeach = 78
            LongBeach = 118,
            Birmingham = 77
        }

        public enum AtlasSAPCharacteristicTypes
        {
            AlternateCoilIndicator = 1,
            EpoxycoatColor = 2,
            KleenkoteColor = 3,
            MaterialGroup = 4,
            MaterialType = 5,
            MaterialPricingGroup = 6,
            MaterialUnitOfMeasure = 7,
            PricingGroup = 8,
            SalesInstruction = 9,
            Specification = 10,
            TubeShape = 11,
            TubeStandard = 12
        }

        public enum AtlasTubeShapes
        {
            Rectangle = 725,
            Round = 726,
            Square = 727
        }

        public enum WheatlandSAPCharacteristicTypes
        {
            MaterialGroup = 13,
            MaterialType = 14,
            ProductLine = 15,
            ProductGroup = 16,
            ProductType = 17,
            ProductColorFinish = 18,
            ProductEndFinish = 19,
            CustomerTextType = 20,
            ProductEndFinishEnergex = 21,
            MetalGrade = 22,
            PaymentTerms = 23,
            Specification = 24,
        }

        public enum DeliveryMethods
        {
            Truck = 1,
            Rail = 2,
            CPUTruck = 3,
            CPURail = 4
        }

        public enum DeliveryTypes
        {
            SalesDelivery = 1,
            SLocToPlant = 2,
            Subcontracted = 3,
            Transfer = 4
        }

        public enum DocymentTypes
        {
            RollingSchedules = 1,
            CapabilitiesBundling = 2,
            MSDS = 3,
            ConflictMaterials = 4,
            RecycledContent = 5,
            ISO = 6,
            Brochures = 7,
            Other = 8
        }

        public enum DataViews
        {
            OpenOrderSummary = 1,
            OpenOrderStatus = 2,
            OpenDeliveries = 3,
            Stock = 4,
            OrderHistory = 5,
            DeliveryHistory = 6,
            SalesOrderBacklog = 7,
            TodaysDeliveries = 8,
            EnrouteDeliveries = 9,
            DeliveredDeliveries = 10
        }

        public enum NonConformityType
        {
            ShippingError = 29
        }
        public enum Department
        {
            LessThanHundredWheatland = 46,
            LessThanHundredPicoma = 47,
            LessThanHundredChicago = 49
        }

        public enum Source
        {
            JMC,
            ATC
        }

        public enum DealType
        {
            Override = 0,
            Discount = 1
        }

        public enum DealTypeText
        {
            ZR02 = 0,
            ZKA0 = 1
        }

        public enum SubmittedApprovedDenied
        {
            Submitted = 1,
            Approved = 2,
            Denied = 3,
            History = 4,
            Modified = 5
        }
    }
}