using AADLBusiness;
using AADLBusiness.Lists.Closed;
using AADLBusiness.Lists.WhiteList;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AADL.clsGlobal;
using static AADL.Lists.ctrlListInfo;

namespace AADL.Lists
{
    public partial class ctrlAddUpdateList : UserControl
    {
       private enum problemTypes { LoadingInfo}

        private int _BlackListID = -1;
        private int _WhiteListID = -1;
        private int _ClosedListID= -1;

        private clsBlackList _BlackList;
        private clsWhiteList _WhiteList;
        private clsClosedList _ClosedList;

        private int _PractitionerID = -1;

        public EventHandler<int> OnListCreation;

        // I might create sub-enum for specilized lists
        public enum enCreationMode { BlackList,RegulatoryWhiteList,RegulatoryClosedList,
           ShariaWhiteList, ShariaClosedList};
        public enum enMode { AddNew,Update};
        private enCreationMode _CreationMode= enCreationMode.BlackList;
        private enMode _Mode=enMode.AddNew;
        public ctrlAddUpdateList()
        {
            InitializeComponent();
        }
        private bool _LoadBlackListReasonsInfo()
        {
          
            try
            {

                Dictionary<int,string> PractitionerBlackListReasonsIdNameDictionary= _BlackList.BlackListPractitionerReasonsIDNamesDictionary;
              
                if (PractitionerBlackListReasonsIdNameDictionary == null)
                {
                    MessageBox.Show("يوجد خطاء ما في تحميل اسباب القائمة السوداءة,القائمة لا تحمل اسباب!. ", "");
                    return false;
                }
           
                for (int Idx = 0; Idx < clbListReasons.Items.Count; Idx++)
                {

                    CheckListBoxItem item = (CheckListBoxItem)clbListReasons.Items[Idx];

                    if (PractitionerBlackListReasonsIdNameDictionary.ContainsKey(item.ID))
                    {

                        // Set the item as checked
                        clbListReasons.SetItemChecked(Idx, true);
                    }

                }


            }
            catch(Exception ex)
            {
                
                MessageBox.Show("حدث خطاء اثناء تحميل البيانات", "فشل"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGlobal.WriteEventToLogFile("Exception due to loading list reasons in add/update list form.\n"+ex.Message
                    ,System.Diagnostics.EventLogEntryType.Error);
            
                return false;
            
            }

            return true;

        }

        private bool _LoadWhiteListReasonsInfo()
        {

            try
            {

                Dictionary<int, string> PractitionerWhiteListReasonsIdNameDictionary = _WhiteList.WhiteListPractitionerReasonsIDNamesDictionary;

                if (PractitionerWhiteListReasonsIdNameDictionary == null)
                {
                    MessageBox.Show("يوجد خطاء ما في تحميل اسباب القائمة البيضاء  ,القائمة لا تحمل اسباب!. ", "");
                    return false;
                }

                for (int Idx = 0; Idx < clbListReasons.Items.Count; Idx++)
                {

                    CheckListBoxItem item = (CheckListBoxItem)clbListReasons.Items[Idx];

                    if (PractitionerWhiteListReasonsIdNameDictionary.ContainsKey(item.ID))
                    {

                        // Set the item as checked
                        clbListReasons.SetItemChecked(Idx, true);
                    }

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("حدث خطاء اثناء تحميل البيانات", "فشل"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGlobal.WriteEventToLogFile("Exception due to loading list reasons in add/update list form.\n" + ex.Message
                    , System.Diagnostics.EventLogEntryType.Error);

                return false;

            }

            return true;

        }

        private bool _LoadClosedListReasonsInfo()
        {

            try
            {

                Dictionary<int, string> PractitionerClosedListReasonsIdNameDictionary = _ClosedList.ClosedListPractitionerReasonsIDNamesDictionary;

                if (PractitionerClosedListReasonsIdNameDictionary == null)
                {
                    MessageBox.Show("يوجد خطاء ما في تحميل اسباب القائمة المغلقة  ,القائمة لا تحمل اسباب!. ", "");
                    return false;
                }

                for (int Idx = 0; Idx < clbListReasons.Items.Count; Idx++)//O(n)
                {

                    CheckListBoxItem item = (CheckListBoxItem)clbListReasons.Items[Idx];

                    if (PractitionerClosedListReasonsIdNameDictionary.ContainsKey(item.ID))//O(1)
                    {

                        // Set the item as checked
                        clbListReasons.SetItemChecked(Idx, true);
                    }

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("حدث خطاء اثناء تحميل البيانات", "فشل"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsGlobal.WriteEventToLogFile("Exception due to loading list reasons in add/update list form.\n" + ex.Message
                    , System.Diagnostics.EventLogEntryType.Error);

                return false;

            }

            return true;

        }

        private bool _SetBlackListSettings()
        { 
            try
            {
                handleLabelsBasedOnMode();
                clbListReasons.Items.Clear();
                DataTable BlackListReasonsDataTable =
                     clsListReasons.GetAllBlackListReasons();

                if (BlackListReasonsDataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in BlackListReasonsDataTable.Rows)
                    {
                        CheckListBoxItem newItem = new CheckListBoxItem((int)row["BlackListReasonID"],
                             row["BlackListReasonName"].ToString());

                        clbListReasons.Items.Add(newItem); // Add item using Name column

                    }

                }

                

            }catch(Exception ex)
            {
                Console.WriteLine("Exception:\t" + ex.Message);
                WriteEventToLogFile("This exception was dropped in ctrlAdd\'Updatelist in _SetBlackListSettings():\n" +
                    ex.Message, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Didn't manage to upload blacklists reasons\n");
                return false;
            }

            return true;
            //Case white -closed...
        }
        private void handleLabelsBasedOnMode()
        {

            if (_Mode == enMode.AddNew)
            {

            switch (_CreationMode)
            {
                case enCreationMode.BlackList:
                    {
                        lblTitle.Text = "أضف الى القائمة السوداء";
                        this.Text = "لوحة القائمة السوداء";
                        this.gbList.Text = "القائمة السوداء";
                        break;
                    }
          
                case enCreationMode.RegulatoryWhiteList:
                    {
                        lblTitle.Text = "أضف الى القائمة البيضاء النظامين";
                        this.Text = "لوحة القائمة البيضاء النظامين";
                        this.gbList.Text = "القائمة البيضاء النظامين";
                        break;
                    }

                case enCreationMode.RegulatoryClosedList:
                    {
                        lblTitle.Text = "أضف الى القائمة المغلقة النظامين";
                        this.Text = "لوحة القائمة المغلقة النظامين";
                        this.gbList.Text = "القائمة المغلقة النظامين";
                        break;
                    }
                  
                case enCreationMode.ShariaWhiteList:
                        {
                            lblTitle.Text = "أضف الى القائمة البيضاء للشرعيين";
                            this.Text = "لوحة القائمة البيضاء للشرعيين";
                            this.gbList.Text = "القائمة البيضاء للشرعيين";
                            break;
                        }
         
                case enCreationMode.ShariaClosedList:
                        {
                            lblTitle.Text = "أضف الى القائمة المغلقة للشرعيين";
                            this.Text = "لوحة القائمة المغلقة للشرعيين";
                            this.gbList.Text = "القائمة المغلقة للشرعيين";
                            break;
                        }

                }

            }

            else
            {
                switch (_CreationMode)
                {
                    case enCreationMode.BlackList:
                        {
                            lblTitle.Text = "تحديث و تعديل القائمة السوداء";
                            break;
                        }

                    case enCreationMode.RegulatoryWhiteList:
                        {
                            lblTitle.Text = "تحديث و تعديل القائمة البيضاء للنظامين";
                            break;
                        }

                    case enCreationMode.RegulatoryClosedList:
                        {
                            lblTitle.Text = "تحديث و تعديل القائمة المغلقة للنظامين";
                            break;
                        }

                    case enCreationMode.ShariaWhiteList:
                        {
                            lblTitle.Text = "تحديث و تعديل القائمة البيضاء للشرعيين";
                            break;
                        }

                    case enCreationMode.ShariaClosedList:
                        {
                            lblTitle.Text = "تحديث و تعديل القائمة المغلقة للشرعيين";
                            break;
                        }

                }

            }
       
        }
        private bool _SetWhiteListSettings()
        {
            try
            {

                handleLabelsBasedOnMode();
                clbListReasons.Items.Clear();
                DataTable WhiteListReasonsDataTable =
                     clsListReasons.GetAllWhiteListReasons();

                if (WhiteListReasonsDataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in WhiteListReasonsDataTable.Rows)
                    {
                        CheckListBoxItem newItem = new CheckListBoxItem((int)row["WhiteListReasonID"],
                             row["WhiteListReasonName"].ToString());

                        clbListReasons.Items.Add(newItem); // Add item using Name column

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:\t" + ex.Message);
                WriteEventToLogFile("This exception was dropped in ctrlAdd\'Updatelist in _SetWhiteListSettings():\n" +
                    ex.Message, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Didn't manage to upload WhiteLists reasons\n");
                return false;
            }

            return true;
            //Case white -closed...
        }
    
        private bool _SetClosedListSettings()

        {
            try
            {

                handleLabelsBasedOnMode();
                clbListReasons.Items.Clear();
                DataTable ClosedListReasonsDataTable =
                     clsListReasons.GetAllClosedListReasons();

                if (ClosedListReasonsDataTable.Rows.Count > 0)
                {

                    foreach (DataRow row in ClosedListReasonsDataTable.Rows)
                    {
                        CheckListBoxItem newItem = new CheckListBoxItem((int)row["ClosedListReasonID"],
                             row["ClosedListReasonName"].ToString());

                        clbListReasons.Items.Add(newItem); // Add item using Name column

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:\t" + ex.Message);
                WriteEventToLogFile("This exception was dropped in ctrlAdd\'Updatelist in _SetClosedListSettings():\n" +
                    ex.Message, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Didn't manage to upload ClosedLists reasons\n");
                return false;
            }

            return true;
            //Case white -closed...
        }

        private bool _ResetToDefaultValues()
        {
            _PractitionerID = -1;
            _BlackListID = -1;
            _WhiteListID = -1;
            _ClosedListID = -1;
            _BlackList = null;
            _WhiteList = null;
            _ClosedList = null;

            btnDelete.Visible = false;
            tbListNote.Text = "";
            lbListlD.Text = "[???]";
            _Mode = enMode.AddNew;
            try
            {

                switch (_CreationMode)
                {
                    case enCreationMode.BlackList:
                        {

                            return _SetBlackListSettings();
                                
                        }
      
                    case enCreationMode.RegulatoryWhiteList:
                        {

                            return _SetWhiteListSettings();

                        }
                    case enCreationMode.ShariaWhiteList:
                        {

                            return _SetWhiteListSettings();

                        }

                    case enCreationMode.RegulatoryClosedList:
                        {

                            return _SetClosedListSettings();

                        }
                    case enCreationMode.ShariaClosedList:
                        {

                            return _SetClosedListSettings();

                        }
                }


            }
            catch (Exception ex)
            {
                WriteEventToLogFile("Exception dropped at regulator form add/update while loading lists reasons",
                    System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Exception dropped while loading Lists Reasons !!\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        
        private void ShowErrorMessageBasedOnCreationMode(problemTypes type)
        {
           
            string ErrorMessage = "";
            if (type == problemTypes.LoadingInfo)
            {

            switch (_CreationMode)
            {
                case enCreationMode.RegulatoryClosedList:
                    {
                        ErrorMessage = "لم استطع ايجاد بيانات خاصة بهذا الشخص داخل القائمة المغلقة الخاصة بالنظامين";
                        break;
                    }
                case enCreationMode.RegulatoryWhiteList:
                    {
                        ErrorMessage = "لم استطع ايجاد بيانات خاصة بهذا الشخص داخل القائمة البيضاء الخاصة بالنظامين";
                        break;
                    }
                case enCreationMode.ShariaWhiteList:
                    {
                        ErrorMessage = "لم استطع ايجاد بيانات خاصة بهذا الشخص داخل القائمة البيضاء الخاصة بالشرعيين";
                        break;
                    }
                case enCreationMode.ShariaClosedList:
                    {
                        ErrorMessage = "لم استطع ايجاد بيانات خاصة بهذا الشخص داخل القائمة المغلقة الخاصة بالشرعيين";
                        break;
                    }
            }
            

            }
            MessageBox.Show(ErrorMessage,"فشل",MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        public  bool LoadInfo(int PractitionerID, enCreationMode CreationMode,int ListID=-1)
        {
          
            try
            {
                _CreationMode = CreationMode;
                _ResetToDefaultValues();

                // Decide creationMode

                _PractitionerID = PractitionerID;
               
               if (_PractitionerID == -1)
               {
                MessageBox.Show("You can't add Practitioner To List while its ID =-1","Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
               }
           
           
                if(CreationMode==enCreationMode.BlackList)
               {

                _BlackListID = ListID;
                _BlackList = new clsBlackList();

               
                //Decide mode (Add,Update)
                if (_BlackListID == -1)
                {
                    _Mode=enMode.AddNew;

                }
                    
                    else
                    {
                        _Mode = enMode.Update;
                        _BlackList = clsBlackList.Find(_BlackListID, clsBlackList.enFindBy.BlackListID);

                        if (_BlackList == null)
                        {
                            ShowErrorMessageBasedOnCreationMode(problemTypes.LoadingInfo);


                            return false;
                        }
                    }

                    if (_Mode == enMode.Update )
                    {

                        lbListlD.Text = _BlackList.BlackListID.ToString();
                        tbListNote.Text = (_BlackList.Notes ?? "").ToString();
                        btnDelete.Visible = true;
                        if (!_LoadBlackListReasonsInfo())
                        {
                            return false;
                        }
                    }
                    handleLabelsBasedOnMode();


                }

                else if (CreationMode == enCreationMode.RegulatoryWhiteList||
                    CreationMode == enCreationMode.ShariaWhiteList)
                {

                    _WhiteListID = ListID;
                    _WhiteList = new clsWhiteList();


                    //Decide mode (Add,Update)
                    if (_WhiteListID == -1)
                    {
                        _Mode = enMode.AddNew;

                    }
     
                    else
                    {
                        _Mode = enMode.Update;
                        _WhiteList = clsWhiteList.Find(_WhiteListID);

                        if (_WhiteList == null)
                        {
                            ShowErrorMessageBasedOnCreationMode(problemTypes.LoadingInfo);
                            return false;
                        }
                    }

                    if (_Mode == enMode.Update )
                    {

                        lbListlD.Text = _WhiteList.WhiteListID.ToString();
                        tbListNote.Text = (_WhiteList.Notes ?? "").ToString();
                        btnDelete.Visible = true;
                        if (!_LoadWhiteListReasonsInfo())
                        {
                            return false;
                        }
                    
                    }
                    handleLabelsBasedOnMode();

                }

                else if (CreationMode == enCreationMode.RegulatoryClosedList||
                    CreationMode==enCreationMode.ShariaClosedList)
                {

                    _ClosedListID = ListID;
                    _ClosedList = new clsClosedList();


                    //Decide mode (Add,Update)
                    if (_ClosedListID == -1)
                    {
                        _Mode = enMode.AddNew;

                    }
                    else
                    {
                        _Mode = enMode.Update;
                        _ClosedList = clsClosedList.Find(_ClosedListID);

                        if (_ClosedList == null)
                        {
                            ShowErrorMessageBasedOnCreationMode(problemTypes.LoadingInfo);


                            return false;
                        }
                    }

                    if (_Mode == enMode.Update)
                    {

                        lbListlD.Text = _ClosedList.ClosedListID.ToString();
                        tbListNote.Text = (_ClosedList.Notes ?? "").ToString();
                        btnDelete.Visible = true;
                        if (!_LoadClosedListReasonsInfo())
                        {
                            return false;
                        }
                    }
                    handleLabelsBasedOnMode();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                WriteEventToLogFile("Exception dropped at regulator form add/update closed loading lists reasons",
                    System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Exception dropped while loading Lists Reasons !!\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           
            return true;
       
        }

        private Dictionary<int, string> _GetCheckedReasons()
        {
            try
            {
                Dictionary<int,string>ListReasonIDNameDictionary= new Dictionary<int, string>();

                foreach (CheckListBoxItem itemChecked in clbListReasons.CheckedItems)
                {

                    ListReasonIDNameDictionary.Add(itemChecked.ID, itemChecked.Text);
                                
                }

                return ListReasonIDNameDictionary;

            }
            catch(Exception ex)
            {
                MessageBox.Show("لقد حدث خطاء فني اثناء حفظ البيانات", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                clsGlobal.WriteEventToLogFile("Problem while getting reasons from check list box in form,\n"+ex.Message,
                    System.Diagnostics.EventLogEntryType.Error);
                return null;

            }
            

        }

        private clsPractitioner.enPractitionerType _ReturnPractitionerTypeBasedOnCreationMode()
        {
            switch (_CreationMode)
            {
                case enCreationMode.RegulatoryWhiteList:
                    {
                        return clsPractitioner.enPractitionerType.Regulatory;
                    }
                case enCreationMode.RegulatoryClosedList:
                    {
                        return clsPractitioner.enPractitionerType.Regulatory;

                    }
                case enCreationMode.ShariaWhiteList:
                    {
                        return clsPractitioner.enPractitionerType.Sharia;

                    }
                case enCreationMode.ShariaClosedList:
                    {
                        return clsPractitioner.enPractitionerType.Sharia;

                    }
            }

            return clsPractitioner.enPractitionerType.Regulatory;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we don't  continue because the form is not valid
                MessageBox.Show("بعض الحقول غير صالحة! ضع الماوس فوق الأيقونة(الأيقونات) الحمراء لرؤية الخطأ",
                    "خطاء في البيانات المدخلة", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
   
            if (_CreationMode == enCreationMode.BlackList)
            {
                if (_Mode == enMode.Update)
                {
                    _BlackList.LastEditByUserID = clsGlobal.CurrentUser.UserID;
                    _BlackList.LastEditDate = DateTime.Now;
                }

                else
                {
                    _BlackList.AddedToListDate = DateTime.Now;
                }

                _BlackList.PractitionerID = _PractitionerID;
                _BlackList.Notes = tbListNote.Text;
                _BlackList.CreatedByUserID = (int)clsGlobal.CurrentUser.UserID;
                _BlackList.BlackListPractitionerReasonsIDNamesDictionary = _GetCheckedReasons();
                //Assign Black-List-Reasons ...

                if (_BlackList.Save())
                {
                    _Mode = enMode.Update;
                    handleLabelsBasedOnMode();
                    MessageBox.Show("حفظ البيانات بنجاح.", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbListlD.Text = _BlackList.BlackListID.ToString();
                    btnDelete.Visible = true;
                }
         
                else
                {
                    MessageBox.Show("فشل: لم تحفظ البيانات بشكل صحيح.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
           
            }

            else if (_CreationMode == enCreationMode.RegulatoryWhiteList || _CreationMode == enCreationMode.ShariaWhiteList)
            {


                if (_Mode == enMode.Update)
                {
                    _WhiteList.LastEditByUserID = clsGlobal.CurrentUser.UserID;
                    _WhiteList.LastEditDate = DateTime.Now;
                }

                else
                {
                    _WhiteList.AddedToListDate = DateTime.Now;
                }

                _WhiteList.PractitionerID = _PractitionerID;
                _WhiteList.Notes = tbListNote.Text;
                _WhiteList.CreatedByUserID = (int)clsGlobal.CurrentUser.UserID;
                _WhiteList.WhiteListPractitionerReasonsIDNamesDictionary = _GetCheckedReasons();
                _WhiteList.PractitionerType = _ReturnPractitionerTypeBasedOnCreationMode();

                if (_WhiteList.Save())
                {
                    _Mode = enMode.Update;
                    handleLabelsBasedOnMode();
                    MessageBox.Show("حفظ البيانات بنجاح.", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbListlD.Text = _WhiteList.WhiteListID.ToString();
                    btnDelete.Visible = true;
                }
                else
                {
                    MessageBox.Show("فشل: لم تحفظ البيانات بشكل صحيح.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

            else if (_CreationMode == enCreationMode.RegulatoryClosedList || _CreationMode == enCreationMode.ShariaClosedList)
            {
                if (_Mode == enMode.Update)
                {
                    _ClosedList.LastEditByUserID = clsGlobal.CurrentUser.UserID;
                    _ClosedList.LastEditDate = DateTime.Now;
                }

                else
                {
                    _ClosedList.AddedToListDate = DateTime.Now;
                }

                _ClosedList.PractitionerID = _PractitionerID;
                _ClosedList.Notes = tbListNote.Text;
                _ClosedList.CreatedByUserID = (int)clsGlobal.CurrentUser.UserID;
                _ClosedList.ClosedListPractitionerReasonsIDNamesDictionary = _GetCheckedReasons();
                _ClosedList.PractitionerType = _ReturnPractitionerTypeBasedOnCreationMode();//This is the only property would differ between different lists

                if (_ClosedList.Save())
                {
                    _Mode = enMode.Update;
                    handleLabelsBasedOnMode();
                    MessageBox.Show("حفظ البيانات بنجاح.", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbListlD.Text = _ClosedList.ClosedListID.ToString();
                    btnDelete.Visible = true;
                }
                else
                {
                    MessageBox.Show("فشل: لم تحفظ البيانات بشكل صحيح.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
       

        
        }

        private void clbBlackListReasons_Validating(object sender, CancelEventArgs e)
        {
            if (gbList.Enabled && clbListReasons.CheckedItems.Count == 0)
            {

                errorProvider1.SetError(clbListReasons, "يجب اختيار سبب واحد على الاقل.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(clbListReasons, "");

                e.Cancel = false;
            }
        }

        public void PerformClick()
        {
            btnSave_Click(null,null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (enMode.Update == _Mode && enCreationMode.BlackList == _CreationMode)
            {
            DialogResult result = MessageBox.Show("سيتم حذف الشخص من القائمة السوداء هل تريد المتابعة؟", "تاكيد", MessageBoxButtons.YesNo);
      
            if (result == DialogResult.Yes)
            {
                if (clsBlackList.DeleteList(_BlackListID))
                {
                    MessageBox.Show("تم حذف العنصر من القائمة ", "معلومة", MessageBoxButtons.OK,MessageBoxIcon.Information);

                    LoadInfo(_PractitionerID, _CreationMode);
                    
                }
                else
                {
                    MessageBox.Show("حصل خطاء ما اثناء القيام بعملية الحذف.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            }
          
            else if (enMode.Update == _Mode && enCreationMode.RegulatoryWhiteList == _CreationMode)
            {
                DialogResult result = MessageBox.Show("سيتم حذف الشخص من القائمة البيضاء للنظامين هل تريد المتابعة؟", "تاكيد", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    if (clsWhiteList.DeleteList(_WhiteListID))
                    {
                        MessageBox.Show("تم حذف العنصر من القائمة البيضاء للنظامين ", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInfo(_PractitionerID, _CreationMode);

                    }
                    else
                    {
                        MessageBox.Show("حصل خطاء ما اثناء القيام بعملية الحذف.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }

            else if (enMode.Update == _Mode && enCreationMode.RegulatoryClosedList == _CreationMode)
            {
                DialogResult result = MessageBox.Show("سيتم حذف الشخص من القائمة المغلقة للنظامين هل تريد المتابعة؟", "تاكيد", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    if (clsClosedList.DeleteList(_ClosedListID))
                    {
                        MessageBox.Show("تم حذف العنصر من القائمة المغلقة للنظامين ", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInfo(_PractitionerID, _CreationMode);

                    }
                    else
                    {
                        MessageBox.Show("حصل خطاء ما اثناء القيام بعملية الحذف.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }

            else if (enMode.Update == _Mode && enCreationMode.ShariaWhiteList == _CreationMode)
            {
                DialogResult result = MessageBox.Show("سيتم حذف الشخص من القائمة البيضاء للشرعيين هل تريد المتابعة؟", "تاكيد", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    if (clsWhiteList.DeleteList(_WhiteListID))
                    {
                        MessageBox.Show("تم حذف العنصر من القائمة البيضاء للشرعيين ", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInfo(_PractitionerID, _CreationMode);

                    }
                    else
                    {
                        MessageBox.Show("حصل خطاء ما اثناء القيام بعملية الحذف.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }

            else if (enMode.Update == _Mode && enCreationMode.ShariaClosedList == _CreationMode)
            {
                DialogResult result = MessageBox.Show("سيتم حذف الشخص من القائمة المغلقة للشرعيين هل تريد المتابعة؟", "تاكيد", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    if (clsClosedList.DeleteList(_ClosedListID))
                    {
                        MessageBox.Show("تم حذف العنصر من القائمة المغلقة للشرعيين ", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInfo(_PractitionerID, _CreationMode);

                    }
                    else
                    {
                        MessageBox.Show("حصل خطاء ما اثناء القيام بعملية الحذف.", "فشل", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
           
            else
            {
                MessageBox.Show("process went what of what was expected", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WriteEventToLogFile("Delete was clicked when mode status ain't update one, clsAddUpdateList ctrl,BtnDelete()"
                    , System.Diagnostics.EventLogEntryType.Warning);
            }
      
        }

    }

}
