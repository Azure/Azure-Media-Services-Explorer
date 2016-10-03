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
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(386, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(176, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Next";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(569, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 42);
            this.label1.TabIndex = 49;
            this.label1.Text = "Step 3\r\nAES Key delivery";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-1, 688);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 55);
            this.panel1.TabIndex = 51;
            // 
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Controls.Add(this.label8);
            this.groupBoxAuthPol.Controls.Add(this.label2);
            this.groupBoxAuthPol.Controls.Add(this.numericUpDownNbOptions);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonDefineAuthPol);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonNoAuthPolicy);
            this.groupBoxAuthPol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAuthPol.Location = new System.Drawing.Point(14, 96);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(645, 144);
            this.groupBoxAuthPol.TabIndex = 52;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Key Delivery From Azure Media Services";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(64, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(574, 42);
            this.label8.TabIndex = 69;
            this.label8.Text = "Having more than one option is useful if you want to support several types of tok" +
    "ens";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(447, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 65;
            this.label2.Text = "option(s)";
            // 
            // numericUpDownNbOptions
            // 
            this.numericUpDownNbOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownNbOptions.Location = new System.Drawing.Point(387, 31);
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
            this.numericUpDownNbOptions.Size = new System.Drawing.Size(54, 23);
            this.numericUpDownNbOptions.TabIndex = 64;
            this.numericUpDownNbOptions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // radioButtonDefineAuthPol
            // 
            this.radioButtonDefineAuthPol.AutoSize = true;
            this.radioButtonDefineAuthPol.Checked = true;
            this.radioButtonDefineAuthPol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDefineAuthPol.Location = new System.Drawing.Point(41, 31);
            this.radioButtonDefineAuthPol.Name = "radioButtonDefineAuthPol";
            this.radioButtonDefineAuthPol.Size = new System.Drawing.Size(340, 19);
            this.radioButtonDefineAuthPol.TabIndex = 63;
            this.radioButtonDefineAuthPol.TabStop = true;
            this.radioButtonDefineAuthPol.Text = "Yes - Define an authorization policy for the content key with";
            this.radioButtonDefineAuthPol.UseVisualStyleBackColor = true;
            this.radioButtonDefineAuthPol.CheckedChanged += new System.EventHandler(this.radioButtonDefineAuthPol_CheckedChanged);
            // 
            // radioButtonNoAuthPolicy
            // 
            this.radioButtonNoAuthPolicy.AutoSize = true;
            this.radioButtonNoAuthPolicy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNoAuthPolicy.Location = new System.Drawing.Point(41, 97);
            this.radioButtonNoAuthPolicy.Name = "radioButtonNoAuthPolicy";
            this.radioButtonNoAuthPolicy.Size = new System.Drawing.Size(303, 19);
            this.radioButtonNoAuthPolicy.TabIndex = 62;
            this.radioButtonNoAuthPolicy.Text = "No - An external key server is used to deliver the keys";
            this.radioButtonNoAuthPolicy.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(32, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(550, 88);
            this.label3.TabIndex = 76;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // labelkeylaurl
            // 
            this.labelkeylaurl.AutoSize = true;
            this.labelkeylaurl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelkeylaurl.Location = new System.Drawing.Point(32, 40);
            this.labelkeylaurl.Name = "labelkeylaurl";
            this.labelkeylaurl.Size = new System.Drawing.Size(113, 15);
            this.labelkeylaurl.TabIndex = 75;
            this.labelkeylaurl.Text = "Key Acquisition Url :";
            // 
            // textBoxLAURL
            // 
            this.textBoxLAURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLAURL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxLAURL.Location = new System.Drawing.Point(35, 58);
            this.textBoxLAURL.Name = "textBoxLAURL";
            this.textBoxLAURL.Size = new System.Drawing.Size(547, 23);
            this.textBoxLAURL.TabIndex = 74;
            this.textBoxLAURL.TextChanged += new System.EventHandler(this.textBoxLAURL_TextChanged);
            // 
            // groupBoxKeyAcqUrl
            // 
            this.groupBoxKeyAcqUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeyAcqUrl.Controls.Add(this.checkBoxFinalExtURL);
            this.groupBoxKeyAcqUrl.Controls.Add(this.label7);
            this.groupBoxKeyAcqUrl.Controls.Add(this.labelkeylaurl);
            this.groupBoxKeyAcqUrl.Controls.Add(this.textBoxLAURL);
            this.groupBoxKeyAcqUrl.Controls.Add(this.label3);
            this.groupBoxKeyAcqUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxKeyAcqUrl.Location = new System.Drawing.Point(14, 260);
            this.groupBoxKeyAcqUrl.Name = "groupBoxKeyAcqUrl";
            this.groupBoxKeyAcqUrl.Size = new System.Drawing.Size(645, 223);
            this.groupBoxKeyAcqUrl.TabIndex = 78;
            this.groupBoxKeyAcqUrl.TabStop = false;
            this.groupBoxKeyAcqUrl.Text = "Key Acquisition Url";
            // 
            // checkBoxFinalExtURL
            // 
            this.checkBoxFinalExtURL.AutoSize = true;
            this.checkBoxFinalExtURL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxFinalExtURL.Location = new System.Drawing.Point(35, 166);
            this.checkBoxFinalExtURL.Name = "checkBoxFinalExtURL";
            this.checkBoxFinalExtURL.Size = new System.Drawing.Size(75, 19);
            this.checkBoxFinalExtURL.TabIndex = 97;
            this.checkBoxFinalExtURL.Text = "Final URL";
            this.checkBoxFinalExtURL.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Location = new System.Drawing.Point(599, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 15);
            this.label7.TabIndex = 80;
            this.label7.Text = "(Url)";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddDynamicEncryptionFrame3_AESDelivery
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.groupBoxKeyAcqUrl);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddDynamicEncryptionFrame3_AESDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic Encryption - Step 3";
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