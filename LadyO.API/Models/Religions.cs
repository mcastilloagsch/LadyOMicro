using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Religions
    {
        #region Atributos
        public int id { get; set; }
        public string name { get; set; }
        public bool confesion { get; set; }
        #endregion

        public Religions()
        {

        }

        public Religions(int id, string name, bool confesion)
        {
            this.id = id;
            this.name = name;
            this.confesion = confesion;
        }

        public static bool ObjInsert(Religions objInsert)
        {
            try
            {
                string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".religions VALUES(0, '" + objInsert.name + "', " + (objInsert.confesion ? 1 : 0) + ")";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        comando.ExecuteReader();
                        conexion.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ObjUpdate(Religions objUpdate)
        {
            try
            {
                if (ObjList(objUpdate.id).Count == 1)
                {
                    string sqlQuery = "UPDATE " + Generic.DBConnection.SCHEMA + ".religions SET name = '" + objUpdate.name + "' WHERE id = " + objUpdate.id;
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            comando.ExecuteReader();
                            conexion.Close();
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Religions> ObjList()
        {
            try
            {
                List<Religions> objReturnList = new List<Religions>();
                string sqlQuery = "SELECT id, name, confesion FROM " + Generic.DBConnection.SCHEMA + ".religions";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Religions(reader.GetInt32(0), reader.GetString(1), (reader.GetInt32(2) == 1 ? true : false)));
                        }
                        conexion.Close();
                    }
                }
                return objReturnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<Religions> ObjList(int id)
        {
            try
            {
                List<Religions> objReturnList = new List<Religions>();
                string sqlQuery = "SELECT id, name, confesion FROM " + Generic.DBConnection.SCHEMA + ".religions WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Religions(reader.GetInt32(0), reader.GetString(1), (reader.GetInt32(2) == 1 ? true : false)));
                        }
                        conexion.Close();
                    }
                }
                return objReturnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}