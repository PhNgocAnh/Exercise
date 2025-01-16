using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ExerciseWeb
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        // Lấy chuỗi kết nối từ web.config
        private string connectionString = "Server=localhost;Database=world;Uid=root;Pwd=123456;";

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        // Lấy tất cả quốc gia
        [WebMethod]
        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            string query = "SELECT Name FROM country";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(reader["Name"].ToString());
                }
            }
            return countries;
        }


        // Lấy mã quốc gia theo tên
        [WebMethod]
        public string GetCountryCode(string countryName)
        {
            string countryCode = "";
            string query = "SELECT Code FROM country WHERE Name = @CountryName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    countryCode = result.ToString();
                }
            }
            return countryCode;
        }

        // Lấy tất cả thành phố của một quốc gia
        [WebMethod]
        public List<string> GetCitiesByCountry(string countryCode)
        {
            List<string> cities = new List<string>();
            string query = "SELECT Name FROM city WHERE CountryCode = @CountryCode";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CountryCode", countryCode);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(reader["Name"].ToString());
                }
            }
            return cities;
        }

        // Lấy ngôn ngữ của 1 quốc gia
        [WebMethod]
        public List<string> GetLanguagesByCountry(string countryCode)
        {
            List<string> languages = new List<string>();
            string query = "SELECT Language FROM countrylanguage WHERE CountryCode = @CountryCode";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CountryCode", countryCode);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    languages.Add(reader["Language"].ToString());
                }
            }
            return languages;
        }


    }
}
