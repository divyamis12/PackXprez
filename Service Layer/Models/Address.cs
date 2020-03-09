using System;
using System.Collections.Generic;

namespace Infosys.PackXperZ.Services.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string EmailId { get; set; }
        public string BuildingNo { get; set; }
        public string StreetName { get; set; }
        public string Locality { get; set; }
        public decimal? Pincode { get; set; }

    }
}
