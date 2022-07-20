using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace LadyO.API.Models
{
    public class UnitAttributes
    {
        public int structure_id { get; set; }
        public int branch_id { get; set; }

        public UnitAttributes()
        {

        }

        public UnitAttributes(int structure_id, int branch_id)
        {
            this.structure_id = structure_id;
            this.branch_id = branch_id;
        }

        public static object getList()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                List<UnitAttributes> objReturnList = new List<UnitAttributes>();
                string sqlQuery = "SELECT structure_id, branch_id FROM " + Generic.DBConnection.SCHEMA + ".unit_attributes";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new UnitAttributes(reader.GetInt32(0), reader.GetInt32(1)));
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

        public static object getObject(int structure_id, int branch_id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                List<UnitAttributes> objReturnList = new List<UnitAttributes>();
                string sqlQuery = "SELECT structure_id, branch_id FROM " + Generic.DBConnection.SCHEMA + ".unit_attributes WHERE structure_id = " + structure_id + " and branch_id = " + branch_id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new UnitAttributes(reader.GetInt32(0), reader.GetInt32(1)));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() == null)
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_UNITATTRIBUTES_GETOBJECT_NO_EXISTE;
                    response.data = null;
                    return response;
                }
                else
                {
                    response.isValid = true;
                    response.msg = string.Empty;
                    response.data = objReturnList.FirstOrDefault();
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


        private static UnitAttributes getUnitAttribute(int structure_id, int branch_id)
        {
            try
            {
                List<UnitAttributes> objReturnList = new List<UnitAttributes>();
                string sqlQuery = "SELECT structure_id, branch_id FROM " + Generic.DBConnection.SCHEMA + ".unit_attributes WHERE structure_id = " + structure_id + " and branch_id = " + branch_id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new UnitAttributes(reader.GetInt32(0), reader.GetInt32(1)));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() != null)
                {
                    return objReturnList.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static Structures getStructure(int id)
        {
            try
            {
                List<Structures> objReturnList = new List<Structures>();
                string sqlQuery = "SELECT id, name, structure_type_id, parent_id FROM " + Generic.DBConnection.SCHEMA + ".structures WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            int? _parent_id = null;
                            if (!reader.IsDBNull(3))
                            {
                                _parent_id = reader.GetInt32(3);
                            }
                            objReturnList.Add(new Structures(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), _parent_id));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() != null)
                {
                    return objReturnList.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static Branches getBranch(int id)
        {
            try
            {
                List<Branches> objReturnList = new List<Branches>();
                string sqlQuery = "SELECT id, name, unit_name, small_team FROM " + Generic.DBConnection.SCHEMA + ".branches WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            string _unit_name = null;
                            string _small_team = null;
                            if (!reader.IsDBNull(2))
                            {
                                _unit_name = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                _small_team = reader.GetString(3);
                            }
                            objReturnList.Add(new Branches(reader.GetInt32(0), reader.GetString(1), _unit_name, _small_team));
                        }
                        conexion.Close();
                    }
                }
                if (objReturnList.FirstOrDefault() != null)
                {
                    return objReturnList.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static object ObjInsert(UnitAttributes objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            UnitAttributes objData = new UnitAttributes();
            try
            {
                Structures validFk_structure = getStructure(objInsert.structure_id);
                Branches validFk_branch = getBranch(objInsert.branch_id);
                if (validFk_structure != null)
                {
                    if (validFk_branch != null)
                    {
                        UnitAttributes valid = getUnitAttribute(objInsert.structure_id, objInsert.branch_id);
                        if (valid == null)
                        {
                            string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".unit_attributes VALUES('" + objInsert.structure_id + "', '" + objInsert.branch_id + "');";
                            using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                            {
                                using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                                {
                                    conexion.Open();
                                    comando.ExecuteScalar();
                                    conexion.Close();
                                }
                            }
                            objData.structure_id = objInsert.structure_id;
                            objData.branch_id = objInsert.branch_id;
                            response.isValid = true;
                            response.msg = string.Empty;
                            response.data = objData;
                            return response;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.ID_UNITATTRIBUTES_REPEATED;
                            response.data = null;
                            return response;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.ID_UNITATTRIBUTES_BRANCH_NO_EXISTE;
                        response.data = null;
                        return response;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.ID_UNITATTRIBUTES_STRUCTURE_NO_EXISTE;
                    response.data = null;
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


    }
}