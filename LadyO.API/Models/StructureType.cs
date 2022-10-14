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
        public int? priority { get; set; }

        public StructureType()
        {

        }

        public StructureType(int id, string name, int? priority)
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
                            int? _priority = null;
                            if (!reader.IsDBNull(2))
                            {
                                _priority = reader.GetInt32(2);
                            }
                            objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), _priority));
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


        private static StructureType getObj(int id)
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
                        int? _priority = null;
                        if (!reader.IsDBNull(2))
                        {
                            _priority = reader.GetInt32(2);
                        }
                        objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), _priority));
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
                StructureType objReturn = StructureType.getObj(id);
                if (objReturn == null)
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


        public static object objAdd(StructureType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    if(obj.priority >= 0)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".structure_types VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.priority + "');SELECT LAST_INSERT_ID();";
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
                        response.msg = Generic.Message.STRUCTURETYPE_POSITION;
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
        

        public static object objUpdate(StructureType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    StructureType objUpdate = new StructureType();
                    objUpdate = StructureType.getObj(obj.id);
                    if (objUpdate != null)
                    {
                        if (obj.name.Length > 0)
                        {
                            if(obj.priority >= 0)
                            {
                                string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".structure_types SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  priority = '" + obj.priority + "'  WHERE id =  " + obj.id;
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
                                response.data = StructureType.getObj(obj.id);
                            }
                            else
                            {
                                response.isValid = false;
                                response.msg = Generic.Message.STRUCTURETYPE_POSITION;
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
                        response.msg = Generic.Message.ID_STRUCTURETYPE_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_STRUCTURETYPE_NO_EXISTE;
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