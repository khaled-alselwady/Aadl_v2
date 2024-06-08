using System.Data;
using AADLBusiness;
using AADLDataAccess.Judger;

namespace AADLBusiness.Judger
{
    public class clsJudgeCaseType
    {

        public enum enMode { AddNew = 0, Update = 1 };

        public enMode Mode = enMode.AddNew;

        public int JudgeCaseTypeID { get; set; }
        public string JudgeCaseTypeName { get; set; }
        public int CreatedByAdminID { get; set; }
        public int? LastEditByAdminID { get; set; }

        public clsAdmin CreadedByAdminInfo { get; set; }
        public clsAdmin LastEditByAdminInfo { get; set; }

        public clsJudgeCaseType()
        {
            this.JudgeCaseTypeID = -1;
            this.JudgeCaseTypeName = null;
            this.CreatedByAdminID = -1;

            Mode = enMode.AddNew;
        }

        private clsJudgeCaseType(int JudgeCaseTypeID, string JudgeCaseTypeName, int CreatedByAdminID, int? LastEditByAdminID)
        {
            this.JudgeCaseTypeID = JudgeCaseTypeID;
            this.JudgeCaseTypeName = JudgeCaseTypeName;
            this.CreatedByAdminID = CreatedByAdminID;
            this.CreadedByAdminInfo = clsAdmin.FindByAdminID(CreatedByAdminID);
            this.LastEditByAdminID = LastEditByAdminID;
            if (LastEditByAdminID != null) this.LastEditByAdminInfo = clsAdmin.FindByAdminID(LastEditByAdminID);

            Mode = enMode.Update;
        }

        public static clsJudgeCaseType Find(int JudgeCaseTypeID)
        {
            string JudgeCaseTypeName = "";
            int CreatedByAdminID = -1;
            int? LastEditByAdminID = null;

            if (clsJudgeCaseTypeData.GetJudgeCaseTypeInfoByCaseTypeID(JudgeCaseTypeID, ref JudgeCaseTypeName, ref CreatedByAdminID, ref LastEditByAdminID))
                return new clsJudgeCaseType(JudgeCaseTypeID, JudgeCaseTypeName, CreatedByAdminID, LastEditByAdminID);
            else
                return null;
        }

        public static clsJudgeCaseType Find(string JudgeCaseTypeName)
        {

            int JudgeCaseTypeID = -1, CreatedByAdminID = -1;
            int? LastEditByAdminID = null;

            if (clsJudgeCaseTypeData.GetJudgeCaseTypeInfoByCaseTypeName(JudgeCaseTypeName, ref JudgeCaseTypeID, ref CreatedByAdminID, ref LastEditByAdminID))
                return new clsJudgeCaseType(JudgeCaseTypeID, JudgeCaseTypeName, CreatedByAdminID, LastEditByAdminID);
            else
                return null;
        }

        public static bool DeleteJudgeCaseType(int JudgeCaseTypeID)
        {
            return clsJudgeCaseTypeData.DeleteJudgeCaseType(JudgeCaseTypeID);
        }

        public static DataTable GetAllJudgeCaseTypes()
        {
            return clsJudgeCaseTypeData.GetAllJudgeCaseTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _Update();
            }

            return false;
        }

        private bool _AddNew()
        {
            this.JudgeCaseTypeID = clsJudgeCaseTypeData.AddNewJudgeCaseType(this.JudgeCaseTypeName, this.CreatedByAdminID);

            return (this.JudgeCaseTypeID != -1);
        }

        private bool _Update()
        {
            return clsJudgeCaseTypeData.UpdateJudgeCaseType(this.JudgeCaseTypeID, this.JudgeCaseTypeName, (int)this.LastEditByAdminID);
        }

    }

}
