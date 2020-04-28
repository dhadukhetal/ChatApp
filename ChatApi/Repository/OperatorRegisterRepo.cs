using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ChatApi.DataServices;
using ChatApi.ServiceContract;
using ChatApi.vmModels;

namespace ChatApi.Repository
{
    public class OperatorRegisterRepo : IOperatorRegistration
    {
        CommonDB objCommonDB = new CommonDB();
        public async Task<Int32> CreateUpdate(OperatorRegistrationVM _vmOperator)
        {
            try
            {
                using (SqlCommand dataCmd = new SqlCommand("OperatorRegistration", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;
                    dataCmd.Parameters.AddWithValue("@OperatorID", _vmOperator._operatorID);
                    dataCmd.Parameters["@OperatorID"].Direction = ParameterDirection.InputOutput;
                    dataCmd.Parameters.Add(new SqlParameter("@OperatorName", _vmOperator._operatorName));
                    dataCmd.Parameters.Add(new SqlParameter("@EmailAddress", _vmOperator._emailAddress));
                    dataCmd.Parameters.Add(new SqlParameter("@Password", _vmOperator._password));
                    dataCmd.Parameters.Add(new SqlParameter("@ContactNo", _vmOperator._contactNo));
                    dataCmd.Parameters.Add(new SqlParameter("@UserName", _vmOperator._userName));
                    dataCmd.Parameters.Add(new SqlParameter("@IsActive", _vmOperator._isActive));
                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    await dataCmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(dataCmd.Parameters["@OperatorID"].Value);
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