using AADL.GlobalClasses;
using AADLBusiness;
using DVLD.Classes;
using MethodTimer;
using myControlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADL.People.controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private enum enFilterBy { FullName, NationalNo, PassportNo, PersonID };

        public EventHandler<PersonCompleteEventArgs> OnPersonComplete;

        public void RaiseOnPersonComplete(int? PersonID)
        {

            RaiseOnPersonComplete(new PersonCompleteEventArgs(PersonID));
        }

        protected virtual void RaiseOnPersonComplete(PersonCompleteEventArgs e)
        {
            if (OnPersonComplete != null)
            {
                OnPersonComplete(this, e);
            }
        }

        private bool _ShowAddPerson = true;

        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }

            set
            {
                _FilterEnabled = value;
                gpFilters.Enabled = _FilterEnabled;
            }

        }


        private int? _PersonID = null;

        public int? PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFilterBy.SelectedIndex = 0;
            ctbFilterValue.Text = PersonID.ToString();
            FindNow();

        }

        private void FindNow()
        {
            if (ctbFilterValue.Text.Length == 0)
            {
                MessageBox.Show("حقل البحث فارغ ,يرجى تعبئته قبل القيام بالبحث", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            switch (cbFilterBy.SelectedIndex)
            {
                case 0://الرقم التعريفي (personID) 
                    {

                        if (int.TryParse(ctbFilterValue.Text, out int PersonID))
                        {
                            ctrlPersonCard1.LoadPersonInfo(PersonID, ctrlPersonCard.LoadPersonBy.PersonID);
                        }
                        break;
                    }
            
                case 1://الاسم الكامل 
                    {
                        if (!string.IsNullOrEmpty(ctbFilterValue.Text))
                        {
                            string FullName = ctbFilterValue.Text.Trim();
                            ctrlPersonCard1.LoadPersonInfo(FullName, ctrlPersonCard.LoadPersonBy.FullName);
                        }
                        break;
                    }

                case 2://الرقم الوطني
                    {
                        if (!string.IsNullOrEmpty(ctbFilterValue.Text))
                        {
                            string NationalNo = ctbFilterValue.Text;
                            ctrlPersonCard1.LoadPersonInfo(NationalNo, ctrlPersonCard.LoadPersonBy.NationalNo);
                        }
                        break;
                    }

                case 3://رقم جواز السفر 
                    {
                        if (!string.IsNullOrEmpty(ctbFilterValue.Text))
                        {
                            string PassportNo = ctbFilterValue.Text;
                            ctrlPersonCard1.LoadPersonInfo(PassportNo, ctrlPersonCard.LoadPersonBy.PassportNo);
                        }
                        break;
                    }

                case 4://رقم الهاتف 
                    {
                        if (!string.IsNullOrEmpty(ctbFilterValue.Text))
                        {
                            string Phone = ctbFilterValue.Text;
                            ctrlPersonCard1.LoadPersonInfo(Phone, ctrlPersonCard.LoadPersonBy.Phone);
                        }
                        break;
                    }

                case 5://رقم الواتس اب 
                    {
                        if (!string.IsNullOrEmpty(ctbFilterValue.Text))
                        {
                            string WhatsApp = ctbFilterValue.Text;
                            ctrlPersonCard1.LoadPersonInfo(WhatsApp, ctrlPersonCard.LoadPersonBy.WhatsApp);
                        }
                        break;
                    }

                case 6://البريد الالكتروني 
                    {
                        if (!string.IsNullOrEmpty(ctbFilterValue.Text))
                        {
                            string Email = ctbFilterValue.Text;
                            ctrlPersonCard1.LoadPersonInfo(Email, ctrlPersonCard.LoadPersonBy.Email);
                        }
                        break;
                    }

                default:
                    break;

            }

            if (OnPersonComplete != null && FilterEnabled)
                // Raise the event with a parameter
                RaiseOnPersonComplete(ctrlPersonCard1.PersonID);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            ctbFilterValue.Text = "";
            ctbFilterValue.Focus();
            if (cbFilterBy.SelectedIndex == 0 || cbFilterBy.SelectedIndex == 4 || cbFilterBy.SelectedIndex == 5)
            {
                ctbFilterValue.IsRequired = true;
                ctbFilterValue.InputType = myCustomControlTextBox.InputTypeEnum.NumberInput;
                ctbFilterValue.MaxLength = 14;
            }
            else if (cbFilterBy.SelectedIndex == 6)
            {
                ctbFilterValue.IsRequired = true;
                ctbFilterValue.MaxLength = 25;
                ctbFilterValue.InputType = myCustomControlTextBox.InputTypeEnum.EmailInput;
            }
            else
            {

                ctbFilterValue.IsRequired = true;
                ctbFilterValue.InputType = myCustomControlTextBox.InputTypeEnum.TextInput;
                ctbFilterValue.MaxLength = 45;

            }

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            ctbFilterValue.Focus();
        }

        private void ctbFilterValue_Validating(object sender, CancelEventArgs e)
        {
            string ErrorMessage = "";
            if (!ctbFilterValue.IsValid(ref ErrorMessage))
            {
                errorProvider1.SetError(ctbFilterValue, ErrorMessage);
                e.Cancel = true;

            }
            else
            {
                errorProvider1.SetError(ctbFilterValue, "");
                e.Cancel = false;
            }

        }
    
        private void DataBackEvent(object sender, PersonCompleteEventArgs e)
        {
            // Handle the data received

            if (e != null)
            {
                cbFilterBy.SelectedIndex = 1;
                ctbFilterValue.Text = e.PersonID.ToString();
                ctrlPersonCard1.LoadPersonInfo(e.PersonID, ctrlPersonCard.LoadPersonBy.PersonID);
            }
        }

        public void FilterFocus()
        {
            ctbFilterValue.Focus();
        }

        [Time]
        private void btnFind_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (!this.ValidateChildren())
                {
                    //Here we dont continue becuase the form is not valid
                    MessageBox.Show("بعض الحقول غير صالحة! حرك الماوس فوق الرمز(أيقونة) الحمراء لرؤية الخطأ.", "خطأ التحقق من الصحة", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }


                FindNow();
            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Exception:\t"+ex.Message,System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("لقد حدث خطاء فني أثناء استرجاع البيانات", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void PerFormClick()
        {
            btnFind_Click_1(null, null);
        }
        [Time]
        private void btnAddNewPerson_Click_1(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();
            form.DataBackEventHandler += DataBackEvent;
            form.ShowDialog();
            //Send back the person Was added .

        }
        private void cbFilterBy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            errorProvider1.SetError(ctbFilterValue, null);
            ctbFilterValue.Text = "";
            ctbFilterValue.Focus();

        }

        private void ctbFilterValue_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }


            // personID&&  phone && whatsapp
            else if (cbFilterBy.SelectedIndex == 0 || cbFilterBy.SelectedIndex == 4 ||
                cbFilterBy.SelectedIndex == 5)
            {

                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            //Full name
            else if (cbFilterBy.SelectedIndex == 1)
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ' || ctbFilterValue.TextLength > 40;
            }

            //Passport/NationalNo
            else if (cbFilterBy.SelectedIndex == 2 || cbFilterBy.SelectedIndex == 3)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) || ctbFilterValue.TextLength > 11;
            }

            //Phone&&Whatsapp
            if (cbFilterBy.SelectedIndex == 4 ||
                cbFilterBy.SelectedIndex == 5)
            {
                e.Handled = ctbFilterValue.TextLength > 14 && !char.IsControl(e.KeyChar);
            }

        }


    }
}
