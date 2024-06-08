using AADLBusiness;
using AADLDataAccess.Judger;
using System;
using System.Collections.Generic;

namespace AADLBusiness.Judger
{
    public class clsJudger : clsPractitioner
    {
        public clsJudger()
        {
            JudgerID = -1;
            PersonID = -1;
            IssueDate = DateTime.MinValue;
            LastEditDate = null;
            LastEditByUserID = null;
            SubscriptionTypeID = -1;
            CreatedByUserID = -1;
            IsActive = false;
            UserInfo = null;
            _JudgeCasesPracticeIDNameDictionary = new Dictionary<int, string>();
            Mode = enMode.AddNew;
        }

        private clsJudger(int JudgerID, int PractitionerID, int PersonID, bool IsLawyer, int SubscriptionTypeID, int SubscriptionWayID, DateTime IssueDate,
            DateTime? LastEditDate, int? LastEditByUserID, int CreatedByUserID, bool IsActive, Dictionary<int, string> ShariaCasesPracticeIDNameDictionary)
        {
            try
            {
                this.JudgerID = JudgerID;
                this.PersonID = PersonID;
                this.SelectedPersonInfo = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);
                this.PractitionerID = PractitionerID;
                this.IsLawyer = IsLawyer;
                this.SubscriptionTypeID = SubscriptionTypeID;
                this.SubscriptionTypeInfo = clsSubscriptionType.Find(SubscriptionTypeID);
                this.SubscriptionWayID = SubscriptionWayID;
                this.SubscriptionWayInfo = clsSubscriptionWay.Find(SubscriptionWayID);
                this.IssueDate = IssueDate;
                this.LastEditDate = LastEditDate;
                this.LastEditByUserID = LastEditByUserID;
                if (this.LastEditByUserID != null) this.LastEditByUserInfo = clsUser.FindByUserID(this.LastEditByUserID);
                this.CreatedByUserID = CreatedByUserID;
                this.UserInfo = clsUser.FindByUserID(this.CreatedByUserID);
                this.IsActive = IsActive;
                this._JudgeCasesPracticeIDNameDictionary = ShariaCasesPracticeIDNameDictionary;

                this.Mode = enMode.Update;
            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile($"Problem happens in construction Judger object: {ex.Message}", System.Diagnostics.EventLogEntryType.Error);
            }
        }

        public int JudgerID { get; set; }

        private Dictionary<int, string> _JudgeCasesPracticeIDNameDictionary;

        public Dictionary<int, string> JudgeCasesPracticeIDNameDictionary
        {
            get
            {
                return _JudgeCasesPracticeIDNameDictionary;
            }
            set
            {

                _JudgeCasesPracticeIDNameDictionary = value;
            }
        }

        public override clsSubscriptionType SubscriptionTypeInfo { get; }

        public override clsSubscriptionWay SubscriptionWayInfo { get; }

        public override clsUser UserInfo { get; }

        public override clsUser LastEditByUserInfo { get; }

        public override clsPerson SelectedPersonInfo { get; }

        public static clsJudger FindByJudgerID(int JudgerID)
        {
            int PractitionerID = -1, PersonID = -1, SubscriptionTypeID = -1, SubscriptionWayID = -1, CreatedByUserID = -1;
            int? LastEditByUserID = null;
            DateTime IssueDate = DateTime.MinValue;
            DateTime? LastEditDate = null;
            bool IsActive = false, IsLawyer = false;
            Dictionary<int, string> CasesJudgerPracticesIDNameDictionary = new Dictionary<int, string>();

            if (clsJudgerData.GetJudgerInfoByJudgerID(JudgerID, ref PractitionerID, ref PersonID, ref IsLawyer, ref IssueDate, ref SubscriptionTypeID,
                ref SubscriptionWayID, ref CreatedByUserID, ref IsActive, ref LastEditByUserID, ref CasesJudgerPracticesIDNameDictionary))
            {
                return new clsJudger(JudgerID, PractitionerID, PersonID, IsLawyer, SubscriptionTypeID, SubscriptionWayID, IssueDate,
                    LastEditDate, LastEditByUserID, CreatedByUserID, IsActive, CasesJudgerPracticesIDNameDictionary);
            }

            return null;
        }

