using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Person
    {
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
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public int IdCommune { get; set; }
        public int IdSex { get; set; }
        public int IdGender { get; set; }
        public int IdReligion { get; set; }
        public int PrimaryPhone { get; set; }
        public int? SecondaryPhone { get; set; }
        public int? PartnerCode { get; set; }
        public int IdCountryOrigin { get; set; }
        public int IdCountryNationality { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModificationDate { get; set; }
        public int LastModificationPerson { get; set; }

        public Person()
        {

        }

        public Person(int idPerson, string firstName, string lastName, string secondLastName, string socialName,
        string dni, string digit, int idDniType, string email, string emailSecundary, string birthDate,
        string address, int idCommune, int idSex, int idGender, int idReligion, int primaryPhone,
        int? secondaryPhone, int? partnerCode, int idCountryOrigin, int idCountryNationality, bool isDeleted,
        string lastModificationDate, int lastModificationPerson)
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
            PrimaryPhone = primaryPhone;
            SecondaryPhone = secondaryPhone;
            PartnerCode = partnerCode;
            IdCountryOrigin = idCountryOrigin;
            IdCountryNationality = idCountryNationality;
            IsDeleted = isDeleted;
            LastModificationDate = lastModificationDate;
            LastModificationPerson = lastModificationPerson;
        }

        public static Person getObj(int idPerson)
        {
            List<Person> objReturnList = new List<Person>();
            string sqlQuery = "SELECT IdPerson, FirstName, LastName, SecondLastName, SocialName, Dni, " +
            "Digit, IdDniType, Email, EmailSecundary, DATE_FORMAT(BirthDate, '%d/%m/%Y'), Address, IdCommune, " +
            "IdSex, IdGender, IdReligion, PrimaryPhone, SecondaryPhone, PartnerCode, IdCountryOrigin, IdCountryNationality, IsDeleted, DATE_FORMAT(LASTMODIFICATIONDATE, '%d/%m/%Y'), LastModificationPerson FROM "
            + nameof(Person).ToUpper() + " WHERE IdPerson = " + idPerson + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        string _SecondLastName = null;
                        string _EmailSecundary = null;
                        int? _SecondaryPhone = null;
                        int? _PartnerCode = null;
                        if (!reader.IsDBNull(3))
                        {
                            _SecondLastName = reader.GetString(3);
                        }
                        if (!reader.IsDBNull(9))
                        {
                            _EmailSecundary = reader.GetString(9);
                        }
                        if (!reader.IsDBNull(17))
                        {
                            _SecondaryPhone = reader.GetInt32(17);
                        }
                        if (!reader.IsDBNull(18))
                        {
                            _PartnerCode = reader.GetInt32(18);
                        }
                        objReturnList.Add(new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), _SecondLastName, reader.GetString(4),
                        reader.GetString(5), reader.GetString(6), reader.GetInt32(7), reader.GetString(8), _EmailSecundary, reader.GetString(10), reader.GetString(11),
                        reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15), reader.GetInt32(16), _SecondaryPhone,
                        _PartnerCode, reader.GetInt32(19), reader.GetInt32(20), reader.GetString(21) == "0" ? false : true, reader.GetString(22), reader.GetInt32(23)));
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
                Person objReturn = Person.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_PERSON_GETOBJECT_NO_EXISTE;
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
    }
}
