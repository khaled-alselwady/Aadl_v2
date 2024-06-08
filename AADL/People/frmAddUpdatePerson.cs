using AADL.GlobalClasses;
using AADL.Properties;
using AADLBusiness;
using CommandLine;
using DVLD.Classes;
using FluentValidation.Results;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using myControlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADL.People
{
    public partial class frmAddUpdatePerson : Form
    {

        // Declare a delegate
        public event EventHandler<PersonCompleteEventArgs> DataBackEventHandler;
        public delegate void EventUpdatePeopleInfo();

        public static Action OnPeopleUpdated { get; set; }
        public enum enMode { AddNew, Update }

        private enMode _Mode;
        public enum enGender { Male = 0, Female = 1 };

        private bool _isValidateChildrenCall = false;

        private int? _PersonID = null; //Update Mode.

        private clsPerson _Person;
        public frmAddUpdatePerson()
        {
            _Mode = enMode.AddNew;
            InitializeComponent();
        }
        public frmAddUpdatePerson(int PersonID)
        {
            _Mode = enMode.Update;
            _PersonID = PersonID;

            InitializeComponent();
        }

        private string _BuildFullName()
        {

            StringBuilder fullNameBuilder = new StringBuilder();

            // Append first ,second,third and last name.

            fullNameBuilder.Append(ctbFirstName.Text.Trim());
            fullNameBuilder.Append(" ").Append(ctbSecondName.Text.Trim());
            fullNameBuilder.Append(" ").Append(ctbThirdName.Text.Trim());
            fullNameBuilder.Append(" ").Append(ctbLastName.Text.Trim());


            return fullNameBuilder.ToString().Trim();

        }
        private bool IsFullNameValidated(string FullName)
        {

            if (FullName != _Person.FullName && clsPerson.IsPersonExist(FullName, clsPerson.enSearchBy.FullName))
            {
                return false;
            }
            return true;

        }
        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException ex)
                    {
                        clsGlobal.WriteEventToLogFile(ex.Message, EventLogEntryType.Error);
                        MessageBox.Show("هناك خطاء ما يحدث عند تحميل الصورة.", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }


        private void _FillCountriesInComoboBox()
        {

            DataTable dataTableCountries = clsCountry.GetAllCountries();
            cbCountry.DataSource = dataTableCountries;
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
            cbCountry.SelectedValue = 102;

        }

        private async void _FillCitiesInComoboBoxAsync()
        {
            if (int.TryParse(cbCountry.SelectedValue.ToString(), out int CountryID))
            {

                DataTable dataTableCities = await clsCity.GetAllCitiesByCountryIDAsync(CountryID);
                if (dataTableCities.Rows.Count > 0)
                {
                    cbCity.DataSource = dataTableCities;
                    cbCity.DisplayMember = "CityName";
                    cbCity.ValueMember = "CityID";
                    cbCity.SelectedValue = 1;
                }

            }

        }
        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID, clsPerson.enSearchBy.PersonID);

            if (_Person == null)
            {
                MessageBox.Show("لا يوجد شخص بهذا الرقم التعريفي = " + _PersonID, "عدم ايجاد البيانات الشخصية", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblPersonID.Text = _PersonID.ToString();
            ctbFirstName.Text = _Person.FirstName;
            ctbSecondName.Text = _Person.SecondName;
            ctbThirdName.Text = _Person.ThirdName;
            ctbLastName.Text = _Person.LastName;
            ctbNationalNo.Text = _Person.NationalNo;
            ctbPassportNo.Text = _Person.PassportNo;
            if (_Person.DateOfBirth.HasValue)
            {
                dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
                dtpDateOfBirth.Value = _Person.DateOfBirth.Value;
                cbCancel.Visible = true;
                cbCancel.Checked = false;
                dtpDateOfBirth.Tag = null;

            }
            else
            {
                dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
                dtpDateOfBirth.CustomFormat = " ";
            }
            if (_Person.Gender == 0)
                rbtnFemale.Checked = true;
            else
                rbtnFemale.Checked = true;

            ctbAddress.Text = _Person.Address;
            ctbPhone.Text = _Person.Phone;
            ctbWhatsApp.Text = _Person.WhatsApp;
            ctbEmail.Text = _Person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
            cbCity.SelectedIndex = cbCity.FindString(_Person.CityInfo.CityName);

            //load person image in case it was set.
            if (_Person.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;

            }

            //hide/show the remove link  in case there is no image for the person.
            llRemoveImage.Visible = (_Person.ImagePath != "");

        }

        private void ResetDefaultValues()
        {
            //this will initialize the reset the defaule values
            _FillCountriesInComoboBox();
            _FillCitiesInComoboBoxAsync();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "اضافة شخص جديد";
                _Person = new clsPerson();
            }

            else
            {
                lblTitle.Text = "و تعديل و تحديث";
            }

            //set default image for the person.
            if (rbtnMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            //hide/show the remove linke incase there is no image for the person.
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to Jordan.
            cbCountry.SelectedValue = 102;

            ctbFirstName.Text = "";
            ctbSecondName.Text = "";
            ctbThirdName.Text = "";
            ctbLastName.Text = "";
            ctbNationalNo.Text = "";
            ctbPassportNo.Text = "";
            rbtnMale.Checked = true;
            ctbEmail.Text = "";
            ctbPhone.Text = "";
            ctbWhatsApp.Text = "";
            ctbAddress.Text = "";
            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.CustomFormat = " ";
            dtpDateOfBirth.Tag = null;
            cbCancel.Visible = false;
            cbCancel.Checked = false;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            myCustomControlTextBox textBox = sender as myCustomControlTextBox;
            if (textBox != null && textBox == ctbPhone || textBox == ctbWhatsApp)
            {

                e.Handled = !char.IsControl(e.KeyChar) && textBox.TextLength > 14 || !char.IsDigit(e.KeyChar);

            }
            else if (textBox != null && textBox == ctbFirstName || textBox == ctbSecondName ||
                textBox == ctbThirdName || textBox == ctbLastName)
            {

                e.Handled = textBox.TextLength > 15 || !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);

            }
            else if (textBox != null && textBox == ctbNationalNo || textBox != null && textBox == ctbPassportNo)
            {
                e.Handled = textBox.TextLength > 12 || !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
            }


        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            //validate person Data First .
            _isValidateChildrenCall = true;
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("بعض الحقول غير صالحة! ضع الماوس فوق الأيقونة(الأيقونات) الحمراء لرؤية الخطأ",
                    "خطاء في البيانات المدخلة", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _isValidateChildrenCall = false;

            if (!_HandlePersonImage())
                return;

            int NationalityCountryID = (int)cbCountry.SelectedValue;
            int CityID = (int)cbCity.SelectedValue;

            _Person.FirstName = ctbFirstName.Text.Trim();
            _Person.SecondName = ctbSecondName.Text.Trim();
            _Person.ThirdName = ctbThirdName.Text.Trim();
            _Person.LastName = ctbLastName.Text.Trim();
            _Person.NationalNo = ctbNationalNo.Text.Trim();
            _Person.PassportNo = ctbPassportNo.Text.Trim();
            _Person.Email = ctbEmail.Text.Trim();
            _Person.Phone = ctbPhone.Text.Trim();
            _Person.WhatsApp = ctbWhatsApp.Text.Trim();
            _Person.Address = ctbAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Tag == null ? (DateTime?)null : dtpDateOfBirth.Value;
            _Person.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Person.CountryID = NationalityCountryID;
            _Person.CityID = CityID;
            _Person.IsActive = true;
            _Person.IssueDate = DateTime.Now;

            if (rbtnMale.Checked)
                _Person.Gender = (short)enGender.Male;
            else
                _Person.Gender = (short)enGender.Female;


            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            try
            {

                if (_Person.Save())
                {
                    lblPersonID.Text = _Person.PersonID.ToString();
                    //change form mode to update.
                    _Mode = enMode.Update;
                    lblTitle.Text = "تحديث و تعديل البيانات الشخصية";

                    MessageBox.Show("تم الحفظ بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Trigger the event to send data back to the caller form.
                    if (DataBackEventHandler != null)
                    {
                        DataBackEventHandler(null, new PersonCompleteEventArgs(_Person.PersonID));
                    }

                    if (OnPeopleUpdated != null)
                    {
                        OnPeopleUpdated();
                    }
                    clsGlobal.WriteEventToLogFile($"{clsGlobal.CurrentUser.UserName}, Save a new person to the system. with ID: " + _Person.PersonID
                    , EventLogEntryType.Information);

                }
                else
                {
                    MessageBox.Show("خطاء : لم يتم حفظ البيانات بشكل صحيح.", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("حدث خطاء فني اثناء عملية تحديث البيانات", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGlobal.WriteEventToLogFile(ex.Message + "\nthis Exception dropped from adding updating person form.",
                    EventLogEntryType.Error);
                clsGlobal.WriteEventToLogFile($"{clsGlobal.CurrentUser.UserName}, try save a new person to the system , but something went wrong" +
                    "that led an exception to be thrown  with this message:\n" + ex.Message, EventLogEntryType.Error);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    
        private void tbWhatsApp_Validating(object sender, CancelEventArgs e)
        {

            if (!string.IsNullOrEmpty(ctbWhatsApp.Text) &&
               ctbWhatsApp.Text != _Person.WhatsApp && clsPerson.IsPersonExist(ctbWhatsApp.Text, clsPerson.enSearchBy.WhatsApp))
            {
                errorProvider1.SetError(ctbWhatsApp, ".رقم الواتس اب مستخدم من قبل");
                e.Cancel = true;
                DialogResult result = clsFormat.CustomMessageBox.Show("يوجد تطابق في رقم الواتس اب مع شخص اخر هل تريد الذهاب الي ملفه؟", "تأكيد", "نعم", "لا", MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User clicked "نعم"
                    frmPersonInfo Form = new frmPersonInfo(clsPerson.Find(ctbWhatsApp.Text, clsPerson.enSearchBy.WhatsApp).PersonID);
                    Form.ShowDialog();
                }
                return;

            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctbWhatsApp, "");
            }




        }
        private void ctbPassportNo_Validating(object sender, CancelEventArgs e)
        {


            if (!string.IsNullOrEmpty(ctbPassportNo.Text) && ctbPassportNo.Text != _Person.PassportNo
                && clsPerson.IsPersonExist(ctbPassportNo.Text, clsPerson.enSearchBy.PassportNo))
            {
                errorProvider1.SetError(ctbPassportNo, ".رقم جواز السفر مستخدم من قبل");
                e.Cancel = true;


                DialogResult result = clsFormat.CustomMessageBox.Show("يوجد تطابق في رقم جواز السفر مع شخص اخر هل تريد الذهاب الي ملفه؟", "تأكيد", "نعم", "لا", MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User clicked "نعم"
                    frmPersonInfo Form = new frmPersonInfo(clsPerson.Find(ctbPassportNo.Text, clsPerson.enSearchBy.PassportNo).PersonID);
                    Form.ShowDialog();
                }

            }

            else
            {

                errorProvider1.SetError(ctbPassportNo, "");
            }

        }

        private void ctbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string ErrorMessage = "";
               
                if (ctbNationalNo.IsValid(ref ErrorMessage))
                {

                    errorProvider1.SetError(ctbNationalNo, ErrorMessage);
                    e.Cancel = true;

                }

                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(ctbNationalNo, "");
                }
                 if (ctbNationalNo.Text != _Person.NationalNo && clsPerson.IsPersonExist(ctbNationalNo.Text, clsPerson.enSearchBy.NationalNo))
                 {
                    errorProvider1.SetError(ctbNationalNo, ErrorMessage);
                    e.Cancel = true;

                    DialogResult result = clsFormat.CustomMessageBox.Show("يوجد تطابق في رقم الوطني مع شخص اخر هل تريد الذهاب الي ملفه؟", "تأكيد", "نعم", "لا", MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // User clicked "نعم"
                        frmPersonInfo Form = new frmPersonInfo(clsPerson.Find(ctbNationalNo.Text, clsPerson.enSearchBy.NationalNo).PersonID);
                        Form.ShowDialog();
                    }
                    return;

                 }

                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(ctbNationalNo, "");
                }
            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Exception in add update from national valditing:\t" + ex.Message, EventLogEntryType.Error);
                    MessageBox.Show("Exception happend:\n" + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCity.DataSource = null;
            cbCity.SelectedIndex = -1;
            _FillCitiesInComoboBoxAsync();

        }

        private void _LoadDuplicatedFullNamePersonInfo(object sender, PersonCompleteEventArgs e)
        {
            using (frmPersonInfo form = new frmPersonInfo(e.PersonID))
            {
                form.ShowDialog();
            }

        }
        private void HandleDuplicatedFullName(string FullName)
        {

            DialogResult result = MessageBox.Show("يوجد هناك اسماء متشابهة, هل تريد الاختيار بينها؟", "سؤال", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (DialogResult.Yes == result)
            {
                using (FrmDuplicatedPersonFullNames form = new FrmDuplicatedPersonFullNames(FullName))
                {

                    FrmDuplicatedPersonFullNames.OnPersonSelected += _LoadDuplicatedFullNamePersonInfo;
                    form.ShowDialog();
                }
            }

            else
            {

                return;
            }

        }

        private void textBoxName_validating(object sender, CancelEventArgs e)
        {

            myCustomControlTextBox textBox = sender as myCustomControlTextBox;
            string ErrorMessage = "";

            if(!textBox.IsValid(ref ErrorMessage))
            {
                errorProvider1.SetError(textBox, ErrorMessage);
                e.Cancel = true;
                return;
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox, null);
            }

            string FullName = _BuildFullName();

            if (!_isValidateChildrenCall && !IsFullNameValidated(FullName))
            {
                errorProvider1.SetError(ctbFirstName, "الاسم الرباعي  (مستخدم) من قبل.");
                errorProvider1.SetError(ctbSecondName, "الاسم الرباعي  (مستخدم) من قبل.");
                errorProvider1.SetError(ctbThirdName, "الاسم الرباعي  (مستخدم) من قبل.");
                errorProvider1.SetError(ctbLastName, "الاسم الرباعي  (مستخدم) من قبل.");

                DialogResult result = clsFormat.CustomMessageBox.Show("يوجد تطابق بالاسم الرباعي مع شخص اخر هل تريد الذهاب الي ملفه؟", "تأكيد", "نعم", "لا", MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (clsPerson.IsFullNameDuplicated(FullName))
                    {

                        HandleDuplicatedFullName(FullName);

                    }
                    else
                    {

                        frmPersonInfo Form = new frmPersonInfo(clsPerson.Find(FullName, clsPerson.enSearchBy.FullName).PersonID);
                        Form.ShowDialog();
                    }

                }

                return;

            }

            else
            {

                errorProvider1.SetError(ctbFirstName, "");
                errorProvider1.SetError(ctbSecondName, "");
                errorProvider1.SetError(ctbThirdName, "");
                errorProvider1.SetError(ctbLastName, "");

            }



        }


        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {

            string ErrorMessage = "";

            if (!ctbEmail.IsValid(ref ErrorMessage))
            {
                errorProvider1.SetError(ctbEmail, ErrorMessage);
                e.Cancel = true;
                return;
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctbEmail, "");

            }


            if (!string.IsNullOrEmpty(ctbEmail.Text) && ctbEmail.Text != _Person.Email && clsPerson.IsPersonExist(ctbEmail.Text, clsPerson.enSearchBy.Email))
            {
                errorProvider1.SetError(ctbEmail, "البريد الالكتروني مستخدم من قبل.");
                e.Cancel = true;
                DialogResult result = clsFormat.CustomMessageBox.Show("يوجد تطابق في  البريد الالكتروني  مع شخص اخر هل تريد الذهاب الي ملفه؟", "تأكيد", "نعم", "لا", MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User clicked "نعم"
                    frmPersonInfo Form = new frmPersonInfo(clsPerson.Find(ctbEmail.Text, clsPerson.enSearchBy.Email).PersonID);
                    Form.ShowDialog();
                }
                return;

            }

            errorProvider1.SetError(ctbEmail, null);
            e.Cancel = false;

        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {

            string PhoneNumber = ctbPhone.Text.Trim(), ErrorMessage = "";

            if (!ctbPhone.IsValid(ref ErrorMessage))
            {
                errorProvider1.SetError(ctbPhone, ErrorMessage);
                e.Cancel = true;
                return;
            }
  
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctbPhone, null);

            }

            if (PhoneNumber != _Person.Phone && clsPerson.IsPersonExist(PhoneNumber, clsPerson.enSearchBy.Phone))
            {
                errorProvider1.SetError(ctbPhone, ".رقم الهاتف مستخدم من قبل");
                e.Cancel = true;

                DialogResult result = clsFormat.CustomMessageBox.Show("يوجد تطابق في رقم الهاتف مع شخص اخر هل تريد الذهاب الي ملفه؟", "تأكيد", "نعم", "لا", MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User clicked "نعم"
                    frmPersonInfo Form = new frmPersonInfo(clsPerson.Find(PhoneNumber, clsPerson.enSearchBy.Phone).PersonID);
                    Form.ShowDialog();
                }
                return;

            }


            e.Cancel = false;
            errorProvider1.SetError(ctbPhone, "");

        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            //change the defualt image to male incase there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        { //change the defualt image to female incase there is no image set.
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female_512;

        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDateOfBirth.Value != DateTimePicker.MinimumDateTime) // Check if a valid date is selected
            {
                // Update format to display the selected date
                dtpDateOfBirth.Format = DateTimePickerFormat.Short;
                cbCancel.Visible = true;
                cbCancel.Checked = false;
                dtpDateOfBirth.Tag = DateTime.Now;
            }


        }


        private void llRemoveImage_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;



            if (rbtnMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;

        }

        private void lbSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void ctbWhatsApp_Validating(object sender, CancelEventArgs e)
        {
            string ErrorMessage = "";

            if (!ctbWhatsApp.IsValid(ref ErrorMessage))
            {
                errorProvider1.SetError(ctbWhatsApp, ErrorMessage);
                e.Cancel = true;
                return;
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ctbWhatsApp, "");

            }

        }

        private void cbCancel_CheckedChanged_1(object sender, EventArgs e)
        {
            // If the date is cleared, reset the format
            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.CustomFormat = " ";
            cbCancel.Visible = false;
            dtpDateOfBirth.Tag = null;
            dtpDateOfBirth.Tag = null;
        }
    }


}

    

