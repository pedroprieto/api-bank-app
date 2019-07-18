using System;
using System.Collections.Generic;

namespace api_bank_app.Models
{
    public class Client
    {
        public long Id { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string phone { get; set; }
    }
}
