using AADL.People;
using AADLBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADL.Users
{
    public partial class frmAddUpdateUser : Form
    {
        public static Action OnUsersUpdated { get; set; }
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _UserID = -1;
        clsUser _User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _UserID = UserID;
        }
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "اضافة مستخدم جديد";
                this.Text = "اضافة مستخدم جديد";
                _User = new clsUser();

            }
            else
            {
                lblTitle.Text = "تعديل و تحديث بيانات المستخدم";
                this.Text = "تعديل و تحديث بيانات المستخدم";
                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;


        }
        private void _LoadData()
        {

            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("لا يوجد مسخدم برقم تعريفي =  " + _User, "لا يوجد مستخدم", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();

        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("بعض الحقول غير صالحة! ضع الماوس فوق الأيقونة(الأيقونات) الحمراء لرؤية الخطأ",
                    "خطاء في البيانات المدخلة", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;
            _User.CreatedByAdminID = (int)clsGlobal.CurrentAdmin.AdminID;
            _User.IssueDate = DateTime.Now;
            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "تعديل و تحديث بيانات المستخدم";
                this.Text = "تعديل و تحديث بيانات المستخدم";

                MessageBox.Show("تم حفظ البيانات بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);


                if(OnUsersUpdated != null)
                {
                    OnUsersUpdated();
                }
            }
            else
                MessageBox.Show("خطاء: لم تحفظ البيانات بشكل صحيح", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "تأكيد كلمة المرور لا يتطابق مع كلمة المرور!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "لا يمكن أن تكون كلمة المرور فارغة");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            };

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "لا يمكن أن يكون اسم المستخدم فارغ");
                return;
            }
  
            else
            {
                errorProvider1.SetError(txtUserName, null);
            };


            if (_Mode == enMode.AddNew)
            {

                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "اسم المستخدم الذي اختره ,مستخدم من قبل شخص اخر.");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
  
            else
            {
                //in case update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "اسم المستخدم تم اختياره داخل النظام, اختر اسم اخر.");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    };
                }
            }
        }
    }

}
