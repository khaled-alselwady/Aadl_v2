using AADL.GlobalClasses;
using AADL.Lists;
using AADL.People;
using AADL.Properties;
using AADLBusiness;
using AADLBusiness;
using AADLBusiness.Sharia;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AADL.People.ctrlPersonCard;
using static AADL.Regulators.ctrlRegulatorCard;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AADL.Regulators
{
    public partial class ctrlRegulatorCard : UserControl
    {
        public enum LoadRegulatorBy { PersonID, RegulatorID, MemberShipNumber,PractitionerID };
        public enum LoadShariaBy { PersonID, ShariaID, ShariaLicenseNumber, PractitionerID };

        public enum enCreationMode { Regulator,Sharia,Expert,Judger}
        private clsRegulator _Regulator;
        private clsSharia _Sharia;


        private int? _RegulatorID = null;

        public int? RegulatorID
        {
            get { return _RegulatorID; }
        }

        private int? _ShariaID = null;

        public int? ShariaID
        {
            get { return _ShariaID; }
        }

        public clsRegulator SelectedRegulatorInfo
        {
            get { return _Regulator; }
        }

        public clsSharia SelectedShariaInfo
        {
            get { return _Sharia; }
        }
        public ctrlRegulatorCard()
        {
            InitializeComponent();
        }

        public void LoadRegulatorInfo<T>(T Value, LoadRegulatorBy enLoadRegulatorBy)
        {
            switch (enLoadRegulatorBy)
            {
                case LoadRegulatorBy.RegulatorID:
                    {

                        if (Value != null && int.TryParse(Value.ToString(), out int RegulatorID))
                        {
                            _Regulator = clsRegulator.Find(RegulatorID, clsRegulator.enSearchBy.RegulatorID);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + RegulatorID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;
                    }

                case LoadRegulatorBy.PersonID:
                    {

                        if (Value != null && int.TryParse(Value.ToString(), out int PersonID))
                        {
                            _Regulator = clsRegulator.Find(PersonID, clsRegulator.enSearchBy.PersonID);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + PersonID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;
                    }

                case LoadRegulatorBy.MemberShipNumber:
                    {
                        string MemberShipNumber = Value.ToString();
                        if (!string.IsNullOrEmpty(MemberShipNumber))
                        {
                            _Regulator = clsRegulator.Find(MemberShipNumber, clsRegulator.enSearchBy.MemberShipNumber);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + MemberShipNumber.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;
                    }

                case LoadRegulatorBy.PractitionerID:
                    {

                        if (Value != null && int.TryParse(Value.ToString(), out int PractitionerID))
                        {
                            _Regulator = clsRegulator.Find(PractitionerID, clsRegulator.enSearchBy.PractitionerID);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + PractitionerID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;

                    }

            }
            _FillRegulatorInfo();

        }

        public void LoadShariaInfo<T>(T Value, LoadShariaBy enLoadShariaBy)
        {
            switch (enLoadShariaBy)
            {
                case LoadShariaBy.ShariaID:
                    {

                        if (Value != null && int.TryParse(Value.ToString(), out int RegulatorID))
                        {
                            _Sharia = clsSharia.Find(RegulatorID, clsSharia.enSearchBy.ShariaID);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + RegulatorID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;
                    }

                case LoadShariaBy.PersonID:
                    {

                        if (Value != null && int.TryParse(Value.ToString(), out int PersonID))
                        {
                            _Regulator = clsRegulator.Find(PersonID, clsRegulator.enSearchBy.PersonID);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + PersonID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;
                    }

                case LoadShariaBy.ShariaLicenseNumber:
                    {
                        string MemberShipNumber = Value.ToString();
                        if (!string.IsNullOrEmpty(MemberShipNumber))
                        {
                            _Regulator = clsRegulator.Find(MemberShipNumber, clsRegulator.enSearchBy.MemberShipNumber);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + MemberShipNumber.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;
                    }

                case LoadShariaBy.PractitionerID:
                    {

                        if (Value != null && int.TryParse(Value.ToString(), out int PractitionerID))
                        {
                            _Regulator = clsRegulator.Find(PractitionerID, clsRegulator.enSearchBy.PractitionerID);
                            if (_Regulator == null)
                            {
                                ResetRegulatorInfo();
                                MessageBox.Show("لا يوجد شخص يحمل (الرقم التعريفي) .  = " + PractitionerID.ToString(), "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        break;

                    }

            }
            _FillRegulatorInfo();

        }

        private void _FillRegulatorInfo()
        {
            try
            {
                //Link - label.+ (black-white-closed lists)
                llEditRegulatorInfo.Enabled = true;
                lbLawyer.Enabled = true;
                lbBlackList.Enabled = _Regulator.IsPractitionerInBlackList();
                //lbWhiteList.Enabled = (clsBlackList.IsLawyerInBlackList(_Regulator.LawyerID)!= null ? true : false);
                // lbClosedList.Enabled = (_Regulator.ClosedListID != null ? true : false);

                //Regulator info.
                _RegulatorID = _Regulator.RegulatorID;
                lblRegulatorID.Text = _Regulator.RegulatorID.ToString();
                lbMemberShip.Text = _Regulator.MemberShipNumber;
                lbSubscriptionType.Text = _Regulator.SubscriptionTypeInfo.SubscriptionName;
                lbSubscriptionWay.Text = _Regulator.SubscriptionWayInfo.SubscriptionName;

                //Edit and create 
                lbCreatedByUserID.Text = _Regulator.UserInfo.UserName;
                lbIssueDate.Text = _Regulator.IssueDate.ToShortDateString();
                lbLastEditDate.Text = _Regulator.LastEditDate == null?"[????]" : _Regulator.LastEditDate.ToString();

                //cases practice
                try
                {
                    if (_Regulator.RegulatorCasesPracticeIDNameDictionary != null && _Regulator.RegulatorCasesPracticeIDNameDictionary.Count != 0)
                    {

                        foreach (string CaseTypeName in _Regulator.RegulatorCasesPracticeIDNameDictionary.Values)
                        {
                            if (!string.IsNullOrEmpty(CaseTypeName))
                            {

                                lvCasestypes.Items.Add(CaseTypeName);
                            }
                        }

                    }
                    else
                    {
                        lvCasestypes.Items.Add("لا يوجد قضايا");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطاء فني اثناء اضافة قضايا المحامي .. \n" + ex.Message, "خطاء",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsGlobal.WriteEventToLogFile("Problem in regulator Card info ,while uploading cases practice,Exception:\n" +
                        ex.Message, System.Diagnostics.EventLogEntryType.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("بعض الحقول قد تكون فارغة بسسب مشكلة داخلية اثناء التحميل", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsHelperClasses.WriteEventToLogFile("Error at card regulator info ctrl , while loading properties of regulator info ,\n" +
                    "some properties drop exception:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }

        }
        public void ResetRegulatorInfo()
        {
            //Regulator info
            _RegulatorID = -1;
            lblRegulatorID.Text = "[????]";
            lbMemberShip.Text = "[????]";
            lbSubscriptionType.Text = "[????]";

            //Edit and create.
            lbCreatedByUserID.Text = "[????]";
            lbIssueDate.Text = "[????]";
            lbLastEditDate.Text = "[????]";

            ////list view 
            lvCasestypes.Items.Clear();
            lvCasestypes.Columns.Clear();

            //Link label
            llEditRegulatorInfo.Enabled = false;
            lbLawyer.Enabled = false;
            lbBlackList.Enabled = false;
            lbWhiteList.Enabled = false;
            lbClosedList.Enabled = false;
        }

        private void lbLawyer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            //Show lawyer info by ID ..

            //remove person info and replace with lawyer data. 

            //frmAddUpdatePerson frm = new frmAddUpdatePerson(ID);
            //frm.ShowDialog();

            ////refresh
            //LoadPersonInfo(_PersonID.Value, LoadPersonBy.PersonID);

        }

        private void lbClosedList_Click(object sender, EventArgs e)
        {

        }

        private void llEditRegulatorInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lbBlackList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmListInfo frmListInfo = new frmListInfo(clsBlackList.Find(_Regulator.PractitionerID, clsBlackList.enFindBy.PractitionerID).BlackListID
                    , ctrlListInfo.CreationMode.BlackList);
                frmListInfo.ShowDialog();
            }catch (Exception ex)
            {
                Console.WriteLine("Exception:"+ex.ToString());

                MessageBox.Show("لقد حدث عطل فني داخل النظام , اثناء محاولة استرجاع البيانات .","فشل",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void lbWhiteList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lbClosedList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

     

        private void أضافةToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check that the selected items is bigger than zero

        }

        private void lvCasestypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
