using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class clsFormat
    {
        public static string DateToShort(DateTime Dt1)
        {
            
            return Dt1.ToString("dd/MMM/yyyy");
        }


        public (string firstName, string secondName, string thirdName, string lastName) ParseFullName(string fullName)
        {
            string[] nameParts = fullName.Split(' ');

            string firstName = nameParts.Length > 0 ? nameParts[0] : "";
            string secondName = nameParts.Length > 1 ? nameParts[1] : "";
            string thirdName = nameParts.Length > 2 ? nameParts[2] : "";
            string lastName = nameParts.Length > 3 ? nameParts[3] : "";

            return (firstName, secondName, thirdName, lastName);
        }


        
        public class CustomMessageBox
        {
       

            public static DialogResult Show(string message, string caption, string yesText, string noText, MessageBoxIcon icon)
            {
                // Set the current culture to Arabic
                CultureInfo currentCulture = new CultureInfo("ar-SA");
                CultureInfo.CurrentCulture = currentCulture;
                CultureInfo.CurrentUICulture = currentCulture;
                return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, icon, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

    }
}
