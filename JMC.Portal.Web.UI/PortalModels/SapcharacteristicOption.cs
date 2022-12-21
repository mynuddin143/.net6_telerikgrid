using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcharacteristicOption
    {
        public SapcharacteristicOption()
        {
            DealsMaterialPricingGroups = new HashSet<DealsMaterialPricingGroup>();
            DealsPricingGroups = new HashSet<DealsPricingGroup>();
            PriceChangeRequestItemSapmaterialGroups = new HashSet<PriceChangeRequestItem>();
            PriceChangeRequestItemSapmaterialPricingGroups = new HashSet<PriceChangeRequestItem>();
            PriceChangeRequestItemSappricingGroups = new HashSet<PriceChangeRequestItem>();
            QuoteMaterialSapepoxycoatColors = new HashSet<QuoteMaterial>();
            QuoteMaterialSapkleenkoteColors = new HashSet<QuoteMaterial>();
            QuoteMaterialSapmaterialUnitOfMeasures = new HashSet<QuoteMaterial>();
            QuoteMaterialSapsalesInstructions = new HashSet<QuoteMaterial>();
            QuoteMaterialSapspecifications = new HashSet<QuoteMaterial>();
            QuoteMaterialSaptubeStandards = new HashSet<QuoteMaterial>();
            SapcustomerTexts = new HashSet<SapcustomerText>();
            SapmaterialSapalternateCoilIndicators = new HashSet<Sapmaterial>();
            SapmaterialSapepoxycoatColors = new HashSet<Sapmaterial>();
            SapmaterialSapkleenkoteColors = new HashSet<Sapmaterial>();
            SapmaterialSapmaterialGroups = new HashSet<Sapmaterial>();
            SapmaterialSapmaterialPricingGroups = new HashSet<Sapmaterial>();
            SapmaterialSapmaterialTypes = new HashSet<Sapmaterial>();
            SapmaterialSapmetalGrades = new HashSet<Sapmaterial>();
            SapmaterialSappricingGroups = new HashSet<Sapmaterial>();
            SapmaterialSapproductColorFinishes = new HashSet<Sapmaterial>();
            SapmaterialSapproductEndFinishEnergices = new HashSet<Sapmaterial>();
            SapmaterialSapproductEndFinishes = new HashSet<Sapmaterial>();
            SapmaterialSapproductGroups = new HashSet<Sapmaterial>();
            SapmaterialSapproductLines = new HashSet<Sapmaterial>();
            SapmaterialSapproductTypes = new HashSet<Sapmaterial>();
            SapmaterialSapsalesInstructions = new HashSet<Sapmaterial>();
            SapmaterialSapspecifications = new HashSet<Sapmaterial>();
            SapmaterialSaptubeShapes = new HashSet<Sapmaterial>();
            SapmaterialSaptubeStandards = new HashSet<Sapmaterial>();
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
            SapstockSapspecifications = new HashSet<Sapstock>();
            SapstockSaptubeStandards = new HashSet<Sapstock>();
            ShoppingCartSaprollings = new HashSet<ShoppingCartSaprolling>();
            Zep1s = new HashSet<Zep1>();
            Zg01s = new HashSet<Zg01>();
            Zr00s = new HashSet<Zr00>();
            Zr01s = new HashSet<Zr01>();
            Zr04s = new HashSet<Zr04>();
        }

        public long SapcharacteristicOptionId { get; set; }
        public long SapcharacteristicTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;
        public bool Active { get; set; }
        public int? SortOrder { get; set; }
        public long? ProductLineId { get; set; }

        public virtual ProductLine? ProductLine { get; set; }
        public virtual SapcharacteristicType SapcharacteristicType { get; set; } = null!;
        public virtual ICollection<DealsMaterialPricingGroup> DealsMaterialPricingGroups { get; set; }
        public virtual ICollection<DealsPricingGroup> DealsPricingGroups { get; set; }
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItemSapmaterialGroups { get; set; }
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItemSapmaterialPricingGroups { get; set; }
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItemSappricingGroups { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterialSapepoxycoatColors { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterialSapkleenkoteColors { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterialSapmaterialUnitOfMeasures { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterialSapsalesInstructions { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterialSapspecifications { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterialSaptubeStandards { get; set; }
        public virtual ICollection<SapcustomerText> SapcustomerTexts { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapalternateCoilIndicators { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapepoxycoatColors { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapkleenkoteColors { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapmaterialGroups { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapmaterialPricingGroups { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapmaterialTypes { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapmetalGrades { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSappricingGroups { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapproductColorFinishes { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapproductEndFinishEnergices { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapproductEndFinishes { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapproductGroups { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapproductLines { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapproductTypes { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapsalesInstructions { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSapspecifications { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSaptubeShapes { get; set; }
        public virtual ICollection<Sapmaterial> SapmaterialSaptubeStandards { get; set; }
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
        public virtual ICollection<Sapstock> SapstockSapspecifications { get; set; }
        public virtual ICollection<Sapstock> SapstockSaptubeStandards { get; set; }
        public virtual ICollection<ShoppingCartSaprolling> ShoppingCartSaprollings { get; set; }
        public virtual ICollection<Zep1> Zep1s { get; set; }
        public virtual ICollection<Zg01> Zg01s { get; set; }
        public virtual ICollection<Zr00> Zr00s { get; set; }
        public virtual ICollection<Zr01> Zr01s { get; set; }
        public virtual ICollection<Zr04> Zr04s { get; set; }
    }
}
