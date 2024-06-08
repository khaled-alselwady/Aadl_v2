using AADLBusiness;
using AADLBusiness.Lists.Closed;
using AADLBusiness.Lists.WhiteList;
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
    public class clsSharia:clsPractitioner
    {
        public enum enSearchBy { ShariaLicenseNumber, PersonID, ShariaID,  LawyerID ,PractitionerID };
        public int ShariaID { set; get; }

        public string ShariaLicenseNumber { set; get; }

        private Dictionary<int,string> _ShariaCasesPracticeIDNameDictionary;

        public Dictionary<int,string>ShariaCasesPracticeIDNameDictionary
        {
            get
            {
                return _ShariaCasesPracticeIDNameDictionary;
            }
            set
            {
 
                _ShariaCasesPracticeIDNameDictionary = value;
            }
        }

        public override clsSubscriptionType SubscriptionTypeInfo { get; }
        public override clsSubscriptionWay SubscriptionWayInfo { get; }

        public override clsUser UserInfo { get; }
        public override clsUser LastEditByUserInfo { get; }
        public override clsPerson SelectedPersonInfo { get; }

        //cls white list - cls closed list.
        public clsSharia()
        {
            this.ShariaID = -1;
            this.PersonID = -1;
            this.ShariaLicenseNumber = "";
            this.IssueDate = DateTime.MinValue;
            this.LastEditDate = null;
            this.LastEditByUserID= null;
            this.SubscriptionTypeID = -1;
            this.CreatedByUserID = -1;
            this.IsActive = false;
            UserInfo = null;
            Mode = enMode.AddNew;
            _ShariaCasesPracticeIDNameDictionary = new Dictionary<int, string>();

        }

        private clsSharia(int PersonID,int PractitionerID,bool IsLawyer, int SubscriptionTypeID,int SubscriptionWayID, int ShariaID, 
            string ShariaLicenseNumber, DateTime IssueDate,
            DateTime? LastEditDate, int? LastEditByUserID, int CreatedByUserID, bool IsActive,
            Dictionary<int, string> ShariaCasesPracticeIDNameDictionary)
        {
            try
            {
                this.PractitionerID = PractitionerID;
                this.PersonID = PersonID;
                this.IsLawyer = IsLawyer;
                this.SubscriptionTypeID = SubscriptionTypeID;
                this.SubscriptionWayID = SubscriptionWayID;
                this.SubscriptionTypeInfo = clsSubscriptionType.Find(SubscriptionTypeID);
                this.SubscriptionWayInfo = clsSubscriptionWay.Find(SubscriptionWayID);
                this.ShariaID = ShariaID;
            this.ShariaLicenseNumber = ShariaLicenseNumber;
            this.PractitionerID= PractitionerID;
            this.IssueDate = IssueDate;
            this.LastEditDate = LastEditDate;
            this.LastEditByUserID= LastEditByUserID;
            this.CreatedByUserID = CreatedByUserID;
            this.IsActive = IsActive;
            this.SelectedPersonInfo = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);

            UserInfo = clsUser.FindByUserID(CreatedByUserID);
            //cases regulator practice.
            _ShariaCasesPracticeIDNameDictionary = ShariaCasesPracticeIDNameDictionary;

                if (LastEditByUserID != null)
                {
                    LastEditByUserInfo = clsUser.FindByUserID(LastEditByUserID);

                }
            Mode = enMode.Update;
            }
                catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                clsHelperClasses.WriteEventToLogFile("Problem happens in construction sharia object,", System.Diagnostics.EventLogEntryType.Error);
            }

        }
        protected  override bool _AddNew()
        {
            //call DataAccess Layer 

            var pair= clsShariaData.AddNewSharia(this.PersonID,
                this.ShariaLicenseNumber, this.SubscriptionTypeID, 
                this.SubscriptionWayID, this.CreatedByUserID, this.IsActive, ShariaCasesPracticeIDNameDictionary);

            this.ShariaID = pair.NewShariaID;
            this.PractitionerID = pair.NewPractitionerID;

            return (ShariaID != -1);

        }
        protected override bool _Update()
        {
            //call DataAccess Layer 
            return clsShariaData.UpdateSharia(this.ShariaID,this.PractitionerID, this.ShariaLicenseNumber,
                this.SubscriptionTypeID,this.SubscriptionWayID,this.IsActive, this.LastEditByUserID,
                 ShariaCasesPracticeIDNameDictionary);

        }
        private static clsSharia _Find<T>(T Value, enSearchBy FindBasedOn)
        {
            int ShariaID = -1, PersonID = -1,PractitionerID=-1;
            int ?LastEditByUserID = null;
            bool IsFound = false,IsLawyer=true;
            int  CreatedByUserID = -1, SubscriptionTypeID = -1, SubscriptionWayID=-1;
            string ShariaLicenseNumber = "";
            bool IsActive = false;
            DateTime IssueDate = DateTime.Now;
            DateTime? LastEditDate = null;
            Dictionary<int, string> ShariaCasesPracticeIDNameDictionary = new Dictionary<int, string>();

            switch (FindBasedOn)
            {
                case enSearchBy.PractitionerID:
                    {


                        if (int.TryParse(Value.ToString(), out int ID))
                        {
                            PractitionerID = ID;
                            IsFound = clsShariaData.GetShariaInfoByPractitionerID
                                   (PractitionerID,ref ShariaID, ref ShariaLicenseNumber, ref PersonID, ref IsLawyer,
                                   ref IssueDate, ref LastEditByUserID, ref SubscriptionTypeID, ref SubscriptionWayID, ref CreatedByUserID, ref IsActive,
                                   ref ShariaCasesPracticeIDNameDictionary);
                        }

                        break;

                    }
                //case enSearchBy.ShariaID:
                //    {


                //        if (int.TryParse(Value.ToString(), out int ID))
                //        {
                //            ShariaID = ID;
                //            IsFound = clsShariaData.GetShariaInfoByShariaID
                //                   (ShariaID, ref PersonID, ref ShariaLicenseNumber, ref IsLawyer, ref PractitionerID,
                //                   ref IssueDate,ref  LastEditByUserID, ref SubscriptionTypeID, ref SubscriptionWayID, ref CreatedByUserID, ref IsActive,
                //                   ref ShariaCasesPracticeIDNameDictionary);
                //        }

                //        break;

                //    }

                //case enSearchBy.PersonID:
                //    {


                //        if (int.TryParse(Value.ToString(), out int ID))
                //        {
                //            PersonID = ID;
                //            IsFound = clsShariaData.GetShariaInfoByPersonID
                //                   (PersonID, ref ShariaID, ref ShariaLicenseNumber, ref IsLawyer, ref PractitionerID,
                //                   ref IssueDate, ref LastEditByUserID, ref SubscriptionTypeID,ref SubscriptionWayID, ref CreatedByUserID, ref IsActive
                //                   , ref ShariaCasesPracticeIDNameDictionary);
                //        }

                //        break;

                //    }

                //case enSearchBy.ShariaLicenseNumber:
                //    {
                //        ShariaLicenseNumber = Value.ToString();

                //        if (string.IsNullOrEmpty(ShariaLicenseNumber))
                //        {
                //            IsFound = clsShariaData.GetShariaInfoByShariaLicenseNumber
                //                   (ShariaLicenseNumber, ref ShariaID, ref PersonID , ref IsLawyer, ref PractitionerID,
                //                   ref IssueDate, ref LastEditByUserID, ref SubscriptionTypeID, ref SubscriptionWayID,ref CreatedByUserID, ref IsActive,
                //                   ref ShariaCasesPracticeIDNameDictionary);
                //        }

                //        break;

                //    }


            }

            if (IsFound)
                //we return new object of that User with the right data
                return new clsSharia( PersonID, PractitionerID,IsLawyer,SubscriptionTypeID,SubscriptionWayID,ShariaID, ShariaLicenseNumber,  
                    IssueDate, LastEditDate, LastEditByUserID, CreatedByUserID, IsActive, ShariaCasesPracticeIDNameDictionary);

            return null;

        }
        public override bool Save()
        {
            try
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        {

                            if (_AddNew())
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
                        {
                            return _Update();

                        }
                
                }

            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Problem while adding a new sharia to the system , review cls sharia class, " +
                    "\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                throw new Exception(ex.Message, ex);
            }

            return false;
        }
        public static clsSharia Find<T>(T value, enSearchBy searchBy)
        {

            switch (searchBy)
            {
                case enSearchBy.PractitionerID:
                    {
                        if (int.TryParse(value.ToString(), out int ID))
                        {
                            return _Find(ID, enSearchBy.PractitionerID);
                        }
                        else
                        {
                            return null;
                        }
                    }

                case enSearchBy.ShariaID:
                    {
                        if (int.TryParse(value.ToString(), out int ID))
                        {
                            return _Find(ID, enSearchBy.ShariaID);
                        }
                        else
                        {
                            return null;
                        }

                    }

                case enSearchBy.PersonID:
                    {
                        if (int.TryParse(value.ToString(), out int ID))
                        {
                            return _Find(ID, enSearchBy.PersonID);
                        }
                        else
                        {
                            return null;
                        }

                    }

                case enSearchBy.ShariaLicenseNumber:
                    {
                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            return _Find(value, enSearchBy.ShariaLicenseNumber);
                        }
                        else
                        {
                            return null;
                        }
                    }


            }

            return null;

        }

        //public override clsPractitioner Find<T>(T value)//override.
        //{
        //    return new clsSharia();
        //}
        public async static Task<DataTable> GetAllSharias()
        {
            return await clsShariaData.GetAllShariasAsync();
        }
        [Obsolete("Not implemented well , don't use it.")]
        public static bool DeleteSharia(int ShariaID)
        {
            return clsShariaData.DeleteSharia(ShariaID);
        }

        public static bool IsShariaExist<T>(T Value, enSearchBy CheckBy)
        {
            switch (CheckBy)
            {

                case enSearchBy.ShariaID:
                    {
                        if (int.TryParse(Value.ToString(), out int ShariaID))
                        {

                            return clsShariaData.IsShariaExistByShariaID(ShariaID);
                        }
                        else
                            return false;
                    }

                case enSearchBy.PersonID:
                    {
                        if (int.TryParse(Value.ToString(), out int PersonID))
                        {

                            return clsShariaData.IsShariaExistByPersonID(PersonID);
                        }
                        else
                        {
                            return false;
                        }

                    }

                case enSearchBy.ShariaLicenseNumber:
                    {
                        string ShariaLicenseNumber = Value.ToString();
                        if (!string.IsNullOrEmpty(ShariaLicenseNumber))
                        {

                            return clsShariaData.IsShariaExistByShariaLicenseNumber(ShariaLicenseNumber);
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enSearchBy.PractitionerID:
                    {
                        if (int.TryParse(Value.ToString(), out int LawyerID))
                        {

                            return clsShariaData.IsShariaExistByPractitionerID(LawyerID);
                        }
                        else
                        {
                            return false;
                        }

                    }


            }

            return false;

        }

        public static bool UpdateRegulatorList(int RegulatorID, int ListID, clsList.enListType listType)
        {

            return clsRegulatorData.UpdateRegulatorList(RegulatorID, ListID, (int)listType);

        }

        public  bool IsShariaInWhiteList()
        {
            return clsWhiteList.IsPractitionerInWhiteList(this.PractitionerID, enPractitionerType.Sharia);
        }
        public bool IsShariaInClosedList()
        {
            return clsClosedList.IsPractitionerInClosedList(this.PractitionerID, enPractitionerType.Sharia);
        }
   

    }
}
