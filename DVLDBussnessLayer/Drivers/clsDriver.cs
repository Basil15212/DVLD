using DLVDData_Access.Drivers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBussnessLayer.Drivers
{
    public class clsDriver
    {
        public enum enMode { AddNew =0 , Update = 1}
        public enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;
        public int DriverID {  get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate { get; set; }


        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode =enMode.AddNew;
        }

        public clsDriver(int DriverDI ,int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            this.DriverID =DriverDI;
            this.PersonID=PersonID;
            this.CreatedByUserID=CreatedByUserID;
            this.CreatedDate=CreatedDate;
            this.PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
        }


        private bool _AddNewDriver ()
        {
            this.DriverID = clsDriverData.AddNewDRiver(PersonID, CreatedByUserID);
            return this.DriverID != -1;
        }
        private bool _Update()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    case enMode.Update:
                    return (_Update());
            }
            return false;
        }


        public static  clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;int PersonID = -1;
            if(clsDriverData.GetDriverInfoByID(DriverID ,ref PersonID ,ref CreatedByUserID,ref  CreatedDate))
            {
                return new clsDriver(DriverID , PersonID, CreatedByUserID,CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        //Some License Metouds Are Waiting To be Finished ;
    }
}
