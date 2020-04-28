using ChatApi.vmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApi.Models
{
    public class ApiResponse
    {
        private bool _isValidUser;
        private int _messageType;
        private string _message = string.Empty;
        private object _dataList;
        private string _token = string.Empty;
        

        public ApiResponse()
        { }
        

        public bool IsValidUser { get; set; }
        public int MessageType { get; set; } //0:error, 1: success, 2: warning, 3: information 
        public string Message { get; set; }

        public string Token { get; set; }
        public object DataList { get; set; }
    }

    public class ChatReceiveResponse
    {
        public string error { get; set; }
        public int OperatorId { get; set; }
        public int UserId { get; set; }
        public int ChatSessionId { get; set; }
        public int ChatStatus { get; set; } //:1/0	--Chat On, Chat Off
        public object DataList { get; set; }
    }
}