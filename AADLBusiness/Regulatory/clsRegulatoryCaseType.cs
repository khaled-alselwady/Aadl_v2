using AADLBusiness;
using AADL_DataAccess;
using AADLDataAccess;
using Iced.Intel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness
{
    public class clsRegulatoryCaseType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int RegulatoryCaseTypeID { get; set; }

        public string RegulatoryCaseTypeName { get; set; }

        public int CreatedByAdminID {  get; set; }

        public clsAdmin AdminInfo { get; }

        public clsRegulatoryCaseType()
        {
            this.RegulatoryCaseTypeID = -1;
            this.RegulatoryCaseTypeName = "";
            this.CreatedByAdminID = -1;
        }

        private clsRegulatoryCaseType(int RegulatoryCaseTypeID, string RegulatoryCaseTypeName,
           int CreatedByAdminID)
        {
            this.RegulatoryCaseTypeID = RegulatoryCaseTypeID;
            this.RegulatoryCaseTypeName = RegulatoryCaseTypeName;
            this.CreatedByAdminID = CreatedByAdminID;
        }

        public static clsRegulatoryCaseType Find(int RegulatoryCaseTypeID)
        {
            string RegulatoryCaseTypeName = "";
            int CreatedByAdminID = -1;

            if (clsRegulatoryCaseTypeData.GetRegulatoryCaseTypeInfoByCaseTypeID(RegulatoryCaseTypeID, ref RegulatoryCaseTypeName,ref CreatedByAdminID))

                return new clsRegulatoryCaseType(RegulatoryCaseTypeID, RegulatoryCaseTypeName, CreatedByAdminID);
            else
                return null;

        }
        public static clsRegulatoryCaseType Find(string RegulatoryCaseTypeName)
        {
           
            int RegulatoryCaseTypeID=-1, CreatedByAdminID = -1;

            if (clsRegulatoryCaseTypeData.GetRegulatoryCaseTypeInfoByCaseTypeName(RegulatoryCaseTypeName, ref RegulatoryCaseTypeID, 
                ref CreatedByAdminID))

                return new clsRegulatoryCaseType(RegulatoryCaseTypeID, RegulatoryCaseTypeName, CreatedByAdminID);
            else
                return null;

        }
        public  static DataTable GetAllRegulatoryCaseTypes()
        {
            return  clsRegulatoryCaseTypeData.GetAllRegulatoryCaseTypes();
        }
     
        private bool _AddNewRegulatoryCaseType()
        {
            //call DataAccess Layer 

            this.RegulatoryCaseTypeID = clsRegulatoryCaseTypeData.AddNewRegulatoryCaseType(this.RegulatoryCaseTypeName,
                this.CreatedByAdminID);

            return (this.RegulatoryCaseTypeID != -1);

        }
        private bool _UpdateRegulatoryCaseType()
        {
            //call DataAccess Layer 

            return clsRegulatoryCaseTypeData.UpdateRegulatoryCaseType(this.RegulatoryCaseTypeID, this.RegulatoryCaseTypeName,
                this.CreatedByAdminID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewRegulatoryCaseType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateRegulatoryCaseType();

            }

            return false;
        }

    }

}
