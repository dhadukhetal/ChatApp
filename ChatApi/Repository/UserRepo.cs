using ChatApi.DataServices;
using ChatApi.ServiceContract;
using ChatApi.vmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChatApi.Repository
{
    public class UserRepo
    {
        CommonDB objCommonDB = new CommonDB();
      
        public async Task<DataTable> GetUserList(int OperatorID)
        {
            try
            {
                UserUpdateRespCheck objUserupdate = new UserUpdateRespCheck();
                using (SqlCommand dataCmd = new SqlCommand("[User_GetOnlyOnline]", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;
                    dataCmd.Parameters.AddWithValue("@OperatorId", OperatorID);

                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    var reader = await objCommonDB.GetDataTableAsync(dataCmd);
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (objCommonDB.con.State == ConnectionState.Open)
                    objCommonDB.con.Close();
            }
        }
      
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}