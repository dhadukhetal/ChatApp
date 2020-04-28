using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using ChatApi.Models;

namespace ChatApi.UTL
{
    public static class TaskUTL
    {
        public static ApiResponse UnAuthorizedAccess(ApiResponse apiResponse)
        {
            apiResponse.IsValidUser = false;
            apiResponse.MessageType = 0;
            apiResponse.Message = "UnAuthorized User";
            apiResponse.DataList = null;
            return apiResponse;
        }

        public static ApiResponse CheckUpdate(bool isValidUser, bool isUpdateAvailable, string token, object dataList)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.IsValidUser = isValidUser;
            if (isValidUser && !string.IsNullOrEmpty(token))
            {
                apiResponse.Token = token;
                apiResponse.DataList = dataList;
                if (isUpdateAvailable)
                {
                    apiResponse.MessageType = 1;
                    apiResponse.Message = "Update available";
                }
                else
                {
                    apiResponse.MessageType = 3;
                    apiResponse.Message = "No update currently available";
                }
            }
            else
            {
                apiResponse.MessageType = 0;
                apiResponse.Message = "Unauthorized user!";
                apiResponse.DataList = null;
            }
            
            return apiResponse;
        }

        public static ApiResponse GenerateApiResponse()
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.IsValidUser = false;
            apiResponse.MessageType = 0;
            apiResponse.Message = "Unauthorized User";
            apiResponse.DataList = null;
            return apiResponse;
        }
        public static ApiResponse GenerateApiResponse(bool IsValidUser, int MessageType, string Message, object DataList)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.IsValidUser = IsValidUser;
            apiResponse.MessageType = MessageType;
            apiResponse.Message = Message.ToString();
            apiResponse.DataList = DataList;
            return apiResponse;
        }

        public static ApiResponse GenerateExceptionResponse(Exception ex, string PageName, bool IsValidUser)
        {
            var apiResponse = new ApiResponse();
            var strExType = ex.GetType().Name;
            //GeneralMessages generalMessages = new GeneralMessages(PageName);

            if (strExType == "DbUpdateException")
            {
                var sqlException = ex.GetBaseException() as SqlException;
                if (sqlException != null)
                {
                    var number = sqlException.Number;
                    if (number == 547)  //For Cascading Delete Exception
                        apiResponse = TaskUTL.GenerateApiResponse(true, 2, "We can't delete this {0} because of some child entry using this record", null);
                    else if (number == 2627) //Exception For Duplicate Entry
                        apiResponse = TaskUTL.GenerateApiResponse(true, 2, "This {0} is already exists", null);
                    else
                        apiResponse = TaskUTL.GenerateApiResponse(true, 0, "An error occured while processing your request in {0} page", null);
                }
            }
            else
            {
                apiResponse = TaskUTL.GenerateApiResponse(true, 0, ex.Message.ToString(), null);
            }
            return apiResponse;
        }
        public static Boolean MoveFile(String SourceFilePath, String DestinationFilePath)
        {
            if (System.IO.File.Exists(SourceFilePath))
            {
                try
                {
                    System.IO.File.Move(SourceFilePath, DestinationFilePath);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean DeleteFile(String SourceFilePath)
        {
            if (System.IO.File.Exists(SourceFilePath))
            {
                try
                {
                    File.Delete(SourceFilePath);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}