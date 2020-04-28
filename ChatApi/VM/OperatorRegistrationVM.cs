using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApi.vmModels
{
    public class OperatorRegistrationVM
    {
        public int _operatorID { get; set; }
        public string _operatorName { get; set; }
        public string _emailAddress { get; set; }
        public string _contactNo { get; set; }
        public string _userName { get; set; }
        public string _password { get; set; }
        public bool _isActive { get; set; }

    }
}