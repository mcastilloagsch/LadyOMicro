using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class Religions
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? confesion { get; set; }

        public Religions()
        {

        }

        public Religions(int id, string name, int? confesion)
        {
            this.id = id;
            this.name = name;
            this.confesion = confesion;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
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
                            int? _confesion = null;
                            if (!reader.IsDBNull(2))
                            {
                                _confesion = reader.GetInt32(2);
                            }
                            objReturnList.Add(new Religions(reader.GetInt32(0), reader.GetString(1), _confesion));
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

        private static Religions getObj(int id)
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
                        int? _confesion = null;
                        if (!reader.IsDBNull(2))
                        {
                            _confesion = reader.GetInt32(2);
                        }
                        objReturnList.Add(new Religions(reader.GetInt32(0), reader.GetString(1), _confesion));
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
                Religions objReturn = Religions.getObj(id);
                if (objReturn == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_RELIGIONS_GETOBJECT_NO_EXISTE;
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


        public static object objAdd(Religions obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    if(obj.confesion >= 0 && obj.confesion < 2)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".religions VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.confesion + "');SELECT LAST_INSERT_ID();";
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
                        response.msg = Generic.Message.RELIGIONS_CONFESION_OUTVALOR;
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


        public static object objUpdate(Religions obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Religions objUpdate = new Religions();
                    objUpdate = Religions.getObj(obj.id);
                    if (objUpdate != null)
                    {
                        if (obj.name.Length > 0)
                        {
                            if(obj.confesion >= 0 && obj.confesion < 2)
                            {
                                string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".religions SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  confesion = '" + obj.confesion + "'  WHERE id =  " + obj.id;
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
                                response.data = Religions.getObj(obj.id);
                            }
                            else
                            {
                                response.isValid = false;
                                response.msg = Generic.Message.RELIGIONS_CONFESION_OUTVALOR;
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
                        response.msg = Generic.Message.ID_RELIGIONS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_RELIGIONS_NO_EXISTE;
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