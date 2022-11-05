using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class SocioEconomic
    {
        public int IdSocioEconomic { get; set; }
        public string SocioEconomicName { get; set; }
        public bool IsDeleted { get; set; }

        public SocioEconomic()
        {

        }

        public SocioEconomic(int idSocioEconomic, string socioEconomicName, bool isDeleted)
        {
            IdSocioEconomic = idSocioEconomic;
            SocioEconomicName = socioEconomicName;
            IsDeleted = isDeleted;
        }

        public static SocioEconomic getObj(int idSocioEconomic)
        {
            List<SocioEconomic> objReturnList = new List<SocioEconomic>();
            string sqlQuery = "SELECT IdSocioEconomic, SocioEconomicName, IsDeleted FROM " + nameof(SocioEconomic).ToUpper() + " WHERE IsDeleted = 0 AND IdSocioEconomic = " + idSocioEconomic + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new SocioEconomic(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                SocioEconomic objReturn = SocioEconomic.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_SOCIOECONOMICS_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(SocioEconomic obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.SocioEconomicName.Length > 0)
                {
                    obj.SocioEconomicName = Generic.Tools.Capital(obj.SocioEconomicName);
                    string sqlQuery = "INSERT INTO " + nameof(SocioEconomic).ToUpper() + " VALUES(NULL, '" + obj.SocioEconomicName + "', 0); SELECT LAST_INSERT_ID();";
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            obj.IdSocioEconomic = Convert.ToInt32(comando.ExecuteScalar());
                            conexion.Close();
                        }
                    }
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = SocioEconomic.getObj(obj.IdSocioEconomic);
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

        public static object objUpdate(SocioEconomic obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdSocioEconomic > 0)
                {
                    if (SocioEconomic.getObj(obj.IdSocioEconomic) != null)
                    {
                        if (obj.SocioEconomicName.Length > 0)
                        {
                            obj.SocioEconomicName = Generic.Tools.Capital(obj.SocioEconomicName);
                            string sqlQueryUpdate = "UPDATE " + nameof(SocioEconomic).ToUpper() + " SET SocioEconomicName = '" + obj.SocioEconomicName + "' WHERE IsDeleted = 0 AND IdSocioEconomic =  " + obj.IdSocioEconomic + ";";
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
                            response.data = SocioEconomic.getObj(obj.IdSocioEconomic);
                        }
                        else
                        {
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_SOCIOECONOMICS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_SOCIOECONOMICS_NO_EXISTE;
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

        public static object objDelete(SocioEconomic obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdSocioEconomic > 0)
                {
                    if (SocioEconomic.getObj(obj.IdSocioEconomic) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (SocioEconomic.getObj(obj.IdSocioEconomic).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(SocioEconomic).ToUpper() + " SET IsDeleted = 0 WHERE IdSocioEconomic =  " + obj.IdSocioEconomic + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(SocioEconomic).ToUpper() + " SET IsDeleted = 1 WHERE IdSocioEconomic =  " + obj.IdSocioEconomic + ";";
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
                        response.data = SocioEconomic.getObj(obj.IdSocioEconomic);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_SOCIOECONOMICS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_SOCIOECONOMICS_NO_EXISTE;
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
                List<SocioEconomic> objReturnList = new List<SocioEconomic>();
                string sqlQuery = "SELECT IdSocioEconomic, SocioEconomicName, IsDeleted FROM " + nameof(SocioEconomic).ToUpper() + " WHERE IsDeleted = 0 ORDER BY SocioEconomicName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new SocioEconomic(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                List<SocioEconomic> objReturnList = new List<SocioEconomic>();
                string sqlQuery = "SELECT IdSocioEconomic, SocioEconomicName, IsDeleted FROM " + nameof(SocioEconomic).ToUpper() + " ORDER BY SocioEconomicName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new SocioEconomic(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
