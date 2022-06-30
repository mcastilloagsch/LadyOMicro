﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Genders
    {
        public int id { get; set; }
        public string name { get; set; }

        public Genders()
        {

        }

        public Genders(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public static bool ObjInsert(Genders objInsert)
        {
            try
            {
                string sqlQuery = "INSERT INTO " + Generic.DBConnection.SCHEMA + ".genders VALUES(0, '" + objInsert.name + "')";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        comando.ExecuteReader();
                        conexion.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ObjUpdate(Genders objUpdate)
        {
            try
            {
                if (ObjList(objUpdate.id).Count == 1)
                {
                    string sqlQuery = "UPDATE " + Generic.DBConnection.SCHEMA + ".genders SET name = '" + objUpdate.name + "' WHERE id = " + objUpdate.id;
                    using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                    {
                        using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                        {
                            conexion.Open();
                            comando.ExecuteReader();
                            conexion.Close();
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Genders> ObjList()
        {
            try
            {
                List<Genders> objReturnList = new List<Genders>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".genders";
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Genders(reader.GetInt32(0), reader.GetString(1)));
                        }
                        conexion.Close();
                    }
                }
                return objReturnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<Genders> ObjList(int id)
        {
            try
            {
                List<Genders> objReturnList = new List<Genders>();
                string sqlQuery = "SELECT id, name FROM " + Generic.DBConnection.SCHEMA + ".genders WHERE id = " + id;
                using (MySqlConnection conexion = Generic.DBConnection.MySqlConnectionObj())
                {
                    using (MySqlCommand comando = new MySqlCommand(sqlQuery, conexion))
                    {
                        conexion.Open();
                        MySqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            objReturnList.Add(new Genders(reader.GetInt32(0), reader.GetString(1)));
                        }
                        conexion.Close();
                    }
                }
                return objReturnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}