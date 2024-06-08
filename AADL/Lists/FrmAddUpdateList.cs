using AADL.People;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADL.Lists
{
    public partial class FrmAddUpdateList : Form
    {
        private int _PractitionerID = -1;
        private int _ListID = -1;
        public enum enMode { AddNew,Update};

        private enMode _Mode=enMode.AddNew;
        private ctrlAddUpdateList.enCreationMode _CreationMode;

        public FrmAddUpdateList(int PractitionerID,ctrlAddUpdateList.enCreationMode CreationMode)
        {
            InitializeComponent();
            _PractitionerID = PractitionerID;
            _Mode = enMode.AddNew;
            _CreationMode = CreationMode;
        }
        public FrmAddUpdateList(int PractitionerID, int ListID, ctrlAddUpdateList.enCreationMode CreationMode)
        {
            InitializeComponent();
            _PractitionerID = PractitionerID;
            _ListID = ListID;
            _Mode = enMode.Update;
            _CreationMode = CreationMode;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddUpdateList_Load(object sender, EventArgs e)
        {
            if(ctrlAddUpdateList1.LoadInfo(_PractitionerID, _CreationMode, _ListID) == false)
            {
                this.Close();

            }

        }

        private void FrmAddUpdateList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                // Prevent the default behavior (playing the key press sound)
                e.Handled = true;
                ctrlAddUpdateList1.PerformClick();
            }
        }
   
    }

}
