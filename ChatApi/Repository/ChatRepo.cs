using ChatApi.DataServices;
using ChatApi.ServiceContract;
using ChatApi.vmModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ChatApi.Repository
{
    public class ChatRepo : IChat
    {
        CommonDB objCommonDB = new CommonDB();
        public async Task<int> ChatSend(ChatVM _chat)
        {
            try
            {
                using (SqlCommand dataCmd = new SqlCommand("ChatSend", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;

                    dataCmd.Parameters.AddWithValue("@flag", _chat._flag);
                    dataCmd.Parameters["@flag"].Direction = ParameterDirection.InputOutput;

                    dataCmd.Parameters.Add(new SqlParameter("@OperatorId", _chat._operatorId));
                    dataCmd.Parameters.Add(new SqlParameter("@UserId", _chat._userId));
                    dataCmd.Parameters.Add(new SqlParameter("@ChatSessionId", _chat._chatSessionId));
                    dataCmd.Parameters.Add(new SqlParameter("@Msg", _chat._message));
                    dataCmd.Parameters.Add(new SqlParameter("@MessageSentBy", _chat._messageSentBy));
                    dataCmd.Parameters.Add(new SqlParameter("@MessageType", _chat._messageType));
                    dataCmd.Parameters.Add(new SqlParameter("@AttachmentType", _chat._attachmentType));
                    dataCmd.Parameters.Add(new SqlParameter("@filePath", _chat._filePath));
                    dataCmd.Parameters.Add(new SqlParameter("@Token", _chat._token));
                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    await dataCmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(dataCmd.Parameters["@flag"].Value);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                if (objCommonDB.con.State == ConnectionState.Open)
                    objCommonDB.con.Close();
            }
        }

        public async Task<RespRepoVM> ChatReceivedByTime(ChatVM _vm)
        {
            try
            {
                DataCommand dataCmd = new DataCommand();
                dataCmd.Command = "ChatReceivedByTime";
                dataCmd.Type = CommandType.StoredProcedure;
                SqlParameter _Flag = new SqlParameter("@flag", _vm._flag);
                _Flag.Direction = ParameterDirection.InputOutput;
                dataCmd.Parameters.Add(_Flag);

                dataCmd.Parameters.Add(new SqlParameter("@OperatorId", _vm._operatorId));
                dataCmd.Parameters.Add(new SqlParameter("@UserId", _vm._userId));
                dataCmd.Parameters.Add(new SqlParameter("@ChatSessionId", _vm._chatSessionId));
                dataCmd.Parameters.Add(new SqlParameter("@lastChatReadTime", _vm._lastChatReadTime));
                dataCmd.Parameters.Add(new SqlParameter("@Token", _vm._token));

                return new RespRepoVM { _dataList = objCommonDB.GetDataTable(dataCmd), _flag = Convert.ToInt32(_Flag.Value) };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> ChatSignOut(ChatVM _chat)
        {
            try
            {
                using (SqlCommand dataCmd = new SqlCommand("Chat_SignOUT", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;

                    dataCmd.Parameters.AddWithValue("@flag", _chat._flag);
                    dataCmd.Parameters["@flag"].Direction = ParameterDirection.InputOutput;

                    dataCmd.Parameters.Add(new SqlParameter("@OperatorId", _chat._operatorId));
                    dataCmd.Parameters.Add(new SqlParameter("@ChatSessionId", _chat._chatSessionId));
                    dataCmd.Parameters.Add(new SqlParameter("@Token", _chat._token));

                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    await dataCmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(dataCmd.Parameters["@flag"].Value);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                if (objCommonDB.con.State == ConnectionState.Open)
                    objCommonDB.con.Close();
            }
        }
    }
}