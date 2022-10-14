using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class Structures
    {
        public int id { get; set; }
        public string name { get; set; }
        public int structure_type_id { get; set; }
        public int? parent_id { get; set; }

        public Structures()
        {

        }

        public Structures(int id, string name, int structure_type_id, int? parent_id)
        {
            this.id = id;
            this.name = name;
            this.structure_type_id = structure_type_id;
            this.parent_id = parent_id;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Structures> objReturnList = new List<Structures>();
                string sqlQuery = "SELECT id, name, structure_type_id, parent_id FROM " + Generic.DBConnection.SCHEMA + ".structures";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            int? _parent_id = null;
                            if (!reader.IsDBNull(3))
                            {
                                _parent_id = reader.GetInt32(3);
                            }
                            objReturnList.Add(new Structures(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _parent_id));
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


        private static Structures getObj(int id)
        {
            List<Structures> objReturnList = new List<Structures>();
            string sqlQuery = "SELECT id, name, structure_type_id, parent_id FROM " + Generic.DBConnection.SCHEMA + ".structures WHERE id = " + id;
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        int? _parent_id = null;
                        if (!reader.IsDBNull(3))
                        {
                            _parent_id = reader.GetInt32(3);
                        }
                        objReturnList.Add(new Structures(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _parent_id));
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
                Structures objReturn = Structures.getObj(id);
                if (objReturn == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_STRUCTURES_GETOBJECT_NO_EXISTE;
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


        private static StructureType getStructureType(int id)
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


        public static object objAdd(Structures obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    if (obj.parent_id > 0 || obj.parent_id == null)
                    {
                        StructureType structure_type_Fk = new StructureType();
                        structure_type_Fk = Structures.getStructureType(obj.structure_type_id);
                        if (structure_type_Fk != null)
                        {
                            string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".structures VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.structure_type_id + "', '" + obj.parent_id + "');SELECT LAST_INSERT_ID();";
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
                            response.msg = Generic.Message.ID_STRUCTURES_STRUCTURETYPE_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_STRUCTURES_PARENT_ID;
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

        public static object objUpdate(Structures obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Structures objUpdate = new Structures();
                    objUpdate = Structures.getObj(obj.id);
                    StructureType structure_type_Fk = new StructureType();
                    structure_type_Fk = Structures.getStructureType(obj.structure_type_id);
                    if (objUpdate != null)
                    {
                        if(structure_type_Fk != null)
                        {
                            if(obj.parent_id > 0 || obj.parent_id == null)
                            {
                                if (obj.name.Length > 0)
                                {
                                    string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".structures SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  structure_type_id = '" + obj.structure_type_id + "', parent_id = '" + obj.parent_id + "'  WHERE id =  " + obj.id;
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
                                    response.data = Structures.getObj(obj.id);
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
                                response.msg = Generic.Message.ID_STRUCTURES_PARENT_ID;
                                return response;
                            }
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.ID_STRUCTURES_STRUCTURETYPE_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_STRUCTURES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_STRUCTURES_NO_EXISTE;
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