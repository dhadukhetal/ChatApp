using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public void BroadCastMessage(String msgFrom, String msg, String GroupName)
        {
            var id = Context.ConnectionId;
            string[] Exceptional = new string[0];
            // chatHubProxy.client.receiveMessage = function (msgFrom, msg, sender,datetime)
            Clients.Group(GroupName, Exceptional).receiveMessage(msgFrom, msg, GroupName,DateTime.Now.ToString());
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
    }
 }