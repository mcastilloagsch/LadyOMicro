using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class PositionType
    {
        #region Variables
        public int IdPositionType { get; set; }
        public string PositionTypeName { get; set; }
        public int IdStructureType { get; set; }
        public bool IsDeleted { get; set; }
        #endregion

        public PositionType()
        {

        }

        public PositionType(int idPositionType, string positionTypeName, int idStructureType, bool isDeleted)
        {
            IdPositionType = idPositionType;
            PositionTypeName = positionTypeName;
            IdStructureType = idStructureType;
            IsDeleted = isDeleted;
        }

        public static PositionType getObj(int idPositionType, bool onlyNotDeleted)
        {
            List<PositionType> objReturnList = new List<PositionType>();
            string sqlQuery = "SELECT IdPositionType, PositionTypeName, IdStructureType, IsDeleted FROM " + nameof(PositionType).ToUpper();
            if (onlyNotDeleted)
            {
                sqlQuery += " WHERE IdPositionType = " + idPositionType + " AND IsDeleted = 0;";
            }
            else
            {
                sqlQuery += " WHERE IdPositionType = " + idPositionType + ";";
            }
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new PositionType(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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
                PositionType objReturn = PositionType.getObj(id, true);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_POSITIONS_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(PositionType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (StructureType.getObj(obj.IdStructureType) != null)
                {
                    if (obj.PositionTypeName.Length > 0)
                    {
                        obj.PositionTypeName = Generic.Tools.Capital(obj.PositionTypeName);
                        string sqlQuery = "INSERT INTO " + nameof(PositionType).ToUpper() + "(IdPositionType, PositionTypeName, IdStructureType, IsDeleted)";
                        sqlQuery += " VALUES(NULL, '" + obj.PositionTypeName + "', " + obj.IdStructureType + " , 0);";
                        sqlQuery += " SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdPositionType = Convert.ToInt32(comando.ExecuteScalar());
                                conexion.Close();
                            }
                        }
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = PositionType.getObj(obj.IdPositionType, true);
                    }
                    else
                    {
                        response.msg = Generic.Message.NAME_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_POSITIONS_STRUCTURETYPE_OBJ_NO_EXISTE;
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

        public static object objUpdate(PositionType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdPositionType > 0)
                {
                    if (PositionType.getObj(obj.IdPositionType, true) != null)
                    {
                        if (StructureType.getObj(obj.IdStructureType) != null)
                        {
                            if (obj.PositionTypeName.Length > 0)
                            {
                                obj.PositionTypeName = Generic.Tools.Capital(obj.PositionTypeName);
                                string sqlQueryUpdate = "UPDATE " + nameof(PositionType).ToUpper() + " SET PositionTypeName = '" + obj.PositionTypeName + "', IdStructureType = " + obj.IdStructureType + " WHERE IdPositionType =  " + obj.IdPositionType + ";";
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
                                response.data = PositionType.getObj(obj.IdPositionType, false);
                            }
                            else
                            {
                                response.msg = Generic.Message.NAME_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.ID_POSITIONS_STRUCTURETYPE_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_POSITIONS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_POSITIONS_NO_EXISTE;
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

        public static object objDelete(PositionType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdPositionType > 0)
                {
                    if (PositionType.getObj(obj.IdPositionType, false) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (PositionType.getObj(obj.IdPositionType, false).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(PositionType).ToUpper() + " SET IsDeleted = 0 WHERE IdPositionType =  " + obj.IdPositionType + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(PositionType).ToUpper() + " SET IsDeleted = 1 WHERE IdPositionType =  " + obj.IdPositionType + ";";
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
                        response.data = PositionType.getObj(obj.IdPositionType, true);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_POSITIONS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_POSITIONS_NO_EXISTE;
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
                List<PositionType> objReturnList = new List<PositionType>();
                string sqlQuery = "SELECT IdPositionType, PositionTypeName, IdStructureType, IsDeleted FROM " + nameof(PositionType).ToUpper() + " WHERE IsDeleted = 0 ORDER BY PositionTypeName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new PositionType(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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
                List<PositionType> objReturnList = new List<PositionType>();
                string sqlQuery = "SELECT IdPositionType, PositionTypeName, IdStructureType,  IsDeleted FROM " + nameof(PositionType).ToUpper() + " ORDER BY PositionTypeName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new PositionType(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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