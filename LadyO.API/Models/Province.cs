using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Province
    {
        public int IdProvince { get; set; }
        public int IdRegion { get; set; }
        public string ProvinceName { get; set; }
        public bool IsDeleted { get; set; }

        public Province()
        {

        }

        public Province(int idProvince, int idRegion, string provinceName, bool isDeleted)
        {
            IdProvince = idProvince;
            IdRegion = idRegion;
            ProvinceName = provinceName;
            IsDeleted = isDeleted;
        }

        public static Province getObj(int idProvince)
        {
            List<Province> objReturnList = new List<Province>();
            string sqlQuery = "SELECT IdProvince, IdRegion, ProvinceName, IsDeleted FROM " + nameof(Province).ToUpper() + " WHERE IsDeleted = 0 AND IdProvince = " + idProvince + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Province(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                Province objReturn = Province.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_PROVINCES_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Province obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (Region.getObj(obj.IdRegion) != null)
                {
                    if (obj.ProvinceName.Length > 0)
                    {
                        obj.ProvinceName = Generic.Tools.Capital(obj.ProvinceName);
                        string sqlQuery = "INSERT INTO " + nameof(Province).ToUpper() + " VALUES(NULL, '" + obj.IdRegion + "', '" + obj.ProvinceName + "' , 0); SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdProvince = Convert.ToInt32(comando.ExecuteScalar());
                                obj.IsDeleted = false;
                                conexion.Close();
                            }
                        }
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = Province.getObj(obj.IdProvince);
                    }
                    else
                    {
                        response.msg = Generic.Message.NAME_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_PROVINCES_REGIONS_OBJ_NO_EXISTE;
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

        public static object objUpdate(Province obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdProvince > 0)
                {
                    if (Province.getObj(obj.IdProvince) != null)
                    {
                        if (Region.getObj(obj.IdRegion) != null)
                        {
                            if (obj.ProvinceName.Length > 0)
                            {
                                obj.ProvinceName = Generic.Tools.Capital(obj.ProvinceName);
                                string sqlQueryUpdate = "UPDATE " + nameof(Province).ToUpper() + " SET ProvinceName = '" + obj.ProvinceName + "', IdRegion = " + obj.IdRegion + " WHERE IsDeleted = 0 AND IdProvince =  " + obj.IdProvince + ";";
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
                                response.data = Province.getObj(obj.IdProvince);
                            }
                            else
                            {
                                response.msg = Generic.Message.NAME_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.ID_PROVINCES_REGIONS_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_PROVINCES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_PROVINCES_NO_EXISTE;
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

        public static object objDelete(Province obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdProvince > 0)
                {
                    if (Province.getObj(obj.IdProvince) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Province.getObj(obj.IdProvince).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Province).ToUpper() + " SET IsDeleted = 0 WHERE IdProvince =  " + obj.IdProvince + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Province).ToUpper() + " SET IsDeleted = 1 WHERE IdProvince =  " + obj.IdProvince + ";";
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
                        response.data = Province.getObj(obj.IdProvince);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_PROVINCES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_PROVINCES_NO_EXISTE;
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
                List<Province> objReturnList = new List<Province>();
                string sqlQuery = "SELECT IdProvince, IdRegion, ProvinceName, IsDeleted FROM " + nameof(Province).ToUpper() + " WHERE IsDeleted = 0 ORDER BY IdRegion, ProvinceName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Province(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                List<Province> objReturnList = new List<Province>();
                string sqlQuery = "SELECT IdProvince, IdRegion, ProvinceName, IsDeleted FROM " + nameof(Province).ToUpper() + " ORDER BY IdRegion, ProvinceName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Province(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
