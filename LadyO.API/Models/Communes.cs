using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class Communes
    {
        public int id { get; set; }
        public string name { get; set; }
        public int province_id { get; set; }
        public string geom { get; set; }

        public Communes()
        {

        }

        public Communes(int id, string name, int province_id, string geom)
        {
            this.id = id;
            this.name = name;
            this.province_id = province_id;
            this.geom = geom;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Communes> objReturnList = new List<Communes>();
                string sqlQuery = "SELECT id, name, province_id, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".communes";
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
                            objReturnList.Add(new Communes(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _geom));
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
                List<Communes> objReturnList = new List<Communes>();
                string sqlQuery = "SELECT id, name, province_id, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".communes WHERE id = " + id;
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
                            objReturnList.Add(new Communes(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _geom));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_COMMUNES_GETOBJECT_NO_EXISTE;
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


        private static Communes getCommune(int id)
        {
            try
            {
                List<Communes> objReturnList = new List<Communes>();
                string sqlQuery = "SELECT id, name, province_id, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".communes WHERE id = " + id;
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
                            objReturnList.Add(new Communes(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _geom));
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

        private static Provinces getProvince(int id)
        {
            try
            {
                List<Provinces> objReturnList = new List<Provinces>();
                string sqlQuery = "SELECT id, name, province_id, ST_AsText(geom) FROM " + Generic.DBConnection.SCHEMA + ".provinces WHERE id = " + id;
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

        public static object ObjInsert(Communes objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            Communes objData = new Communes();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Provinces validFk = getProvince(objInsert.province_id);
                    if (validFk != null)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".communes VALUES(0, '" + objInsert.name + "', '" + objInsert.province_id + "', ST_GeomFromText('" + objInsert.geom + "'));SELECT LAST_INSERT_ID();";
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
                        objData.province_id = objInsert.province_id;
                        objData.geom = objInsert.geom;
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = objData;
                        return response;
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_COMMUNES_PROVINCES_OBJ_NO_EXISTE;
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

        public static object ObjUpdate(Communes objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            Communes objData = new Communes();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Communes valid = getCommune(objUpdate.id);
                    Provinces validFk = getProvince(objUpdate.province_id);
                    if (valid != null)
                    {
                        if (validFk != null)
                        {
                            string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".communes SET name = '" + objUpdate.name + "' ,  province_id = '" + objUpdate.province_id + "', geom = ST_GeomFromText('" + objUpdate.geom + "') WHERE id =  " + objUpdate.id;
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
                            objData.province_id = objUpdate.province_id;
                            objData.geom = objUpdate.geom;
                            response.isValid = true;
                            response.msg = string.Empty;
                            response.data = objData;
                            return response;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.ID_COMMUNES_PROVINCES_OBJ_NO_EXISTE;
                            response.data = null;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_COMMUNES_NO_EXISTE;
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