using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApi.vmModels
{
    public class NewOtpVM
    {
        public int _userId { get; set; }
        public string _token { get; set; }
    }

    public class OtpValidationVM
    {
        public int _flag { get; set; }
        public int _otp { get; set; }
        public int _operatorId { get; set; }
        public int _userId { get; set; }
        public string _ipAddress { get; set; }
        public string _osVersion { get; set; }
        public string _otherDetail { get; set; }
        public string _exeType { get; set; }

    }

    public class RespRepoVM
    {
        public int _flag { get; set; }
        public Object _dataList { get; set; }
    }
}