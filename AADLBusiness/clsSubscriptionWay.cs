using AADLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness
{
    public class clsSubscriptionWay
    {
        public int SubscriptionWayID { set; get; }
        public string SubscriptionName { set; get; }

        public clsSubscriptionWay()
        {
            this.SubscriptionWayID = -1;
            this.SubscriptionName = "";

        }

        private clsSubscriptionWay(int SubscriptionWayID, string SubscriptionName)

        {
            this.SubscriptionWayID = SubscriptionWayID;
            this.SubscriptionName = SubscriptionName;
        }

        public static clsSubscriptionWay Find(int SubscriptionWayID)
        {
            string SubscriptionName = "";

            if (clsSubscriptionWayData.GetSubscriptionInfoByID(SubscriptionWayID, ref SubscriptionName))

                return new clsSubscriptionWay(SubscriptionWayID, SubscriptionName);
            else
                return null;

        }

        public static clsSubscriptionWay Find(string SubscriptionName)
        {

            int SubscriptionWayID = -1;

            if (clsSubscriptionTypeData.GetSubscriptionInfoByName(SubscriptionName, ref SubscriptionWayID))

                return new clsSubscriptionWay(SubscriptionWayID, SubscriptionName);
            else
                return null;

        }

        public static DataTable GetAllSubscriptionWays()
        {
            return clsSubscriptionWayData.GetAllSubscriptionWays();

        }

    }
}
