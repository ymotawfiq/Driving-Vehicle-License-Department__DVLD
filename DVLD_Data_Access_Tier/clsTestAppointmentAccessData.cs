using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_Data_Access_Tier
{
    public class clsTestAppointmentAccessData
    {

        public static bool ExistsByTestTypeAndLDLAppID(int TestTypeID, int ldlAppID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 found=1 from TestAppointments 
                where 
                TestAppointments.TestTypeID=@TestTypeID 
                and 
                TestAppointments.LocalDrivingLicenseApplicationID =@ldlAppID
                and
                TestAppointments.IsLocked=0
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int x))
                {
                    isFound = true;
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
            return isFound;
        }

        public static bool FindByID(int id, ref int TestTypeID, ref int ldlAppID, 
            ref DateTime AppointmentDate, ref double PaidFees, ref int UserID, ref bool IsLocked)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from TestAppointments 
                where 
                TestAppointments.TestAppointmentID=@id 
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
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);
                    ldlAppID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
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

        public static bool IsTestAppointmentLocked(int testAppointmentID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 found=1 from TestAppointments 
                where 
                TestAppointments.TestAppointmentID=@testAppointmentID 
                and 
                TestAppointments.IsLocked =1
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

        public static DataTable GetAllTestAppointmentsByLocalDrivingLicenseAndTestType(
            int ldlAppID, int TestTypeID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from TestAppointments 
                where 
                TestAppointments.LocalDrivingLicenseApplicationID =@ldlAppID
                and TestAppointments.TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
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
            return dataTable;
        }

        public static bool AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, double PaidFees, int CreatedByUserID, ref int AppointmentID)
        {
            bool isAdded = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                INSERT INTO [dbo].[TestAppointments]
                ([TestTypeID]
               ,[LocalDrivingLicenseApplicationID]
               ,[AppointmentDate]
               ,[PaidFees]
               ,[CreatedByUserID]
               ,[IsLocked])
                VALUES
               (@TestTypeID
               ,@LocalDrivingLicenseApplicationID
               ,@AppointmentDate
               ,@PaidFees
               ,@CreatedByUserID
               ,0); select SCOPE_IDENTITY();
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", 
                LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    AppointmentID = id;
                    isAdded = true;
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
            return isAdded;
        }

        public static bool UpdateTestAppointment(int AppointmentID, int TestTypeID, 
            int LocalDrivingLicenseApplicationID, bool IsLocked,
            DateTime AppointmentDate, double PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                UPDATE [dbo].[TestAppointments]
                   SET [TestTypeID] = @TestTypeID
                      ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID
                      ,[AppointmentDate] = @AppointmentDate
                      ,[PaidFees] = @PaidFees
                      ,[CreatedByUserID] = @CreatedByUserID
                      ,[IsLocked] = @IsLocked
                 WHERE TestAppointmentID=@AppointmentID
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",
                LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
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
