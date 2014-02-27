using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ITM.DAOBase;
using ITM.LogManager;
using ITM.Utilities;
using System.Data.Common;
using System.Web.Mvc;

namespace DisSol.Models
{
    public class DiseaseDetailModel
    {
        public int Id { get; set; }

        public int DocId { get; set; }
        
        [Required]
        [DisplayName("Age (in years)")]
        [Range(0, 120, ErrorMessage = "Please provid Age.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please select gender.")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please select Area.")]
        public int AreaId { get; set; }

        //for dynamically loading value from another table
        // for dropdownlist
        public IEnumerable<SelectListItem> Areas { get; set; }

       public DateTime PatientAddDate { get; set; }

        [Required(ErrorMessage = "Please select Disease type.")]
        [DisplayName("Disease Type")]
       public int DiseaseId { get; set; }

        //dynamically loading disease from disease dtail table by disease id
       public IEnumerable<SelectListItem> Disease { get; set; }

        /// <summary>
        /// Saving records into the database
        /// </summary>
        public void Save()
        {
            try
            {
                
                string Q_DiseaseDetailModel = "insert into diseasedetails(DocId, Age, Gender, AreaId, PatientAddDate, DiseaseId) values({0}, {1}, '{2}', {3}, '{4}', {5})";

                Logger.Debug("DiseaseMasterModel", "Save", "Assigning values to query.");
                string cmdText = string.Format(Q_DiseaseDetailModel, this.DocId, this.Age, ParameterFormater.FormatParameter(this.Gender), this.AreaId, this.PatientAddDate.ToString("yyyy/MM/dd HH:mm:ss.fff"), this.DiseaseId);

                Logger.Debug("DiseaseMasterModel", "Save", "Formatting the special character");
                Database db = new Database();
                Logger.Debug("DiseaseMasterModel", "Save", "Database object created.");

                Logger.Debug("DiseaseMasterModel", "Save", "Before inserting value in database");
                db.Insert(cmdText);
                Logger.Debug("DiseaseMasterModel", "Save", "After value inserted in database");

            }
            catch (Exception ex)
            {
                Logger.Error("DiseaseMasterModel", "Save", "Error occured while saving area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseMasterModel", "Save", "Killing sleep connection.");
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
                string Q_UpdateDiseaseDetail = "Update diseasedetails set DocId = {0}, Age = {1}, Gender = '{2}', AreaId = {3}, PatientAddDate = '{4}', DiseaseId = {5} where Id = {6}";

                Logger.Debug("DiseaseDetailModel", "Update", "Assigning values to query.");
                string cmdText = string.Format(Q_UpdateDiseaseDetail, this.DocId, this.Age, ParameterFormater.FormatParameter(this.Gender), this.AreaId, this.PatientAddDate.ToString("yyyy/MM/dd HH:mm:ss.fff"), this.DiseaseId);
                Logger.Debug("DiseaseMasterModel", "Update", "Formatting the special character.");


                Database db = new Database();
                Logger.Debug("DiseaseMasterModel", "Update", "Database object created.");

                Logger.Debug("DiseaseMasterModel", "Update", "Before Update value in database.");
                db.Update(cmdText);
                Logger.Debug("DiseaseMasterModel", "Update", "Updated value in database.");
            }
            catch (Exception ex)
            {

                Logger.Error("DiseaseMasterModel", "Update", "Error occured while updating area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseMasterModel", "Update", "Killing sleep connection.");
                Database.KillConnections();
            }
        }

        /// <summary>
        /// Showing record by ID from database DiseaseDetails
        /// </summary>
        /// <param name="diseaseDetailId"></param>
        /// <returns></returns>

        public DiseaseDetailModel GetDiseaseDetailById(int diseaseDetailId)
        {
            try
            {
                string Q_ShowDiseaseDetailById = "select * from diseasedetails where Id = {0}";

                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "storing values in variable.");

                string cmdText = string.Format(Q_ShowDiseaseDetailById, diseaseDetailId);
                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "storing values in variable after id check.");

                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", " creating Database");
                Database db = new Database();
                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Database created");

                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Reading value  from Database by Id");

                DbDataReader reader = db.Select(cmdText);
                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Reading value done");

                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "checking database has rows!");
                if (reader.HasRows)
                {
                    DiseaseDetailModel diseaseDetail = new DiseaseDetailModel();
                    Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Diseasemaster object created");

                    Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Reading database values");
                    while (reader.Read())
                    {
                        diseaseDetail.Id = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Id read from database");

                        diseaseDetail.DocId = Convert.ToInt32(reader["DocId"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DocId read from database");

                        diseaseDetail.Age = Convert.ToInt32(reader["Age"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Age read from database");

                        diseaseDetail.Gender = ParameterFormater.UnescapeXML(reader["Gender"].ToString());
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Gender read from database");

                        diseaseDetail.AreaId = Convert.ToInt32(reader["AreaId"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "AreaId read from database");


                        diseaseDetail.DiseaseId = Convert.ToInt32(reader["DiseaseId"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DiseaseId read from database");


                        diseaseDetail.PatientAddDate = Convert.ToDateTime(reader["PatientAddDate"].ToString());
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Patient admit date read from database");
                     /*
                        diseaseDetail.PatientAddDate = ParameterFormater.UnescapeXML(reader["PatientAddDate"].ToString());
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "ShortAreaName read from database.");diseaseDetail.DocId = Convert.ToInt32(reader["DocId"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DocId read from database");
                     */
                    }

                    Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "closing database connection");
                        reader.Close();
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Connection closed");
                    }

                    Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Object returning value");
                    return diseaseDetail;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DiseaseDetailModel", "GetDiseaseDetailById", "Erro occured while Selecting data from disease master detail by id", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Closed sleeping connection");
                Database.KillConnections();
            }

        }

        /// <summary>
        /// Showing the complete record from the diseasedetails database
        /// </summary>
        /// <returns></returns>
        public List<DiseaseDetailModel> GetDiseaseDetailAll()
        {
            try
            {
                string Q_ShowAllDiseaseDetailAll = "select * from diseasedetails";

                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailAll", "Assigning value to the variable");
                string cmdText = string.Format(Q_ShowAllDiseaseDetailAll);

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Creating database");
                Database db = new Database();
                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Database created");

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "reading data from database");
                DbDataReader reader = db.Select(cmdText);

                if (reader.HasRows)
                {

                    List<DiseaseDetailModel> diseaseDetailList = new List<DiseaseDetailModel>();

                    while (reader.Read())
                    {
                        DiseaseDetailModel diseaseDetail = new DiseaseDetailModel();

                        diseaseDetail.Id = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Id read from database");

                        diseaseDetail.DocId = Convert.ToInt32(reader["DocId"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DocId read from database");

                        diseaseDetail.Age = Convert.ToInt32(reader["Age"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Age read from database");

                        diseaseDetail.Gender = ParameterFormater.UnescapeXML(reader["Gender"].ToString());
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Gender read from database");

                        diseaseDetail.AreaId = Convert.ToInt32(reader["AreaId"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "AreaId read from database");

                        diseaseDetail.PatientAddDate = Convert.ToDateTime(reader["PatientAddDate"].ToString());
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Patient admit date read from database");

                        diseaseDetail.DiseaseId = Convert.ToInt32(reader["DiseaseId"]);
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "DiseaseId read from database");

                        diseaseDetailList.Add(diseaseDetail);
                    }

                    Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "closing database connection");
                        reader.Close();
                    }

                    Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Object returning value");
                    return diseaseDetailList;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DiseaseDetailModel", "GetDiseaseDetailById", "Error occured while Selecting data from disease detail for all", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseDetailModel", "GetDiseaseDetailById", "Closed sleeping connection");
                Database.KillConnections();
            }

        }




    }
}