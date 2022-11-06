using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class PersonPositionStructure
    {
        public int IdPersonPositionStructure { get; set; }
        public int IdPerson { get; set; }
        public int IdPosition { get; set; }
        public int IdStructure { get; set; }
        public bool Principal { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModificationDate { get; set; }
        public int LastModificationPerson { get; set; }

        public PersonPositionStructure()
        {

        }

        public PersonPositionStructure(int idPersonPositionStructure, int idPerson, int idPosition, int idStructure, bool principal, bool isDeleted, string lastModificationDate, int lastModificationPerson)
        {
            IdPersonPositionStructure = idPersonPositionStructure;
            IdPerson = idPerson;
            IdPosition = idPosition;
            IdStructure = idStructure;
            Principal = principal;
            IsDeleted = isDeleted;
            LastModificationDate = lastModificationDate;
            LastModificationPerson = lastModificationPerson;
        }

        public static PersonPositionStructure getObj(int idPersonPositionStructure)
        {
            List<PersonPositionStructure> objReturnList = new List<PersonPositionStructure>();
            string sqlQuery = "SELECT IdPersonPositionStructure, IdPerson, IdPosition, IdStructure, Principal, IsDeleted, DATE_FORMAT(LastModificationDate, '%d/%m/%Y'), LastModificationPerson FROM "
            + nameof(PersonPositionStructure).ToUpper() + " AND IdPersonPositionStructure = " + idPersonPositionStructure + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new PersonPositionStructure(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),
                        reader.GetString(4) == "0" ? false : true, reader.GetString(5) == "0" ? false : true, reader.GetString(6), reader.GetInt32(7)));
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
                PersonPositionStructure objReturn = PersonPositionStructure.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(PersonPositionStructure obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (Person.getObj(obj.IdPerson) != null)
                {
                    if (Position.getObj(obj.IdPosition) != null)
                    {
                        if (Structure.getObj(obj.IdStructure) != null)
                        {
                            if (obj.Principal != null)
                            {
                                if (obj.LastModificationDate.Length > 0)
                                {
                                    if (obj.LastModificationPerson > 0)
                                    {
                                        string sqlQuery = "INSERT INTO " + nameof(PersonPositionStructure).ToUpper() + " VALUES(NULL, '" + obj.IdPerson + "', '" + obj.IdPosition + "', '" + obj.IdStructure + "', '" + obj.Principal + "', 0,  STR_TO_DATE('" + obj.LastModificationDate + "', '%d/%m/%Y'), '" + obj.LastModificationPerson + "'); SELECT LAST_INSERT_ID();";
                                        using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                                        {
                                            using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                                            {
                                                conexion.Open();
                                                obj.IdPersonPositionStructure = Convert.ToInt32(comando.ExecuteScalar());
                                                obj.IsDeleted = false;
                                                conexion.Close();
                                            }
                                        }
                                        response.isValid = true;
                                        response.msg = string.Empty;
                                        response.data = PersonPositionStructure.getObj(obj.IdPersonPositionStructure);
                                    }
                                    else
                                    {
                                        response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_LASTMODIFICATIONPERSON_NO_EXISTE;
                                        return response;
                                    }
                                }
                                else
                                {
                                    response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_LASTMODIFICATIONDATE_NO_EXISTE;
                                    return response;
                                }
                            }
                            else
                            {
                                response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_PRINCIPAL_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_STRUCTURE_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_POSITIONS_OBJ_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_PERSON_OBJ_NO_EXISTE;
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

        public static object objUpdate(PersonPositionStructure obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdPersonPositionStructure > 0)
                {
                    if (PersonPositionStructure.getObj(obj.IdPersonPositionStructure) != null)
                    {
                        if (Person.getObj(obj.IdPerson) != null)
                        {
                            if (Position.getObj(obj.IdPosition) != null)
                            {
                                if (Structure.getObj(obj.IdStructure) != null)
                                {
                                    if (obj.Principal != null)
                                    {
                                        if (obj.LastModificationDate.Length > 0)
                                        {
                                            if (obj.LastModificationPerson > 0)
                                            {
                                                string sqlQueryUpdate = "UPDATE " + nameof(PersonPositionStructure).ToUpper() + " SET IdPerson = '" + obj.IdPerson + "', IdPosition = " + obj.IdPosition + ", IdStructure = " + obj.IdStructure + ", Principal = " + obj.Principal + ", LastModificationDate = STR_TO_DATE('" + obj.LastModificationDate + "', '%d/%m/%Y'), LastModificationPerson = '" + obj.LastModificationPerson + "' WHERE IsDeleted = 0 AND IdPersonPositionStructure =  " + obj.IdPersonPositionStructure + ";";
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
                                                response.data = PersonPositionStructure.getObj(obj.IdPersonPositionStructure);
                                            }
                                            else
                                            {
                                                response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_LASTMODIFICATIONPERSON_NO_EXISTE;
                                                return response;
                                            }
                                        }
                                        else
                                        {
                                            response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_LASTMODIFICATIONDATE_NO_EXISTE;
                                            return response;
                                        }
                                    }
                                    else
                                    {
                                        response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_PRINCIPAL_NO_EXISTE;
                                        return response;
                                    }
                                }
                                else
                                {
                                    response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_STRUCTURE_OBJ_NO_EXISTE;
                                    return response;
                                }
                            }
                            else
                            {
                                response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_POSITIONS_OBJ_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_PERSON_OBJ_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_NO_EXISTE;
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

        public static object objDelete(PersonPositionStructure obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdPersonPositionStructure > 0)
                {
                    if (PersonPositionStructure.getObj(obj.IdPersonPositionStructure) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (PersonPositionStructure.getObj(obj.IdPersonPositionStructure).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(PersonPositionStructure).ToUpper() + " SET IsDeleted = 0 WHERE IdPersonPositionStructure =  " + obj.IdPersonPositionStructure + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(PersonPositionStructure).ToUpper() + " SET IsDeleted = 1 WHERE IdPersonPositionStructure =  " + obj.IdPersonPositionStructure + ";";
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
                        response.data = PersonPositionStructure.getObj(obj.IdPersonPositionStructure);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_PERSONPOSITIONSTRUCTURE_NO_EXISTE;
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
                List<PersonPositionStructure> objReturnList = new List<PersonPositionStructure>();
                string sqlQuery = "SELECT IdPersonPositionStructure, IdPerson, IdPosition, IdStructure, Principal, IsDeleted, DATE_FORMAT(LastModificationDate, '%d/%m/%Y'), LastModificationPerson FROM " + nameof(PersonPositionStructure).ToUpper() + " WHERE IsDeleted = 0 ORDER BY IdPersonPositionStructure;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new PersonPositionStructure(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),
                            reader.GetString(4) == "0" ? false : true, reader.GetString(5) == "0" ? false : true, reader.GetString(6), reader.GetInt32(7)));
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
                List<PersonPositionStructure> objReturnList = new List<PersonPositionStructure>();
                string sqlQuery = "SELECT IdPersonPositionStructure, IdPerson, IdPosition, IdStructure, Principal, IsDeleted, DATE_FORMAT(LastModificationDate, '%d/%m/%Y'), LastModificationPerson FROM " + nameof(PersonPositionStructure).ToUpper() + " ORDER BY IdPersonPositionStructure;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new PersonPositionStructure(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),
                            reader.GetString(4) == "0" ? false : true, reader.GetString(5) == "0" ? false : true, reader.GetString(6), reader.GetInt32(7)));
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
