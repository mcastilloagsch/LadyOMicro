using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Structure
    {
        public int IdStructure { get; set; }
        public int? IdStructureParent { get; set; }
        public string Address { get; set; }
        public int? IdCommune { get; set; }
        public int IdStructureType { get; set; }
        public int IdSocioEconomic { get; set; }
        public string StructureName { get; set; }
        public int? IdBranch { get; set; }
        public string SponsorName { get; set; }
        public string SponsorAddress { get; set; }
        public string SponsorDni { get; set; }
        public string SponsorEmail { get; set; }
        public int? SponsorPhone { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModificationDate { get; set; }
        public int LastModificationPerson { get; set; }

        public Structure()
        {

        }

        public Structure(int idStructure, int? idStructureParent, string address, int? idCommune, int idStructureType,
        int idSocioEconomic, string structureName, int? idBranch, string sponsorName, string sponsorAddress, string sponsorDni,
        string sponsorEmail, int? sponsorPhone, bool isDeleted, string lastModificationDate, int lastModificationPerson)
        {
            IdStructure = idStructure;
            IdStructureParent = idStructureParent;
            Address = address;
            IdCommune = idCommune;
            IdStructureType = idStructureType;
            IdSocioEconomic = idSocioEconomic;
            StructureName = structureName;
            IdBranch = idBranch;
            SponsorName = sponsorName;
            SponsorAddress = sponsorAddress;
            SponsorDni = sponsorDni;
            SponsorEmail = sponsorEmail;
            SponsorPhone = sponsorPhone;
            IsDeleted = isDeleted;
            LastModificationDate = lastModificationDate;
            LastModificationPerson = lastModificationPerson;
        }

        private static Structure getObj(int idStructure)
        {
            List<Structure> objReturnList = new List<Structure>();
            string sqlQuery = "SELECT IdStructure, IdStructureParent, Address, IdCommune, IdStructureType, IdSocioEconomic, " +
            "StructureName, IdBranch, SponsorName, SponsorAddress, SponsorDni, SponsorEmail, SponsorPhone, IsDeleted, DATE_FORMAT(LASTMODIFICATIONDATE, '%d/%m/%Y'), LastModificationPerson FROM "
            + nameof(Structure).ToUpper() + " WHERE IdStructure = " + idStructure + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        int? _IdStructureParent = null;
                        string _Address = null;
                        int? _IdCommune = null;
                        string _StructureName = null;
                        int? _IdBranch = null;
                        string _SponsorName = null;
                        string _SponsorAddress = null;
                        string _SponsorDni = null;
                        string _SponsorEmail = null;
                        int? _SponsorPhone = null;
                        if (!reader.IsDBNull(1))
                        {
                            _IdStructureParent = reader.GetInt32(1);
                        }
                        if (!reader.IsDBNull(2))
                        {
                            _Address = reader.GetString(2);
                        }
                        if (!reader.IsDBNull(3))
                        {
                            _IdCommune = reader.GetInt32(3);
                        }
                        if (!reader.IsDBNull(6))
                        {
                            _StructureName = reader.GetString(6);
                        }
                        if (!reader.IsDBNull(7))
                        {
                            _IdBranch = reader.GetInt32(7);
                        }
                        if (!reader.IsDBNull(8))
                        {
                            _SponsorName = reader.GetString(8);
                        }
                        if (!reader.IsDBNull(9))
                        {
                            _SponsorAddress = reader.GetString(9);
                        }
                        if (!reader.IsDBNull(10))
                        {
                            _SponsorDni = reader.GetString(10);
                        }
                        if (!reader.IsDBNull(11))
                        {
                            _SponsorEmail = reader.GetString(11);
                        }
                        if (!reader.IsDBNull(12))
                        {
                            _SponsorPhone = reader.GetInt32(12);
                        }
                        objReturnList.Add(new Structure(reader.GetInt32(0), _IdStructureParent, _Address, _IdCommune, reader.GetInt32(4),
                        reader.GetInt32(5), _StructureName, _IdBranch, _SponsorName, _SponsorAddress, _SponsorDni, _SponsorEmail,
                        _SponsorPhone, reader.GetString(13) == "0" ? false : true, reader.GetString(14), reader.GetInt32(15)));
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
                Structure objReturn = Structure.getObj(id);
                if (objReturn == null)
                {
                    response.msg = Generic.Message.ID_STRUCTURES_GETOBJECT_NO_EXISTE;
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

        public static object objAdd(Structure obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (StructureType.getObj(obj.IdStructureType) != null)
                {
                    
                    if (SocioEconomic.getObj(obj.IdSocioEconomic) != null)
                    {
                        if (obj.LastModificationDate.Length > 0)
                        {
                            if (Person.getObj(obj.LastModificationPerson) != null)
                            {
                                int? _IdStructureParent = null;
                                int? _IdCommune = null;
                                int? _IdBranch = null;
                                if (obj.IdStructureParent != null || obj.IdStructureParent > 0)
                                {
                                    int IdStructureParent = Convert.ToInt32(obj.IdStructureParent);
                                    if (Structure.getObj(IdStructureParent) != null)
                                    {
                                        _IdStructureParent = obj.IdStructureParent;
                                    }
                                    else
                                    {
                                        response.msg = Generic.Message.ID_STRUCTURES_PARENT_NO_EXISTE;
                                        return response;
                                    }
                                }
                                if (obj.IdCommune != null || obj.IdCommune > 0)
                                {
                                    int IdCommune = Convert.ToInt32(obj.IdCommune);
                                    if (Commune.getObj(IdCommune) != null)
                                    {
                                        _IdCommune = obj.IdCommune;
                                    }
                                    else
                                    {
                                        response.msg = Generic.Message.ID_STRUCTURES_COMMUNE_NO_EXISTE;
                                        return response;
                                    }
                                }
                                if (obj.IdBranch != null || obj.IdBranch > 0)
                                {
                                    int IdBranch = Convert.ToInt32(obj.IdBranch);
                                    if (Branch.getObj(IdBranch) != null)
                                    {
                                        _IdBranch = obj.IdBranch;
                                    }
                                    else
                                    {
                                        response.msg = Generic.Message.ID_STRUCTURES_BRANCH_NO_EXISTE;
                                        return response;
                                    }
                                }
                                obj.StructureName = Generic.Tools.Capital(obj.StructureName);
                                string sqlQuery = "INSERT INTO " + nameof(Structure).ToUpper() + " VALUES(NULL, '" + _IdStructureParent + "' , '" + obj.Address + "' , '" + _IdCommune + "'," +
                                " '" + obj.IdStructureType + "' , '" + obj.IdSocioEconomic + "' , '" + obj.StructureName + "' , '" + _IdBranch + "' , '" + obj.SponsorName + "' , '" + obj.SponsorAddress + "'," +
                                " '" + obj.SponsorDni + "' , '" + obj.SponsorEmail + "' , '" + obj.SponsorPhone + "' , 0 , STR_TO_DATE('" + obj.LastModificationDate + "', '%d/%m/%Y'), '" + obj.LastModificationPerson + "'); SELECT LAST_INSERT_ID();";
                                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                                {
                                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                                    {
                                        conexion.Open();
                                        obj.IdStructure = Convert.ToInt32(comando.ExecuteScalar());
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
                                response.msg = Generic.Message.ID_STRUCTURES_PERSON_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.ID_STRUCTURES_LASTMODIFICATIONDATE_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_STRUCTURES_SOCIOECONOMIC_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_STRUCTURES_STRUCTURETYPE_NO_EXISTE;
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

        public static object objUpdate(Structure obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdStructure > 0)
                {
                    if (Structure.getObj(obj.IdStructure) != null)
                    {
                        if (StructureType.getObj(obj.IdStructureType) != null)
                        {
                            if (SocioEconomic.getObj(obj.IdSocioEconomic) != null)
                            {
                                if (obj.LastModificationDate.Length > 0)
                                {
                                    if (Person.getObj(obj.LastModificationPerson) != null)
                                    {
                                        int? _IdStructureParent = null;
                                        int? _IdCommune = null;
                                        int? _IdBranch = null;
                                        if (obj.IdStructureParent != null || obj.IdStructureParent > 0)
                                        {
                                            int IdStructureParent = Convert.ToInt32(obj.IdStructureParent);
                                            if (Structure.getObj(IdStructureParent) != null)
                                            {
                                                _IdStructureParent = obj.IdStructureParent;
                                            }
                                            else
                                            {
                                                response.msg = Generic.Message.ID_STRUCTURES_PARENT_NO_EXISTE;
                                                return response;
                                            }
                                        }
                                        if (obj.IdCommune != null || obj.IdCommune > 0)
                                        {
                                            int IdCommune = Convert.ToInt32(obj.IdCommune);
                                            if (Commune.getObj(IdCommune) != null)
                                            {
                                                _IdCommune = obj.IdCommune;
                                            }
                                            else
                                            {
                                                response.msg = Generic.Message.ID_STRUCTURES_COMMUNE_NO_EXISTE;
                                                return response;
                                            }
                                        }
                                        if (obj.IdBranch != null || obj.IdBranch > 0)
                                        {
                                            int IdBranch = Convert.ToInt32(obj.IdBranch);
                                            if (Branch.getObj(IdBranch) != null)
                                            {
                                                _IdBranch = obj.IdBranch;
                                            }
                                            else
                                            {
                                                response.msg = Generic.Message.ID_STRUCTURES_BRANCH_NO_EXISTE;
                                                return response;
                                            }
                                        }
                                        obj.StructureName = Generic.Tools.Capital(obj.StructureName);
                                        string sqlQueryUpdate = "UPDATE " + nameof(Structure).ToUpper() + " SET IdStructureParent = '" + _IdStructureParent + "', Address = '" + obj.Address + "', IdCommune = '" + _IdCommune + "', " +
                                        "IdStructureType = '" + obj.IdStructureType + "', IdSocioEconomic = '" + obj.IdSocioEconomic + "', StructureName = '" + obj.StructureName + "', IdBranch = '" + _IdBranch + "', " +
                                        "SponsorName = '" + obj.SponsorName + "', SponsorAddress = '" + obj.SponsorAddress + "', SponsorDni = '" + obj.SponsorDni + "', SponsorEmail = '" + obj.SponsorEmail + "', " +
                                        "SponsorPhone = '" + obj.SponsorPhone + "', LastModificationDate = STR_TO_DATE('" + obj.LastModificationDate + "', '%d/%m/%Y'), LastModificationPerson = '" + obj.LastModificationPerson + "' WHERE IdStructure =  " + obj.IdStructure + ";";
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
                                        response.msg = Generic.Message.ID_STRUCTURES_PERSON_NO_EXISTE;
                                        return response;
                                    }
                                }
                                else
                                {
                                    response.msg = Generic.Message.ID_STRUCTURES_LASTMODIFICATIONDATE_NO_EXISTE;
                                    return response;
                                }
                            }
                            else
                            {
                                response.msg = Generic.Message.ID_STRUCTURES_SOCIOECONOMIC_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.ID_STRUCTURES_STRUCTURETYPE_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_STRUCTURES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_STRUCTURES_NO_EXISTE;
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


        public static object objDelete(Structure obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdStructure > 0)
                {
                    if (Structure.getObj(obj.IdStructure) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Structure.getObj(obj.IdStructure).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Structure).ToUpper() + " SET IsDeleted = 0 WHERE IdStructure =  " + obj.IdStructure + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Structure).ToUpper() + " SET IsDeleted = 1 WHERE IdStructure =  " + obj.IdStructure + ";";
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
                        response.data = Structure.getObj(obj.IdStructure);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_STRUCTURES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_STRUCTURES_NO_EXISTE;
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
                List<Structure> objReturnList = new List<Structure>();
                string sqlQuery = "SELECT IdStructure, IdStructureParent, Address, IdCommune, IdStructureType, IdSocioEconomic, " +
                "StructureName, IdBranch, SponsorName, SponsorAddress, SponsorDni, SponsorEmail, SponsorPhone, IsDeleted, DATE_FORMAT(LASTMODIFICATIONDATE, '%d/%m/%Y'), LastModificationPerson FROM "
                + nameof(Structure).ToUpper() + " WHERE IsDeleted = 0 ORDER BY StructureName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            int? _IdStructureParent = null;
                            string _Address = null;
                            int? _IdCommune = null;
                            string _StructureName = null;
                            int? _IdBranch = null;
                            string _SponsorName = null;
                            string _SponsorAddress = null;
                            string _SponsorDni = null;
                            string _SponsorEmail = null;
                            int? _SponsorPhone = null;
                            if (!reader.IsDBNull(1))
                            {
                                _IdStructureParent = reader.GetInt32(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                _Address = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                _IdCommune = reader.GetInt32(3);
                            }
                            if (!reader.IsDBNull(6))
                            {
                                _StructureName = reader.GetString(6);
                            }
                            if (!reader.IsDBNull(7))
                            {
                                _IdBranch = reader.GetInt32(7);
                            }
                            if (!reader.IsDBNull(8))
                            {
                                _SponsorName = reader.GetString(8);
                            }
                            if (!reader.IsDBNull(9))
                            {
                                _SponsorAddress = reader.GetString(9);
                            }
                            if (!reader.IsDBNull(10))
                            {
                                _SponsorDni = reader.GetString(10);
                            }
                            if (!reader.IsDBNull(11))
                            {
                                _SponsorEmail = reader.GetString(11);
                            }
                            if (!reader.IsDBNull(12))
                            {
                                _SponsorPhone = reader.GetInt32(12);
                            }
                            objReturnList.Add(new Structure(reader.GetInt32(0), _IdStructureParent, _Address, _IdCommune, reader.GetInt32(4),
                            reader.GetInt32(5), _StructureName, _IdBranch, _SponsorName, _SponsorAddress, _SponsorDni, _SponsorEmail,
                            _SponsorPhone, reader.GetString(13) == "0" ? false : true, reader.GetString(14), reader.GetInt32(15)));
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
                List<Structure> objReturnList = new List<Structure>();
                string sqlQuery = "SELECT IdStructure, IdStructureParent, Address, IdCommune, IdStructureType, IdSocioEconomic, " +
                "StructureName, IdBranch, SponsorName, SponsorAddress, SponsorDni, SponsorEmail, SponsorPhone, IsDeleted, DATE_FORMAT(LASTMODIFICATIONDATE, '%d/%m/%Y'), LastModificationPerson FROM "
                + nameof(Structure).ToUpper() + " ORDER BY StructureName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            int? _IdStructureParent = null;
                            string _Address = null;
                            int? _IdCommune = null;
                            string _StructureName = null;
                            int? _IdBranch = null;
                            string _SponsorName = null;
                            string _SponsorAddress = null;
                            string _SponsorDni = null;
                            string _SponsorEmail = null;
                            int? _SponsorPhone = null;
                            if (!reader.IsDBNull(1))
                            {
                                _IdStructureParent = reader.GetInt32(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                _Address = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                _IdCommune = reader.GetInt32(3);
                            }
                            if (!reader.IsDBNull(6))
                            {
                                _StructureName = reader.GetString(6);
                            }
                            if (!reader.IsDBNull(7))
                            {
                                _IdBranch = reader.GetInt32(7);
                            }
                            if (!reader.IsDBNull(8))
                            {
                                _SponsorName = reader.GetString(8);
                            }
                            if (!reader.IsDBNull(9))
                            {
                                _SponsorAddress = reader.GetString(9);
                            }
                            if (!reader.IsDBNull(10))
                            {
                                _SponsorDni = reader.GetString(10);
                            }
                            if (!reader.IsDBNull(11))
                            {
                                _SponsorEmail = reader.GetString(11);
                            }
                            if (!reader.IsDBNull(12))
                            {
                                _SponsorPhone = reader.GetInt32(12);
                            }
                            objReturnList.Add(new Structure(reader.GetInt32(0), _IdStructureParent, _Address, _IdCommune, reader.GetInt32(4),
                            reader.GetInt32(5), _StructureName, _IdBranch, _SponsorName, _SponsorAddress, _SponsorDni, _SponsorEmail,
                            _SponsorPhone, reader.GetString(13) == "0" ? false : true, reader.GetString(14), reader.GetInt32(15)));
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
