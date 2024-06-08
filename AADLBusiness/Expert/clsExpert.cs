using AADLBusiness;
using AADLDataAccess.Expert;
using System;
using System.Collections.Generic;
using System.Data;

namespace AADLBusiness.Expert
{
    public class clsExpert : clsPractitioner
    {
        public enum enDeleteMode
        {
            Permanently = 0,
            Softly = 1
        }

        public enum enFindBy
        {
            ExpertID = 0,
            PersonID = 1,
            PractitionerID = 2
        }

        public int? ExpertID { get; set; }

        private Dictionary<int, string> _expertCasesPracticeIDNameDictionary = new Dictionary<int, string>();

        public Dictionary<int, string> ExpertCasesPracticeIDNameDictionary
        {
            get => _expertCasesPracticeIDNameDictionary;
            set => _expertCasesPracticeIDNameDictionary = value;
        }

        public clsExpert()
        {
            ExpertID = null;
            PractitionerID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            IssueDate = DateTime.Now;
            IsActive = false;
            LastEditByUserID = null;
            this.IsLawyer = true;
            UserInfo = null;

            Mode = enMode.AddNew;
        }

        private clsExpert(int? expertID, int? practitionerID, int? personID,
            int? createdByUserID, DateTime issueDate, bool isActive, int? lastEditByUserID, DateTime? LastEditDate,
            bool IsLawyer, int SubscriptionTypeID, int SubscriptionWayID,
            Dictionary<int, string> ExpertCasesPracticeIDNameDictionary)
        {
            ExpertID = expertID;
            PractitionerID = practitionerID.Value;
            PersonID = personID.Value;
            CreatedByUserID = createdByUserID.Value;
            IssueDate = issueDate;
            IsActive = isActive;
            LastEditByUserID = lastEditByUserID;
            this.IsLawyer = IsLawyer;
            this.SubscriptionTypeID = SubscriptionTypeID;
            this.SubscriptionWayID = SubscriptionWayID;
            this.LastEditDate = LastEditDate;
            _expertCasesPracticeIDNameDictionary = ExpertCasesPracticeIDNameDictionary;

            this.SelectedPersonInfo = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);
            this.SubscriptionTypeInfo = clsSubscriptionType.Find(SubscriptionTypeID);
            this.SubscriptionWayInfo = clsSubscriptionWay.Find(SubscriptionWayID);
            this.UserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.SelectedPersonInfo = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);
            this.LastEditByUserInfo = clsUser.FindByUserID(LastEditByUserID);

