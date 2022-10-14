using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Sexes
    {
        public int id { get; set; }
        public string name { get; set; }

        public Sexes()
        {

        }

        public Sexes(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Sexes> objReturnList = new List<Sexes>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".sexes";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Sexes(reader.GetInt32(0), reader.GetString(1)));
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

        private static Sexes getObj(int id)
        {
            List<Sexes> objReturnList = new List<Sexes>();
            string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".sexes WHERE id = " + id;
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Sexes(reader.GetInt32(0), reader.GetString(1)));
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
                Sexes objReturn = Sexes.getObj(id);
                if (objReturn == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_SEXES_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Sexes obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".sexes VALUES(0, '" + Generic.Tools.Capital(obj.name) + "');SELECT LAST_INSERT_ID();";
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

        public static object objUpdate(Sexes obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Sexes objUpdate = new Sexes();
                    objUpdate = Sexes.getObj(obj.id);
                    if (objUpdate != null)
                    {
                        if(obj.name.Length > 0)
                        {
                            string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".sexes SET name = '" + Generic.Tools.Capital(obj.name) + "'  WHERE id =  " + obj.id;
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
                            response.data = Sexes.getObj(obj.id);
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
                        response.msg = Generic.Message.ID_SEXES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_SEXES_NO_EXISTE;
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