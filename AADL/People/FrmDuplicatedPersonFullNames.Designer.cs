namespace AADL
{
    partial class FrmDuplicatedPersonFullNames
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
            this.components = new System.ComponentModel.Container();
            this.dgvDuplicatedPeople = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicatedPeople)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDuplicatedPeople
            // 
            this.dgvDuplicatedPeople.AllowUserToAddRows = false;
            this.dgvDuplicatedPeople.AllowUserToDeleteRows = false;
            this.dgvDuplicatedPeople.BackgroundColor = System.Drawing.Color.Azure;
            this.dgvDuplicatedPeople.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDuplicatedPeople.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDuplicatedPeople.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDuplicatedPeople.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDuplicatedPeople.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgvDuplicatedPeople.Location = new System.Drawing.Point(-2, 51);
            this.dgvDuplicatedPeople.Name = "dgvDuplicatedPeople";
            this.dgvDuplicatedPeople.ReadOnly = true;
            this.dgvDuplicatedPeople.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDuplicatedPeople.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDuplicatedPeople.Size = new System.Drawing.Size(1230, 121);
            this.dgvDuplicatedPeople.TabIndex = 26;
            this.dgvDuplicatedPeople.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDuplicatedPeople_CellDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.selectPersonToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 92);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Image = global::AADL.Properties.Resources.PersonDetails_32;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.showDetailsToolStripMenuItem.Text = "اظهار التفاصيل";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::AADL.Properties.Resources.edit_32;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.editToolStripMenuItem.Text = "تعديل";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::AADL.Properties.Resources.Delete_32;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deleteToolStripMenuItem.Text = "حذف";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // selectPersonToolStripMenuItem
            // 
            this.selectPersonToolStripMenuItem.Image = global::AADL.Properties.Resources.Person_32;
            this.selectPersonToolStripMenuItem.Name = "selectPersonToolStripMenuItem";
            this.selectPersonToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.selectPersonToolStripMenuItem.Text = "أختيار الشخص";
            this.selectPersonToolStripMenuItem.Click += new System.EventHandler(this.selectPersonToolStripMenuItem_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(-2, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1230, 39);
            this.lblTitle.TabIndex = 76;
            this.lblTitle.Text = "الاسماء المكررة";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmDuplicatedPersonFullNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 174);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvDuplicatedPeople);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmDuplicatedPersonFullNames";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "الاسماء المكررة";
            this.Load += new System.EventHandler(this.FrmDuplicatedPersonFullNames_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuplicatedPeople)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDuplicatedPeople;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectPersonToolStripMenuItem;
        private System.Windows.Forms.Label lblTitle;
    }
}