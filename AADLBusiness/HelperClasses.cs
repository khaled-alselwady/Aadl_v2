using AADLDataAccess;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace AADLBusiness
{ 
    public class AdvancedSearchPractitionerProperties
    {
        public enum enPropertiesName { FullName };
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool ?IsLawyer {  get; set; }
        public int? SubscriptionTypeID { get; set; }

        public int? SubscriptionWayID { get; set; }


        public bool? IsRegulatorActive { get; set; }
        public bool? IsShariaActive { get; set; }
        public bool? IsExpertActive { get; set; }
        public bool? IsJudgerActive { get; set; }

        public string MemberShipNumber { get; set; }
        public string ShariaLicenseNumber { get; set; }

        public DateTime? RegulatorIssueDate { get; set; }
        public DateTime? ShariaIssueDate { get; set; }
        public DateTime? JudgerIssueDate { get; set; }
        public DateTime? ExpertIssueDate { get; set; }

        public DateTime? RegulatorIssueDateFrom { get; set; }
        public DateTime? RegulatorIssueDateTo { get; set; }

        public DateTime? ShariaIssueDateFrom { get; set; }
        public DateTime? ShariaIssueDateTo { get; set; }

        public DateTime? JudgerIssueDateFrom { get; set; }
        public DateTime? JudgerIssueDateTo { get; set; }

        public DateTime? ExpertIssueDateFrom { get; set; }
        public DateTime? ExpertIssueDateTo { get; set; }


        public bool? IsPractitionerInBlackList { get; set; }

        public bool? IsRegulatoryInWhiteList { get; set; }
        public bool? IsRegulatoryInClosedList { get; set; }

        public bool? IsShariaInWhiteList { get; set; }
        public bool? IsShariaInClosedList { get; set; }

        public bool? IsJudgerInWhiteList { get; set; }
        public bool? IsJudgerInClosedList { get; set; }


        public bool? IsExpertInWhiteList { get; set; }
        public bool? IsExpertInClosedList { get; set; }

        public string RegulatorCreatedByUserName { get; set; }
        public string ShariaCreatedByUserName { get; set; }
        public string JudgerCreatedByUserName { get; set; }
        public string ExpertCreatedByUserName { get; set; }

         public AdvancedSearchPractitionerProperties() {
        }
     
         public AdvancedSearchPractitionerProperties(string fullName,
         string phoneNumber, string email, bool? IsLawyer,int? subscriptionTypeID, int? subscriptionWayID,
         bool? isRegulatorActive, bool? isShariaActive, bool? isExpertActive, bool? isJudgerActive,
         string memberShipNumber, string shariaLicenseNumber, 
         DateTime? regulatorIssueDate, DateTime? shariaIssueDate,
         DateTime? JudgerIssueDate, DateTime? ExpertIssueDate,
         DateTime? regulatorIssueDateFrom, DateTime? regulatorIssueDateTo,
         DateTime? shariaIssueDateFrom,DateTime? shariaIssueDateTo,
         DateTime? JudgerIssueDateFrom, DateTime? JudgerIssueDateTo,
         DateTime? ExpertIssueDateFrom, DateTime? ExpertIssueDateTo,
         bool? isPractitionerInBlackList, bool? isRegulatoryInWhiteList, bool? isRegulatoryInClosedList,
         bool? isShariaInWhiteList, bool? isShariaInClosedList,
          bool? isJudgerInWhiteList, 
          bool? isJudgerInClosedList,
          bool? isExpertInWhiteList,
          bool? isExpertInClosedList,
         string RegulatorCreatedByUserName, 
         string ShariaCreatedByUserName,
         string JudgerCreatedByUserName,string ExpertCreatedByUserName
         )
         {
          
            this.FullName = fullName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
          
            this.IsLawyer = IsLawyer;
            
            this.SubscriptionTypeID = subscriptionTypeID; 
            this.SubscriptionWayID = subscriptionWayID;
       
            this.IsRegulatorActive = isRegulatorActive;
            this.IsShariaActive = isShariaActive;
            this.IsExpertActive = isExpertActive;
            this.IsJudgerActive = isJudgerActive;
          
            this.MemberShipNumber = memberShipNumber;
            this.ShariaLicenseNumber = shariaLicenseNumber;
            
            this.RegulatorIssueDate = regulatorIssueDate;
            this.ShariaIssueDate = shariaIssueDate;
            this.JudgerIssueDate = JudgerIssueDate;
            this.ExpertIssueDate = ExpertIssueDate;

            this.RegulatorIssueDateFrom = regulatorIssueDateFrom;
            this.RegulatorIssueDateTo = regulatorIssueDateTo;
           
            this.ShariaIssueDateFrom = shariaIssueDateFrom;
            this.ShariaIssueDateTo = shariaIssueDateTo;

            this.JudgerIssueDateFrom =JudgerIssueDateFrom;
            this.JudgerIssueDateTo   =JudgerIssueDateTo;
            
            this.ExpertIssueDateFrom = ExpertIssueDateFrom;
            this.ExpertIssueDateTo = ExpertIssueDateTo;     
         
            this.IsPractitionerInBlackList = isPractitionerInBlackList;
          
            this.IsRegulatoryInWhiteList = isRegulatoryInWhiteList;
            this.IsRegulatoryInClosedList = IsRegulatoryInClosedList;
        
            this.IsShariaInWhiteList = IsShariaInWhiteList;
            this.IsShariaInClosedList = IsShariaInClosedList;

            this.IsJudgerInWhiteList = IsJudgerInWhiteList;
            this.IsJudgerInClosedList = IsJudgerInClosedList;

            this.RegulatorCreatedByUserName = RegulatorCreatedByUserName;
            this.ShariaCreatedByUserName = ShariaCreatedByUserName;
            this.JudgerCreatedByUserName = ShariaCreatedByUserName;
            this.ExpertCreatedByUserName = ShariaCreatedByUserName;


         }
    
    }

    public class clsHelperClasses
    {
        public static bool WriteEventToLogFile(string Message, EventLogEntryType eventLogEntryType)
        {

            string SourceName = "AADLAPPLICATION";

            try
            {

                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Application");
                    Console.WriteLine("Event source created");
                }

                EventLog.WriteEntry(SourceName, Message, eventLogEntryType);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return true;
        }
        public static (string firstName, string secondName, string thirdName, string lastName) ParseFullName(string fullName)
        {
            fullName = fullName.Trim();
            string[] nameParts = fullName.Split(' ');
            Console.WriteLine(nameParts.Length);
            string firstName = nameParts.Length > 0 ? nameParts[0] : "";
            string secondName = nameParts.Length > 1 ? nameParts[1] : "";
            string thirdName = nameParts.Length > 2 ? nameParts[2] : "";
            string lastName = nameParts.Length > 3 ? nameParts[3] : "";



            return (firstName, secondName, thirdName, lastName);
        }



    }

}
