using AADL.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using AADLBusiness;
using MethodTimer;
using AADL.GlobalClasses;
namespace AADL.People
{
    public partial class ctrlPersonCard : UserControl
    {

        public enum LoadPersonBy { PersonID,NationalNo,PassportNo,FullName,Phone,WhatsApp,Email};
        private clsPerson _Person;

        private int? _PersonID = null;

        public int? PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private  void _LoadPersonInfo(object sender, PersonCompleteEventArgs e)
        {
            _PersonID = e.PersonID;
            _Person = e.PersonInfo;
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("لا يوجد شخص بهذا البيانات. = " , "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();

        }

        [Time]
        public void LoadPersonInfo<T>(T Value, LoadPersonBy enLoadPersonBy)
        {

            switch (enLoadPersonBy)
            {
                case LoadPersonBy.PersonID:
                    {

                        if (Value != null && int.TryParse(Value.ToString(), out int PersonID))
                        {
                            _Person = clsPerson.Find(PersonID, clsPerson.enSearchBy.PersonID);
                            if (_Person == null)
                            {
                                ResetPersonInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + PersonID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;
                    }

                case LoadPersonBy.NationalNo:
                    {
                        string NationalNo = Value.ToString();
                        if (!string.IsNullOrEmpty(NationalNo))
                        {
                            _Person = clsPerson.Find(NationalNo, clsPerson.enSearchBy.NationalNo);
                            if (_Person == null)
                            {
                                ResetPersonInfo();
                                MessageBox.Show("لا يوجد شخص يحمل الرقم الوطني  =" + NationalNo.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;

                    }

                case LoadPersonBy.PassportNo:
                    {
                        string PassportNo = Value.ToString();
                        if (!string.IsNullOrEmpty(PassportNo))
                        {
                            _Person = clsPerson.Find(PassportNo, clsPerson.enSearchBy.PassportNo);
                            if (_Person == null)
                            {
                                ResetPersonInfo();
                                MessageBox.Show("لا يوجد شخص يحمل رقم جواز السفر  = " + PassportNo.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;


                    }

                case LoadPersonBy.FullName:
                    {
                        
                        string FullName = Value.ToString();
                    
                        if (!string.IsNullOrEmpty(FullName))
                        {
                  
                         
                            _Person = clsPerson.Find(FullName, clsPerson.enSearchBy.FullName);
                            if (_Person == null)
                            {
                                ResetPersonInfo();
                                MessageBox.Show("لا يوجد شخص بهذا الاسم. = " + FullName.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }


                        }

                        break;


                    }

                case LoadPersonBy.Phone:
                    {
                        string Phone = Value.ToString();
                        if (!string.IsNullOrEmpty(Phone))
                        {
                            _Person = clsPerson.Find(Phone, clsPerson.enSearchBy.Phone);
                            if (_Person == null)
                            {
                                ResetPersonInfo();
                                MessageBox.Show("لا يوجد شخص يحمل رقم الهاتف. = " + Phone.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;


                    }

                case LoadPersonBy.WhatsApp:
                    {
                        string WhatsApp = Value.ToString();
                        if (!string.IsNullOrEmpty(WhatsApp))
                        {
                            _Person = clsPerson.Find(WhatsApp, clsPerson.enSearchBy.WhatsApp);
                            if (_Person == null)
                            {
                                ResetPersonInfo();
                                MessageBox.Show("لا يوجد شخص يحمل رقم الواتس اب. = " + WhatsApp.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;


                    }
                
                case LoadPersonBy.Email:
                    {
                        string Email = Value.ToString();
                        if (!string.IsNullOrEmpty(Email))
                        {
                            _Person = clsPerson.Find(Email, clsPerson.enSearchBy.Email);
                            if (_Person == null)
                            {
                                ResetPersonInfo();
                                MessageBox.Show("لا يوجد شخص يحمل البريد الالكتروني. = " + Email.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;


                    }


            }

            _FillPersonInfo();

        }
       
        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
            try
            {

            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo!=""?_Person.NationalNo: "[????]";
            lbPassportNo.Text = _Person.PassportNo != "" ? _Person.PassportNo : "[????]";
            lblFullName.Text = _Person.FullName;
            lbGender.Text = _Person.Gender.HasValue ? (_Person.Gender ==0? "ذكر" : "أنثى"): "[????]";
            lbDateOfBirth.Text = _Person.DateOfBirth.HasValue ? _Person.DateOfBirth.Value.ToShortDateString() : "[????]";
            lbAddress.Text = _Person.Address != "" ? _Person.Address : "[????]";
            lbPhone.Text = _Person.Phone;
            lbWhatsApp.Text=_Person.WhatsApp != "" ? _Person.WhatsApp : "[????]";
            lbEmail.Text = _Person.Email;
            lbCountry.Text = _Person.CountryInfo.CountryName;
            lbCity.Text = _Person.CityInfo.CityName;
            lbIssueDate.Text = _Person.IssueDate.ToShortDateString();
               
            //Edit and create 

            lbCreatedByUserID.Text = _Person.UserInfo.UserName;

            }
            catch (Exception ex)
            {
                MessageBox.Show(" بعض الحقول قد تكون فارغة بسسب مشكلة داخلية اثناء التحميل", "فشل",MessageBoxButtons.OK,MessageBoxIcon.Error);
                clsHelperClasses.WriteEventToLogFile("Error at card person info ctrl , while loading properties of person info ,\n" +
                    "some properties drop exception:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }

            _LoadPersonImage();


        }

        public void ResetPersonInfo()
        {
         
            _PersonID = null;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lbPassportNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGender.Image = Resources.Man_32;
            lbGender.Text = "[????]";
            lbDateOfBirth.Text = "[????]";
            lbAddress.Text = "[????]";
            lbPhone.Text = "[????]";
            lbWhatsApp.Text = "[????]";
            lbEmail.Text = "[????]";
            lbCountry.Text = "[????]";
            lbCity.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
            llEditPersonInfo.Enabled = false;
            
            //Edit and create.

            lbCreatedByUserID.Text = "[????]";
            lbIssueDate.Text = "[????]";

        }

      
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int ID = (int)_PersonID;

            frmAddUpdatePerson frm = new frmAddUpdatePerson(ID);
            frm.ShowDialog();

            //refresh
            LoadPersonInfo(_PersonID.Value, LoadPersonBy.PersonID);
        }

    }
}
