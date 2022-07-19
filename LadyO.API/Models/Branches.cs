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

        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
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
                if (objReturnList.FirstOrDefault() == null)
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


        private static Branches getBranch(int id)
        {
            try
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

        public static object ObjInsert(Branches objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            Branches objData = new Branches();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".branches VALUES(0, '" + objInsert.name + "', '" + objInsert.unit_name + "', '" + objInsert.small_team + "');SELECT LAST_INSERT_ID();";
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
                    objData.unit_name = objInsert.unit_name;
                    objData.small_team = objInsert.small_team;
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

        public static object ObjUpdate(Branches objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            Branches objData = new Branches();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    Branches valid = getBranch(objUpdate.id);
                    if (valid != null)
                    {
                        string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".branches SET name = '" + objUpdate.name + "' ,  unit_name = '" + objUpdate.unit_name + "', small_team = '" + objUpdate.small_team + "'  WHERE id =  " + objUpdate.id;
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
                        objData.unit_name = objUpdate.unit_name;
                        objData.small_team = objUpdate.small_team;
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = objData;
                        return response;
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_BRANCHES_NO_EXISTE;
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