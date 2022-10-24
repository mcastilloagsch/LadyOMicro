using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class DniType
    {
        public int IdDniType { get; set; }
        public string DniTypeName { get; set; }
        public string ShortName { get; set; }
        public bool IsDeleted { get; set; }

        public DniType()
        {

        }

        public DniType(int idDniType, string dniTypeName, string shortName, bool isDeleted)
        {
            IdDniType = idDniType;
            DniTypeName = dniTypeName;
            ShortName = shortName;
            IsDeleted = isDeleted;
        }

        private static DniType getObj(int idDniType)
        {
            List<DniType> objReturnList = new List<DniType>();
            string sqlQuery = "SELECT IdDniType, DniTypeName, ShortName, IsDeleted FROM " + nameof(DniType).ToUpper() + " WHERE IdDniType = " + idDniType + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new DniType(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                DniType objReturn = DniType.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_DNITYPE_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(DniType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.DniTypeName.Length > 0)
                {
                    if (obj.ShortName.Length > 0)
                    {
                        obj.DniTypeName = Generic.Tools.Capital(obj.DniTypeName);
                        string sqlQuery = "INSERT INTO " + nameof(DniType).ToUpper() + " VALUES(NULL, '" + obj.DniTypeName + "', '" + obj.ShortName + "' , 0); SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdDniType = Convert.ToInt32(comando.ExecuteScalar());
                                obj.IsDeleted = false;
                                conexion.Close();
                            }
                        }
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = obj;
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_DNITYPE_SHORTNAME_NO_EXISTE;
                        return response;
                    }
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

        public static object objUpdate(DniType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdDniType > 0)
                {
                    if (DniType.getObj(obj.IdDniType) != null)
                    {
                        if (obj.DniTypeName.Length > 0)
                        {
                            if (obj.ShortName.Length > 0)
                            {
                                obj.DniTypeName = Generic.Tools.Capital(obj.DniTypeName);
                                string sqlQueryUpdate = "UPDATE " + nameof(DniType).ToUpper() + " SET DniTypeName = '" + obj.DniTypeName + "' , ShortName = '" + obj.ShortName + "' WHERE IdDniType =  " + obj.IdDniType + ";";
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
                                response.data = obj;
                            }
                            else
                            {
                                response.msg = Generic.Message.ID_DNITYPE_SHORTNAME_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_DNITYPE_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_DNITYPE_NO_EXISTE;
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

        public static object objDelete(DniType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdDniType > 0)
                {
                    if (DniType.getObj(obj.IdDniType) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (DniType.getObj(obj.IdDniType).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(DniType).ToUpper() + " SET IsDeleted = 0 WHERE IdDniType =  " + obj.IdDniType + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(DniType).ToUpper() + " SET IsDeleted = 1 WHERE IdDniType =  " + obj.IdDniType + ";";
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
                        response.data = DniType.getObj(obj.IdDniType);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_DNITYPE_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_DNITYPE_NO_EXISTE;
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
                List<DniType> objReturnList = new List<DniType>();
                string sqlQuery = "SELECT IdDniType, DniTypeName, ShortName, IsDeleted FROM " + nameof(DniType).ToUpper() + " WHERE IsDeleted = 0 ORDER BY DniTypeName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new DniType(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                List<DniType> objReturnList = new List<DniType>();
                string sqlQuery = "SELECT IdDniType, DniTypeName, ShortName, IsDeleted FROM " + nameof(DniType).ToUpper() + " ORDER BY DniTypeName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new DniType(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
