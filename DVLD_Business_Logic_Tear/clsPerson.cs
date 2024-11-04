using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{

    public class clsPerson
    {
        public int PersonID { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }

        public string FullName { get 
            {
                return $"{FirstName} {SecondName} {ThirdName} {LastName}";
            } } 

        public enum enMode { AddNew, Update};
        public enMode Mode = enMode.AddNew;
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.DateOfBirth = DateTime.Now;
            this.Email = "";
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.NationalityCountryID = 0;
            this.ImagePath = "";
            Mode = enMode.AddNew;
        }
        private clsPerson(int ID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, short Gendor,
            string Address, string Phone, string Email, int NationalityCountryID,
            string ImagePath)
        {
            this.PersonID = ID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }
        private bool _AddNewPerson()
        {
            int id = clsPersonAccessData.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, 
                LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            if (id != -1) PersonID = id;
            return id != -1;
        }

        private bool _UpdatePerson()
        {
            return clsPersonAccessData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName, ThirdName,
                LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
        }

        public static clsPerson FindByID(int ID)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            short gendor = 0;
            string address = "", phone = "", email = "";
            int nationalityCountryID = 0;
            string imagePath = "";
            bool find = clsPersonAccessData.FindByID(ID, ref nationalNo, ref firstName, ref secondName,
                ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref phone, 
                ref email, ref nationalityCountryID, ref imagePath);
            if (find)
            {
                return new clsPerson(ID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth,
                    gendor, address, phone, email, nationalityCountryID, imagePath);
            }
            return null;
        }

        public static clsPerson FindByNationalNo(string nationalNo)
        {
            string firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            short gendor = 0;
            string address = "", phone = "", email = "";
            int nationalityCountryID = 0, ID = 0;
            string imagePath = "";
            bool find = clsPersonAccessData.FindByNationalNo(nationalNo, ref ID, ref firstName, ref secondName,
                ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref phone,
                ref email, ref nationalityCountryID, ref imagePath);
            if (find)
            {
                return new clsPerson(ID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth,
                    gendor, address, phone, email, nationalityCountryID, imagePath);
            }
            return null;
        }

        public static clsPerson FindByEmail(string email)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            short gendor = 0;
            string address = "", phone = "";
            int nationalityCountryID = 0, ID = 0;
            string imagePath = "";
            bool find = clsPersonAccessData.FindByEmail(email, ref ID, ref firstName, ref secondName,
                ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address, ref phone,
                ref email, ref nationalityCountryID, ref imagePath);
            if (find)
            {
                return new clsPerson(ID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth,
                    gendor, address, phone, email, nationalityCountryID, imagePath);
            }
            return null;
        }

        public static clsPerson FindByPhone(string phone)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            short gendor = 0;
            string address = "", email = "";
            int nationalityCountryID = 0, ID=0;
            string imagePath = "";
            bool find = clsPersonAccessData.FindByPhone(phone, ref ID, ref firstName,
                ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address,
                ref email, ref nationalNo, ref nationalityCountryID, ref imagePath);
            if (find)
            {
                return new clsPerson(ID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth,
                    gendor, address, phone, email, nationalityCountryID, imagePath);
            }
            return null;
        }

        public static clsPerson FindFirstPersonByName(string name)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            short gendor = 0;
            string address = "", email = "", phone="";
            int nationalityCountryID = 0, ID = 0;
            string imagePath = "";
            bool find = clsPersonAccessData.FindFirstPersonByName(name, ref ID, ref firstName,
                ref secondName, ref thirdName,
                ref lastName, ref dateOfBirth, ref gendor, ref address,
                ref email, ref phone, ref nationalNo, ref nationalityCountryID, ref imagePath);
            if (find)
            {
                return new clsPerson(ID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth,
                    gendor, address, phone, email, nationalityCountryID, imagePath);
            }
            return null;
        }

        public static DataTable FindTopUserByName(string name)
        {
            return clsPersonAccessData.FindTopUserWithName(name);
        }

        public static DataTable FindTopUserByEmail(string email)
        {
            return clsPersonAccessData.FindTopUserWithEmail(email);
        }

        public static DataTable FindTopUserByNationalNo(string nationalNo)
        {
            return clsPersonAccessData.FindTopUserWithNationalNo(nationalNo);
        }

        public static DataTable FindTopUserByPhone(string phone)
        {
            return clsPersonAccessData.FindTopUserWithPhone(phone);
        }

        public static DataTable FindTopUserByID(int id)
        {
            return clsPersonAccessData.FindTopUserWithID(id);
        }

        public static DataTable FindAllPersonsByName(string name)
        {
            return clsPersonAccessData.FindAllPersonsByName(name);
        }

        public static DataTable FindAllPersonsByNationality(int nationalityNo)
        {
            return clsPersonAccessData.FindAllPersonsByNationality(nationalityNo);
        }

        public static DataTable FindAllPersonsByNationality(string CountryName)
        {
            return clsPersonAccessData.FindAllPersonsByNationality(CountryName);
        }

        public static DataTable FindAllPersonsByEmail(string email)
        {
            return clsPersonAccessData.FindAllPersonsByEmail(email);
        }

        public static DataTable FindAllPersonsByFirstName(string FirstName)
        {
            return clsPersonAccessData.FindAllPersonsByFirstName(FirstName);
        }
        public static DataTable FindAllPersonsBySecondName(string SecondName)
        {
            return clsPersonAccessData.FindAllPersonsBySecondName(SecondName);
        }
        public static DataTable FindAllPersonsByThirdName(string ThirdName)
        {
            return clsPersonAccessData.FindAllPersonsByThirdName(ThirdName);
        }
        public static DataTable FindAllPersonsByLastName(string LastName)
        {
            return clsPersonAccessData.FindAllPersonsByLastName(LastName);
        }

        public static DataTable FindAllPersonsByGender(int gender)
        {
            return clsPersonAccessData.FindAllPersonsByGender(gender);
        }

        public static bool ExistsByID(int ID)
        {
            return clsPersonAccessData.ExistsByID(ID);
        }
        public static bool ExistsByEmail(string email)
        {
            return clsPersonAccessData.ExistsByEmail(email);
        }

        public static bool ExistsByEmailForFilter(string email)
        {
            return clsPersonAccessData.ExistsByEmailForFilter(email);
        }

        public static bool ExistsByPhone(string phone)
        {
            return clsPersonAccessData.ExistsByPhone(phone);
        }

        public static bool ExistsByNationalNo(string nationalNo)
        {
            return clsPersonAccessData.ExistsByNationalNo(nationalNo);
        }

        public static bool ExistsByNationalNoForPerson(string nationalNo, int personID)
        {
            return clsPersonAccessData.ExistsByNationalNoForPerson(nationalNo, personID);
        }

        public static bool ExistsByPhoneForPerson(string phone, int personID)
        {
            return clsPersonAccessData.ExistsByPhoneForPerson(phone, personID);
        }

        public static bool ExistsByEmailForPerson(string email, int personID)
        {
            return clsPersonAccessData.ExistsByEmailForPerson(email, personID);
        }


        public static bool DeletePerson(int ID)
        {
            return clsPersonAccessData.Delete(ID);
        }

        public static DataTable GetAllPersons()
        {
            return clsPersonAccessData.GetAllPersons();
        }

        public bool Save()
        {
            if(Mode == enMode.AddNew)
            {
                if (_AddNewPerson())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return _UpdatePerson();
            }
        }

        public static int Count()
        {
            return clsPersonAccessData.Count();
        }

        public static DataTable GetAllLicenses(int personID)
        {
            return clsPersonAccessData.GetAllLicensesByPersonID(personID);
        }

        public static DataTable GetAllLocalDrivingLicensesByPersonID(int personID)
        {
            return clsPersonAccessData.GetAllLocalDrivingLicensesByPersonID(personID);
        }

        public static int GetPersonIDByLocalDrivingLicense(int ldlAppID)
        {
            return clsPersonAccessData.GetPersonIDByLocalDrivingLicense(ldlAppID);
        }

        public static int GetPersonIDByLicenseID(int licenseID)
        {
            return clsPersonAccessData.GetPersonIDByLicenseID(licenseID);
        }

        public static int GetPersonIDByInternationalLicenseID(int ilid)
        {
            return clsPersonAccessData.GetPersonIDByInternationalLicenseID(ilid);
        }

    }
}
