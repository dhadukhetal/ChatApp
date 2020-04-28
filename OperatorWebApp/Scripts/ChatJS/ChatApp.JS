var IntervalVal;
$(function () {

    // Declare a proxy to reference the hub.

    var chatHub = $.connection.chatHub;
    registerClientMethods(chatHub);

    // Start Hub
    $.connection.hub.start().done(function () {
        registerEvents(chatHub);
    });

    function registerEvents(chatHub) {
        // Send Button Click Event
        var el = $("#txtMessage").emojioneArea();
        $('#hdUserName').val("Operator");
        $('#btnSendMsg').click(function () {

            var msg = $("#txtMessage").val();

            if (msg.length > 0) {

                //var userName = $('#hdUserName').val();

                var date = GetCurrentDateTime(new Date());

                chatHub.server.send("Operator", msg, date);

                //$("div.emojionearea-editor").data("emojioneArea").setText('');


                el[0].emojioneArea.setText(''); // clear input 
            }
        });

        // Send Message on Enter Button
        $("#txtMessage").keypress(function (e) {
            if (e.which == 13) {
                $('#btnSendMsg').click();
            }
        });
    }

    function registerClientMethods(chatHub) {

        //On New User Connected
        chatHub.client.onNewUserConnected = function (id, name, UserImage, loginDate) {
            AddUser(id, name, UserImage, loginDate);
        };

        chatHub.client.RecieveMessage = function (name, message, time, userimg) {
            //writeToLog(name + ":" + message);
            AddMessage(name, message, time, userimg);
        };
        
        chatHub.client.sendPrivateMessage = function (windowId, fromUserName, message, userimg, CurrentDateTime) {

            var ctrId = 'private_' + windowId;
            if ($('#' + ctrId).length == 0) {

                OpenPrivateChatBox(chatHub, windowId, ctrId, fromUserName, userimg);

            }

            var CurrUser = $('#hdUserName').val();
            var Side = 'right';
            var TimeSide = 'left';

            if (CurrUser == fromUserName) {
                Side = 'left';
                TimeSide = 'right';

            }
            else {
                var Notification = 'New Message From ' + fromUserName;
                IntervalVal = setInterval("ShowTitleAlert('SignalR Chat App', '" + Notification + "')", 800);

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
    }

    function AddUser(id, name, UserImage, date) {

        //var userId = $('#hdId').val();
        var code, Clist;
        code = $('<div class="box-comment">' +
            '<img class="img-circle img-sm" src="' + UserImage + '" alt="User Image" />' +
            ' <div class="comment-text">' +
            '<span class="username">' + name + '<span class="text-muted pull-right">' + date + '</span>  </span></div></div>');

        Clist = $(
            '<li>' +
            '<a href="#">' +
            '<img class="contacts-list-img" src="' + UserImage + '" alt="User Image" />' +

            ' <div class="contacts-list-info">' +
            '<span class="contacts-list-name" id="' + id + '">' + name + ' <small class="contacts-list-date pull-right">' + date + '</small> </span>' +
            ' <span class="contacts-list-msg">How have you been? I was...</span></div></a > </li >');

        var UserLink = $('<a id="' + id + '" class="user" >' + name + '<a>');
        $(code).click(function () {

            var id = $(UserLink).attr('id');


            var ctrId = 'private_' + id;
            OpenPrivateChatBox(chatHub, id, ctrId, name);



        });

        var link = $('<span class="contacts-list-name" id="' + id + '">');
        $(Clist).click(function () {

            var id = $(link).attr('id');


            var ctrId = 'private_' + id;
            OpenPrivateChatBox(chatHub, id, ctrId, name);



        });

        $("#divusers").append(code);

        $("#ContactList").append(Clist);

    }

    function AddMessage(userName, message, time, userimg) {

        var CurrUser = $('#hdUserName').val();
        var Side = 'right';
        var TimeSide = 'left';

        if (CurrUser == userName) {
            Side = 'left';
            TimeSide = 'right';

        }

        var divChat = '<div class="direct-chat-msg ' + Side + '">' +
            '<div class="direct-chat-info clearfix">' +
            '<span class="direct-chat-name pull-' + Side + '">' + userName + '</span>' +
            '<span class="direct-chat-timestamp pull-' + TimeSide + '"">' + time + '</span>' +
            '</div>' +

            ' <img class="direct-chat-img" src="' + userimg + '" alt="Message User Image">' +
            ' <div class="direct-chat-text" >' + message + '</div> </div>';

        $('#divChatWindow').append(divChat);

        var height = $('#divChatWindow')[0].scrollHeight;

        // Apply Slim Scroll Bar in Group Chat Box
        $('#divChatWindow').slimScroll({
            height: height
        });

        ParseEmoji('#divChatWindow');

    }

    function GetCurrentDateTime(now) {

        var localdate = dateFormat(now, "dddd, mmmm dS, yyyy, h:MM:ss TT");

        return localdate;
    }

    function ParseEmoji(div) {
        var input = $(div).html();

        var output = emojione.unicodeToImage(input);

        $(div).html(output);
    }

    function OpenPrivateChatBox(chatHub, userId, ctrId, userName) {

        var PWClass = $('#PWCount').val();

        if ($('#PWCount').val() == 'info')
            PWClass = 'danger';
        else if ($('#PWCount').val() == 'danger')
            PWClass = 'warning';
        else
            PWClass = 'info';

        $('#PWCount').val(PWClass);
        var div1 = ' <div class="col-md-4"> <div  id="' + ctrId + '" class="box box-solid box-' + PWClass + ' direct-chat direct-chat-' + PWClass + '">' +
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
                chatHub.server.sendPrivateMessage(userId, msg);
                $textBox.val('');
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
        var msgTextbox = $div.find("#txtPrivateMessage");
        $(msgTextbox).emojioneArea();
    }
});