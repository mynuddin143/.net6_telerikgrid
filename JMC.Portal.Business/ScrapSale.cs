using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using JMC.Portal.Business.AtlasSAPPortal;
using JMC.Portal.Business.WheatlandPortal;
using System.Configuration;

namespace JMC.Portal.Business
{
    public partial class ScrapSale
    {
        public ICollection<SAPSalesOrderItem> SAPSalesOrderItems { get; set; }

        public string Name
        {
            get { return "#" + this.ScrapSaleID + " " + this.Date.ToString("MMM d, yyyy h:mmtt") + " - " + (this.ScrapSapsoldToID > 0 ? "(Scrap) " + this.ScrapSapsoldTo.Name : "(Secondary) " + this.RandomLengthSapsoldTo.Name); }
        }

        public string DisplayNameLinkHTML
        {
            get
            {
                return "<a href=\"OpenSales.aspx?ScrapRandomLengthSaleID=" + this.ScrapSaleID + "\" tabindex=\"-1\">#" + this.ScrapSaleID.ToString() + "&nbsp;" + this.Date.ToString("MMM dd, yyyy h:mm tt") + "</a>";
            }
        }

        public decimal? NetWeight
        {
            get { return this.GrossWeight - this.TareWeight; }
        }

        public string BoxString
        {
            get
            {
                ArrayList boxListArray = new ArrayList();

                foreach (Box box in this.Boxes)
                {
                    boxListArray.Add(box.Name);
                }

                return string.Join(", ", (string[])boxListArray.ToArray(typeof(string)));
            }
        }

        public bool Consignment
        {
            get
            {
                return (!this.SAPSalesOrderItem.SapsalesOrder.IsNull() && this.SAPSalesOrderItem.SapsalesOrder is SAPScrapOrder) ? ((SAPScrapOrder)this.SAPSalesOrderItem.SapsalesOrder).Consignment : false;
            }
        }

        public bool InformationIsComplete
        {
            get
            {
                return this.TareWeight > 0 && this.GrossWeight > 0 && this.SAPSalesOrderItems.Count() > 0;
            }
        }

        public bool CanStillCreateDelivery
        {
            get
            {
                return this.InformationIsComplete && (this.SapscrapDeliveryID.IsNullOrZero() || this.SapscrapDelivery.Number.ToInt() <= 0);
            }
        }

        public bool CreatedSAPDelivery
        {
            get
            {
                return (!this.SapscrapDeliveryID.IsNullOrZero() && this.SapscrapDelivery.Number.ToInt() > 0);
            }
        }

        public bool Complete
        {
            get
            {
                return (this.SapscrapDeliveryID > 0 && this.SapscrapDelivery.Number.ToInt() > 0);
            }
        }

        public SAPSoldTo SAPSoldTo
        {
            get
            {
                if (this.ScrapSapsoldToID > 0)
                {
                    return this.ScrapSapsoldTo.SapsoldTo;
                }
                else if (this.RandomLengthSapsoldToID > 0)
                {
                    return this.RandomLengthSapsoldTo.SapsoldTo;
                }

                return new SAPSoldTo();
            }
        }

        public SAPSalesOrderItem SAPSalesOrderItem
        {
            get
            {
                return this.SAPSalesOrderItems.FirstOrDefault() ?? new SAPSalesOrderItem();
            }
        }

