using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Socioeconomics
    {
        public int id { get; set; }
        public string name { get; set; }
        public string values { get; set; }

        public Socioeconomics()
        {

        }

        public Socioeconomics(int id, string name, string values)
        {
            this.id = id;
            this.name = name;
            this.values = values;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Socioeconomics> objReturnList = new List<Socioeconomics>();
                string sqlQuery = "select * from " + Generic.DBConnection.SCHEMA + ".socioeconomics";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _values = null;
                            if (!reader.IsDBNull(2))
                            {
                                _values = reader.GetString(2);
                            }
                            objReturnList.Add(new Socioeconomics(reader.GetInt32(0), reader.GetString(1), _values));
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

        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                List<Socioeconomics> objReturnList = new List<Socioeconomics>();
                string sqlQuery = "SELECT * FROM " + Generic.DBConnection.SCHEMA + ".socioeconomics WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _values = null;
                            if (!reader.IsDBNull(2))
                            {
                                _values = reader.GetString(2);
                            }
                            objReturnList.Add(new Socioeconomics(reader.GetInt32(0), reader.GetString(1), _values));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_SOCIOECONOMICS_GETOBJECT_NO_EXISTE;
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
        private static Socioeconomics getSocioeconomic(int id)
        {
            try
            {
                List<Socioeconomics> objReturnList = new List<Socioeconomics>();
                string sqlQuery = "SELECT * FROM " + Generic.DBConnection.SCHEMA + ".socioeconomics WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _values = null;
                            if (!reader.IsDBNull(2))
                            {
                                _values = reader.GetString(2);
                            }
                            objReturnList.Add(new Socioeconomics(reader.GetInt32(0), reader.GetString(1), _values));
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


        public static object ObjInsert(Socioeconomics objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            Socioeconomics objData = new Socioeconomics();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".socioeconomics VALUES(0, '" + objInsert.name + "', '" + objInsert.values + "' );SELECT LAST_INSERT_ID();";
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
                    objData.values = objInsert.values;
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = objData;
                    return response;
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.NAME_NO_EXISTE;
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


        public static object ObjUpdate(Socioeconomics objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            Socioeconomics objData = new Socioeconomics();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Socioeconomics valid = getSocioeconomic(objUpdate.id);
                    if (valid != null)
                    {
                        string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".socioeconomics SET name = '" + objUpdate.name + "', values = '" + objUpdate.values + "' WHERE id =  " + objUpdate.id;
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
                        objData.values = objUpdate.values;
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = objData;
                        return response;
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_SOCIOECONOMICS_NO_EXISTE;
                        response.data = null;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.NAME_NO_EXISTE;
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
    }
}