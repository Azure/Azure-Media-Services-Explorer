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
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox1.Controls.Add(this.dateTimePickerEndDate);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // dateTimePickerEndTime
            // 
            resources.ApplyResources(this.dateTimePickerEndTime, "dateTimePickerEndTime");
            this.errorProvider1.SetError(this.dateTimePickerEndTime, resources.GetString("dateTimePickerEndTime.Error"));
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.errorProvider1.SetIconAlignment(this.dateTimePickerEndTime, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dateTimePickerEndTime.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dateTimePickerEndTime, ((int)(resources.GetObject("dateTimePickerEndTime.IconPadding"))));
            this.dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // dateTimePickerEndDate
            // 
            resources.ApplyResources(this.dateTimePickerEndDate, "dateTimePickerEndDate");
            this.errorProvider1.SetError(this.dateTimePickerEndDate, resources.GetString("dateTimePickerEndDate.Error"));
            this.errorProvider1.SetIconAlignment(this.dateTimePickerEndDate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dateTimePickerEndDate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dateTimePickerEndDate, ((int)(resources.GetObject("dateTimePickerEndDate.IconPadding"))));
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
            this.errorProvider1.SetError(this.groupBoxStartDate, resources.GetString("groupBoxStartDate.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBoxStartDate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBoxStartDate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBoxStartDate, ((int)(resources.GetObject("groupBoxStartDate.IconPadding"))));
            this.groupBoxStartDate.Name = "groupBoxStartDate";
            this.groupBoxStartDate.TabStop = false;
            // 
            // checkBoxStartDate
            // 
            resources.ApplyResources(this.checkBoxStartDate, "checkBoxStartDate");
            this.errorProvider1.SetError(this.checkBoxStartDate, resources.GetString("checkBoxStartDate.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxStartDate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxStartDate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxStartDate, ((int)(resources.GetObject("checkBoxStartDate.IconPadding"))));
            this.checkBoxStartDate.Name = "checkBoxStartDate";
            this.checkBoxStartDate.UseVisualStyleBackColor = true;
            this.checkBoxStartDate.CheckedChanged += new System.EventHandler(this.checkBoxStartDate_CheckedChanged_1);
            // 
            // dateTimePickerStartTime
            // 
            resources.ApplyResources(this.dateTimePickerStartTime, "dateTimePickerStartTime");
            this.errorProvider1.SetError(this.dateTimePickerStartTime, resources.GetString("dateTimePickerStartTime.Error"));
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.errorProvider1.SetIconAlignment(this.dateTimePickerStartTime, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dateTimePickerStartTime.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dateTimePickerStartTime, ((int)(resources.GetObject("dateTimePickerStartTime.IconPadding"))));
            this.dateTimePickerStartTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // dateTimePickerStartDate
            // 
            resources.ApplyResources(this.dateTimePickerStartDate, "dateTimePickerStartDate");
            this.errorProvider1.SetError(this.dateTimePickerStartDate, resources.GetString("dateTimePickerStartDate.Error"));
            this.errorProvider1.SetIconAlignment(this.dateTimePickerStartDate, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dateTimePickerStartDate.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dateTimePickerStartDate, ((int)(resources.GetObject("dateTimePickerStartDate.IconPadding"))));
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
            this.errorProvider1.SetError(this.groupBox4, resources.GetString("groupBox4.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox4, ((int)(resources.GetObject("groupBox4.IconPadding"))));
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // checkBoxAddContentKeyIdentifierClaim
            // 
            resources.ApplyResources(this.checkBoxAddContentKeyIdentifierClaim, "checkBoxAddContentKeyIdentifierClaim");
            this.checkBoxAddContentKeyIdentifierClaim.Checked = true;
            this.checkBoxAddContentKeyIdentifierClaim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorProvider1.SetError(this.checkBoxAddContentKeyIdentifierClaim, resources.GetString("checkBoxAddContentKeyIdentifierClaim.Error"));
            this.errorProvider1.SetIconAlignment(this.checkBoxAddContentKeyIdentifierClaim, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("checkBoxAddContentKeyIdentifierClaim.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.checkBoxAddContentKeyIdentifierClaim, ((int)(resources.GetObject("checkBoxAddContentKeyIdentifierClaim.IconPadding"))));
            this.checkBoxAddContentKeyIdentifierClaim.Name = "checkBoxAddContentKeyIdentifierClaim";
            this.checkBoxAddContentKeyIdentifierClaim.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTokenClaims
            // 
            resources.ApplyResources(this.dataGridViewTokenClaims, "dataGridViewTokenClaims");
            this.dataGridViewTokenClaims.AllowUserToAddRows = false;
            this.dataGridViewTokenClaims.AllowUserToDeleteRows = false;
            this.dataGridViewTokenClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTokenClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.errorProvider1.SetError(this.dataGridViewTokenClaims, resources.GetString("dataGridViewTokenClaims.Error"));
            this.errorProvider1.SetIconAlignment(this.dataGridViewTokenClaims, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dataGridViewTokenClaims.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.dataGridViewTokenClaims, ((int)(resources.GetObject("dataGridViewTokenClaims.IconPadding"))));
            this.dataGridViewTokenClaims.Name = "dataGridViewTokenClaims";
            this.dataGridViewTokenClaims.ReadOnly = true;
            this.dataGridViewTokenClaims.RowHeadersVisible = false;
            this.dataGridViewTokenClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider1.SetError(this.label4, resources.GetString("label4.Error"));
            this.errorProvider1.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
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
            this.errorProvider1.SetError(this.listViewAutOptions, resources.GetString("listViewAutOptions.Error"));
            this.listViewAutOptions.FullRowSelect = true;
            this.listViewAutOptions.HideSelection = false;
            this.errorProvider1.SetIconAlignment(this.listViewAutOptions, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("listViewAutOptions.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.listViewAutOptions, ((int)(resources.GetObject("listViewAutOptions.IconPadding"))));
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
            this.errorProvider1.SetError(this.textBoxIssuer, resources.GetString("textBoxIssuer.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxIssuer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxIssuer.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxIssuer, ((int)(resources.GetObject("textBoxIssuer.IconPadding"))));
            this.textBoxIssuer.Name = "textBoxIssuer";
            this.textBoxIssuer.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // textBoxAudience
            // 
            resources.ApplyResources(this.textBoxAudience, "textBoxAudience");
            this.errorProvider1.SetError(this.textBoxAudience, resources.GetString("textBoxAudience.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAudience, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAudience.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAudience, ((int)(resources.GetObject("textBoxAudience.IconPadding"))));
            this.textBoxAudience.Name = "textBoxAudience";
            this.textBoxAudience.ReadOnly = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            // 
            // panelJWTX509Cert
            // 
            resources.ApplyResources(this.panelJWTX509Cert, "panelJWTX509Cert");
            this.panelJWTX509Cert.Controls.Add(this.buttonImportPFX);
            this.panelJWTX509Cert.Controls.Add(this.labelCertificateFile);
            this.errorProvider1.SetError(this.panelJWTX509Cert, resources.GetString("panelJWTX509Cert.Error"));
            this.errorProvider1.SetIconAlignment(this.panelJWTX509Cert, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelJWTX509Cert.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panelJWTX509Cert, ((int)(resources.GetObject("panelJWTX509Cert.IconPadding"))));
            this.panelJWTX509Cert.Name = "panelJWTX509Cert";
            // 
            // buttonImportPFX
            // 
            resources.ApplyResources(this.buttonImportPFX, "buttonImportPFX");
            this.errorProvider1.SetError(this.buttonImportPFX, resources.GetString("buttonImportPFX.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonImportPFX, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonImportPFX.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonImportPFX, ((int)(resources.GetObject("buttonImportPFX.IconPadding"))));
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // labelCertificateFile
            // 
            resources.ApplyResources(this.labelCertificateFile, "labelCertificateFile");
            this.errorProvider1.SetError(this.labelCertificateFile, resources.GetString("labelCertificateFile.Error"));
            this.errorProvider1.SetIconAlignment(this.labelCertificateFile, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelCertificateFile.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelCertificateFile, ((int)(resources.GetObject("labelCertificateFile.IconPadding"))));
            this.labelCertificateFile.Name = "labelCertificateFile";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
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