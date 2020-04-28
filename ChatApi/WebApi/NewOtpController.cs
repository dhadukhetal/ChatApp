using ChatApi.Models;
using ChatApi.Repository;
using ChatApi.ServiceContract;
using ChatApi.UTL;
using ChatApi.vmModels;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatApi.WebApi
{
    public class NewOtpController : ApiController
    {
        string _pageName = string.Empty;
        private INewOtp _INewOTP;
        public NewOtpController()
        {
            _pageName = "New Otp";
        }

        [HttpPost]
        public async Task<ApiResponse> GetNewOTP(NewOtpVM _vmNewOtp)
        {
            ApiResponse apiResponse = new ApiResponse();
            _INewOTP = new NewOtpRepo();
            try
            {
                var result = await _INewOTP.GetNewOTP(_vmNewOtp);
                return new ApiResponse { IsValidUser = true, Message = string.Empty, MessageType = 1, DataList = result };
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }

        }
    }
}