using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BracPGWApi.Models
{
    public class ProcessResultVm
    {
        public string transactionStatus { get; set; }
        public string postDate { get; set; }
        public string transactioreference { get; set; }
        public string trackId { get; set; }
        public string transactionId { get; set; }
        public string transactionAmt { get; set; }
        public string paymentid { get; set; }
        public string ECI { get; set; }
        public string cardNo { get; set; }
        public string issuerRespCode { get; set; }
        public string authCode { get; set; }
    }
}