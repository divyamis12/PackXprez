using System;
using System.Collections.Generic;

namespace Infosys.PackXperZ.Services.Models
{
    public  class CustomerDetails
    {
      

        public string EmailId { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public int AddressId { get; set; }
        public string BuildingNo { get; set; }
        public string StreetName { get; set; }
        public string Locality { get; set; }
        public decimal? Pincode { get; set; }
    }
}
