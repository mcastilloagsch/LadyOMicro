using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Position
    {
        public int IdPosition { get; set; }
        public string PositionName { get; set; }
        public int IdStructureType { get; set; }
        public bool IsDeleted { get; set; }

        public Position()
        {

        }

        public Position(int idPosition, string positionName, int idStructureType, bool isDeleted)
        {
            IdPosition = idPosition;
            PositionName = positionName;
            IdStructureType = idStructureType;
            IsDeleted = isDeleted;
        }

        private static Position getObj(int idPosition)
        {
            List<Position> objReturnList = new List<Position>();
            string sqlQuery = "SELECT IdPosition, PositionName, IdStructureType, IsDeleted FROM " + nameof(Position).ToUpper() + " WHERE IsDeleted = 0 AND IdPosition = " + idPosition + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Position(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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
                Position objReturn = Position.getObj(id);
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

        public static object objAdd(Position obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (StructureType.getObj(obj.IdStructureType) != null)
                {
                    if (obj.PositionName.Length > 0)
                    {
                        obj.PositionName = Generic.Tools.Capital(obj.PositionName);
                        string sqlQuery = "INSERT INTO " + nameof(Position).ToUpper() + " VALUES(NULL, '" + obj.PositionName + "', '" + obj.IdStructureType + "' , 0); SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdPosition = Convert.ToInt32(comando.ExecuteScalar());
                                conexion.Close();
                            }
                        }
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = Position.getObj(obj.IdPosition);
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

        public static object objUpdate(Position obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdPosition > 0)
                {
                    if (Position.getObj(obj.IdPosition) != null)
                    {
                        if (StructureType.getObj(obj.IdStructureType) != null)
                        {
                            if (obj.PositionName.Length > 0)
                            {
                                obj.PositionName = Generic.Tools.Capital(obj.PositionName);
                                string sqlQueryUpdate = "UPDATE " + nameof(Position).ToUpper() + " SET PositionName = '" + obj.PositionName + "', IdStructureType = " + obj.IdStructureType + " WHERE IdPosition =  " + obj.IdPosition + ";";
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
                                response.data = Position.getObj(obj.IdPosition);
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

        public static object objDelete(Position obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdPosition > 0)
                {
                    if (Position.getObj(obj.IdPosition) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Position.getObj(obj.IdPosition).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Position).ToUpper() + " SET IsDeleted = 0 WHERE IdPosition =  " + obj.IdPosition + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Position).ToUpper() + " SET IsDeleted = 1 WHERE IdPosition =  " + obj.IdPosition + ";";
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
                List<Position> objReturnList = new List<Position>();
                string sqlQuery = "SELECT IdPosition, PositionName, IdStructureType, IsDeleted FROM " + nameof(Position).ToUpper() + " WHERE IsDeleted = 0 ORDER BY PositionName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Position(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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
                List<Position> objReturnList = new List<Position>();
                string sqlQuery = "SELECT IdPosition, PositionName, IdStructureType,  IsDeleted FROM " + nameof(Position).ToUpper() + " ORDER BY PositionName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Position(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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