        public string CreateScrapDelivery(ref PortalEntities db)
        {
            decimal availableToPromise = 0;
            decimal inventoryRequired = 0;
            string unitOfMeasure = string.Empty;
            string alreadyChangedSalesOrder = this.WeightAddedToSapsalesOrder.ToBool() ? "X" : " ";
            string deliveryNumber = (this.SapscrapDeliveryID > 0 && this.SapscrapDelivery.Number.Trim().ToInt() > 0) ? this.SapscrapDelivery.Number : string.Empty;
            string changedSalesOrder = string.Empty;

            switch (this.Plant.DivisionID)
            {
                case (long)Enums.Divisions.Atlas:
                    deliveryNumber = this.CreateATCScrapDelivery(ref db, ref availableToPromise, ref inventoryRequired, ref unitOfMeasure);

                    break;

                case (long)Enums.Divisions.Wheatland:
                case (long)Enums.Divisions.EnergeX:
                    deliveryNumber = this.CreateWTCScrapDelivery(ref db, ref availableToPromise, ref inventoryRequired, ref unitOfMeasure, alreadyChangedSalesOrder, ref changedSalesOrder);

                    this.WeightAddedToInventory = inventoryRequired;

                    if (this.WeightAddedToSapsalesOrder != true)
                    {
                        this.WeightAddedToSapsalesOrder = changedSalesOrder.ToBool();
                    }

                    break;
            }

            db.SaveChanges();

            if (inventoryRequired > 0)
            {
                StringBuilder emailToSB = new StringBuilder();
                StringBuilder emailbodySB = new StringBuilder();
                String emailCC = "christian.cooper@jmcsteel.com;zahir.kapadia@jmcsteelcom";
                string subject = string.Empty;

                if (this.Plant.ATPEmployees.Any())
                {
                    foreach (Employee employee in this.Plant.ATPEmployees)
                    {
                        emailToSB.Append(employee.Email.ToString().Trim() + ";");
                    }

                    emailbodySB.Append("Material Ordered: " + this.SAPSalesOrderItem.Sapmaterial.Number + "/ " + this.SAPSalesOrderItem.Sapmaterial.Name + " .<br />");
                    emailbodySB.Append(this.NetWeight.ToDecimal() + " " + unitOfMeasure + " ordered.<br />");
                    emailbodySB.Append(availableToPromise + " " + unitOfMeasure + " was available to promise.<br />");
                    emailbodySB.Append(inventoryRequired + unitOfMeasure + " was added to inventory for Plant " + this.Plant.Name + " to create delivery " + deliveryNumber + ".<br />");

                    if (Plant.DivisionID == (long)Enums.Divisions.Atlas)
                    {
                        subject = "Scrap Inventory update to Atlas Plant:" + Plant.Code;
                    }
                    else if (Plant.DivisionID == (long)Enums.Divisions.Wheatland)
                    {
                        subject = "Scrap Inventory update to Wheatland Plant:" + Plant.Code;
                    }
                }

                Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], emailToSB.ToString(), emailCC, subject, emailbodySB.ToString());
            }

