using ChatApi.Models;
using ChatApi.Repository;
using ChatApi.UTL;
using ChatApi.vmModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ChatApi.WebAPI
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthAPIController : ApiController
    {
        string _pageName = string.Empty;
        public AuthAPIController()
        {
            _pageName = "Login";
        }

        [HttpPost]
       
        public async Task<ApiResponse> CheckLogin(LoginVM objLoginModel)
        {
            ApiResponse apiResponse = new ApiResponse();
            LoginRepo _loginRepo = new LoginRepo();
            try
            {
                UserDetail objUserDetail = await _loginRepo.UserLogin(objLoginModel);
                if (objUserDetail.UserID > 0)
                {
                    apiResponse = TaskUTL.GenerateApiResponse(true, 1, "Login succeessful", objUserDetail);
                }
                else
                {
                    apiResponse = TaskUTL.GenerateApiResponse(false, 0, "Login failed. Please check your UserName and/or Password.", null);
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CheckUpdateDownload(UserUpdateCheck obj)
        {
            ApiResponse apiResponse = new ApiResponse();
            LoginRepo _loginRepo = new LoginRepo();
            try
            {
                HttpResponseMessage objUserUpdate = await _loginRepo.CheckUpdateDownload(obj);
                return objUserUpdate;
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return null;
            }
        }

        [HttpPost]
        public async Task<ApiResponse> CheckUpdate(UserUpdateCheck obj)
        {
            LoginRepo loginRepo = new LoginRepo();
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                UserUpdateRespCheck objCheckUpdate = await loginRepo.CheckUpdate(obj);
               
                if (objCheckUpdate.IsValidUser)
                {
                    apiResponse = TaskUTL.CheckUpdate(objCheckUpdate.IsValidUser, objCheckUpdate.IsUpdateAvailable,obj.Token, objCheckUpdate);
                }
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
            }
            return apiResponse;
        }
        [HttpPost]
        public async Task<ApiResponse> UpdateFiles(LoginVM objLoginModel)
        {
            ApiResponse apiResponse = new ApiResponse();
            LoginRepo _loginRepo = new LoginRepo();
            try
            {
                UserDetail objUserDetail = await _loginRepo.UserLogin(objLoginModel);
                if (objUserDetail.UserID > 0)
                {
                    apiResponse = TaskUTL.GenerateApiResponse(true, 1, "login succeessfully", objUserDetail);
                }
                else
                {
                    apiResponse = TaskUTL.GenerateApiResponse(false, 0, "Login failed. Please check your UserName and/or Password.", null);
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }
        }

        //
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<ApiResponse> OperatorLogin(LoginVM objLoginModel)
        {
            ApiResponse apiResponse = new ApiResponse();
            LoginRepo _loginRepo = new LoginRepo();
            try
            {
                OperatorDetail objUserDetail = await _loginRepo.OperatorLogin(objLoginModel);
                if (objUserDetail.OperatorId > 0)
                {
                    apiResponse = TaskUTL.GenerateApiResponse(true, 1, "Login succeessful", objUserDetail);
                }
                else
                {
                    apiResponse = TaskUTL.GenerateApiResponse(false, 0, "Login failed. Please check your UserName and/or Password.", null);
                }
                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }
        }
    }
}
