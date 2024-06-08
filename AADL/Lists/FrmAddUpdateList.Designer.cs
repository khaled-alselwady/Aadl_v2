namespace AADL.Lists
{
    partial class FrmAddUpdateList
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlAddUpdateList1 = new AADL.Lists.ctrlAddUpdateList();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::AADL.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(32, 365);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 34);
            this.btnClose.TabIndex = 122;
            this.btnClose.Text = "اغلاق";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlAddUpdateList1
            // 
            this.ctrlAddUpdateList1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlAddUpdateList1.Location = new System.Drawing.Point(32, 12);
            this.ctrlAddUpdateList1.Name = "ctrlAddUpdateList1";
            this.ctrlAddUpdateList1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctrlAddUpdateList1.Size = new System.Drawing.Size(366, 347);
            this.ctrlAddUpdateList1.TabIndex = 0;
            // 
            // FrmAddUpdateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 411);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlAddUpdateList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAddUpdateList";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "اضافة الى القائمة";
            this.Load += new System.EventHandler(this.FrmAddUpdateList_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmAddUpdateList_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlAddUpdateList ctrlAddUpdateList1;
        private System.Windows.Forms.Button btnClose;
    }
}