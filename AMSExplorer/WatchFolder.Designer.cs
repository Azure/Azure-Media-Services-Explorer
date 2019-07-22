namespace AMSExplorer
{
    partial class WatchFolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatchFolder));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonON = new System.Windows.Forms.RadioButton();
            this.radioButtonOFF = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonSelFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.checkBoxDeleteFile = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.checkBoxRunJobTemplate = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.checkBoxPublishOAssets = new System.Windows.Forms.CheckBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.checkBoxSendEMail = new System.Windows.Forms.CheckBox();
            this.textBoxEMail = new System.Windows.Forms.TextBox();
            this.buttonTestEmail = new System.Windows.Forms.Button();
            this.radioButtonInsertSelectedAssets = new System.Windows.Forms.RadioButton();
            this.groupBoxProcess = new System.Windows.Forms.GroupBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.panelInsertAsset = new System.Windows.Forms.Panel();
            this.checkBoAddAssetsToInput = new System.Windows.Forms.CheckBox();
            this.listViewTemplates = new AMSExplorer.ListViewTemplates();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxProcessJSONSemaphore = new System.Windows.Forms.CheckBox();
            this.buttonSeeRhozetExample = new System.Windows.Forms.Button();
            this.checkBoxProcessXMLRohzet = new System.Windows.Forms.CheckBox();
            this.checkBoxCallAPI = new System.Windows.Forms.CheckBox();
            this.textBoxAPIUrl = new System.Windows.Forms.TextBox();
            this.buttonJsonBody = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBoxProcess.SuspendLayout();
            this.panelInsertAsset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.groupBoxOptions.SuspendLayout();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Name = "label1";
            // 
            // radioButtonON
            // 
            resources.ApplyResources(this.radioButtonON, "radioButtonON");
            this.radioButtonON.Name = "radioButtonON";
            this.radioButtonON.UseVisualStyleBackColor = true;
            // 
            // radioButtonOFF
            // 
            resources.ApplyResources(this.radioButtonOFF, "radioButtonOFF");
            this.radioButtonOFF.Checked = true;
            this.radioButtonOFF.Name = "radioButtonOFF";
            this.radioButtonOFF.TabStop = true;
            this.radioButtonOFF.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.radioButtonOFF);
            this.groupBox4.Controls.Add(this.radioButtonON);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.watch_folder;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // buttonSelFolder
            // 
            resources.ApplyResources(this.buttonSelFolder, "buttonSelFolder");
            this.buttonSelFolder.Name = "buttonSelFolder";
            this.buttonSelFolder.UseVisualStyleBackColor = true;
            this.buttonSelFolder.Click += new System.EventHandler(this.buttonSelFolder_Click);
            // 
            // textBoxFolder
            // 
            resources.ApplyResources(this.textBoxFolder, "textBoxFolder");
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.TextChanged += new System.EventHandler(this.textBoxFolder_TextChanged);
            // 
            // checkBoxDeleteFile
            // 
            resources.ApplyResources(this.checkBoxDeleteFile, "checkBoxDeleteFile");
            this.checkBoxDeleteFile.Name = "checkBoxDeleteFile";
            this.checkBoxDeleteFile.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.delete;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // checkBoxRunJobTemplate
            // 
            resources.ApplyResources(this.checkBoxRunJobTemplate, "checkBoxRunJobTemplate");
            this.checkBoxRunJobTemplate.Name = "checkBoxRunJobTemplate";
            this.checkBoxRunJobTemplate.UseVisualStyleBackColor = true;
            this.checkBoxRunJobTemplate.CheckedChanged += new System.EventHandler(this.checkBoxRunJobTemplate_CheckedChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Name = "panel1";
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.streaming_locator;
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // checkBoxPublishOAssets
            // 
            resources.ApplyResources(this.checkBoxPublishOAssets, "checkBoxPublishOAssets");
            this.checkBoxPublishOAssets.Name = "checkBoxPublishOAssets";
            this.checkBoxPublishOAssets.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Image = global::AMSExplorer.Bitmaps.create_outlook_report;
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // checkBoxSendEMail
            // 
            resources.ApplyResources(this.checkBoxSendEMail, "checkBoxSendEMail");
            this.checkBoxSendEMail.Name = "checkBoxSendEMail";
            this.checkBoxSendEMail.UseVisualStyleBackColor = true;
            this.checkBoxSendEMail.CheckedChanged += new System.EventHandler(this.checkBoxSendEMail_CheckedChanged);
            // 
            // textBoxEMail
            // 
            resources.ApplyResources(this.textBoxEMail, "textBoxEMail");
            this.textBoxEMail.Name = "textBoxEMail";
            // 
            // buttonTestEmail
            // 
            resources.ApplyResources(this.buttonTestEmail, "buttonTestEmail");
            this.buttonTestEmail.Name = "buttonTestEmail";
            this.buttonTestEmail.UseVisualStyleBackColor = true;
            this.buttonTestEmail.Click += new System.EventHandler(this.buttonTestEmail_Click);
            // 
            // radioButtonInsertSelectedAssets
            // 
            resources.ApplyResources(this.radioButtonInsertSelectedAssets, "radioButtonInsertSelectedAssets");
            this.radioButtonInsertSelectedAssets.Checked = true;
            this.radioButtonInsertSelectedAssets.Name = "radioButtonInsertSelectedAssets";
            this.radioButtonInsertSelectedAssets.TabStop = true;
            this.radioButtonInsertSelectedAssets.UseVisualStyleBackColor = true;
            this.radioButtonInsertSelectedAssets.CheckedChanged += new System.EventHandler(this.radioButtonInsertSelectedAssets_CheckedChanged);
            // 
            // groupBoxProcess
            // 
            resources.ApplyResources(this.groupBoxProcess, "groupBoxProcess");
            this.groupBoxProcess.Controls.Add(this.labelWarning);
            this.groupBoxProcess.Controls.Add(this.panelInsertAsset);
            this.groupBoxProcess.Controls.Add(this.checkBoAddAssetsToInput);
            this.groupBoxProcess.Controls.Add(this.listViewTemplates);
            this.groupBoxProcess.Name = "groupBoxProcess";
            this.groupBoxProcess.TabStop = false;
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Name = "labelWarning";
            // 
            // panelInsertAsset
            // 
            resources.ApplyResources(this.panelInsertAsset, "panelInsertAsset");
            this.panelInsertAsset.Controls.Add(this.radioButtonInsertSelectedAssets);
            this.panelInsertAsset.Name = "panelInsertAsset";
            // 
            // checkBoAddAssetsToInput
            // 
            resources.ApplyResources(this.checkBoAddAssetsToInput, "checkBoAddAssetsToInput");
            this.checkBoAddAssetsToInput.Name = "checkBoAddAssetsToInput";
            this.checkBoAddAssetsToInput.UseVisualStyleBackColor = true;
            this.checkBoAddAssetsToInput.CheckedChanged += new System.EventHandler(this.checkBoAddAssetsToInput_CheckedChanged);
            // 
            // listViewTemplates
            // 
            resources.ApplyResources(this.listViewTemplates, "listViewTemplates");
            this.listViewTemplates.FullRowSelect = true;
            this.listViewTemplates.HideSelection = false;
            this.listViewTemplates.MultiSelect = false;
            this.listViewTemplates.Name = "listViewTemplates";
            this.listViewTemplates.Tag = -1;
            this.listViewTemplates.UseCompatibleStateImageBehavior = false;
            this.listViewTemplates.View = System.Windows.Forms.View.Details;
            this.listViewTemplates.SelectedIndexChanged += new System.EventHandler(this.listViewTemplates_SelectedIndexChanged);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::AMSExplorer.Bitmaps.encoding;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.Color.DarkBlue;
            this.label13.Name = "label13";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Name = "panel2";
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.button1);
            this.groupBoxOptions.Controls.Add(this.checkBoxProcessJSONSemaphore);
            this.groupBoxOptions.Controls.Add(this.buttonSeeRhozetExample);
            this.groupBoxOptions.Controls.Add(this.checkBoxProcessXMLRohzet);
            this.groupBoxOptions.Controls.Add(this.checkBoxDeleteFile);
            this.groupBoxOptions.Controls.Add(this.pictureBox2);
            resources.ApplyResources(this.groupBoxOptions, "groupBoxOptions");
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxProcessJSONSemaphore
            // 
            resources.ApplyResources(this.checkBoxProcessJSONSemaphore, "checkBoxProcessJSONSemaphore");
            this.checkBoxProcessJSONSemaphore.Name = "checkBoxProcessJSONSemaphore";
            this.checkBoxProcessJSONSemaphore.UseVisualStyleBackColor = true;
            // 
            // buttonSeeRhozetExample
            // 
            resources.ApplyResources(this.buttonSeeRhozetExample, "buttonSeeRhozetExample");
            this.buttonSeeRhozetExample.Name = "buttonSeeRhozetExample";
            this.buttonSeeRhozetExample.UseVisualStyleBackColor = true;
            this.buttonSeeRhozetExample.Click += new System.EventHandler(this.buttonSeeRhozetExample_Click);
            // 
            // checkBoxProcessXMLRohzet
            // 
            resources.ApplyResources(this.checkBoxProcessXMLRohzet, "checkBoxProcessXMLRohzet");
            this.checkBoxProcessXMLRohzet.Name = "checkBoxProcessXMLRohzet";
            this.checkBoxProcessXMLRohzet.UseVisualStyleBackColor = true;
            // 
            // checkBoxCallAPI
            // 
            resources.ApplyResources(this.checkBoxCallAPI, "checkBoxCallAPI");
            this.checkBoxCallAPI.Name = "checkBoxCallAPI";
            this.checkBoxCallAPI.UseVisualStyleBackColor = true;
            this.checkBoxCallAPI.CheckedChanged += new System.EventHandler(this.checkBoxCallAPI_CheckedChanged);
            // 
            // textBoxAPIUrl
            // 
            resources.ApplyResources(this.textBoxAPIUrl, "textBoxAPIUrl");
            this.textBoxAPIUrl.Name = "textBoxAPIUrl";
            // 
            // buttonJsonBody
            // 
            resources.ApplyResources(this.buttonJsonBody, "buttonJsonBody");
            this.buttonJsonBody.Name = "buttonJsonBody";
            this.buttonJsonBody.UseVisualStyleBackColor = true;
            this.buttonJsonBody.Click += new System.EventHandler(this.buttonJsonBody_Click);
            // 
            // WatchFolder
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonJsonBody);
            this.Controls.Add(this.textBoxAPIUrl);
            this.Controls.Add(this.checkBoxCallAPI);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.groupBoxProcess);
            this.Controls.Add(this.buttonTestEmail);
            this.Controls.Add(this.textBoxEMail);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.checkBoxRunJobTemplate);
            this.Controls.Add(this.checkBoxSendEMail);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.checkBoxPublishOAssets);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.buttonSelFolder);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Name = "WatchFolder";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.WatchFolder_Load);
            this.Shown += new System.EventHandler(this.WatchFolder_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBoxProcess.ResumeLayout(false);
            this.groupBoxProcess.PerformLayout();
            this.panelInsertAsset.ResumeLayout(false);
            this.panelInsertAsset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonON;
        private System.Windows.Forms.RadioButton radioButtonOFF;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSelFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxDeleteFile;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxRunJobTemplate;
        private ListViewTemplates listViewTemplates;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox checkBoxPublishOAssets;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.CheckBox checkBoxSendEMail;
        private System.Windows.Forms.TextBox textBoxEMail;
        private System.Windows.Forms.Button buttonTestEmail;
        private System.Windows.Forms.RadioButton radioButtonInsertSelectedAssets;
        private System.Windows.Forms.GroupBox groupBoxProcess;
        private System.Windows.Forms.CheckBox checkBoAddAssetsToInput;
        private System.Windows.Forms.Panel panelInsertAsset;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxProcessXMLRohzet;
        private System.Windows.Forms.Button buttonSeeRhozetExample;
        private System.Windows.Forms.CheckBox checkBoxCallAPI;
        private System.Windows.Forms.TextBox textBoxAPIUrl;
        private System.Windows.Forms.Button buttonJsonBody;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxProcessJSONSemaphore;
    }
}