using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChatApi.Models;
using ChatApi.Repository;
using ChatApi.UTL;
using ChatApi.vmModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChatApi
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        [HubMethodName("BroadCastMessage")]
        public async Task BroadCastMessage(ChatVM _chatmsg)
        {
            var id = Context.ConnectionId;
            string[] Exceptional = new string[0];
            Clients.Group(_chatmsg._token, Exceptional).receiveMessage(_chatmsg._userName, _chatmsg._message, _chatmsg._token, DateTime.Now.ToString());
            var _result = await Chat_Sent(_chatmsg);
            Clients.Group(_chatmsg._token, Exceptional).dbAlert(_result);
        }
        public void BroadCastMessage(String msgFrom, String msg, String GroupName)
        {

            /*
             
            _model._operatorId = Convert.ToInt32(formData["OperatorId"].ToString());
            _model._userId = Convert.ToInt32(formData["UserId"].ToString());
            _model._chatSessionId = Convert.ToInt32(formData["ChatSessionId"].ToString());
            _model._message = formData["Message"].ToString();
            _model._flag = Convert.ToInt32(formData["flag"].ToString());
            _model._messageSentBy = Convert.ToInt32(formData["MessageSentBy"].ToString());
            _model._messageType = Convert.ToInt32(formData["MessageType"].ToString());
            _model._attachmentType = Convert.ToInt32(formData["AttachmentType"].ToString());
            _model._token = formData["Token"].ToString();
             
             
             */
            var id = Context.ConnectionId;
            string[] Exceptional = new string[0];
            // chatHubProxy.client.receiveMessage = function (msgFrom, msg, sender,datetime)
            Clients.Group(GroupName, Exceptional).receiveMessage(msgFrom, msg, GroupName, DateTime.Now.ToString());

            //Clients.Caller.receiveMessage(msgFrom, msg, GroupName, DateTime.Now.ToString());
            //Clients.All.receiveMessage(msgFrom, msg, "");
            /*string[] Exceptional = new string[1];
            Exceptional[0] = id;       
            Clients.AllExcept(Exceptional).receiveMessage(msgFrom, msg);*/
        }

        [HubMethodName("groupconnect")]
        public void Get_Connect(String username, String userid, String connectionid, String GroupName)
        {
            string count = "NA";
            string msg = "Welcome to group " + GroupName;
            string list = "";

            var id = Context.ConnectionId;
            Groups.Add(id, GroupName);

            string[] Exceptional = new string[1];
            Exceptional[0] = id;
            //function (msgFrom, msg, senderid,id)
            Clients.Caller.receiveMessage(username, msg, GroupName, DateTime.Now.ToString());
            Clients.OthersInGroup(GroupName).receiveMessage(username, msg, GroupName, DateTime.Now.ToString());
            //Clients.AllExcept(Exceptional).receiveMessage("NewConnection", username + " " + id, count);
        }

        public async Task<ApiResponse> Chat_Sent(ChatVM _vm)
        {
            ApiResponse apiResponse = new ApiResponse();
            ChatRepo _obj = new ChatRepo();
            try
            {
                _vm._flag = 0;
                var result = await _obj.ChatMessageSend(_vm);
                if (result > 0)
                    return new ApiResponse { IsValidUser = true, Message = "success", MessageType = result, DataList = result };
                else
                    return new ApiResponse { IsValidUser = true, Message = "someting went wrong..!!", MessageType = result, DataList = result };


            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, "hub", true);
                return apiResponse;
            }

        }

    }
}