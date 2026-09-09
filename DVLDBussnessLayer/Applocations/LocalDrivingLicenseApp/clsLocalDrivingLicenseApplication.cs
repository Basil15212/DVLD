using DLVDData_Access.Local_Driving_License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussnessLayer.Applocations.LocalDrivingLicenseApp
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int LicenseClassID {  get; set; }
        //public clsLicenseClass LicenseClassInfo; // wiil be implnted later
        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication
                (
                int LocalDrivingLicenseApplicationID ,int ApplicationID,int ApplicantPersonID , DateTime ApplicationDate, int ApplicationTypeID,
                enApplicationStatus applicationStatus ,DateTime LastStatusDate ,float PaidFees ,int CreatedByUserID, int LicenseClassID
                )
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            //this will bedone when finish the LicenseClass Part
            //this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;

        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                (this.ApplicationID, this.LicenseClassID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return (clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                (this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID));
        }
        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicensApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                    (LocalDrivingLicensApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication
                    (LocalDrivingLicensApplicationID, ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate,
                        Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate,
                            Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LoclDrivingLicenseApplicationID =-1 , LicenseClassID = -1;
            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                    (ApplicationID, ref LoclDrivingLicenseApplicationID,ref LicenseClassID);
            if (IsFound)
            {
                clsApplication App = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LoclDrivingLicenseApplicationID, ApplicationID, App.ApplicantPersonID,
                            App.ApplicationDate, App.ApplicationTypeID, App.ApplicationStatus, App.LastStatusDate, App.PaidFees,
                            App.CreatedByUserID, LicenseClassID);

            }
            else
                return null;
        }

        public bool Save()
        {
            // Here we Go 
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    case enMode.Update:
                    return (_UpdateLocalDrivingLicenseApplication());
            }
            return false;
        }

        public bool Delete()
        {
            bool IsBaseApplicationDeleted =false;
            bool IsLocalDrivingApplicationDeleted =false;

            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
            if(!IsLocalDrivingApplicationDeleted)
                return false;
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;

        }

        //alot of methouds are  waitning to be done sooon
    }
}
