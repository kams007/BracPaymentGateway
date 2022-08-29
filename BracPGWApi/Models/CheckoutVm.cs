using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BracPGWApi.Models
{
    public class CheckoutVm
    {
        public string trackId { get; set; }
        public string amount { get; set; }
        public string action { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string returnUrl { get; set; }
    }
}