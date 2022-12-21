using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business
{
    internal class SSAPPricingHelper
    {
        private static SSAPPricingHelper obj;
        public List<SAPCharacteristicOption> AllSAPMaterialGroups { get; private set; }
        public List<SAPCharacteristicOption> SAPMaterialPricingGroups { get; private set; }
        public List<SAPCharacteristicOption> SAPTubeStandards { get; private set; }
        public List<SAPCharacteristicOption> SAPPricingGroups { get; private set; }
        // private constructor to force use of 
        // getInstance() to create Singleton object 
        private SSAPPricingHelper() { }

        public static SSAPPricingHelper getInstance()
        {
            if (obj == null)
            {
                var db = new PortalEntities();
                obj = new SSAPPricingHelper();
                obj.SAPMaterialPricingGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialPricingGroup select co).ToList();
                obj.SAPPricingGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.PricingGroup select co).ToList();
                obj.AllSAPMaterialGroups = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.MaterialGroup select co).ToList();
                obj.SAPTubeStandards = (from co in db.SapcharacteristicOptions where co.SapcharacteristicTypeID == (long)Enums.AtlasSAPCharacteristicTypes.TubeStandard select co).ToList();

            }
            return obj;
        }
    } 

}
