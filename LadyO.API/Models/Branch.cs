using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Branch
    {
        public int IdBranch { get; set; }
        public string BranchName { get; set; }
        public string UnitName { get; set; }
        public string TeamName { get; set; }
        public bool IsDeleted { get; set; }
        public string LastModificationDate { get; set; }
        public int LastModificationPerson { get; set; }

        public Branch()
        {

        }

        public Branch(int idBranch, string branchName, string unitName, string teamName, bool isDeleted, string lastModificationDate, int lastModificationPerson)
        {
            IdBranch = idBranch;
            BranchName = branchName;
            UnitName = unitName;
            TeamName = teamName;
            IsDeleted = isDeleted;
            LastModificationDate = lastModificationDate;
            LastModificationPerson = lastModificationPerson;
        }

        public static Branch getObj(int idBranch)
        {
            List<Branch> objReturnList = new List<Branch>();
            string sqlQuery = "SELECT IdBranch, BranchName, UnitName, TeamName, IsDeleted, DATE_FORMAT(LASTMODIFICATIONDATE, '%d/%m/%Y'), LastModificationPerson FROM " + nameof(Branch).ToUpper() + " WHERE IdBranch = " + idBranch + ";";
            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
            {
                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                {
                    conexion.Open();
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        objReturnList.Add(new Branch(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4) == "0" ? false : true, reader.GetString(5), reader.GetInt32(6)));
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
                Branch objReturn = Branch.getObj(id);
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

        public static object objAdd(Branch obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.BranchName.Length > 0)
                {
                    if (obj.UnitName.Length > 0)
                    {
                        if (obj.TeamName.Length > 0)
                        {
                            if (obj.LastModificationDate.Length > 0)
                            {
                                if (obj.LastModificationPerson > 0)
                                {
                                    obj.BranchName = Generic.Tools.Capital(obj.BranchName);
                                    obj.UnitName = Generic.Tools.Capital(obj.UnitName);
                                    obj.TeamName = Generic.Tools.Capital(obj.TeamName);
                                    string sqlQuery = "INSERT INTO " + nameof(Branch).ToUpper() + " VALUES(NULL, '" + obj.BranchName + "' , '" + obj.TeamName + "' , 0 , '" + obj.UnitName + "', STR_TO_DATE('" + obj.LastModificationDate + "', '%d/%m/%Y'), '" + obj.LastModificationPerson + "); SELECT LAST_INSERT_ID();";
                                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                                    {
                                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                                        {
                                            conexion.Open();
                                            obj.IdBranch = Convert.ToInt32(comando.ExecuteScalar());
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
                                    response.msg = Generic.Message.ID_BRANCHES_LASTMODIFICATIONPERSON_NO_EXISTE;
                                    return response;
                                }
                            }
                            else
                            {
                                response.msg = Generic.Message.ID_BRANCHES_LASTMODIFICATIONDATE_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.ID_BRANCHES_TEAMNAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_BRANCHES_UNITNAME_NO_EXISTE;
                        return response;
                    }
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

        public static object objUpdate(Branch obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdBranch > 0)
                {
                    if (Branch.getObj(obj.IdBranch) != null)
                    {
                        if (obj.BranchName.Length > 0)
                        {
                            if (obj.UnitName.Length > 0)
                            {
                                if (obj.TeamName.Length > 0)
                                {
                                    if (obj.LastModificationDate.Length > 0)
                                    {
                                        if (obj.LastModificationPerson > 0)
                                        {
                                            obj.BranchName = Generic.Tools.Capital(obj.BranchName);
                                            string sqlQueryUpdate = "UPDATE " + nameof(Branch).ToUpper() + " SET BranchName = '" + obj.BranchName + "', UnitName = '" + obj.UnitName + "', TeamName = '" + obj.TeamName + "', LastModificationDate = STR_TO_DATE('" + obj.LastModificationDate + "', '%d/%m/%Y'), LastModificationPerson = '" + obj.LastModificationPerson + "' WHERE IdBranch =  " + obj.IdBranch + ";";
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
                                            response.msg = Generic.Message.ID_BRANCHES_LASTMODIFICATIONPERSON_NO_EXISTE;
                                            return response;
                                        }
                                    }
                                    else
                                    {
                                        response.msg = Generic.Message.ID_BRANCHES_LASTMODIFICATIONDATE_NO_EXISTE;
                                        return response;
                                    }
                                }
                                else
                                {
                                    response.msg = Generic.Message.ID_BRANCHES_TEAMNAME_NO_EXISTE;
                                    return response;
                                }
                            }
                            else
                            {
                                response.msg = Generic.Message.ID_BRANCHES_UNITNAME_NO_EXISTE;
                                return response;
                            }
                        }
                        else
                        {
                            response.msg = Generic.Message.NAME_NO_EXISTE;
                            return response;
                        }
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_BRANCHES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_BRANCHES_NO_EXISTE;
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


        public static object objDelete(Branch obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            response.isValid = false;
            try
            {
                if (obj.IdBranch > 0)
                {
                    if (Branch.getObj(obj.IdBranch) != null)
                    {
                        string sqlQueryUpdate = string.Empty;
                        if (Branch.getObj(obj.IdBranch).IsDeleted)
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Branch).ToUpper() + " SET IsDeleted = 0 WHERE IdBranch =  " + obj.IdBranch + ";";
                        }
                        else
                        {
                            sqlQueryUpdate = "UPDATE " + nameof(Branch).ToUpper() + " SET IsDeleted = 1 WHERE IdBranch =  " + obj.IdBranch + ";";
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
                        response.data = Branch.getObj(obj.IdBranch);
                    }
                    else
                    {
                        response.msg = Generic.Message.ID_BRANCHES_NO_EXISTE;
                        return response;
                    }
                }
                else
                {
                    response.msg = Generic.Message.ID_BRANCHES_NO_EXISTE;
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
                List<Branch> objReturnList = new List<Branch>();
                string sqlQuery = "SELECT IdBranch, BranchName, UnitName, TeamName, IsDeleted, DATE_FORMAT(LastModificationDate, '%d/%m/%Y'), LastModificationPerson FROM " + nameof(Branch).ToUpper() + " WHERE IsDeleted = 0 ORDER BY BranchName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Branch(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4) == "0" ? false : true, reader.GetString(5), reader.GetInt32(6)));
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
                List<Branch> objReturnList = new List<Branch>();
                string sqlQuery = "SELECT IdBranch, BranchName, UnitName, TeamName, IsDeleted, DATE_FORMAT(LastModificationDate, '%d/%m/%Y'), LastModificationPerson FROM " + nameof(Branch).ToUpper() + " WHERE IsDeleted = 0 ORDER BY BranchName;";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Branch(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4) == "0" ? false : true, reader.GetString(5), reader.GetInt32(6)));
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
