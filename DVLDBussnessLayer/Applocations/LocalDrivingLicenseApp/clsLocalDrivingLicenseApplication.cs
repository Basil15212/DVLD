using DLVDData_Access.Local_Driving_License;
using DVLDBussnessLayer.License_Classes;
using System;
using System.Collections.Generic;
using System.Data;
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
        public clsLicenseClass LicenseClassInfo; // wiil be implnted later
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

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicensApplications();
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

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool DoesPassTestType(int LocalDrivingLicenseID,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType PreviousTestTypeID)
        {
            switch(PreviousTestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);
                case clsTestType.enTestType.StreetTest:
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);
                default:
                    return false;
            }
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendedTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID ,(int)TestTypeID);
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLiceseAppID ,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLiceseAppID, (int)TestTypeID);
        }
    
        public static bool AttendedTest(int LOcalDrivingLicenseAppID ,clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendedTest(LOcalDrivingLicenseAppID ,(int)TestTypeID);
        }
        public bool AttendedTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendedTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLiceseAppID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLiceseAppID, (int)TestTypeID);
        }
        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        //Line 274





    }
}
