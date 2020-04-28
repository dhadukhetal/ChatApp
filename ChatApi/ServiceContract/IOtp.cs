using ChatApi.vmModels;
using System;
using System.Threading.Tasks;

namespace ChatApi.ServiceContract
{
    public interface IOtp
    {
        Int32 ValidateOTP(OtpValidationVM _vm);
        Task<RespRepoVM> CheckOTP(OtpValidationVM _vm);
    }
}
