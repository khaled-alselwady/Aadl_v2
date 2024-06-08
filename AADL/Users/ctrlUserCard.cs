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
    public partial class ctrlUserCard : UserControl
    {


        private clsUser _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("لا يوجد مستخدم برقم المعرّف= " + UserID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {

            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "نعم";
            else
                lblIsActive.Text = "لا";

        }

        private void _ResetPersonInfo()
        {

            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
