using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Country
    {
        #region Variables
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
        public bool IsDeleted { get; set; }
        #endregion

        public Country()
        {

        }

        public Country(int idCountry, string countryName, bool isDeleted)
        {
            IdCountry = idCountry;
            CountryName = countryName;
            IsDeleted = isDeleted;
        }

        private static Country getObj(int idCountry, bool onlyNotDeleted)
        {
            List<Country> objReturnList = new List<Country>();
            string sqlQuery = "SELECT IdCountry, CountryName, IsDeleted FROM " + nameof(Country).ToUpper();
            if (onlyNotDeleted)
            {
                sqlQuery += " WHERE IdCountry = " + idCountry + " AND IsDeleted = 0;";
            }
            else
            {
                sqlQuery += " WHERE IdCountry = " + idCountry + ";";
            }
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Country(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                Country objReturn = Country.getObj(id, true);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_COUNTRIES_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Country obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.CountryName.Length > 0)
                {
                    obj.CountryName = Generic.Tools.Capital(obj.CountryName);
                    string sqlQuery = "INSERT INTO " + nameof(Country).ToUpper()+ "(IdCountry, CountryName, IsDeleted)";
                    sqlQuery += " VALUES(NULL, '" + obj.CountryName + "', 0);";
                    sqlQuery += " SELECT LAST_INSERT_ID();";
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            obj.IdCountry = Convert.ToInt32(comando.ExecuteScalar());
                            conexion.Close();
                        }
                    }
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = Country.getObj(obj.IdCountry, true);
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

        public static object objUpdate(Country obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdCountry > 0)
                {
                    if (Country.getObj(obj.IdCountry, true) != null)
                    {
                        if (obj.CountryName.Length > 0)
                        {
                            obj.CountryName = Generic.Tools.Capital(obj.CountryName);
                            string sqlQueryUpdate = "UPDATE " + nameof(Country).ToUpper() + " SET CountryName = '" + obj.CountryName + "' WHERE IsDeleted = 0 AND IdCountry =  " + obj.IdCountry + ";";
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
                            response.data = Country.getObj(obj.IdCountry, true);
                        }
                        else
                        {
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_COUNTRIES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_COUNTRIES_NO_EXISTE;
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

        public static object objDelete(Country obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdCountry > 0)
                {
                    var objGeneric = Country.getObj(obj.IdCountry, false);
                    if (objGeneric != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (objGeneric.IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Country).ToUpper() + " SET IsDeleted = 0 WHERE IdCountry =  " + obj.IdCountry + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Country).ToUpper() + " SET IsDeleted = 1 WHERE IdCountry =  " + obj.IdCountry + ";";
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
                        response.data = Country.getObj(obj.IdCountry, true);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_COUNTRIES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_COUNTRIES_NO_EXISTE;
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
                List<Country> objReturnList = new List<Country>();
                string sqlQuery = "SELECT IdCountry, CountryName, IsDeleted FROM " + nameof(Country).ToUpper() + " WHERE IsDeleted = 0 ORDER BY CountryName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Country(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
                List<Country> objReturnList = new List<Country>();
                string sqlQuery = "SELECT IdCountry, CountryName, IsDeleted FROM " + nameof(Country).ToUpper() + " ORDER BY CountryName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Country(reader.GetInt32(0), reader.GetString(1), reader.GetString(2) == "0" ? false : true));
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
