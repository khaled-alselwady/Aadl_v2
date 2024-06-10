using AADLBusiness;
using AADLBusiness;
using AADL_DataAccess;
using AADLDataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static AADLBusiness.clsPerson;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Diagnostics.Tracing.Parsers.JScript;
using System.ComponentModel.Design;
using AADLBusiness.Lists.WhiteList;
using AADLDataAccess.Lists.White;
using AADLBusiness.Lists.Closed;
using AADLDataAccess.Expert;
using static AADLBusiness.Expert.clsExpert;

namespace AADLBusiness
{
    public class clsRegulator:clsPractitioner
    {
        public enum enSearchBy { PersonID, RegulatorID, MemberShipNumber ,PractitionerID};
        public int RegulatorID { set; get; }
        public string MemberShipNumber { set; get; }
 
        private Dictionary<int,string>_RegulatorCasesPracticeIDNameDictionary;

        public Dictionary<int, string> RegulatorCasesPracticeIDNameDictionary
        {
            get
            {
                return _RegulatorCasesPracticeIDNameDictionary;
            }
            set
            {
                
                _RegulatorCasesPracticeIDNameDictionary = value;
            }
        }
        public override clsUser UserInfo { get; }
        public override clsUser LastEditByUserInfo { get; }
        public override clsPerson SelectedPersonInfo { get;  }
        public override clsSubscriptionType SubscriptionTypeInfo { get; }
        public override clsSubscriptionWay SubscriptionWayInfo { get; }

        public clsRegulator()
        {
            this.RegulatorID = -1;
            this.PersonID = -1;
            this.MemberShipNumber = "";
            this.IsLawyer = true;
            this.IssueDate = DateTime.MinValue;
            this.LastEditDate = null;
            this.CreatedByUserID = -1;
            this.IsActive = false;
            UserInfo = null;
            Mode = enMode.AddNew;
            _RegulatorCasesPracticeIDNameDictionary = new Dictionary<int, string>();

        }

