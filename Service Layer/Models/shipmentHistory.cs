using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.PackXperZ.Services.Models
{
    public class shipmentHistory
    {
        public int TransitionId { get; set; }
        public int? Awbnumber { get; set; }
        public string SenderAddr { get; set; }
        public string ReceiverAddr { get; set; }
        public string DeliveryStatus { get; set; }


    }
}
