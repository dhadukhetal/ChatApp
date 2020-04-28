using ChatApi.Filters;
using ChatApi.Models;
using ChatApi.Repository;
using ChatApi.ServiceContract;
using ChatApi.UTL;
using ChatApi.vmModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ChatApi.WebApi
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserRegistrationController : ApiController
    {
        string _pageName = string.Empty;
        private IUserRegistration _IuserRegistration;
        public UserRegistrationController()
        {
            _pageName = "User Registration";
        }

        [HttpPost]
        [ModelValidateFilter]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //public async Task<ApiResponse> CreateUpdate(UserRegistrationVM _vmEmployee)
        public async Task<IHttpActionResult> CreateUpdate(UserRegistrationVM _vmEmployee)
        {
            ApiResponse apiResponse = new ApiResponse();
            _IuserRegistration = new UserRegisterRepo();
            try
            {
                //Int32 Result = await _IuserRegistration.CreateUpdate(_vmEmployee);
                //return new ApiResponse { IsValidUser = true, Message = string.Empty, MessageType = Result };
                IHttpActionResult objResult = await _IuserRegistration.CreateUpdate(_vmEmployee);
                
                return objResult;
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return null;
            }

        }
    }
}
