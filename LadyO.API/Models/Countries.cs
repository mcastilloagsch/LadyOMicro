using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

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
                            string _name = null;
                            string _nationality = null;
                            string _iso = null;
                            if (!reader.IsDBNull(1))
                            {
                                _name = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                _nationality = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                _iso = reader.GetString(3);
                            }
                            objReturnList.Add(new Countries(reader.GetInt32(0), _name, _nationality, _iso));
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
                            string _name = null;
                            string _nationality = null;
                            string _iso = null;
                            if (!reader.IsDBNull(1))
                            {
                                _name = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                _nationality = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                _iso = reader.GetString(3);
                            }
                            objReturnList.Add(new Countries(reader.GetInt32(0), _name, _nationality, _iso));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_COUNTRIES_GETOBJECT_NO_EXISTE;
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


        private static Countries getCountry(int id)
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
                            string _name = null;
                            string _nationality = null;
                            string _iso = null;
                            if (!reader.IsDBNull(1))
                            {
                                _name = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                _nationality = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                _iso = reader.GetString(3);
                            }
                            objReturnList.Add(new Countries(reader.GetInt32(0), _name, _nationality, _iso));
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

        public static object ObjInsert(Countries objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            Countries objData = new Countries();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    string str_nationality = objInsert.nationality;
                    string String_nationality = Regex.Replace(str_nationality, @"\s", "");
                    int length_nationality = String_nationality.Length;
                    if (length_nationality >= 1)
                    {
                        string str_iso = objInsert.iso;
                        string String_iso = Regex.Replace(str_iso, @"\s", "");
                        int length_iso = String_iso.Length;
                        if (length_iso >= 1)
                        {
                            string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".countries VALUES(0, '" + objInsert.name + "', '" + objInsert.nationality + "', '" + objInsert.iso + "');SELECT LAST_INSERT_ID();";
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
                            objData.nationality = objInsert.nationality;
                            objData.iso = objInsert.iso;
                            response.isValid = true;
                            response.msg = string.Empty;
                            response.data = objData;
                            return response;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.ISO_COUNTRIES_NO_EXISTE;
                            response.data = null;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.NATIONALITY_COUNTRIES_NO_EXISTE;
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

        public static object ObjUpdate(Countries objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            Countries objData = new Countries();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    string str_nationality = objUpdate.nationality;
                    string String_nationality = Regex.Replace(str_nationality, @"\s", "");
                    int length_nationality = String_nationality.Length;
                    if (length_nationality >= 1)
                    {
                        string str_iso = objUpdate.iso;
                        string String_iso = Regex.Replace(str_iso, @"\s", "");
                        int length_iso = String_iso.Length;
                        if (length_iso >= 1)
                        {
                            Countries valid = getCountry(objUpdate.id);
                            if (valid != null)
                            {
                                string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".countries SET name = '" + objUpdate.name + "' ,  nationality = '" + objUpdate.nationality + "', iso = '" + objUpdate.iso + "'  WHERE id =  " + objUpdate.id;
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
                                objData.nationality = objUpdate.nationality;
                                objData.iso = objUpdate.iso;
                                response.isValid = true;
                                response.msg = string.Empty;
                                response.data = objData;
                                return response;
                            }
                            else
                            {
                                response.isValid = false;
                                response.msg = Generic.Message.ID_COUNTRIES_NO_EXISTE;
                                response.data = null;
                                return response;
                            }
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.ISO_COUNTRIES_NO_EXISTE;
                            response.data = null;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.NATIONALITY_COUNTRIES_NO_EXISTE;
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
