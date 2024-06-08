using AADLBusiness;
using AADLDataAccess;
using AADLDataAccess.Sharia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness.Sharia
{
    public class clsShariaCaseType
    {
    
        public enum enMode { AddNew = 0, Update = 1 };
    
        public enMode Mode = enMode.AddNew;
    
        public int ShariaCaseTypeID { get; set; }

        public string ShariaCaseTypeName { get; set; }

        public int CreatedByAdminID { get; set; }
        
        public clsAdmin AdminInfo { get; }
    
        public clsShariaCaseType()
        {
            this.ShariaCaseTypeID = -1;
    
            this.ShariaCaseTypeName = "";
    
            this.CreatedByAdminID = -1;
            
        }

        private clsShariaCaseType(int ShariaCaseTypeID, string ShariaCaseTypeName,
            int CreatedByAdminID)
            {
                this.ShariaCaseTypeID = ShariaCaseTypeID;
                this.ShariaCaseTypeName = ShariaCaseTypeName;
                this.CreatedByAdminID = CreatedByAdminID;
            }

        public static clsShariaCaseType Find(int ShariaCaseTypeID)
            {
                string ShariaCaseTypeName = "";
                int CreatedByAdminID = -1;

                if (clsShariaCaseTypeData.GetShariaCaseTypeInfoByCaseTypeID(ShariaCaseTypeID, ref ShariaCaseTypeName, ref CreatedByAdminID))

                    return new clsShariaCaseType(ShariaCaseTypeID, ShariaCaseTypeName, CreatedByAdminID);
                else
                    return null;

            }
    
        public static clsShariaCaseType Find(string ShariaCaseTypeName)
            {

                int ShariaCaseTypeID = -1, CreatedByAdminID = -1;

                if (clsShariaCaseTypeData.GetShariaCaseTypeInfoByCaseTypeName(ShariaCaseTypeName, ref ShariaCaseTypeID,
                    ref CreatedByAdminID))

                    return new clsShariaCaseType(ShariaCaseTypeID, ShariaCaseTypeName, CreatedByAdminID);
                else
                    return null;

            }
    
        public static DataTable GetAllShariaCaseTypes()
            {
                return clsShariaCaseTypeData.GetAllShariaCaseTypes();
            }

        private bool _AddNewShariaCaseType()
            {
                //call DataAccess Layer 

                this.ShariaCaseTypeID = clsShariaCaseTypeData.AddNewShariaCaseType(this.ShariaCaseTypeName,
                    this.CreatedByAdminID);

                return (this.ShariaCaseTypeID != -1);

            }
    
        private bool _UpdateShariaCaseType()
            {
                //call DataAccess Layer 

                return clsShariaCaseTypeData.UpdateShariaCaseType(this.ShariaCaseTypeID, this.ShariaCaseTypeName,
                    this.CreatedByAdminID);
            }
    
        public bool Save()
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewShariaCaseType())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return _UpdateShariaCaseType();

                }

                return false;
            }


    }


}
