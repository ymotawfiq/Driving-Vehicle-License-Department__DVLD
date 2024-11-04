using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsInternationalDrivingLicense
    {

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        public enum enMode { AddNew, Update}
        public enMode Mode { get; set; }

        public clsInternationalDrivingLicense()
        {
            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = true;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsInternationalDrivingLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
        }

        private bool _AddNew(int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int licenceID = clsInternationalDrivingLicenseAccessData.AddNew(ApplicationID,
                DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            if (licenceID != -1)
            {
                this.InternationalLicenseID = licenceID;
                return true;
            }
            return false;
        }

        private bool _Update(int licenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            bool updated = clsInternationalDrivingLicenseAccessData.UpdateByID(licenseID, ApplicationID,
                DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            if (updated)
            {
                return true;
            }
            return false;
        }

        public bool Save()
        {
            if(Mode == enMode.AddNew)
            {
                if(_AddNew(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate,
                    ExpirationDate, IsActive, CreatedByUserID))
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }
            else
            {
                if (_Update(InternationalLicenseID, ApplicationID, DriverID, 
                    IssuedUsingLocalLicenseID, IssueDate,
                    ExpirationDate, IsActive, CreatedByUserID))
                {
                    return true;
                }
                return false;
            }
        }

        public static bool ExistsByID(int id)
        {
            return clsInternationalDrivingLicenseAccessData.ExistsByID(id);
        }

        public static bool ExistsByLicenseID(int licenseID)
        {
            return clsInternationalDrivingLicenseAccessData.ExistsByLicenseID(licenseID);
        }

        public static bool DeleteByID(int id)
        {
            return clsInternationalDrivingLicenseAccessData.DeleteByID(id);
        }

        public static clsInternationalDrivingLicense FindByID(int id)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;
            if (clsInternationalDrivingLicenseAccessData.ExistsByID(id))
            {
                clsInternationalDrivingLicenseAccessData.FindByID(id, 
                    ref ApplicationID, ref DriverID,
                    ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive,
                    ref CreatedByUserID);
                return new clsInternationalDrivingLicense(id, ApplicationID, DriverID,
                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            return null;
        }

        public static clsInternationalDrivingLicense FindByLicenseID(int licenseID)
        {
            int InternationalLicenseID = -1;
            int ApplicationID = -1;
            int DriverID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;
            if (clsInternationalDrivingLicenseAccessData.ExistsByLicenseID(licenseID))
            {
                clsInternationalDrivingLicenseAccessData.FindByLicenseID(licenseID,
                    ref InternationalLicenseID,
                    ref ApplicationID, ref DriverID, ref IssueDate, ref ExpirationDate, ref IsActive,
                    ref CreatedByUserID);
                return new clsInternationalDrivingLicense(InternationalLicenseID, ApplicationID, DriverID,
                    licenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            return null;
        }

        public static DataTable GetAllByPersonID(int personID)
        {
            return clsInternationalDrivingLicenseAccessData.GetAllByPersonID(personID);
        }

        public static DataTable GetAll()
        {
            return clsInternationalDrivingLicenseAccessData.GetAll();
        }

    }
}
