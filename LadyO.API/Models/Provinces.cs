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

        private static Provinces getObj(int id)
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
            return objReturnList.FirstOrDefault(); 
        }


        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                Provinces objReturn = Provinces.getObj(id);
                if (objReturn == null)
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

        private static Regions getRegion(int id)
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
            return objReturnList.FirstOrDefault(); 
        }

        public static object objAdd(Provinces obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    Regions regions_Fk = new Regions();
                    regions_Fk = Provinces.getRegion(obj.region_id);
                    if (regions_Fk != null)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".provinces (id,name,region_id) VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.region_id + "');SELECT LAST_INSERT_ID();";
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
                        response.msg = Generic.Message.ID_PROVINCES_REGIONS_OBJ_NO_EXISTE;
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

        public static object objUpdate(Provinces obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Provinces objUpdate = new Provinces();
                    objUpdate = Provinces.getObj(obj.id);
                    Regions regions_Fk = new Regions();
                    regions_Fk = Provinces.getRegion(obj.region_id);
                    if (objUpdate != null)
                    {
                        if(regions_Fk != null)
                        {
                            if (obj.name.Length > 0)
                            {
                                string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".provinces SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  region_id = '" + obj.region_id + "'  WHERE id =  " + obj.id;
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
                                response.data = Provinces.getObj(obj.id);
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
                            response.msg = Generic.Message.ID_PROVINCES_REGIONS_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_PROVINCES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_PROVINCES_NO_EXISTE;
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