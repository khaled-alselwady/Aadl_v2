namespace AADL.People.controls
{
    partial class ctrlPersonCardWithFilter
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gpFilters = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.ctbFilterValue = new myControlLibrary.myCustomControlTextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlPersonCard1 = new AADL.People.ctrlPersonCard();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpFilters
            // 
            this.gpFilters.Controls.Add(this.btnAddNewPerson);
            this.gpFilters.Controls.Add(this.ctbFilterValue);
            this.gpFilters.Controls.Add(this.btnFind);
            this.gpFilters.Controls.Add(this.cbFilterBy);
            this.gpFilters.Controls.Add(this.label1);
            this.gpFilters.Location = new System.Drawing.Point(355, 25);
            this.gpFilters.Name = "gpFilters";
            this.gpFilters.Size = new System.Drawing.Size(696, 69);
            this.gpFilters.TabIndex = 0;
            this.gpFilters.TabStop = false;
            this.gpFilters.Text = "فلتر";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddNewPerson.Image = global::AADL.Properties.Resources.SearchPerson;
            this.btnAddNewPerson.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewPerson.Location = new System.Drawing.Point(64, 20);
            this.btnAddNewPerson.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(44, 37);
            this.btnAddNewPerson.TabIndex = 22;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnFind_Click_1);
            // 
            // ctbFilterValue
            // 
            this.ctbFilterValue.InputType = myControlLibrary.myCustomControlTextBox.InputTypeEnum.TextInput;
            this.ctbFilterValue.IsRequired = true;
            this.ctbFilterValue.Location = new System.Drawing.Point(115, 29);
            this.ctbFilterValue.Name = "ctbFilterValue";
            this.ctbFilterValue.Size = new System.Drawing.Size(248, 20);
            this.ctbFilterValue.TabIndex = 21;
            this.ctbFilterValue.Validating += new System.ComponentModel.CancelEventHandler(this.ctbFilterValue_Validating);
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Image = global::AADL.Properties.Resources.AddPerson_32;
            this.btnFind.Location = new System.Drawing.Point(13, 20);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(44, 37);
            this.btnFind.TabIndex = 21;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnAddNewPerson_Click_1);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "الرقم التعريفي",
            "الاسم الكامل",
            "الرقم الوطني",
            "رقم جواز السفر",
            "رقم الهاتف",
            "رقم الواتس اب",
            "البريد الالكتروني"});
            this.cbFilterBy.Location = new System.Drawing.Point(369, 29);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(210, 21);
            this.cbFilterBy.TabIndex = 17;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(590, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "البحث بواسطة:";
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.Location = new System.Drawing.Point(14, 100);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(1059, 623);
            this.ctrlPersonCard1.TabIndex = 1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlPersonCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.ctrlPersonCard1);
            this.Controls.Add(this.gpFilters);
            this.Name = "ctrlPersonCardWithFilter";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1063, 725);
            this.Load += new System.EventHandler(this.ctrlPersonCardWithFilter_Load);
            this.gpFilters.ResumeLayout(false);
            this.gpFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Button btnAddNewPerson;
        private myControlLibrary.myCustomControlTextBox ctbFilterValue;
        private System.Windows.Forms.Button btnFind;
        private ctrlPersonCard ctrlPersonCard1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
