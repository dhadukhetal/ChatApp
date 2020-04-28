using ChatApi.Models;
using ChatApi.Repository;
using ChatApi.UTL;
using ChatApi.vmModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
namespace ChatApi.WebApi
{
    public class ChatController : ApiController
    {
        string _pageName = string.Empty;

        public ChatController()
        {
            _pageName = "Chat";
        }

        [HttpPost]
        public async Task<ApiResponse> ChatSend_drop(ChatVM _vm)
        {
            ApiResponse apiResponse = new ApiResponse();
            ChatRepo _obj = new ChatRepo();
            try
            {
                _vm._flag = 0;
                var result = await _obj.ChatSend(_vm);
                if (result > 0)
                    return new ApiResponse { IsValidUser = true, Message = "success", MessageType = result, DataList = result };
                else
                    return new ApiResponse { IsValidUser = true, Message = "someting went wrong..!!", MessageType = result, DataList = result };


            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }

        }

        [HttpPost]
        public async Task<ChatReceiveResponse> ChatReceivedByTime(ChatVM _vm)
        {
            ChatReceiveResponse apiResponse = new ChatReceiveResponse();
            ChatRepo _obj = new ChatRepo();
            try
            {
                _vm._flag = 0;
                var result = await _obj.ChatReceivedByTime(_vm);
                if (result._flag > 0)
                    return new ChatReceiveResponse { ChatStatus = 1, DataList = result._dataList };
                else
                    return new ChatReceiveResponse { ChatStatus = 0, DataList = result._dataList };
            }
            catch (Exception ex)
            {
                apiResponse = new ChatReceiveResponse { error = ex.Message.ToString() };  //TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }

        }

        [HttpPost]
        public async Task<ApiResponse> ChatSignOut(ChatVM _vm)
        {
            ApiResponse apiResponse = new ApiResponse();
            ChatRepo _obj = new ChatRepo();
            try
            {
                _vm._flag = 0;
                var result = await _obj.ChatSignOut(_vm);
                if (result > 0)
                    return new ApiResponse { IsValidUser = true, Message = "success", MessageType = result, DataList = null };
                else
                    return new ApiResponse { IsValidUser = true, Message = "Already Chat Off", MessageType = result, DataList = null };


            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }

        }


        /// <summary>  
        /// Upload Document.....  
        /// </summary>        
        /// <returns></returns>  
        [HttpPost]
        //[Route("api/DocumentUpload/MediaUpload")]
        public async Task<ApiResponse> ChatSend()
        {
            // Check if the request contains multipart/form-data.  
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
            //access form data  
            NameValueCollection formData = provider.FormData;

            ChatVM _model = new ChatVM();
            _model._operatorId = Convert.ToInt32(formData["OperatorId"].ToString());
            _model._userId = Convert.ToInt32(formData["UserId"].ToString());
            _model._chatSessionId = Convert.ToInt32(formData["ChatSessionId"].ToString());
            _model._message = formData["Message"].ToString();
            _model._flag = Convert.ToInt32(formData["flag"].ToString());
            _model._messageSentBy = Convert.ToInt32(formData["MessageSentBy"].ToString());
            _model._messageType = Convert.ToInt32(formData["MessageType"].ToString());
            _model._attachmentType = Convert.ToInt32(formData["AttachmentType"].ToString());
            _model._token = formData["Token"].ToString();

            //access files  
            string _thisFileName = string.Empty;
            ApiResponse _response = new ApiResponse();
            IList<HttpContent> files = provider.Files;
            string URL = String.Empty;
            if (files.Count > 0)
            {
                foreach (var item in files)
                {
                    _thisFileName = item.Headers.ContentDisposition.FileName.Trim('\"');
                    _model._filePath = _thisFileName != string.Empty ? "/" + _model._chatSessionId.ToString() + "/" + _thisFileName.ToString() : "";
                    _response = await Chat_Sent(_model);
                    if (!_response.IsValidUser || !(_response.MessageType > 0))
                        return _response;

                    if (_model._messageType == 0)
                        return _response;

                    HttpContent file1 = item;
                    string filename = String.Empty;
                    Stream input = await file1.ReadAsStreamAsync();
                    string directoryName = String.Empty;

                    string tempDocUrl = WebConfigurationManager.AppSettings["DocsUrl"];

                    if (formData["ClientDocs"] == "ClientDocs")
                    {
                        var path = HttpRuntime.AppDomainAppPath;
                        directoryName = System.IO.Path.Combine(path, "ClientDocument");
                        //filename = System.IO.Path.Combine(directoryName, thisFileName);

                        String _path = System.IO.Path.Combine(path, "ClientDocument/" + _model._chatSessionId.ToString());
                        if (!Directory.Exists(_path))
                        {
                            Directory.CreateDirectory(_path);
                        }
                        filename = System.IO.Path.Combine(_path, _thisFileName);

                        //Deletion exists file  
                        if (File.Exists(filename))
                        {
                            File.Delete(filename);
                            //URL = DocsPath + _thisFileName+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString()+ DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                        }
                        string DocsPath = tempDocUrl + "/" + "ClientDocument" + "/";
                        URL = DocsPath + _thisFileName;
                    }
                    //Directory.CreateDirectory(@directoryName);  
                    using (Stream file = File.OpenWrite(filename))
                    {
                        input.CopyTo(file);
                        //close file  
                        file.Close();
                    }
                }
            }
            else
            {
                _model._filePath = "";
                _response = await Chat_Sent(_model);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("DocsUrl", URL);
            return _response;
        }


        public async Task<ApiResponse> Chat_Sent(ChatVM _vm)
        {
            ApiResponse apiResponse = new ApiResponse();
            ChatRepo _obj = new ChatRepo();
            try
            {
                _vm._flag = 0;
                var result = await _obj.ChatSend(_vm);
                if (result > 0)
                    return new ApiResponse { IsValidUser = true, Message = "success", MessageType = result, DataList = result };
                else
                    return new ApiResponse { IsValidUser = true, Message = "someting went wrong..!!", MessageType = result, DataList = result };


            }
            catch (Exception ex)
            {
                apiResponse = TaskUTL.GenerateExceptionResponse(ex, _pageName, true);
                return apiResponse;
            }

        }

        //public static string AutoRenameFilename(FileInfo file)
        //{
        //    var filename = file.Name.Replace(file.Extension, string.Empty);
        //    var dir = file.Directory.FullName;
        //    var ext = file.Extension;

        //    if (file.Exists)
        //    {
        //        int count = 0;
        //        string added;

        //        do
        //        {
        //            count++;
        //            added = "(" + count + ")";
        //        } while (File.Exists(dir + "\\" + filename + " " + added + ext));

        //        filename += " " + added;
        //    }

        //    return (dir + filename + ext);
        //}
    }
}