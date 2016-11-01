namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryptionFrame1));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonAESClearKey = new System.Windows.Forms.RadioButton();
            this.radioButtonCENCKey = new System.Windows.Forms.RadioButton();
            this.groupBoxKeyType = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.radioButtonCENCCbcsKey = new System.Windows.Forms.RadioButton();
            this.radioButtonNoDynEnc = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonDecryptStorage = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBoxDelivery = new System.Windows.Forms.GroupBox();
            this.panelDynEnc = new System.Windows.Forms.Panel();
            this.checkBoxProtocolProgressiveDownload = new System.Windows.Forms.CheckBox();
            this.panelPackaging = new System.Windows.Forms.Panel();
            this.panelPackagingCENC = new System.Windows.Forms.Panel();
            this.checkBoxPlayReadyPackaging = new System.Windows.Forms.CheckBox();
            this.textBoxCustomAttributes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxWidevinePackaging = new System.Windows.Forms.CheckBox();
            this.checkBoxFairPlayPackaging = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxProtocolHLS = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolDASH = new System.Windows.Forms.CheckBox();
            this.checkBoxProtocolSmooth = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableDynEnc = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxKeyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBoxDelivery.SuspendLayout();
            this.panelDynEnc.SuspendLayout();
            this.panelPackaging.SuspendLayout();
            this.panelPackagingCENC.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonAESClearKey
            // 
            resources.ApplyResources(this.radioButtonAESClearKey, "radioButtonAESClearKey");
            this.radioButtonAESClearKey.Name = "radioButtonAESClearKey";
            this.radioButtonAESClearKey.UseVisualStyleBackColor = true;
            this.radioButtonAESClearKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // radioButtonCENCKey
            // 
            resources.ApplyResources(this.radioButtonCENCKey, "radioButtonCENCKey");
            this.radioButtonCENCKey.Checked = true;
            this.radioButtonCENCKey.Name = "radioButtonCENCKey";
            this.radioButtonCENCKey.TabStop = true;
            this.radioButtonCENCKey.UseVisualStyleBackColor = true;
            this.radioButtonCENCKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // groupBoxKeyType
            // 
            resources.ApplyResources(this.groupBoxKeyType, "groupBoxKeyType");
            this.groupBoxKeyType.Controls.Add(this.label8);
            this.groupBoxKeyType.Controls.Add(this.label3);
            this.groupBoxKeyType.Controls.Add(this.label2);
            this.groupBoxKeyType.Controls.Add(this.pictureBox4);
            this.groupBoxKeyType.Controls.Add(this.radioButtonCENCCbcsKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonNoDynEnc);
            this.groupBoxKeyType.Controls.Add(this.pictureBox1);
            this.groupBoxKeyType.Controls.Add(this.radioButtonDecryptStorage);
            this.groupBoxKeyType.Controls.Add(this.pictureBox3);
            this.groupBoxKeyType.Controls.Add(this.pictureBox2);
            this.groupBoxKeyType.Controls.Add(this.radioButtonCENCKey);
            this.groupBoxKeyType.Controls.Add(this.radioButtonAESClearKey);
            this.groupBoxKeyType.Name = "groupBoxKeyType";
            this.groupBoxKeyType.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Name = "label8";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Name = "label2";
            // 
            // pictureBox4
            // 
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // radioButtonCENCCbcsKey
            // 
            resources.ApplyResources(this.radioButtonCENCCbcsKey, "radioButtonCENCCbcsKey");
            this.radioButtonCENCCbcsKey.Name = "radioButtonCENCCbcsKey";
            this.radioButtonCENCCbcsKey.UseVisualStyleBackColor = true;
            this.radioButtonCENCCbcsKey.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // radioButtonNoDynEnc
            // 
            resources.ApplyResources(this.radioButtonNoDynEnc, "radioButtonNoDynEnc");
            this.radioButtonNoDynEnc.Name = "radioButtonNoDynEnc";
            this.radioButtonNoDynEnc.UseVisualStyleBackColor = true;
            this.radioButtonNoDynEnc.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.storage_decryption;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // radioButtonDecryptStorage
            // 
            resources.ApplyResources(this.radioButtonDecryptStorage, "radioButtonDecryptStorage");
            this.radioButtonDecryptStorage.Name = "radioButtonDecryptStorage";
            this.radioButtonDecryptStorage.UseVisualStyleBackColor = true;
            this.radioButtonDecryptStorage.CheckedChanged += new System.EventHandler(this.radioButtonCENCKey_CheckedChanged);
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.DRM_protection;
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.envelope_encryption;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // groupBoxDelivery
            // 
            resources.ApplyResources(this.groupBoxDelivery, "groupBoxDelivery");
            this.groupBoxDelivery.Controls.Add(this.panelDynEnc);
            this.groupBoxDelivery.Controls.Add(this.checkBoxEnableDynEnc);
            this.groupBoxDelivery.Name = "groupBoxDelivery";
            this.groupBoxDelivery.TabStop = false;
            // 
            // panelDynEnc
            // 
            resources.ApplyResources(this.panelDynEnc, "panelDynEnc");
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolProgressiveDownload);
            this.panelDynEnc.Controls.Add(this.panelPackaging);
            this.panelDynEnc.Controls.Add(this.label4);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolHLS);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolDASH);
            this.panelDynEnc.Controls.Add(this.checkBoxProtocolSmooth);
            this.panelDynEnc.Name = "panelDynEnc";
            // 
            // checkBoxProtocolProgressiveDownload
            // 
            resources.ApplyResources(this.checkBoxProtocolProgressiveDownload, "checkBoxProtocolProgressiveDownload");
            this.checkBoxProtocolProgressiveDownload.Name = "checkBoxProtocolProgressiveDownload";
            this.checkBoxProtocolProgressiveDownload.UseVisualStyleBackColor = true;
            // 
            // panelPackaging
            // 
            resources.ApplyResources(this.panelPackaging, "panelPackaging");
            this.panelPackaging.Controls.Add(this.panelPackagingCENC);
            this.panelPackaging.Controls.Add(this.checkBoxFairPlayPackaging);
            this.panelPackaging.Controls.Add(this.label5);
            this.panelPackaging.Name = "panelPackaging";
            // 
            // panelPackagingCENC
            // 
            resources.ApplyResources(this.panelPackagingCENC, "panelPackagingCENC");
            this.panelPackagingCENC.Controls.Add(this.checkBoxPlayReadyPackaging);
            this.panelPackagingCENC.Controls.Add(this.textBoxCustomAttributes);
            this.panelPackagingCENC.Controls.Add(this.label6);
            this.panelPackagingCENC.Controls.Add(this.label7);
            this.panelPackagingCENC.Controls.Add(this.checkBoxWidevinePackaging);
            this.panelPackagingCENC.Name = "panelPackagingCENC";
            // 
            // checkBoxPlayReadyPackaging
            // 
            resources.ApplyResources(this.checkBoxPlayReadyPackaging, "checkBoxPlayReadyPackaging");
            this.checkBoxPlayReadyPackaging.Checked = true;
            this.checkBoxPlayReadyPackaging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlayReadyPackaging.Name = "checkBoxPlayReadyPackaging";
            this.checkBoxPlayReadyPackaging.UseVisualStyleBackColor = true;
            this.checkBoxPlayReadyPackaging.CheckedChanged += new System.EventHandler(this.checkBoxPlayReadyPackaging_CheckedChanged);
            // 
            // textBoxCustomAttributes
            // 
            resources.ApplyResources(this.textBoxCustomAttributes, "textBoxCustomAttributes");
            this.textBoxCustomAttributes.Name = "textBoxCustomAttributes";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // checkBoxWidevinePackaging
            // 
            resources.ApplyResources(this.checkBoxWidevinePackaging, "checkBoxWidevinePackaging");
            this.checkBoxWidevinePackaging.Checked = true;
            this.checkBoxWidevinePackaging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWidevinePackaging.Name = "checkBoxWidevinePackaging";
            this.checkBoxWidevinePackaging.UseVisualStyleBackColor = true;
            // 
            // checkBoxFairPlayPackaging
            // 
            resources.ApplyResources(this.checkBoxFairPlayPackaging, "checkBoxFairPlayPackaging");
            this.checkBoxFairPlayPackaging.Checked = true;
            this.checkBoxFairPlayPackaging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFairPlayPackaging.Name = "checkBoxFairPlayPackaging";
            this.checkBoxFairPlayPackaging.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label4.Name = "label4";
            // 
            // checkBoxProtocolHLS
            // 
            resources.ApplyResources(this.checkBoxProtocolHLS, "checkBoxProtocolHLS");
            this.checkBoxProtocolHLS.Checked = true;
            this.checkBoxProtocolHLS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolHLS.Name = "checkBoxProtocolHLS";
            this.checkBoxProtocolHLS.UseVisualStyleBackColor = true;
            // 
            // checkBoxProtocolDASH
            // 
            resources.ApplyResources(this.checkBoxProtocolDASH, "checkBoxProtocolDASH");
            this.checkBoxProtocolDASH.Checked = true;
            this.checkBoxProtocolDASH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolDASH.Name = "checkBoxProtocolDASH";
            this.checkBoxProtocolDASH.UseVisualStyleBackColor = true;
            this.checkBoxProtocolDASH.CheckedChanged += new System.EventHandler(this.checkBoxProtocolDASH_CheckedChanged);
            // 
            // checkBoxProtocolSmooth
            // 
            resources.ApplyResources(this.checkBoxProtocolSmooth, "checkBoxProtocolSmooth");
            this.checkBoxProtocolSmooth.Checked = true;
            this.checkBoxProtocolSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProtocolSmooth.Name = "checkBoxProtocolSmooth";
            this.checkBoxProtocolSmooth.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableDynEnc
            // 
            resources.ApplyResources(this.checkBoxEnableDynEnc, "checkBoxEnableDynEnc");
            this.checkBoxEnableDynEnc.Checked = true;
            this.checkBoxEnableDynEnc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableDynEnc.Name = "checkBoxEnableDynEnc";
            this.checkBoxEnableDynEnc.UseVisualStyleBackColor = true;
            this.checkBoxEnableDynEnc.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            // 
            // AddDynamicEncryptionFrame1
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxDelivery);
            this.Controls.Add(this.groupBoxKeyType);
            this.Name = "AddDynamicEncryptionFrame1";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.groupBoxKeyType.ResumeLayout(false);
            this.groupBoxKeyType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBoxDelivery.ResumeLayout(false);
            this.groupBoxDelivery.PerformLayout();
            this.panelDynEnc.ResumeLayout(false);
            this.panelDynEnc.PerformLayout();
            this.panelPackaging.ResumeLayout(false);
            this.panelPackaging.PerformLayout();
            this.panelPackagingCENC.ResumeLayout(false);
            this.panelPackagingCENC.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonAESClearKey;
        private System.Windows.Forms.RadioButton radioButtonCENCKey;
        private System.Windows.Forms.GroupBox groupBoxKeyType;
        private System.Windows.Forms.GroupBox groupBoxDelivery;
        private System.Windows.Forms.CheckBox checkBoxProtocolSmooth;
        private System.Windows.Forms.CheckBox checkBoxProtocolDASH;
        private System.Windows.Forms.CheckBox checkBoxProtocolHLS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonDecryptStorage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonNoDynEnc;
        private System.Windows.Forms.CheckBox checkBoxEnableDynEnc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxWidevinePackaging;
        private System.Windows.Forms.CheckBox checkBoxPlayReadyPackaging;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCustomAttributes;
        private System.Windows.Forms.Panel panelDynEnc;
        private System.Windows.Forms.Panel panelPackaging;
        private System.Windows.Forms.CheckBox checkBoxProtocolProgressiveDownload;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RadioButton radioButtonCENCCbcsKey;
        private System.Windows.Forms.CheckBox checkBoxFairPlayPackaging;
        private System.Windows.Forms.Panel panelPackagingCENC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}