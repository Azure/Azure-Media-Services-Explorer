namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame3_AESDelivery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryptionFrame3_AESDelivery));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxAuthPol = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNbOptions = new System.Windows.Forms.NumericUpDown();
            this.radioButtonDefineAuthPol = new System.Windows.Forms.RadioButton();
            this.radioButtonNoAuthPolicy = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.labelkeylaurl = new System.Windows.Forms.Label();
            this.textBoxLAURL = new System.Windows.Forms.TextBox();
            this.groupBoxKeyAcqUrl = new System.Windows.Forms.GroupBox();
            this.checkBoxFinalExtURL = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBoxAuthPol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptions)).BeginInit();
            this.groupBoxKeyAcqUrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
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
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Controls.Add(this.label8);
            this.groupBoxAuthPol.Controls.Add(this.label2);
            this.groupBoxAuthPol.Controls.Add(this.numericUpDownNbOptions);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonDefineAuthPol);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonNoAuthPolicy);
            resources.ApplyResources(this.groupBoxAuthPol, "groupBoxAuthPol");
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Name = "label8";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDownNbOptions
            // 
            resources.ApplyResources(this.numericUpDownNbOptions, "numericUpDownNbOptions");
            this.numericUpDownNbOptions.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNbOptions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbOptions.Name = "numericUpDownNbOptions";
            this.numericUpDownNbOptions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonDefineAuthPol
            // 
            resources.ApplyResources(this.radioButtonDefineAuthPol, "radioButtonDefineAuthPol");
            this.radioButtonDefineAuthPol.Checked = true;
            this.radioButtonDefineAuthPol.Name = "radioButtonDefineAuthPol";
            this.radioButtonDefineAuthPol.TabStop = true;
            this.radioButtonDefineAuthPol.UseVisualStyleBackColor = true;
            this.radioButtonDefineAuthPol.CheckedChanged += new System.EventHandler(this.radioButtonDefineAuthPol_CheckedChanged);
            // 
            // radioButtonNoAuthPolicy
            // 
            resources.ApplyResources(this.radioButtonNoAuthPolicy, "radioButtonNoAuthPolicy");
            this.radioButtonNoAuthPolicy.Name = "radioButtonNoAuthPolicy";
            this.radioButtonNoAuthPolicy.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Name = "label3";
            // 
            // labelkeylaurl
            // 
            resources.ApplyResources(this.labelkeylaurl, "labelkeylaurl");
            this.labelkeylaurl.Name = "labelkeylaurl";
            // 
            // textBoxLAURL
            // 
            resources.ApplyResources(this.textBoxLAURL, "textBoxLAURL");
            this.textBoxLAURL.Name = "textBoxLAURL";
            this.textBoxLAURL.TextChanged += new System.EventHandler(this.textBoxLAURL_TextChanged);
            // 
            // groupBoxKeyAcqUrl
            // 
            resources.ApplyResources(this.groupBoxKeyAcqUrl, "groupBoxKeyAcqUrl");
            this.groupBoxKeyAcqUrl.Controls.Add(this.checkBoxFinalExtURL);
            this.groupBoxKeyAcqUrl.Controls.Add(this.label7);
            this.groupBoxKeyAcqUrl.Controls.Add(this.labelkeylaurl);
            this.groupBoxKeyAcqUrl.Controls.Add(this.textBoxLAURL);
            this.groupBoxKeyAcqUrl.Controls.Add(this.label3);
            this.groupBoxKeyAcqUrl.Name = "groupBoxKeyAcqUrl";
            this.groupBoxKeyAcqUrl.TabStop = false;
            // 
            // checkBoxFinalExtURL
            // 
            resources.ApplyResources(this.checkBoxFinalExtURL, "checkBoxFinalExtURL");
            this.checkBoxFinalExtURL.Name = "checkBoxFinalExtURL";
            this.checkBoxFinalExtURL.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Name = "label7";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddDynamicEncryptionFrame3_AESDelivery
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBoxKeyAcqUrl);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "AddDynamicEncryptionFrame3_AESDelivery";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxAuthPol.ResumeLayout(false);
            this.groupBoxAuthPol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptions)).EndInit();
            this.groupBoxKeyAcqUrl.ResumeLayout(false);
            this.groupBoxKeyAcqUrl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxAuthPol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNbOptions;
        private System.Windows.Forms.RadioButton radioButtonDefineAuthPol;
        private System.Windows.Forms.RadioButton radioButtonNoAuthPolicy;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label labelkeylaurl;
        public System.Windows.Forms.TextBox textBoxLAURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxKeyAcqUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox checkBoxFinalExtURL;
    }
}