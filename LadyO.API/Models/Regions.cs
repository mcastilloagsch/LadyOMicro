using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class Regions
    {
        public int id { get; set; }
        public string name { get; set; }
        public string geom { get; set; }

        public Regions()
        {

        }

        public Regions(int id, string name, string geom)
        {
            this.id = id;
            this.name = name;
            this.geom = geom;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Regions> objReturnList = new List<Regions>();
                string sqlQuery = "SELECT id, name, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".regions";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _geom = null;
                            if (!reader.IsDBNull(2))
                            {
                                _geom = reader.GetString(2);
                            }
                            objReturnList.Add(new Regions(reader.GetInt32(0), reader.GetString(1), _geom));
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

        private static Regions getObj(int id)
        {
            List<Regions> objReturnList = new List<Regions>();
            string sqlQuery = "SELECT id, name, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".regions WHERE id = " + id;
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        string _geom = null;
                        if (!reader.IsDBNull(2))
                        {
                            _geom = reader.GetString(2);
                        }
                        objReturnList.Add(new Regions(reader.GetInt32(0), reader.GetString(1), _geom));
                    }
                    conexion.Close();
                }
            }
            return objReturnList.FirstOrDefault(); ;
        }

        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                Regions objReturn = Regions.getObj(id);
                if (objReturn == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_REGIONS_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Regions obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".regions (id, name) VALUES(0, '" + obj.name + "');SELECT LAST_INSERT_ID();";
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
                    response.msg = Generic.Message.REGIONS_NO_NAME;
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

        public static object objUpdate(Regions obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            Regions objData = new Regions();
            try
            {
                if (obj.id > 0)
                {
                    Regions objUpdate = new Regions();
                    objUpdate = Regions.getObj(obj.id);
                    if (objUpdate != null)
                    {
                        if (obj.name.Length > 0)
                        {
                            string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".regions SET name = '" + Generic.Tools.Capital(obj.name) + "' WHERE id =  " + obj.id;
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
                            response.data = Regions.getObj(obj.id);
                            return response;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.REGIONS_NO_NAME;
                            response.data = null;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_REGIONS_NO_EXISTE;
                        response.data = null;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_REGIONS_NO_EXISTE;
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