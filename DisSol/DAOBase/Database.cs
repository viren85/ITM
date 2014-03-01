
using System;
using System.Data;
using ITM.LogManager;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections;

namespace ITM.DAOBase
{
    public class Database
    {
        private static string ConnectionString { get; set; }
        private static string CommandText { get; set; }
        private static string database;
        
        static Database()
        {
                                    
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }


        private DbConnection GetConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(database))
                {
                    database = "mysql";
                }
                //return new MySqlConnection();

                switch (database)
                {
                    case "mysql":
                        return new MySqlConnection();
                    case "sqlserver":
                        return new SqlConnection();
                    default:
                        return new MySqlConnection();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("11061");
            }

        }

        public DbDataReader Select(string cmdText)
        {
            DbConnection con = GetConnection();
            DbDataReader result = null;

            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            }
            if (!string.IsNullOrEmpty(cmdText))
            {
                try
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    DbCommand cmd = con.CreateCommand();
                    cmd.CommandText = cmdText;
                    result = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Error("Database", "Select", ex);
                    throw ex;
                }
            }
            return result;
        }

        public void Insert(string cmdText)
        {
            //insert record into a specific table according to user's query.
            DbConnection con = GetConnection();
            DbDataReader result = null;

            if (!string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(cmdText))
            {
                try
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    DbCommand cmd = con.CreateCommand();
                    cmd.CommandText = cmdText;
                    int r = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.Error("Database", "Insert", ex);
                    throw new Exception("11063");
                }
                finally
                {
                    if (result != null && !result.IsClosed)
                    {
                        result.Close();
                    }
                    con.Close();
                }
            }
        }

        public void Delete(string cmdText)
        {
            //Delete record from the database according to user's query.
            DbConnection con = GetConnection();

            if (!string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(cmdText))
            {
                try
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    DbCommand cmd = con.CreateCommand();
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {                    
                    Logger.Error("Database", "Delete", ex);
                    throw new Exception("11064");
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static void KillSleepingConnections()
        {
            //Manages processlist.
            string strSQL = "show processlist";
            ArrayList m_ProcessesToKill = new ArrayList();

            MySqlConnection myConn = new MySqlConnection();
            MySqlDataReader result = null;
            MySqlCommand cmd = null;
            myConn.ConnectionString = ConnectionString;

            try
            {
                myConn.Open();
                cmd = myConn.CreateCommand();
                cmd.CommandText = strSQL;
                result = cmd.ExecuteReader();

                while (result.Read())
                {
                    // Find all processes sleeping with a timeout value higher than our threshold.
                    int iPID = Convert.ToInt32(result["Id"].ToString());
                    string strState = result["Command"].ToString();
                    string db = result["DB"].ToString();

                    if (strState == "Sleep" && string.Equals("sndt", db) && iPID > 0)
                    {
                        // This connection is sitting around doing nothing. Kill it.
                        m_ProcessesToKill.Add(iPID);
                    }
                }

                result.Close();

                foreach (int aPID in m_ProcessesToKill)
                {
                    strSQL = "kill " + aPID;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Database", "KillSleepingConnections", " Error occurred while killing sleeping connections", ex);
                throw new Exception("11500");
              
            }
            finally
            {
                if (myConn != null && myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            return;
        }

        public static void KillConnections()
        {
            //Manages processlist.
            string strSQL = "show processlist";
            ArrayList m_ProcessesToKill = new ArrayList();

            MySqlConnection myConn = new MySqlConnection();
            MySqlDataReader result = null;
            MySqlCommand cmd = null;
            myConn.ConnectionString = ConnectionString;

            try
            {
                myConn.Open();                
                cmd = myConn.CreateCommand();
                cmd.CommandTimeout = 5000000;
                cmd.CommandText = strSQL;
                result = cmd.ExecuteReader();

                while (result.Read())
                {
                    // Find all processes sleeping with a timeout value higher than our threshold.
                    int iPID = Convert.ToInt32(result["Id"].ToString());
                    string strState = result["Command"].ToString();
                    string db = result["DB"].ToString();

                    //if (strState == "Sleep" && string.Equals("exam", db) && iPID > 0)
                    if (strState == "Sleep" && iPID > 0)
                    {
                        // This connection is sitting around doing nothing. Kill it.
                        m_ProcessesToKill.Add(iPID);
                    }
                }

                result.Close();

                foreach (int aPID in m_ProcessesToKill)
                {
                    strSQL = "kill " + aPID;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Database", "KillSleepingConnections", " Error occurred while killing sleeping connections", ex);
                //throw new Exception("11500");

            }
            finally
            {
                if (myConn != null && myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            return;
        }

        public void Update(string cmdText)
        {
            //update record into database table according to user's query.
            DbConnection con = GetConnection();
            DbDataReader result = null;

            if (!string.IsNullOrEmpty(ConnectionString) && !string.IsNullOrEmpty(cmdText))
            {
                try
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    DbCommand cmd = con.CreateCommand();
                    cmd.CommandText = cmdText;
                    result = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Logger.Error("Database", "Update", ex);
                    throw new Exception("11065");
                }
                finally
                {
                    if (result != null && !result.IsClosed)
                    {
                        result.Close();
                    }
                    con.Close();
                    // KillSleepingConnections();
                }
            }
        }
    }
}