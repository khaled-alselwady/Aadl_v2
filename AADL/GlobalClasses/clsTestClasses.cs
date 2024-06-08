using AADLBusiness.Sharia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADL.GlobalClasses
{
    internal class clsTestClasses
    {

        internal abstract class ShariaDataBaseInterface
        {

            public abstract clsSharia Find(int lawyerID);
        }


        public class ShariaDataBaseTest:ShariaDataBaseInterface
        {

            public override clsSharia Find(int lawyerID)
            {

                return null;
            }

        }

    }
}
