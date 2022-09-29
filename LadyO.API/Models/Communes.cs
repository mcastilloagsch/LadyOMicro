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

        private static Communes getObj(int id)
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
            return objReturnList.FirstOrDefault();
        }


        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                Communes objReturn = Communes.getObj(id);
                if (objReturn == null)
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
        

        private static Provinces getProvince(int id)
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

        public static object objAdd(Communes obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                Provinces provinces_Fk = new Provinces();
                provinces_Fk = Communes.getProvince(obj.province_id);
                if(provinces_Fk != null)
                {
                    if (obj.name.Length > 0)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".communes (id,name,province_id) VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.province_id + "');SELECT LAST_INSERT_ID();";
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
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_COMMUNES_PROVINCES_OBJ_NO_EXISTE;
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

        public static object objUpdate(Communes obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Communes objUpdate = new Communes();
                    objUpdate = Communes.getObj(obj.id);
                    Provinces provinces_Fk = new Provinces();
                    provinces_Fk = Communes.getProvince(obj.province_id);
                    if (objUpdate != null)
                    {
                        if(provinces_Fk != null){
                            if (obj.name.Length > 0)
                            {
                                string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".communes SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  province_id = '" + obj.province_id + "' WHERE id =  " + obj.id;
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
                                response.data = Communes.getObj(obj.id);
                                return response;
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
                        response.msg = Generic.Message.ID_COMMUNES_PROVINCES_OBJ_NO_EXISTE;
                        return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_COMMUNES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_COMMUNES_NO_EXISTE;
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