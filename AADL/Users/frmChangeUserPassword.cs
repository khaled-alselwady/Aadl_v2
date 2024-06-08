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
    public partial class frmChangeUserPassword : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmChangeUserPassword(int UserID)
        {
            InitializeComponent();
            _UserID= UserID;
        }
        private void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("لم أتمكن من أيجاد الرقم المستخدم = " + _UserID,
                    "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }
            ctrlUserCard1.LoadUserInfo(_UserID);

        }


        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "لا يمكن أن يكون اسم المستخدم فارغ");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };

            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "كلمة المرور الحالية خاطئة");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "كلمة المرور الجديدة لا يمكن ان تكون فارغة");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            };
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "تأكيد كلمة المرور لا يتطابق مع كلمة المرور الجديدة!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };

        }
        private void btnSave_Click(object sender, EventArgs e)
        {



            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("بعض الحقول غير صالحة! ضع الماوس فوق الأيقونة(الأيقونات) الحمراء لرؤية الخطأ",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txtNewPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("تم تغيير كلمة المرور بنجاح.",
                   "حفظ.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("حدث خطأ، لم يتم تغيير كلمة المرور.",
                   "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }
}
