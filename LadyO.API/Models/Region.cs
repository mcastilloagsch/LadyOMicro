using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Region
    {
        public int IdRegion { get; set; }
        public string RegionName { get; set; }
        public int OrderSec { get; set; }
        public bool IsDeleted { get; set; }

        public Region()
        {

        }

        public Region(int idRegion, string regionName, int orderSec, bool isDeleted)
        {
            IdRegion = idRegion;
            RegionName = regionName;
            OrderSec = orderSec;
            IsDeleted = isDeleted;
        }

        private static Region getObj(int idRegion)
        {
            List<Region> objReturnList = new List<Region>();
            string sqlQuery = "SELECT IdRegion, RegionName, OrderSec, IsDeleted FROM " + nameof(Region).ToUpper() + " WHERE IdRegion = " + idRegion + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Region(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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
                Region objReturn = Region.getObj(id);
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
                    obj.RegionName = Generic.Tools.Capital(obj.RegionName);
                    string sqlQuery = "INSERT INTO " + nameof(Region).ToUpper() + " VALUES(NULL, '" + Generic.Tools.Capital(obj.RegionName) + "', " + obj.OrderSec + " , 0); SELECT LAST_INSERT_ID();";
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
                    response.data = obj;
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
                    if (Region.getObj(obj.IdRegion) != null)
                    {
                        if (obj.RegionName.Length > 0)
                        {
                            obj.RegionName = Generic.Tools.Capital(obj.RegionName);
                            string sqlQueryUpdate = "UPDATE " + nameof(Region).ToUpper() + " SET RegionName = '" + obj.RegionName + "', ORDERSEC = " + obj.OrderSec + " WHERE IdRegion =  " + obj.IdRegion + ";";
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
                            response.isValid = false;
                            response.msg = Generic.Message.REGIONS_NO_NAME;
                            return response;
                        }
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

        public static object objDelete(Region obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (obj.IdRegion > 0)
                {
                    if (Region.getObj(obj.IdRegion) != null)
                    {
                        if (obj.RegionName.Length > 0)
                        {
                            string sqlQueryUpdate = string.Empty;
                            obj.RegionName = Generic.Tools.Capital(obj.RegionName);
                            if (Region.getObj(obj.IdRegion).IsDeleted)
                            {
                                sqlQueryUpdate = "UPDATE " + nameof(Region).ToUpper() + " SET IsDeleted = 0 WHERE IdRegion =  " + obj.IdRegion + ";";
                            }
                            else
                            {
                                sqlQueryUpdate = "UPDATE " + nameof(Region).ToUpper() + " SET IsDeleted = 1 WHERE IdRegion =  " + obj.IdRegion + ";";
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
                            response.data = Region.getObj(obj.IdRegion);
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
                string sqlQuery = "SELECT IdRegion, RegionName, OrderSec, IsDeleted FROM " + nameof(Region).ToUpper() + " WHERE IsDeleted = 0 ORDER BY 4, 1;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Region(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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
                string sqlQuery = "SELECT IdRegion, RegionName, OrderSec, IsDeleted FROM " + nameof(Region).ToUpper() + " ORDER BY 4, 1;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Region(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3) == "0" ? false : true));
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