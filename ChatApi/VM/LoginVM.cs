using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ChatApi.vmModels
{
    public class LoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ExeType { get; set; }

        public string IpAddress { get; set; }
        public string OsVersion { get; set; }
        public string OtherDetail { get; set; }
        public int CurrentExeVersion { get; set; }
    }

    public class UserDetail
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string UpdateDuration { get; set; }
        public string ApiUrl { get; set; }
        public string Exes { get; set; }
        //      public string _latestVersion { get; set; }
        public string UpdateUrl { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int LatestVersion { get; set; }
        //public MultipartContent _fileContents { get; set; }
    }

    public class UserUpdateCheck
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public int CurrentVersion { get; set; }
    }

    public class UserUpdateResp
    {
        //   public string _latestVersion { get; set; }
        public bool IsValid { get; set; }
        public string ApiUrl { get; set; }
        public string _exes { get; set; }
        public string UpdateUrl { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int LatestVersion { get; set; }
    }

    public class UserUpdateRespCheck
    {
        public bool IsValidUser { get; set; }
        public int MessageType { get; set; }
        public bool IsUpdateAvailable { get; set; }
        public int LatestVersion { get; set; }
    }

    public class UpdateFileContents
    {
        public string FileName { get; set; }
        public byte[] FileContents { get; set; }
    }

    public class OperatorDetail
    {
        public int OperatorId { get; set; }
        public string DisplayName { get; set; }
        public string OperatorMaxUserConnectionLimit { get; set; }
        public string OnHoldDefaultMessage { get; set; }
        public string Token { get; set; }
    }
}