﻿namespace AMSExplorer
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
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(380, 14);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(156, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Create Test Token";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(558, 14);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox1.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox1.Location = new System.Drawing.Point(350, 493);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 150);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "End date/time";
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(10, 99);
            this.dateTimePickerEndTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(294, 23);
            this.dateTimePickerEndTime.TabIndex = 2;
            this.dateTimePickerEndTime.ValueChanged += new System.EventHandler(this.dateTimePickerEndTime_ValueChanged);
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.CustomFormat = "";
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(10, 69);
            this.dateTimePickerEndDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(294, 23);
            this.dateTimePickerEndDate.TabIndex = 1;
            this.dateTimePickerEndDate.ValueChanged += new System.EventHandler(this.dateTimePickerEndDate_ValueChanged);
            // 
            // groupBoxStartDate
            // 
            this.groupBoxStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxStartDate.Controls.Add(this.checkBoxStartDate);
            this.groupBoxStartDate.Controls.Add(this.dateTimePickerStartTime);
            this.groupBoxStartDate.Controls.Add(this.dateTimePickerStartDate);
            this.groupBoxStartDate.Location = new System.Drawing.Point(19, 492);
            this.groupBoxStartDate.Name = "groupBoxStartDate";
            this.groupBoxStartDate.Size = new System.Drawing.Size(313, 150);
            this.groupBoxStartDate.TabIndex = 1;
            this.groupBoxStartDate.TabStop = false;
            this.groupBoxStartDate.Text = "Start date/time";
            // 
            // checkBoxStartDate
            // 
            this.checkBoxStartDate.AutoSize = true;
            this.checkBoxStartDate.Location = new System.Drawing.Point(12, 30);
            this.checkBoxStartDate.Name = "checkBoxStartDate";
            this.checkBoxStartDate.Size = new System.Drawing.Size(157, 19);
            this.checkBoxStartDate.TabIndex = 0;
            this.checkBoxStartDate.Text = "Specify a start date/time:";
            this.checkBoxStartDate.UseVisualStyleBackColor = true;
            this.checkBoxStartDate.CheckedChanged += new System.EventHandler(this.checkBoxStartDate_CheckedChanged_1);
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "";
            this.dateTimePickerStartTime.Enabled = false;
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(13, 99);
            this.dateTimePickerStartTime.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(292, 23);
            this.dateTimePickerStartTime.TabIndex = 2;
            this.dateTimePickerStartTime.ValueChanged += new System.EventHandler(this.dateTimePickerStartTime_ValueChanged);
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Enabled = false;
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(13, 69);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(292, 23);
            this.dateTimePickerStartDate.TabIndex = 1;
            this.dateTimePickerStartDate.ValueChanged += new System.EventHandler(this.dateTimePickerStartDate_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.checkBoxAddContentKeyIdentifierClaim);
            this.groupBox4.Controls.Add(this.dataGridViewTokenClaims);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.listViewAutOptions);
            this.groupBox4.Controls.Add(this.textBoxIssuer);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.textBoxAudience);
            this.groupBox4.Location = new System.Drawing.Point(19, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(644, 458);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select an authorization option";
            // 
            // checkBoxAddContentKeyIdentifierClaim
            // 
            this.checkBoxAddContentKeyIdentifierClaim.AutoSize = true;
            this.checkBoxAddContentKeyIdentifierClaim.Checked = true;
            this.checkBoxAddContentKeyIdentifierClaim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddContentKeyIdentifierClaim.Enabled = false;
            this.checkBoxAddContentKeyIdentifierClaim.Location = new System.Drawing.Point(8, 324);
            this.checkBoxAddContentKeyIdentifierClaim.Name = "checkBoxAddContentKeyIdentifierClaim";
            this.checkBoxAddContentKeyIdentifierClaim.Size = new System.Drawing.Size(200, 19);
            this.checkBoxAddContentKeyIdentifierClaim.TabIndex = 71;
            this.checkBoxAddContentKeyIdentifierClaim.Text = "Add Content Key Identifier Claim";
            this.checkBoxAddContentKeyIdentifierClaim.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTokenClaims
            // 
            this.dataGridViewTokenClaims.AllowUserToAddRows = false;
            this.dataGridViewTokenClaims.AllowUserToDeleteRows = false;
            this.dataGridViewTokenClaims.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTokenClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTokenClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTokenClaims.Location = new System.Drawing.Point(7, 366);
            this.dataGridViewTokenClaims.Name = "dataGridViewTokenClaims";
            this.dataGridViewTokenClaims.ReadOnly = true;
            this.dataGridViewTokenClaims.RowHeadersVisible = false;
            this.dataGridViewTokenClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTokenClaims.Size = new System.Drawing.Size(630, 85);
            this.dataGridViewTokenClaims.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 15);
            this.label4.TabIndex = 63;
            this.label4.Text = "Others claims :";
            // 
            // listViewAutOptions
            // 
            this.listViewAutOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAutOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderContentKeyType,
            this.columnHeaderAutPolName,
            this.columnHeaderId,
            this.columnHeaderType,
            this.columnHeaderKeyType});
            this.listViewAutOptions.FullRowSelect = true;
            this.listViewAutOptions.HideSelection = false;
            this.listViewAutOptions.Location = new System.Drawing.Point(8, 22);
            this.listViewAutOptions.MultiSelect = false;
            this.listViewAutOptions.Name = "listViewAutOptions";
            this.listViewAutOptions.Size = new System.Drawing.Size(628, 161);
            this.listViewAutOptions.TabIndex = 62;
            this.listViewAutOptions.UseCompatibleStateImageBehavior = false;
            this.listViewAutOptions.View = System.Windows.Forms.View.Details;
            this.listViewAutOptions.SelectedIndexChanged += new System.EventHandler(this.listViewAutOptions_SelectedIndexChanged);
            // 
            // columnHeaderContentKeyType
            // 
            this.columnHeaderContentKeyType.Text = "Content Key Type";
            // 
            // columnHeaderAutPolName
            // 
            this.columnHeaderAutPolName.Text = "Option Name";
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "Option Id";
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Token Type";
            // 
            // columnHeaderKeyType
            // 
            this.columnHeaderKeyType.Text = "Token Key Type";
            // 
            // textBoxIssuer
            // 
            this.textBoxIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIssuer.Location = new System.Drawing.Point(10, 225);
            this.textBoxIssuer.Name = "textBoxIssuer";
            this.textBoxIssuer.ReadOnly = true;
            this.textBoxIssuer.Size = new System.Drawing.Size(626, 23);
            this.textBoxIssuer.TabIndex = 56;
            this.textBoxIssuer.Text = "http://testacs.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "Issuer of the token Url :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 15);
            this.label3.TabIndex = 59;
            this.label3.Text = "Audience or scope of the token Url :";
            // 
            // textBoxAudience
            // 
            this.textBoxAudience.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAudience.Location = new System.Drawing.Point(10, 282);
            this.textBoxAudience.Name = "textBoxAudience";
            this.textBoxAudience.ReadOnly = true;
            this.textBoxAudience.Size = new System.Drawing.Size(626, 23);
            this.textBoxAudience.TabIndex = 57;
            this.textBoxAudience.Text = "urn:test";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 708);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 55);
            this.panel1.TabIndex = 60;
            // 
            // panelJWTX509Cert
            // 
            this.panelJWTX509Cert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelJWTX509Cert.Controls.Add(this.buttonImportPFX);
            this.panelJWTX509Cert.Controls.Add(this.labelCertificateFile);
            this.panelJWTX509Cert.Enabled = false;
            this.panelJWTX509Cert.Location = new System.Drawing.Point(14, 659);
            this.panelJWTX509Cert.Name = "panelJWTX509Cert";
            this.panelJWTX509Cert.Size = new System.Drawing.Size(649, 36);
            this.panelJWTX509Cert.TabIndex = 68;
            // 
            // buttonImportPFX
            // 
            this.buttonImportPFX.Location = new System.Drawing.Point(3, 3);
            this.buttonImportPFX.Name = "buttonImportPFX";
            this.buttonImportPFX.Size = new System.Drawing.Size(227, 27);
            this.buttonImportPFX.TabIndex = 66;
            this.buttonImportPFX.Text = "Import X509 Certificate (.PFX)...";
            this.buttonImportPFX.UseVisualStyleBackColor = true;
            this.buttonImportPFX.Click += new System.EventHandler(this.buttonImportPFX_Click);
            // 
            // labelCertificateFile
            // 
            this.labelCertificateFile.Location = new System.Drawing.Point(238, 9);
            this.labelCertificateFile.Name = "labelCertificateFile";
            this.labelCertificateFile.Size = new System.Drawing.Size(405, 21);
            this.labelCertificateFile.TabIndex = 67;
            this.labelCertificateFile.Text = "(no file selected)";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateTestToken
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 763);
            this.Controls.Add(this.panelJWTX509Cert);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxStartDate);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "CreateTestToken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create a Test Token";
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