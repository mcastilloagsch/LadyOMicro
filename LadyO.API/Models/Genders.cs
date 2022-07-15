using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Genders
    {
        public int id { get; set; }
        public string name { get; set; }

        public Genders()
        {

        }

        public Genders(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                List<Genders> objReturnList = new List<Genders>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".genders WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Genders(reader.GetInt32(0), reader.GetString(1)));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_GENDERS_NO_EXISTE;
                    response.data = null;
                    return response;
                }
                else
                {
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = objReturnList.FirstOrDefault();
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }
        private static Genders getGender(int id)
        {
            try
            {
                List<Genders> objReturnList = new List<Genders>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".genders WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Genders(reader.GetInt32(0), reader.GetString(1)));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() != null)
                {
                    return objReturnList.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static object ObjInsert(Genders objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            Genders objData = new Genders();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".genders VALUES(0, '" + objInsert.name +"');SELECT LAST_INSERT_ID();";
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())

                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            objData.id = Convert.ToInt32(comando.ExecuteScalar());
                            conexion.Close();
                        }
                    }
                    objData.name = objInsert.name;
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = objData;
                    return response;
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.NAME_GENDERS_SIN_CARACTERES;
                    response.data = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }


        public static object ObjUpdate(Genders objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            Genders objData = new Genders();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Genders valid = getGender(objUpdate.id);
                    if (valid != null)
                    {
                        string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".genders SET name = '" + objUpdate.name +"'  WHERE id =  " + objUpdate.id;
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQueryUpdate, conexion))
                            {
                                conexion.Open();
                                comando.ExecuteReader();
                                conexion.Close();
                            }
                        }
                        objData.id = objUpdate.id;
                        objData.name = objUpdate.name;
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = objData;
                        return response;
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_GENDERS_NO_EXISTE;
                        response.data = null;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.NAME_GENDERS_NO_EXISTE;
                    response.data = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }

        public static List<Genders> ObjList()
        {
            try
            {
                List<Genders> objReturnList = new List<Genders>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".genders";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Genders(reader.GetInt32(0), reader.GetString(1)));
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

        private static List<Genders> ObjList(int id)
        {
            try
            {
                List<Genders> objReturnList = new List<Genders>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".genders WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Genders(reader.GetInt32(0), reader.GetString(1)));
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
                List<Genders> objReturnList = new List<Genders>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".genders";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Genders(reader.GetInt32(0), reader.GetString(1)));
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