using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Commune
    {
        #region Atributos
        public int IdCommune { get; set; }
        public int IdProvince { get; set; }
        public string CommuneName { get; set; }
        public string ComCod { get; set; }
        public bool IsDeleted { get; set; }
        #endregion

        public Commune()
        {

        }

        public Commune(int idCommune, int idProvince, string communeName, string comCod, bool isDeleted)
        {
            IdCommune = idCommune;
            IdProvince = idProvince;
            CommuneName = communeName;
            ComCod = comCod;
            IsDeleted = isDeleted;
        }

        public static Commune getObj(int idCommune, bool onlyNotDeleted)
        {
            List<Commune> objReturnList = new List<Commune>();
            string sqlQuery = "SELECT IdCommune, IdProvince, CommuneName, ComCod, IsDeleted FROM " + nameof(Commune).ToUpper();
            if (onlyNotDeleted)
            {
                sqlQuery += " WHERE IdCommune = " + idCommune + " AND IsDeleted = 0;";
            }
            else
            {
                sqlQuery += " WHERE IdCommune = " + idCommune + ";";
            }
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Commune(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4) == "0" ? false : true));
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
                Commune objReturn = Commune.getObj(id, true);
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

        public static object objAdd(Commune obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (Province.getObj(obj.IdProvince) != null)
                {
                    if (obj.CommuneName.Length > 0)
                    {
                        obj.CommuneName = Generic.Tools.Capital(obj.CommuneName);
                        string sqlQuery = "INSERT INTO " + nameof(Commune).ToUpper() + "(IdCommune, IdProvince, CommuneName, ComCod, IsDeleted) ";
                        sqlQuery += " VALUES(NULL, '" + obj.IdProvince + "', '" + obj.CommuneName + "', '" + obj.ComCod + "', 0);";
                        sqlQuery += " SELECT LAST_INSERT_ID();";
                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                        {
                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                            {
                                conexion.Open();
                                obj.IdCommune = Convert.ToInt32(comando.ExecuteScalar());
                                conexion.Close();
                            }
                        }
                        response.isValid = true;
                        response.msg = string.Empty;
                        response.data = Commune.getObj(obj.IdCommune, true);
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
                    if (Commune.getObj(obj.IdCommune, true) != null)
                    {
                        if (Province.getObj(obj.IdProvince) != null)
                        {
                            if (obj.CommuneName.Length > 0)
                            {
                                obj.CommuneName = Generic.Tools.Capital(obj.CommuneName);
                                string sqlQueryUpdate = "UPDATE " + nameof(Commune).ToUpper();
                                sqlQueryUpdate += " SET IdProvince = '" + obj.IdProvince + "', CommuneName = '" + obj.CommuneName + "', ComCod = '" + obj.ComCod + "' ";
                                sqlQueryUpdate += " WHERE IdCommune =  " + obj.IdCommune + ";";
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
                                response.data = Commune.getObj(obj.IdCommune, true);
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
                    var objGeneric = Commune.getObj(obj.IdCommune, false);
                    if (objGeneric != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (objGeneric.IsDeleted)
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
                        response.data = Commune.getObj(obj.IdCommune, true);
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
                string sqlQuery = "SELECT IdCommune, IdProvince, CommuneName, ComCod, IsDeleted FROM " + nameof(Commune).ToUpper() + " WHERE IsDeleted = 0 ORDER BY CommuneName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Commune(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4) == "0" ? false : true));
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
                            objReturnList.Add(new Commune(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4) == "0" ? false : true));
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
