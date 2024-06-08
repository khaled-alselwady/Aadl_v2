using AADLDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLBusiness
{
    public static class clsPractitionerServices

    {

        public static DataTable Find(AdvancedSearchPractitionerProperties practitionerProperties)
        {
            // Use DataAccessLayer to perform the search
            DataTable PractitionersDataTable =
                  clsPractitionerData.GetAllPractitionersFitAdvanceProperties
                  (

                  practitionerProperties.FullName,
                  practitionerProperties.PhoneNumber,
                  practitionerProperties.Email,
                  practitionerProperties.ShariaLicenseNumber,
                  practitionerProperties.SubscriptionTypeID,
                  practitionerProperties.SubscriptionWayID,
                  practitionerProperties.MemberShipNumber,
                  practitionerProperties.IsRegulatorActive,
                  practitionerProperties.IsShariaActive,
                  practitionerProperties.RegulatorCreatedByUserName,
                  practitionerProperties.ShariaCreatedByUserName,
                  //practitionerProperties.IsExpertActive,
                  //practitionerProperties.IsJudgerActive,
                  practitionerProperties.RegulatorIssueDate,
                  practitionerProperties.ShariaIssueDate,
                  practitionerProperties.RegulatorIssueDateFrom,
                  practitionerProperties.RegulatorIssueDateTo,
                  practitionerProperties.ShariaIssueDateFrom,
                  practitionerProperties.ShariaIssueDateTo,
                  practitionerProperties.IsPractitionerInBlackList,
                  practitionerProperties.IsInRegulatoryWhiteList,
                  practitionerProperties.IsInRegulatoryClosedList,
                  practitionerProperties.IsInShariaWhiteList,
                  practitionerProperties.IsInShariaClosedList

              );

            return PractitionersDataTable;
        }
        public static DataTable GetAllPractitionersInfo()
        {
            return clsPractitionerData.GetAllPractitioners();
        }
    }

}
