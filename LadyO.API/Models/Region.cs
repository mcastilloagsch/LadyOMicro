using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Region
    {
        #region Atributos
        public int IdRegion { get; set; }
        public string RegionName { get; set; }
        public string RegCod { get; set; }
        public int OrderSec { get; set; }
        public bool IsDeleted { get; set; }
        #endregion

        public Region()
        {

        }

        public Region(int idRegion, string regionName, string regCod, int orderSec, bool isDeleted)
        {
            IdRegion = idRegion;
            RegionName = regionName;
            RegCod = regCod;
            OrderSec = orderSec;
            IsDeleted = isDeleted;
        }

        public static Region getObj(int idRegion, bool onlyNotDeleted)
        {
            List<Region> objReturnList = new List<Region>();
            string sqlQuery = "SELECT IdRegion, RegionName, RegCod, OrderSec, IsDeleted FROM " + nameof(Region).ToUpper();
            if (onlyNotDeleted)
            {
                sqlQuery += " WHERE IdRegion = " + idRegion + " AND IsDeleted = 0;";
            }
            else
            {
                sqlQuery += " WHERE IdRegion = " + idRegion + ";";
            }
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Region(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4) == "0" ? false : true));
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
                Region objReturn = Region.getObj(id, true);
                if (objReturn == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_REGIONS_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Region obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.RegionName.Length > 0)
                {
                    if (obj.RegCod.Length > 0)
                    {
                        obj.RegionName = Generic.Tools.Capital(obj.RegionName);
                        string sqlQuery = "INSERT INTO " + nameof(Region).ToUpper() + "(IdRegion, RegionName, RegCod, OrderSec, IsDeleted)";
                        sqlQuery += " VALUES(NULL, '" + Generic.Tools.Capital(obj.RegionName) + "', '" + obj.RegCod + "', " + obj.OrderSec + " , 0);";
                        sqlQuery += " SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdRegion = Convert.ToInt32(comando.ExecuteScalar());
                                obj.IsDeleted = false;
                                conexion.Close();
                            }
                        }
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = Region.getObj(obj.IdRegion, true);
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.REGIONS_NO_COD;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.REGIONS_NO_NAME;
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

        public static object objUpdate(Region obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.IdRegion > 0)
                {
                    if (Region.getObj(obj.IdRegion, true) != null)
                    {
                        if (obj.RegionName.Length > 0)
                        {
                            if (obj.RegCod.Length > 0)
                            {
                                obj.RegionName = Generic.Tools.Capital(obj.RegionName);
                                obj.RegCod = obj.RegCod.ToUpper();
                                string sqlQueryUpdate = "UPDATE " + nameof(Region).ToUpper() + " SET RegionName = '" + obj.RegionName + "', RegCod = '" + obj.RegCod + "', OrderSec = " + obj.OrderSec + " WHERE IsDeleted = 0 AND IdRegion =  " + obj.IdRegion + ";";
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
                                response.data = Region.getObj(obj.IdRegion, true);
                            }
                            else
                            {
                                response.isValid = false;
                                response.msg = Generic.Message.REGIONS_NO_COD;
                                return response;
                            }
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.REGIONS_NO_NAME;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_REGIONS_GETOBJECT_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_REGIONS_NO_EXISTE;
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

        public static object objDelete(Region obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.IdRegion > 0)
                {
                    var objDelete = Region.getObj(obj.IdRegion, false);
                    if (objDelete != null)
                    {
                        string sqlQuery = "UPDATE " + nameof(Region).ToUpper();
                        if (objDelete.IsDeleted)
                        {
                            sqlQuery += " SET IsDeleted = 0";
                        }
                        else
                        {
                            sqlQuery += " SET IsDeleted = 1";
                        }
                        sqlQuery += " WHERE IdRegion = " + obj.IdRegion + ";";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                comando.ExecuteReader();
                                conexion.Close();
                            }
                        }
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = Region.getObj(obj.IdRegion, true);
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_REGIONS_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_REGIONS_NO_EXISTE;
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

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<Region> objReturnList = new List<Region>();
                string sqlQuery = "SELECT IdRegion, RegionName, RegCod, OrderSec, IsDeleted FROM " + nameof(Region).ToUpper() + " WHERE IsDeleted = 0 ORDER BY OrderSec, RegionName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Region(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4) == "0" ? false : true));
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
                List<Region> objReturnList = new List<Region>();
                string sqlQuery = "SELECT IdRegion, RegionName, RegCod, OrderSec, IsDeleted FROM " + nameof(Region).ToUpper() + " ORDER BY 3, 1;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Region(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4) == "0" ? false : true));
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