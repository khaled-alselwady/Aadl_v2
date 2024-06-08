using AADLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness
{
    public class clsSubscriptionType
    {
        public int SubscriptionTypeID { set; get; }
        public string SubscriptionName { set; get; }

        public clsSubscriptionType()
        {
            this.SubscriptionTypeID = -1;
            this.SubscriptionName = "";

        }

        private clsSubscriptionType(int SubscriptionTypeID, string SubscriptionName)

        {
            this.SubscriptionTypeID = SubscriptionTypeID;
            this.SubscriptionName = SubscriptionName;
        }

        public static clsSubscriptionType Find(int SubscriptionTypeID)
        {
            string SubscriptionName = "";

            if (clsSubscriptionTypeData.GetSubscriptionInfoByID(SubscriptionTypeID, ref SubscriptionName))

                return new clsSubscriptionType(SubscriptionTypeID, SubscriptionName);
            else
                return null;

        }

        public static clsSubscriptionType Find(string SubscriptionName)
        {

            int SubscriptionTypeID = -1;

            if (clsSubscriptionTypeData.GetSubscriptionInfoByName(SubscriptionName, ref SubscriptionTypeID))

                return new clsSubscriptionType(SubscriptionTypeID, SubscriptionName);
            else
                return null;

        }

        public static DataTable GetAllSubscriptionTypes()
        {
            return clsSubscriptionTypeData.GetAllSubscriptionTypes();

        }

        
    }
}
