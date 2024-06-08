using AADLBusiness;
using MethodTimer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADL.People
{
    public partial class frmListPeople : Form
    {
        private bool _IsDataLoading = true;
        private DataTable _dtPeople;
        public frmListPeople()
        {
            InitializeComponent();
        }

        private async void _RefreshPeopleList()
        {
            _dtPeople = await Task.Run(() => clsPerson.GetAllPeople());
            dgvPeople.DataSource = _dtPeople;
            if (_dtPeople.Rows.Count > 0)
            {
                dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                cbFilterBy.SelectedIndex = 0;
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            }
        }
        [Time]
        private async void frmListPeople_Load(object sender, EventArgs e)
        {
            _IsDataLoading = true;
            _dtPeople = await Task.Run(() => clsPerson.GetAllPeople());

            if (_dtPeople.Rows.Count > 0)
            {

                dgvPeople.DataSource = _dtPeople;

                dgvPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                cbFilterBy.SelectedIndex = 0;
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            }
            _IsDataLoading = false;

            //dgvPeople.DataSource = await clsPerson.GetAllPeople();
            //cbFilterBy.SelectedIndex = 0;
            //lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            //if (dgvPeople.Rows.Count > 0)
            //{

            //    dgvPeople.Columns[0].HeaderText = "الرقم التعريفي";
            //    dgvPeople.Columns[0].Width = 110;

            //    dgvPeople.Columns[1].HeaderText = "الاسم الكامل";
            //    dgvPeople.Columns[1].Width = 300;

            //    dgvPeople.Columns[2].HeaderText = "الرقم الوطني";
            //    dgvPeople.Columns[2].Width = 120;

            //    dgvPeople.Columns[3].HeaderText = "رقم جواز السفر";
            //    dgvPeople.Columns[3].Width = 120;

            //    dgvPeople.Columns[4].HeaderText = "تاريخ الميلاد";
            //    dgvPeople.Columns[4].Width = 140;

            //    dgvPeople.Columns[5].HeaderText = "الجنس";
            //    dgvPeople.Columns[5].Width = 120;

            //    dgvPeople.Columns[6].HeaderText = "رقم الهاتف";
            //    dgvPeople.Columns[6].Width = 120;

            //    dgvPeople.Columns[7].HeaderText = "الواتس اب";
            //    dgvPeople.Columns[7].Width = 120;


            //    dgvPeople.Columns[8].HeaderText = "البريد الالكتروني";
            //    dgvPeople.Columns[8].Width = 120;


            //    dgvPeople.Columns[9].HeaderText = "الدولة";
            //    dgvPeople.Columns[9].Width = 120;


            //    dgvPeople.Columns[10].HeaderText = "المدينة";
            //    dgvPeople.Columns[10].Width = 120;

            //    dgvPeople.Columns[11].HeaderText = "العنوان";
            //    dgvPeople.Columns[11].Width = 120;

            //    dgvPeople.Columns[12].HeaderText = "تم الانشاء من قبل";
            //    dgvPeople.Columns[12].Width = 120;

            //    dgvPeople.Columns[13].HeaderText = "تاريخ الانشاء";
            //    dgvPeople.Columns[13].Width = 120;

            //    dgvPeople.Columns[14].HeaderText = "فعال";
            //    dgvPeople.Columns[14].Width = 120;
            //}

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.SelectedIndex)
            {
                case 1:
                    FilterColumn = "الرقم التعريفي";
                    break;

                case 2:
                    FilterColumn = "الرقم الوطني";
                    break;

                case 3:
                    FilterColumn = "رقم جواز السفر";
                    break;

                case 4:
                    FilterColumn = "الاسم الكامل";
                    break;

                case 5:
                    FilterColumn = "الهاتف";
                    break;
                case 6:
                    FilterColumn = "الواتس اب";
                    break;
                case 7:
                    FilterColumn = "البريد الالكتروني";
                    break;

                case 8:
                    FilterColumn = "الدولة";
                    break;

                case 9:
                    FilterColumn = "المدينة";
                    break;

                case 10:
                    FilterColumn = "تم إنشاؤه بواسطة اسم المستخدم";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            try
            {

            if (FilterColumn == "الرقم التعريفي")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            catch(Exception ex)
            {
                clsGlobal.WriteEventToLogFile("Exception dropped from list people form ,while filter data.. \n"+ex.Message,System.Diagnostics.EventLogEntryType.Error); 
            }
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.SelectedIndex == 1|| cbFilterBy.SelectedIndex == 5|| cbFilterBy.SelectedIndex == 6)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            //Form frm1 = new frmAddUpdatePerson();
            //frmAddUpdatePerson.OnPeopleUpdated += _RefreshPeopleList;
            //frm1.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Form frm = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            //frmAddUpdatePerson.OnPeopleUpdated += _RefreshPeopleList;
            //frm.ShowDialog();


        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {

            if (MessageBox.Show("هل انت متاكد من حذف بيانات هذا الشخص  [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "تاكيد الحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("تم حذف الشخص بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }


            }
            }
            catch(Exception ex)
            {
                 
                    MessageBox.Show(" لم يتم حذف الشخص لمشاكل تتعلق بالبيانات.", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Form frm = new frmAddUpdatePerson();
            //frm.ShowDialog();

            //_RefreshPeopleList();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvPeople_MouseDown(object sender, MouseEventArgs e)
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

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
