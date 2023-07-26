using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UserDataApplication.Models;


namespace UserDataApplication.Repository
{
    public class UserDataClass:InterfaceUserData
    {
        public static IWebHostEnvironment _environment;
        public sdirectdbContext dbContext;
        private readonly IServer server;

        public UserDataClass(sdirectdbContext dbContext, IWebHostEnvironment environment, IServer server)
        {

            this.dbContext = dbContext;
            _environment = environment;
            this.server = server;
        }
        public ResponseModel GetUserData()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var data = (from u in dbContext.UserData080723s
                            join c in dbContext.Country080723s on u.CountryId equals c.CountryId
                            join s in dbContext.State080723s on u.StateId equals s.StateId
                            join city in dbContext.City080723s on u.CityId equals city.CityId
                            where u.IsDeleted == false
                            select new UserDataModel
                            {
                                Id = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Gender = u.Gender,
                                Dob = u.Dob,
                                Phone = u.Phone,
                                Email= u.Email,
                                Address = u.Address,
                                CountryName = c.CountryName,
                                StateName=s.StateName,
                                CityName=city.CityName,
                                ZipCode= u.ZipCode,
                                ImgLoc=u.ImgLoc
                            }).ToList();
                responseModel.Message = "Fetch Succesfull";
                responseModel.ListData = data;
            }

            catch (Exception ex) 
            { 
                responseModel.Message = ex.Message;
            }

