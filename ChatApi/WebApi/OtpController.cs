using ChatApi.Models;
using ChatApi.Repository;
using ChatApi.UTL;
using ChatApi.vmModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;
namespace ChatApi.WebApi
{
    public class OtpController : ApiController
    {
        string _pageName = string.Empty;

        public OtpController()
        {
            _pageName = "Otp";
        }

        [HttpPost]
        public async Task<ApiResponse> ValidateOtp(OtpValidationVM _vm)
        {
            ApiResponse apiResponse = new ApiResponse();
            OtpRepo _repo = new OtpRepo();
            try
            {
                var result = await _repo.CheckOTP(_vm);
                if (result._flag == 1)
                {
                    return new ApiResponse { IsValidUser = true, DataList = result._dataList, Message = "success", MessageType = result._flag };
                }
                else if (result._flag == 2)
                {
                    return new ApiResponse { IsValidUser = true, Message = "InValid OTP", MessageType = result._flag };
                }
                else
                {
                    return new ApiResponse { IsValidUser = true, Message = "Somthing went Wrong..!!", MessageType = result._flag };
                }
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }

        }
    }
}