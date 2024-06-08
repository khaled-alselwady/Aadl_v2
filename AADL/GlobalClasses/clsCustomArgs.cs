using AADLBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADL.GlobalClasses
{
    
   
    
        public class PersonCompleteEventArgs
        {
            public int? PersonID { get; }

            public clsPerson PersonInfo { get; }

        /// <summary>
        /// Constructor to send PersonId if exists , 
        /// </summary>
        /// <param name="PersonID">Person Id relate to clsPerson class</param>
        /// <param name="FindPersonInfo">A choice to find person object too or just send back Person ID .</param>
            public PersonCompleteEventArgs(int? PersonID,bool FindPersonInfo=false)
            {
                this.PersonID = PersonID;
            if(FindPersonInfo)
            {

                PersonInfo = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);
            }
            }

        } 
    }
