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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxAuthPol = new System.Windows.Forms.GroupBox();
            this.labelkeylaurl = new System.Windows.Forms.Label();
            this.textBoxLAURL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNbOptions = new System.Windows.Forms.NumericUpDown();
            this.radioButtonDefineAuthPol = new System.Windows.Forms.RadioButton();
            this.radioButtonNoAuthPolicy = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBoxAuthPol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            this.panel1.Location = new System.Drawing.Point(-1, 710);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 55);
            this.panel1.TabIndex = 51;
            // 
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Controls.Add(this.labelkeylaurl);
            this.groupBoxAuthPol.Controls.Add(this.textBoxLAURL);
            this.groupBoxAuthPol.Controls.Add(this.label8);
            this.groupBoxAuthPol.Controls.Add(this.label2);
            this.groupBoxAuthPol.Controls.Add(this.numericUpDownNbOptions);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonDefineAuthPol);
            this.groupBoxAuthPol.Controls.Add(this.radioButtonNoAuthPolicy);
            this.groupBoxAuthPol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAuthPol.Location = new System.Drawing.Point(14, 96);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(645, 197);
            this.groupBoxAuthPol.TabIndex = 52;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Key Delivery From Azure Media Services";
            // 
            // labelkeylaurl
            // 
            this.labelkeylaurl.AutoSize = true;
            this.labelkeylaurl.Enabled = false;
            this.labelkeylaurl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelkeylaurl.Location = new System.Drawing.Point(60, 126);
            this.labelkeylaurl.Name = "labelkeylaurl";
            this.labelkeylaurl.Size = new System.Drawing.Size(113, 15);
            this.labelkeylaurl.TabIndex = 75;
            this.labelkeylaurl.Text = "Key Acquisition Url :";
            // 
            // textBoxLAURL
            // 
            this.textBoxLAURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLAURL.Enabled = false;
            this.textBoxLAURL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxLAURL.Location = new System.Drawing.Point(63, 144);
            this.textBoxLAURL.Name = "textBoxLAURL";
            this.textBoxLAURL.Size = new System.Drawing.Size(547, 23);
            this.textBoxLAURL.TabIndex = 74;
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
            // AddDynamicEncryptionFrame3_AESDelivery
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
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
    }
}