using System;
using System.Configuration;
using System.Data.SqlClient;

namespace M17_TP01_N02.Modal
{
    public class Database
    {
        private static Database _instance;
        private readonly string _connectionString;
        private readonly SqlConnection _ligDb;

        public static Database DatabaseInstance => _instance ?? (_instance = new Database());

        public Database()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["sql"].ToString();
            _ligDb = new SqlConnection(_connectionString);
            _ligDb.Open();
        }

        ~Database()
        {
            try
            {
                _ligDb.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}