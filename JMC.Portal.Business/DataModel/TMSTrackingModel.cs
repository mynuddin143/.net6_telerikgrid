using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace JMC.Portal.Business.DataModel
{
    public class TMSTrackingModel
    {
        public string Origin
        {
            get
            {
                return this.Events.Last().ToLastKnownLocation();
            }
        }
        public string Destination
        {
            get
            {
                return this.Events.First().ToLastKnownLocation();
            }
        }

        public List<Event> Events { get; set; }

        public string GMapApiKey { get; set; }

        public void Process(string culture)
        {

        }
    }
    public class Event
    {
        public string Delivery { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string SightingCity { get; set; }
        public string SightingState { get; set; }
        public DateTime SightingTime { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public int Miles { get; set; }
        public string DeliveryDate { get; set; }
        public string Tracking { get; set; }

        public string ToDeliveryDate()
        {
            DateTime dateValue;
            if (DateTime.TryParse(DeliveryDate, out dateValue))
                return dateValue.ToString("g");
            return string.Empty;
        }

        public bool IsInValidStatus
        {
            get { return Status == "Error" || Status == "Unknown"; }
        }       

        public string ToDeliveryStatus(string tmsurl, string culture)
        {
            if (IsInValidStatus) return "Tracking Not Available";
            var returnStr = GetCulturedMiles(culture);
            if (IsDelivered) returnStr = "Delivered";
            if (!string.IsNullOrWhiteSpace(tmsurl))
                return string.Format("<a href=\"#\" onclick=\"javascript:closeModal();viewTMSTrackingModal('" + tmsurl + "')\" class=\"googleLink\">" + returnStr + "</a> ", Delivery); 
            return returnStr;
        }

        private string GetCulturedMiles(string culture)
        {
            var miles = string.Format("{0} miles away", Miles);
            if (culture == "CAD")
                miles = string.Format("{0} Kms away", Math.Round(Miles * 1.60934, 2));
            return miles;
        }

        public string ToDeliveryLocation()
        {
            return (IsInValidStatus) ? string.Empty : string.Format("{0},{1}", DestinationCity, DestinationState);
        }

        public string ToLastKnownLocation()
        {
            return (IsInValidStatus) ? string.Empty : string.Format("{0},{1}", SightingCity, SightingState);
        }

        public string ToLastKnownDateTime()
        {
            return (IsInValidStatus) ? string.Empty : SightingTime.ToString("g"); 
        }

        public string ToLastKnownDate()
        {
            return (IsInValidStatus) ? string.Empty : SightingTime.ToString("d");
        }

        public string ToLastKnownTime()
        {
            return (IsInValidStatus) ? string.Empty : SightingTime.ToString("t");
        }    

        public bool IsDelivered
        {
            get { return !string.IsNullOrWhiteSpace(DeliveryDate) || Status.ToUpper() == "DELIVERED"; }
        }
    }
}
