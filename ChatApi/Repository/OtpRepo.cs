using ChatApi.DataServices;
using ChatApi.ServiceContract;
using ChatApi.vmModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ChatApi.Repository
{
    public class OtpRepo : IOtp
    {
        CommonDB objCommonDB = new CommonDB();
        public Int32 ValidateOTP(OtpValidationVM _vm)
        {
            try
            {
                using (SqlCommand dataCmd = new SqlCommand("ValidateOTP", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;

                    dataCmd.Parameters.AddWithValue("@flag", _vm._flag);
                    dataCmd.Parameters["@flag"].Direction = ParameterDirection.InputOutput;

                    dataCmd.Parameters.Add(new SqlParameter("@otp", _vm._otp));
                    dataCmd.Parameters.Add(new SqlParameter("@UserId", _vm._userId));
                    dataCmd.Parameters.Add(new SqlParameter("@IPAddress", _vm._ipAddress));
                    dataCmd.Parameters.Add(new SqlParameter("@OSVersion", _vm._osVersion));
                    dataCmd.Parameters.Add(new SqlParameter("@OtherDetail", _vm._otherDetail));
                    dataCmd.Parameters.Add(new SqlParameter("@ExeType", _vm._exeType));
                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    dataCmd.ExecuteNonQuery();
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

        public async Task<RespRepoVM> CheckOTP(OtpValidationVM _vm)
        {
            try
            {
                DataCommand dataCmd = new DataCommand();
                dataCmd.Command = "ValidateOTP";
                dataCmd.Type = CommandType.StoredProcedure;
                SqlParameter _flag = new SqlParameter("@flag", _vm._flag);
                _flag.Direction = ParameterDirection.InputOutput;
                dataCmd.Parameters.Add(_flag);

                dataCmd.Parameters.Add(new SqlParameter("@otp", _vm._otp));
                dataCmd.Parameters.Add(new SqlParameter("@UserId", _vm._userId));
                dataCmd.Parameters.Add(new SqlParameter("@IPAddress", _vm._ipAddress));
                dataCmd.Parameters.Add(new SqlParameter("@OSVersion", _vm._osVersion));
                dataCmd.Parameters.Add(new SqlParameter("@OtherDetail", _vm._otherDetail));
                dataCmd.Parameters.Add(new SqlParameter("@ExeType", _vm._exeType));

                return new RespRepoVM { _dataList = objCommonDB.GetDataTable(dataCmd), _flag = Convert.ToInt32(_flag.Value) };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}