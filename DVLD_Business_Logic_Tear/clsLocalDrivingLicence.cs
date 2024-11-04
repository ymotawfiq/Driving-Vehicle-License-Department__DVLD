using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using static DVLD_Business_Logic_Tear.clsApplication;


namespace DVLD_Business_Logic_Tear
{
    public class clsLocalDrivingLicence
    {

        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public int LocalDrivingLicenseID { get; set; }

        public struct stApplicationBasicInfoCtrl
        {
            public int LocalDrivingLicenseApplicationID { get; set; }
            public int ApplicationID { get; set; }
            public string CreatedByUserName { get; set; }
            public string ApplicationPersonName { get; set; }
            public string ApplicationType { get; set; }
            public string ApplicationStatus { get; set; }
            public double PaidFees { get; set; }
            public DateTime ApplicationDate { get; set; }
            public DateTime LastStatusDate { get; set; }
        }

        public struct stDrivingLicenseCtrlInfo
        {
            public int LocalDrivingLicenseAppID { get; set; }
            public byte PassedTests { get; set; }
            public string ClassName { get; set; } 
        }

        public clsLocalDrivingLicence()
        {
            ApplicationID = -1;
            LicenseClassID = -1;
            LocalDrivingLicenseID = -1;
        }

        private clsLocalDrivingLicence(int LocalDrivingLicenseID, int ApplicationID, int LicenseClassID)
        {
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LocalDrivingLicenseID = LocalDrivingLicenseID;
        }

        public static clsLocalDrivingLicence AddNewLocalDrivingLicence(int appID, int licenseClassID)
        {
            int localDrivingLicenseID = clsLocalDrivingLicenceApplicationAccessData
                .AddLocalDrivingLicenceApplication(appID, licenseClassID);
            if (localDrivingLicenseID != -1)
            {
                return new clsLocalDrivingLicence(localDrivingLicenseID, appID, licenseClassID);
            }
            return null;
        }

        public static DataTable GetAllLocalDrivingLicences()
        {
            return clsLocalDrivingLicenceApplicationAccessData.GetAllLocalDrivingLicenceApplications();
        }

        public static DataTable GetAllLocalDrivingLicencesByNationalNo(string NationalNo)
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetAllLocalDrivingLicenceApplicationsByNationalNo(NationalNo);
        }

        public static DataTable GetAllLocalDrivingLicencesByFullName(string FullName)
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetAllLocalDrivingLicenceApplicationsByFullName(FullName);
        }

        public static DataTable GetAllLocalDrivingLicencesByApplicationID(int applicationID)
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetAllLocalDrivingLicenceApplicationsByApplicationID(applicationID);
        }

        public static DataTable GetAllNewLocalDrivingLicencesByFullName()
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetAllNewLocalDrivingLicenceApplications();
        }

        public static DataTable GetAllCanceledLocalDrivingLicencesByFullName()
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetAllCanceledLocalDrivingLicenceApplications();
        }

        public static DataTable GetAllCompletedLocalDrivingLicencesByFullName()
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetAllCompletedLocalDrivingLicenceApplications();
        }

        public static bool ExistsByID(int id)
        {
            return clsLocalDrivingLicenceApplicationAccessData.ExistsByID(id);
        }

        public static clsLocalDrivingLicence FindByID(int id)
        {
            int appID = 0, licenceClassID = 0;
            if(clsLocalDrivingLicenceApplicationAccessData.FindByID(id, ref appID, ref licenceClassID))
            {
                return new clsLocalDrivingLicence(id, appID, licenceClassID);
            }
            return null;
        }

        public static clsLocalDrivingLicence FindByApplicationID(int appID)
        {
            int id = 0, licenceClassID = 0;
            if (clsLocalDrivingLicenceApplicationAccessData.FindByID(appID, ref id, ref licenceClassID))
            {
                return new clsLocalDrivingLicence(id, appID, licenceClassID);
            }
            return null;
        }

        public static stApplicationBasicInfoCtrl FindApplicationInfoForCtrlBasicInfo(int ldlAppID)
        {
            string appStatus = "";
            string appType = "", userName = "", appPerson = "";
            double paidFees = 0d;
            DateTime appDate = DateTime.Now, lastStatusDate = DateTime.Now;
            int appID = 0;
            stApplicationBasicInfoCtrl application = new stApplicationBasicInfoCtrl();
            if (clsLocalDrivingLicenceApplicationAccessData.ExistsByID(ldlAppID))
            {
                bool isFound = clsLocalDrivingLicenceApplicationAccessData
                    .FindApplicationInfoForCtrlBasicInfo(ldlAppID, ref appID, ref appPerson,
                    ref appType,
                    ref appStatus, ref paidFees, ref appDate, ref lastStatusDate, ref userName);
                if (isFound)
                {
                    application.PaidFees = paidFees;
                    application.LastStatusDate = lastStatusDate;
                    application.CreatedByUserName = userName;
                    application.ApplicationType = appType;
                    application.ApplicationStatus = appStatus;
                    application.ApplicationPersonName = appPerson;
                    application.ApplicationDate = appDate;
                    application.LocalDrivingLicenseApplicationID = ldlAppID;
                    application.ApplicationID = appID;
                    return application;
                }
            }
            else application.LocalDrivingLicenseApplicationID = -1;
            return application;
        }


        public static byte GetApplicationStatus(int ldlAppID)
        {
            return clsLocalDrivingLicenceApplicationAccessData.GetApplicationStatusByLDLAppID(ldlAppID);
        }

        public static stDrivingLicenseCtrlInfo GetDataForLocalDrivingLicenseInfoControl(int ldlAppID)
        {
            stDrivingLicenseCtrlInfo stDrivingLicenseCtrlInfo = new stDrivingLicenseCtrlInfo();
            if (clsLocalDrivingLicenceApplicationAccessData.ExistsByID(ldlAppID))
            {
                string ClassName = "";
                byte PassedTests = 0;
                if(clsLocalDrivingLicenceApplicationAccessData.GetDataForLocalDrivingLicenseInfoControl(ldlAppID,
                   ref  ClassName, ref PassedTests))
                {
                    stDrivingLicenseCtrlInfo.ClassName = ClassName;
                    stDrivingLicenseCtrlInfo.PassedTests = PassedTests;
                    stDrivingLicenseCtrlInfo.LocalDrivingLicenseAppID = ldlAppID;
                    return stDrivingLicenseCtrlInfo;
                }
            }
            stDrivingLicenseCtrlInfo.LocalDrivingLicenseAppID = -1;
            return stDrivingLicenseCtrlInfo;
        }

        public static int GetCountOfPassedTestsByLocalDrivingLicense(int ldlAppID)
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetCountOfPassedTestsByLocalDrivingLicense(ldlAppID);
        }

        public static int GetCountOfTrialsByLocalDrivingLicenseAndTestType(int ldlAppID, int testTypeID)
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetCountOfTrialsByLocalDrivingLicenseAndTestType(ldlAppID, testTypeID);
        }

        public static int GetLDLAppIDByLicenseID(int licenseID)
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .GetLDLAppIDByLicenseID(licenseID);
        }

        public static bool DeleteByLocalDrivingLicenseApplicationID(int ldlAppID)
        {
            return clsLocalDrivingLicenceApplicationAccessData
                .DeleteByLocalDrivingLicenseApplicationID(ldlAppID);
        }



    }
}
