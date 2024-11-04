using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsUser
    {

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            IsActive = false;
            UserName = "";
            Password = "";
            Mode = enMode.AddNew;
        }

        public enum enMode { AddNew, Update}
        public enMode Mode;

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
        }


        private bool _AddNewUser()
        {
            int userID = -1;
            bool isAdded = clsUserAccessData.AddNewUser(PersonID, UserName, Password, IsActive, ref userID);
            UserID = userID;
            return isAdded;
        }

        private bool _UpdateUser()
        {
            return clsUserAccessData.UpdateUser(UserName, Password, IsActive, UserID);
        }

        public static bool UpdateUser(string UserName, string Password, bool IsActive, int UserID)
        {
            return clsUserAccessData.UpdateUser(UserName, Password, IsActive, UserID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (_AddNewUser())
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }
            return _UpdateUser();
        }


        public static clsUser CheckLogin(string userName, string password)
        {
            int personID = 0, userID = 0;
            bool isActive = false;
            bool isLoginSuccess = clsUserAccessData.CheckLoginInfo(userName, password, ref userID, 
                ref personID, ref isActive);
            if (isLoginSuccess) return new clsUser(userID, personID, userName, password, isActive);
            return null;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserAccessData.GetAllUsers();
        }

        public static DataTable GetAllUsersByName(string name)
        {
            return clsUserAccessData.GetAllUsersByName(name);
        }

        public static DataTable GetAllUsersByUserName(string userName)
        {
            return clsUserAccessData.GetAllUsersByUserName(userName);
        }

        public static DataTable GetAllActiveUsers()
        {
            return clsUserAccessData.GetAllActiveUsers();
        }

        public static DataTable GetAllNonActiveUsers()
        {
            return clsUserAccessData.GetAllNonActiveUsers();
        }

        public static DataTable FindUserByID(int userID)
        {
            return clsUserAccessData.FindUserByID(userID);
        }

        public static DataTable FindUsersByPersonID(int personID)
        {
            return clsUserAccessData.FindUsersByPersonID(personID);
        }

        public static bool ExistsByPersonID(int PersonID)
        {
            return clsUserAccessData.ExistsByPersonID(PersonID);
        }

        public static bool ExistsByID(int UserID)
        {
            return clsUserAccessData.ExistsByID(UserID);
        }
        public static bool ExistsByUserName(string userName)
        {
            return clsUserAccessData.ExistsByUserName(userName);
        }

        public static clsUser FindByID(int userID)
        {
            int personID = 0;
            string userName = "", password = "";
            bool isActive = false;
            bool isFound = clsUserAccessData.FindUserByID(userID, ref personID, ref userName, ref password,
                ref isActive);
            if (isFound)
            {
                return new clsUser(userID, personID, userName, password, isActive);
            }
            return null;
        }

        public static bool DeleteByID(int userID)
        {
            return clsUserAccessData.DeleteUserByID(userID);
        }

    }
}
