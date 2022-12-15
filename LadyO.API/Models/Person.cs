using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Person
    {
        #region Variables
        public int IdPerson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string SocialName { get; set; }
        public string Dni { get; set; }
        public string Digit { get; set; }
        public int IdDniType { get; set; }
        public string Email { get; set; }
        public string EmailSecundary { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public int? IdCommune { get; set; }
        public int? IdSex { get; set; }
        public int? IdGender { get; set; }
        public int? IdReligion { get; set; }
        public int? IdAcademicLevel { get; set; }
        public int? PrimaryPhone { get; set; }
        public int? SecondaryPhone { get; set; }
        public int? PartnerCode { get; set; }
        public int? IdCountryOrigin { get; set; }
        public int? IdCountryNationality { get; set; }
        public bool IsDeleted { get; set; }
        public string UserPassword { get; set; }
        public string UserToken { get; set; }
        public DateTime LastModificationDate { get; set; }
        public int LastModificationPerson { get; set; }
        #endregion

        public Person()
        {

        }

        public Person(int idPerson, string firstName, string lastName, string secondLastName, string socialName, string dni, string digit, int idDniType, string email, string emailSecundary, DateTime? birthDate, string address, int? idCommune, int? idSex, int? idGender, int? idReligion, int? idAcademicLevel, int? primaryPhone, int? secondaryPhone, int? partnerCode, int? idCountryOrigin, int? idCountryNationality, bool isDeleted, string userPassword, string userToken, DateTime lastModificationDate, int lastModificationPerson)
        {
            IdPerson = idPerson;
            FirstName = firstName;
            LastName = lastName;
            SecondLastName = secondLastName;
            SocialName = socialName;
            Dni = dni;
            Digit = digit;
            IdDniType = idDniType;
            Email = email;
            EmailSecundary = emailSecundary;
            BirthDate = birthDate;
            Address = address;
            IdCommune = idCommune;
            IdSex = idSex;
            IdGender = idGender;
            IdReligion = idReligion;
            IdAcademicLevel = idAcademicLevel;
            PrimaryPhone = primaryPhone;
            SecondaryPhone = secondaryPhone;
            PartnerCode = partnerCode;
            IdCountryOrigin = idCountryOrigin;
            IdCountryNationality = idCountryNationality;
            IsDeleted = isDeleted;
            UserPassword = userPassword;
            UserToken = userToken;
            LastModificationDate = lastModificationDate;
            LastModificationPerson = lastModificationPerson;
        }

        public static Person getObj(int idPerson, bool onlyNotDeleted)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                List<Person> objReturnList = new List<Person>();
                Person objAdd = new Person();
                string sqlQuery = "SELECT IdPerson, FirstName, LastName, SecondLastName, SocialName, Dni, Digit, IdDniType, Email, EmailSecundary, DATE_FORMAT(BirthDate, '%Y-%m-%d') AS BirthDate, Address, IdCommune, IdSex, IdGender, IdReligion, IdAcademicLevel, PrimaryPhone, SecondaryPhone, PartnerCode, IdCountryOrigin, IdCountryNationality, IsDeleted, DATE_FORMAT(LastModificationDate, '%Y-%m-%d %H:%i:%S') AS LastModificationDate, LastModificationPerson FROM " + nameof(Person).ToUpper();
                if (onlyNotDeleted)
                {
                    sqlQuery += " WHERE IdPerson = " + idPerson + " AND IsDeleted = 0;";
                }
                else
                {
                    sqlQuery += " WHERE IdPerson = " + idPerson + ";";
                }
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objAdd.IdPerson = reader.GetInt32(0);
                            objAdd.FirstName = reader.GetString(1);
                            objAdd.LastName = reader.GetString(2);
                            objAdd.SecondLastName = null;
                            if (!reader.IsDBNull(3))
                                objAdd.SecondLastName = reader.GetString(3);
                            objAdd.SocialName = null;
                            if (!reader.IsDBNull(4))
                                objAdd.SocialName = reader.GetString(4);
                            objAdd.Dni = reader.GetString(5);
                            objAdd.Digit = reader.GetString(6);
                            objAdd.IdDniType = reader.GetInt32(7);
                            objAdd.Email = reader.GetString(8).ToLower();
                            objAdd.EmailSecundary = null;
                            if (!reader.IsDBNull(9))
                                objAdd.EmailSecundary = reader.GetString(9);
                            objAdd.BirthDate = null;
                            if (!reader.IsDBNull(10))
                                objAdd.BirthDate = reader.GetDateTime(10);
                            objAdd.Address = null;
                            if (!reader.IsDBNull(11))
                                objAdd.Address = reader.GetString(11);
                            objAdd.IdCommune = null;
                            if (!reader.IsDBNull(12))
                                objAdd.IdCommune = reader.GetInt32(12);
                            objAdd.IdSex = null;
                            if (!reader.IsDBNull(13))
                                objAdd.IdSex = reader.GetInt32(13);
                            objAdd.IdGender = null;
                            if (!reader.IsDBNull(14))
                                objAdd.IdGender = reader.GetInt32(14);
                            objAdd.IdReligion = null;
                            if (!reader.IsDBNull(15))
                                objAdd.IdReligion = reader.GetInt32(15);
                            if (!reader.IsDBNull(16))
                                objAdd.IdAcademicLevel = reader.GetInt32(16);
                            objAdd.PrimaryPhone = null;
                            if (!reader.IsDBNull(17))
                                objAdd.PrimaryPhone = reader.GetInt32(17);
                            objAdd.SecondaryPhone = null;
                            if (!reader.IsDBNull(18))
                                objAdd.SecondaryPhone = reader.GetInt32(18);
                            if (!reader.IsDBNull(19))
                                objAdd.PartnerCode = reader.GetInt32(19);
                            if (!reader.IsDBNull(20))
                                objAdd.IdCountryOrigin = reader.GetInt32(20);
                            if (!reader.IsDBNull(21))
                                objAdd.IdCountryNationality = reader.GetInt32(21);
                            objAdd.IsDeleted = reader.GetString(22) == "0" ? false : true;
                            objAdd.UserPassword = null;
                            objAdd.UserToken = null;
                            objAdd.LastModificationDate = reader.GetDateTime(23);
                            objAdd.LastModificationPerson = reader.GetInt32(24);
                            objReturnList.Add(objAdd);
                        }
                        conexion.Close();
                    }
                }
                return objReturnList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Person getObjByMail(string eMail)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                List<Person> objReturnList = new List<Person>();
                Person objAdd = new Person();
                eMail = eMail.ToLower();
                string sqlQuery = "SELECT IdPerson, FirstName, LastName, SecondLastName, SocialName, Dni, Digit, IdDniType, Email, EmailSecundary, DATE_FORMAT(BirthDate, '%Y-%m-%d') AS BirthDate, Address, IdCommune, IdSex, IdGender, IdReligion, IdAcademicLevel, PrimaryPhone, SecondaryPhone, PartnerCode, IdCountryOrigin, IdCountryNationality, IsDeleted, DATE_FORMAT(LastModificationDate, '%Y-%m-%d %H:%i:%S') AS LastModificationDate, LastModificationPerson FROM " + nameof(Person).ToUpper();
                sqlQuery += " WHERE Email = LOWER('" + eMail + "');";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objAdd.IdPerson = reader.GetInt32(0);
                            objAdd.FirstName = reader.GetString(1);
                            objAdd.LastName = reader.GetString(2);
                            objAdd.SecondLastName = null;
                            if (!reader.IsDBNull(3))
                                objAdd.SecondLastName = reader.GetString(3);
                            objAdd.SocialName = null;
                            if (!reader.IsDBNull(4))
                                objAdd.SocialName = reader.GetString(4);
                            objAdd.Dni = reader.GetString(5);
                            objAdd.Digit = reader.GetString(6);
                            objAdd.IdDniType = reader.GetInt32(7);
                            objAdd.Email = reader.GetString(8).ToLower();
                            objAdd.EmailSecundary = null;
                            if (!reader.IsDBNull(9))
                                objAdd.EmailSecundary = reader.GetString(9);
                            objAdd.BirthDate = null;
                            if (!reader.IsDBNull(10))
                                objAdd.BirthDate = reader.GetDateTime(10);
                            objAdd.Address = null;
                            if (!reader.IsDBNull(11))
                                objAdd.Address = reader.GetString(11);
                            objAdd.IdCommune = null;
                            if (!reader.IsDBNull(12))
                                objAdd.IdCommune = reader.GetInt32(12);
                            objAdd.IdSex = null;
                            if (!reader.IsDBNull(13))
                                objAdd.IdSex = reader.GetInt32(13);
                            objAdd.IdGender = null;
                            if (!reader.IsDBNull(14))
                                objAdd.IdGender = reader.GetInt32(14);
                            objAdd.IdReligion = null;
                            if (!reader.IsDBNull(15))
                                objAdd.IdReligion = reader.GetInt32(15);
                            if (!reader.IsDBNull(16))
                                objAdd.IdAcademicLevel = reader.GetInt32(16);
                            objAdd.PrimaryPhone = null;
                            if (!reader.IsDBNull(17))
                                objAdd.PrimaryPhone = reader.GetInt32(17);
                            objAdd.SecondaryPhone = null;
                            if (!reader.IsDBNull(18))
                                objAdd.SecondaryPhone = reader.GetInt32(18);
                            if (!reader.IsDBNull(19))
                                objAdd.PartnerCode = reader.GetInt32(19);
                            if (!reader.IsDBNull(20))
                                objAdd.IdCountryOrigin = reader.GetInt32(20);
                            if (!reader.IsDBNull(21))
                                objAdd.IdCountryNationality = reader.GetInt32(21);
                            objAdd.IsDeleted = reader.GetString(22) == "0" ? false : true;
                            objAdd.UserPassword = null;
                            objAdd.UserToken = null;
                            objAdd.LastModificationDate = reader.GetDateTime(23);
                            objAdd.LastModificationPerson = reader.GetInt32(24);
                            objReturnList.Add(objAdd);
                        }
                        conexion.Close();
                    }
                }
                return objReturnList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object getObject(int idPerson)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.isValid = false;
            response.data = null;
            try
            {
                Branch objReturn = Branch.getObj(idPerson);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_BRANCHES_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Person obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                #region Validaciones
                if (string.IsNullOrEmpty(obj.FirstName))
                {
                    response.isValid = false;
                    response.msg = Generic.Message.PERSON_NO_FIRST_NAME;
                    response.data = null;
                    return response;
                }
                else
                {
                    obj.FirstName = Generic.Tools.Capital(obj.FirstName);
                }
                if (string.IsNullOrEmpty(obj.LastName))
                {
                    response.isValid = false;
                    response.msg = Generic.Message.PERSON_NO_LAST_NAME;
                    response.data = null;
                    return response;
                }
                else
                {
                    obj.LastName = Generic.Tools.Capital(obj.LastName);
                }
                if (!string.IsNullOrEmpty(obj.SecondLastName))
                    obj.SecondLastName = Generic.Tools.Capital(obj.SecondLastName);
                if (!string.IsNullOrEmpty(obj.SocialName))
                    obj.SecondLastName = Generic.Tools.Capital(obj.SocialName);
                if (string.IsNullOrEmpty(obj.Dni))
                {
                    response.isValid = false;
                    response.msg = Generic.Message.PERSON_NO_DNI;
                    response.data = null;
                    return response;
                }
                else
                {
                    obj.Dni = obj.Dni.ToUpper();
                    if (string.IsNullOrEmpty(obj.Digit))
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.PERSON_NO_DNI_DIGIT;
                        response.data = null;
                        return response;
                    }
                    else
                    {
                        obj.Digit = obj.Digit.ToUpper();
                        if (DniType.getObj(obj.IdDniType) == null)
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.PERSON_NO_DNI_TYPE;
                            response.data = null;
                            return response;
                        }
                        else
                        {
                            if (!Generic.Tools.ValidarDNI(obj.IdDniType, obj.Dni, obj.Digit))
                            {
                                response.isValid = false;
                                response.msg = Generic.Message.PERSON_DNI_INVALID;
                                response.data = null;
                                return response;
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(obj.Email))
                {
                    response.isValid = false;
                    response.msg = Generic.Message.PERSON_NO_EMAIL;
                    response.data = null;
                    return response;
                }
                else
                {
                    if (!Generic.Tools.ValidarEmail(obj.Email))
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.PERSON_EMAIL_PRIMARY_ERROR;
                        response.data = null;
                        return response;
                    }
                    else
                    {
                        if (Person.getObjByMail(obj.Email) != null)
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.PERSON_EMAIL_PRIMARY_EXIST;
                            response.data = null;
                            return response;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(obj.EmailSecundary))
                {
                    if (!Generic.Tools.ValidarEmail(obj.EmailSecundary))
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.PERSON_EMAIL_SECUNDARY_ERROR;
                        response.data = null;
                        return response;
                    }
                    else
                    {
                        if (Person.getObjByMail(obj.EmailSecundary) != null)
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.PERSON_EMAIL_PRIMARY_EXIST;
                            response.data = null;
                            return response;
                        }
                    }
                }
                #endregion
                string sqlQuery = "INSERT INTO " + nameof(Person).ToUpper() + "(IdPerson, FirstName, LastName, SecondLastName, SocialName, Dni, Digit, IdDniType, Email, EmailSecundary, BirthDate, Address, IdCommune, IdSex, IdGender, IdReligion, IdAcademicLevel, PrimaryPhone, SecondaryPhone, PartnerCode, IdCountryOrigin, IdCountryNationality, IsDeleted, UserPassword, UserToken, LastModificationDate, LastModificationPerson)";
                sqlQuery += " VALUES(NULL, '" + obj.FirstName + "', '" + obj.LastName + "', " + (string.IsNullOrEmpty(obj.SecondLastName) ? "NULL" : ("'" + obj.SecondLastName + "'")) + ", " + (string.IsNullOrEmpty(obj.SocialName) ? "NULL" : ("'" + obj.SocialName + "'")) + ", '" + obj.Dni + "', '" + obj.Digit + "', " + obj.IdDniType + ", " + (string.IsNullOrEmpty(obj.Email) ? "NULL" : ("'" + obj.Email + "'")) + ", " + (string.IsNullOrEmpty(obj.EmailSecundary) ? "NULL" : ("'" + obj.EmailSecundary + "'")) + ", " + (!obj.BirthDate.HasValue ? "NULL" : ("'" + obj.BirthDate.Value.ToString("yyyy-MM-dd") + "'")) + ", " + (string.IsNullOrEmpty(obj.Address) ? "NULL" : ("'" + obj.Address + "'")) + ", " + (string.IsNullOrEmpty(obj.IdCommune.ToString()) ? "NULL" : obj.IdCommune.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdSex.ToString()) ? "NULL" : obj.IdSex.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdGender.ToString()) ? "NULL" : obj.IdGender.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdReligion.ToString()) ? "NULL" : obj.IdReligion.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdAcademicLevel.ToString()) ? "NULL" : obj.IdAcademicLevel.ToString()) + ", " + (string.IsNullOrEmpty(obj.PrimaryPhone.ToString()) ? "NULL" : obj.PrimaryPhone.ToString()) + ", " + (string.IsNullOrEmpty(obj.SecondaryPhone.ToString()) ? "NULL" : obj.SecondaryPhone.ToString()) + ", " + (string.IsNullOrEmpty(obj.PartnerCode.ToString()) ? "NULL" : obj.PartnerCode.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdCountryOrigin.ToString()) ? "NULL" : obj.IdCountryOrigin.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdCountryNationality.ToString()) ? "NULL" : obj.IdCountryNationality.ToString()) + ", 0, NULL, NULL, NOW(), " + obj.LastModificationPerson + ");";
                sqlQuery += " SELECT LAST_INSERT_ID();";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        obj.IdPerson = Convert.ToInt32(comando.ExecuteScalar());
                        conexion.Close();
                    }
                }
                response.isValid = true;
                response.msg = string.Empty;
                response.data = Person.getObj(obj.IdPerson, true);
                return response;
            }
            catch (Exception ex)
            {
                response.msg = ex.Message;
                return response;
            }
        }

        public static object objAddAdmin(Person obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            bool isError = false;
            try
            {
                if (string.IsNullOrEmpty(obj.FirstName))
                {
                    isError = true;
                    response.msg = Generic.Message.PERSON_NO_FIRST_NAME;
                }
                else
                {
                    obj.FirstName = Generic.Tools.Capital(obj.FirstName);
                    if (string.IsNullOrEmpty(obj.LastName))
                    {
                        isError = true;
                        response.msg = Generic.Message.PERSON_NO_LAST_NAME;
                    }
                    else
                    {
                        obj.LastName = Generic.Tools.Capital(obj.LastName);
                        if (!string.IsNullOrEmpty(obj.SecondLastName))
                        {
                            obj.SecondLastName = Generic.Tools.Capital(obj.SecondLastName);
                        }
                        if (!string.IsNullOrEmpty(obj.SocialName))
                        {
                            obj.SocialName = Generic.Tools.Capital(obj.SocialName);
                        }
                        if (string.IsNullOrEmpty(obj.Dni))
                        {
                            isError = true;
                            response.msg = Generic.Message.PERSON_NO_DNI;
                        }
                        else
                        {
                            obj.Dni = obj.Dni.ToUpper();
                            if (string.IsNullOrEmpty(obj.Digit))
                            {
                                isError = true;
                                response.msg = Generic.Message.PERSON_NO_DNI_DIGIT;
                            }
                            else
                            {
                                if (!Generic.Tools.ValidarDNI(obj.IdDniType, obj.Dni, obj.Digit))
                                {
                                    isError = true;
                                    response.msg = Generic.Message.PERSON_DNI_INVALID;
                                }
                                else
                                {
                                    obj.Digit = obj.Digit.ToUpper();
                                    if (obj.IdDniType <= 0)
                                    {
                                        isError = true;
                                        response.msg = Generic.Message.PERSON_NO_DNI_TYPE;
                                    }
                                    else if (string.IsNullOrEmpty(obj.Email))
                                    {
                                        isError = true;
                                        response.msg = Generic.Message.PERSON_NO_EMAIL;
                                    }
                                    else
                                    {
                                        if (!Generic.Tools.ValidarEmail(obj.Email.ToLower()))
                                        {
                                            isError = true;
                                            response.msg = Generic.Message.PERSON_EMAIL_ERROR;
                                        }
                                        else
                                        {
                                            obj.Email = obj.Email.ToLower();
                                            if (getObjByMail(obj.Email) != null)
                                            {
                                                isError = true;
                                                response.msg = Generic.Message.PERSON_EMAIL_EXIST;
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(obj.EmailSecundary))
                                                {
                                                    if (!Generic.Tools.ValidarEmail(obj.EmailSecundary.ToLower()))
                                                    {
                                                        isError = true;
                                                        response.msg = Generic.Message.PERSON_EMAIL_SECUNDARY_ERROR;
                                                    }
                                                    else
                                                    {
                                                        obj.EmailSecundary = obj.EmailSecundary.ToLower();
                                                        if (!string.IsNullOrEmpty(obj.Address))
                                                        {
                                                            obj.Address = Generic.Tools.Capital(obj.Address);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (!isError)
                {
                    string sqlQuery = "INSERT INTO " + nameof(Person).ToUpper() + "(IdPerson, FirstName, LastName, SecondLastName, SocialName, Dni, Digit, IdDniType, Email, EmailSecundary, BirthDate, Address, IdCommune, IdSex, IdGender, IdReligion, IdAcademicLevel, PrimaryPhone, SecondaryPhone, PartnerCode, IdCountryOrigin, IdCountryNationality, IsDeleted, UserPassword, UserToken, LastModificationDate, LastModificationPerson)";
                    sqlQuery += " VALUES(NULL, '" + obj.FirstName + "', '" + obj.LastName + "', " + (string.IsNullOrEmpty(obj.SecondLastName) ? "NULL" : ("'" + obj.SecondLastName + "'")) + ", " + (string.IsNullOrEmpty(obj.SocialName) ? "NULL" : ("'" + obj.SocialName + "'")) + ", '" + obj.Dni + "', '" + obj.Digit + "', " + obj.IdDniType + ", " + (string.IsNullOrEmpty(obj.Email) ? "NULL" : ("'" + obj.Email + "'")) + ", " + (string.IsNullOrEmpty(obj.EmailSecundary) ? "NULL" : ("'" + obj.EmailSecundary + "'")) + ", " + (!obj.BirthDate.HasValue ? "NULL" : ("'" + obj.BirthDate.Value.ToString("yyyy-MM-dd") + "'")) + ", " + (string.IsNullOrEmpty(obj.Address) ? "NULL" : ("'" + obj.Address + "'")) + ", " + (string.IsNullOrEmpty(obj.IdCommune.ToString()) ? "NULL" : obj.IdCommune.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdSex.ToString()) ? "NULL" : obj.IdSex.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdGender.ToString()) ? "NULL" : obj.IdGender.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdReligion.ToString()) ? "NULL" : obj.IdReligion.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdAcademicLevel.ToString()) ? "NULL" : obj.IdAcademicLevel.ToString()) + ", " + (string.IsNullOrEmpty(obj.PrimaryPhone.ToString()) ? "NULL" : obj.PrimaryPhone.ToString()) + ", " + (string.IsNullOrEmpty(obj.SecondaryPhone.ToString()) ? "NULL" : obj.SecondaryPhone.ToString()) + ", " + (string.IsNullOrEmpty(obj.PartnerCode.ToString()) ? "NULL" : obj.PartnerCode.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdCountryOrigin.ToString()) ? "NULL" : obj.IdCountryOrigin.ToString()) + ", " + (string.IsNullOrEmpty(obj.IdCountryNationality.ToString()) ? "NULL" : obj.IdCountryNationality.ToString()) + ", 0, NULL, NULL, NOW(), " + obj.LastModificationPerson + ");";
                    sqlQuery += " SELECT LAST_INSERT_ID();";
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            obj.IdPerson = Convert.ToInt32(comando.ExecuteScalar());
                            conexion.Close();
                        }
                    }
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = Person.getObj(obj.IdPerson, true);
                    return response;
                }
                else
                {
                    response.isValid = false;
                    response.data = null;
                    return response;
                }
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
                List<Person> objReturnList = new List<Person>();
                Person objAdd = new Person();
                string sqlQuery = "SELECT IdPerson, FirstName, LastName, SecondLastName, SocialName, Dni, Digit, IdDniType, Email, EmailSecundary, DATE_FORMAT(BirthDate, '%Y-%m-%d') AS BirthDate, Address, IdCommune, IdSex, IdGender, IdReligion, IdAcademicLevel, PrimaryPhone, SecondaryPhone, PartnerCode, IdCountryOrigin, IdCountryNationality, IsDeleted, DATE_FORMAT(LastModificationDate, '%Y-%m-%d %H:%i:%S') AS LastModificationDate, LastModificationPerson FROM " + nameof(Person).ToUpper();
                sqlQuery += " ORDER BY LastName, SecondLastName, FirstName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objAdd.IdPerson = reader.GetInt32(0);
                            objAdd.FirstName = reader.GetString(1);
                            objAdd.LastName = reader.GetString(2);
                            objAdd.SecondLastName = null;
                            if (!reader.IsDBNull(3))
                                objAdd.SecondLastName = reader.GetString(3);
                            objAdd.SocialName = null;
                            if (!reader.IsDBNull(4))
                                objAdd.SocialName = reader.GetString(4);
                            objAdd.Dni = reader.GetString(5);
                            objAdd.Digit = reader.GetString(6);
                            objAdd.IdDniType = reader.GetInt32(7);
                            objAdd.Email = reader.GetString(8).ToLower();
                            objAdd.EmailSecundary = null;
                            if (!reader.IsDBNull(9))
                                objAdd.EmailSecundary = reader.GetString(9);
                            objAdd.BirthDate = null;
                            if (!reader.IsDBNull(10))
                                objAdd.BirthDate = reader.GetDateTime(10);
                            objAdd.Address = null;
                            if (!reader.IsDBNull(11))
                                objAdd.Address = reader.GetString(11);
                            objAdd.IdCommune = null;
                            if (!reader.IsDBNull(12))
                                objAdd.IdCommune = reader.GetInt32(12);
                            objAdd.IdSex = null;
                            if (!reader.IsDBNull(13))
                                objAdd.IdSex = reader.GetInt32(13);
                            objAdd.IdGender = null;
                            if (!reader.IsDBNull(14))
                                objAdd.IdGender = reader.GetInt32(14);
                            objAdd.IdReligion = null;
                            if (!reader.IsDBNull(15))
                                objAdd.IdReligion = reader.GetInt32(15);
                            if (!reader.IsDBNull(16))
                                objAdd.IdAcademicLevel = reader.GetInt32(16);
                            objAdd.PrimaryPhone = null;
                            if (!reader.IsDBNull(17))
                                objAdd.PrimaryPhone = reader.GetInt32(17);
                            objAdd.SecondaryPhone = null;
                            if (!reader.IsDBNull(18))
                                objAdd.SecondaryPhone = reader.GetInt32(18);
                            if (!reader.IsDBNull(19))
                                objAdd.PartnerCode = reader.GetInt32(19);
                            if (!reader.IsDBNull(20))
                                objAdd.IdCountryOrigin = reader.GetInt32(20);
                            if (!reader.IsDBNull(21))
                                objAdd.IdCountryNationality = reader.GetInt32(21);
                            objAdd.IsDeleted = reader.GetString(22) == "0" ? false : true;
                            objAdd.UserPassword = null;
                            objAdd.UserToken = null;
                            objAdd.LastModificationDate = reader.GetDateTime(23);
                            objAdd.LastModificationPerson = reader.GetInt32(24);
                            objReturnList.Add(objAdd);
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
