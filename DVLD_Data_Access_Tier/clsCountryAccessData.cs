using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Tier
{
    public class clsCountryAccessData
    {

        public static DataTable GetAllCountries()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = "select * from Countries";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }
        
        public static void FindCountryByName(string countryName, ref int countryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = "select CountryID from Countries where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result!=null)
                {
                    countryID = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void FindCountryByID(int countryID, ref string countryName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = "select * from Countries where CountryID=@CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", countryID+1);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    countryName = reader["CountryName"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool ExistsByCountryNameForFilter(string countryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = "select found = 1 from Countries where CountryName like '%'+@CountryName+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", countryName);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

    }
}
