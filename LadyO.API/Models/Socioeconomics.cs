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
                string sqlQuery = "select id, name, `values` from " + Generic.DBConnection.SCHEMA + ".socioeconomics";
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

        private static Socioeconomics getObj(int id)
        {
            List<Socioeconomics> objReturnList = new List<Socioeconomics>();
            string sqlQuery = "SELECT id, name, `values` FROM " + Generic.DBConnection.SCHEMA + ".socioeconomics WHERE id = " + id;
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
            return objReturnList.FirstOrDefault();
        }


        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                Socioeconomics objReturn = Socioeconomics.getObj(id);
                if (objReturn == null)
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


        public static object objAdd(Socioeconomics obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".socioeconomics VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.values + "' );SELECT LAST_INSERT_ID();";
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


        public static object objUpdate(Socioeconomics obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Socioeconomics objUpdate = new Socioeconomics();
                    objUpdate = Socioeconomics.getObj(obj.id);
                    if (objUpdate != null)
                    {
                        if (obj.name.Length > 0)
                        {
                            string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".socioeconomics SET name = '" + Generic.Tools.Capital(obj.name) + "', `values` = '" + obj.values + "' WHERE id =  " + obj.id;
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
                            response.data = Socioeconomics.getObj(obj.id);
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
                        response.msg = Generic.Message.ID_SOCIOECONOMICS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_SOCIOECONOMICS_NO_EXISTE;
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