            return deliveryNumber;
        }

        private string CreateATCScrapDelivery(ref PortalEntities db, ref decimal availableToPromise, ref decimal inventoryRequired, ref string unitOfMeasure)
        {
            string deliveryNumber = string.Empty;

            ZWS_PORTALClient sapPortalService = new ZWS_PORTALClient("ATLAS_ZWS_PORTAL");
            sapPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["AtlasSAPUserName"];
            sapPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["AtlasSAPPassword"];

            AtlasSAPPortal.ZCreateScrapDelivery ATCcreateScrapDelivery = new AtlasSAPPortal.ZCreateScrapDelivery();

            ATCcreateScrapDelivery.ImMaterialNumber = this.SAPSalesOrderItem.Sapmaterial.Number;
            ATCcreateScrapDelivery.ImPlant = this.Plant.Code;
            ATCcreateScrapDelivery.ImSalesOrderNumber = this.SAPSalesOrderItem.SapsalesOrder.Number;
            ATCcreateScrapDelivery.ImSalesOrderPosition = this.SAPSalesOrderItem.Position.ToString();
            ATCcreateScrapDelivery.ImStorageLocation = "SCRP";
            ATCcreateScrapDelivery.ImWeight = this.NetWeight.ToDecimal();
            ATCcreateScrapDelivery.ImTriedBeforeAndFailed = string.Empty;
            ATCcreateScrapDelivery.ImBolHeaderText = "#" + this.ScrapSaleID + ":" + this.GrossWeight + "-" + this.TareWeight + "=" + this.NetWeight + "lbs";

            if (this.PlantID == (long)Enums.Plants.Harrow)
            {
                ATCcreateScrapDelivery.ImOutputDevice = "HLSS1";
            }
            else if (this.PlantID == (long)Enums.Plants.ChicagoAtlas)
            {
                ATCcreateScrapDelivery.ImOutputDevice = "CLSHPB";
            }

            if (this.SapscrapDeliveryID > 0 && this.SapscrapDelivery.Number.Trim().ToInt() <= 0 && this.WeightAddedToSapsalesOrder.ToBool())
            {
                ATCcreateScrapDelivery.ImTriedBeforeAndFailed = "X";
            }

            ATCcreateScrapDelivery.Output = new AtlasSAPPortal.Bapiret2[] { new AtlasSAPPortal.Bapiret2() };

            sapPortalService.Open();
            AtlasSAPPortal.ZCreateScrapDeliveryResponse ATCcreateScrapDeliveryResponse = sapPortalService.ZCreateScrapDeliveryAsync(ATCcreateScrapDelivery);
            sapPortalService.Close();

            deliveryNumber = ATCcreateScrapDeliveryResponse.ExDeliveryNumber;

            if (this.SapscrapDeliveryID.IsNullOrZero())
            {
                SAPShipment SAPShipment = new SAPShipment();
                SAPShipment.DivisionID = this.Plant.DivisionID;
                SAPShipment.Number = (deliveryNumber.jIsEmpty() ? "ScrapSale#" + this.ScrapSaleID.ToString() : deliveryNumber);
                SAPShipment.ActualGoodsMovementDate = DateTime.Today;
                SAPShipment.Scrap = true;
                SAPShipment.SapsoldToID = this.SAPSalesOrderItem.SapsalesOrder.SapsoldToID;
                SAPShipment.PlantID = this.PlantID;
                SAPShipment.SapdeliveryTypeID = (long)Enums.DeliveryTypes.SalesDelivery;
                SAPShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPUTruck;

                SAPScrapDelivery SAPScrapDelivery = new SAPScrapDelivery();
                SAPScrapDelivery.DivisionID = this.Plant.DivisionID;
                SAPScrapDelivery.Number = (deliveryNumber.jIsEmpty() ? "ScrapSale#" + this.ScrapSaleID.ToString() : deliveryNumber);
                SAPScrapDelivery.ActualGoodsMovementDate = DateTime.Today;
                SAPScrapDelivery.SAPSoldToID = this.SAPSalesOrderItem.SapsalesOrder.SapsoldToID;
                SAPScrapDelivery.PlantID = this.PlantID;
                SAPScrapDelivery.SAPShipment = SAPShipment;

                this.SapscrapDelivery = SAPScrapDelivery;

                db.SapscrapDeliveries.Add(SAPScrapDelivery);
            }
            else if (deliveryNumber.ToLong() > 0 && this.SapscrapDeliveryID > 0)
            {
                this.SapscrapDelivery.Number = deliveryNumber;
                this.SapscrapDelivery.SAPShipment.DeliveryNumber = deliveryNumber;
            }

            this.SapscrapDelivery.BAPIReturnMessage += "***" + DateTime.Now.ToString("MMM dd, yyyy h:mm tt") + "***";

            foreach (AtlasSAPPortal.Bapiret2 bapiRet2 in ATCcreateScrapDeliveryResponse.Output)
            {
                this.SapscrapDelivery.BAPIReturnMessage += bapiRet2.Message + "\r\n";

                if (bapiRet2.Message.ToLower().Contains("salesorder:standard order") && bapiRet2.Message.ToLower().Contains("has been saved"))
                {
                    this.WeightAddedToSapsalesOrder = true;
                }
            }

            return deliveryNumber;
        }

        private string CreateWTCScrapDelivery(ref PortalEntities db, ref decimal availableToPromise, ref decimal inventoryRequired, ref string unitOfMeasure, string alreadyChangedSalesOrder, ref string changedSalesOrder)
        {
            zws_portalClient wtcPortalService = new zws_portalClient("WHEATLAND_ZWS_PORTAL");
            wtcPortalService.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WheatlandSAPUserName"];
            wtcPortalService.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WheatlandSAPPassword"];
            string outputDevice;
            string deliveryNumber = (this.SapscrapDeliveryID > 0 && this.SapscrapDelivery.Number.Trim().ToInt() > 0) ? this.SapscrapDelivery.Number : string.Empty;

            WheatlandPortal.ZCreateScrapDelivery createScrapDelivery = new WheatlandPortal.ZCreateScrapDelivery();

            string materialNumber = this.SAPSalesOrderItem.Sapmaterial.Number;
            string plant = this.Plant.Code;
            string salesOrderNumber = this.SAPSalesOrderItem.SapsalesOrder.Number;
            string salesOrderPosition = this.SAPSalesOrderItem.Position.ToString();

            //string storageLocation = "0020";
            string storageLocation = this.Plant.SapstorageLocations.FirstOrDefault().Sapcode.ToString();
            //if (this.Plant.Code == "7010" || this.Plant.Code == "7015" || this.Plant.Code == "7100" || this.Plant.Code == "2100") {
            //   storageLocation = "0025";
            //} else {
            //   storageLocation = "0020";
            //}
            decimal weight = this.NetWeight.ToDecimal();
            string bolHeaderText = "#" + this.ScrapSaleID + ":" + this.GrossWeight + "-" + this.TareWeight + "=" + this.NetWeight + "lbs";

            if (this.PlantID == (long)Enums.Plants.ChicagoWheatland)
            {
                outputDevice = "ISH1";
            }
            else
            {
                outputDevice = "LOCAL";
            }

            createScrapDelivery.ImAlreadyChangedSalesOrder = alreadyChangedSalesOrder;
            createScrapDelivery.ImBolHeaderText = bolHeaderText;
            createScrapDelivery.ImDeliveryNumber = deliveryNumber;
            createScrapDelivery.ImMaterialNumber = materialNumber;
            createScrapDelivery.ImOutputDevice = outputDevice;
            createScrapDelivery.ImPlant = plant;
            createScrapDelivery.ImSalesOrderNumber = salesOrderNumber;
            createScrapDelivery.ImSalesOrderPosition = salesOrderPosition;
            createScrapDelivery.ImStorageLocation = storageLocation;
            createScrapDelivery.ImWeight = weight;

            createScrapDelivery.Output = new WheatlandPortal.Bapiret2[] { new WheatlandPortal.Bapiret2() };

            wtcPortalService.Open();
            WheatlandPortal.ZCreateScrapDeliveryResponse createScrapDeliveryResponse = wtcPortalService.ZCreateScrapDeliveryAsync(createScrapDelivery);
            wtcPortalService.Close();

            availableToPromise = createScrapDeliveryResponse.ExAvailableToPromise;
            changedSalesOrder = createScrapDeliveryResponse.ExChangedSalesOrder;
            deliveryNumber = createScrapDeliveryResponse.ExDeliveryNumber;
            inventoryRequired = createScrapDeliveryResponse.ExInventoryRequired;
            unitOfMeasure = createScrapDeliveryResponse.ExUom;

            if (this.SapscrapDeliveryID.IsNullOrZero())
            {
                SAPShipment SAPShipment = new SAPShipment();
                SAPShipment.DivisionID = this.Plant.DivisionID;
                SAPShipment.Number = (deliveryNumber.jIsEmpty() ? "ScrapSale#" + this.ScrapSaleID.ToString() : deliveryNumber);
                SAPShipment.ActualGoodsMovementDate = DateTime.Today;
                SAPShipment.Scrap = true;
                SAPShipment.SapsoldToID = this.SAPSalesOrderItem.SapsalesOrder.SapsoldToID;
                SAPShipment.PlantID = this.PlantID;
                SAPShipment.SapdeliveryTypeID = (long)Enums.DeliveryTypes.SalesDelivery;
                SAPShipment.DeliveryMethodID = (long)Enums.DeliveryMethods.CPUTruck;

                SAPScrapDelivery SAPScrapDelivery = new SAPScrapDelivery();
                SAPScrapDelivery.DivisionID = this.Plant.DivisionID;
                SAPScrapDelivery.Number = (deliveryNumber.jIsEmpty() ? "ScrapSale#" + this.ScrapSaleID.ToString() : deliveryNumber);
                SAPScrapDelivery.ActualGoodsMovementDate = DateTime.Today;
                SAPScrapDelivery.SAPSoldToID = this.SAPSalesOrderItem.SapsalesOrder.SapsoldToID;
                SAPScrapDelivery.PlantID = this.PlantID;
                SAPScrapDelivery.SAPShipment = SAPShipment;

                this.SapscrapDelivery = SAPScrapDelivery;

                db.SapscrapDeliveries.Add(SAPScrapDelivery);
            }
            else if (deliveryNumber.ToLong() > 0 && this.SapscrapDeliveryID > 0)
            {
                this.SapscrapDelivery.Number = deliveryNumber;
                this.SapscrapDelivery.SAPShipment.DeliveryNumber = deliveryNumber;
            }

            this.SapscrapDelivery.BAPIReturnMessage += "***" + DateTime.Now.ToString("MMM dd, yyyy h:mm tt") + "***";

            foreach (WheatlandPortal.Bapiret2 bapiRet2 in createScrapDeliveryResponse.Output)
            {
                if (!bapiRet2.Message.Contains("SHIPMENT") || (bapiRet2.Message.Contains("VW~178") && bapiRet2.Message.Contains("VW~861") && bapiRet2.Message.Contains("VW~482") && bapiRet2.Message.Contains("VW~515") && bapiRet2.Message.Contains("VW~488")))
                {
                    this.SapscrapDelivery.BAPIReturnMessage += bapiRet2.Message + "\r\n";
                }

                if (bapiRet2.Message.ToLower().Contains("salesorder:standard order") && bapiRet2.Message.ToLower().Contains("has been saved"))
                {
                    this.WeightAddedToSapsalesOrder = true;
                }
            }

            return deliveryNumber;
        }
    }
}