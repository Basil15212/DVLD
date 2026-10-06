using DLVDData_Access.Licenses;
using DVLDBussnessLayer.Drivers;
using DVLDBussnessLayer.License_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussnessLayer.Licenses
{
    public class clsLicense
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public clsDriver DriverInfo;
        public int LicenseID {  get; set; }
        public int ApplicationID {  get; set; }
        public int DriverID { get; set; }
        public int LicenseClass {  get; set; }
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate {  get; set; }
        public DateTime ExpirationDate {  get; set; }
        public string Notes { get; set; }
        public enIssueReason IssueReason { set; get; }
        public decimal PaidFees {  get; set; }
        public bool IsActive { get; set; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        //public clsDetainedLicense DetainedInfo { set; get; }

        public int CreatedByUserID {  get; set; }

        //public bool IsDetained
        //{
        //    get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID); }
        //}

        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsLicense(int LicenseID ,int ApplicationID ,int DriverID ,int LicenseClassID ,DateTime IssueDate, DateTime ExpirationDate ,
                            string Notes, decimal PaidFees , bool IsActive , enIssueReason IssueReason ,int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID=ApplicationID;
            this.DriverID=DriverID;
            this.LicenseClass=LicenseClassID;
            this.IssueDate=IssueDate;
            this.ExpirationDate=ExpirationDate;
            this.Notes =Notes;
            this.PaidFees =PaidFees;
            this.IsActive=IsActive;
            this.IssueReason =IssueReason;
            this.CreatedByUserID=CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassInfo = clsLicenseClass.Find(this.LicenseClass);
            //this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);
            Mode = enMode.Update;
        }



        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return (clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID));
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    {
                        if(_AddNewLicense())
                        {
                            Mode =enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    case enMode.Update:
                    {
                        return (_UpdateLicense());
                    }
            }
            return false;
        }

        public static clsLicense FindByLicenseID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate =DateTime.Now ,ExpirationDate = DateTime.Now ;
            string Notes = ""; bool IsActive = false;
            decimal PaidFees = 0; byte IssueReason = 1; 
            if(clsLicenseData.GetLicenseInfoByID(LicenseID ,ref ApplicationID ,ref DriverID ,ref LicenseClass ,ref IssueDate,
                    ref ExpirationDate ,ref Notes ,ref PaidFees ,ref IsActive ,ref IssueReason , ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive,
                            (enIssueReason)IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }

        public static bool IsLicenseExistByPersonID(int LicenseID ,int LicenseClassID)
        {
            return GetActiveLicenseIDByPersonID(LicenseID, LicenseClassID) != -1;
        }
        public static int GetActiveLicenseIDByPersonID(int PersonID ,int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID , LicenseClassID);
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }
        public bool DeActivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }


        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        // Will complet after finishing Detaind and reNew 
        //Line 204
    }
}
