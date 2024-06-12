using AADL.Pracititoners;
using AADLBusiness;
using System;
using System.Data;
using System.Windows.Forms;

namespace AADL.Regulators
{
    public partial class frmPractitionersList : Form
    {

        public frmPractitionersList()
        {
            InitializeComponent();
        }
        private void _Reset()
        {
            frmPractitionersList_Load(null, null);

        }

        private void _UpdateColumnsWidth()
        {
            if (dgvPractitioners.Rows.Count > 0)
            {
                dgvPractitioners.Columns[0].Width = 110;
                dgvPractitioners.Columns[1].Width = 200;
                dgvPractitioners.Columns[2].Width = 130;
                dgvPractitioners.Columns[3].Width = 160;
                dgvPractitioners.Columns[4].Width = 150;
                dgvPractitioners.Columns[5].Width = 120;
                dgvPractitioners.Columns[6].Width = 130;
                dgvPractitioners.Columns[7].Width = 130;
                dgvPractitioners.Columns[8].Width = 130;
                dgvPractitioners.Columns[9].Width = 130;
                dgvPractitioners.Columns[10].Width = 170;
                dgvPractitioners.Columns[11].Width = 220;
                dgvPractitioners.Columns[12].Width = 220;
                dgvPractitioners.Columns[13].Width = 220;
                dgvPractitioners.Columns[14].Width = 170;
                dgvPractitioners.Columns[15].Width = 170;
                dgvPractitioners.Columns[16].Width = 170;
                dgvPractitioners.Columns[17].Width = 170;
                dgvPractitioners.Columns[18].Width = 170;
            }
        }

        private void _LoadDataTableToGridViewControl(DataTable PractitionerDataTable)
        {
            if (PractitionerDataTable != null && PractitionerDataTable.Rows.Count > 0)
            {

                dgvPractitioners.DataSource = PractitionerDataTable;
                cbFilterBy.SelectedIndex = 0;
                _UpdateColumnsWidth();
            }
            else
            {
                dgvPractitioners.DataSource = null;// Clear all rows

                dgvPractitioners.Refresh();    // Refresh the DataGridView display
            }
            lblRecordsCount.Text = dgvPractitioners.Rows.Count.ToString();


        }
        private void frmPractitionersList_Load(object sender, EventArgs e)
        {
            _LoadDataTableToGridViewControl(clsPractitionerServices.GetAllPractitionersInfo());

        }

        private DataTable GetAllSubscriptionsTypes()
        {
            return clsSubscriptionType.GetAllSubscriptionTypes();
        }

