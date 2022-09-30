using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class Positions
    {
        public int id { get; set; }
        public string name { get; set; }
        public int structure_type_id { get; set; }

        public Positions()
        {

        }

        public Positions(int id, string name, int structure_type_id)
        {
            this.id = id;
            this.name = name;
            this.structure_type_id = structure_type_id;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Positions> objReturnList = new List<Positions>();
                string sqlQuery = "SELECT id, name, structure_type_id FROM " + Generic.DBConnection.SCHEMA + ".positions";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Positions(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
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

        private static Positions getObj(int id)
        {
            List<Positions> objReturnList = new List<Positions>();
            string sqlQuery = "SELECT id, name, structure_type_id FROM " + Generic.DBConnection.SCHEMA + ".positions WHERE id = " + id;
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Positions(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
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
                Positions objReturn = Positions.getObj(id);
                if (objReturn == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_POSITIONS_GETOBJECT_NO_EXISTE;
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


        public static object objAdd(Positions obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.name.Length > 0)
                {
                    StructureType structure_type_Fk = new StructureType();
                    structure_type_Fk = Positions.getStructureType(obj.structure_type_id);
                    if(structure_type_Fk != null)
                    {
                        string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".positions VALUES(0, '" + Generic.Tools.Capital(obj.name) + "', '" + obj.structure_type_id + "');SELECT LAST_INSERT_ID();";
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
                        response.msg = Generic.Message.ID_POSITIONS_STRUCTURETYPE_OBJ_NO_EXISTE;
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


        public static object objUpdate(Positions obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.id > 0)
                {
                    Positions objUpdate = new Positions();
                    objUpdate = Positions.getObj(obj.id);
                    StructureType structure_type_Fk = new StructureType();
                    structure_type_Fk = Positions.getStructureType(obj.structure_type_id);
                    if (objUpdate != null)
                    {
                        if(structure_type_Fk != null)
                        {
                            if (obj.name.Length > 0)
                            {
                                string sqlQueryUpdate = "UPDATE " + Generic.DBConnection.SCHEMA + ".positions SET name = '" + Generic.Tools.Capital(obj.name) + "' ,  structure_type_id = '" + obj.structure_type_id + "'  WHERE id =  " + obj.id;
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
                                response.data = Positions.getObj(obj.id);
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
                            response.msg = Generic.Message.ID_POSITIONS_STRUCTURETYPE_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_POSITIONS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_POSITIONS_NO_EXISTE;
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