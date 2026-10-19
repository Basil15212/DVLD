using DLVDData_Access.International_Driving_License;
using DVLDBussnessLayer.Drivers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLDBussnessLayer.clsApplication;

namespace DVLDBussnessLayer.Applocations.International_Licesne
{
    public class clsInternationalLicense :clsApplication
    {
        public enum enMode { AddNew= 0 , Update =1 }
        public enMode Mode = enMode.AddNew;

        public clsDriver DriverInfo {  get; set; }
        public int InternationalLicenseID { get; set; }
        public int DriverID {  get; set; }
        public int IssuedUsingLocalLicenseID {  get; set; }
        public DateTime IssueDate {  get; set; }
        public DateTime ExpirationDate {  get; set; }
        public bool IsActive { get; set; }
        
        public clsInternationalLicense()
        {
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.IsActive = true;
            Mode = enMode.AddNew;
        }

        public clsInternationalLicense(int ApplicationID ,int ApplicantPersonID ,DateTime ApplicationDate ,
               enApplicationStatus ApplicationStatus ,DateTime LastStatusDate ,decimal PaidFees ,int CreatedByUserID,
               int InternationalLicenseID ,int DriverID ,int IssuedUsingLocalLicenseID ,DateTime IssueDate ,DateTime ExpirationDate,
               bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = (float)PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            Mode = enMode.Update;

        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalDrivingLicenseData.AddNewInternationalLicense(this.ApplicationID,
                    this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }
        private bool _UpdateInternationalLicense()
        {
            return clsInternationalDrivingLicenseData.UpdateInternationalLicense(this.InternationalLicenseID,
                    this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive,
                    this.CreatedByUserID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch(Mode)
            {
                case enMode.AddNew:
                    {
                        if(_AddNewInternationalLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                        return _UpdateInternationalLicense();
            }
            return false;
        }

        public static clsInternationalLicense Find(int InternationalLicensesID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate =DateTime.Now , ExpirationDate = DateTime.Now ;
            bool IsActive = false;
            if (clsInternationalDrivingLicenseData.GetInternationalLicenseInfoByID(InternationalLicensesID,
                ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive,
                ref CreatedByUserID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsInternationalLicense(Application.ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate,
                        Application.ApplicationStatus, Application.LastStatusDate,(Decimal)Application.PaidFees,
                            Application.CreatedByUserID, InternationalLicensesID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,
                                IsActive);
            }
            else
            {
                return null;
            }


        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalDrivingLicenseData.GetAllInternationalLicenses();
        }
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalDrivingLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalDrivingLicenseData.GetDriverInternationalLicenses(DriverID);
        }
    }
}
