using AADLBusiness;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace AADL.Lists
{
    public partial class ctrlListInfo : UserControl
    {
        private int _BlackListID = -1;

        private clsBlackList _BlackList;
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

                        lbBlackListlD.Text = _BlackList.BlackListID.ToString();

                        //reasons

                        foreach(string ReasonName in _BlackList.BlackListPractitionerReasonsIDNamesDictionary.Values)
                        {
                            lvReasons.Items.Add(ReasonName);
                        }

                        tbBlackListNote.Text=_BlackList.Notes.ToString();
                        lbIssueDate.Text = _BlackList.AddedToListDate.ToShortDateString();

                        lbCreatedByUser.Text = clsUser.FindByUserID(_BlackList.CreatedByUserID).UserName.ToString();

                        lbLastEditBy.Text=_BlackList.LastEditByUserID==null?"[???]":
                           clsUser.FindByUserID( _BlackList.LastEditByUserID).UserName.ToString();

                        lbLastEditDate.Text = _BlackList.LastEditDate == null ? "[???]" :
                             _BlackList.LastEditDate.Value.ToShortDateString();

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
                        if(int.TryParse(Value.ToString(),out _BlackListID))
                        {
                            
                            _BlackList=clsBlackList.Find(_BlackListID, 
                                clsBlackList.enFindBy.BlackListID);
                            if(_BlackList!= null)
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
