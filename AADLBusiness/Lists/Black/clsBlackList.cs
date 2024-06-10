using AADLDataAccess.Lists.Black;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace AADLBusiness
{
    public class clsBlackList:clsList
    {
        public enum enFindBy { BlackListID, PractitionerID }
        public int BlackListID { get; set; }

        private Dictionary<int,string>_BlackListPractitionerReasonsIDNamesDictionary;

        public Dictionary<int, string>BlackListPractitionerReasonsIDNamesDictionary
        {
            get { return _BlackListPractitionerReasonsIDNamesDictionary; }

            set
            {
                if (value != null)
                {

                    _BlackListPractitionerReasonsIDNamesDictionary = value;
               
                }

                else
                {
                    throw  new ArgumentException("You can't assign this property with anything unless, you assign it with hash_set<int|string>");
                }

            }
        }
 
        public clsBlackList()
        {
            BlackListID = -1;
            BlackListPractitionerReasonsIDNamesDictionary = new Dictionary<int,string>();
            Mode = enMode.AddNew;
        }

        private clsBlackList (int ListID, string Notes,  DateTime AddedToListDate
          , int CreatedByUserID, DateTime? LastEditDate, int? LastEditByUserID, int blackListID, int PractitionerID,Dictionary<int, string> BlackListReasonsIdNamesDictionary)
        {
            this.ListID = ListID;
            this.Notes = Notes;
            this.AddedToListDate = AddedToListDate;
            this.CreatedByUserID = CreatedByUserID;
            this.LastEditDate = LastEditDate;
            this.LastEditByUserID = LastEditByUserID;
            this.BlackListID = blackListID;
            this.PractitionerID = PractitionerID;
            this.BlackListPractitionerReasonsIDNamesDictionary = BlackListReasonsIdNamesDictionary;
            Mode = enMode.Update;
        }

        protected override bool _AddNewList()
        {

            var pair= clsBlackListData.AddNewBlackList(Notes, AddedToListDate, CreatedByUserID,
               PractitionerID, BlackListPractitionerReasonsIDNamesDictionary);
            
            this.ListID = pair.NewListID;
            this.BlackListID = pair.NewBlackListID;
            
            return (this.ListID!=-1&&this.BlackListID != -1);

        }
        protected override bool _UpdateList()
        {

            return clsBlackListData.UpdateBlackList(this.BlackListID, this.Notes,this.PractitionerID, 
                 this.LastEditByUserID, BlackListPractitionerReasonsIDNamesDictionary);
               
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
        
        public  static clsBlackList Find(int? Value, enFindBy FindBy)
        {
            bool IsFound = false;

            int ListID = -1, CreatedByUserID = -1, BlackListID = -1, PractitionerID = -1;
            string Notes = "";
            DateTime AddedToListDate = DateTime.Now;
            DateTime? LastEditDate = null;
            int? LastEditByUserID = null;
            Dictionary<int,string> BlackListReasonsIdNamesDictionary = new Dictionary<int, string>();

            switch (FindBy)
            {
                case enFindBy.BlackListID:
                    {
                        if (int.TryParse(Value.ToString(), out BlackListID))
                        {
                            IsFound = clsBlackListData.AccessBlackListInfoByBlackListID(BlackListID, ref ListID, ref Notes, ref AddedToListDate, ref
                                CreatedByUserID,  ref LastEditByUserID, ref PractitionerID, ref BlackListReasonsIdNamesDictionary);


                            if (IsFound)
                                return new clsBlackList(ListID, Notes, AddedToListDate, CreatedByUserID, LastEditDate, LastEditByUserID, BlackListID,
                                    PractitionerID, BlackListReasonsIdNamesDictionary);

                        }

                        else
                        {
                            return null;
                        }


                        break;

                    }

                case enFindBy.PractitionerID:
                    {
                        if (int.TryParse(Value.ToString(), out PractitionerID))
                        {
                            IsFound = clsBlackListData.AccessBlackListInfoByPractitionerID(PractitionerID, ref BlackListID, ref ListID, ref Notes, ref AddedToListDate, ref
                                CreatedByUserID, ref LastEditByUserID, ref BlackListReasonsIdNamesDictionary);

                            if (IsFound)
                                return new clsBlackList(ListID, Notes, AddedToListDate, CreatedByUserID, LastEditDate, LastEditByUserID, BlackListID,
                                    PractitionerID, BlackListReasonsIdNamesDictionary);
                        }
                        else
                        {
                            return null;
                        }
                        break;

                    }

            }

            return null;

        }

        public  static bool DeleteList(int BlackListID)
        {
            return clsBlackListData.DeleteBlackList(BlackListID);

        }
  
        public static bool IsPractitionerInBlackList(int PractitionerID)
        {
            return clsBlackListData.IsPractitionerInBlackList(PractitionerID);
        }

    }

}
