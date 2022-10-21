using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Sex
    {
        public int IdSex { get; set; }
        public string SexName { get; set; }
        public bool IsDeleted { get; set; }

        public Sex()
        {

        }

        public Sex(int idSex, string sexName, bool isDeleted)
        {
            IdSex = idSex;
            SexName = sexName;
            IsDeleted = isDeleted;
        }

        public static Sex getObj(int idSex)
        {
            List<Sex> objReturnList = new List<Sex>();
            string sqlQuery = "SELECT IdSex, SexName, IsDeleted FROM " + nameof(Sex).ToUpper() + " WHERE IsDeleted = 0 AND IsDeleted = 0 AND IdSex = " + idSex + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Sex(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                Sex objReturn = Sex.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_SEXES_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Sex obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.SexName.Length > 0)
                {
                    obj.SexName = Generic.Tools.Capital(obj.SexName);
                    string sqlQuery = "INSERT INTO " + nameof(Sex).ToUpper() + " VALUES(NULL, '" + obj.SexName + "', 0); SELECT LAST_INSERT_ID();";
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            obj.IdSex = Convert.ToInt32(comando.ExecuteScalar());
                            conexion.Close();
                        }
                    }
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = Sex.getObj(obj.IdSex);
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

        public static object objUpdate(Sex obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdSex > 0)
                {
                    if (Sex.getObj(obj.IdSex) != null)
                    {
                        if (obj.SexName.Length > 0)
                        {
                            obj.SexName = Generic.Tools.Capital(obj.SexName);
                            string sqlQueryUpdate = "UPDATE " + nameof(Sex).ToUpper() + " SET SexName = '" + obj.SexName + "' WHERE IsDeleted = 0 AND IdSex =  " + obj.IdSex + ";";
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
                            response.data = Sex.getObj(obj.IdSex);
                        }
                        else
                        {
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_SEXES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_SEXES_NO_EXISTE;
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

        public static object objDelete(Sex obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdSex > 0)
                {
                    if (Sex.getObj(obj.IdSex) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Sex.getObj(obj.IdSex).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Sex).ToUpper() + " SET IsDeleted = 0 WHERE IdSex =  " + obj.IdSex + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Sex).ToUpper() + " SET IsDeleted = 1 WHERE IdSex =  " + obj.IdSex + ";";
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
                        response.msg = Generic.Message.ID_SEXES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_SEXES_NO_EXISTE;
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
                List<Sex> objReturnList = new List<Sex>();
                string sqlQuery = "SELECT IdSex, SexName, IsDeleted FROM " + nameof(Sex).ToUpper() + " WHERE IsDeleted = 0 ORDER BY SexName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Sex(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                List<Sex> objReturnList = new List<Sex>();
                string sqlQuery = "SELECT IdSex, SexName, IsDeleted FROM " + nameof(Sex).ToUpper() + " ORDER BY SexName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Sex(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