        private clsRegulator(int PractitionerID,int PersonID,bool IsLawyer,int SubscriptionTypeID,int SubscriptionWayID,
            int RegulatorID,   string MemberShipNumber, DateTime IssueDate,
            DateTime? LastEditDate, int ?LastEditByUserID, int CreatedByUserID, bool IsActive,
            Dictionary<int, string> RegulatorCasesPracticeIDNameDictionary)
        {
            this.PractitionerID = PractitionerID;
            this.PersonID = PersonID;
            this.SelectedPersonInfo = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);
            this.IsLawyer = IsLawyer;
            this.SubscriptionTypeID = SubscriptionTypeID;
            this.SubscriptionWayID = SubscriptionWayID;
            this.SubscriptionTypeInfo = clsSubscriptionType.Find(SubscriptionTypeID);
            this.SubscriptionWayInfo = clsSubscriptionWay.Find(SubscriptionWayID);
            this.RegulatorID = RegulatorID;
            this.MemberShipNumber = MemberShipNumber;
            this.IssueDate = IssueDate;
            this.LastEditDate = LastEditDate;
            this.LastEditByUserID = LastEditByUserID;
            this.CreatedByUserID = CreatedByUserID;
            this.IsActive = IsActive;
            _RegulatorCasesPracticeIDNameDictionary = RegulatorCasesPracticeIDNameDictionary;
            UserInfo = clsUser.FindByUserID(CreatedByUserID);
            SelectedPersonInfo = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);
            LastEditByUserInfo=clsUser.FindByUserID(LastEditByUserID);
            Mode = enMode.Update;

        }
        protected override bool _AddNew()
        {
            //call DataAccess Layer 

            var pair = clsRegulatorData.AddNewRegulator(this.PersonID,
                this.MemberShipNumber,   this.SubscriptionTypeID, this.SubscriptionWayID,
                this.CreatedByUserID, this.IsActive, RegulatorCasesPracticeIDNameDictionary);

            
                this.RegulatorID = pair.NewRegulatorID;
                this.PractitionerID = pair.NewPractitionerID;

            return (RegulatorID != -1);

        }
        protected override bool _Update()
        {
            //call DataAccess Layer 

            return clsRegulatorData.UpdateRegulator(this.RegulatorID,this.PractitionerID,
                 this.MemberShipNumber, (int)this.LastEditByUserID, this.SubscriptionTypeID, this.SubscriptionWayID,
                 this.IsActive, RegulatorCasesPracticeIDNameDictionary);
            
        }

        public static clsRegulator Find<T>(T Value,enSearchBy FindBasedOn)
        {
            int RegulatorID = -1, PersonID = -1,
              PractitionerID=-1,CreatedByUserID = -1,SubscriptionTypeID=-1,SubscriptionWayID=-1;
            int? LastEditByUserID = null;
            bool IsFound = false, IsLawyer = true;
            string MemberShipNumber = "";
            bool IsActive = false;
            DateTime IssueDate = DateTime.Now;
            DateTime ?LastEditDate= null;

            Dictionary<int, string> RegulatorCasesPracticeIDNameDictionary = new Dictionary<int, string>();
     
            switch (FindBasedOn)
            {
                case enSearchBy.RegulatorID:
                    {
                        RegulatorID = Convert.ToInt32(Value);


                            IsFound = clsRegulatorData.GetRegulatorInfoByRegulatorID
                                   (RegulatorID, ref PersonID, ref MemberShipNumber, ref IsLawyer, ref PractitionerID,
                                   ref IssueDate, ref LastEditByUserID, ref SubscriptionTypeID, ref SubscriptionWayID,
                                   ref CreatedByUserID, ref IsActive,ref RegulatorCasesPracticeIDNameDictionary);

                        break;

                    }
          
                case enSearchBy.PersonID:
                    {
                        PersonID = Convert.ToInt32(Value);


                            IsFound = clsRegulatorData.GetRegulatorInfoByPersonID
                                   (PersonID,ref RegulatorID, ref MemberShipNumber, ref IsLawyer, ref PractitionerID,
                                   ref IssueDate, ref LastEditByUserID, ref SubscriptionTypeID, ref SubscriptionTypeID, ref CreatedByUserID,ref  IsActive
                                   , ref RegulatorCasesPracticeIDNameDictionary);

                        break;

                    }

                case enSearchBy.MemberShipNumber:
                    {
                         MemberShipNumber=Value.ToString();
                      
                       
                        IsFound = clsRegulatorData.GetRegulatorInfoByMemberShipNumber
                               (MemberShipNumber, ref PersonID, ref RegulatorID, ref PractitionerID, ref IsLawyer,
                               ref IssueDate, ref LastEditByUserID, ref SubscriptionTypeID, ref SubscriptionTypeID,
                               ref CreatedByUserID, ref IsActive,ref RegulatorCasesPracticeIDNameDictionary);

                        break;

                    }

                case enSearchBy.PractitionerID:
                    {

                         PractitionerID = Convert.ToInt32(Value);

                            IsFound = clsRegulatorData.GetRegulatorInfoByPractitionerID
                                   (PractitionerID, ref PersonID, ref RegulatorID, ref MemberShipNumber, ref IsLawyer,
                                   ref IssueDate, ref LastEditByUserID, ref SubscriptionTypeID, ref SubscriptionWayID, ref CreatedByUserID, ref IsActive
                                   , ref RegulatorCasesPracticeIDNameDictionary);
                        break;

                    }


            }

            if (IsFound)
                //we return new object of that User with the right data
                return new clsRegulator( PractitionerID,  PersonID,IsLawyer,  SubscriptionTypeID,  SubscriptionWayID,
                                          RegulatorID,  MemberShipNumber,  IssueDate,
                                          LastEditDate, LastEditByUserID,  CreatedByUserID,  IsActive,
                                          RegulatorCasesPracticeIDNameDictionary);

            throw new ArgumentNullException("No entity for \'Regulator\' was found in database that carry or fit with your input.");
            
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
                clsHelperClasses.WriteEventToLogFile("Problem while adding a new regulator to the system , review cls regulator class, " +
                    "\n"+ex.Message,System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine(ex.ToString());
                throw ex;

            }

            return false;

        }

        public async static Task<DataTable> GetAllRegulatorsAsync()
        {
            return await clsRegulatorData.GetAllRegulatorsAsync();
        }
        public  static DataTable GetAllRegulators()
        {
            return  clsRegulatorData.GetAllRegulators();
        }

        private static bool _DeletePermanently(int? RegulatorID)
     => clsRegulatorData.DeletePermanently(RegulatorID);

        private static bool _DeleteSoftly(int? RegulatorID)
               => clsRegulatorData.DeleteSoftly(RegulatorID);

        public static bool Delete(int? RegulatorID, enDeleteMode mode)
        {
            switch (mode)
            {
                case enDeleteMode.Permanently:
                    return _DeletePermanently(RegulatorID);

                case enDeleteMode.Softly:
                    return _DeleteSoftly(RegulatorID);

                default:
                    return false;
            }
        }


        public static bool IsRegulatorExist<T>(T Value, enSearchBy CheckBy)
        {
            switch (CheckBy)
            {

                case enSearchBy.RegulatorID:
                    {
                        if (int.TryParse(Value.ToString(), out int RegulatorID))
                        {

                            return clsRegulatorData.IsRegulatorExistByRegulatorID(RegulatorID);
                        }
                        else
                            return false;
                    }

                case enSearchBy.PersonID:
                    {
                        if (int.TryParse(Value.ToString(), out int PersonID))
                        {

                            return clsRegulatorData.IsRegulatorExistByPersonID(PersonID);
                        }
                        else
                        {
                            return false;
                        }

                    }

                case enSearchBy.MemberShipNumber:
                    {
                        string MemberShipNumberValue = Value.ToString();
                        if (!string.IsNullOrEmpty(MemberShipNumberValue))
                        {

                            return clsRegulatorData.IsRegulatorExistByMemberShipNumber(MemberShipNumberValue);
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enSearchBy.PractitionerID:
                    {
                        if (int.TryParse(Value.ToString(), out int PractitionerID))
                        {

                            return clsRegulatorData.IsRegulatorExistByPractitionerID(PractitionerID);
                        }
                        else
                        {
                            return false;
                        }

                    }


            }
            return false;
        }

        [Obsolete ("Not implemented well ,yet.")]
        public  static bool UpdateRegulatorList(int RegulatorID,int ListID, clsList.enListType listType)
        {

            return clsRegulatorData.UpdateRegulatorList(RegulatorID, ListID, (int)listType);
             
        }

        public  bool IsRegulatorInWhiteList()
        {
            //Data access , set the right type of practitioner 
            return clsWhiteList.IsPractitionerInWhiteList(this.PractitionerID, clsPractitioner.enPractitionerType.Regulatory);
        }
        public bool IsRegulatorInClosedList()
        {
            //Data access , set the right type of practitioner 
            return clsClosedList.IsPractitionerInClosedList(this.PractitionerID, clsPractitioner.enPractitionerType.Regulatory);
        }


    }

}

