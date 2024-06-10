using AADLBusiness;
using AADLBusiness;
using AADLBusiness.Lists.Closed;
using AADLBusiness.Lists.WhiteList;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace AADL.Lists
{
    public partial class ctrlListInfo : UserControl
    {
        private int? _BlackListID =null;
        private int? _WhiteListID =null;
        private int? _ClosedListID =null;


        private clsBlackList  _BlackList;
        private clsWhiteList  _WhiteList;
        private clsClosedList _ClosedList;

        public enum CreationMode {BlackList,WhiteList,ClosedList};

        private CreationMode _CreationMode;
        public ctrlListInfo()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            switch (_CreationMode)
            {
                case CreationMode.BlackList:
                    {
                        groupBox.Text = "القائمة السوداء";
                        this.Text = "نافذة القائمة السوداء";

                        lbListlD.Text = _BlackList.BlackListID.ToString();

                        //reasons

                        foreach(string ReasonName in _BlackList.BlackListPractitionerReasonsIDNamesDictionary.Values)
                        {
                            lvReasons.Items.Add(ReasonName);
                        }

                        tbListNote.Text=_BlackList.Notes.ToString();
                        lbIssueDate.Text = _BlackList.AddedToListDate.ToShortDateString();

                        lbCreatedByUser.Text = clsUser.FindByUserID(_BlackList.CreatedByUserID).UserName.ToString();

                        lbLastEditBy.Text=_BlackList.LastEditByUserID==null?"[???]":
                           clsUser.FindByUserID( _BlackList.LastEditByUserID).UserName.ToString();

                        lbLastEditDate.Text = _BlackList.LastEditDate == null ? "[???]" :
                             _BlackList.LastEditDate.Value.ToShortDateString();

                        break;
                    }
                
                case CreationMode.WhiteList:
                    {
                        groupBox.Text = "القائمة البيضاء";
                        this.Text = "نافذة القائمة البيضاء";

                        lbListlD.Text = _WhiteList.WhiteListID.ToString();

                        //reasons

                        foreach (string ReasonName in _WhiteList.WhiteListPractitionerReasonsIDNamesDictionary.Values)
                        {
                            lvReasons.Items.Add(ReasonName);
                        }

                        tbListNote.Text = _WhiteList.Notes.ToString();
                        lbIssueDate.Text = _WhiteList.AddedToListDate.ToShortDateString();

                        lbCreatedByUser.Text = clsUser.FindByUserID(_WhiteList.CreatedByUserID).UserName.ToString();

                        lbLastEditBy.Text = _WhiteList.LastEditByUserID == null ? "[???]" :
                           clsUser.FindByUserID(_WhiteList.LastEditByUserID).UserName.ToString();

                        lbLastEditDate.Text = _WhiteList.LastEditDate == null ? "[???]" :
                             _WhiteList.LastEditDate.Value.ToShortDateString();

                        break;
                    }

                case CreationMode.ClosedList:
                    {
                        groupBox.Text = "القائمة المغلقة";
                        this.Text = "نافذة القائمة المغلقة";
                        
                        lbListlD.Text = _ClosedList.ClosedListID.ToString();

                        //reasons

                        foreach (string ReasonName in _ClosedList.ClosedListPractitionerReasonsIDNamesDictionary.Values)
                        {
                            lvReasons.Items.Add(ReasonName);
                        }

                        tbListNote.Text = _ClosedList.Notes.ToString();
                        lbIssueDate.Text = _ClosedList.AddedToListDate.ToShortDateString();

                        lbCreatedByUser.Text = clsUser.FindByUserID(_ClosedList.CreatedByUserID).UserName.ToString();

                        lbLastEditBy.Text = _ClosedList.LastEditByUserID == null ? "[???]" :
                           clsUser.FindByUserID(_ClosedList.LastEditByUserID).UserName.ToString();

                        lbLastEditDate.Text = _ClosedList.LastEditDate == null ? "[???]" :
                             _ClosedList.LastEditDate.Value.ToShortDateString();

                        break;
                    }

            }
        }

       public void LoadInfo(int Value,CreationMode creationMode)
       {
                _CreationMode = creationMode;

            switch(creationMode)
            {
                case CreationMode.BlackList:
                    {
                        if(int.TryParse(Value.ToString(),out int ID))
                        {
                            _BlackListID=ID;
                            
                            _BlackList=clsBlackList.Find(_BlackListID, 
                                clsBlackList.enFindBy.BlackListID);
                            if(_BlackList!= null)
                            {
                                _LoadData();
                            }
                        }
                            break;
                    }

                case CreationMode.WhiteList:
                    {
                        if (int.TryParse(Value.ToString(), out int ID))
                        {
                            _WhiteListID = ID;

                            _WhiteList = clsWhiteList.Find((int)_WhiteListID);
                            if (_WhiteList != null)
                            {
                                _LoadData();
                            }
                        }
                        break;
                    }

                case CreationMode.ClosedList:
                    {
                        if (int.TryParse(Value.ToString(), out int ID))
                        {
                            _ClosedListID = ID;

                            _ClosedList= clsClosedList.Find((int)_ClosedListID);
                            if (_ClosedList != null)
                            {
                                _LoadData();
                            }
                        }
                        break;
                    }
            }

       }
    }
}
