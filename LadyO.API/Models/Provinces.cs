using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class Provinces
    {
        public int id { get; set; }
        public string name { get; set; }
        public int region_id { get; set; }
        public string geom { get; set; }

        public Provinces()
        {

        }

        public Provinces(int id, string name, int region_id, string geom)
        {
            this.id = id;
            this.name = name;
            this.region_id = region_id;
            this.geom = geom;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Provinces> objReturnList = new List<Provinces>();
                string sqlQuery = "SELECT id, name, region_id, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".provinces";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _geom = null;
                            if (!reader.IsDBNull(3))
                            {
                                _geom = reader.GetString(3);
                            }
                            objReturnList.Add(new Provinces(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _geom));
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
                List<Provinces> objReturnList = new List<Provinces>();
                string sqlQuery = "SELECT id, name, region_id, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".provinces WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _geom = null;
                            if (!reader.IsDBNull(3))
                            {
                                _geom = reader.GetString(3);
                            }
                            objReturnList.Add(new Provinces(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _geom));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_PROVINCES_GETOBJECT_NO_EXISTE;
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


        private static Provinces getProvince(int id)
        {
            try
            {
                List<Provinces> objReturnList = new List<Provinces>();
                string sqlQuery = "SELECT id, name, region_id, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".provinces WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _geom = null;
                            if (!reader.IsDBNull(3))
                            {
                                _geom = reader.GetString(3);
                            }
                            objReturnList.Add(new Provinces(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _geom));
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

        public static object ObjInsert(Provinces objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            Provinces objData = new Provinces();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Regions validFk = getRegion(objInsert.region_id);
                    if (validFk != null)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".provinces VALUES(0, '" + objInsert.name + "', '" + objInsert.region_id + "', ST_GeomFromText('" + objInsert.geom + "'));SELECT LAST_INSERT_ID();";
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
                        objData.region_id = objInsert.region_id;
                        objData.geom = objInsert.geom;
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = objData;
                        return response;
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_PROVINCES_REGIONS_OBJ_NO_EXISTE;
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

        public static object ObjUpdate(Provinces objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            Provinces objData = new Provinces();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Provinces valid = getProvince(objUpdate.id);
                    Regions validFk = getRegion(objUpdate.region_id);
                    if (valid != null)
                    {
                        if (validFk != null)
                        {
                            string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".provinces SET name = '" + objUpdate.name + "' ,  region_id = '" + objUpdate.region_id + "', geom = ST_GeomFromText('" + objUpdate.geom + "')  WHERE id =  " + objUpdate.id;
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
                            objData.region_id = objUpdate.region_id;
                            objData.geom = objUpdate.geom;
                            response.isValid = true;
                            response.msg = string.Empty;
                            response.data = objData;
                            return response;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.ID_PROVINCES_REGIONS_OBJ_NO_EXISTE;
                            response.data = null;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_PROVINCES_NO_EXISTE;
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