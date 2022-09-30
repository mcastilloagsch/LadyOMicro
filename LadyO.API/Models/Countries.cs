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

        private static Countries getObj(int id)
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
            return objReturnList.FirstOrDefault();
        }


        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                Countries objReturn = Countries.getObj(id);
                if (objReturn == null)
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
                    response.data = objReturn;
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

        public static object objAdd(Countries obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    if(obj.nationality.Length > 0)
                    {
                        if(obj.iso.Length > 0)
                        {
                            string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".countries VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.nationality + "', '" + obj.iso + "');SELECT LAST_INSERT_ID();";
                            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                            {
                                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                                {
                                    conexion.Open();
                                    obj.id = Convert.ToInt32(comando.ExecuteScalar());
                                    conexion.Close();
                                }
                            }
                            response.isValid = true;
                            response.msg = string.Empty;
                            response.data = obj;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.ISO_COUNTRIES_NO_EXISTE;
                            return response; 
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.NATIONALITY_COUNTRIES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.NAME_NO_EXISTE;
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }

        public static object objUpdate(Countries obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if(obj.id > 0)
                {
                    Countries objUpdate = new Countries();
                    objUpdate = Countries.getObj(obj.id);
                    if (objUpdate != null)
                    {
                        if (obj.name.Length > 0)
                        {
                            if (obj.nationality.Length > 0)
                            {
                                if (obj.iso.Length > 0)
                                {
                                    string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".countries SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  nationality = '" + obj.nationality + "', iso = '" + obj.iso + "'  WHERE id =  " + obj.id;
                                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                                    {
                                        using (MySqlCommand comando = new MySqlCommand(sqlQueryUpdate, conexion))
                                        {
                                            conexion.Open();
                                            comando.ExecuteReader();
                                            conexion.Close();
                                        }
                                    }
                                    response.isValid = true;
                                    response.msg = string.Empty;
                                    response.data = Countries.getObj(obj.id);
                                }
                                else
                                {
                                    response.isValid = false;
                                    response.msg = Generic.Message.ISO_COUNTRIES_NO_EXISTE;
                                    return response;
                                }
                            }
                            else
                            {
                                response.isValid = false;
                                response.msg = Generic.Message.NATIONALITY_COUNTRIES_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_COUNTRIES_GETOBJECT_NO_EXISTE;
                        return response;
                    }
                }    
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_COUNTRIES_GETOBJECT_NO_EXISTE;
                    return response;
                }
                return response;
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
