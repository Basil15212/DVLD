using DLVDData_Access.License_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussnessLayer.License_Classes
{
    public class clsLicenseClass
    {
       enum enMode { AddNew =0 ,Update =1}
        enMode Mode = enMode.AddNew;

        public int LicenseClassID {  get; set; }
        public string ClassName {  get; set; }
        public string ClassDescription {  get; set; }
        public short MinimumAllowedAge { get; set; }
        public short DefaultValidityLength { get; set; }    
        public decimal ClassFees { get; set; }  

        public clsLicenseClass()

        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;
            Mode = enMode.AddNew;

        }

        public clsLicenseClass(int LicenseCLassID , string CLassName ,string ClassDiscrp ,short minAge , short ValidtyLingth ,decimal Fees)
        {
            this.LicenseClassID = LicenseCLassID;
            this.ClassName = CLassName;
            this.ClassDescription= ClassDiscrp;
            this.MinimumAllowedAge= minAge;
            this.DefaultValidityLength= ValidtyLingth;
            this.ClassFees = Fees;
            Mode = enMode.Update;
        }


        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseCLassesData.AddNew(this.ClassName , this.ClassDescription ,
                this.MinimumAllowedAge ,this.DefaultValidityLength ,this.ClassFees);

            return this.LicenseClassID != -1;
        }

        private bool _UpdateLicenseClass()
        {
           return  clsLicenseCLassesData.Update(this.LicenseClassID, this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public static clsLicenseClass Find(int ID)
        {
            string Name = "", Discription = ""; short MinAge = 0, MaxValid = 0; decimal Fees = 0;

            if (clsLicenseCLassesData.GetLicenseClassWithID(ID, ref Name, ref Discription, ref MinAge, ref MaxValid, ref Fees))
            {
                return new clsLicenseClass(ID, Name, Discription, MinAge, MaxValid, Fees);
            }
            else
                return null;
        }

        public static clsLicenseClass FindByName(string Name)
        {
            int ID = -1; string Discription = ""; short MinAge = 0, MaxValid = 0; decimal Fees = 0;

            if (clsLicenseCLassesData.GetLicenseClassWithName(Name, ref ID, ref Discription, ref MinAge, ref MaxValid, ref Fees))
            {
                return new clsLicenseClass(ID, Name, Discription, MinAge, MaxValid, Fees);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicenseClass();
            }
            return false;
        }

        public static DataTable GettAll()
        {
            return clsLicenseCLassesData.GetAllLicenseClasses();

        }

    }
}
