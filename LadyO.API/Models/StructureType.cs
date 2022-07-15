using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class StructureType
    {
        public int id { get; set; }
        public string name { get; set; }
        public int priority { get; set; }

        public StructureType()
        {

        }

        public StructureType(int id, string name, int priority)
        {
            this.id = id;
            this.name = name;
            this.priority = priority;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<StructureType> objReturnList = new List<StructureType>();
                string sqlQuery = "SELECT id, name, priority FROM " + Generic.DBConnection.SCHEMA + ".structure_types";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
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
                List<StructureType> objReturnList = new List<StructureType>();
                string sqlQuery = "SELECT id, name, priority FROM " + Generic.DBConnection.SCHEMA + ".structure_types WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_STRUCTURETYPE_GETOBJECT_NO_EXISTE;
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


        private static StructureType getStructureType(int id)
        {
            try
            {
                List<StructureType> objReturnList = new List<StructureType>();
                string sqlQuery = "SELECT id, name, priority FROM " + Generic.DBConnection.SCHEMA + ".structure_types WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
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

        public static object ObjInsert(StructureType objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            StructureType objData = new StructureType();
            try
            {
                string str_name = objInsert.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    if (objInsert.priority >= 0)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".structure_types VALUES(0, '" + objInsert.name + "', '" + objInsert.priority + "');SELECT LAST_INSERT_ID();";
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
                        objData.priority = objInsert.priority;
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = objData;
                        return response;
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.STRUCTURETYPE_POSITION;
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

        public static object ObjUpdate(StructureType objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            StructureType objData = new StructureType();
            try
            {
                string str_name = objUpdate.name;
                string String_name = Regex.Replace(str_name, @"\s", "");
                int length_name = String_name.Length;
                if (length_name >= 1)
                {
                    StructureType valid = getStructureType(objUpdate.id);
                    if (valid != null)
                    {
                        if (objUpdate.priority >= 0)
                        {
                            string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".structure_types SET name = '" + objUpdate.name + "' ,  priority = '" + objUpdate.priority + "'  WHERE id =  " + objUpdate.id;
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
                            objData.priority = objUpdate.priority;
                            response.isValid = true;
                            response.msg = string.Empty;
                            response.data = objData;
                            return response;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.STRUCTURETYPE_POSITION;
                            response.data = null;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_STRUCTURETYPE_NO_EXISTE;
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