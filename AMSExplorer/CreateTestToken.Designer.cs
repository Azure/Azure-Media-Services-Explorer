namespace AMSExplorer
{
    partial class CreateTestToken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTestToken));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBoxStartDate = new System.Windows.Forms.GroupBox();
            this.checkBoxStartDate = new System.Windows.Forms.CheckBox();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxAddContentKeyIdentifierClaim = new System.Windows.Forms.CheckBox();
            this.dataGridViewTokenClaims = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewAutOptions = new System.Windows.Forms.ListView();
            this.columnHeaderContentKeyType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAutPolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKeyType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxIssuer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAudience = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelJWTX509Cert = new System.Windows.Forms.Panel();
            this.buttonImportPFX = new System.Windows.Forms.Button();
            this.labelCertificateFile = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBoxStartDate.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelJWTX509Cert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox1.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // dateTimePickerEndTime
            // 
            resources.ApplyResources(this.dateTimePickerEndTime, "dateTimePickerEndTime");
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // dateTimePickerEndDate
            // 
            resources.ApplyResources(this.dateTimePickerEndDate, "dateTimePickerEndDate");
            this.dateTimePickerEndDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // groupBoxStartDate
            // 
            resources.ApplyResources(this.groupBoxStartDate, "groupBoxStartDate");
            this.groupBoxStartDate.Controls.Add(this.checkBoxStartDate);
            this.groupBoxStartDate.Controls.Add(this.dateTimePickerStartTime);
            this.groupBoxStartDate.Controls.Add(this.dateTimePickerStartDate);
            this.groupBoxStartDate.Name = "groupBoxStartDate";
            this.groupBoxStartDate.TabStop = false;
            // 
            // checkBoxStartDate
            // 
            resources.ApplyResources(this.checkBoxStartDate, "checkBoxStartDate");
            this.checkBoxStartDate.Name = "checkBoxStartDate";
            this.checkBoxStartDate.UseVisualStyleBackColor = true;
            this.checkBoxStartDate.CheckedChanged += new System.EventHandler(this.checkBoxStartDate_CheckedChanged_1);
            // 
            // dateTimePickerStartTime
            // 
            resources.ApplyResources(this.dateTimePickerStartTime, "dateTimePickerStartTime");
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // dateTimePickerStartDate
            // 
            resources.ApplyResources(this.dateTimePickerStartDate, "dateTimePickerStartDate");
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.checkBoxAddContentKeyIdentifierClaim);
            this.groupBox4.Controls.Add(this.dataGridViewTokenClaims);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.listViewAutOptions);
            this.groupBox4.Controls.Add(this.textBoxIssuer);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.textBoxAudience);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // checkBoxAddContentKeyIdentifierClaim
            // 
            resources.ApplyResources(this.checkBoxAddContentKeyIdentifierClaim, "checkBoxAddContentKeyIdentifierClaim");
            this.checkBoxAddContentKeyIdentifierClaim.Checked = true;
            this.checkBoxAddContentKeyIdentifierClaim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddContentKeyIdentifierClaim.Name = "checkBoxAddContentKeyIdentifierClaim";
            this.checkBoxAddContentKeyIdentifierClaim.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTokenClaims
            // 
            this.dataGridViewTokenClaims.AllowUserToAddRows = false;
            this.dataGridViewTokenClaims.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewTokenClaims, "dataGridViewTokenClaims");
            this.dataGridViewTokenClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTokenClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTokenClaims.Name = "dataGridViewTokenClaims";
            this.dataGridViewTokenClaims.ReadOnly = true;
            this.dataGridViewTokenClaims.RowHeadersVisible = false;
            this.dataGridViewTokenClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // listViewAutOptions
            // 
            resources.ApplyResources(this.listViewAutOptions, "listViewAutOptions");
            this.listViewAutOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderContentKeyType,
            this.columnHeaderAutPolName,
            this.columnHeaderId,
            this.columnHeaderType,
            this.columnHeaderKeyType});
            this.listViewAutOptions.FullRowSelect = true;
            this.listViewAutOptions.HideSelection = false;
            this.listViewAutOptions.MultiSelect = false;
            this.listViewAutOptions.Name = "listViewAutOptions";
            this.listViewAutOptions.UseCompatibleStateImageBehavior = false;
            this.listViewAutOptions.View = System.Windows.Forms.View.Details;
            this.listViewAutOptions.SelectedIndexChanged += new System.EventHandler(this.listViewAutOptions_SelectedIndexChanged);
            // 
            // columnHeaderContentKeyType
            // 
            resources.ApplyResources(this.columnHeaderContentKeyType, "columnHeaderContentKeyType");
            // 
            // columnHeaderAutPolName
            // 
            resources.ApplyResources(this.columnHeaderAutPolName, "columnHeaderAutPolName");
            // 
            // columnHeaderId
            // 
            resources.ApplyResources(this.columnHeaderId, "columnHeaderId");
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(this.columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderKeyType
            // 
            resources.ApplyResources(this.columnHeaderKeyType, "columnHeaderKeyType");
            // 
            // textBoxIssuer
            // 
            resources.ApplyResources(this.textBoxIssuer, "textBoxIssuer");
            this.textBoxIssuer.Name = "textBoxIssuer";
            this.textBoxIssuer.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxAudience
            // 
            resources.ApplyResources(this.textBoxAudience, "textBoxAudience");
            this.textBoxAudience.Name = "textBoxAudience";
            this.textBoxAudience.ReadOnly = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // panelJWTX509Cert
            // 
            resources.ApplyResources(this.panelJWTX509Cert, "panelJWTX509Cert");
            this.panelJWTX509Cert.Controls.Add(this.buttonImportPFX);
            this.panelJWTX509Cert.Controls.Add(this.labelCertificateFile);
            this.panelJWTX509Cert.Name = "panelJWTX509Cert";
            // 
            // buttonImportPFX
            // 
            resources.ApplyResources(this.buttonImportPFX, "buttonImportPFX");
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // labelCertificateFile
            // 
            resources.ApplyResources(this.labelCertificateFile, "labelCertificateFile");
            this.labelCertificateFile.Name = "labelCertificateFile";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateTestToken
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panelJWTX509Cert);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxStartDate);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateTestToken";
            this.Load += new System.EventHandler(this.CreateTestToken_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxStartDate.ResumeLayout(false);
            this.groupBoxStartDate.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokenClaims)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelJWTX509Cert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.GroupBox groupBoxStartDate;
        private System.Windows.Forms.CheckBox checkBoxStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelJWTX509Cert;
        private System.Windows.Forms.Button buttonImportPFX;
        private System.Windows.Forms.Label labelCertificateFile;
        private System.Windows.Forms.DataGridView dataGridViewTokenClaims;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIssuer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAudience;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewAutOptions;
        private System.Windows.Forms.ColumnHeader columnHeaderAutPolName;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.CheckBox checkBoxAddContentKeyIdentifierClaim;
        private System.Windows.Forms.ColumnHeader columnHeaderKeyType;
        private System.Windows.Forms.ColumnHeader columnHeaderContentKeyType;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}