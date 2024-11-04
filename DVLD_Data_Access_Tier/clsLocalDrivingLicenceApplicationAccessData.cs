using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_Data_Access_Tier
{
    public class clsLocalDrivingLicenceApplicationAccessData
    {

        public static int AddLocalDrivingLicenceApplication(int ApplicationID, int LicenceClassID)
        {
            int id = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"insert into LocalDrivingLicenseApplications(ApplicationID, LicenseClassID)
            values(@ApplicationID, @LicenceClassID)
            select SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenceClassID", LicenceClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int x))
                {
                    id = x;
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
            return id;
        }

        public static int UpdateLocalDrivingLicenceApplication(int LocalDrivingLicenseApplicationID,
            int LicenceClassID)
        {
            int id = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"update LocalDrivingLicenseApplications set
            LicenceClassID=@LicenceClassID 
            where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenceClassID", LicenceClassID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",
                LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int x))
                {
                    id = x;
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
            return id;
        }

        public static DataTable GetAllLocalDrivingLicenceApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
				select * from
                (select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDLAppID, 
                ApplicationTypes.ApplicationTypeTitle,ApplicationTypes.ApplicationTypeID,
                LicenseClasses.ClassName as 'ClassName',
                People.NationalNo,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as 'FullName', 
                Applications.ApplicationDate,  
                (select count(distinct TestAppointments.TestTypeID)
                from TestAppointments where TestAppointments.LocalDrivingLicenseApplicationID 
                =LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID 
                and 
                TestAppointments.IsLocked=1) as PassedTests,
                CASE 
                    when Applications.ApplicationStatus=1 then 'New'
                    when Applications.ApplicationStatus=2 then 'Canceled'
                    else 'Completed'
                END as 'Status'
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join ApplicationTypes
                on
                ApplicationTypes.ApplicationTypeID=Applications.ApplicationTypeID
                inner join People
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LicenseClasses
                on
                LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID)R1
                where R1.ApplicationTypeID=1";
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

        public static DataTable GetAllLocalDrivingLicenceApplicationsByFullName(string FullName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from
                (select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDLAppID, 
                LicenseClasses.ClassName as 'ClassName',
                People.NationalNo,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as 'FullName', 
                Applications.ApplicationDate,  
                CASE 
	                when Applications.ApplicationStatus=1 then '0'
	                when Applications.ApplicationStatus=2 then '0'
	                else '3'
                END
                 as PassedTests,
                CASE 
	                when Applications.ApplicationStatus=1 then 'New'
	                when Applications.ApplicationStatus=2 then 'Canceled'
	                else 'Completed'
                END as 'Status'
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join People
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LicenseClasses
                on
                LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID)
                R1
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

        public static DataTable GetAllLocalDrivingLicenceApplicationsByNationalNo(string NationalNo)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from
                (select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDLAppID, 
                LicenseClasses.ClassName as 'ClassName',
                People.NationalNo,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as 'FullName', 
                Applications.ApplicationDate,  
                CASE 
	                when Applications.ApplicationStatus=1 then '0'
	                when Applications.ApplicationStatus=2 then '0'
	                else '3'
                END
                 as PassedTests,
                CASE 
	                when Applications.ApplicationStatus=1 then 'New'
	                when Applications.ApplicationStatus=2 then 'Canceled'
	                else 'Completed'
                END as 'Status'
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join People
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LicenseClasses
                on
                LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID)
                R1
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

        public static DataTable GetAllLocalDrivingLicenceApplicationsByApplicationID(int ApplicationID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from
                (select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDLAppID, 
                LicenseClasses.ClassName as 'ClassName',
                People.NationalNo,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as 'FullName', 
                Applications.ApplicationDate,  
                CASE 
	                when Applications.ApplicationStatus=1 then '0'
	                when Applications.ApplicationStatus=2 then '0'
	                else '3'
                END
                 as PassedTests,
                CASE 
	                when Applications.ApplicationStatus=1 then 'New'
	                when Applications.ApplicationStatus=2 then 'Canceled'
	                else 'Completed'
                END as 'Status'
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join People
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LicenseClasses
                on
                LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID)
                R1
                where R1.[L.D.L.AppID] like '%'+@ApplicationID+'%'";
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

        public static DataTable GetAllNewLocalDrivingLicenceApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from
                (select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDLAppID, 
                LicenseClasses.ClassName as 'ClassName',
                People.NationalNo,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as 'FullName', 
                Applications.ApplicationDate,  
                CASE 
	                when Applications.ApplicationStatus=1 then '0'
	                when Applications.ApplicationStatus=2 then '0'
	                else '3'
                END
                 as PassedTests,
                CASE 
	                when Applications.ApplicationStatus=1 then 'New'
	                when Applications.ApplicationStatus=2 then 'Canceled'
	                else 'Completed'
                END as 'Status'
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join People
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LicenseClasses
                on
                LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID)
                R1
                where R1.Status='New'";
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

        public static DataTable GetAllCanceledLocalDrivingLicenceApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from
                (select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDLAppID, 
                LicenseClasses.ClassName as 'ClassName',
                People.NationalNo,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as 'FullName', 
                Applications.ApplicationDate,  
                CASE 
	                when Applications.ApplicationStatus=1 then '0'
	                when Applications.ApplicationStatus=2 then '0'
	                else '3'
                END
                 as PassedTests,
                CASE 
	                when Applications.ApplicationStatus=1 then 'New'
	                when Applications.ApplicationStatus=2 then 'Canceled'
	                else 'Completed'
                END as 'Status'
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join People
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LicenseClasses
                on
                LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID)
                R1
                where R1.Status='Canceled'";
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

        public static DataTable GetAllCompletedLocalDrivingLicenceApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from
                (select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDLAppID, 
                LicenseClasses.ClassName as 'ClassName',
                People.NationalNo,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as 'FullName', 
                Applications.ApplicationDate,  
                CASE 
	                when Applications.ApplicationStatus=1 then '0'
	                when Applications.ApplicationStatus=2 then '0'
	                else '3'
                END
                 as PassedTests,
                CASE 
	                when Applications.ApplicationStatus=1 then 'New'
	                when Applications.ApplicationStatus=2 then 'Canceled'
	                else 'Completed'
                END as 'Status'
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join People
                on
                Applications.ApplicantPersonID=People.PersonID
                inner join LicenseClasses
                on
                LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID)
                R1
                where R1.Status='Completed'";
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


        public static bool ExistsByID(int id)
        {
            bool found = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found=1 from LocalDrivingLicenseApplications 
            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int x))
                {
                    found = true;
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
            return found;
        }

        public static bool FindByID(int id, ref int ApplicationID, ref int LicenseClassID)
        {
            bool found = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from LocalDrivingLicenseApplications 
            where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    found = true;
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
            return found;
        }

        public static bool FindByApplicationID(int ApplicationID, ref int id, ref int LicenseClassID)
        {
            bool found = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from LocalDrivingLicenseApplications 
            where LocalDrivingLicenseApplications.ApplicationID=@ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    found = true;
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
            return found;
        }

        public static bool FindApplicationInfoForCtrlBasicInfo(int ldlAppID, ref int ApplicationID, 
            ref string ApplicationPersonName,
            ref string ApplicationType,
            ref string ApplicationStatus, ref double PaidFees, ref DateTime ApplicationDate,
            ref DateTime LastStatusDate, ref string CreatedByUserName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1
                Applications.ApplicationID,
                Applications.PaidFees,ApplicationTypes.ApplicationTypeTitle as Type,
                People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName as Applicant,
                Users.UserName as CreatedBy,
                Applications.ApplicationDate,
                Applications.LastStatusDate,
                CASE 
	                when Applications.ApplicationStatus=1 then 'New'
	                when Applications.ApplicationStatus=2 then 'Canceled'
	                else 'Completed'
                END
                as Status
                from LocalDrivingLicenseApplications
                inner join Applications
                on
                LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                inner join ApplicationTypes
                on
                Applications.ApplicationTypeID=1
                inner join People
                on 
                Applications.ApplicantPersonID=People.PersonID
                inner join Users
                on
                Applications.CreatedByUserID=Users.UserID
                where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                =@LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", ldlAppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationPersonName = reader["Applicant"].ToString();
                    ApplicationType = reader["Type"].ToString();
                    CreatedByUserName = reader["CreatedBy"].ToString();
                    ApplicationStatus = reader["Status"].ToString();
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
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


        public static byte GetApplicationStatusByLDLAppID(int ldlAppID)
        {
            byte status = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select Applications.ApplicationStatus 
                from Applications
                where
                Applications.ApplicationID = (
                select top 1 LocalDrivingLicenseApplications.ApplicationID 
                from LocalDrivingLicenseApplications
                where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@ldlAppID)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int x))
                {
                    status = Convert.ToByte(x);
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
            return status;
        }


        public static bool GetDataForLocalDrivingLicenseInfoControl(int ldlAppID, ref string className,
            ref byte passedTests)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID,
                LicenseClasses.ClassName,
                (select count(distinct TestAppointments.TestTypeID) from TestAppointments
                where TestAppointments.LocalDrivingLicenseApplicationID=@ldlAppID) as PassedTests
                from LocalDrivingLicenseApplications
                inner join LicenseClasses
                on
                LocalDrivingLicenseApplications.LicenseClassID=LicenseClasses.LicenseClassID
                inner join TestAppointments
                on
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@ldlAppID
                group by 
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                ,
                LicenseClasses.ClassName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    className = reader["ClassName"].ToString();
                    passedTests = Convert.ToByte(reader["PassedTests"]);
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
            return isFound;
        }


        public static int GetCountOfPassedTestsByLocalDrivingLicense(int ldlAppID)
        {
            int countPassedTests = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PassedTests from
                (
                select count(Tests.TestResult) as PassedTests,
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                from Tests
                inner join TestAppointments
                on 
                Tests.TestAppointmentID=TestAppointments.TestAppointmentID
                inner join LocalDrivingLicenseApplications
                on
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                =TestAppointments.LocalDrivingLicenseApplicationID
                where Tests.TestResult=1
                group by LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                )R1
                where R1.LocalDrivingLicenseApplicationID=@ldlAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int passedTests))
                {
                    countPassedTests = passedTests;
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
            return countPassedTests;
        }

        public static int GetCountOfTrialsByLocalDrivingLicenseAndTestType(int ldlAppID, int testTypeID)
        {
            int countPassedTests = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select count(TestAppointments.TestTypeID)
                from TestAppointments
                where 
                TestAppointments.TestTypeID=@TestTypeID
                and
                TestAppointments.LocalDrivingLicenseApplicationID=@ldlAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int passedTests))
                {
                    countPassedTests = passedTests;
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
            return countPassedTests;
        }

        public static int GetLDLAppIDByLicenseID(int LicenseID)
        {
            int countPassedTests = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
            from Licenses
            inner join Applications
            on
            Licenses.ApplicationID=Applications.ApplicationID
            inner join LocalDrivingLicenseApplications
            on
            LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
            where Licenses.LicenseID=@LicenseID
            group by LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int passedTests))
                {
                    countPassedTests = passedTests;
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
            return countPassedTests;
        }

        public static bool DeleteByLocalDrivingLicenseApplicationID(int ldlAppID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                delete from LocalDrivingLicenseApplications
                where
                LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@ldlAppID
                ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
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
