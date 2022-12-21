using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business
{
    public partial class Plant : Location
    {

        public string DisplayName
        {
            get
            {
                //if (this.LocationID == 1)
                //  return "Harrow / Plymouth";
                return this.Name;
            }
        }

        public static new IQueryable<Plant> GetAllActive(ref PortalEntities db)
        {
            return GetAllActive(ref db, null);
        }

        public static new IQueryable<Plant> GetAllActive(ref PortalEntities db, long? divisionID)
        {
            if (divisionID > 0)
            {
                return db.Locations.OfType<Plant>().Where(d => d.Active == true && d.DivisionID == divisionID);
            }

            return db.Locations.OfType<Plant>().Where(d => d.Active == true);
        }

        public static new Dictionary<string, string> GetAllActiveDictionary(ref PortalEntities db, bool addBlankOption, long? divisionID)
        {
            Dictionary<string, string> dictionary = Plant.GetAllActive(ref db, divisionID).ToDictionary(d => d.LocationID.ToString(), d => d.Name);

            if (addBlankOption)
            {
                dictionary.Add(string.Empty, string.Empty);
            }

            return dictionary;
        }
        public bool HasAMillFor(SAPMaterial SAPMaterial, decimal LengthFeet, decimal LengthInch)
        {
            return HasAMillFor(SAPMaterial, LengthFeet + (LengthInch / 12M), "");
        }

        public bool HasAMillFor(SAPMaterial SAPMaterial, decimal length, string millName)
        {
            if (SAPMaterial.IsNull()) return false;

            foreach (var activeMill in this.Mills.Where(x => x.Active && (x.WorkCenter == millName || millName == "")))
            {
                if (MillCanAccept(activeMill, SAPMaterial, length))
                {
                    return true;
                }
            }
            return false;
        }

        private bool MillCanAccept(Mill mill, SAPMaterial SAPMaterial, decimal length)
        {
            var canAccept = CanMillExceptionAccept(mill.MillID, SAPMaterial, length);

            if (canAccept != null && !canAccept.GetValueOrDefault()) // if mill exception has any one record and can accept the size and length.
                return false;

            return mill.CanAccept(SAPMaterial, length);
        }        

        private bool? CanMillExceptionAccept(long millId, SAPMaterial SAPMaterial, decimal length)
        {
            PortalEntities db = new PortalEntities();
            var millExpections = db.MillExceptions.Where(x => x.MillID == millId).ToList();
            millExpections = millExpections.Where(x => x.MinGauge.Value <= SAPMaterial.GaugeRestrictable.Value && SAPMaterial.GaugeRestrictable.Value <= x.MaxGauge.Value).ToList();
            if (SAPMaterial.IsRound) 
            {
                millExpections = millExpections.Where(x => (x.MinSize.Value == SAPMaterial.Diameter.Value) && (x.MaxSize.Value == SAPMaterial.Diameter.Value)).ToList();
            }
            else
            {
                millExpections = millExpections.Where(x => (x.MinSize.Value == SAPMaterial.Size.Value) && (x.MaxSize.Value == SAPMaterial.Size2.Value)).ToList();
            }
            if (!millExpections.Any())
                return null;

            bool canAccept = true;
            foreach (MillException millException in millExpections)
            {
                canAccept = millException.CanAccept(SAPMaterial, length);
                if (canAccept)
                    break;
            }
            return canAccept;
        }
    }
}
