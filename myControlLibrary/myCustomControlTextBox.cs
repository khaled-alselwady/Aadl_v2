using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myControlLibrary
{
    public partial class myCustomControlTextBox : TextBox
    {
        public myCustomControlTextBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public bool IsRequired { set;get; }

        public enum InputTypeEnum { TextInput,NumberInput,EmailInput}

        public InputTypeEnum InputType { set; get; }= InputTypeEnum.TextInput;


        private bool _IsNumeric()
        {
            Regex numericPattern = new Regex(@"^-?\d*\.?\d+$");

            return numericPattern.IsMatch(this.Text);
        }
        public  bool ValidateEmail()
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(this.Text.Trim());
        }
        public bool IsValid(ref string ProblemText)
        {
            if (IsRequired == true&&this.Text.Trim().Length==0)
            {
                ProblemText = "هذا الحقل مطلوب!";

                return false;
            }
            
            if(InputType == InputTypeEnum.NumberInput && this.Text.Trim().Length != 0)
            {
                if (!_IsNumeric())
                {
                    ProblemText = "يسمح فقط ان يكون النص بالارقام.";
                    return false;
                }
            }
     
            else if (InputType == InputTypeEnum.EmailInput)
            {
                if (!ValidateEmail())
                {
                    ProblemText = "صيغة عنوان البريد الإلكتروني غير صالحة";
                    return false;
                }

                else
                {
                    return true;
                }
            }

           

            return true;
        }


    }
}
