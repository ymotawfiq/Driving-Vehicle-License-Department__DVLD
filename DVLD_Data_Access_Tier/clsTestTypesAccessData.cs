using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Data_Access_Tier;

namespace DVLD_Business_Logic_Tear
{
    public class clsTestTypesAccessData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from TestTypes";
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

        public static bool FindTestTypeByID(int id, ref string title, ref string description,ref double fees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from TestTypes where TestTypeID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    title = reader["TestTypeTitle"].ToString();
                    description = reader["TestTypeDescription"].ToString();
                    fees = Convert.ToDouble(reader["TestTypeFees"]);
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
            return isFound;
        }

        public static bool UpdateTestTypeByID(int id, string title, string description, double fees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"update TestTypes set TestTypeTitle=@Title,
               TestTypeFees=@Fees, TestTypeDescription=@Description where TestTypeID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Fees", fees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }


    }
}
