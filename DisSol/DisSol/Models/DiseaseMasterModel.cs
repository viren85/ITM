using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITM.DAOBase;
using ITM.LogManager;
using ITM.Utilities;
using System.Data.Common;

namespace DisSol.Models
{
    public class DiseaseMasterModel
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; }


        public string ShortDiseaseName { get; set; }

        /// <summary>
        /// Saving records into the database
        /// </summary>
        public void Save()
        {
            try
            {
                string Q_SaveDiseaseMasterDetail = "insert into diseasemaster(DiseaseName, ShortDiseaseName) values('{0}','{1}')";

                Logger.Debug("DiseaseMasterModel", "Save", "Assigning values to query.");
                string cmdText = string.Format(Q_SaveDiseaseMasterDetail, ParameterFormater.FormatParameter(this.DiseaseName), ParameterFormater.FormatParameter(this.ShortDiseaseName));

                Logger.Debug("DiseaseMasterModel", "Save", "Formatting the special character");
                Database db = new Database();
                Logger.Debug("DiseaseMasterModel", "Save", "Database object created.");

                Logger.Debug("DiseaseMasterModel", "Save", "Before inserting value in database");
                db.Insert(cmdText);
                Logger.Debug("DiseaseMasterModel", "Save", "After value inserted in database");

            }
            catch (Exception ex)
            {
                Logger.Error("DiseaseMasterModel", "Save", "Error occured while saving disease master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseMasterModel", "Save", "Killing sleep connection.");
                Database.KillConnections();
            }
        }

        /// <summary>
        /// Updating existing record in database
        /// </summary>
        public void Update()
        {
            try
            {
                string Q_UpdateDiseaseMasterDetail = "Update diseasemaster set DiseaseName = '{0}', ShortDiseaseName = '{1}' where Id = {2}";

                Logger.Debug("DiseaseMasterModel", "Update", "Assigning values to query.");
                string cmdText = string.Format(Q_UpdateDiseaseMasterDetail, ParameterFormater.FormatParameter(this.DiseaseName), ParameterFormater.FormatParameter(this.ShortDiseaseName), this.Id);
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
        /// removing existing record from database
        /// </summary>
        public void Delete(int diseaseMasterId)
        {
            try
            {
                string Q_DeleteDiseaseMasterDetail = "Delete from diseasemaster where Id = {0}";

                Logger.Debug("DiseaseMasterModel", "Delete", "Assigning values to query.");
                string cmdText = string.Format(Q_DeleteDiseaseMasterDetail, diseaseMasterId);


                Logger.Debug("DiseaseMasterModel", "Delete", "Formatting the special character.");
                Database db = new Database();
                Logger.Debug("DiseaseMasterModel", "Delete", "Database object created.");

                Logger.Debug("DiseaseMasterModel", "Delete", "Before Delete value in database.");
                db.Delete(cmdText);
                Logger.Debug("DiseaseMasterModel", "Delete", "Deleted value in database.");
            }
            catch (Exception ex)
            {
                Logger.Error("DiseaseMasterModel", "Delete", "Error occured while deleting from area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseMasterModel", "Delete", "Killing sleep connection.");
                Database.KillConnections();
            }
        }

        /// <summary>
        /// Showing the record from database by id
        /// </summary>
        /// <param name="diseaseMasterId"></param>
        /// <returns></returns>
        public DiseaseMasterModel GetDiseaseMasterById(int diseaseMasterId)
        {
            try
            {
                string Q_ShowDiseaseMasterDetail = "select * from diseasemaster where Id = {0}";

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "storing values in variable.");

                string cmdText = string.Format(Q_ShowDiseaseMasterDetail, diseaseMasterId);
                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "storing values in variable after id check.");

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", " creating Database");
                Database db = new Database();
                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Database created");

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Reading value  from Database by Id");

                DbDataReader reader = db.Select(cmdText);
                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Reading value done");

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "checking database has rows!");
                if (reader.HasRows)
                {
                    DiseaseMasterModel diseaseMaster = new DiseaseMasterModel();
                    Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Diseasemaster object created");

                    Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Reading database values");
                    while (reader.Read())
                    {
                        diseaseMaster.Id = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Id read from database");

                        diseaseMaster.DiseaseName = ParameterFormater.UnescapeXML(reader["DiseaseName"].ToString());
                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Areaname read from database");

                        diseaseMaster.ShortDiseaseName = ParameterFormater.UnescapeXML(reader["ShortDiseaseName"].ToString());
                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "ShortAreaName read from database.");
                    }

                    Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "closing database connection");
                        reader.Close();
                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Connection closed");
                    }

                    Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Object returning value");
                    return diseaseMaster;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DiseaseMasterModel", "GetDiseaseMasterById", "Erro occured while Selecting data from disease master detail by id", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterById", "Closed sleeping connection");
                Database.KillConnections();
            }

        }

        /// <summary>
        /// Showing complete record from database
        /// </summary>
        /// <returns></returns>
        public List<DiseaseMasterModel> GetDiseaseMasterAll()
        {
            try
            {
                string Q_ShowAllDiseaseMasterDetail = "select * from diseasemaster";

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Assigning value to the variable");
                string cmdText = string.Format(Q_ShowAllDiseaseMasterDetail);

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Creating database");
                Database db = new Database();
                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Database created");

                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "reading data from database");
                DbDataReader reader = db.Select(cmdText);

                if (reader.HasRows)
                {

                    List<DiseaseMasterModel> diseaseMasterList = new List<DiseaseMasterModel>();

                    while (reader.Read())
                    {
                        DiseaseMasterModel diseaseMas = new DiseaseMasterModel();

                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Id read from database");
                        diseaseMas.Id = Convert.ToInt32(reader["Id"]);

                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "DiseaseName read from database.");
                        diseaseMas.DiseaseName = ParameterFormater.UnescapeXML(reader["DiseaseName"].ToString());

                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "ShortAreaName read from database.");
                        diseaseMas.ShortDiseaseName = ParameterFormater.UnescapeXML(reader["DiseaseShortName"].ToString());

                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Adding object to list");
                        diseaseMasterList.Add(diseaseMas);
                    }

                    Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "closing database connection");
                        reader.Close();
                    }

                    Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Object returning value");
                    return diseaseMasterList;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("DiseaseMasterModel", "GetDiseaseMasterAll", "Erro occured while Selecting data from disease master detail for all", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("DiseaseMasterModel", "GetDiseaseMasterAll", "Closed sleeping connection");
                Database.KillConnections();
            }

        }

    }
}