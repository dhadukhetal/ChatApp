using System;

namespace ChatApi.vmModels
{
    public class ChatVM
    {
        public int _operatorId { get; set; }
        public int _userId { get; set; }
        public int _chatSessionId { get; set; }
        public string _message { get; set; }
        public int _flag { get; set; }
        public DateTime _lastChatReadTime { get; set; }
        // New Added Parameter
        public int _messageSentBy { get; set; }  // 1:Operator, 2:Client
        public int _messageType { get; set; }    // 0:No Attachment,1:Attachment
        public int _attachmentType { get; set; } // 0:Doc,1:image,2:video,3:other
        public string _filePath { get; set; }   // /ChessionId/fileName.ext(path format )
        public string _token { get; set; }   // /ChessionId/fileName.ext(path format )
    }
}