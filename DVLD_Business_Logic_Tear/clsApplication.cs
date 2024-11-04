using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsApplication
    {

        public enum enMode
        {
            AddNew, Update
        }

        public enMode Mode { get; set; }

        public int ApplicationID { get; set; }
        public int CreatedByUserID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ApplicationStatus { get; set; }
        public double PaidFees { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            CreatedByUserID = -1;
            ApplicantPersonID = -1;
            ApplicationTypeID = -1;
            ApplicationStatus = -1;
            PaidFees = 0d;
            ApplicationDate = DateTime.MinValue;
            LastStatusDate = DateTime.MinValue;
        }

        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate,
            int CreatedByUserID, double PaidFees)
        {
            this.ApplicationDate = ApplicationDate;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.CreatedByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            Mode = enMode.AddNew;
        }


        private int AddNewApplication(int ApplicantPersonID,
            int ApplicationTypeID, int ApplicationStatus, double paidFees, int userID)
        {
            int appID = clsApplicationsAccessData.AddNewApplication(ApplicantPersonID, ApplicationTypeID,
                ApplicationStatus, paidFees, userID);
            return appID;
        }

        private bool UpdateApplication(int ApplicationID, DateTime ApplicationDate, int ApplicantPersonID,
            int ApplicationTypeID, int ApplicationStatus, double paidFees, int userID)
        {
            return clsApplicationsAccessData.UpdateApplication(ApplicationID, ApplicationDate, ApplicantPersonID,
                ApplicationTypeID, ApplicationStatus, paidFees, userID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                int appID = AddNewApplication(ApplicantPersonID, ApplicationTypeID, ApplicationStatus,
                    PaidFees, CreatedByUserID);
                if (appID != -1)
                {
                    Mode = enMode.Update;
                    ApplicationID = appID;
                    return true;
                }
                return false;
            }
            return UpdateApplication(ApplicationID, ApplicationDate, ApplicantPersonID, ApplicationTypeID, 
                ApplicationStatus, PaidFees, CreatedByUserID);
        }

        public static bool DeleteApplication(int appID)
        {
            return clsApplicationsAccessData.DeleteApplication(appID);
        }

        public static bool ExistsByPersonAndStatus(int personID, int licenceClassID)
        {
            return clsApplicationsAccessData.ExistsByApplicationTypeAndIsNewApplication(personID, licenceClassID);
        }

        public static bool CancelApplication(int appID)
        {
            return clsApplicationsAccessData.CancelApplication(appID);
        }


        public static bool ExistsByID(int appID)
        {
            return clsApplicationsAccessData.ExistsByID(appID);
        }

        public static bool IsPersonHasCompletetOrActiveApplicationFromType(int personID, 
            int appTypeID, int LicenseClassID)
        {
            return clsApplicationsAccessData
                .IsPersonHasCompletetOrActiveApplicationFromTypeAndLicenceClass(personID, 
                appTypeID, LicenseClassID);
        }

        public static clsApplication FindByID(int id)
        {
            int appStatus = 0, appTypeID = 0, userID = 0, appPersonID = 0;
            double paidFees = 0d;
            DateTime appDate = DateTime.Now, lastStatusDate = DateTime.Now;
            if (clsApplicationsAccessData.ExistsByID(id))
            {
                clsApplicationsAccessData.FindByID(id, ref appPersonID, ref appTypeID, ref appStatus
                    , ref paidFees, ref appDate, ref lastStatusDate, ref userID);
                return new clsApplication(id, appPersonID, appDate, appTypeID, appStatus, 
                    lastStatusDate, userID, paidFees); 
            }
            return null;
        }

    }
}
