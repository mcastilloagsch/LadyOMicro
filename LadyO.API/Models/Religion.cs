using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Religion
    {
        public int IdReligion { get; set; }
        public string ReligionName { get; set; }
        public string Confesion { get; set; }
        public bool IsDeleted { get; set; }

        public Religion()
        {

        }

        public Religion(int idReligion, string religionName, string confesion, bool isDeleted)
        {
            IdReligion = idReligion;
            ReligionName = religionName;
            Confesion = confesion;
            IsDeleted = isDeleted;
        }

        private static Religion getObj(int idReligion)
        {
            List<Religion> objReturnList = new List<Religion>();
            string sqlQuery = "SELECT IdReligion, Religion, Confesion, IsDeleted FROM " + nameof(Religion).ToUpper() + " WHERE IdReligion = " + idReligion + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Religion(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                Religion objReturn = Religion.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_RELIGIONS_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Religion obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.ReligionName.Length > 0)
                {
                    if (obj.Confesion.Length > 0)
                    {
                        obj.ReligionName = Generic.Tools.Capital(obj.ReligionName);
                        string sqlQuery = "INSERT INTO " + nameof(Religion).ToUpper() + " VALUES(NULL, '" + obj.ReligionName + "', '" + obj.Confesion + "' , 0); SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdReligion = Convert.ToInt32(comando.ExecuteScalar());
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
                        response.msg = Generic.Message.RELIGIONS_CONFESION_NO_EXISTE;
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

        public static object objUpdate(Religion obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdReligion > 0)
                {
                    if (Religion.getObj(obj.IdReligion) != null)
                    {
                        if (obj.ReligionName.Length > 0)
                        {
                            if (obj.Confesion.Length > 0)
                            {
                                obj.ReligionName = Generic.Tools.Capital(obj.ReligionName);
                                string sqlQueryUpdate = "UPDATE " + nameof(Religion).ToUpper() + " SET Religion = '" + obj.ReligionName + "' , Confesion = '" + obj.Confesion + "' WHERE IdReligion =  " + obj.IdReligion + ";";
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
                                response.msg = Generic.Message.RELIGIONS_CONFESION_NO_EXISTE;
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
                        response.msg = Generic.Message.ID_RELIGIONS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_RELIGIONS_NO_EXISTE;
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

        public static object objDelete(Religion obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdReligion > 0)
                {
                    if (Religion.getObj(obj.IdReligion) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Religion.getObj(obj.IdReligion).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Religion).ToUpper() + " SET IsDeleted = 0 WHERE IdReligion =  " + obj.IdReligion + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Religion).ToUpper() + " SET IsDeleted = 1 WHERE IdReligion =  " + obj.IdReligion + ";";
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
                        response.data = Religion.getObj(obj.IdReligion);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_RELIGIONS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_RELIGIONS_NO_EXISTE;
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
                List<Religion> objReturnList = new List<Religion>();
                string sqlQuery = "SELECT IdReligion, Religion, Confesion, IsDeleted FROM " + nameof(Religion).ToUpper() + " WHERE IsDeleted = 0 ORDER BY Religion;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Religion(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                List<Religion> objReturnList = new List<Religion>();
                string sqlQuery = "SELECT IdReligion, Religion, Confesion, IsDeleted FROM " + nameof(Religion).ToUpper() + " ORDER BY Religion;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Religion(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
