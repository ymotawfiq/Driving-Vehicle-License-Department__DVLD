using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Tier
{
    public class clsUserAccessData
    {

        public static bool AddNewUser(int personID, string userName, string password, bool isActive, 
            ref int userID)
        {
            bool isAdded = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"insert into Users(PersonID,UserName,Password,IsActive)
            values(@PersonID,@UserName,@Password,@IsActive); select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result !=null && int.TryParse(result.ToString(), out int id))
                {
                    userID = id;
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

        public static bool UpdateUser(string userName, string password, bool isActive, int userID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"update Users set UserName=@UserName, Password=@Password,
            IsActive=@IsActive where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);
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

        public static bool CheckActiveLoginInfo(string UserName, string Password, ref int UserID, 
            ref int PersonID, ref bool IsActive)
        {
            bool success = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select top 1 * from Users where UserName=@UserName and Password=@Password
            and IsActive=1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                success = reader.HasRows;
                if (reader.Read())
                {
                    success = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    IsActive = (bool)reader["IsActive"];
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
            return success;
        }

        public static bool CheckLoginInfo(string UserName, string Password, ref int UserID, ref int PersonID,
            ref bool IsActive)
        {
            bool success = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select top 1 * from Users where UserName=@UserName and Password=@Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                success = reader.HasRows;
                if (reader.Read())
                {
                    success = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    IsActive = (bool)reader["IsActive"];
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
            return success;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select Users.UserID, Users.PersonID, People.FirstName + ' ' 
                            + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName as FullName, Users.UserName, Users.IsActive
                            from Users
                            inner join People
                            on
                            Users.PersonID = People.PersonID";
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

        public static DataTable FindUsersByPersonID(int PersonID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select Users.UserID, Users.PersonID, People.FirstName + ' ' 
                            + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName as FullName, Users.UserName, Users.IsActive
                            from Users
                            inner join People
                            on
                            Users.PersonID = People.PersonID and
                            Users.PersonID = @PersonID";
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

        public static DataTable FindUserByID(int UserID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select Users.UserID, Users.PersonID, People.FirstName + ' ' 
                            + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName as FullName, Users.UserName, Users.IsActive
                            from Users
                            inner join People
                            on
                            Users.PersonID = People.PersonID and
                            Users.UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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

        public static bool FindUserByID(int UserID, ref int PersonID, ref string UserName,
            ref string Password, ref bool IsActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select * from Users where
                            UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = (bool)reader["IsActive"];
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

        public static bool DeleteUserByID(int UserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"delete from Users where
                            UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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

        public static DataTable GetAllUsersByName(string Name)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select Users.UserID, Users.PersonID, People.FirstName + ' ' 
                            + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName as FullName, Users.UserName, Users.IsActive
                            from Users
                            inner join People
                            on
                            Users.PersonID = People.PersonID and
                            (People.FirstName like '%'+@Name+'%' or
							People.SecondName like '%'+@Name+'%' or
							People.ThirdName like '%'+@Name+'%' or
							People.LastName like '%'+@Name+'%' or
							People.FirstName + '' + People.LastName + ' '+
							People.ThirdName + ' ' + People.LastName like '%'+@Name+'%')";
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

        public static DataTable GetAllActiveUsers()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select Users.UserID, Users.PersonID, People.FirstName + ' ' 
                            + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName as FullName, Users.UserName, Users.IsActive
                            from Users
                            inner join People
                            on
                            Users.PersonID = People.PersonID and
                            (Users.IsActive = 1)";
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

        public static DataTable GetAllNonActiveUsers()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select Users.UserID, Users.PersonID, People.FirstName + ' ' 
                            + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName as FullName, Users.UserName, Users.IsActive
                            from Users
                            inner join People
                            on
                            Users.PersonID = People.PersonID and
                            (Users.IsActive = 0)";
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

        public static DataTable GetAllUsersByUserName(string UserName)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select Users.UserID, Users.PersonID, People.FirstName + ' ' 
                            + People.SecondName + ' ' + 
                            People.ThirdName + ' ' + People.LastName as FullName, Users.UserName, Users.IsActive
                            from Users
                            inner join People
                            on
                            Users.PersonID = People.PersonID and
                            (Users.UserName like '%'+@UserName+'%')";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
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

        public static bool ExistsByID(int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found=1 from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
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

        public static bool ExistsByPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found=1 from Users where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
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

        public static bool ExistsByUserName(string userName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessTierSettings.ConnectionString);
            string query = @"select found=1 from Users where UserName=@userName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userName", userName);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
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
