using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Commune
    {
        public int IdCommune { get; set; }
        public int IdProvince { get; set; }
        public string CommuneName { get; set; }
        public bool IsDeleted { get; set; }

        public Commune()
        {

        }

        public Commune(int idCommune, int idProvince, string communeName, bool isDeleted)
        {
            IdCommune = idCommune;
            IdProvince = idProvince;
            CommuneName = communeName;
            IsDeleted = isDeleted;
        }

        private static Commune getObj(int idCommune)
        {
            List<Commune> objReturnList = new List<Commune>();
            string sqlQuery = "SELECT IdCommune, IdProvince, CommuneName, IsDeleted FROM " + nameof(Commune).ToUpper() + " WHERE IdCommune = " + idCommune + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Commune(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                Commune objReturn = Commune.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_COMMUNES_GETOBJECT_NO_EXISTE;
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

        private static Province getProvince(int idProvince)
        {
            List<Province> objReturnList = new List<Province>();
            string sqlQuery = "SELECT IdProvince, IdRegion, ProvinceName, IsDeleted FROM " + nameof(Province).ToUpper() + " WHERE IdProvince = " + idProvince + ";";
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

        public static object objAdd(Commune obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (Commune.getProvince(obj.IdProvince) != null)
                {
                    if (obj.CommuneName.Length > 0)
                    {
                        obj.CommuneName = Generic.Tools.Capital(obj.CommuneName);
                        string sqlQuery = "INSERT INTO " + nameof(Commune).ToUpper() + " VALUES(NULL, '" + obj.IdProvince + "', '" + obj.CommuneName + "' , 0); SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdCommune = Convert.ToInt32(comando.ExecuteScalar());
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
                }
                else
                {
                    response.msg = Generic.Message.ID_COMMUNES_PROVINCES_OBJ_NO_EXISTE;
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

        public static object objUpdate(Commune obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdCommune > 0)
                {
                    if (Commune.getObj(obj.IdCommune) != null)
                    {
                        if (Commune.getProvince(obj.IdProvince) != null)
                        {
                            if (obj.CommuneName.Length > 0)
                            {
                                obj.CommuneName = Generic.Tools.Capital(obj.CommuneName);
                                string sqlQueryUpdate = "UPDATE " + nameof(Commune).ToUpper() + " SET CommuneName = '" + obj.CommuneName + "', IdProvince = " + obj.IdProvince + " WHERE IdCommune =  " + obj.IdCommune + ";";
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
                            response.msg = Generic.Message.ID_COMMUNES_PROVINCES_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_COMMUNES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_COMMUNES_NO_EXISTE;
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

        public static object objDelete(Commune obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdCommune > 0)
                {
                    if (Commune.getObj(obj.IdCommune) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Commune.getObj(obj.IdCommune).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Commune).ToUpper() + " SET IsDeleted = 0 WHERE IdCommune =  " + obj.IdCommune + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Commune).ToUpper() + " SET IsDeleted = 1 WHERE IdCommune =  " + obj.IdCommune + ";";
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
                        response.data = Commune.getObj(obj.IdCommune);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_COMMUNES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_COMMUNES_NO_EXISTE;
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
                List<Commune> objReturnList = new List<Commune>();
                string sqlQuery = "SELECT IdCommune, IdProvince, CommuneName, IsDeleted FROM " + nameof(Commune).ToUpper() + " WHERE IsDeleted = 0 ORDER BY CommuneName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Commune(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
                List<Commune> objReturnList = new List<Commune>();
                string sqlQuery = "SELECT IdCommune, IdProvince, CommuneName, IsDeleted FROM " + nameof(Commune).ToUpper() + " ORDER BY CommuneName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Commune(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) == "0" ? false : true));
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
