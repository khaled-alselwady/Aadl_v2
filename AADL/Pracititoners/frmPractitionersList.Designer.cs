namespace AADL.Regulators
{
    partial class frmPractitionersList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbIsActiveSubscription = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPractitioners = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.تفاصيلToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.أضافةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.تعديلToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.حذفToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.شرعيToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.خبيرToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.محكمToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.القائمةالسوداءToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ctbFilterValue = new myControlLibrary.myCustomControlTextBox();
            this.gpAdvnacedSearch = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPractitioners)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.gpAdvnacedSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // cbIsActiveSubscription
            // 
            this.cbIsActiveSubscription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsActiveSubscription.FormattingEnabled = true;
            this.cbIsActiveSubscription.Location = new System.Drawing.Point(389, 258);
            this.cbIsActiveSubscription.Name = "cbIsActiveSubscription";
            this.cbIsActiveSubscription.Size = new System.Drawing.Size(121, 21);
            this.cbIsActiveSubscription.TabIndex = 121;
            this.cbIsActiveSubscription.Visible = false;
            this.cbIsActiveSubscription.SelectedIndexChanged += new System.EventHandler(this.cbIsActive_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(0, 671);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(1520, 36);
            this.btnClose.TabIndex = 120;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRecordsCount.Location = new System.Drawing.Point(0, 707);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(19, 13);
            this.lblRecordsCount.TabIndex = 119;
            this.lblRecordsCount.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 720);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 118;
            this.label2.Text = "سجلات:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "لا شئ",
            "الرقم التعريفي",
            "الاسم الكامل",
            "رقم الهاتف",
            "البريد الالكتروني",
            "رقم العضوية",
            "نوع الاشتراك",
            "تم الانشاء من قبل",
            "هل فعال"});
            this.cbFilterBy.Location = new System.Drawing.Point(151, 259);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(210, 21);
            this.cbFilterBy.TabIndex = 117;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 115;
            this.label1.Text = "البحث بواسطة:";
            // 
            // dgvPractitioners
            // 
            this.dgvPractitioners.AllowUserToAddRows = false;
            this.dgvPractitioners.AllowUserToDeleteRows = false;
            this.dgvPractitioners.AllowUserToResizeRows = false;
            this.dgvPractitioners.BackgroundColor = System.Drawing.Color.White;
            this.dgvPractitioners.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPractitioners.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPractitioners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPractitioners.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPractitioners.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPractitioners.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPractitioners.Location = new System.Drawing.Point(49, 288);
            this.dgvPractitioners.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvPractitioners.MultiSelect = false;
            this.dgvPractitioners.Name = "dgvPractitioners";
            this.dgvPractitioners.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPractitioners.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPractitioners.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPractitioners.Size = new System.Drawing.Size(1800, 700);
            this.dgvPractitioners.TabIndex = 114;
            this.dgvPractitioners.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(45, 45);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.تفاصيلToolStripMenuItem,
            this.أضافةToolStripMenuItem,
            this.تعديلToolStripMenuItem,
            this.حذفToolStripMenuItem,
            this.شرعيToolStripMenuItem,
            this.خبيرToolStripMenuItem,
            this.محكمToolStripMenuItem,
            this.القائمةالسوداءToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 420);
            // 
            // تفاصيلToolStripMenuItem
            // 
            this.تفاصيلToolStripMenuItem.Image = global::AADL.Properties.Resources.lawyer_search;
            this.تفاصيلToolStripMenuItem.Name = "تفاصيلToolStripMenuItem";
            this.تفاصيلToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.تفاصيلToolStripMenuItem.Text = "تفاصيل";
            // 
            // أضافةToolStripMenuItem
            // 
            this.أضافةToolStripMenuItem.Image = global::AADL.Properties.Resources.lawyer_add__2_;
            this.أضافةToolStripMenuItem.Name = "أضافةToolStripMenuItem";
            this.أضافةToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.أضافةToolStripMenuItem.Text = "أضافة";
            // 
            // تعديلToolStripMenuItem
            // 
            this.تعديلToolStripMenuItem.Image = global::AADL.Properties.Resources.lawyer_config;
            this.تعديلToolStripMenuItem.Name = "تعديلToolStripMenuItem";
            this.تعديلToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.تعديلToolStripMenuItem.Text = "تعديل";
            // 
            // حذفToolStripMenuItem
            // 
            this.حذفToolStripMenuItem.Image = global::AADL.Properties.Resources.lawyer_delete;
            this.حذفToolStripMenuItem.Name = "حذفToolStripMenuItem";
            this.حذفToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.حذفToolStripMenuItem.Text = "حذف";
            // 
            // شرعيToolStripMenuItem
            // 
            this.شرعيToolStripMenuItem.Image = global::AADL.Properties.Resources.balance;
            this.شرعيToolStripMenuItem.Name = "شرعيToolStripMenuItem";
            this.شرعيToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.شرعيToolStripMenuItem.Text = "شرعي";
            // 
            // خبيرToolStripMenuItem
            // 
            this.خبيرToolStripMenuItem.Image = global::AADL.Properties.Resources.client;
            this.خبيرToolStripMenuItem.Name = "خبيرToolStripMenuItem";
            this.خبيرToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.خبيرToolStripMenuItem.Text = "خبير";
            // 
            // محكمToolStripMenuItem
            // 
            this.محكمToolStripMenuItem.Image = global::AADL.Properties.Resources.lawyer__2_;
            this.محكمToolStripMenuItem.Name = "محكمToolStripMenuItem";
            this.محكمToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.محكمToolStripMenuItem.Text = "محكم";
            // 
            // القائمةالسوداءToolStripMenuItem
            // 
            this.القائمةالسوداءToolStripMenuItem.Image = global::AADL.Properties.Resources.lawyer_female;
            this.القائمةالسوداءToolStripMenuItem.Name = "القائمةالسوداءToolStripMenuItem";
            this.القائمةالسوداءToolStripMenuItem.Size = new System.Drawing.Size(176, 52);
            this.القائمةالسوداءToolStripMenuItem.Text = "القائمة السوداء";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 189);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1520, 39);
            this.lblTitle.TabIndex = 122;
            this.lblTitle.Text = "ادارة المحامين";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // ctbFilterValue
            // 
            this.ctbFilterValue.InputType = myControlLibrary.myCustomControlTextBox.InputTypeEnum.TextInput;
            this.ctbFilterValue.IsRequired = false;
            this.ctbFilterValue.Location = new System.Drawing.Point(389, 259);
            this.ctbFilterValue.Name = "ctbFilterValue";
            this.ctbFilterValue.Size = new System.Drawing.Size(256, 20);
            this.ctbFilterValue.TabIndex = 123;
            this.ctbFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.ctbFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // gpAdvnacedSearch
            // 
            this.gpAdvnacedSearch.Controls.Add(this.button1);
            this.gpAdvnacedSearch.Location = new System.Drawing.Point(1234, 49);
            this.gpAdvnacedSearch.Name = "gpAdvnacedSearch";
            this.gpAdvnacedSearch.Size = new System.Drawing.Size(184, 100);
            this.gpAdvnacedSearch.TabIndex = 125;
            this.gpAdvnacedSearch.TabStop = false;
            this.gpAdvnacedSearch.Text = "البحث المتقدم";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::AADL.Properties.Resources.SearchPerson;
            this.button1.Location = new System.Drawing.Point(19, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 58);
            this.button1.TabIndex = 124;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::AADL.Properties.Resources.refresh__1_;
            this.btnRefresh.Location = new System.Drawing.Point(1424, 91);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(84, 58);
            this.btnRefresh.TabIndex = 126;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddUser.Image = global::AADL.Properties.Resources.lawyer_add__1_;
            this.btnAddUser.Location = new System.Drawing.Point(1750, 215);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(88, 65);
            this.btnAddUser.TabIndex = 112;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbPersonImage.Image = global::AADL.Properties.Resources.Local_Driving_License_512;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(0, 0);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(1520, 189);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 101;
            this.pbPersonImage.TabStop = false;
            // 
            // frmPractitionersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1520, 740);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.gpAdvnacedSearch);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.ctbFilterValue);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbIsActiveSubscription);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPractitioners);
            this.Controls.Add(this.pbPersonImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPractitionersList";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.Text = "نافذة المحامين النظامين";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPractitionersList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPractitioners)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.gpAdvnacedSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ComboBox cbIsActiveSubscription;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPractitioners;
        private System.Windows.Forms.Label lblTitle;
        private myControlLibrary.myCustomControlTextBox ctbFilterValue;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem تفاصيلToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem أضافةToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem تعديلToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem حذفToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem شرعيToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem خبيرToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem محكمToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem القائمةالسوداءToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gpAdvnacedSearch;
        private System.Windows.Forms.Button btnRefresh;
    }
}