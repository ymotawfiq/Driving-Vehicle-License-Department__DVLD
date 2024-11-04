using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Tier
{
    public class clsInternationalDrivingLicenseAccessData
    {

        public static int AddNew(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int licenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            INSERT INTO [dbo].[InternationalLicenses]
           ([ApplicationID]
           ,[DriverID]
           ,[IssuedUsingLocalLicenseID]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[IsActive]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@IssuedUsingLocalLicenseID
           ,@IssueDate
           ,@ExpirationDate
           ,@IsActive
           ,@CreatedByUserID);
            select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int id))
                {
                    licenseID = id;
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
            return licenseID;
        }

        public static bool UpdateByID(int licenseID, int ApplicationID, int DriverID, 
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            UPDATE [dbo].[InternationalLicenses]
               SET [ApplicationID] = @ApplicationID
                  ,[DriverID] = @DriverID
                  ,[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID
                  ,[IssueDate] = @IssueDate
                  ,[ExpirationDate] = @ExpirationDate
                  ,[IsActive] = @IsActive
                  ,[CreatedByUserID] = @CreatedByUserID
             WHERE InternationalLicenseID=@licenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", licenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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

        public static bool FindByID(int id, ref int ApplicationID, ref int DriverID, 
            ref int IssuedUsingLocalLicenseID,
        ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                SELECT *
                  FROM [dbo].[InternationalLicenses]
                  where InternationalLicenses.InternationalLicenseID=@InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
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

        public static bool FindByLicenseID(int licenseID, ref int id, ref int ApplicationID, 
            ref int DriverID,
        ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                SELECT *
                  FROM [dbo].[InternationalLicenses]
                  where InternationalLicenses.IssuedUsingLocalLicenseID=@licenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", licenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    id = Convert.ToInt32(reader["InternationalLicenseID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
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

        public static bool ExistsByID(int id)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                SELECT found = 1
                  FROM [dbo].[InternationalLicenses]
                  where InternationalLicenses.InternationalLicenseID=@InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", id);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int x))
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

        public static bool ExistsByLicenseID(int licenseID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                SELECT *
                  FROM [dbo].[InternationalLicenses]
                  where InternationalLicenses.IssuedUsingLocalLicenseID=@licenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseID", licenseID);
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

        public static bool DeleteByID(int id)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                delete
                  FROM [dbo].[InternationalLicenses]
                  where InternationalLicenses.InternationalLicenseID=@InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", id);
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


        public static DataTable GetAllByPersonID(int PersonID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"                
                select * from
                (select distinct
				InternationalLicenses.InternationalLicenseID,
                InternationalLicenses.IssuedUsingLocalLicenseID, 
                InternationalLicenses.ApplicationID,
                Licenses.LicenseClass,
                InternationalLicenses.IssueDate,
                InternationalLicenses.ExpirationDate,
                InternationalLicenses.IsActive,
                InternationalLicenses.DriverID
                from InternationalLicenses
				inner join Licenses
				on
				InternationalLicenses.IssuedUsingLocalLicenseID=Licenses.LicenseID
                inner join Drivers
                on
                Licenses.DriverID=Drivers.DriverID
                inner join People
                on
                Drivers.PersonID=People.PersonID
                inner join Applications
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LocalDrivingLicenseApplications
                on
                LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
                )R1
                where R1.DriverID=(
                select DriverID 
                from Drivers 
                where PersonID=@PersonID)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }

        public static DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"                
                select * from
                (select distinct
				InternationalLicenses.InternationalLicenseID,
                InternationalLicenses.IssuedUsingLocalLicenseID, 
                InternationalLicenses.ApplicationID,
                Licenses.LicenseClass,
                InternationalLicenses.IssueDate,
                InternationalLicenses.ExpirationDate,
                InternationalLicenses.IsActive,
                InternationalLicenses.DriverID
                from InternationalLicenses
				inner join Licenses
				on
				InternationalLicenses.IssuedUsingLocalLicenseID=Licenses.LicenseID
                inner join Drivers
                on
                Licenses.DriverID=Drivers.DriverID
                inner join People
                on
                Drivers.PersonID=People.PersonID
                inner join Applications
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LocalDrivingLicenseApplications
                on
                LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
                )R1";
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

    }
}
