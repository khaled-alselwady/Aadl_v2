using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AADLDataAccess;

namespace AADLBusiness
{
    public class clsListReasons
    {

        public int ListReasonID { set; get; }
        public string ListReasonName { set; get; }

        private clsListReasons(int ListReasonID, string ListReasonName)
        {
            this.ListReasonID = ListReasonID;
                this.ListReasonName = ListReasonName;
        }
        public static DataTable GetAllBlackListReasons()
        {
            return clsListReasonsData.GetAllBlackListReasons();

        }
        public static DataTable GetAllWhiteListReasons()
        {
            return clsListReasonsData.GetAllWhiteListReasons();

        }
        public static DataTable GetAllClosedListReasons()
        {
            return clsListReasonsData.GetAllClosedListReasons();

        }
        public static clsListReasons Find(int ReasonID,clsList.enListType listType)
        {
            switch(listType)
            {
                case clsList.enListType.Black:
                    {
                        string BlackListReasonName = "";

                        if (clsListReasonsData.GetBlackListReasonNameByID(ReasonID,
                            ref BlackListReasonName))
                        {
                            return new clsListReasons(ReasonID, BlackListReasonName);
                        }
                        break;

                    }
                case clsList.enListType.White:
                    {
                        string WhiteListReasonName = "";

                        if (clsListReasonsData.GetWhiteListReasonNameByID(ReasonID,
                            ref WhiteListReasonName))
                        {
                            return new clsListReasons(ReasonID, WhiteListReasonName);
                        }
                        break;

                    }

            }
            return null;

        }
       
        //public static clsListReasons Find(string BlackListReasonName)
        //{
        //    int BlackListReasonID = -1;

        //    if (clsListReasonsData.GetBlackListIDByName(BlackListReasonName,
        //        ref BlackListReasonID))
        //    {
        //        return new clsListReasons(BlackListReasonID, BlackListReasonName);
        //    }
        //    return null;


        //}

    }

}
