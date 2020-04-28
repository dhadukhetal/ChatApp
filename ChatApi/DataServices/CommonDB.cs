
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace ChatApi.DataServices
{
    public class CommonDB
    {

        #region DECLARATION
        public SqlConnection con;
        string constring = string.Empty;
        #endregion

        #region CONSTRUCTOR
        public CommonDB()
        {
            constring = Connection.Constr;
            con = new SqlConnection(constring);
        }
        #endregion

        #region METHOD
        public void ExecuteCommand(DataCommand dataCmd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(dataCmd.Command, con))
                    {
                        cmd.CommandType = dataCmd.Type;
                        foreach (SqlParameter param in dataCmd.Parameters)
                            cmd.Parameters.Add(param);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable GetDataTable(DataCommand dataCmd)
        {
            try
            {
                
                using (SqlConnection con = new SqlConnection(constring))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(dataCmd.Command, con))
                    {
                        cmd.CommandType = dataCmd.Type;
                        cmd.CommandTimeout = 0;
                        foreach (SqlParameter param in dataCmd.Parameters)
                            cmd.Parameters.Add(param);

                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable ds = new DataTable();
                        adp.SelectCommand = cmd;
                        adp.Fill(ds);

                        return ds;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public async Task<DataTable> GetDataTableAsync(System.Data.Common.DbCommand data_command, string tableName = null)
        {
            TaskCompletionSource<DataTable> source = new TaskCompletionSource<DataTable>();
            var resultTable = new DataTable(tableName ?? data_command.CommandText);
            data_command.Connection = new SqlConnection(constring);
           
            DbDataReader dataReader = null;


            try
            {
                await data_command.Connection.OpenAsync();
                dataReader = await data_command.ExecuteReaderAsync(CommandBehavior.Default);
                resultTable.Load(dataReader);
                source.SetResult(resultTable);
            }
            catch (Exception ex)
            {
                source.SetException(ex);
            }
            finally
            {
                if (dataReader != null)
                    dataReader.Close();

                data_command.Connection.Close();
            }

            return resultTable;
        }

        public DataSet GetDataSet(DataCommand dataCmd)
        {
            try
            {
                
                using (SqlConnection con = new SqlConnection(constring))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(dataCmd.Command, con))
                    {
                        cmd.CommandType = dataCmd.Type;
                        cmd.CommandTimeout = 50;
                        foreach (SqlParameter param in dataCmd.Parameters)
                            cmd.Parameters.Add(param);

                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adp.SelectCommand = cmd;
                        adp.SelectCommand.CommandTimeout = 0;
                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                // return ex.Message.ToString();
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public string GetJsonResult_Paging_Sorting(DataSet ds)
        {
            DataTable dt = new DataTable();
            int TotalRecordCount = 0;

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    TotalRecordCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
                }
            }
            return JsonConvert.SerializeObject(new { Result = "OK", Records = dt, TotalRecordCount = TotalRecordCount }, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
        public DataTable GetDataTable(string Query)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                using (DataTable ds = new DataTable())
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter(Query, con))
                    {
                        adp.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        private DataTable ToDataTable<T>(List<T> items) where T : class
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            if (items != null)
            {
                //Get all the properties
                System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name, type);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
            }
            return dataTable;
        }
    }


}
