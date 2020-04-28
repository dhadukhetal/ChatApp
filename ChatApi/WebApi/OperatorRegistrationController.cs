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
    public class OperatorRegistrationController : ApiController
    {
        string _pageName = string.Empty;
        private IOperatorRegistration _IOperatorRegistration;
        public OperatorRegistrationController()
        {
            _pageName = "Operator Registration";
        }

        [HttpPost]
        public async Task<ApiResponse> CreateUpdate(OperatorRegistrationVM _vmOperatorRegistration)
        {
            ApiResponse apiResponse = new ApiResponse();
            _IOperatorRegistration = new OperatorRegisterRepo();
            try
            {
                Int32 Result = await _IOperatorRegistration.CreateUpdate(_vmOperatorRegistration);
                return new ApiResponse { IsValidUser = true, Message = string.Empty, MessageType = Result };
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }

        }
    }
}
