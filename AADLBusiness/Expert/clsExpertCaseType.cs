using AADLDataAccess.Expert;
using System.Data;

namespace AADLBusiness.Expert
{
    public class clsExpertCaseType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? ExpertCaseTypeID { get; set; }
        public string ExpertCaseTypeName { get; set; }
        public int? CreatedByAdminID { get; set; }
        public int? LastEditByAdminID { get; set; }

        public clsExpertCaseType()
        {
            ExpertCaseTypeID = null;
            ExpertCaseTypeName = string.Empty;
            CreatedByAdminID = null;
            LastEditByAdminID = null;

            Mode = enMode.AddNew;
        }

        private clsExpertCaseType(int? expertCaseTypeID,
            string expertCaseTypeName, int? createdByAdminID, int? lastEditByAdminID)
        {
            ExpertCaseTypeID = expertCaseTypeID;
            ExpertCaseTypeName = expertCaseTypeName;
            CreatedByAdminID = createdByAdminID;
            LastEditByAdminID = lastEditByAdminID;

            Mode = enMode.Update;
        }

        private bool _Add()
        {
            ExpertCaseTypeID = clsExpertCaseTypeData.Add(ExpertCaseTypeName,
                CreatedByAdminID.Value, LastEditByAdminID);

            return (ExpertCaseTypeID.HasValue);
        }

        private bool _Update()
        {
            return clsExpertCaseTypeData.Update(ExpertCaseTypeID ?? -1, ExpertCaseTypeName,
                CreatedByAdminID.Value, LastEditByAdminID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_Add())
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

        public static clsExpertCaseType Find(int? expertCaseTypeID)
        {
            string expertCaseTypeName = string.Empty;
            int? createdByAdminID = null;
            int? lastEditByAdminID = null;

            bool isFound = clsExpertCaseTypeData.GetInfoByCaseTypeID(expertCaseTypeID,
                ref expertCaseTypeName, ref createdByAdminID, ref lastEditByAdminID);

            return (isFound) ? (new clsExpertCaseType(expertCaseTypeID, expertCaseTypeName, createdByAdminID, lastEditByAdminID)) : null;
        }

        public static clsExpertCaseType Find(string expertCaseTypeName)
        {
            int? expertCaseTypeID = null;
            int? createdByAdminID = null;
            int? lastEditByAdminID = null;

            bool isFound = clsExpertCaseTypeData.GetInfoByCaseTypeName(expertCaseTypeName,
                ref expertCaseTypeID, ref createdByAdminID, ref lastEditByAdminID);

            return (isFound) ? (new clsExpertCaseType(expertCaseTypeID,
                expertCaseTypeName, createdByAdminID, lastEditByAdminID)) : null;
        }

        public static bool Delete(int? expertCaseTypeID)
        => clsExpertCaseTypeData.Delete(expertCaseTypeID);

        public static bool Exists(int? expertCaseTypeID)
        => clsExpertCaseTypeData.Exists(expertCaseTypeID);

        public static DataTable All()
        => clsExpertCaseTypeData.All();
    }
}