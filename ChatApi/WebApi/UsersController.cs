using ChatApi.Models;
using ChatApi.Repository;
using ChatApi.UTL;
using ChatApi.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChatApi.WebApi
{
    public class UsersController : ApiController
    {
        string _pageName = string.Empty;
        
        public UsersController()
        {
            _pageName = "User API";
        }

        [HttpPost]
        public async Task<ApiResponse> GetOnlineUserListByOperatorID(UserVM _vmUser)
        {
            ApiResponse apiResponse = new ApiResponse();
            UserRepo _user = new UserRepo();
            try
            {
                var result = await _user.GetUserList(_vmUser.OperatorID);
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