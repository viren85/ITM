using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ITM.DAOBase;
using ITM.LogManager;
using ITM.Utilities;

namespace DisSol.Models
{
    public class DoctorDetailsModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage="Please provide Doctor Name.")]
        [DisplayName("Doctor Name")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Please provide Department.")]
        [DisplayName("Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Please provide Mobile Number.")]
        [DisplayName("Mobile Number")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Mobile No")]
        public long MobileNumber { get; set; }

        [Required(ErrorMessage = "Please provide Address.")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select Area.")]
        [DisplayName("Area")]
        public int AreaId { get; set; }

        [Required(ErrorMessage = "Please provide User Name.")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please provide Password.")]
        [DisplayName("Password")]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        public IEnumerable<SelectListItem> Areas { get; set; }
        
        public DateTime CreateDate { get; set; }

        public void Save()
        {
            try
            {
                string Q_DoctorDetailModel = "insert into doctordetails(Id, DoctorName, Department, MobileNumber, Address, AreaId, Username, Password, CreatedDate) values({0}, '{1}', '{2}', {3}, '{4}', {5}, '{6}', '{7}', '{8}')";

                Logger.Debug("DoctorDetailsModel", "Save", "Assigning values to query.");
                string cmdText = string.Format(Q_DoctorDetailModel, this.Id, ParameterFormater.FormatParameter(this.DoctorName), ParameterFormater.FormatParameter(this.Department), this.MobileNumber, ParameterFormater.FormatParameter(this.Address), this.AreaId, ParameterFormater.FormatParameter(this.UserName), this.Password, this.CreateDate.ToString("yyyy/MM/dd HH:mm:ss.fff"));

                Logger.Debug("DoctorDetailsModel", "Save", "Formatting the special character");
                Database db = new Database();
                Logger.Debug("DoctorDetailsModel", "Save", "Database object created.");

                Logger.Debug("DoctorDetailsModel", "Save", "Before inserting value in database");
                db.Insert(cmdText);
                Logger.Debug("DoctorDetailsModel", "Save", "After value inserted in database");

            }
            catch (Exception ex)
            {
                Logger.Error("DoctorDetailsModel", "Save", "Error occured while saving area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DoctorDetailsModel", "Save", "Killing sleep connection.");
                Database.KillConnections();
            }
        }

        /// <summary>
        /// Updating existing record into Disease details 
        /// </summary>
        public void Update()
        {
            try
            {
                string Q_UpdateDoctorDetail = "Update doctordetails set DoctorName = '{0}', Department = '{1}', MobileNumber = '{2}', Adderess = '{3}', AreaId = {4}, Username = '{5}', Password = '{6}', CreatedDate = '{7}' where Id = {8}";

                Logger.Debug("DoctorDetailsModel", "Update", "Assigning values to query.");
                string cmdText = string.Format(Q_UpdateDoctorDetail, ParameterFormater.FormatParameter(this.DoctorName), ParameterFormater.FormatParameter(this.Department), this.MobileNumber, ParameterFormater.FormatParameter(this.Address), this.AreaId, ParameterFormater.FormatParameter(this.UserName), ParameterFormater.FormatParameter(this.Password), this.CreateDate.ToString("yyyy/MM/dd HH:mm:ss.fff"), this.Id);
                Logger.Debug("DoctorDetailsModel", "Update", "Formatting the special character.");


                Database db = new Database();
                Logger.Debug("DoctorDetailsModel", "Update", "Database object created.");

                Logger.Debug("DoctorDetailsModel", "Update", "Before Update value in database.");
                db.Update(cmdText);
                Logger.Debug("DoctorDetailsModel", "Update", "Updated value in database.");
            }
            catch (Exception ex)
            {

                Logger.Error("DoctorDetailsModel", "Update", "Error occured while updating area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DoctorDetailsModel", "Update", "Killing sleep connection.");
                Database.KillConnections();
            }
        }


        /// <summary>
        /// Showing record by ID from database DiseaseDetails
        /// </summary>
        /// <param name="doctorDetailId"></param>
        /// <returns></returns>

        public DoctorDetailsModel GetDoctorDetailById(int doctorDetailId)
        {
            try
            {
                string Q_ShowDoctorDetailById = "select * from doctordetails where Id = {0}";

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "storing values in variable.");

                string cmdText = string.Format(Q_ShowDoctorDetailById, doctorDetailId);
                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "storing values in variable after id check.");

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", " creating Database");
                Database db = new Database();
                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Database created");

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Reading value  from Database by Id");

                DbDataReader reader = db.Select(cmdText);
                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Reading value done");

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "checking database has rows!");
                if (reader.HasRows)
                {
                    DoctorDetailsModel doctorDetails = new DoctorDetailsModel();
                    Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "DoctorDetail object created");

                    Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Reading database values");
                    while (reader.Read())
                    {
                        doctorDetails.Id = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Id read from database");

                        doctorDetails.DoctorName = ParameterFormater.UnescapeXML(reader["DoctorName"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Doctor Name read from database");

                        doctorDetails.Department = ParameterFormater.UnescapeXML(reader["Department"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Department read from database");



                        doctorDetails.MobileNumber = Convert.ToInt64(reader["MobileNumber"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Mobile Number read from database");

                        doctorDetails.Address = ParameterFormater.UnescapeXML(reader["Address"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Address read from database");

                        doctorDetails.AreaId = Convert.ToInt32(reader["AreaId"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "AreaId read from database");

                        doctorDetails.UserName = ParameterFormater.UnescapeXML(reader["Username"].ToString());
                        doctorDetails.Password = ParameterFormater.UnescapeXML(reader["Password"].ToString());

                        doctorDetails.CreateDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Create Date read from database");
                        /*
                           diseaseDetail.PatientAddDate = ParameterFormater.UnescapeXML(reader["PatientAddDate"].ToString());
                           Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "ShortAreaName read from database.");diseaseDetail.DocId = Convert.ToInt32(reader["DocId"]);
                           Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DocId read from database");
                        */
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "closing database connection");
                        reader.Close();
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Connection closed");
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Object returning value");
                    return doctorDetails;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DoctorDetailsModel", "GetDiseaseDetailById", "Erro occured while Selecting data from disease master detail by id", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Closed sleeping connection");
                Database.KillConnections();
            }

        }


        /// <summary>
        /// Showing the complete record from the diseasedetails database
        /// </summary>
        /// <returns></returns>
        public List<DoctorDetailsModel> GetDoctorDetailAll()
        {
            try
            {
                string Q_ShowAllDoctorDetail = "select * from doctordetails";

                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Assigning value to the variable");
                string cmdText = string.Format(Q_ShowAllDoctorDetail);

                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Creating database");
                Database db = new Database();
                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Database created");

                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "reading data from database");
                DbDataReader reader = db.Select(cmdText);

                if (reader.HasRows)
                {

                    List<DoctorDetailsModel> diseaseDetailList = new List<DoctorDetailsModel>();

                    while (reader.Read())
                    {
                        DoctorDetailsModel doctorDetail = new DoctorDetailsModel();

                        doctorDetail.Id = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Id read from database");

                        doctorDetail.DoctorName = ParameterFormater.UnescapeXML(reader["DoctorName"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Gender read from database");

                        doctorDetail.Department = ParameterFormater.UnescapeXML(reader["Department"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "DoctorName read from database");

                        doctorDetail.MobileNumber = Convert.ToInt64(reader["MobileNumber"]);
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "MobileNumber read from database");

                        doctorDetail.Address = ParameterFormater.UnescapeXML(reader["Address"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Address read from database");

                        doctorDetail.AreaId = Convert.ToInt32(reader["AreaId"]);
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "AreaId read from database");

                        doctorDetail.UserName = ParameterFormater.UnescapeXML(reader["Username"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Username read from database");

                        doctorDetail.Password = ParameterFormater.UnescapeXML(reader["Password"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Password read from database");

                        doctorDetail.CreateDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Patient admit date read from database");

                        diseaseDetailList.Add(doctorDetail);
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "closing database connection");
                        reader.Close();
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Object returning value");
                    return diseaseDetailList;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DoctorDetailsModel", "GetDiseaseDetailAll", "Error occured while Selecting data from disease detail for all", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailAll", "Closed sleeping connection");
                Database.KillConnections();
            }

        }


        public DoctorDetailsModel UserAuthentication(string userName, string password)
        {
            

            try
            {
                string Q_CheckUserName = "select * from doctordetails where Username = '{0}' and Password = '{1}'";

                Logger.Debug("DoctorDetailsModel", "UserAuthentication", "storing values in variable.");

                string cmdText = string.Format(Q_CheckUserName, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));
              
                Database db = new Database();
                

              

                DbDataReader reader = db.Select(cmdText);
              

                
                if (reader.HasRows)
                {
                    DoctorDetailsModel doctorDetails = new DoctorDetailsModel();
                   
                    while (reader.Read())
                    {
                        doctorDetails.Id = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Id read from database");

                        doctorDetails.DoctorName = ParameterFormater.UnescapeXML(reader["DoctorName"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Doctor Name read from database");

                        doctorDetails.Department = ParameterFormater.UnescapeXML(reader["Department"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Department read from database");



                        doctorDetails.MobileNumber = Convert.ToInt64(reader["MobileNumber"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Mobile Number read from database");

                        doctorDetails.Address = ParameterFormater.UnescapeXML(reader["Address"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Address read from database");

                        doctorDetails.AreaId = Convert.ToInt32(reader["AreaId"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "AreaId read from database");

                        doctorDetails.UserName = ParameterFormater.UnescapeXML(reader["Username"].ToString());
                        doctorDetails.Password = ParameterFormater.UnescapeXML(reader["Password"].ToString());

                        doctorDetails.CreateDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Create Date read from database");
                        /*
                           diseaseDetail.PatientAddDate = ParameterFormater.UnescapeXML(reader["PatientAddDate"].ToString());
                           Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "ShortAreaName read from database.");diseaseDetail.DocId = Convert.ToInt32(reader["DocId"]);
                           Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DocId read from database");
                        */
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "closing database connection");
                        reader.Close();
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Connection closed");
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Object returning value");
                    return doctorDetails;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DoctorDetailsModel", "GetDiseaseDetailById", "Erro occured while Selecting data from disease master detail by id", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Closed sleeping connection");
                Database.KillConnections();
            }

        }


        public DoctorDetailsModel GetDoctorDetailByUserName(string userName)
        {
            try
            {
                string Q_ShowDoctorDetailByUserName = "select * from doctordetails where UserName = '{0}'";

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "storing values in variable.");

                string cmdText = string.Format(Q_ShowDoctorDetailByUserName, userName);
                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "storing values in variable after id check.");

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", " creating Database");
                Database db = new Database();
                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Database created");

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Reading value  from Database by Id");

                DbDataReader reader = db.Select(cmdText);
                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Reading value done");

                Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "checking database has rows!");
                if (reader.HasRows)
                {
                    DoctorDetailsModel doctorDetails = new DoctorDetailsModel();
                    Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "DoctorDetail object created");

                    Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Reading database values");
                    while (reader.Read())
                    {
                        doctorDetails.Id = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Id read from database");

                        doctorDetails.DoctorName = ParameterFormater.UnescapeXML(reader["DoctorName"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Doctor Name read from database");

                        doctorDetails.Department = ParameterFormater.UnescapeXML(reader["Department"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Department read from database");



                        doctorDetails.MobileNumber = Convert.ToInt64(reader["MobileNumber"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Mobile Number read from database");

                        doctorDetails.Address = ParameterFormater.UnescapeXML(reader["Address"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Address read from database");

                        doctorDetails.AreaId = Convert.ToInt32(reader["AreaId"]);
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "AreaId read from database");

                        doctorDetails.UserName = ParameterFormater.UnescapeXML(reader["Username"].ToString());
                        doctorDetails.Password = ParameterFormater.UnescapeXML(reader["Password"].ToString());

                        doctorDetails.CreateDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                        Logger.Debug("DoctorDetailsModel", "GetDoctorDetailById", "Create Date read from database");
                        /*
                           diseaseDetail.PatientAddDate = ParameterFormater.UnescapeXML(reader["PatientAddDate"].ToString());
                           Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "ShortAreaName read from database.");diseaseDetail.DocId = Convert.ToInt32(reader["DocId"]);
                           Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DocId read from database");
                        */
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "closing database connection");
                        reader.Close();
                        Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Connection closed");
                    }

                    Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Object returning value");
                    return doctorDetails;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DoctorDetailsModel", "GetDiseaseDetailById", "Erro occured while Selecting data from disease master detail by id", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DoctorDetailsModel", "GetDiseaseDetailById", "Closed sleeping connection");
                Database.KillConnections();
            }

        }


    }
}