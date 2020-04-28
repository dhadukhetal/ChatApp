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
    public class LoginRepo : ILogin
    {
        CommonDB objCommonDB = new CommonDB();
        public async Task<UserDetail> UserLogin(LoginVM objLoginModel)
        {
            try
            {
                UserDetail objUserDetail = new UserDetail();
                using (SqlCommand dataCmd = new SqlCommand("LoginCheck", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;
                    dataCmd.Parameters.AddWithValue("@UserName", objLoginModel.UserName);
                    dataCmd.Parameters.AddWithValue("@Password", objLoginModel.Password);
                    dataCmd.Parameters.AddWithValue("@ExeType", objLoginModel.ExeType);
                    dataCmd.Parameters.AddWithValue("@IPAddress", objLoginModel.IpAddress);
                    dataCmd.Parameters.AddWithValue("@OSVersion", objLoginModel.OsVersion);
                    dataCmd.Parameters.AddWithValue("@OtherDetail", objLoginModel.OtherDetail);
                    dataCmd.Parameters.AddWithValue("@CurrExeVersion", objLoginModel.CurrentExeVersion);

                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    //await dataCmd.ExecuteNonQueryAsync();
                    var reader = await dataCmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        objUserDetail.UserId = Convert.ToInt32(reader["UserID"].ToString());
                        objUserDetail.UserName = reader["UserName"].ToString();
                        objUserDetail.Token = reader["Token"].ToString();
                        objUserDetail.UpdateDuration = reader["UpdateDuration"].ToString();
                        objUserDetail.ApiUrl = reader["ApiUrl"].ToString();
                        objUserDetail.Exes = reader["FileName"].ToString();
                        //objUserDetail._latestVersion = reader["LatestVersion"].ToString();
                        objUserDetail.UpdateUrl = reader["UpdateUrl"].ToString();
                        objUserDetail.FileName = reader["FileName"].ToString();
                        objUserDetail.FilePath = reader["FilePath"].ToString();
                        objUserDetail.LatestVersion = Convert.ToInt32(reader["VersionId"].ToString());
                    }
                    return objUserDetail;
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

        /// <summary>
        /// Version check + Download new version
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CheckUpdateDownload(UserUpdateCheck obj)
        {
            try
            {
                UserUpdateResp objUserupdate = new UserUpdateResp();
                using (SqlCommand dataCmd = new SqlCommand("GetLatestVersionByUser", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;
                    dataCmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    dataCmd.Parameters.AddWithValue("@Token", obj.Token);
                    dataCmd.Parameters.AddWithValue("@CurrExeVersion", obj.CurrentVersion);

                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    var reader = await dataCmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                //        objUserupdate._latestVersion = reader["LatestVersion"].ToString();
                //        objUserupdate._isValid = Convert.ToBoolean(reader["IsValid"]);
                        objUserupdate.ApiUrl = reader["ApiUrl"].ToString();
                        objUserupdate.UpdateUrl = reader["UpdateUrl"].ToString();
                        objUserupdate.FileName = reader["FileName"].ToString();
                        objUserupdate.FilePath = reader["FilePath"].ToString();
                        objUserupdate.LatestVersion = Convert.ToInt32(reader["VersionId"].ToString());
                        objUserupdate._exes = reader["FileName"].ToString();
                  
                    }
                    var multiFileContent = new MultipartContent();
                    if (obj.CurrentVersion != objUserupdate.LatestVersion)
                    {
                        //string[] _arrExe = objUserupdate.FileName.Split(',');

                        List<int> versionList = new List<int>();
                        versionList.Add(objUserupdate.LatestVersion);
                        //List<string> list = new List<string>(_arrExe);
                        //var fileNames = new List<string>(_arrExe);
                        //var objectContent = new ObjectContent<List<string>>(fileNames, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                        //multiFileContent.Add(objectContent);

                        var objectContent1 = new ObjectContent<List<int>>(versionList, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
                        multiFileContent.Add(objectContent1);

                        var fullPath = objUserupdate.FilePath + objUserupdate.FileName;

                        var fileBytes = File.ReadAllBytes(fullPath);

                        //for (int i = 0; i < fileNames.Count ; i++)
                        //{
                          //  string fileName = fileNames[i].ToString();
                           // var filePath = objUserupdate.UpdateUrl + fileName;
                            //client = new WebClient();

//                            byte[] fileBytes = client.DownloadData((new Uri(filePath)));
                            var fileMemoryStream = new MemoryStream(fileBytes);

                            var fileContent = new StreamContent(fileMemoryStream);
                            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                            fileContent.Headers.ContentDisposition.FileName = objUserupdate.FileName;
                            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            fileContent.Headers.ContentLength = fileMemoryStream.Length;

                            multiFileContent.Add(fileContent);
                        //}
                    }
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    //response.Headers.Add("objUserupdate", new JavaScriptSerializer().Serialize(objUserupdate));
                    response.Content = multiFileContent;
                    return response;
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

        /// <summary>
        /// Only for Version Check
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<UserUpdateRespCheck> CheckUpdate(UserUpdateCheck obj)
        {
            try
            {
                UserUpdateRespCheck objUserupdate = new UserUpdateRespCheck();
                using (SqlCommand dataCmd = new SqlCommand("CheckUpdateExists", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;
                    dataCmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    dataCmd.Parameters.AddWithValue("@Token", obj.Token);
                    dataCmd.Parameters.AddWithValue("@CurrExeVersion", obj.CurrentVersion);

                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    var reader = await dataCmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        objUserupdate.IsUpdateAvailable = Convert.ToBoolean(reader["IsUpdateAvailable"]);
                        objUserupdate.IsValidUser = Convert.ToBoolean(reader["IsValidUser"]);
                        objUserupdate.LatestVersion = Convert.ToInt32(reader["VersionId"]);

                        //objUserupdate._apiUrl = reader["ApiUrl"].ToString();

                        //objUserupdate._exes = reader["Exes"].ToString();
                        //objUserupdate._updateUrl = reader["UpdateUrl"].ToString();
                    }
                    return objUserupdate;
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


        public async Task<OperatorDetail> OperatorLogin(LoginVM objLoginModel)
        {
            try
            {
                OperatorDetail objUserDetail = new OperatorDetail();
                using (SqlCommand dataCmd = new SqlCommand("Operator_Login", objCommonDB.con))
                {
                    dataCmd.CommandType = CommandType.StoredProcedure;
                    dataCmd.Parameters.AddWithValue("@UserName", objLoginModel.UserName);
                    dataCmd.Parameters.AddWithValue("@Password", objLoginModel.Password);

                    if (objCommonDB.con.State == ConnectionState.Closed)
                        objCommonDB.con.Open();
                    //await dataCmd.ExecuteNonQueryAsync();
                    var reader = await dataCmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        objUserDetail.OperatorId = Convert.ToInt32(reader["OperatorId"].ToString());
                        objUserDetail.DisplayName = reader["DisplayName"].ToString();
                        objUserDetail.OperatorMaxUserConnectionLimit = reader["OperatorMaxUserConnectionLimit"].ToString();
                        objUserDetail.OnHoldDefaultMessage = reader["OnHoldDefaultMessage"].ToString();
                      
                    }
                    return objUserDetail;
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