using ChatApi.DataServices;
using ChatApi.ServiceContract;
using ChatApi.UTL;
using ChatApi.vmModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ChatApi.Repository
{
    public class UserRegisterRepo : IUserRegistration
    {
        CommonDB objCommonDB = new CommonDB();
        public async Task<IHttpActionResult> CreateUpdate(UserRegistrationVM objEmployee)
        {
            try
            {
                using (SqlCommand dataCmd = new SqlCommand("UserRegistration", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;
                    //dataCmd.Parameters.AddWithValue("@UserID", objEmployee._userId);
                    //dataCmd.Parameters["@UserID"].Direction = ParameterDirection.InputOutput;
                    dataCmd.Parameters.Add(new SqlParameter("@UserName", objEmployee._userName));
                    dataCmd.Parameters.Add(new SqlParameter("@FirstName", objEmployee._firstName));
                    dataCmd.Parameters.Add(new SqlParameter("@LastName", objEmployee._lastName));
                    dataCmd.Parameters.Add(new SqlParameter("@Password", objEmployee._password));
                    dataCmd.Parameters.Add(new SqlParameter("@Phone", objEmployee._phone));
                    dataCmd.Parameters.Add(new SqlParameter("@EmailId", objEmployee._emailId));
                    dataCmd.Parameters.Add(new SqlParameter("@City", objEmployee._city));
                    dataCmd.Parameters.Add(new SqlParameter("@State", objEmployee._state));
                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    //await dataCmd.ExecuteNonQueryAsync();
                    var reader = await dataCmd.ExecuteReaderAsync();
                    //return Convert.ToInt32(dataCmd.Parameters["@UserID"].Value);
                    UserRegistrationResp objUser = new UserRegistrationResp();
                    while (reader.Read())
                    {
                        objUser.ResultType = Convert.ToInt16(reader["ResultType"].ToString());
                        objUser.UpdateUrl = reader["UpdateUrl"].ToString();
                        objUser.Result = reader["Result"].ToString();
                    }
                    var multiFileContent = new MultipartContent();
                    if (objUser.ResultType == 1)
                    {
                        //string[] _arrExe = objUserupdate.FileName.Split(',');

                        //List<string> list = new List<string>(_arrExe);
                        //var fileNames = new List<string>(_arrExe);
                        //var objectContent = new ObjectContent<List<string>>(fileNames, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                        //multiFileContent.Add(objectContent);
                        string fileName = "Setup.msi";
                        //var fullPath = objUser.UpdateUrl + "" + fileName ;
                        var fullPath = @"E:\demo_repo\Bhavesh\LockDown-15-04-2020\" + fileName;

                        var fileBytes = File.ReadAllBytes(fullPath);

                        //for (int i = 0; i < fileNames.Count ; i++)
                        //{
                        //  string fileName = fileNames[i].ToString();
                        // var filePath = objUserupdate.UpdateUrl + fileName;
                        //client = new WebClient();

                        //byte[] fileBytes = client.DownloadData((new Uri(filePath)));
                        var fileMemoryStream = new MemoryStream(fileBytes);

                        var fileContent = new StreamContent(fileMemoryStream);
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        fileContent.Headers.ContentDisposition.FileName = fileName;
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        fileContent.Headers.ContentLength = fileMemoryStream.Length;

                        multiFileContent.Add(fileContent);

                        return new ApiFileResult(fileMemoryStream,
                                     "application/octet-stream",
                                     $"Billing Export {DateTime.Today:yyyy-MM-dd}{fileName}");
                        //}
                    }
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Headers.Add("objUser", new JavaScriptSerializer().Serialize(objUser));
                    response.Content = multiFileContent;

                   
                    return null;
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

    }
}