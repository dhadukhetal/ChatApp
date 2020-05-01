

var _IntervalVal;
//var _HubUrl = "http://localhost:58786/signalr";
var _HubUrl = "http://chatapi.saaction.in/signalr";
$(function () {
    //Reference to simpleHub proxy
    var chatHubProxy;
    var _Operator_Display_Name = $("#hdnDisplayName").val();
    var _chatSessionID = $("#hdnChatSessionId").val();
    var OperatorID = $("#hdnOperatorID").val();
    // get online user
    //alert(_Operator_Display_Name);
    function getOnlineUser() {
        
        $.ajax({
            type: "POST",
            url: "http://chatapi.saaction.in/api/Users/GetOnlineUserListByOperatorID",
            //url: "http://localhost:58786/api/Users/GetOnlineUserListByOperatorID",
            data: {
                OperatorID: $("#hdnOperatorID").val()
            },
            dataType: 'json',
            cache: false,
            async: true,
            success: function (data) {
                if (data.MessageType == 1) {
                    //console.log(data);
                    $.each(data.DataList, function (index, value) {
                       // console.log(value);
                        //AddUser(id, name, UserImage, date, chatHub)
                        //{UserId: 15, UserName: "Emma", FirstName: "Emma", LastName: "Stone"}
                        //example : AddUser(id, msg, "../images/dummy.png", senderid, chatHubProxy);
                        AddUser(value.UserId, value.UserName + ' ' + value.LastName, "../images/dummy.png", new Date().toLocaleString(), chatHubProxy, value.UserToken, value.ChatSessionID);
                    });
                }
                else {
                    $("#message").css('display', 'block');
                    $("#message").addClass('alert alert-danger alert-dismissible');
                    $("#message").html(data.Message);

                    if (data.MessageType == 0) {
                        $('#btnSignIn').attr('disabled', false);
                        $("#message").delay(4000).fadeOut();
                    }
                }
            },
            error: function () {
            }
        });

    }
  

    //Connect to the SignalR server and get the proxy for chatHub
    function connect() {

        $.getScript(_HubUrl + "/hubs", function () {

            $.connection.hub.url = _HubUrl;

            // Declare a proxy to reference the hub.
            chatHubProxy = $.connection.chatHub;

            //Reigster to the "AddMessage" callback method of the hub
            //This method is invoked by the hub
            chatHubProxy.client.addMessage = function (name, message) {
                writeToLog(name + ":" + message);
            };

            //Connect to hub  
            $.connection.hub.start().done(function () {

                writeToLog("Connected.");
                getOnlineUser();
                try {
                    // chatHubProxy.server.groupconnect("Operator 1",1, "_connectid", "555");
                } catch (e) { alert(e.message); }


                //chatHubProxy.server.setUserName($('#txtUserName').val());
            });

            chatHubProxy.client.dbAlert = function (_resp) {
                alert(_resp);
            };

            chatHubProxy.client.receiveMessage = function (msgFrom, msg, sender,datetime) {
                alert(msgFrom);
                writeToLog("msgFrom :" + msgFrom + " || senderid : " + sender + " || Message : " + msg + " || Date : " + datetime);
                //AddMessage(windowId, fromUserName, message, userimg, CurrentDateTime)
                AddMessage(sender, msgFrom, msg, "../images/dummy.png", datetime);
            };

        });


    }
    connect();


    function AddUser(_userId, _UserName, UserImage, date, chatHub, UserToken, ChatSessionID) {

        //var userId = $('#hdId').val();
        var code, Clist;
        code = $('<div class="box-comment">' +
            ' <img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
            ' <div class="comment-text">' +
            ' <span class="username">' + _UserName + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');

        Clist = $(
            '<li>' +
            '<a href="#">' +
            '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +

            ' <div class="contacts-list-info">' +
            ' <span class="contacts-list-name" id="' + UserToken + '">' + _UserName + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
            ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');

        var UserLink = $('<a id="' + UserToken + '" class="user" >' + _UserName + '<a>');

        $(code).click(function () {

            var id = $(UserLink).attr('id');


            var ctrId = 'private_' + id;
            OpenPrivateChatBox(chatHub, _userId, ctrId, _UserName,UserToken);



        });

        var link = $('<span class="contacts-list-name" id="' + UserToken + '">');

        $(Clist).click(function () {

            var id = $(link).attr('id');


            var ctrId = 'private_' + id;

            OpenPrivateChatBox(chatHub, _userId, ctrId, _UserName,UserToken);

        });

        $("#divusers").append(code);

        $("#ContactList").append(Clist);

    }

    function OpenPrivateChatBox(chatHub, userId, ctrId, userName, userToken) {

        var PWClass = $('#PWCount').val();

        if ($('#PWCount').val() == 'info')
            PWClass = 'danger';
        else if ($('#PWCount').val() == 'danger')
            PWClass = 'warning';
        else
            PWClass = 'info';

        $('#PWCount').val(PWClass);
        var div1 = ' <div class="col-md-4"> <div  id="' + ctrId + '" data-username="' + userName +'" data-token="'+ userToken +'" class="box box-solid box-' + PWClass + ' direct-chat direct-chat-' + PWClass + '">' +
            '<div class="box-header with-border">' +
            ' <h3 class="box-title">' + userName + '</h3>' +
           
            ' <div class="box-tools pull-right">' +
            ' <span data-toggle="tooltip" id="MsgCountP" title="0 New Messages" class="badge bg-' + PWClass + '">0</span>' +
            ' <button type="button" class="btn btn-box-tool" data-widget="collapse">' +
            '    <i class="fa fa-minus"></i>' +
            '  </button>' +
            '  <button id="imgDelete" type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button></div></div>' +

            ' <div class="box-body">' +
            ' <div id="divMessage" class="direct-chat-messages">' +

            ' </div>' +

            '  </div>' +
            '  <div class="box-footer">' +


            '    <input type="text" id="txtPrivateMessage" name="message" placeholder="Type Message ..." class="form-control"  />' +

            '  <div class="input-group">' +
            '    <input type="text" name="message" placeholder="Type Message ..." class="form-control" style="visibility:hidden;" />' +
            '   <span class="input-group-btn">' +
            '          <input type="button" id="btnSendMessage" class="btn btn-' + PWClass + ' btn-flat" value="send" />' +
            '          <button id="btnFile" class="btn btn-default btn-flat"><i class="glyphicon glyphicon-paperclip"></i></button>' +
            '   </span>' +


            '  </div>' +

            ' </div>' +
            ' </div></div>';



        var $div = $(div1);

        // Closing Private Chat Box
        $div.find('#imgDelete').click(function () {
            $('#' + ctrId).remove();
        });

        // Send Button event in Private Chat
        $div.find("#btnSendMessage").click(function () {

            
            $textBox = $div.find("#txtPrivateMessage");

            var msg = $textBox.val();
            if (msg.length > 0) {
                //chatHub.server.sendPrivateMessage(userId, msg);
                //BroadCastMessage1(string UserName, string OperatorName, int OperatorId, int UserId, int ChatSessionId, string Message, int MessageSentBy, int MessageType, int AttachmentType, string Token)
                //chatHubProxy.server.broadCastMessage(_Operator_Display_Name, msg, userId);
                //chatHubProxy.server.broadCastMessage1(userName, _Operator_Display_Name, OperatorID, userId, _chatSessionID, msg, 1, 0, userToken);
                var _chatvm = {
                    _operatorId: OperatorID,
                    _userId: userId,
                    _chatSessionId: _chatSessionID,
                    _message: msg,
                    _flag: 0,
                    _messageSentBy: 1,
                    _messageType: 0,
                    _attachmentType: 0,
                    _filePath: '',
                    _token: userToken,
                    _userName: _Operator_Display_Name
                };
                chatHubProxy.server.BroadCastMessage(_chatvm);

                //$textBox.val('');
            }
        });

        // Text Box event on Enter Button
        $div.find("#txtPrivateMessage").keypress(function (e) {
            if (e.which == 13) {
                $div.find("#btnSendMessage").click();
            }
        });

        // Clear Message Count on Mouse over           
        $div.find("#divMessage").mouseover(function () {

            $("#MsgCountP").html('0');
            $("#MsgCountP").attr("title", '0 New Messages');
        });

        // Append private chat div inside the main div
        $('#PriChatDiv').append($div);
        chatHubProxy.server.groupconnect(userName, userId, "_connectid", userToken);

        //chatHubProxy.server.groupconnect("Operator 1", 1, "_connectid", "555");
        var msgTextbox = $div.find("#txtPrivateMessage");
        $(msgTextbox).emojioneArea();
    }

    function AddMessage(windowId, fromUserName, message, userimg, CurrentDateTime) {

        var ctrId = 'private_' + windowId;
        if ($('#' + ctrId).length == 0) {
            // OpenPrivateChatBox(chatHub, userId, ctrId, userName, userToken)
            OpenPrivateChatBox(chatHub, windowId, ctrId, fromUserName, "");

        }

//        var CurrUser = $('#hdUserName').val();
        var Side = 'right';
        var TimeSide = 'left';

        if (_Operator_Display_Name == fromUserName) {
            Side = 'left';
            TimeSide = 'right';

        }
        else {
            var Notification = 'New Message From ' + fromUserName;
            //IntervalVal = setInterval("ShowTitleAlert('SignalR Chat App', '" + Notification + "')", 800);

            var msgcount = $('#' + ctrId).find('#MsgCountP').html();
            msgcount++;
            $('#' + ctrId).find('#MsgCountP').html(msgcount);
            $('#' + ctrId).find('#MsgCountP').attr("title", msgcount + ' New Messages');
        }

        var divChatP = '<div class="direct-chat-msg ' + Side + '">' +
            '<div class="direct-chat-info clearfix">' +
            '<span class="direct-chat-name pull-' + Side + '">' + fromUserName + '</span>' +
            '<span class="direct-chat-timestamp pull-' + TimeSide + '"">' + CurrentDateTime + '</span>' +
            '</div>' +

            ' <img class="direct-chat-img" src="' + userimg + '" alt="Message User Image">' +
            ' <div class="direct-chat-text" >' + message + '</div> </div>';

        $('#' + ctrId).find('#divMessage').append(divChatP);

        // Apply Slim Scroll Bar in Private Chat Box
        var ScrollHeight = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
        $('#' + ctrId).find('#divMessage').slimScroll({
            height: ScrollHeight
        });
    }

    //Write given text to log area
    function writeToLog(log) {
        $("#txtLog").append(log + "&#10;&#13;");
    }

    function ShowTitleAlert(newMessageTitle, pageTitle) {
        if (document.title == pageTitle) {
            document.title = newMessageTitle;
        }
        else {
            document.title = pageTitle;
        }
    }
});