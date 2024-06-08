using AADLDataAccess.Lists.Closed;
using AADLDataAccess.Lists.White;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness.Lists.Closed
{
    public class clsClosedList : clsList
    {
        public enum enFindBy { ClosedList, PractitionerID }
        public int? ClosedListID { get; set; }

        private Dictionary<int, string> _ClosedListPractitionerReasonsIDNamesDictionary;

        public Dictionary<int, string> ClosedListPractitionerReasonsIDNamesDictionary
        {
            get { return _ClosedListPractitionerReasonsIDNamesDictionary; }

            set
            {
                if (value != null)
                {

                    _ClosedListPractitionerReasonsIDNamesDictionary = value;

                }

                else
                {
                    throw new ArgumentException("You can't assign this property with anything unless, you assign it with Dictionary<int|string>");
                }

            }
        }

        public clsClosedList()
        {
            ClosedListID = null;
            ClosedListPractitionerReasonsIDNamesDictionary = new Dictionary<int, string>();
            Mode = enMode.AddNew;
        }

        private clsClosedList(int ListID, string Notes, DateTime AddedToListDate
          , int CreatedByUserID, DateTime? LastEditDate, int? LastEditByUserID, int ClosedListID, int PractitionerID,
            clsPractitioner.enPractitionerType PractitionerType, Dictionary<int, string> ClosedListReasonsIdNamesDictionary)
        {

            this.ListID = ListID;
            this.Notes = Notes;
            this.AddedToListDate = AddedToListDate;
            this.CreatedByUserID = CreatedByUserID;
            this.LastEditDate = LastEditDate;
            this.LastEditByUserID = LastEditByUserID;
            this.ClosedListID = ClosedListID;
            this.PractitionerID = PractitionerID;
            this.PractitionerType = PractitionerType;
            this.ClosedListPractitionerReasonsIDNamesDictionary = ClosedListReasonsIdNamesDictionary;
            Mode = enMode.Update;

        }

        protected override bool _AddNewList()
        {

            var pair = clsClosedListData.AddNewClosedList(Notes, AddedToListDate, CreatedByUserID,
              (int)PractitionerID, (int)PractitionerType, ClosedListPractitionerReasonsIDNamesDictionary);


            ListID = pair.NewListID;
            ClosedListID = pair.NewClosedListID;
            
            return (ListID != null && ClosedListID!= null);

        }
        protected override bool _UpdateList()
        {

            return clsClosedListData.UpdateClosedList(ListID, Notes, LastEditByUserID, ClosedListID, PractitionerID,
                (int)PractitionerType, ClosedListPractitionerReasonsIDNamesDictionary);

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

        public  static bool DeleteList(int ClosedListID)
        {
            return clsClosedListData.DeleteClosedList(ClosedListID);
        }

        /// <summary>
        /// Find by practitioner ID , and its type ID (Regulator,expert,judger,sharia)
        /// </summary>
        /// <param name="PractitionerID">put Practitioner ID </param>
        /// <param name="practitionerType">type to distinguish between other types</param>
        /// <returns></returns>
        public static clsClosedList Find(int PractitionerID,clsPractitioner.enPractitionerType practitionerType)
        {
            bool IsFound = false;
            int ListID = -1, CreatedByUserID = -1, ClosedListID = -1;
            string Notes = "";
            DateTime AddedToListDate = DateTime.Now;
            DateTime? LastEditDate = null;
            int? LastEditByUserID = null;
            Dictionary<int, string> ClosedListReasonsIdNamesDictionary = new Dictionary<int, string>();

      
                            IsFound = clsClosedListData.AccessClosedListInfoByPractitionerID(PractitionerID,(int) practitionerType, ref ListID, ref Notes, ref AddedToListDate, ref
                                CreatedByUserID, ref LastEditByUserID, ref ClosedListID, ref ClosedListReasonsIdNamesDictionary);


                            if (IsFound)
                                return new clsClosedList(ListID, Notes, AddedToListDate, CreatedByUserID, LastEditDate, LastEditByUserID, ClosedListID,
                                    PractitionerID,practitionerType, ClosedListReasonsIdNamesDictionary);
                    


            return null;

        }

        /// <summary>
        /// Find by closed-list ID 
        /// </summary>
        /// <param name="ClosedListID"></param>
        /// <returns>Closed list in case listID matches with any list</returns>
        public static clsClosedList Find(int ClosedListID)
        {
            bool IsFound = false;
            int ListID = -1, CreatedByUserID = -1, PractitionerID = -1
            , PractitionerTypeID = -1;
            string Notes = "";
            DateTime AddedToListDate = DateTime.Now;
            DateTime? LastEditDate = null;
            int? LastEditByUserID = null;
            Dictionary<int, string> ClosedListReasonsIdNamesDictionary = new Dictionary<int, string>();

                        
                            IsFound = clsClosedListData.AccessClosedListInfoByClosedListID(ClosedListID, ref ListID, ref Notes, ref AddedToListDate, ref
                                CreatedByUserID, ref LastEditByUserID, ref PractitionerID, ref PractitionerTypeID, ref ClosedListReasonsIdNamesDictionary);


                            if (IsFound)
                            {
                                return new clsClosedList(ListID, Notes, AddedToListDate, CreatedByUserID, LastEditDate, LastEditByUserID, ClosedListID,
                                    PractitionerID, (clsPractitioner.enPractitionerType)PractitionerTypeID, ClosedListReasonsIdNamesDictionary);
                            }


            return null;

        }

        public static bool IsPractitionerInClosedList(int PractitionerID, clsPractitioner.enPractitionerType practitionerType)
        {

            return clsClosedListData.IsPractitionerInClosedList(PractitionerID, (int)practitionerType);

        }

    }

}
