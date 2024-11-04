using System;
using System.Data.SqlClient;


namespace DVLD_Data_Access_Tier
{
    public class clsTestAccessData
    {

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int UserID)
        {
            int TestID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
           INSERT INTO [dbo].[Tests]
           ([TestAppointmentID]
           ,[TestResult]
           ,[Notes]
           ,[CreatedByUserID])
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);
            select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", UserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int id))
                {
                    TestID = id;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return TestID;
        }

        public static bool ExistsIfPassedTestByLDLAppIDAndTestTypeID(int LocalDrivingLicenseApplicationID,
            int TestTypeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 found=1 
                from Tests
                inner join TestAppointments
                on
                Tests.TestAppointmentID=TestAppointments.TestAppointmentID
                where
                TestAppointments.TestTypeID=@TestTypeID
                and 
                Tests.TestResult=1
                and 
                Tests.TestAppointmentID=
                (select top 1 TestAppointments.TestAppointmentID from TestAppointments
                where 
                TestAppointments.LocalDrivingLicenseApplicationID
                =@LocalDrivingLicenseApplicationID
                order by TestAppointments.TestAppointmentID desc)
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", 
                LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int x))
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

        public static bool ExistsIfFailedTestByTestAppointment(int testAppointmentID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 found=1 from Tests
                where 
                Tests.TestAppointmentID=@testAppointmentID 
                and 
                Tests.TestResult = 0
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentID", testAppointmentID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int x))
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

        public static bool FindByID(int id, ref int TestAppointmentID, ref bool TestResult,
        ref string Notes, ref int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from Tests 
                where 
                Tests.TestID=@id 
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    Notes = reader["Notes"].ToString();
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

        public static bool FindByTestAppointmentID(int TestAppointmentID, ref int id, ref bool TestResult,
            ref string Notes, ref int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from Tests 
                where 
                Tests.TestAppointmentID=@TestAppointmentID 
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    id = Convert.ToInt32(reader["id"]);
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    Notes = reader["Notes"].ToString();
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

    }
}
