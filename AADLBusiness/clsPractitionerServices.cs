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
                  practitionerProperties.IsLawyer,
                  practitionerProperties.SubscriptionTypeID,
                  practitionerProperties.SubscriptionWayID,
               
                  practitionerProperties.MemberShipNumber,
                  practitionerProperties.ShariaLicenseNumber,
                
                  practitionerProperties.IsRegulatorActive,
                  practitionerProperties.IsShariaActive,
                  practitionerProperties.IsExpertActive, 
                  practitionerProperties.IsJudgerActive,
                
                  practitionerProperties.RegulatorCreatedByUserName,
                  practitionerProperties.ShariaCreatedByUserName,
                  practitionerProperties.JudgerCreatedByUserName,
                  practitionerProperties.ExpertCreatedByUserName,
             
                  practitionerProperties.RegulatorIssueDate,
                  practitionerProperties.ShariaIssueDate,
                  practitionerProperties.JudgerIssueDate,
                  practitionerProperties.ExpertIssueDate,
               
                  practitionerProperties.RegulatorIssueDateFrom,
                  practitionerProperties.RegulatorIssueDateTo,
             
                  practitionerProperties.ShariaIssueDateFrom,
                  practitionerProperties.ShariaIssueDateTo,
                  
                  practitionerProperties.JudgerIssueDateFrom,
                  practitionerProperties.JudgerIssueDateTo,


                  practitionerProperties.ExpertIssueDateFrom,
                  practitionerProperties.ExpertIssueDateTo,

                  practitionerProperties.IsPractitionerInBlackList,
                  
                  practitionerProperties.IsRegulatoryInWhiteList,
                  practitionerProperties.IsRegulatoryInClosedList,

                  practitionerProperties.IsShariaInWhiteList,
                  practitionerProperties.IsShariaInClosedList,


                  practitionerProperties.IsJudgerInWhiteList,
                  practitionerProperties.IsJudgerInClosedList,

                  practitionerProperties.IsExpertInWhiteList,
                  practitionerProperties.IsExpertInClosedList

                  );

            return PractitionersDataTable;
        }
        public static DataTable GetAllPractitionersInfo()
        {
            return clsPractitionerData.GetAllPractitioners();
        }
    }

}
