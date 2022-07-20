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

        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
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
                if (objReturnList.FirstOrDefault() == null)
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

        private static Regions getRegion(int id)
        {
            try
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

        public static object ObjInsert(Regions objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            Regions objData = new Regions();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {

                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".regions VALUES(0, '" + objInsert.name + "', ST_GeomFromText('" + objInsert.geom + "'));SELECT LAST_INSERT_ID();";
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
                    objData.geom = objInsert.geom;
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

        public static object ObjUpdate(Regions objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            Regions objData = new Regions();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Regions valid = getRegion(objUpdate.id);
                    if (valid != null)
                    {

                        string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".regions SET name = '" + objUpdate.name + "' ,  geom = ST_GeomFromText('" + objUpdate.geom + "')  WHERE id =  " + objUpdate.id;
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
                        objData.geom = objUpdate.geom;
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = objData;
                        return response;

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