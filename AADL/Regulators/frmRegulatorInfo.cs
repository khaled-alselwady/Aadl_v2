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

namespace AADL.Regulators
{
    public partial class frmRegulatorInfo : Form
    {
        private int _RegulatorID = -1;

        public frmRegulatorInfo(int RegulatorID)
        {
            _RegulatorID = RegulatorID;
            InitializeComponent();
        }
   

        private void frmRegulatorInfo_Load(object sender, EventArgs e)
        {
            try
            {

                if (_RegulatorID != -1)
                {
                    ctrlRegulatorCard1.LoadRegulatorInfo(_RegulatorID, ctrlRegulatorCard.LoadRegulatorBy.RegulatorID);
                }
                else
                {
                    MessageBox.Show("الرقم التعريفي للمحامي النظامي ,غير مدخل بشكل صحيح", "فشل",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (
            Exception ex)
            {
                clsHelperClasses.WriteEventToLogFile("Regulator info form ,\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    
    }

}