            return responseModel;
        }

        public ResponseModel PostUserData(PostUserDataModel postUserData)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                if (postUserData.Id == 0)
                {
                    //converting byte64 to image 
                    string convert = postUserData.ImgLoc.Replace("data:image/jpeg;base64,", string.Empty);

                    byte[] imageBytes = Convert.FromBase64String(convert);

                    //storing image by combining fname and currTime in uploads folder

                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }

                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\");

                    string val = DateTime.Now.ToString();
                    val = val.Replace("-", string.Empty);
                    val = val.Replace(":", string.Empty);
                    val = val.Replace(" ", string.Empty);

                    string imageName = postUserData.FirstName +val+ ".jpg";
                    string imgPath = Path.Combine(pathToSave, imageName);

                    File.WriteAllBytes(imgPath, imageBytes);


                    //saving data

                    var builder = WebApplication.CreateBuilder();
                    string conStr = builder.Configuration.GetConnectionString("con");
                    SqlConnection con = new SqlConnection(conStr);
                    con.Open();
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("UserdataSP080723", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("command", SqlDbType.VarChar).Value = "post";
                    cmd.Parameters.Add("FirstName", SqlDbType.VarChar).Value = postUserData.FirstName;
                    cmd.Parameters.Add("LastName", SqlDbType.VarChar).Value = postUserData.LastName;
                    cmd.Parameters.Add("DOB", SqlDbType.DateTime).Value = postUserData.Dob;
                    cmd.Parameters.Add("Phone", SqlDbType.VarChar).Value = postUserData.Phone;
                    cmd.Parameters.Add("Email", SqlDbType.VarChar).Value = postUserData.Email;
                    cmd.Parameters.Add("Gender", SqlDbType.VarChar).Value = postUserData.Gender;
                    cmd.Parameters.Add("CityId", SqlDbType.Int).Value = postUserData.CityId;
                    cmd.Parameters.Add("StateId", SqlDbType.Int).Value = postUserData.StateId;
                    cmd.Parameters.Add("CountryId", SqlDbType.Int).Value = postUserData.CountryId;
                    cmd.Parameters.Add("ZipCode", SqlDbType.VarChar).Value = postUserData.ZipCode;
                    cmd.Parameters.Add("Address", SqlDbType.VarChar).Value = postUserData.Address;
                    cmd.Parameters.Add("ImgLoc", SqlDbType.VarChar).Value = imageName;
                    cmd.ExecuteNonQuery();
                    responseModel.Message = "Added Succesfully";
                    return responseModel;
                }
                else if (postUserData.Id > 0)
                {
                    var builder = WebApplication.CreateBuilder();
                    string conStr = builder.Configuration.GetConnectionString("con");
                    SqlConnection con = new SqlConnection(conStr);
                    con.Open();
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("UserdataSP080723", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("command", SqlDbType.VarChar).Value = "update";
                    cmd.Parameters.Add("Id", SqlDbType.Int).Value = postUserData.Id;
                    cmd.Parameters.Add("FirstName", SqlDbType.VarChar).Value = postUserData.FirstName;
                    cmd.Parameters.Add("LastName", SqlDbType.VarChar).Value = postUserData.LastName;
                    cmd.Parameters.Add("DOB", SqlDbType.DateTime).Value = postUserData.Dob;
                    cmd.Parameters.Add("Phone", SqlDbType.VarChar).Value = postUserData.Phone;
                    cmd.Parameters.Add("Email", SqlDbType.VarChar).Value = postUserData.Email;
                    cmd.Parameters.Add("Gender", SqlDbType.VarChar).Value = postUserData.Gender;
                    cmd.Parameters.Add("CityId", SqlDbType.Int).Value = postUserData.CityId;
                    cmd.Parameters.Add("StateId", SqlDbType.Int).Value = postUserData.StateId;
                    cmd.Parameters.Add("CountryId", SqlDbType.Int).Value = postUserData.CountryId;
                    cmd.Parameters.Add("ZipCode", SqlDbType.VarChar).Value = postUserData.ZipCode;
                    cmd.Parameters.Add("Address", SqlDbType.VarChar).Value = postUserData.Address;
                    cmd.Parameters.Add("ImgLoc", SqlDbType.VarChar).Value = postUserData.ImgLoc;
                    cmd.ExecuteNonQuery();
                    responseModel.Message = "Updated Succesfully";
                    return responseModel;
                }
                else
                {
                    responseModel.Message = "Error";
                    return responseModel;
                }
            }

            catch(Exception e)
            {
                responseModel.Message=e.Message;
            }
            return responseModel;
        }

        public ResponseModel Delete(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var data = dbContext.UserData080723s.Where(i => i.Id == id).FirstOrDefault();
                if (data != null)
                {
                    data.IsDeleted = true;
                    dbContext.SaveChanges();
                    responseModel.Message = "Deleted Succesfully";
                    return responseModel;
                }
                responseModel.Message = "Id Not Found";
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                return responseModel;
            }
        }
        public ResponseModel GetUserDataById(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var checkUserExist = dbContext.UserData080723s.Where(i => i.Id == id).FirstOrDefault();
                if (checkUserExist == null)
                {
                    responseModel.Message = "User Doesn't Exist";
                    return responseModel;
                }
                else
                {
                    var data = (from u in dbContext.UserData080723s
                                join c in dbContext.Country080723s on u.CountryId equals c.CountryId
                                join s in dbContext.State080723s on u.StateId equals s.StateId
                                join city in dbContext.City080723s on u.CityId equals city.CityId
                                where u.Id == id
                                select new UserDataModel
                                {
                                    Id = u.Id,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    Gender = u.Gender,
                                    Dob = u.Dob,
                                    Phone = u.Phone,
                                    Email = u.Email,
                                    Address = u.Address,
                                    CountryName = c.CountryName,
                                    StateName = s.StateName,
                                    CityName = city.CityName,
                                    ZipCode = u.ZipCode,
                                    CountryId = u.CountryId,
                                    StateId = u.StateId,
                                    CityId = u.CityId,
                                    ImgLoc = u.ImgLoc
                                }).FirstOrDefault();


                    //getting localhost and port number
                    var addresses = server.Features.Get<IServerAddressesFeature>().Addresses;
                    //converting to string
                    var str = string.Join(", ", addresses);
                    //adding string to create full path
                    var fullPath = str + "/Upload/" + data.ImgLoc;
                    //assigning img loc path to data.imgloc
                    data.ImgLoc = fullPath;
                    responseModel.Data = data;
                }
            }
            catch(Exception e)
            {
                responseModel.Message = e.Message;
            }
            return responseModel;
        }

        public ResponseModel GetCountryData()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var data = dbContext.Country080723s.ToList();
                responseModel.CountryData = data;
            }
            catch(Exception e)
            {
                responseModel.Message= e.Message;
            }
            return responseModel;
        }

        public ResponseModel GetStateData(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var data = dbContext.State080723s.Where(i=>i.CountryId==id).ToList();
                responseModel.StateData = data;
            }
            catch (Exception e)
            {
                responseModel.Message = e.Message;
            }
            return responseModel;
        }

        public ResponseModel GetCityData(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var data = dbContext.City080723s.Where(i => i.StateId ==id).ToList();
                responseModel.CityData = data;
            }
            catch (Exception e)
            {
                responseModel.Message = e.Message;
            }
            return responseModel;
        }
    }
}
