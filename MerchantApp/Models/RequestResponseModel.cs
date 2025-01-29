using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantApp.Models
{
    public class RequestResponseModel
    {
        
    }

    public class BenefitRequest
    {
        public string id { get; set; }
        public string trandata { get; set; }
    }

    public class BenefitResponse
    {
        public string status { get; set; }
        public string result { get; set; }
        
    }

}