        private void AddIsActiveTypesToComboBox()
        {

            cbIsActiveSubscription.Items.Add("الكل");

            cbIsActiveSubscription.Items.Add("نعم");
            cbIsActiveSubscription.Items.Add("لا");

        }
        private bool AddSubscriptionsTypesToComboBox()
        {

            try
            {

                DataTable dtSubscriptionTypes = GetAllSubscriptionsTypes();

                cbIsActiveSubscription.DataSource = dtSubscriptionTypes;
                cbIsActiveSubscription.DisplayMember = "SubscriptionTypeName";
            }
            catch (Exception ex)
            {

                clsHelperClasses.WriteEventToLogFile("You need to review your clsRegulatorsList ,AddSubscriptionsTypesToComboBox() ",
                    System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine(ex.Message);
                MessageBox.Show("هناك خطاء فني ما حدث انثاء تحميل انواع الاشتراكات ", "Failed",
              MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;

            }

            return true;

        }
        private void HandleComboBoxFilterBy()
        {
            ctbFilterValue.Visible = false;
            cbIsActiveSubscription.DataSource = null;

            if (cbFilterBy.Text == "هل فعال")
            {
                AddIsActiveTypesToComboBox();

            }

            else if (cbFilterBy.Text == "نوع الاشتراك")
            {
                AddSubscriptionsTypesToComboBox();
            }

            cbIsActiveSubscription.Visible = true;
            cbIsActiveSubscription.Focus();
            cbIsActiveSubscription.SelectedIndex = 0;

        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number in-case user id is selected.
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 3)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "هل فعال" || cbFilterBy.Text == "نوع الاشتراك")
            {
                HandleComboBoxFilterBy();

            }

            else
            {

                ctbFilterValue.Visible = (cbFilterBy.Text != "لا شئ");
                cbIsActiveSubscription.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    ctbFilterValue.Enabled = false;
                }
                else
                    ctbFilterValue.Enabled = true;

                ctbFilterValue.Text = "";
                ctbFilterValue.Focus();
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.SelectedIndex)
            {

                case 0:
                    {
                        FilterColumn = "لا شئ";
                        break;
                    }

                case 1:
                    {

                        FilterColumn = "الرقم التعريفي";
                        break;
                    }

                case 2:
                    {

                        FilterColumn = "الاسم الكامل";
                        break;
                    }


                case 3:
                    {

                        FilterColumn = "رقم الهاتف";
                        break;

                    }
                case 4:
                    {
                        FilterColumn = "البريد الالكتروني";
                        break;
                    }

                case 5:
                    {

                        FilterColumn = "رقم العضوية";
                        break;

                    }

                case 6:
                    {

                        FilterColumn = "نوع الاشتراك";
                        break;
                    }

                case 7:
                    {

                        FilterColumn = "تم الانشاء من قبل";
                        break;
                    }

                case 8:
                    {
                        FilterColumn = "هل فعال";
                        break;
                    }

                default:
                    FilterColumn = "None";
                    break;

            }
            DataTable dataTableAllPractitioners = new DataTable();
            //Reset the filters in case nothing selected or filter value conains nothing.
            if (ctbFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                dataTableAllPractitioners.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPractitioners.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "الاسم الكامل" && FilterColumn != "البريد الالكتروني" && FilterColumn != "تم الانشاء من قبل"
                && FilterColumn != "رقم العضوية")
                //in this case we deal with numbers not string.
                dataTableAllPractitioners.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, ctbFilterValue.Text.Trim());
            else
                dataTableAllPractitioners.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, ctbFilterValue.Text.Trim());

            lblRecordsCount.Text = dataTableAllPractitioners.Rows.Count.ToString();

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            string FilterValue = "";

            if (cbFilterBy.SelectedIndex == 6)
            {
                FilterColumn = "نوع الاشتراك";
                FilterValue = cbIsActiveSubscription.Text;
            }
            else if (cbFilterBy.SelectedIndex == 8)
            {
                FilterColumn = "هل فعال";
                FilterValue = cbIsActiveSubscription.Text;
            }

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
            DataTable dataTableAllPractitioners = new DataTable();


            if (FilterValue == "الكل")
                dataTableAllPractitioners.DefaultView.RowFilter = "";
            else
            {
                //in this case we deal with numbers not string.
                //_dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

                dataTableAllPractitioners.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, FilterValue);
            }

            lblRecordsCount.Text = dataTableAllPractitioners.Rows.Count.ToString();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdatePractitioner frmAddUpdatePractitioner = new frmAddUpdatePractitioner();
            frmAddUpdatePractitioner.NewPractitionerWasAdded += _NewPractitionerWasAddedUpdateYourSelf;
            frmAddUpdatePractitioner.ShowDialog();
        }
        private void _NewPractitionerWasAddedUpdateYourSelf(object sender, EventArgs e)
        {
            frmPractitionersList_Load(sender, e);
        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void AdvancePractitionerPropertiesQuery(AdvancedSearchPractitionerProperties PractitionerProperties)
        {
            try
            {

                DataTable AdvancedPractitionerQueryDataTable = clsPractitionerServices.Find(PractitionerProperties);
                _LoadDataTableToGridViewControl(AdvancedPractitionerQueryDataTable);
            }
            catch (Exception ex)
            {

                clsHelperClasses.WriteEventToLogFile("A problem in loading filter data of practitioner by advanced query:\n" +
                    ex.Message, System.Diagnostics.EventLogEntryType.Warning);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmAdvancedSearch frmAdvancedSearch = new frmAdvancedSearch();
            frmAdvancedSearch.SelectedPractitionerProperties += AdvancePractitionerPropertiesQuery;
            frmAdvancedSearch.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _Reset();
        }

    }
}