        public static clsJudger FindByPersonID(int PersonID)
        {
            int PractitionerID = -1, JudgerID = -1, SubscriptionTypeID = -1, SubscriptionWayID = -1, CreatedByUserID = -1;
            int? LastEditByUserID = null;
            DateTime IssueDate = DateTime.MinValue;
            DateTime? LastEditDate = null;
            bool IsActive = false, IsLawyer = false;
            Dictionary<int, string> CasesJudgerPracticesIDNameDictionary = new Dictionary<int, string>();

            if (clsJudgerData.GetJudgerInfoByPersonID(PersonID, ref JudgerID, ref PractitionerID, ref IsLawyer, ref IssueDate, ref SubscriptionTypeID,
                ref SubscriptionWayID, ref CreatedByUserID, ref IsActive, ref LastEditByUserID, ref CasesJudgerPracticesIDNameDictionary))
            {
                return new clsJudger(JudgerID, PractitionerID, PersonID, IsLawyer, SubscriptionTypeID, SubscriptionWayID, IssueDate,
                    LastEditDate, LastEditByUserID, CreatedByUserID, IsActive, CasesJudgerPracticesIDNameDictionary);
            }

            return null;
        }

        public static clsJudger FindByPractitionerID(int PractitionerID)
        {
            int PersonID = -1, JudgerID = -1, SubscriptionTypeID = -1, SubscriptionWayID = -1, CreatedByUserID = -1;
            int? LastEditByUserID = null;
            DateTime IssueDate = DateTime.MinValue;
            DateTime? LastEditDate = null;
            bool IsActive = false, IsLawyer = false;
            Dictionary<int, string> CasesJudgerPracticesIDNameDictionary = new Dictionary<int, string>();

            if (clsJudgerData.GetJudgerInfoByPractitionerID(PractitionerID, ref JudgerID, ref PersonID, ref IsLawyer, ref IssueDate, ref SubscriptionTypeID,
                ref SubscriptionWayID, ref CreatedByUserID, ref IsActive, ref LastEditByUserID, ref CasesJudgerPracticesIDNameDictionary))
            {
                return new clsJudger(JudgerID, PractitionerID, PersonID, IsLawyer, SubscriptionTypeID, SubscriptionWayID, IssueDate,
                    LastEditDate, LastEditByUserID, CreatedByUserID, IsActive, CasesJudgerPracticesIDNameDictionary);
            }

            return null;
        }

        public static bool IsJudgerExistByJudgerID(int JudgerID)
        {
            return clsJudgerData.IsJudgerExist(JudgerID, clsJudgerData.enWhichID.JudgerID);
        }

        public static bool IsJudgerExistByPersonID(int PersonID)
        {
            return clsJudgerData.IsJudgerExist(PersonID, clsJudgerData.enWhichID.PersonID);
        }

        public static bool IsJudgerExistByPractitionerID(int PractitionerID)
        {
            return clsJudgerData.IsJudgerExist(PractitionerID, clsJudgerData.enWhichID.PractitionerID);
        }

        public static bool DeleteJudgerSoftlyByJudgerID(int JudgerID)
        {
            return clsJudgerData.DeleteJudgerSoftly(JudgerID, clsJudgerData.enWhichID.JudgerID);
        }

        public static bool DeleteJudgerSoftlyByPersonID(int PersonID)
        {
            return clsJudgerData.DeleteJudgerSoftly(PersonID, clsJudgerData.enWhichID.PersonID);
        }

        public static bool DeleteJudgerSoftlyByPractitionerID(int PractitionerID)
        {
            return clsJudgerData.DeleteJudgerSoftly(PractitionerID, clsJudgerData.enWhichID.PractitionerID);
        }

        public override bool Save()
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
                default:
                    return false;
            }
        }

        protected override bool _AddNew()
        {
            (this.JudgerID, this.PractitionerID) = clsJudgerData.AddNewJudger(PersonID, SubscriptionTypeID, SubscriptionWayID, CreatedByUserID,
                                                                                    IsActive, JudgeCasesPracticeIDNameDictionary);

            return this.JudgerID != -1;
        }

        protected override bool _Update()
        {
            return clsJudgerData.UpdateJudger(JudgerID, PractitionerID, SubscriptionTypeID, SubscriptionWayID, IsActive, LastEditByUserID, JudgeCasesPracticeIDNameDictionary);
        }
    }
}
