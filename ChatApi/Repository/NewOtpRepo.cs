using ChatApi.DataServices;
using ChatApi.ServiceContract;
using ChatApi.vmModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ChatApi.Repository
{
    public class NewOtpRepo : INewOtp
    {
        CommonDB objCommonDB = new CommonDB();
        public async Task<DataTable> GetNewOTP(NewOtpVM newotpmodel)
        {
            try
            {
                DataCommand dataCmd = new DataCommand();
                dataCmd.Command = "SetOtpByOperator";
                if (newotpmodel._userId > 0)
                    dataCmd.Parameters.Add(new SqlParameter("@operatorId", newotpmodel._userId));
                dataCmd.Type = CommandType.StoredProcedure;
                return objCommonDB.GetDataTable(dataCmd);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}