            Mode = enMode.Update;
        }

        public override clsSubscriptionType SubscriptionTypeInfo { get; }

        public override clsSubscriptionWay SubscriptionWayInfo { get; }

        public override clsUser UserInfo { get; }

        public override clsUser LastEditByUserInfo { get; }

        public override clsPerson SelectedPersonInfo { get; }

        private static clsExpert _FindByExpertID(int? expertID)
        {
            int? practitionerID = null, personID = -1, createdByUserID = -1, SubscriptionTypeID = -1, SubscriptionWayID = -1;
            int? lastEditByUserID = null;
            DateTime issueDate = DateTime.Now;
            DateTime? LastEditDate = null;
            bool IsLawyer = false, isActive = false;
            Dictionary<int, string> CasesExpertPracticesIDNameDictionary = new Dictionary<int, string>();

            bool isFound = clsExpertData.GetInfoByExpertID(expertID, ref practitionerID, ref personID,
                ref IsLawyer, ref createdByUserID, ref issueDate, ref SubscriptionTypeID,
                ref SubscriptionWayID, ref isActive, ref lastEditByUserID, ref CasesExpertPracticesIDNameDictionary);

            return (isFound) ? (new clsExpert(expertID, practitionerID, personID, createdByUserID,
                                issueDate, isActive, lastEditByUserID, LastEditDate, IsLawyer,
                                SubscriptionTypeID.Value, SubscriptionWayID.Value, CasesExpertPracticesIDNameDictionary))
                             : null;
        }

        private static clsExpert _FindByPersonID(int? personID)
        {
            int? practitionerID = null, expertID = null, createdByUserID = -1, SubscriptionTypeID = -1, SubscriptionWayID = -1;
            int? lastEditByUserID = null;
            DateTime issueDate = DateTime.Now;
            DateTime? LastEditDate = null;
            bool IsLawyer = false, isActive = false;
            Dictionary<int, string> CasesExpertPracticesIDNameDictionary = new Dictionary<int, string>();

            bool isFound = clsExpertData.GetInfoByPersonID(personID, ref practitionerID, ref expertID,
                ref IsLawyer, ref createdByUserID, ref issueDate, ref SubscriptionTypeID,
                ref SubscriptionWayID, ref isActive, ref lastEditByUserID, ref CasesExpertPracticesIDNameDictionary);

            return (isFound) ? (new clsExpert(expertID, practitionerID, personID, createdByUserID,
                                issueDate, isActive, lastEditByUserID, LastEditDate, IsLawyer,
                                SubscriptionTypeID.Value, SubscriptionWayID.Value, CasesExpertPracticesIDNameDictionary))
                             : null;
        }

        private static clsExpert _FindByPractitionerID(int? practitionerID)
        {
            int? expertID = null, personID = -1, createdByUserID = -1, SubscriptionTypeID = -1, SubscriptionWayID = -1;
            int? lastEditByUserID = null;
            DateTime issueDate = DateTime.Now;
            DateTime? LastEditDate = null;
            bool IsLawyer = false, isActive = false;
            Dictionary<int, string> CasesExpertPracticesIDNameDictionary = new Dictionary<int, string>();

            bool isFound = clsExpertData.GetInfoByPractitionerID(practitionerID, ref expertID, ref personID,
                ref IsLawyer, ref createdByUserID, ref issueDate, ref SubscriptionTypeID,
                ref SubscriptionWayID, ref isActive, ref lastEditByUserID, ref CasesExpertPracticesIDNameDictionary);

            return (isFound) ? (new clsExpert(expertID, practitionerID, personID, createdByUserID,
                                issueDate, isActive, lastEditByUserID, LastEditDate, IsLawyer,
                                SubscriptionTypeID.Value, SubscriptionWayID.Value, CasesExpertPracticesIDNameDictionary))
                             : null;
        }

        public static clsExpert Find(int? entityID, enFindBy findBy)
        {
            switch (findBy)
            {
                case enFindBy.ExpertID:
                    return _FindByExpertID(entityID);

                case enFindBy.PersonID:
                    return _FindByPersonID(entityID);

                case enFindBy.PractitionerID:
                    return _FindByPractitionerID(entityID);

                default:
                    return null;
            }
        }

        protected override bool _AddNew()
        {
            (this.ExpertID, this.PractitionerID) = clsExpertData.Add(PersonID, SubscriptionTypeID,
                                                   SubscriptionWayID, CreatedByUserID, IsActive,
                                                   ExpertCasesPracticeIDNameDictionary);

            return (ExpertID.HasValue);
        }

        protected override bool _Update()
        => clsExpertData.Update(ExpertID ?? -1, PractitionerID, LastEditByUserID,
            SubscriptionTypeID, SubscriptionWayID, IsActive, ExpertCasesPracticeIDNameDictionary);

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
            }

            return false;
        }

        private static bool _DeletePermanently(int? expertID)
        => clsExpertData.DeletePermanently(expertID);

        private static bool _DeleteSoftly(int? expertID)
               => clsExpertData.DeleteSoftly(expertID);

        public static bool Delete(int? expertID, enDeleteMode mode)
        {
            switch (mode)
            {
                case enDeleteMode.Permanently:
                    return _DeletePermanently(expertID);

                case enDeleteMode.Softly:
                    return _DeleteSoftly(expertID);

                default:
                    return false;
            }
        }

        private static bool _ExistsByExpertID(int? expertID)
        => clsExpertData.ExistsByExpertID(expertID);

        private static bool _ExistsByPersonID(int? personID)
        => clsExpertData.ExistsByPersonID(personID);

        private static bool _ExistsByPractitionerID(int? practitionerID)
        => clsExpertData.ExistsByPractitionerID(practitionerID);

        public static bool Exists(int? entityID, enFindBy findBy)
        {
            switch (findBy)
            {
                case enFindBy.ExpertID:
                    return _ExistsByExpertID(entityID);

                case enFindBy.PersonID:
                    return _ExistsByPersonID(entityID);

                case enFindBy.PractitionerID:
                    return _ExistsByPractitionerID(entityID);

                default:
                    return false;
            }
        }

        public static DataTable All()
        => clsExpertData.All();
    }
}
