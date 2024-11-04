using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Tier
{
    public class clsDriversAccessData
    {
        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int driverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
        INSERT INTO [dbo].[Drivers]
           ([PersonID]
           ,[CreatedByUserID]
           ,[CreatedDate])
     VALUES
           (@PersonID
           ,@CreatedByUserID
           ,@CreatedDate);
            select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int id))
                {
                    driverID = id;
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
            return driverID;
        }

        public static bool IsDriverExistsByDriverID(int DriverID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select found=1 from Drivers where DriverID=@DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
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

        public static bool IsDriverExistsByPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select found=1 from Drivers where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
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

        public static bool FindByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID,
            ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from Drivers where DriverID=@DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
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

        public static bool FindByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID,
            ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from Drivers where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
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


        public static DataTable GetAllDrivers()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select distinct * from
            (select
            Drivers.DriverID,
            People.PersonID,
            People.NationalNo,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            Licenses.IssueDate,
            (select count(*) from Licenses
            where
            Licenses.IsActive=1
            and
            Licenses.DriverID=Drivers.DriverID) as ActiveLicenses
            from Drivers
            inner join People
            on
            Drivers.PersonID=People.PersonID
            inner join Applications
            on
            Applications.ApplicantPersonID=People.PersonID
            inner join LocalDrivingLicenseApplications
            on
            LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
            inner join Licenses
            on
            Licenses.IsActive=1
            )R1
            where R1.IssueDate=(
            select top 1 IssueDate 
            from Licenses 
            where 
            DriverID=R1.DriverID
            order by IssueDate desc
            )";
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

        public static DataTable GetAllDriversByID(int DriverID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select distinct * from
            (select
            Drivers.DriverID,
            People.PersonID,
            People.NationalNo,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            Licenses.IssueDate,
            (select count(*) from Licenses
            where
            Licenses.IsActive=1
            and
            Licenses.DriverID=Drivers.DriverID) as ActiveLicenses
            from Drivers
            inner join People
            on
            Drivers.PersonID=People.PersonID
            inner join Applications
            on
            Applications.ApplicantPersonID=People.PersonID
            inner join LocalDrivingLicenseApplications
            on
            LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
            inner join Licenses
            on
            Licenses.IsActive=1
            )R1
            where R1.IssueDate=(
            select top 1 IssueDate 
            from Licenses 
            where 
            DriverID=R1.DriverID
            order by IssueDate desc
            )
            and R1.DriverID=@DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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

        public static DataTable GetAllDriversByPersonID(int PersonID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select distinct * from
            (select
            Drivers.DriverID,
            People.PersonID,
            People.NationalNo,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            Licenses.IssueDate,
            (select count(*) from Licenses
            where
            Licenses.IsActive=1
            and
            Licenses.DriverID=Drivers.DriverID) as ActiveLicenses
            from Drivers
            inner join People
            on
            Drivers.PersonID=People.PersonID
            inner join Applications
            on
            Applications.ApplicantPersonID=People.PersonID
            inner join LocalDrivingLicenseApplications
            on
            LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
            inner join Licenses
            on
            Licenses.IsActive=1
            )R1
            where R1.IssueDate=(
            select top 1 IssueDate 
            from Licenses 
            where 
            DriverID=R1.DriverID
            order by IssueDate desc
            )
            and R1.PersonID=@PersonID";
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

        public static DataTable GetAllDriversByNationalNo(string NationalNo)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select distinct * from
            (select
            Drivers.DriverID,
            People.PersonID,
            People.NationalNo,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            Licenses.IssueDate,
            (select count(*) from Licenses
            where
            Licenses.IsActive=1
            and
            Licenses.DriverID=Drivers.DriverID) as ActiveLicenses
            from Drivers
            inner join People
            on
            Drivers.PersonID=People.PersonID
            inner join Applications
            on
            Applications.ApplicantPersonID=People.PersonID
            inner join LocalDrivingLicenseApplications
            on
            LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
            inner join Licenses
            on
            Licenses.IsActive=1
            )R1
            where R1.IssueDate=(
            select top 1 IssueDate 
            from Licenses 
            where 
            DriverID=R1.DriverID
            order by IssueDate desc
            )
            and R1.NationalNo like '%'+@NationalNo+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
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

        public static DataTable GetAllDriversByFullName(string FullName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select distinct * from
            (select
            Drivers.DriverID,
            People.PersonID,
            People.NationalNo,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            Licenses.IssueDate,
            (select count(*) from Licenses
            where
            Licenses.IsActive=1
            and
            Licenses.DriverID=Drivers.DriverID) as ActiveLicenses
            from Drivers
            inner join People
            on
            Drivers.PersonID=People.PersonID
            inner join Applications
            on
            Applications.ApplicantPersonID=People.PersonID
            inner join LocalDrivingLicenseApplications
            on
            LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
            inner join Licenses
            on
            Licenses.IsActive=1
            )R1
            where R1.IssueDate=(
            select top 1 IssueDate 
            from Licenses 
            where 
            DriverID=R1.DriverID
            order by IssueDate desc
            )
            and R1.FullName like '%'+@FullName+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", FullName);
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
