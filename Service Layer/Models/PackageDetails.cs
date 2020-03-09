using System;
using System.Collections.Generic;

namespace Infosys.PackXperZ.Services.Models
{
    public  class PackageDetails
    {
        public int TransitionId { get; set; }
        
        public string EmailId { get; set; }
        public string SenderAddr { get; set; }
        public string ReceiverAddr { get; set; }
        public int PackageCost { get; set; }
        public bool? PackagingRequired { get; set; }
        public string DeliveryStatus { get; set; }
        public bool? Insurance { get; set; }

       
    }
}
