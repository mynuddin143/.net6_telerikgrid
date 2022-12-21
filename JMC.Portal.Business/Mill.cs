using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business
{
    public partial class Mill
    {
        public bool CanAccept(SAPMaterial SAPMaterial, decimal length)
        {
            if ((length < (this.MinLength ?? 0) || length > (this.MaxLength ?? 1000))) return false;

            if ((SAPMaterial.Diameter ?? 0) > 0)
            {
                if ((SAPMaterial.Diameter ?? 0) < (this.MinSize ?? 0) || (SAPMaterial.Diameter ?? 0) > (this.MaxSize ?? 1000)) return false;
            }
            else
            {
                if (SAPMaterial.Size.HasValue)
                {
                    if (SAPMaterial.Size.Value < (this.MinSize ?? 0) || (SAPMaterial.Size.Value > (this.MaxSize ?? 1000))) return false;
                }
                if (SAPMaterial.Size2.HasValue)
                {
                    if (SAPMaterial.Size2.Value < (this.MinSize ?? 0) || (SAPMaterial.Size2.Value > (this.MaxSize ?? 1000))) return false;
                }
            }
            if (SAPMaterial.GaugeRestrictable.HasValue)
            {
                if (SAPMaterial.GaugeRestrictable.Value < (this.MinGauge ?? 0) || SAPMaterial.GaugeRestrictable.Value > (this.MaxGauge ?? 100)) return false;
            }
            return true;
        }
    }
}
