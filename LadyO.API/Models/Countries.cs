using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Countries
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nationality { get; set; }
        public string iso { get; set; }

        public Countries()
        {

        }

        public Countries(int id, string name, string nationality, string iso)
        {
            this.id = id;
            this.name = name;
            this.nationality = nationality;
            this.iso = iso;
        }

        public static bool ObjInsert(Countries objInsert)
        {
            try
            {
                string str_name = objInsert.name;
                int length_name = str_name.Length;
                if (length_name >= 1)
                {
                    string str_nationality = objInsert.nationality;
                    int length_nationality = str_nationality.Length;
                    if (length_nationality >= 1)
                    {
                        string str_iso = objInsert.iso;
                        int length_iso = str_iso.Length;
                        if (length_iso >= 1)
                        {
                            string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".countries VALUES(0, '" + objInsert.name + "', '" + objInsert.nationality + "', '" + objInsert.iso + "')";
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
                    else
                    {
                        return false;
                    }
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

        public static bool ObjUpdate(Countries objUpdate)
        {
            try
            {
                string str_name = objUpdate.name;
                int length_name = str_name.Length;
                if(length_name >= 1)
                {
                    string str_nationality = objUpdate.nationality;
                    int length_nationality = str_nationality.Length;
                    if(length_nationality >= 1)
                    {
                        string str_iso = objUpdate.iso;
                        int length_iso = str_iso.Length;
                        if(length_iso >= 1)
                        {
                            if (ObjList(objUpdate.id).Count == 1)
                            {
                                string sqlQuery = "UPDATE " + Generic.DBConnection.SCHEMA + ".countries SET name = '" + objUpdate.name + "' ,  nationality = '" + objUpdate.nationality + "', iso = '" + objUpdate.nationality + "'  WHERE id =  " + objUpdate.id;
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
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
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

        public static List<Countries> ObjList(int id)
        {
            try
            {
                List<Countries> objReturnList = new List<Countries>();
                string sqlQuery = "SELECT id, name, nationality, iso FROM " + Generic.DBConnection.SCHEMA + ".countries WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Countries(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
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

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Countries> objReturnList = new List<Countries>();
                string sqlQuery = "SELECT id, name, nationality, iso FROM " + Generic.DBConnection.SCHEMA + ".countries";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Countries(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                        }
                        conexion.Close();
                    }
                }
                response.isValid = true;
                response.msg = string.Empty;
                response.data = objReturnList;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }




}
