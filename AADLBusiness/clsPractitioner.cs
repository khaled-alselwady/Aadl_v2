using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AADLBusiness;
using AADLDataAccess;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace AADLBusiness
{


    //OCP Design pattern was applied here...
    //The common  behaviour in the base class.
    //common behaviour that stays, that same must be encapsulated and abstracted away
    public abstract class clsPractitioner
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int PractitionerID { get; set; }
        public int PersonID { get; set; }
        public bool IsLawyer { get; set; }
        public  enum enPractitionerType{ Regulatory=1, Expert=2, Sharia=3,Judger=4}
        public int SubscriptionTypeID { get; set; }
        public int SubscriptionWayID { get; set; }
        public DateTime IssueDate { set; get; }
        public DateTime? LastEditDate { set; get; }
        public int? LastEditByUserID { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsActive { set; get; }
        public abstract clsSubscriptionType SubscriptionTypeInfo { get; }
        public abstract clsSubscriptionWay SubscriptionWayInfo { get; }
        public abstract clsUser UserInfo { get; }
        public abstract clsUser LastEditByUserInfo { get; }
        public abstract clsPerson SelectedPersonInfo { get; }
        public static bool IsPractitionerExists(int PractitionerID)
        {

            return clsPractitionerData.IsPractitionerExistByPractitionerID(PractitionerID); 
           
        }
        public  bool IsPractitionerInBlackList()
        {
            return clsBlackList.IsPractitionerInBlackList(this.PractitionerID);
        }
        protected abstract bool _AddNew();
        protected abstract bool _Update();
        public abstract bool Save();

    }

}
