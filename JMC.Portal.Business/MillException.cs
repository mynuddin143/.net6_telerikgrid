using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business
{
    public partial class MillException
    {        
        public bool CanAccept(SAPMaterial SAPMaterial, decimal length)
        {
            bool isSizeBetween = false; bool isGaugeBetween = false;
            if ((SAPMaterial.Diameter ?? 0) > 0)
            {
                if ((SAPMaterial.Diameter ?? 0) >= (this.MinSize ?? 0) && (SAPMaterial.Diameter ?? 0) <= (this.MaxSize ?? 1000)) isSizeBetween = true;
            }
            else
            {
                if (SAPMaterial.Size.HasValue && SAPMaterial.Size2.HasValue)
                {
                    if (SAPMaterial.Size.Value >= (this.MinSize ?? 0) && (SAPMaterial.Size2.Value <= (this.MaxSize ?? 1000))) isSizeBetween = true;
                }  
            }
            if (SAPMaterial.GaugeRestrictable.HasValue)
            {
                if (SAPMaterial.GaugeRestrictable.Value >= (this.MinGauge ?? 0) && SAPMaterial.GaugeRestrictable.Value <= (this.MaxGauge ?? 100)) isGaugeBetween = true;
            }
            if (length == 0)
                return isGaugeBetween && isSizeBetween;

            if (isGaugeBetween && isSizeBetween)
            {
                return (length >= (this.MinLength ?? 0) && length <= (this.MaxLength ?? 1000));
            }
            return true;
        }
    }
}
