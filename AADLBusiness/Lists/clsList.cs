using AADLDataAccess;
using AADLDataAccess.Lists.Black;
using Microsoft.Diagnostics.Tracing.Parsers.Tpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AADLBusiness.clsBlackList;

namespace AADLBusiness
{
    
    public abstract class clsList
    {
        public enum enListType { Black=1,White=2,Closed=3,RegulatoryWhite=4, RegulatoryClosed = 5,
            ShariaWhite , ShariaClosed, ExpertWhite, ExpertClosed, JudgerWhite, JudgerClosed
        };
        public clsPractitioner.enPractitionerType PractitionerType { get; set; }
        public enum enMode { AddNew, Update };
        protected  enMode Mode { set; get; }
        public int PractitionerID { get; set; }

        public int? ListID { get; set; }
        public string Notes { get; set; }

        public DateTime AddedToListDate { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime? LastEditDate { get; set; }

        public int? LastEditByUserID { get; set; }

        protected abstract bool _AddNewList();
        protected abstract bool _UpdateList();

        public abstract bool Save();


    }
}
