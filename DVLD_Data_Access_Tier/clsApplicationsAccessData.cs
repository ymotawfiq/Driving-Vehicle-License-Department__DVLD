

using System;
using System.Data.SqlClient;

namespace DVLD_Data_Access_Tier
{
    public class clsApplicationsAccessData
    {

        public static int AddNewApplication(int PersonID, int AppTypeID, int AppStatus, double PaidFees,
            int UserID)
        {
            int appID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"insert into Applications(ApplicantPersonID,ApplicationDate,ApplicationTypeID,
            ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
            values(@PersonID,GETDATE(),@AppTypeID,@AppStatus,GETDATE(),@PaidFees,@UserID);
            select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@AppTypeID", AppTypeID);
            command.Parameters.AddWithValue("@AppStatus", AppStatus);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int x))
                {
                    appID = x;
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
            return appID;
        }

        public static bool UpdateApplication(int ApplicationID, DateTime ApplicationDate,
            int PersonID, int AppTypeID, int AppStatus, double PaidFees, int UserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                UPDATE [dbo].[Applications]
               SET [ApplicantPersonID] = @ApplicantPersonID
                  ,[ApplicationDate] = @ApplicationDate
                  ,[ApplicationTypeID] = @ApplicationTypeID
                  ,[ApplicationStatus] = @ApplicationStatus
                  ,[LastStatusDate] = @LastStatusDate
                  ,[PaidFees] = @PaidFees
                  ,[CreatedByUserID] = @CreatedByUserID
             WHERE ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", AppTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", AppStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", UserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            
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

        public static bool CancelApplication(int AppID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"update Applications set ApplicationStatus=2,
            LastStatusDate=GETDATE() where ApplicationID=@AppID and ApplicationStatus=1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
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

        public static bool DeleteApplication(int AppID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"delete from Applications where ApplicationID=@AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
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

        public static bool ExistsByApplicationTypeAndIsNewApplication(int personID, int licenceClassID)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select found=1 from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
                and
                LocalDrivingLicenseApplications.LicenseClassID=@licenceClassID
                and
                Applications.ApplicationStatus = 2
                inner join People
                on Applications.ApplicantPersonID=People.PersonID
                and People.PersonID=@personID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", personID);
            command.Parameters.AddWithValue("@licenceClassID", licenceClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int x))
                {
                    isExists = true;
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
            return isExists;
        }

        public static bool IsPersonHasCompletetOrActiveApplicationFromTypeAndLicenceClass(int personID, 
            int ApplicationTypeID, int LicenseClassID)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 found=1
                from LocalDrivingLicenseApplications
                inner join
                Applications
                on LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
                and LocalDrivingLicenseApplications.LicenseClassID=@LicenseClassID
                where
                (Applications.ApplicationID=(select top 1 Applications.ApplicationID from Applications 
                where Applications.ApplicantPersonID=@ApplicantPersonID
                order by Applications.ApplicationID desc))
                or
                ( 
                Applications.ApplicantPersonID=@ApplicantPersonID
                and
                Applications.ApplicationTypeID=@ApplicationTypeID
                and Applications.ApplicationStatus=3
                )";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", personID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int x))
                {
                    isExists = true;
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
            return isExists;
        }


        public static bool ExistsByID(int AppID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found=1 from Applications where ApplicationID=@AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
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

        public static bool FindByID(int AppID, ref int ApplicantPersonID, ref int ApplicationTypeID,
            ref int ApplicationStatus, ref double PaidFees, ref DateTime ApplicationDate,
            ref DateTime LastStatusDate, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from Applications where ApplicationID=@AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
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
