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
    public partial class frmListInfo : Form
    {
        private int _ListID = -1;
        private ctrlListInfo.CreationMode _CreationMode=ctrlListInfo.CreationMode.BlackList;
        public frmListInfo(int ListID ,ctrlListInfo.CreationMode CreationMode)
        {
            this._ListID = ListID;
            this._CreationMode = CreationMode;
            InitializeComponent();
        }

        private void frmListInfo_Load(object sender, EventArgs e)
        {
            ctrlListInfo1.LoadInfo(_ListID, _CreationMode) ;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
