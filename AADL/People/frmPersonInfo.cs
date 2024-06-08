using AADL.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADL
{
    public partial class frmPersonInfo : Form
    {
        private int? _PersonID = null;
        public frmPersonInfo(int? personID)
        {
            _PersonID= personID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonInfo_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.LoadPersonInfo(_PersonID, ctrlPersonCard.LoadPersonBy.PersonID);
        }
    }
}
