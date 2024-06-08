using AADL.GlobalClasses;
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
    public partial class frmSearchPersonInfo : Form
    {
        public frmSearchPersonInfo()
        {
            InitializeComponent();
            this.KeyPress += ctrlPersonCardWithFilter1_KeyPress;
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {

                // Prevent the default behavior (playing the key press sound)
                e.Handled = true;
                ctrlPersonCardWithFilter1.PerFormClick();
            }
           
        }

        private void frmSearchPersonInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
