using AADL.GlobalClasses;
using AADL.Lists;
using AADLBusiness;
using AADLBusiness;
using AADLBusiness.Judger;
using AADLBusiness.Lists.Closed;
using AADLBusiness.Lists.WhiteList;
using AADLBusiness.Sharia;
using MethodTimer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Diagnostics.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static AADL.GlobalClasses.clsTestClasses;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace AADL.Regulators
{
   
    public partial class frmAddUpdatePractitioner : Form
    {
        // Define a delegate for the event handler
        public delegate void EntityAddedEventHandler(object sender, EventArgs e);

        // Define the event in PractitionerForm
        public event EntityAddedEventHandler NewPractitionerWasAdded;

        private enum enSubscriptionType { Free=1,Medium=2,Special=3};
        private enum enSubscriptionWay{ SpecialSupport=1, scholarship = 2};

        public enum enMode { AddNew = 0, Update = 1 }
    

        private enMode _Mode;

        private enMode _RegulatorMode;

        private enMode _ShariaMode;

        private enMode _ExpertMode;

        private enMode _JudgerMode;


        private int _PractitionerID = -1;
        clsRegulator _Regulator;
        clsSharia _Sharia;
        clsJudger _Judger;

        protected virtual void OnEntityAdded(EventArgs e)
        {
            if(NewPractitionerWasAdded != null)
            {

            NewPractitionerWasAdded(this, e);
            }
        }
        public frmAddUpdatePractitioner()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            ctrlPersonCardWithFilter1.OnPersonComplete += CheckItOut;

        }
        private void CheckItOut(object sender, PersonCompleteEventArgs e)
        {

            if (e.PersonID == null)
            {
                _SwitchCurrentMode();
                MessageBox.Show("لا يمكن اضافة او تعديل بيانات لهذا الشخص , لعدم وجود بيانات مدنية له.", "فشل", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                clsGlobal.WriteEventToLogFile("Lack of accurate personID, to the lawyer or practitioner in add/update practitioner info",
                    System.Diagnostics.EventLogEntryType.Error);
                _ResetDefaultValues();
            }

        }
        public frmAddUpdatePractitioner(int  practitionerID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PractitionerID = practitionerID;
            ctrlPersonCardWithFilter1.OnPersonComplete += CheckItOut;
        }
        [Time]
        private void _loadRegulatoryCasesTypes()
        {
            
            try
            {

                DataTable RegulatoryCasesTypeDataTable = clsRegulatoryCaseType.GetAllRegulatoryCaseTypes();
                if (RegulatoryCasesTypeDataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in RegulatoryCasesTypeDataTable.Rows)
                    {
                        int RegulatoryCaseTypeID = (int)row["RegulatoryCaseTypeID"];
                        string RegulatoryCaseTypeName = (string)row["RegulatoryCaseTypeName"];

                        clsGlobal.CheckListBoxItem RegulatoryCaseTypeItem = new clsGlobal.CheckListBoxItem(RegulatoryCaseTypeID,
                            RegulatoryCaseTypeName);

                        clbRegulatoryCasesTypes.Items.Add(RegulatoryCaseTypeItem); // Add item using Name column

                    }
                }

            }
            catch (Exception ex)
            {
                clsGlobal.WriteEventToLogFile("Exception dropped at regulator form add/update while loading cases types ",
                                  System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Exception dropped while loading Lists cases !!\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void _loadShariaCasesTypes()
        {
            try
            {

                DataTable ShariaCasesTypeDataTable = clsShariaCaseType.GetAllShariaCaseTypes();
                if (ShariaCasesTypeDataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in ShariaCasesTypeDataTable.Rows)
                    {
                        int ShariaCaseTypeID = (int)row["ShariaCaseTypeID"];
                        string ShariaCaseTypeName = (string)row["ShariaCaseTypeName"];

                        clsGlobal.CheckListBoxItem ShariaCaseTypeItem = new clsGlobal.CheckListBoxItem(ShariaCaseTypeID,
                            ShariaCaseTypeName);

                        clbShariaCasesTypes.Items.Add(ShariaCaseTypeItem);// Add item using Name column

                    }
                }

            }
            catch (Exception ex)
            {
                clsGlobal.WriteEventToLogFile("Exception dropped at Sharia form add/update while loading cases types ",
                                  System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Exception dropped while loading Lists cases !!\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void _loadJudgerCasesTypes()
        {
            try
            {

                DataTable JudgerCasesTypeDataTable = clsJudgeCaseType.GetAllJudgeCaseTypes();
                if (JudgerCasesTypeDataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in JudgerCasesTypeDataTable.Rows)
                    {
                        int JudgerCaseTypeID = (int)row["JudgeCaseTypeID"];
                        string JudgerCaseTypeName = (string)row["JudgeCaseTypeName"];

                        clsGlobal.CheckListBoxItem JudgerCaseTypeItem = new clsGlobal.CheckListBoxItem(JudgerCaseTypeID,
                            JudgerCaseTypeName);

                        clbJudgerCasesTypes.Items.Add(JudgerCaseTypeItem);// Add item using Name column

                    }
                }

            }
            catch (Exception ex)
            {
                clsGlobal.WriteEventToLogFile("Exception dropped at Judger form add/update while loading cases types ",
                                  System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Exception dropped while loading Lists cases !!\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _loadRegulatorInfoData()
        {
            try
            {
                _Regulator = clsRegulator.Find(_PractitionerID, clsRegulator.enSearchBy.PractitionerID);

                if (_Regulator != null)
                {

                    //the following code will not be executed if the regulator was not found
                    _LoadRegulatorCasesPractice();
                    lblRegulatorID.Text = _Regulator.RegulatorID.ToString();
                    ctbRegulatoryMemberShipNumber.Text = _Regulator.MemberShipNumber;
                    chkRegulatorIsActive.Checked = _Regulator.IsActive;
                    switch (_Regulator.SubscriptionTypeID)
                    {
                        case 1://Free
                            {
                                rbtnRegulatoryFree.Checked = true;
                                break;

                            }
                        case 2://Medium
                            {
                                rbtnRegulatoryMedium.Checked = true;
                                break;
                            }
                        case 3://Special
                            {
                                rbtnRegulatorySpecial.Checked = true;
                                break;
                            }
                    }
                    switch (_Regulator.SubscriptionWayID)
                    {
                        case 1://SCHOLARSHIP
                            {
                                rbtnRScholarship.Checked = true;
                                break;

                            }
                        case 2://SUPPORT
                            {
                                rbtnRSpecialSupport.Checked = true;
                                break;
                            }

                    }

                }

                else
                {
                    _RegulatorMode = enMode.AddNew;
                    MessageBox.Show("حدث هنالك خطاء فني  , اثناء تحميل البيانات المتعلقة بالمحامي النظامي","مشكلة",
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
                    //Ignore it ...
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("حدث خطاء فني اثناء تحميل البيانات ", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsHelperClasses.WriteEventToLogFile("Exception was dropped in ADd update practitioner form , while loading regulator info data,\n" + ex.Message,
                    System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine(ex.Message);

            }
        }
        private void _loadShariaInfoData()
        {
            try
            {
                _Sharia = clsSharia.Find(_PractitionerID, clsSharia.enSearchBy.PractitionerID);


                if (_Sharia != null)
                {
                    _LoadShariaCasesPractice();
                    //the following code will not be executed if the sharia was not found
                    lblShariaID.Text = _Sharia.ShariaID.ToString();
                    ctbShariaLicenseNumber.Text = _Sharia.ShariaLicenseNumber;
                    chkShariaIsActive.Checked = _Sharia.IsActive;
                    
                    switch (_Sharia.SubscriptionTypeID)
                    {
                        case 1://Free
                            {
                                rbtnShariaFree.Checked = true;
                                break;

                            }
                        case 2://Medium
                            {
                                rbtnShariaMedium.Checked = true;
                                break;
                            }
                        case 3://Special
                            {
                                rbtnShariaSpecial.Checked = true;
                                break;
                            }
                    }
                    switch (_Sharia.SubscriptionWayID)
                    {
                        case 1://Free
                            {
                                rbtnSScholarship.Checked = true;
                                break;

                            }
                        case 2://Medium
                            {
                                rbtnSSpecialSupport.Checked = true;
                                break;
                            }
                       
                    }

                }

                else
                {
                    _ShariaMode = enMode.AddNew;
                    MessageBox.Show("حدث هنالك خطاء فني  , اثناء تحميل البيانات المتعلقة بالمحامي الشرغي", "مشكلة",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Ignore it ...
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("حدث خطاء فني اثناء تحميل البيانات ", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsHelperClasses.WriteEventToLogFile("Exception was dropped in ADd update practitioner form, while loading sharia info data,\n" + ex.Message,
                    System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine(ex.Message);

            }
        }
        private void _loadJudgerInfoData()
        {
            try
            {
                _Judger = clsJudger.FindByPractitionerID(_PractitionerID);


                if (_Judger != null)
                {
                    _LoadJudgerCasesPractice();
                    //the following code will not be executed if the Judger was not found
                    lblJudgerID.Text = _Judger.JudgerID.ToString();
                    chkJudgerIsActive.Checked = _Judger.IsActive;

                    switch (_Judger.SubscriptionTypeID)
                    {
                        case 1://Free
                            {
                                rbtnJudgerFree.Checked = true;
                                break;

                            }
                        case 2://Medium
                            {
                                rbtnJudgerMedium.Checked = true;
                                break;
                            }
                        case 3://Special
                            {
                                rbtnJudgerSpecial.Checked = true;
                                break;
                            }
                    }
                    switch (_Judger.SubscriptionWayID)
                    {
                        case 1://Free
                            {
                                rbtnJScholarship.Checked = true;
                                break;

                            }
                        case 2://Medium
                            {
                                rbtnJSpecialSupport.Checked = true;
                                break;
                            }

                    }

                }

                else
                {
                    _JudgerMode = enMode.AddNew;
                    MessageBox.Show("حدث هنالك خطاء فني  , اثناء تحميل البيانات المتعلقة بالمحكم", "مشكلة",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Ignore it ...
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("حدث خطاء فني اثناء تحميل البيانات ", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsHelperClasses.WriteEventToLogFile("Exception was dropped in ADd update practitioner  form , while loading Judger info data,\n" + ex.Message,
                    System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine(ex.Message);

            }
        }

        private void _LoadRegulatorCasesPractice()
        {
            // Wait for the UI thread to process pending events
            if (_Regulator.RegulatorID != -1)
            {

                if (_Regulator.RegulatorCasesPracticeIDNameDictionary.Count > 0)
                {
                    for (int idx = 0; idx < clbRegulatoryCasesTypes.Items.Count; idx++)
                    {

                        clsGlobal.CheckListBoxItem item = (clsGlobal.CheckListBoxItem)clbRegulatoryCasesTypes.Items[idx];

                        // Check if the key case ID  exists in the Dictionary of cases of regulator 
                        if (_Regulator.RegulatorCasesPracticeIDNameDictionary.ContainsKey(item.ID))
                        {
                            // If the case ID  exists in the CheckedListBox items, set its checked state
                            clbRegulatoryCasesTypes.SetItemChecked(idx, true);
                        }

                    }

                    lbRegulatoryCasesRecord.Text = clbRegulatoryCasesTypes.CheckedItems.Count.ToString();
                }
            }
        }

        private void _LoadShariaCasesPractice()
        {
            // Wait for the UI thread to process pending events
            if (_Sharia.ShariaID != -1)
            {

                if (_Sharia.ShariaCasesPracticeIDNameDictionary.Count > 0)
                {
                    for (int idx = 0; idx < clbShariaCasesTypes.Items.Count; idx++)
                    {

                        clsGlobal.CheckListBoxItem item = (clsGlobal.CheckListBoxItem)clbShariaCasesTypes.Items[idx];

                        // Check if the key case ID  exists in the Dictionary of cases of sharia 
                        if (_Sharia.ShariaCasesPracticeIDNameDictionary.ContainsKey(item.ID))
                        {
                            // If the case ID  exists in the CheckedListBox items, set its checked state
                            clbShariaCasesTypes.SetItemChecked(idx, true);
                        }

                    }

                    lbShariaCasesRecord.Text = clbShariaCasesTypes.CheckedItems.Count.ToString();
                }
            }
        }
        private void _LoadJudgerCasesPractice()
        {
            // Wait for the UI thread to process pending events
            if (_Judger.JudgerID!= -1)
            {

                if (_Judger.JudgeCasesPracticeIDNameDictionary.Count > 0)
                {
                    for (int idx = 0; idx < clbJudgerCasesTypes.Items.Count; idx++)
                    {

                        clsGlobal.CheckListBoxItem item = (clsGlobal.CheckListBoxItem)clbJudgerCasesTypes.Items[idx];

                        // Check if the key case ID  exists in the Dictionary of cases of Judger 
                        if (_Judger.JudgeCasesPracticeIDNameDictionary.ContainsKey(item.ID))
                        {
                            // If the case ID  exists in the CheckedListBox items, set its checked state
                            clbJudgerCasesTypes.SetItemChecked(idx, true);
                        }

                    }

                    lbJudgerCasesRecord.Text = clbJudgerCasesTypes.CheckedItems.Count.ToString();
                }
            }
        }

        private void _ResetRegulatorInfo()
        {
            _Regulator = new clsRegulator();
            ctbRegulatoryMemberShipNumber.Text = "";
            chkRegulatorIsActive.Checked = true;
            rbtnRegulatoryFree.Checked = true;
            lbRegulatoryCasesRecord.Text = "0";
            _RegulatorMode = enMode.AddNew;
            cbAddRegulator.Checked = false;
            tpRegulatorInfo.Enabled = false;
            _loadRegulatoryCasesTypes();
        }
        private void _ResetShariaInfo()
        {
            _Sharia=new clsSharia();
            ctbShariaLicenseNumber.Text = "";
            chkShariaIsActive.Checked = true;
            rbtnShariaFree.Checked = true;
            lbShariaCasesRecord.Text = "0"; 
            _ShariaMode = enMode.AddNew;
            tpShariaInfo.Enabled = false;
            cbAddSharia.Checked = false;
            _loadShariaCasesTypes();
        }
        private void _ResetJudgerInfo()
        {
            //Reset - juder Info.
            _Judger = new clsJudger();
            _JudgerMode = enMode.AddNew;
            chkJudgerIsActive.Checked = true;
            rbtnJudgerFree.Checked = true;
            lbJudgerCasesRecord.Text = "0";
            _JudgerMode = enMode.AddNew;
            tpJudgerInfo.Enabled = false;
            cbAddJudger.Checked = false;
            _loadJudgerCasesTypes();

        }
        private void _ResetExpertInfo()
        {
            //Reset - expert Info.

            _ExpertMode = enMode.AddNew;
            cbAddExpert.Checked = false;

        }
        private void _ResetDefaultValues()
        {

            lblTitle.Text = "أضافة مزاول جديد للمهنة";

            this.Text = "أضافة مزاول جديد للمهنة";

             if (_Mode == enMode.Update)
             {
                lblTitle.Text = "أضافة مزاول جديد للمهنة";
                this.Text = "أضافة مزاول جديد للمهنة";
                lblTitle.Text = "تحديث و تعديل بيانات مزاول للمهنة";
                this.Text = "تحديث و تعديل بيانات مزاول للمهنة";

             }
           
            ctrlPersonCardWithFilter1.FilterFocus();

            _ResetRegulatorInfo();
            _ResetShariaInfo();
            _ResetJudgerInfo();
            _ResetExpertInfo();

        }

        private void _SwitchCurrentMode()
        {
            if (_Mode == enMode.Update)
            {
                _RegulatorMode= enMode.AddNew;
                _ShariaMode = enMode.AddNew;
                _ExpertMode= enMode.AddNew;
                _JudgerMode= enMode.AddNew;
                _Mode = enMode.AddNew;
            }

            else
            {
                _Mode = enMode.Update;
                _RegulatorMode= enMode.Update;
                _ShariaMode= enMode.Update;
                _ExpertMode= enMode.Update;
                _JudgerMode = enMode.Update;
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _LoadData()
        {
            bool IsPersonInfoLoaded = false;
            try
            {

                if (clsPractitioner.IsPractitionerExists(_PractitionerID))
                {
                    if (clsRegulator.IsRegulatorExist(_PractitionerID, clsRegulator.enSearchBy.PractitionerID))
                    {
                        _loadRegulatorInfoData();
                        _RegulatorMode = enMode.Update;
                              if (!IsPersonInfoLoaded)
                              {
                            
                                  ctrlPersonCardWithFilter1.LoadPersonInfo(_Regulator.PersonID);
                                  ctrlPersonCardWithFilter1.FilterEnabled = false;
                                  IsPersonInfoLoaded = true;
                              }

                    }

                    if (clsSharia.IsShariaExist(_PractitionerID, clsSharia.enSearchBy.PractitionerID))
                    {
                        _loadShariaInfoData();
                        _ShariaMode = enMode.Update;
                              if (!IsPersonInfoLoaded)
                              {
                            
                                  ctrlPersonCardWithFilter1.LoadPersonInfo(_Sharia.PersonID);
                                  ctrlPersonCardWithFilter1.FilterEnabled = false;
                                  IsPersonInfoLoaded = true;
                            
                              }
                    }
                   
                    if (clsJudger.IsJudgerExistByPractitionerID(_PractitionerID))
                    {
                        _loadJudgerInfoData();
                        _JudgerMode = enMode.Update;
                        if (!IsPersonInfoLoaded)
                        {

                            ctrlPersonCardWithFilter1.LoadPersonInfo(_Sharia.PersonID);
                            ctrlPersonCardWithFilter1.FilterEnabled = false;
                            IsPersonInfoLoaded = true;

                        }
                    }

                }

                else
                {
                    MessageBox.Show("لا يوجد مزاول للمهنة يحمل رقم التعريف = " + _PractitionerID, "لا يوجد مزاول بهذا الرقم التعريفي", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

            }
            catch (Exception ex)
            {
                clsGlobal.WriteEventToLogFile("You have a problem in loading Practitioner info into the form due to lack of data access," +
                    "try to check the passed Lawyer Profile before launch (FrmADdUpdatePractitioners),\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show("حدث عطل فني داخل النظام اثناء تحميل البيانات,احتمال وجود " +
                    "حذف باليانات لعدم القدرة على استرجاعها بشكل صحيح", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _SwitchCurrentMode();
                _ResetDefaultValues();

            }
        }
        private void frmAddUpdateRegulator_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();

        }
        private int _GetSelectedSubscriptionTypeID()
        {

            if (rbtnRegulatoryFree.Checked)
            {
                return 1;
            }
            else if (rbtnRegulatoryMedium.Checked)
            {
                return 2;
            }
            else
            {
                return 3;
            }


        }
        private int _GetSelectedSubscriptionWayID()
        {

            if (rbtnRScholarship.Checked)
            {
                return 1;
            }
            else if (rbtnRSpecialSupport.Checked)
            {
                return 2;
            }


            return 1;

        }
        private Dictionary<int, string> _GetRegulatorCasesPractice()
        {
            try
            {

                Dictionary<int, string> RegulatorCasesPracticeIdNameDictionary = new Dictionary<int, string>();

                // Get the selected items
                foreach (clsGlobal.CheckListBoxItem selectedItem in clbRegulatoryCasesTypes.CheckedItems)
                {

                    RegulatorCasesPracticeIdNameDictionary.Add(selectedItem.ID, selectedItem.Text);

                }

                return RegulatorCasesPracticeIdNameDictionary;


            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Regulator add/update form , getRegulatorCasesPractice().\n" +
                    ex.Message, System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show("Exception:\t" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;

            }

        }
        private Dictionary<int, string> _GetShariaCasesPractice()
        {
            try
            {

                Dictionary<int, string> ShariaCasesPracticeIdNameDictionary = new Dictionary<int, string>();

                // Get the selected items
                foreach (clsGlobal.CheckListBoxItem selectedItem in clbShariaCasesTypes.CheckedItems)
                {

                    ShariaCasesPracticeIdNameDictionary.Add(selectedItem.ID, selectedItem.Text);

                }

                return ShariaCasesPracticeIdNameDictionary;


            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Sharia add/update form , getShariaCasesPractice().\n" +
                    ex.Message, System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show("Exception:\t" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;

            }

        }
        private Dictionary<int, string> _GetJudgerCasesPractice()
        {
            try
            {

                Dictionary<int, string> JudgerCasesPracticeIdNameDictionary = new Dictionary<int, string>();

                // Get the selected items
                foreach (clsGlobal.CheckListBoxItem selectedItem in clbShariaCasesTypes.CheckedItems)
                {

                    JudgerCasesPracticeIdNameDictionary.Add(selectedItem.ID, selectedItem.Text);

                }

                return JudgerCasesPracticeIdNameDictionary;


            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Judger add/update form , getJudgerCasesPractice().\n" +
                    ex.Message, System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show("Exception:\t" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;

            }

        }

        private void _AssignRegulator()
        {
            if (cbAddRegulator.Checked)
            {

                if (_RegulatorMode == enMode.AddNew)
                {

                    _Regulator.IssueDate = DateTime.Now;
                    _Regulator.CreatedByUserID = (int)clsGlobal.CurrentUser.UserID;

                }
                else if (_RegulatorMode == enMode.Update)//update
                {
                    _Regulator.LastEditByUserID = clsGlobal.CurrentUser.UserID;
                }

                _Regulator.PersonID = (int)ctrlPersonCardWithFilter1.PersonID;
                _Regulator.MemberShipNumber = ctbRegulatoryMemberShipNumber.Text.Trim();
                _Regulator.IsActive = chkRegulatorIsActive.Checked;
                _Regulator.SubscriptionTypeID = _GetSelectedSubscriptionTypeID();
                _Regulator.SubscriptionWayID = _GetSelectedSubscriptionWayID();
                _Regulator.RegulatorCasesPracticeIDNameDictionary = _GetRegulatorCasesPractice();

            }
        }
        private void _AssignSharia()
        {

            if (cbAddSharia.Checked)
            {
                if (_ShariaMode == enMode.AddNew)
                {
                    _Sharia.IssueDate = DateTime.Now;
                    _Sharia.CreatedByUserID = (int)clsGlobal.CurrentUser.UserID;
                }

                else if (_ShariaMode == enMode.Update)
                {
                    _Sharia.LastEditByUserID = clsGlobal.CurrentUser.UserID;
                }

                _Sharia.PersonID = (int)ctrlPersonCardWithFilter1.PersonID;
                _Sharia.ShariaLicenseNumber = ctbShariaLicenseNumber.Text.Trim();
                _Sharia.IsActive = chkShariaIsActive.Checked;
                _Sharia.SubscriptionTypeID = _GetSelectedSubscriptionTypeID();
                _Sharia.SubscriptionWayID = _GetSelectedSubscriptionWayID();
                _Sharia.ShariaCasesPracticeIDNameDictionary = _GetShariaCasesPractice();


            }

        }
        private void _AssignJudger()
        {

            if (cbAddJudger.Checked)
            {
                if (_JudgerMode == enMode.AddNew)
                {
                    _Judger.IssueDate = DateTime.Now;
                    _Judger.CreatedByUserID = (int)clsGlobal.CurrentUser.UserID;
                }

                else if (_JudgerMode == enMode.Update)
                {
                    _Judger.LastEditByUserID = clsGlobal.CurrentUser.UserID;
                }

                _Judger.PersonID = (int)ctrlPersonCardWithFilter1.PersonID;
                _Judger.IsActive = chkJudgerIsActive.Checked;
                _Judger.SubscriptionTypeID = _GetSelectedSubscriptionTypeID();
                _Judger.SubscriptionWayID = _GetSelectedSubscriptionWayID();
                _Judger.JudgeCasesPracticeIDNameDictionary = _GetJudgerCasesPractice();


            }

        }
        private bool _AssignData()
        {

            try
            {
                _AssignRegulator();
                _AssignSharia();
                _AssignJudger();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem occured while assign info to practitioner.\nException:"+ex.Message,"Failed.",MessageBoxButtons.OK,MessageBoxIcon.Error);

                clsGlobal.WriteEventToLogFile("Exception in practitioner form add/update assign DATA method: \n Exception:" + ex.Message,
                    System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine("Exception:"+ex.ToString());
                return false;
            }
            return true;
        }

        private void _SaveRegulator()
        {
            if (cbAddRegulator.Checked)
            {
                if (_Regulator.Save())
                {
                    lblRegulatorID.Text = _Regulator.RegulatorID.ToString();
                    _PractitionerID = _Regulator.PractitionerID;
                    //change form mode to update.
                    _Mode = enMode.Update;
                    _RegulatorMode = enMode.Update;
                    lblTitle.Text = "تحديث  و تعديل البيانات";
                    this.Text = "تحديث  و تعديل البيانات";
                    MessageBox.Show("حفظ البيانات للمحامي النظامي بنجاح.", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Save the entity to the database
                    // After saving successfully, raise the event
                    OnEntityAdded(EventArgs.Empty);
                }

                else
                {
                    MessageBox.Show("فشل: لم تحفظ البيانات المحامي النظامي بشكل صحيح.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void _SaveSharia()
        {
            if (cbAddSharia.Checked)
            {
                if (_Sharia.Save())
                {
                    lblShariaID.Text = _Sharia.ShariaID.ToString();
                    _PractitionerID = _Sharia.PractitionerID;
                    //change form mode to update.
                    _Mode = enMode.Update;
                    _ShariaMode = enMode.Update;
                    lblTitle.Text = "تحديث  و تعديل البيانات";
                    this.Text = "تحديث  و تعديل البيانات";
                    MessageBox.Show("حفظ البيانات للمحامي الشرعي بنجاح.", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Save the entity to the database
                    // After saving successfully, raise the event
                    OnEntityAdded(null);
                }

                else
                {
                    MessageBox.Show("فشل: لم تحفظ البيانات للمحامي الشرعي بشكل صحيح.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void _SaveJudger()
        {

            if (cbAddJudger.Checked)
            {
                if (_Judger.Save())
                {
                    lblJudgerID.Text = _Judger.JudgerID.ToString();
                    _PractitionerID = _Judger.PractitionerID;
                    //change form mode to update.
                    _Mode = enMode.Update;
                    _JudgerMode = enMode.Update;
                    lblTitle.Text = "تحديث  و تعديل البيانات";
                    this.Text = "تحديث  و تعديل البيانات";
                    MessageBox.Show("حفظ البيانات  للمحكم بنجاح.", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Save the entity to the database
                    // After saving successfully, raise the event
                    OnEntityAdded(null);
                }

                else
                {
                    MessageBox.Show("فشل: لم تحفظ البيانات للمحكم بشكل صحيح.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Think deeply about saving , and updating ...
            try
            {
                if (!this.ValidateChildren())
                {
                    //Here we don't  continue because the form is not valid
                    MessageBox.Show("بعض الحقول غير صالحة! ضع الماوس فوق الأيقونة(الأيقونات) الحمراء لرؤية الخطأ",
                        "خطاء في البيانات المدخلة", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                if (_AssignData())
                {
                    _SaveRegulator();
                    _SaveSharia();
                    _SaveJudger();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception :" + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //I stopped here...
        private void tbRegulatorMemberShip_Validating(object sender, CancelEventArgs e)
        {
            if (cbAddRegulator.Checked)
            {
                try
                {

                    string ErrorMessage = "";
                    if (!ctbRegulatoryMemberShipNumber.IsValid(ref ErrorMessage))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(ctbRegulatoryMemberShipNumber, ErrorMessage);
                        return;
                    }

                    else
                    {
                        e.Cancel = false;

                        errorProvider1.SetError(ctbRegulatoryMemberShipNumber, null);
                    }

                    if (_RegulatorMode == enMode.AddNew)
                    {

                        if (clsRegulator.IsRegulatorExist(ctbRegulatoryMemberShipNumber.Text.Trim(), clsRegulator.enSearchBy.MemberShipNumber))
                        {

                            e.Cancel = true;
                            errorProvider1.SetError(ctbRegulatoryMemberShipNumber, "رقم العضوية مستخدم بالفعل من قبل محامي أخر");
                            if (MessageBox.Show("هل تريد رؤية الملف الذي يحتوي على تطابق مع رقم العضوية؟\n",
                            "تاكيد ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {

                                frmRegulatorInfo frmRegulator = new frmRegulatorInfo(clsRegulator.Find(ctbRegulatoryMemberShipNumber.Text.Trim(),
                                    clsRegulator.enSearchBy.MemberShipNumber).RegulatorID);
                            }
                        }

                        else
                        {
                            e.Cancel = false;

                            errorProvider1.SetError(ctbRegulatoryMemberShipNumber, null);
                        };

                    }

                    else
                    {
                        //in case update make sure not to use another user name
                        if (_Regulator.MemberShipNumber != ctbRegulatoryMemberShipNumber.Text.Trim())
                        {
                            if (clsRegulator.IsRegulatorExist(ctbRegulatoryMemberShipNumber.Text.Trim(), clsRegulator.enSearchBy.MemberShipNumber))
                            {
                                e.Cancel = true;
                                errorProvider1.SetError(ctbRegulatoryMemberShipNumber, "رقم العضوية مستخدم بالفعل من قبل محامي أخر");
                                return;
                            }
                            else
                            {
                                e.Cancel = false;

                                errorProvider1.SetError(ctbRegulatoryMemberShipNumber, null);
                            };
                        }
                    }
                } catch (Exception ex)
                {

                    clsGlobal.WriteEventToLogFile("Exception happen in Add/update practitioner while validting the member ship of regulator " +
                        "\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    MessageBox.Show("حصل خطاء ما داخل النظام , اثناء محاولة رفع المحتوى المتطابق");
                }

            }

        }
        private void btnRegulatorInfoNext_Click(object sender, EventArgs e)
        {

            if (ctrlPersonCardWithFilter1.PersonID == null)
            {
                MessageBox.Show("يرجى اختيار شخص ما.", "اختار شخص ما", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }
            if (_RegulatorMode == enMode.AddNew &&
                 clsRegulator.IsRegulatorExist(ctrlPersonCardWithFilter1.PersonID,
                     clsRegulator.enSearchBy.PersonID))
            {

                MessageBox.Show("الشخص المحدد لديه ملف محامي(نظامي) بالفعل، يرجى اختيار شخص آخر", "اختار شخص اخر", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }


            else if (cbAddRegulator.Checked == false)
            {
                tcPractitionernfo.SelectedTab = tpRegulatorInfo;
                return;
            }

            else if (_RegulatorMode == enMode.Update)
            {

                btnSave.Enabled = true;
                tpRegulatorInfo.Enabled = true;
                tcPractitionernfo.SelectedTab = tpRegulatorInfo;
            }

            else
            {
                btnSave.Enabled = true;
                tpRegulatorInfo.Enabled = true;
                tcPractitionernfo.SelectedTab = tpRegulatorInfo;
            }


            

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {


        }

        private void tcRegulatorInfo_Selecting(object sender, TabControlCancelEventArgs e)
        {

            if(ctrlPersonCardWithFilter1.PersonID == null)
            {
                MessageBox.Show("يرجى اختيار شخص ما.", "اختار شخص ما", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }
  
            if (e.TabPage == tpRegulatorInfo) // 'tpRegulatorInfo' is the TabPage you want to restrict
            {
            
                if (_RegulatorMode == enMode.AddNew&&
                 clsRegulator.IsRegulatorExist(ctrlPersonCardWithFilter1.PersonID,
                     clsRegulator.enSearchBy.PersonID))
                {

                    MessageBox.Show("الشخص المحدد لديه ملف محامي(نظامي) بالفعل، يرجى اختيار شخص آخر", "اختار شخص اخر", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                    return;
                }

              
                else if (cbAddRegulator.Checked == false)
                { 
                    tcPractitionernfo.SelectedTab = tpRegulatorInfo;
                    return;
                }
              
                else if (_RegulatorMode == enMode.Update)
                {

                    btnSave.Enabled = true;
                    tpRegulatorInfo.Enabled = true;
                    tcPractitionernfo.SelectedTab = tpRegulatorInfo;
                }
          
                else
                {
                    btnSave.Enabled = true;
                    tpRegulatorInfo.Enabled = true;
                    tcPractitionernfo.SelectedTab = tpRegulatorInfo;
                }

            }
            
       

            else if (e.TabPage == tpShariaInfo)
            {
                if (_ShariaMode == enMode.Update && cbAddSharia.Checked == true && ctrlPersonCardWithFilter1.PersonID != null)
                {
                    btnSave.Enabled = true;
                    tpShariaInfo.Enabled = true;
                    tcPractitionernfo.SelectedTab = tpShariaInfo;
                }

                else if (_ShariaMode == enMode.AddNew && ctrlPersonCardWithFilter1.PersonID != null && cbAddSharia.Checked == true)
                {

                    if (clsSharia.IsShariaExist(ctrlPersonCardWithFilter1.PersonID,

                        clsSharia.enSearchBy.PersonID))

                    {

                        MessageBox.Show("الشخص المحدد لديه ملف محامي(شرعي) بالفعل، يرجى اختيار شخص آخر", "اختار شخص اخر", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        ctrlPersonCardWithFilter1.FilterFocus();

                    }


                    else if (cbAddSharia.Checked == true)

                    {

                        btnSave.Enabled = true;

                        tpShariaInfo.Enabled = true;

                        tcPractitionernfo.SelectedTab = tpShariaInfo;

                    }



                }
            }
            
         
       
        }

        [Obsolete("This condition ain't used anymore")]
        private void clbRegulatoryCasesTypes_Validating(object sender, CancelEventArgs e)
        {
            if (clbRegulatoryCasesTypes.CheckedItems.Count == 0)
            {

                errorProvider1.SetError(clbRegulatoryCasesTypes, "يجب اختيار قضية واحدة على الاقل.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(clbRegulatoryCasesTypes, "");

                e.Cancel = false;
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

            BackColor = System.Drawing.Color.Black;
        }

        private void btnBlackList_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Mode == enMode.Update)
                {
                    
                    if (_Regulator.IsPractitionerInBlackList())
                    {
                        DialogResult result = MessageBox.Show("الشخص مضاف بالفعل الى القائمة السوداء , ان كنت تريد التعديل اضعط على 'نعم ' ", "سؤال"
                             , MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.OK)
                        {
                            // get ID of black-list by practitioner ID 
                            FrmAddUpdateList form = new FrmAddUpdateList(_PractitionerID, 
                                 clsBlackList.Find(_PractitionerID, clsBlackList.enFindBy.PractitionerID).BlackListID,
                                 ctrlAddUpdateList.enCreationMode.BlackList);
                            form.ShowDialog();
                        }
                    }
                    //Check if there is already an exists ID  for his black-list ID .
                    else
                    {
                        FrmAddUpdateList form = new FrmAddUpdateList(_PractitionerID, ctrlAddUpdateList.enCreationMode.BlackList);
                        form.ShowDialog();
                    }
                }


                else
                {
                    MessageBox.Show("لا يوجد بيانات (مزاولة المنهة)داخل البرنامج,عليك اولا اضافته كمحامي ثم يمكنك اضافته الى القائمة السوداء.",
                       "فشل", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                clsHelperClasses.WriteEventToLogFile("Exception in FrmAddupdateRegulator, BtnBlackList()\nException:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }

        }

        private void clbRegulatoryCasesTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbRegulatoryCasesRecord.Text= clbRegulatoryCasesTypes.CheckedItems.Count.ToString();
        }

        private void frmAddUpdateRegulator_Shown(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();

        }

        private void btnResetCases_Click(object sender, EventArgs e)
        {
            // Iterate through each item in the CheckedListBox
            for (int i = 0; i < clbRegulatoryCasesTypes.Items.Count; i++)
            {
                // Set the Checked property of each item to false to uncheck it
                clbRegulatoryCasesTypes.SetItemChecked(i, false);
            }

            lbRegulatoryCasesRecord.Text=clbRegulatoryCasesTypes.CheckedItems.Count.ToString(); 
        }
        private void ctrlPersonCardWithFilter1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tpRegulatorInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnToShariaNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpShariaInfo.Enabled = true;
                tcPractitionernfo.SelectedTab = tpShariaInfo;
                return;
            }

            //in case of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != null)
            {

                if (clsSharia.IsShariaExist(ctrlPersonCardWithFilter1.PersonID,
                    clsSharia.enSearchBy.PersonID))
                {
                    MessageBox.Show("الشخص المحدد لديه ملف محامي(شرعي) بالفعل، يرجى اختيار شخص آخر", "اختار شخص اخر", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpShariaInfo.Enabled = true;
                    tcPractitionernfo.SelectedTab = tpShariaInfo;
                }

            }

            else
            {
                MessageBox.Show("يرجى اختيار شخص ما.", "اختار شخص ما", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }
        }

        private void btnResetShariaCases_Click(object sender, EventArgs e)
        {
            // Iterate through each item in the CheckedListBox
            for (int i = 0; i < clbShariaCasesTypes.Items.Count; i++)
            {
                // Set the Checked property of each item to false to uncheck it
                clbShariaCasesTypes.SetItemChecked(i, false);
            }

            lbShariaCasesRecord.Text = clbShariaCasesTypes.CheckedItems.Count.ToString();
        }

        private void btnToRegulatorPrevious_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID == null)
            {
                MessageBox.Show("يرجى اختيار شخص ما.", "اختار شخص ما", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }
            if (_RegulatorMode == enMode.AddNew &&
                 clsRegulator.IsRegulatorExist(ctrlPersonCardWithFilter1.PersonID,
                     clsRegulator.enSearchBy.PersonID))
            {

                MessageBox.Show("الشخص المحدد لديه ملف محامي(نظامي) بالفعل، يرجى اختيار شخص آخر", "اختار شخص اخر", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }


            else if (cbAddRegulator.Checked == false)
            {
                tcPractitionernfo.SelectedTab = tpRegulatorInfo;
                return;
            }

            else if (_RegulatorMode == enMode.Update)
            {

                btnSave.Enabled = true;
                tpRegulatorInfo.Enabled = true;
                tcPractitionernfo.SelectedTab = tpRegulatorInfo;
            }

            else
            {
                btnSave.Enabled = true;
                tpRegulatorInfo.Enabled = true;
                tcPractitionernfo.SelectedTab = tpRegulatorInfo;
            }

        }

        private void btnToPersonalPrevious_Click_1(object sender, EventArgs e)
        {
            tcPractitionernfo.SelectedTab = tpPersonalInfo;

        }

        private void clbShariaCasesTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbShariaCasesRecord.Text = clbShariaCasesTypes.CheckedItems.Count.ToString();


        }

        private void ctbShariaLicenseNumber_Validating(object sender, CancelEventArgs e)
        {

            if (cbAddSharia.Checked)
            {

            try
            { 
           
            string ErrorMessage = "";
            if (!ctbShariaLicenseNumber.IsValid(ref ErrorMessage))
            {
                e.Cancel = true;
                errorProvider1.SetError(ctbShariaLicenseNumber, ErrorMessage);
                return;
            }

            else
            {
                e.Cancel = false;

                errorProvider1.SetError(ctbShariaLicenseNumber, null);
            }

            if (_ShariaMode == enMode.AddNew)
            {

                if (clsSharia.IsShariaExist(ctbShariaLicenseNumber.Text.Trim(), clsSharia.enSearchBy.ShariaLicenseNumber))
                {

                    e.Cancel = true;
                    errorProvider1.SetError(ctbShariaLicenseNumber, "رقم الاجازة الشرعية مستخدم بالفعل من قبل محامي أخر");
                    if (MessageBox.Show("هل تريد رؤية الملف الذي يحتوي على تطابق مع رقم الاجازة الشرعية؟\n",
                    "تاكيد ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        MessageBox.Show("Not Implemented yet");
                    }
                }

                else
                {
                    e.Cancel = false;

                    errorProvider1.SetError(ctbShariaLicenseNumber, null);
                };

            }

            else
            {
                //in case update make sure not to use another user name
                if (_Sharia.ShariaLicenseNumber!= ctbShariaLicenseNumber.Text.Trim())
                {
                    if (clsSharia.IsShariaExist(ctbShariaLicenseNumber.Text.Trim(), clsSharia.enSearchBy.ShariaLicenseNumber))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(ctbShariaLicenseNumber, "رقم الاجازة الشرعية مستخدم بالفعل من قبل محامي أخر");
                        return;
                    }
                    else
                    {
                        e.Cancel = false;

                        errorProvider1.SetError(ctbShariaLicenseNumber, null);
                    };
                }
                }

            }
            catch (Exception ex)
            {


                clsGlobal.WriteEventToLogFile("Exception happen in Add/update practitioner while validting the License number of Sharia" +
                    "\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("حصل خطاء ما داخل النظام , اثناء محاولة رفع المحتوى المتطابق");

            }
            }

        }

        private void _UpdateSaveButton(object sender, EventArgs e)
        {
            if(ctrlPersonCardWithFilter1.PersonID!=null&& cbAddExpert.Checked ||
               ctrlPersonCardWithFilter1.PersonID != null && cbAddJudger.Checked ||
               ctrlPersonCardWithFilter1.PersonID != null && cbAddRegulator.Checked ||
               ctrlPersonCardWithFilter1.PersonID != null && cbAddSharia.Checked)
            {

            btnSave.Enabled = true;
            }
            else
            { btnSave.Enabled = false; }
        }
        private void _UpdateTabs(object sender ,EventArgs e)
        {
            tpShariaInfo.Enabled = cbAddSharia.Checked && ctrlPersonCardWithFilter1.SelectedPersonInfo != null&&
                               !clsSharia.IsShariaExist(ctrlPersonCardWithFilter1.PersonID, clsSharia.enSearchBy.PersonID);

            tpRegulatorInfo.Enabled = cbAddRegulator.Checked && ctrlPersonCardWithFilter1.SelectedPersonInfo != null&&
                !clsRegulator.IsRegulatorExist(ctrlPersonCardWithFilter1.PersonID,clsRegulator.enSearchBy.PersonID);
            //.Enabled = SASA.Checked;
            //  SASA.Enabled = S.Checked;
        }
        private void cbAdd_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateTabs(sender,e);

            _UpdateSaveButton(null,null);
        }

        private void RadioButton_SubscriptionType_CheckedChanged(object sender, EventArgs e)
        {

            bool isChecked = false;
            if (sender is RadioButton radioButton)
            {
                isChecked = radioButton.Checked;

                if (int.TryParse(radioButton.Tag.ToString(),out int TagValue)) 
                {
                    if (TagValue == (int)enSubscriptionType.Free)
                    {
                        rbtnRegulatoryFree.Checked = rbtnShariaFree.Checked = isChecked;
                    }
                    else if (TagValue == (int)enSubscriptionType.Medium)
                    {
                        rbtnRegulatoryMedium.Checked = rbtnShariaMedium.Checked = isChecked;
                    }
                    else if(TagValue == (int)enSubscriptionType.Special)
                    {
                        rbtnRegulatorySpecial.Checked = rbtnShariaSpecial.Checked = isChecked;
                    }
                }

            }

        }

        private void tcPractitionernfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(tcPractitionernfo.SelectedIndex ==0)
            {
                if (_Mode == enMode.AddNew)
                {
                    lblTitle.Text = "أضافة مزاول جديد للمهنة";
                    this.Text = "أضافة مزاول جديد للمهنة";
                    lblTitle.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);

                }


                else if (_Mode == enMode.Update)
                {
                  
                    lblTitle.Text = "تحديث و تعديل بيانات مزاول للمهنة";
                    this.Text = "تحديث و تعديل بيانات مزاول للمهنة";
                    lblTitle.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);

                }
            }
          
            else if (tcPractitionernfo.SelectedIndex == 1)
            {
                if (_RegulatorMode == enMode.AddNew)
                {
                    lblTitle.Text = "أضافة محامي نظامي جديد";
                    lblTitle.ForeColor = System.Drawing.Color.SandyBrown;

                }
                else if(_RegulatorMode == enMode.Update)
                {
                    lblTitle.Text = "تحديث او تعديل بيانات المحامي نظامي";
                    lblTitle.ForeColor = System.Drawing.Color.SandyBrown;

                }
            }
      
            else if (tcPractitionernfo.SelectedIndex == 2)
            {
                if (_ShariaMode == enMode.AddNew)
                {
                    lblTitle.Text = "أضافة محامي شرعي جديد";
                    lblTitle.ForeColor = System.Drawing.Color.LimeGreen;

                }
                else if (_ShariaMode == enMode.Update)
                {
                    lblTitle.Text = "تحديث او تعديل بيانات المحامي الشرعي ";
                    lblTitle.ForeColor = System.Drawing.Color.LimeGreen;
                }
            }
        
        }

        private void RadioButton_SubscriptionWay_CheckedChanged(object sender, EventArgs e)
        {

            bool isChecked = false;
            if (sender is RadioButton radioButton)
            {
                isChecked = radioButton.Checked;
               
                if (int.TryParse(radioButton.Tag.ToString(), out int TagValue))
                {
                    if (TagValue == (int)enSubscriptionWay.SpecialSupport)
                    {
                        rbtnRSpecialSupport.Checked = rbtnSSpecialSupport.Checked=isChecked;
                    }
                    else if (TagValue == (int)enSubscriptionWay.scholarship)
                    {
                        rbtnSScholarship.Checked = rbtnRScholarship.Checked = isChecked;
                    }
                  
                }
            }
        }
   
        private void frmAddUpdatePractitioner_KeyPress(object sender, KeyPressEventArgs e)
        {
            ctrlPersonCardWithFilter1.PerFormClick();

        }

        private void btnRegulatoryWhiteList_Click(object sender, EventArgs e)
        {
            try
            {
                if (_PractitionerID != -1 && clsRegulator.IsRegulatorExist(_PractitionerID, clsRegulator.enSearchBy.PractitionerID))
                {
                    if (_Mode == enMode.Update && _RegulatorMode == enMode.Update 
                    && _Regulator.IsRegulatorInWhiteList())
                {
                    DialogResult result = MessageBox.Show("الشخص مضاف بالفعل الى القائمة البيضاء للنظامين , ان كنت تريد التعديل اضعط على 'نعم ' ", "سؤال"
                         , MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.OK)
                    {
                        // get ID of white-list by practitioner ID 
                        FrmAddUpdateList form = new FrmAddUpdateList(_PractitionerID,
                            (int)clsWhiteList.Find(_PractitionerID, clsPractitioner.enPractitionerType.Regulatory).WhiteListID,
                             ctrlAddUpdateList.enCreationMode.RegulatoryWhiteList);
                        form.ShowDialog();
                    }
                }
                else
                {

                    FrmAddUpdateList AddUpdateWhiteList = new FrmAddUpdateList(_PractitionerID,
                    ctrlAddUpdateList.enCreationMode.RegulatoryWhiteList);
                    AddUpdateWhiteList.ShowDialog();

                    }
                }
                else
                {
                    MessageBox.Show(text: "عليك اولا اضافة ملف للمحامي النظامي قبل اضافته الى احد القوائم."
                   , "فشل", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("I have a problem in loading white list form for practitioners type " +
                    "regulators,You might need to check add/update practitioner form \nexception:" + ex.Message
                    ,System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show(text: "Problem happen in the system while loading data,you might need to contact maintainance team."
                    , "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Exception:\t" + ex.Message);

            }
           

        }

        private void btnRegulatoryClosedList_Click(object sender, EventArgs e)
        {
            try
            {
                if (_PractitionerID != -1 && clsRegulator.IsRegulatorExist(_PractitionerID, clsRegulator.enSearchBy.PractitionerID))
                {
                    if (_Mode == enMode.Update && _RegulatorMode == enMode.Update
                    && _Regulator.IsRegulatorInClosedList())
                    {
                        DialogResult result = MessageBox.Show("الشخص مضاف بالفعل الى القائمة المغلقة للنظامين , ان كنت تريد التعديل اضعط على 'نعم ' ", "سؤال"
                             , MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.OK)
                        {
                            // get ID of white-list by practitioner ID 
                            FrmAddUpdateList form = new FrmAddUpdateList(_PractitionerID,
                                (int)clsClosedList.Find(_PractitionerID, clsPractitioner.enPractitionerType.Regulatory).ClosedListID,
                                 ctrlAddUpdateList.enCreationMode.RegulatoryClosedList);
                            form.ShowDialog();
                        }
                    }
                    else
                    {

                        FrmAddUpdateList AddUpdateClosedList = new FrmAddUpdateList(_PractitionerID,
                        ctrlAddUpdateList.enCreationMode.RegulatoryClosedList);
                        AddUpdateClosedList.ShowDialog();

                    }
                }
                else
                {
                    MessageBox.Show(text: "عليك اولا اضافة ملف للمحامي النظامي قبل اضافته الى احد القوائم."
                   , "فشل", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("I have a problem in loading closed list form for practitioners type " +
                    "regulators,You might need to check add/update practitioner form \nexception:" + ex.Message
                    , System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show(text: "Problem happen in the system while loading data,you might need to contact maintenance team."
                    , "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Exception:\t" + ex.Message);

            }
        }

        private void btnShariaWhite_Click(object sender, EventArgs e)
        {
            try
            {
                if (_PractitionerID != -1 && clsSharia.IsShariaExist(_PractitionerID, clsSharia.enSearchBy.PractitionerID))
                {

                if (_Mode == enMode.Update && _ShariaMode == enMode.Update 
                    && _Sharia.IsShariaInWhiteList())
                {
                    DialogResult result = MessageBox.Show("الشخص مضاف بالفعل الى القائمة البيضاء للشرعيين , ان كنت تريد التعديل اضعط على 'نعم ' ", "سؤال"
                         , MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.OK)
                    {
                        // get ID of white-list by practitioner ID 
                        FrmAddUpdateList form = new FrmAddUpdateList(_PractitionerID,
                            (int)clsWhiteList.Find(_PractitionerID, clsPractitioner.enPractitionerType.Sharia).WhiteListID,
                             ctrlAddUpdateList.enCreationMode.ShariaWhiteList);
                        form.ShowDialog();
                    }
                }
                else
                {

                    FrmAddUpdateList AddUpdateWhiteList = new FrmAddUpdateList(_PractitionerID,
                    ctrlAddUpdateList.enCreationMode.ShariaWhiteList);
                    AddUpdateWhiteList.ShowDialog();

                }
                }
                else
                {
                    MessageBox.Show(text: "عليك اولا اضافة ملف للمحامي الشرعي قبل اضافته الى احد القوائم."
                   , "فشل", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("I have a problem in loading white list form for practitioners type " +
                    "sharia,You might need to check add/update practitioner form \nexception:" + ex.Message
                    , System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show(text: "Problem happen in the system while loading data,you might need to contact maintenance team."
                    , "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Exception:\t" + ex.Message);

            }

        }

        private void btnShariaClosedList_Click(object sender, EventArgs e)

        {
            try
            {
                if (_PractitionerID != -1 && clsSharia.IsShariaExist(_PractitionerID, clsSharia.enSearchBy.PractitionerID))
                {
                    if (_Mode == enMode.Update && _ShariaMode == enMode.Update 
                    && _Sharia.IsShariaInClosedList())
                {
                    DialogResult result = MessageBox.Show("الشخص مضاف بالفعل الى القائمة المغلقة للشرعيين , ان كنت تريد التعديل اضعط على 'نعم ' ", "سؤال"
                         , MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.OK)
                    {
                        // get ID of white-list by practitioner ID 
                        FrmAddUpdateList form = new FrmAddUpdateList(_PractitionerID,
                            (int)clsClosedList.Find(_PractitionerID, clsPractitioner.enPractitionerType.Sharia).ClosedListID,
                             ctrlAddUpdateList.enCreationMode.ShariaClosedList);
                        form.ShowDialog();
                    }
                }
                else
                {

                    FrmAddUpdateList AddUpdateClosedList = new FrmAddUpdateList(_PractitionerID,
                    ctrlAddUpdateList.enCreationMode.ShariaClosedList);
                    AddUpdateClosedList.ShowDialog();

                    }
                }
                else
                {
                    MessageBox.Show(text: "عليك اولا اضافة ملف للمحامي الشرعي قبل اضافته الى احد القوائم."
                   , "فشل", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("I have a problem in loading closed list form for practitioners type " +
                    "sharia,You might need to check add/update practitioner form \nexception:" + ex.Message
                    , System.Diagnostics.EventLogEntryType.Error);

                MessageBox.Show(text: "Problem happen in the system while loading data,you might need to contact maintenance team."
                    , "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Exception:\t" + ex.Message);

            }
        }
    }

}
