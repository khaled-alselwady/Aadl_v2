using AADL.GlobalClasses;
using AADLBusiness;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MethodTimer;
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
    public partial class FrmDuplicatedPersonFullNames : Form
    {
        private bool _IsPeopleLoaded = false;

        private string _DuplicatedFullName = "";

        public static EventHandler<PersonCompleteEventArgs> OnPersonSelected;


        public void RaiseOnPersonSelected(int? PersonID)
        {

            RaiseOnPersonSelected(new PersonCompleteEventArgs(PersonID,true));
        }

        protected virtual void RaiseOnPersonSelected(PersonCompleteEventArgs e)
        {
            if (OnPersonSelected != null)
            {
                OnPersonSelected(this, e);
                this.Close();
            }
        }
        [Time]
        public FrmDuplicatedPersonFullNames(string DuplicatedFullName)
        {
            _DuplicatedFullName= DuplicatedFullName;
            InitializeComponent();

        }

        private void dgvDuplicatedPeople_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (int.TryParse(dgvDuplicatedPeople.CurrentRow.Cells[0].Value.ToString(), out int PersonID))
                {
                    RaiseOnPersonSelected(PersonID);
                }
            }
            catch (Exception ex )
            {
                clsGlobal.WriteEventToLogFile("Selected personID on duplicated data grid view of full names," +
                    "didn't worked well , (TryParse()), people file , FrmDuplicatedPersonFullNames.\nException:"+ex.Message, System.Diagnostics.EventLogEntryType.Warning);
            }
           
        }

        [Time]
        private async void LoadData()
        {
            
            try
            {
                DataTable dtPeople = await Task.Run(() => clsPerson.GetAllDuplicatedFullNamePeople(_DuplicatedFullName));

                if (dtPeople.Rows.Count > 0)
                {


                    dgvDuplicatedPeople.DataSource = dtPeople;

                    dgvDuplicatedPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.Message, "FAiled");
            }

            _IsPeopleLoaded = true;

        }

     
        private  void FrmDuplicatedPersonFullNames_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (int.TryParse(dgvDuplicatedPeople.CurrentRow.Cells[0].Value.ToString(), out int PersonID))
            {

               frmPersonInfo form =new frmPersonInfo(PersonID);
                form.ShowDialog();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل انت متاكد من الحذف, لا يمكن استرجاع البيانات مرة اخرى في حال تم الحذف.", "سؤال"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {

            if (int.TryParse(dgvDuplicatedPeople.CurrentRow.Cells[0].Value.ToString(), out int PersonID))
            {
                try
                {

                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("لقد تم حذف الشخص بنجاح من النظام", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("لقد تم حدث خطاء لم تيم حذف البيانات.", "خطاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clsGlobal.WriteEventToLogFile("Exception:\n " +ex.Message+ "\nProblem in deleting a person info , in duplicated data grid view form .", System.Diagnostics.EventLogEntryType.Error);
                }

            }
            }

        }

        private void selectPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvDuplicatedPeople_CellDoubleClick(null, null);
        }
    }

}
