using AADLBusiness.Lists.Closed;
using AADLDataAccess.Lists.Black;
using AADLDataAccess.Lists.Closed;
using AADLDataAccess.Lists.White;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness.Lists.WhiteList
{

    /// <summary>
    /// this class represents all white lists  types for practitioners
    /// by using enum(enMemberType) you are able to manipulate each practitioner
    /// base on his type.
    /// </summary>
    public class clsWhiteList:clsList
    {
        public enum enFindBy { WhiteListID, PractitionerID}
        public int? WhiteListID { get; set; }

        private Dictionary<int, string> _WhiteListPractitionerReasonsIDNamesDictionary;

        public Dictionary<int, string> WhiteListPractitionerReasonsIDNamesDictionary
        {
            get { return _WhiteListPractitionerReasonsIDNamesDictionary; }

            set
            {
                if (value != null)
                {

                    _WhiteListPractitionerReasonsIDNamesDictionary = value;

                }

                else
                {
                    throw new ArgumentException("You can't assign this property with anything unless, you assign it with Dictionary<int|string>");
                }

            }
        }

        public clsWhiteList()
        {
            WhiteListID =null;
            WhiteListPractitionerReasonsIDNamesDictionary = new Dictionary<int, string>();
            Mode = enMode.AddNew;
        }

        private clsWhiteList(int ListID, string Notes, DateTime AddedToListDate
          , int CreatedByUserID, DateTime? LastEditDate, int? LastEditByUserID, int WhiteListID, int PractitionerID,
            clsPractitioner.enPractitionerType PractitionerType,Dictionary<int, string> WhiteListReasonsIdNamesDictionary)
        {         

            this.ListID = ListID;
            this.Notes = Notes;
            this.AddedToListDate = AddedToListDate;
            this.CreatedByUserID = CreatedByUserID;
            this.LastEditDate = LastEditDate;
            this.LastEditByUserID = LastEditByUserID;
            this.WhiteListID = WhiteListID;
            this.PractitionerID = PractitionerID;
            this.PractitionerType =PractitionerType;
            this.WhiteListPractitionerReasonsIDNamesDictionary = WhiteListReasonsIdNamesDictionary;
            Mode = enMode.Update;

        }

        protected override bool _AddNewList()
        {

            var pair = clsWhiteListData.AddNewWhiteList(Notes, AddedToListDate, CreatedByUserID,
              (int) PractitionerID, (int)PractitionerType, WhiteListPractitionerReasonsIDNamesDictionary);

           
            ListID = pair.NewListID;
            WhiteListID = pair.NewWhiteListID;

            return (ListID != null && WhiteListID != null);

        }
        protected override bool _UpdateList()
        {
          
            return clsWhiteListData.UpdateWhiteList(ListID, Notes, LastEditByUserID, WhiteListID, PractitionerID,
                (int)PractitionerType, WhiteListPractitionerReasonsIDNamesDictionary);

        }
        public override bool Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:
                    {
                        if (_AddNewList())
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

                        return _UpdateList();
                    }
            }

            return false;

        }

        public  static bool DeleteList(int WhiteListID)
        {
            return clsWhiteListData.DeleteWhiteList(WhiteListID);
        }

        /// <summary>
        /// Find by closed-list ID 
        /// </summary>
        /// <param name="ClosedListID"></param>
        /// <returns>Closed list in case listID matches with any list</returns>
        public static clsWhiteList Find(int WhiteListID)
        {
            bool IsFound = false;
            int ListID = -1, CreatedByUserID = -1, PractitionerID = -1
            , PractitionerTypeID = -1;
            string Notes = "";
            DateTime AddedToListDate = DateTime.Now;
            DateTime? LastEditDate = null;
            int? LastEditByUserID = null;
            Dictionary<int, string> WhiteListReasonsIdNamesDictionary = new Dictionary<int, string>();


            IsFound = clsWhiteListData.AccessWhiteListInfoByWhiteListID(WhiteListID, ref ListID, ref Notes, ref AddedToListDate, ref
                CreatedByUserID, ref LastEditByUserID, ref PractitionerID, ref PractitionerTypeID, ref WhiteListReasonsIdNamesDictionary);


            if (IsFound)
            {
                return new clsWhiteList(ListID, Notes, AddedToListDate, CreatedByUserID, LastEditDate, LastEditByUserID, WhiteListID,
                    PractitionerID, (clsPractitioner.enPractitionerType)PractitionerTypeID, WhiteListReasonsIdNamesDictionary);
            }


            return null;

        }
        /// <summary>
        /// Find by practitioner ID , and its type ID (Regulator,expert,judger,sharia)
        /// </summary>
        /// <param name="PractitionerID">put Practitioner ID </param>
        /// <param name="practitionerType">type to distinguish between other types(Regulatory,Sharia,Experts,Judgement)</param>
        /// <returns>return while list based on both parameters</returns>
        public static clsWhiteList Find(int PractitionerID, clsPractitioner.enPractitionerType practitionerType)
        {
            bool IsFound = false;
            int ListID = -1, CreatedByUserID = -1, WhiteListID = -1;
            string Notes = "";
            DateTime AddedToListDate = DateTime.Now;
            DateTime? LastEditDate = null;
            int? LastEditByUserID = null;
            Dictionary<int, string> WhiteListReasonsIdNamesDictionary = new Dictionary<int, string>();


            IsFound = clsWhiteListData.AccessWhiteListInfoByPractitionerID(PractitionerID, (int)practitionerType, ref ListID, ref Notes, ref AddedToListDate, ref
                CreatedByUserID, ref LastEditByUserID, ref WhiteListID, ref WhiteListReasonsIdNamesDictionary);


            if (IsFound)
                return new clsWhiteList(ListID, Notes, AddedToListDate, CreatedByUserID, LastEditDate, LastEditByUserID, WhiteListID,
                    PractitionerID, practitionerType, WhiteListReasonsIdNamesDictionary);



            return null;

        }

        public static bool IsPractitionerInWhiteList(int PractitionerID,clsPractitioner.enPractitionerType practitionerType)
        {

            return clsWhiteListData.IsPractitionerInWhiteList(PractitionerID,(int)practitionerType);

        }
    
    }

}
