using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LadyO.API.Generic
{
    public class DBConnection
    {
        public string SERVIDOR { get; set; }
        public string DB { get; set; }
        public string USUARIO { get; set; }
        public string PASSWORD { get; set; }

        public static string SCHEMA = getSCHEMA();
        public string PORT { get; set; }

        public DBConnection()
        {

        }

        public static MySqlConnection MySqlConnectionObj()
        {
            try
            {
                MySqlConnection obj = new MySqlConnection();
                DBConnection objConecttion = new DBConnection();
                string URL = string.Empty;
                using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @".\\bin\param.config"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        switch (values[0])
                        {
                            case "SERVIDOR":
                                objConecttion.SERVIDOR = values[1];
                                break;
                            case "DB":
                                objConecttion.DB = values[1];
                                break;
                            case "USUARIO":
                                objConecttion.USUARIO = values[1];
                                break;
                            case "PASSWORD":
                                objConecttion.PASSWORD = values[1];
                                break;
                            case "PORT":
                                objConecttion.PORT = values[1];
                                break;
                        }
                    }
                }
                obj.ConnectionString = "Database=" + objConecttion.DB + "; Port="+ objConecttion.PORT + "; Data Source=" + objConecttion.SERVIDOR + "; User Id=" + objConecttion.USUARIO + "; Password=" + objConecttion.PASSWORD;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string getSCHEMA()
        {
            try
            {
                string tempSCHEMA = string.Empty;
                using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @".\\bin\param.config"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        switch (values[0])
                        {
                            case "DB":
                                tempSCHEMA = values[1];
                                break;
                        }
                    }
                }
                return tempSCHEMA;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}