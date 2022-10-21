using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class StructureType
    {
        public int IdStructureType { get; set; }
        public string StructureTypeName { get; set; }
        public bool IsDeleted { get; set; }

        public StructureType()
        {

        }

        public StructureType(int idStructureType, string structureTypeName, bool isDeleted)
        {
            IdStructureType = idStructureType;
            StructureTypeName = structureTypeName;
            IsDeleted = isDeleted;
        }

        public static StructureType getObj(int idStructureType)
        {
            List<StructureType> objReturnList = new List<StructureType>();
            string sqlQuery = "SELECT IdStructureType, StructureTypeName, IsDeleted FROM " + nameof(StructureType).ToUpper() + " WHERE IsDeleted = 0 AND IdStructureType = " + idStructureType + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
                    }
                    conexion.Close();
                }
            }
            return objReturnList.FirstOrDefault();
        }

        public static object getObject(int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.isValid = false;
            response.data = null;
            try
            {
                StructureType objReturn = StructureType.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_STRUCTURETYPE_GETOBJECT_NO_EXISTE;
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
                response.msg = ex.Message;
                return response;
            }
        }

        public static object objAdd(StructureType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.StructureTypeName.Length > 0)
                {
                    obj.StructureTypeName = Generic.Tools.Capital(obj.StructureTypeName);
                    string sqlQuery = "INSERT INTO " + nameof(StructureType).ToUpper() + " VALUES(NULL, '" + obj.StructureTypeName + "', 0); SELECT LAST_INSERT_ID();";
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            obj.IdStructureType = Convert.ToInt32(comando.ExecuteScalar());
                            conexion.Close();
                        }
                    }
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = StructureType.getObj(obj.IdStructureType);
                }
                else
                {
                    response.msg = Generic.Message.NAME_NO_EXISTE;
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                return response;
            }
        }

        public static object objUpdate(StructureType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdStructureType > 0)
                {
                    if (StructureType.getObj(obj.IdStructureType) != null)
                    {
                        if (obj.StructureTypeName.Length > 0)
                        {
                            obj.StructureTypeName = Generic.Tools.Capital(obj.StructureTypeName);
                            string sqlQueryUpdate = "UPDATE " + nameof(StructureType).ToUpper() + " SET StructureTypeName = '" + obj.StructureTypeName + "' WHERE IsDeleted = 0 AND IdStructureType =  " + obj.IdStructureType + ";";
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
                            response.data = StructureType.getObj(obj.IdStructureType);
                        }
                        else
                        {
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_STRUCTURETYPE_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_STRUCTURETYPE_NO_EXISTE;
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                return response;
            }
        }

        public static object objDelete(StructureType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdStructureType > 0)
                {
                    if (StructureType.getObj(obj.IdStructureType) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (StructureType.getObj(obj.IdStructureType).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(StructureType).ToUpper() + " SET IsDeleted = 0 WHERE IdStructureType =  " + obj.IdStructureType + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(StructureType).ToUpper() + " SET IsDeleted = 1 WHERE IdStructureType =  " + obj.IdStructureType + ";";
                        }
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
                        response.data = null;
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_STRUCTURETYPE_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_STRUCTURETYPE_NO_EXISTE;
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                return response;
            }
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<StructureType> objReturnList = new List<StructureType>();
                string sqlQuery = "SELECT IdStructureType, StructureTypeName, IsDeleted FROM " + nameof(StructureType).ToUpper() + " WHERE IsDeleted = 0 ORDER BY StructureTypeName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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

        public static object getListAdm(int idPerson)
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<StructureType> objReturnList = new List<StructureType>();
                string sqlQuery = "SELECT IdStructureType, StructureTypeName, IsDeleted FROM " + nameof(StructureType).ToUpper() + " ORDER BY StructureTypeName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new StructureType(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
    }
}
