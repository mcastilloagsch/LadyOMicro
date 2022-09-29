using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class Branches
    {
        public int id { get; set; }
        public string name { get; set; }
        public string unit_name { get; set; }
        public string small_team { get; set; }

        public Branches()
        {

        }

        public Branches(int id, string name, string unit_name, string small_team)
        {
            this.id = id;
            this.name = name;
            this.unit_name = unit_name;
            this.small_team = small_team;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Branches> objReturnList = new List<Branches>();
                string sqlQuery = "SELECT id, name, unit_name, small_team FROM " + Generic.DBConnection.SCHEMA + ".branches";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _unit_name = null;
                            string _small_team = null;
                            if (!reader.IsDBNull(2))
                            {
                                _unit_name = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                _small_team = reader.GetString(3);
                            }
                            objReturnList.Add(new Branches(reader.GetInt32(0), reader.GetString(1), _unit_name, _small_team));
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


        private static Branches getObj(int id)
        {
            List<Branches> objReturnList = new List<Branches>();
            string sqlQuery = "SELECT id, name, unit_name, small_team FROM " + Generic.DBConnection.SCHEMA + ".branches WHERE id = " + id;
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        string _unit_name = null;
                        string _small_team = null;
                        if (!reader.IsDBNull(2))
                        {
                            _unit_name = reader.GetString(2);
                        }
                        if (!reader.IsDBNull(3))
                        {
                            _small_team = reader.GetString(3);
                        }
                        objReturnList.Add(new Branches(reader.GetInt32(0), reader.GetString(1), _unit_name, _small_team));
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
                Branches objReturn = Branches.getObj(id);
                if (objReturn == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_BRANCHES_GETOBJECT_NO_EXISTE;
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


        public static object objAdd(Branches obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".branches VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.unit_name + "', '" + obj.small_team + "');SELECT LAST_INSERT_ID();";
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

        
        public static object objUpdate(Branches obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Branches objUpdate = new Branches();
                    objUpdate = Branches.getObj(obj.id);
                    if (objUpdate != null)
                    {
                        if (obj.name.Length > 0)
                        {
                            string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".branches SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  unit_name = '" + obj.unit_name + "', small_team = '" + obj.small_team + "'  WHERE id =  " + obj.id;
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
                            response.data = Branches.getObj(obj.id);
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
                        response.msg = Generic.Message.ID_BRANCHES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_BRANCHES_NO_EXISTE;
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