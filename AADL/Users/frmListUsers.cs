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
    public partial class frmListUsers : Form
    {

        private static DataTable _dtAllUsers;

        private bool _IsDataLoading = true;
        public frmListUsers()
        {
            InitializeComponent();
        }
        private async void frmListUsers_Load(object sender, EventArgs e)
        {
            _IsDataLoading = true;
            _dtAllUsers = await Task.Run(() => clsUser.GetAllUsers());
          
            if (_dtAllUsers.Rows.Count > 0)
            {

                dgvUsers.DataSource = _dtAllUsers;

                dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                cbFilterBy.SelectedIndex = 0;
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                
            }
            _IsDataLoading = false;
       

        }

        private void _Reset()
        {
            frmListUsers_Load(null, null);

        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.SelectedIndex == 3)
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
          
            else
            {
                txtFilterValue.Visible = (cbFilterBy.SelectedIndex != 0)||(cbFilterBy.SelectedIndex == 3);
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }


        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.SelectedIndex)
            {

                case 0:
                    FilterColumn = "لاشيء";
                    break;
             
                case 1:
                    FilterColumn = "رقم المستخدم";
                    break;

                case 2:
                    FilterColumn = "اسم المستخدم";
                    break;


                case 3:
                    FilterColumn = "هل نشط";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString();

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {


            string FilterColumn = "هل نشط";
            string FilterValue = cbIsActive.Text;

            //switch (FilterValue)
            //{
            //    case "الكل":
            //        break;
            //    case "نعم":
            //        FilterValue = 1;
            //        break;
            //    case "لا":
            //        FilterValue = "0";
            //        break;
            //}


            if (FilterValue == "الكل")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
            {
                //in this case we deal with numbers not string.
                //_dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
                
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, FilterValue);
            }

            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString();


        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser Frm1 = new frmAddUpdateUser();
            frmAddUpdateUser.OnUsersUpdated += _Reset;
            Frm1.ShowDialog();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAddUpdateUser Frm1 = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmAddUpdateUser.OnUsersUpdated += _Reset;
            Frm1.ShowDialog();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser Frm1 = new frmAddUpdateUser();
            frmAddUpdateUser.OnUsersUpdated += _Reset;
            Frm1.ShowDialog();
        }

        private void dgvUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUserInfo Frm1 = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo Frm1 = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();

        }

        private void ChangePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {

            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmChangeUserPassword Frm1 = new frmChangeUserPassword(UserID);
            Frm1.ShowDialog();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من رغبتك في حذف المستخدم؟\n"+ dgvUsers.CurrentRow.Cells[1].Value,
                "تاكيد الحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {
                try
                {

                    int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
                    if (clsUser.DeleteUser(UserID))
                    {
                        MessageBox.Show("تم حذف المستخدم بنجاح", "حُذِفَ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListUsers_Load(null, null);
                    }

                    else
                        MessageBox.Show("لم يتم حذف المستخدم بسبب البيانات المتصلة به.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("لم يتم حذف المستخدم بسبب خطاء فني داخل النظام!!\n" + ex.Message, "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUsers_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsDataLoading)
            {
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                Cursor.Current = Cursors.Default;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

}
