using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Tier
{
    public class clsDetainedLicensesAccessData
    {

        public static int AddDetainedLicense(int LicenseID, DateTime DetainDate, double FineFees,
            int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            int licenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
        INSERT INTO [dbo].[DetainedLicenses]
           ([LicenseID]
           ,[DetainDate]
           ,[FineFees]
           ,[CreatedByUserID]
           ,[IsReleased]
           ,[ReleaseDate]
           ,[ReleasedByUserID]
           ,[ReleaseApplicationID])
     VALUES
           (@LicenseID
           ,@DetainDate
           ,@FineFees
           ,@CreatedByUserID
           ,@IsReleased
           ,@ReleaseDate
           ,@ReleasedByUserID
           ,@ReleaseApplicationID);
            select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            if (ReleaseDate == default)
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            if (ReleasedByUserID == default)
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            if (ReleaseApplicationID == default)
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            double FineFees,
        int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID,
        int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            UPDATE [dbo].[DetainedLicenses]
               SET [LicenseID] = @LicenseID
                  ,[DetainDate] = @DetainDate
                  ,[FineFees] = @FineFees
                  ,[CreatedByUserID] = @CreatedByUserID
                  ,[IsReleased] = @IsReleased
                  ,[ReleaseDate] = @ReleaseDate
                  ,[ReleasedByUserID] = @ReleasedByUserID
                  ,[ReleaseApplicationID] = @ReleaseApplicationID
             WHERE DetainID=@DetainID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            if(ReleaseDate == default)
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            if(ReleasedByUserID == default)
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            else 
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            if(ReleaseApplicationID==default)
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            else 
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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

        public static bool FindByID(int DetainID, ref int LicenseID, ref DateTime DetainDate,
            ref double FineFees,
        ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID,
        ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select * from [dbo].[DetainedLicenses]
             WHERE DetainID=@DetainID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToDouble(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);
                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleaseDate = default;
                    else
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = default;
                    else 
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);
                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = default;
                    else 
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
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
        public static bool ExistsByID(int DetainID) { 
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select found=1 from [dbo].[DetainedLicenses]
             WHERE DetainID=@DetainID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
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

        public static bool FindByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate,
            ref double FineFees,
        ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID,
        ref int ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select * from [dbo].[DetainedLicenses]
            WHERE LicenseID=@LicenseID and DetainedLicenses.IsReleased=0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToDouble(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);
                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleaseDate = default;
                    else
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = default;
                    else
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);
                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = default;
                    else
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
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

        public static bool ExistsByLicenseID(int LicenseID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select found=1 from [dbo].[DetainedLicenses]
            WHERE LicenseID=@LicenseID and DetainedLicenses.IsReleased=0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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

        public static bool DeleteByID(int DetainID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            delete from [dbo].[DetainedLicenses]
             WHERE DetainID=@DetainID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
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

        public static DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID";
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

        public static DataTable GetAllReleasedLicenses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID
            where DetainedLicenses.IsReleased=1";
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

        public static DataTable GetAllUnReleasedLicenses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID
            where DetainedLicenses.IsReleased=0";
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

        public static DataTable GetAllByFullName(string FullName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select * from
            (select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID
            )R1
            where R1.FullName like '%'+@FullName+'%'";
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

        public static DataTable GetAllByNationalNo(string NationalNo)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select * from
            (select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID
            )R1
            where R1.NationalNo like '%'+@NationalNo+'%'";
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

        public static DataTable GetAllByLicenseID(int LicenseID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select * from
            (select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID
            )R1
            where R1.LID=@LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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

        public static DataTable GetByDetainLicenseID(int DetainLicenseID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select * from
            (select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID
            )R1
            where R1.DLID=@DetainLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainLicenseID", DetainLicenseID);
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

        public static DataTable GetByApplicationID(int ApplicationID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select * from
            (select DetainedLicenses.DetainID as DLID, 
            DetainedLicenses.LicenseID as LID,
            DetainedLicenses.DetainDate as DDate,
            DetainedLicenses.IsReleased,
            DetainedLicenses.FineFees,
            DetainedLicenses.ReleaseDate,
            People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName,
            People.NationalNo,
            DetainedLicenses.ReleaseApplicationID as ApplicationID
            from DetainedLicenses
            inner join Licenses
            on
            DetainedLicenses.LicenseID=Licenses.LicenseID
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join People
            on
            Applications.ApplicantPersonID=People.PersonID
            )R1
            where R1.ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
