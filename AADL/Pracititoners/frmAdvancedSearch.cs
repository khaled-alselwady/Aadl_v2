using AADLBusiness;
using CommandLine;
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
using static AADL.Pracititoners.frmAdvancedSearch;


namespace AADL.Pracititoners
{
    public partial class frmAdvancedSearch : Form
    {

        private AdvancedSearchPractitionerProperties _advancedSearchPractitionerProperties=new AdvancedSearchPractitionerProperties();

        public delegate void MyDelegate(AdvancedSearchPractitionerProperties PractitionerProperties); 
        public MyDelegate SelectedPractitionerProperties; // Declare a delegate variable

        //public Func<clsHelperClasses.AdvancedSearchPractitionerProperties> SelectedPractitionerProperties;

        public frmAdvancedSearch()
        {
            InitializeComponent();
        }
        private void InitializeDateTimePicker(DateTimePicker dateTimePicker, CheckBox checkBox)
        {
            dateTimePicker.Tag = 0;
            dateTimePicker.CustomFormat = " ";
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            checkBox.CheckedChanged += checkBox_CheckedChanged;
            dateTimePicker.ValueChanged += dateTimePicker_ValueChanged;
        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {

                switch (Convert.ToInt32(checkBox.Tag))
                {

                    case 1:
                        {
                            
                            dtpRegulatorIssueDate.Tag = 0;
                            dtpRegulatorIssueDate.CustomFormat = " ";
                            break;
                        }
                 
                    case 2:
                        {
                            dtpRegulatorIssueDateFrom.Tag = 0;
                            dtpRegulatorIssueDateFrom.CustomFormat = " ";
                            break;
                        }
                  
                    case 3:
                        {
                            dtpRegulatorIssueDateTo.Tag = 0;
                            dtpRegulatorIssueDateTo.CustomFormat = " ";
                            break;
                        }

                    case 4:
                        {

                            dtpShariaIssueDate.Tag = 0;
                            dtpShariaIssueDate.CustomFormat = " ";
                            break;
                        }

                    case 5:
                        {
                            dtpShariaIssueDateFrom.Tag = 0;
                            dtpShariaIssueDateFrom.CustomFormat = " ";
                            break;
                        }

                    case 6:
                        {
                            dtpShariaIssueDateTo.Tag = 0;
                            dtpShariaIssueDateTo.CustomFormat = " ";
                            break;
                        }
              
                }

            }
        }
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (cbEnableRegulatorIssueDate.Checked == false&&(DateTimePicker)sender== dtpRegulatorIssueDate)
            {
                //you need to extend it to all controls.
                dtpRegulatorIssueDate.Tag = 1;
                dtpRegulatorIssueDate.CustomFormat = "dd/MM/yyyy";

            }
            else if (cbEnableRegulatorFromDate.Checked == false && (DateTimePicker)sender == dtpRegulatorIssueDateFrom)
            {
                //you need to extend it to all controls.
                dtpRegulatorIssueDateFrom.Tag = 1;
                dtpRegulatorIssueDateFrom.CustomFormat = "dd/MM/yyyy";

            }
            else if (cbEnableRegulatorToDate.Checked ==false &&(DateTimePicker)sender == dtpRegulatorIssueDateTo)
            {
                //you need to extend it to all controls.
                dtpRegulatorIssueDateTo.Tag = 1;
                dtpRegulatorIssueDateTo.CustomFormat = "dd/MM/yyyy";

            }


        }
        private void ResetValuesToDefault()
        {

            _loadSubscriptionTypes();
            _loadSubscriptionWays();

            InitializeDateTimePicker(dtpRegulatorIssueDate, cbEnableRegulatorIssueDate);
            InitializeDateTimePicker(dtpRegulatorIssueDateFrom, cbEnableRegulatorFromDate);
            InitializeDateTimePicker(dtpRegulatorIssueDateTo, cbEnableRegulatorToDate);

            InitializeDateTimePicker(dtpShariaIssueDate, cbEnableShariaIssueDate);
            InitializeDateTimePicker(dtpShariaIssueDateFrom, cbEnableShariaFromDate);
            InitializeDateTimePicker(dtpShariaIssueDateTo, cbEnableShariaToDate);

            InitializeDateTimePicker(dateTimePicker1, checkBox1);
            InitializeDateTimePicker(dateTimePicker2, checkBox2);
            InitializeDateTimePicker(dateTimePicker3, checkBox3);
            InitializeDateTimePicker(dateTimePicker4, checkBox4);
            InitializeDateTimePicker(dateTimePicker5, checkBox5);
            InitializeDateTimePicker(dateTimePicker6, checkBox6);



        }
        private void frmAdvancedSearch_Load(object sender, EventArgs e)
        {
            try
            {

            ResetValuesToDefault();
            }catch(Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Exception was dropped in your (frmAdavanceSearch)form , you might want to take" +
                    "a look on load the form event\n"+ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }
        private void _loadSubscriptionTypes()
        {
            DataTable dataTableSubscriptionTypes = clsSubscriptionType.GetAllSubscriptionTypes();
            cbSubscriptionType.DataSource = dataTableSubscriptionTypes;
            cbSubscriptionType.DisplayMember = "SubscriptionName";
            cbSubscriptionType.ValueMember = "SubscriptionTypeID";
            cbSubscriptionType.ResetText();

            //to reset selected value
            cbSubscriptionType.SelectedIndex = -1;
        }
        private void _loadSubscriptionWays()
        {
            DataTable dataTableSubscriptionWays = clsSubscriptionWay.GetAllSubscriptionWays();
            cbSubscriptionWay.DataSource = dataTableSubscriptionWays;
            cbSubscriptionWay.DisplayMember = "SubscriptionName";
            cbSubscriptionWay.ValueMember = "SubscriptionWayID";
            cbSubscriptionWay.ResetText();

            //to reset selected value
            cbSubscriptionWay.SelectedIndex = -1;
        }
        private bool? IsPractitionerActive(clsPractitioner.enPractitionerType practitionerType)
        {
            
            switch (practitionerType)
            {
                case clsPractitioner.enPractitionerType.Regulatory:
                    {

                        if (rbtnIsRegulatorActiveNo.Checked)
                        {
                            return false;
                        }
                        else if (rbtnIsRegulatorActiveYes.Checked)
                        {
                            return true;
                        }
                        break;

                    }

                case clsPractitioner.enPractitionerType.Sharia:
                    {

                        if (rbtnIsShariaActiveNo.Checked)
                        {
                            return false;
                        }
                        else if (rbtnIsShariaActiveYes.Checked)
                        {
                            return true;
                        }

                        break;
                    }

                    //case clsPractitioner.enPractitionerType.Expert:
                    //    {

                    //        if (rbtnIsExpertActiveNo.Checked)
                    //        {
                    //            return false;
                    //        }
                    //        else if (rbtnIsExpertActiveYes.Checked)
                    //        {
                    //            return true;
                    //        }
                    //        else
                    //            return null;
                    //    }
                    //case clsPractitioner.enPractitionerType.Judger:
                    //    {

                    //        if (rbtnIsJudgerActiveNo.Checked)
                    //        {
                    //            return false;
                    //        }
                    //        else if (rbtnIsJudgerActiveYes.Checked)
                    //        {
                    //            return true;
                    //        }
                    //        else
                    //            return null;
                    //    }

            }

            return null;

        }
        private bool? IsPractitionerInList (clsList.enListType listType)
        {
            switch (listType) 
            {

                case clsList.enListType.Black:
                    {

                        //3 case yes , no ,nothing 
                        if (rbtnIsPractitionerInBlackListYes.Checked)
                        {
                            return true;
                        }
                        else if (rbtnIsPractitionerInBlackListNo.Checked)
                        {
                            return false;
                        }
                        break;

                    }
                
                case clsList.enListType.RegulatoryWhite:
                    {

                        //3 case yes , no ,nothing 
                        if (rbtnIsRegulatorWhiteListYes.Checked)
                        {
                            return true;

                        }
                        else if (rbtnIsRegulatorWhiteListNo.Checked)
                        {
                            return false;

                        }
                        break;

                    }

                case clsList.enListType.RegulatoryClosed:
                    {

                        //3 case yes , no ,nothing 
                        if (rbtnIsRegulatorClosedListYes.Checked)
                        {
                            return true;

                        }
                        else if (rbtnIsRegulatorClosedListNo.Checked)
                        {
                            return false;

                        }
                        break;

                    }

                case clsList.enListType.ShariaWhite:
                    {

                        //3 case yes , no ,nothing 
                        if (rbtnIsShariaWhiteListYes.Checked)
                        {
                            return true;

                        }
              
                        else if (rbtnIsShariaWhiteListNo.Checked)
                        {
                            return false;

                        }
                   
                        break;

                    }

                case clsList.enListType.ShariaClosed:
                    {

                        if (rbtnIsRegulatorClosedListYes.Checked)
                        {
                            return true;

                        }

                        else if (rbtnIsRegulatorClosedListNo.Checked)
                        {
                            return false;

                        }
                   
                        break;

                    }
               
            }

            return null;
        }
            
        private void AssignPersonalInfo()
        {

            _advancedSearchPractitionerProperties.FullName = string.IsNullOrWhiteSpace(mtbFullName.Text) ? "" : mtbFullName.Text;
            _advancedSearchPractitionerProperties.PhoneNumber = string.IsNullOrWhiteSpace(mtbPhone.Text) ? "" : mtbPhone.Text;
            _advancedSearchPractitionerProperties.Email = string.IsNullOrWhiteSpace(mtbEmail.Text) ? "" : mtbEmail.Text;

        }
        private void AssignRegulatorInfo()
        {

            _advancedSearchPractitionerProperties.MemberShipNumber = string.IsNullOrWhiteSpace(mtbMemberShipNumber.Text) ? "" : mtbMemberShipNumber.Text;
            _advancedSearchPractitionerProperties.IsRegulatorActive = IsPractitionerActive(clsPractitioner.enPractitionerType.Regulatory);
            _advancedSearchPractitionerProperties.RegulatorIssueDate = (int)dtpRegulatorIssueDate.Tag == 0 ? (DateTime?)null : dtpRegulatorIssueDate.Value.Date;
            _advancedSearchPractitionerProperties.RegulatorIssueDateFrom = (int)dtpRegulatorIssueDateFrom.Tag == 0 ? (DateTime?)null : dtpRegulatorIssueDateFrom.Value.Date;
            _advancedSearchPractitionerProperties.RegulatorIssueDateTo = (int)dtpRegulatorIssueDateTo.Tag == 0 ? (DateTime?)null : dtpRegulatorIssueDateTo.Value.Date;
            _advancedSearchPractitionerProperties.RegulatorCreatedByUserName = string.IsNullOrWhiteSpace(mtbRegulatorCreatedByUserName.Text) ?
                "" : mtbRegulatorCreatedByUserName.Text;
            _advancedSearchPractitionerProperties.IsInRegulatoryWhiteList = IsPractitionerInList(clsList.enListType.RegulatoryWhite);
            _advancedSearchPractitionerProperties.IsInRegulatoryClosedList = IsPractitionerInList(clsList.enListType.RegulatoryClosed);

        }
        private void AssignShariaInfo()
        {

            _advancedSearchPractitionerProperties.ShariaLicenseNumber = string.IsNullOrWhiteSpace(mtbShariaLicenseNumber.Text) ? "" : mtbShariaLicenseNumber.Text;
            _advancedSearchPractitionerProperties.IsShariaActive = IsPractitionerActive(clsPractitioner.enPractitionerType.Sharia);
            _advancedSearchPractitionerProperties.ShariaIssueDate = (int)dtpShariaIssueDate.Tag == 0 ? (DateTime?)null : dtpShariaIssueDate.Value.Date;
            _advancedSearchPractitionerProperties.ShariaIssueDateFrom = (int)dtpShariaIssueDateFrom.Tag == 0 ? (DateTime?)null : dtpShariaIssueDateFrom.Value.Date;
            _advancedSearchPractitionerProperties.ShariaIssueDateTo = (int)dtpShariaIssueDateTo.Tag == 0 ? (DateTime?)null : dtpShariaIssueDateTo.Value.Date;
            _advancedSearchPractitionerProperties.ShariaCreatedByUserName = string.IsNullOrWhiteSpace(mtbShariaCreatedByUserName.Text) ?
                "" : mtbShariaCreatedByUserName.Text;
            _advancedSearchPractitionerProperties.IsInShariaWhiteList = IsPractitionerInList(clsList.enListType.ShariaWhite);
            _advancedSearchPractitionerProperties.IsInShariaClosedList = IsPractitionerInList(clsList.enListType.ShariaClosed);

        }
        private int? GetSubscriptionTypeID()
        {

            DataRowView selectedRow = (DataRowView)cbSubscriptionType.SelectedItem;
            int? SubscriptionTypeID = selectedRow != null ? Convert.ToInt32(selectedRow["SubscriptionTypeID"]) : (int?)null;
            return SubscriptionTypeID;
        }
        private int? GetSubscriptionWayID()
        {


            DataRowView selectedRow = (DataRowView)cbSubscriptionWay.SelectedItem;
            int? SubscriptionWayID = selectedRow != null ? Convert.ToInt32(selectedRow["SubscriptionWayID"]) : (int?)null;
            return SubscriptionWayID;
        }
        private void AssignPractitionerInfo()
        {
            _advancedSearchPractitionerProperties.IsPractitionerInBlackList = IsPractitionerInList(clsList.enListType.Black);
            _advancedSearchPractitionerProperties.SubscriptionTypeID= GetSubscriptionTypeID();
            _advancedSearchPractitionerProperties.SubscriptionWayID = GetSubscriptionWayID();

        }

        private AdvancedSearchPractitionerProperties GetQueryProperties()
        {
            AssignPersonalInfo();
            AssignPractitionerInfo();
            AssignRegulatorInfo();
            AssignShariaInfo();
            bool? IsExpertActive = IsPractitionerActive(clsPractitioner.enPractitionerType.Expert);
            bool? IsJudgerActive = IsPractitionerActive(clsPractitioner.enPractitionerType.Judger);

  
            return _advancedSearchPractitionerProperties;

        }
        private void button1_Click(object sender, EventArgs e)
        {
           AdvancedSearchPractitionerProperties practitionerProperties = GetQueryProperties();
            // Check if there's a delegate assigned
            if (SelectedPractitionerProperties != null)
            {
                SelectedPractitionerProperties(practitionerProperties); // Invoke the delegate with the object
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
