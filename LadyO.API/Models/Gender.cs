using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Gender
    {
        public int IdGender { get; set; }
        public string GenderName { get; set; }
        public bool IsDeleted { get; set; }

        public Gender()
        {

        }

        public Gender(int idGender, string genderName, bool isDeleted)
        {
            IdGender = idGender;
            GenderName = genderName;
            IsDeleted = isDeleted;
        }

        private static Gender getObj(int idGender)
        {
            List<Gender> objReturnList = new List<Gender>();
            string sqlQuery = "SELECT IdGender, GenderName, IsDeleted FROM " + nameof(Gender).ToUpper() + " WHERE IdGender = " + idGender + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Gender(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                Gender objReturn = Gender.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_GENDERS_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Gender obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.GenderName.Length > 0)
                {
                    obj.GenderName = Generic.Tools.Capital(obj.GenderName);
                    string sqlQuery = "INSERT INTO " + nameof(Gender).ToUpper() + " VALUES(NULL, '" + obj.GenderName + "', 0); SELECT LAST_INSERT_ID();";
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            obj.IdGender = Convert.ToInt32(comando.ExecuteScalar());
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

        public static object objUpdate(Gender obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdGender > 0)
                {
                    if (Gender.getObj(obj.IdGender) != null)
                    {
                        if (obj.GenderName.Length > 0)
                        {
                            obj.GenderName = Generic.Tools.Capital(obj.GenderName);
                            string sqlQueryUpdate = "UPDATE " + nameof(Gender).ToUpper() + " SET GenderName = '" + obj.GenderName + "' WHERE IdGender =  " + obj.IdGender + ";";
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
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_GENDERS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_GENDERS_NO_EXISTE;
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

        public static object objDelete(Gender obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdGender > 0)
                {
                    if (Gender.getObj(obj.IdGender) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Gender.getObj(obj.IdGender).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Gender).ToUpper() + " SET IsDeleted = 0 WHERE IdGender =  " + obj.IdGender + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Gender).ToUpper() + " SET IsDeleted = 1 WHERE IdGender =  " + obj.IdGender + ";";
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
                        response.data = Gender.getObj(obj.IdGender);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_GENDERS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_GENDERS_NO_EXISTE;
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
                List<Gender> objReturnList = new List<Gender>();
                string sqlQuery = "SELECT IdGender, GenderName, IsDeleted FROM " + nameof(Gender).ToUpper() + " WHERE IsDeleted = 0 ORDER BY GenderName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Gender(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                List<Gender> objReturnList = new List<Gender>();
                string sqlQuery = "SELECT IdGender, GenderName, IsDeleted FROM " + nameof(Gender).ToUpper() + " ORDER BY GenderName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Gender(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
