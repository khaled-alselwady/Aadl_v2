using AADL.People;
using AADL.Regulators;
using AADL.Users;
using AADLBusiness;
using AADLBusiness.Sharia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace AADL
{
    public partial class frmLaunch : Form
    {
        public frmLaunch()
        {
            InitializeComponent();
        }

        private void frmLaunch_Load(object sender, EventArgs e)
        {

        }

        private void btnLawyerInfo_Click(object sender, EventArgs e)
        {
            // Show input dialog to get integer input
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter an RegulatorID:", "Input", "");

            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int intValue))
            {
                // Valid integer input
                frmRegulatorInfo frmRegulator = new frmRegulatorInfo(intValue);
                frmRegulator.ShowDialog();
            }
            else
            {
                // Display error if input is empty or not a valid integer
                MessageBox.Show("Please enter a valid integer.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePractitioner frmAddUpdateRegulator = new frmAddUpdatePractitioner();
            frmAddUpdateRegulator.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show input dialog to get integer input
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter an PractitionerID:", "Input", "");

            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int intValue))
            {
                // Valid integer input
                frmAddUpdatePractitioner frmAddUpdateprac = new frmAddUpdatePractitioner(intValue);
                frmAddUpdateprac.ShowDialog();
            }
            else
            {
                // Display error if input is empty or not a valid integer
                MessageBox.Show("Please enter a valid integer.");
            }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmListPeople frmListPeople = new frmListPeople();
            frmListPeople.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmListUsers frmListUsers = new frmListUsers();
            frmListUsers.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmPractitionersList   regulatorsList = new frmPractitionersList();
            regulatorsList.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int practitionerID = 12
                , RegulatorID = 62, PersonID = 1082,
                LawyerID = 1038;
            string MemberShip = "84651377";



            if (clsRegulator.IsRegulatorExist(practitionerID, clsRegulator.enSearchBy.PractitionerID)){
                MessageBox.Show("I found it by practitionerID ID");
            }
            else
            {
                MessageBox.Show("I couldn't found it with practitionerID", "FAiled",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            if (clsRegulator.IsRegulatorExist(RegulatorID, clsRegulator.enSearchBy.RegulatorID))
            {
                MessageBox.Show("I found it by regulator ID");
            }
            else
            {
                MessageBox.Show("I couldn't found it with regulatorID", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (clsRegulator.IsRegulatorExist(PersonID, clsRegulator.enSearchBy.PersonID))
            {
                MessageBox.Show("I found it by PersonID ID");
            }
            else
            {
                MessageBox.Show("I couldn't found it with PersonID", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (clsRegulator.IsRegulatorExist(MemberShip, clsRegulator.enSearchBy.MemberShipNumber))
            {
                MessageBox.Show("I found it by MemberShip ID");
            }
            else
            {
                MessageBox.Show("I couldn't found it with MemberShip", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button7_Click(object sender, EventArgs e)

        {
            int practitionerID = 15
                , ShariaID = 61, PersonID = 1079,
                LawyerID = 37;
            string ShariaLicenseNumber = "1414";



            //if (clsSharia.IsShariaExist(practitionerID, clsSharia.enSearchBy.PractitionerID))
            //{
            //    MessageBox.Show("I found it by practitionerID ID");
            //}
            //else
            //{
            //    MessageBox.Show("I couldn't found it with practitionerID", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //if (clsSharia.IsShariaExist(ShariaID, clsSharia.enSearchBy.ShariaID))
            //{
            //    MessageBox.Show("I found it by ShariaID ");
            //}
            //else
            //{
            //    MessageBox.Show("I couldn't found it with ShariaID", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //if (clsSharia.IsShariaExist(PersonID, clsSharia.enSearchBy.PersonID))
            //{
            //    MessageBox.Show("I found it by PersonID ID");
            //}
            //else
            //{
            //    MessageBox.Show("I couldn't found it with PersonID", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //if (clsSharia.IsShariaExist(LawyerID, clsSharia.enSearchBy.LawyerID))
            //{
            //    MessageBox.Show("I found it by LawyerID ID");
            //}
            //else
            //{
            //    MessageBox.Show("I couldn't found it with LawyerID", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


            //if (clsSharia.IsShariaExist(ShariaLicenseNumber, clsSharia.enSearchBy.ShariaLicenseNumber))
            //{
            //    MessageBox.Show("I found it byShariaLicenseNumber");
            //}
            //else
            //{
            //    MessageBox.Show("I couldn't found it with ShariaLicenseNumber", "FAiled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }
    }
}
