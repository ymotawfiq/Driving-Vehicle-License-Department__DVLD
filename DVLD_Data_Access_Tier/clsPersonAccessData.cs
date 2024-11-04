using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Tier
{
    public class clsPersonAccessData
    {
        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, short Gendor,
            string Address, string Phone, string Email, int NationalityCountryID, 
            string ImagePath)
        {
            int id = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"insert into People(NationalNo, FirstName, SecondName, ThirdName, LastName, 
            DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)
            values(@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, 
            @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
            select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "") command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            if (ThirdName != null) command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            if (Email != null) command.Parameters.AddWithValue("@Email", Email);
            else command.Parameters.AddWithValue("@Email", DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result!=null && int.TryParse(result.ToString(), out int personID))
                {
                    id = personID;
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

        public static bool UpdatePerson(int ID, string NationalNo, string FirstName, string SecondName,
        string ThirdName, string LastName, DateTime DateOfBirth, short Gendor,
        string Address, string Phone, string Email, int NationalityCountryID,
        string ImagePath)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"UPDATE [dbo].[People]
                       SET [NationalNo] = @NationalNo
                          ,[FirstName] = @FirstName
                          ,[SecondName] = @SecondName
                          ,[ThirdName] = @ThirdName
                          ,[LastName] = @LastName
                          ,[DateOfBirth] = @DateOfBirth
                          ,[Gendor] = @Gendor
                          ,[Address] = @Address
                          ,[Phone] = @Phone
                          ,[Email] = @Email
                          ,[NationalityCountryID] = @NationalityCountryID
                          ,[ImagePath] = @ImagePath
                           WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "") command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            if (ThirdName != "") command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            if (Email != "") command.Parameters.AddWithValue("@Email", Email);
            else command.Parameters.AddWithValue("@Email", DBNull.Value);

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

        public static bool FindByID(int ID, ref string NationalNo, ref string FirstName, 
            ref string SecondName,
        ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
        ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
        ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from People where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    if (reader["ImagePath"] == DBNull.Value) ImagePath = "";
                    else ImagePath = reader["ImagePath"].ToString();

                    if (reader["Email"] == DBNull.Value) Email = "";
                    else Email = reader["Email"].ToString();

                    if (reader["ThirdName"] == DBNull.Value) ThirdName = "";
                    else ThirdName = reader["ThirdName"].ToString();

                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
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
            return IsFound;
        }

        public static bool FindByNationalNo(string NationalNo, ref int ID, ref string FirstName,
        ref string SecondName,
        ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
        ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
        ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from People where NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    if (reader["ImagePath"] == DBNull.Value) ImagePath = "";
                    else ImagePath = reader["ImagePath"].ToString();

                    if (reader["Email"] == DBNull.Value) Email = "";
                    else Email = reader["Email"].ToString();

                    if (reader["ThirdName"] == DBNull.Value) ThirdName = "";
                    else ThirdName = reader["ThirdName"].ToString();

                    ID = Convert.ToInt32(reader["PersonID"]);
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
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
            return IsFound;
        }

        public static bool FindByEmail(string Email, ref int ID, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
        ref string Address, ref string Phone, ref string NationalNo, ref int NationalityCountryID,
        ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from People where Email=@Email";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    if (reader["ImagePath"] == DBNull.Value) ImagePath = "";
                    else ImagePath = reader["ImagePath"].ToString();

                    if (reader["ThirdName"] == DBNull.Value) ThirdName = "";
                    else ThirdName = reader["ThirdName"].ToString();

                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    ID = Convert.ToInt32(reader["PersonID"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
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
            return IsFound;
        }

        public static bool FindByPhone(string Phone, ref int ID, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
        ref string Address, ref string Email, ref string NationalNo, ref int NationalityCountryID,
        ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from People where Phone=@Phone}";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", Phone);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    if (reader["ImagePath"] == DBNull.Value) ImagePath = "";
                    else ImagePath = reader["ImagePath"].ToString();

                    if (reader["ThirdName"] == DBNull.Value) ThirdName = "";
                    else ThirdName = reader["ThirdName"].ToString();

                    if (reader["Email"] == DBNull.Value) Email = "";
                    else Email = reader["Email"].ToString();

                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    Address = reader["Address"].ToString();
                    ID = Convert.ToInt32(reader["PersonID"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
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
            return IsFound;
        }

        public static bool FindFirstPersonByName(string Name, ref int ID, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
        ref string Address, ref string Email, ref string Phone, ref string NationalNo, ref int NationalityCountryID,
        ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select top 1 * from People where FirstName like '%'+@Name+'%' or 
            SecondName like '%'+@Name+'%' or ThirdName like '%'+@Name+'%'
            or LastName like '%'+@Name+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", Name);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;

                    if (reader["ImagePath"] == DBNull.Value) ImagePath = "";
                    else ImagePath = reader["ImagePath"].ToString();

                    if (reader["ThirdName"] == DBNull.Value) ThirdName = "";
                    else ThirdName = reader["ThirdName"].ToString();

                    if (reader["Email"] == DBNull.Value) Email = "";
                    else Email = reader["Email"].ToString();

                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    Address = reader["Address"].ToString();
                    ID = Convert.ToInt32(reader["PersonID"]);
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
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
            return IsFound;
        }

        public static DataTable FindTopUserWithName(string FirstName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where FirstName + SecondName + ThirdName + LastName
            like '%'+@FirstName+'%'";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
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

        public static DataTable FindTopUserWithEmail(string Email)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where Email like '%'+@Email+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email);
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

        public static DataTable FindTopUserWithPhone(string Phone)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where Phone like '%'+@Phone+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", Phone);
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

        public static DataTable FindTopUserWithID(int PersonID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where PersonID = @PersonID";
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

        public static DataTable FindTopUserWithNationalNo(string NationalNo)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select top 1 PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where NationalNo like '%'+@NationalNo+'%'";
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

        public static DataTable GetAllPersons()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People";
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

        public static DataTable FindAllPersonsByEmail(string Email)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select  PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where Email like '%'+@Email+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email);
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

        public static DataTable FindAllPersonsByName(string Name)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where FirstName + SecondName + ThirdName + LastName 
                like '%'+@Name+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", Name);
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

        public static DataTable FindAllPersonsByFirstName(string FirstName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where FirstName like '%'+@FirstName+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
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

        public static DataTable FindAllPersonsBySecondName(string SecondName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where SecondName like '%'+@SecondName+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SecondName", SecondName);
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

        public static DataTable FindAllPersonsByThirdName(string ThirdName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where ThirdName like '%'+@ThirdName+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
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

        public static DataTable FindAllPersonsByLastName(string LastName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where LastName like '%'+@LastName+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LastName", LastName);
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

        public static DataTable FindAllPersonsByNationality(int NationalityCountryID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where NationalityCountryID=@NationalityCountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
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

        public static DataTable FindAllPersonsByGender(int Gendor)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
	                when Gendor = 1 then 'Female'
	                else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where Gendor=@Gendor";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Gendor", Gendor);
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

        public static DataTable FindAllPersonsByNationality(string CountryName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select PersonID, FirstName, SecondName, ThirdName, LastName,

                CASE 
                    when Gendor = 1 then 'Female'
                    else 'Male'
                END
                as Gendor, DateOfBirth, 
                (select CountryName from Countries where CountryID = People.NationalityCountryID)
                as Nationality, Phone, Email

                from People where NationalityCountryID=(select top 1 CountryID from Countries 
                where CountryName like '%'+@CountryName+'%')";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
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

        public static bool ExistsByID(int ID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found = 1 from People where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool ExistsByNationalNo(string NationalNo)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found = 1 from People where NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool ExistsByNationalNoForPerson(string NationalNo, int personID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found=1 from People where NationalNo=@NationalNo and PersonID=@personID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@personID", personID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool ExistsByEmail(string Email)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found = 1 from People where Email=@Email";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool ExistsByEmailForFilter(string Email)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found = 1 from People where Email like '%'+@Email+'%'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool ExistsByEmailForPerson(string email, int personID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found=1 from People where Email=@email and PersonID=@personID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@personID", personID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool ExistsByPhone(string Phone)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found = 1 from People where Phone=@Phone";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", Phone);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool ExistsByPhoneForPerson(string Phone, int personID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found = 1 from People where Phone=@Phone and PersonID=@personID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@personID", personID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PersonID))
                {
                    IsFound = true;
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
            return IsFound;
        }

        public static bool Delete(int ID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"delete from People where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);
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

        public static int Count()
        {
            int count = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select count(*) from People";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int rows))
                {
                    count = rows;
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
            return count;
        }

        public static int GetPersonIDByLocalDrivingLicense(int ldlAppID)
        {
            int PersonID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select Applications.ApplicantPersonID
                from Applications
                inner join LocalDrivingLicenseApplications
                on
                LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
                where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@ldlAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ldlAppID", ldlAppID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    PersonID = id;
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
            return PersonID;
        }

        public static int GetPersonIDByLicenseID(int LicenseID)
        {
            int PersonID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select Drivers.PersonID 
            from Drivers
            inner join Licenses
            on
            Licenses.DriverID=Drivers.DriverID
            where Licenses.LicenseID=@LicenseID
            group by Drivers.PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    PersonID = id;
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
            return PersonID;
        }

        public static int GetPersonIDByInternationalLicenseID(int InternationalLicenseID)
        {
            int PersonID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
            select Drivers.PersonID
            from Drivers
            inner join InternationalLicenses
            on
            InternationalLicenses.DriverID=Drivers.DriverID
            where InternationalLicenses.InternationalLicenseID=@InternationalLicenseID
            group by Drivers.PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    PersonID = id;
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
            return PersonID;
        }

        public static DataTable GetAllLicensesByPersonID(int PersonID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select 
                Licenses.LicenseID, 
                Licenses.ApplicationID,
                Licenses.LicenseClass,
                Licenses.IssueDate,
                Licenses.ExpirationDate,
                Licenses.IsActive
                from Licenses 
                where DriverID=(
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

        public static DataTable GetAllLocalDrivingLicensesByPersonID(int PersonID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"
                select * from
                (select distinct
                Licenses.LicenseID, 
                Licenses.ApplicationID,
                Licenses.LicenseClass,
                Licenses.IssueDate,
                Licenses.ExpirationDate,
                Licenses.IsActive,
                Licenses.DriverID
                from Licenses 
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
