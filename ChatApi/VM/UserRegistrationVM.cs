using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ChatApi.vmModels
{
    public class UserRegistrationVM
    {
        public int _userId { get; set; }

        [Required]
        public string _userName { get; set; }

        [Required]
        public string _firstName { get; set; }

        [Required]
        public string _lastName { get; set; }

        [Required]
        public string _password { get; set; }

        public string _phone { get; set; }

        [EmailAddress]
        public string _emailId { get; set; }

        public string _city { get; set; }

        public string _state { get; set; }

    }
    public class UserRegistrationResp
    {
        public int ResultType { get; set; }
        public string Result { get; set; }
        public string UpdateUrl { get; set; }
    }

}