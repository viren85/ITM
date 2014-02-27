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
    public class AreaMasterModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string ShortAreaName { get; set; }

        /// <summary>
        ///inserting new record in databse  
        /// </summary>
        public void Save()
        {
            try
            {
                string Q_SaveAreaMasterDetail = "insert into areamaster(AreaName, ShortAreaName) values({0}','{1}')";

                Logger.Debug("AreaMasterModel", "Save", "Assigning values to query.");
                string cmdText = string.Format(Q_SaveAreaMasterDetail, ParameterFormater.FormatParameter(this.AreaName), ParameterFormater.FormatParameter(this.ShortAreaName));

                Logger.Debug("AreaMasterModel", "Save", "Formatting the special character");
                Database db = new Database();
                Logger.Debug("AreaMasterModel", "Save", "Database object created.");

                Logger.Debug("AreaMasterModel", "Save", "Before inserting value in database");
                db.Insert(cmdText);
                Logger.Debug("AreaMasterModel", "Save", "After value inserted in database");

            }
            catch (Exception ex)
            {
                Logger.Error("AreaMasterModel", "Save", "Error occured while saving area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("AreaMasterModel", "Save", "Killing sleep connection.");
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
                string Q_UpdateAreaMasterDetail = "Update areamaster set AreaName = '{0}', ShortAreaName = '{1}' where Id = {2}";

                Logger.Debug("AreaMasterModel", "Update", "Assigning values to query.");
                string cmdText = string.Format(Q_UpdateAreaMasterDetail, ParameterFormater.FormatParameter(this.AreaName), ParameterFormater.FormatParameter(this.ShortAreaName), this.AreaId);
                Logger.Debug("AreaMasterModel", "Update", "Formatting the special character.");

               
                Database db = new Database();
                Logger.Debug("AreaMasterModel", "Update", "Database object created.");

                Logger.Debug("AreaMasterModel", "Update", "Before Update value in database.");
                db.Update(cmdText);
                Logger.Debug("AreaMasterModel", "Update", "Updated value in database.");
            }
            catch (Exception ex)
            {

                Logger.Error("AreaMasterModel", "Update", "Error occured while updating area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("AreaMasterModel", "Update", "Killing sleep connection.");
                Database.KillConnections();
            }
        }

        /// <summary>
        /// removing existing record from database
        /// </summary>
        public void Delete(int areaMasterId)
        {
            try
            {
                string Q_DeleteAreaMasterDetail = "Delete from areamaster where Id = {0}";

                Logger.Debug("AreaMasterModel", "Delete", "Assigning values to query.");
                string cmdText = string.Format(Q_DeleteAreaMasterDetail, areaMasterId);


                Logger.Debug("AreaMasterModel", "Delete", "Formatting the special character.");
                Database db = new Database();
                Logger.Debug("AreaMasterModel", "Delete", "Database object created.");

                Logger.Debug("AreaMasterModel", "Delete", "Before Delete value in database.");
                db.Delete(cmdText);
                Logger.Debug("AreaMasterModel", "Delete", "Deleted value in database.");
            }
            catch (Exception ex)
            {
                Logger.Error("AreaMasterModel", "Delete", "Error occured while deleting from area master detail", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("AreaMasterModel", "Delete", "Killing sleep connection.");
                Database.KillConnections();
            }
        }

        /// <summary>
        /// Showing the record from database by id
        /// </summary>
        /// <param name="areaMasterId"></param>
        /// <returns></returns>
        public AreaMasterModel GetAreaMasterById(int areaMasterId)
        {
            try
            {
                string Q_ShowAreaMasterDetail = "select * from areamaster where Id = {0}";

                Logger.Debug("AreaMasterModel", "GetAreaMasterById", "storing values in variable.");

                string cmdText = string.Format(Q_ShowAreaMasterDetail, areaMasterId);
                Logger.Debug("AreaMasterModel", "GetAreaMasterById", "storing values in variable after id check.");

                Logger.Debug("AreaMasterModel", "GetAreaMasterById", " creating Database");
                Database db = new Database();
                Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Database created");

                Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Reading value  from Database by Id");
                DbDataReader reader = db.Select(cmdText);
                Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Reading value done");

                Logger.Debug("AreaMasterModel", "GetAreaMasterById", "checking database has rows!");
                if (reader.HasRows)
                {
                    AreaMasterModel areaMaster = new AreaMasterModel();
                    Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Areamaster object created");

                    Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Reading database values");
                    while (reader.Read())
                    {
                        areaMaster.AreaId = Convert.ToInt32(reader["Id"]);
                        Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Id read from database");

                        areaMaster.AreaName = ParameterFormater.UnescapeXML(reader["AreaName"].ToString());
                        Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Areaname read from database");

                        areaMaster.ShortAreaName = ParameterFormater.UnescapeXML(reader["ShortAreaName"].ToString());
                        Logger.Debug("AreaMasterModel", "GetAreaMasterById", "ShortAreaName read from database.");
                    }

                    Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("AreaMasterModel", "GetAreaMasterById", "closing database connection");
                        reader.Close();
                        Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Connection closed");
                    }

                    Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Object returning value");
                    return areaMaster;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("AreaMasterModel", "Delete", "Erro occured while Selecting data from area master detail by id", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("AreaMasterModel", "GetAreaMasterById", "Closed sleeping connection");
                Database.KillConnections();
            }

        }

        /// <summary>
        /// Showing complete record from database
        /// </summary>
        /// <returns></returns>
        public List<AreaMasterModel> GetAreaMasterAll()
        {
            try
            {
                string Q_ShowAllAreaMasterDetail = "select * from areamaster";

                Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Assigning value to the variable");
                string cmdText = string.Format(Q_ShowAllAreaMasterDetail, this.AreaId);

                Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Creating database");
                Database db = new Database();
                Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Database created");

                Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "reading data from database");
                DbDataReader reader = db.Select(cmdText);

                if (reader.HasRows)
                {
                    
                    List<AreaMasterModel> areaMasterList = new List<AreaMasterModel>();

                    while (reader.Read())
                    {
                        AreaMasterModel areaMas = new AreaMasterModel();

                        Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Id read from database");
                        areaMas.AreaId = Convert.ToInt32(reader["Id"]);

                        Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "AreaName read from database.");
                        areaMas.AreaName = ParameterFormater.UnescapeXML(reader["AreaName"].ToString());

                        Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "ShortAreaName read from database.");
                        areaMas.ShortAreaName = ParameterFormater.UnescapeXML(reader["ShortAreaName"].ToString());

                        Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Adding object to list");
                        areaMasterList.Add(areaMas);
                    }

                    Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Checking reader has closed the connection.");
                    if (!reader.IsClosed)
                    {
                        Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "closing database connection");
                        reader.Close();
                    }

                    Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Object returning value");
                    return areaMasterList;
                }

                return null;

            }
            catch (Exception ex)
            {
                Logger.Error("AreaMasterModel", "GetAreaMasterAll", "Erro occured while Selecting data from area master detail for all", ex);
                throw ex;
            }
            finally
            {
                Logger.Debug("AreaMasterModel", "GetAreaMasterAll", "Closed sleeping connection");
                Database.KillConnections();
            }

        }


    }
}