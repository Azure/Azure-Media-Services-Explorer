namespace AMSExplorer
{
    partial class Hyperlapse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hyperlapse));
            this.label3 = new System.Windows.Forms.Label();
            this.textboxoutputassetname = new System.Windows.Forms.TextBox();
            this.processorlabel = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStartFrame = new System.Windows.Forms.TextBox();
            this.labelAssetName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelspeed = new System.Windows.Forms.Label();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.textBoxNumFrames = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moreinfoprofilelink = new System.Windows.Forms.LinkLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 384);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Output asset name :";
            // 
            // textboxoutputassetname
            // 
            this.textboxoutputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxoutputassetname.Location = new System.Drawing.Point(15, 400);
            this.textboxoutputassetname.Name = "textboxoutputassetname";
            this.textboxoutputassetname.Size = new System.Drawing.Size(313, 20);
            this.textboxoutputassetname.TabIndex = 21;
            // 
            // processorlabel
            // 
            this.processorlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.processorlabel.Location = new System.Drawing.Point(12, 435);
            this.processorlabel.Name = "processorlabel";
            this.processorlabel.Size = new System.Drawing.Size(529, 22);
            this.processorlabel.TabIndex = 20;
            this.processorlabel.Text = "processor name";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(444, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Number of frames :";
            // 
            // textBoxStartFrame
            // 
            this.textBoxStartFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStartFrame.Location = new System.Drawing.Point(18, 106);
            this.textBoxStartFrame.MaxLength = 128;
            this.textBoxStartFrame.Name = "textBoxStartFrame";
            this.textBoxStartFrame.Size = new System.Drawing.Size(129, 20);
            this.textBoxStartFrame.TabIndex = 34;
            this.textBoxStartFrame.Text = "0";
            this.textBoxStartFrame.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxStartFrame_Validating);
            // 
            // labelAssetName
            // 
            this.labelAssetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAssetName.Location = new System.Drawing.Point(13, 50);
            this.labelAssetName.Name = "labelAssetName";
            this.labelAssetName.Size = new System.Drawing.Size(317, 30);
            this.labelAssetName.TabIndex = 50;
            this.labelAssetName.Text = "assetname";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Start frame :";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(15, 352);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(313, 20);
            this.textBoxJobName.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Job name :";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelspeed);
            this.groupBox1.Controls.Add(this.trackBarSpeed);
            this.groupBox1.Controls.Add(this.textBoxNumFrames);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxStartFrame);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 213);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // labelspeed
            // 
            this.labelspeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelspeed.AutoSize = true;
            this.labelspeed.Location = new System.Drawing.Point(18, 157);
            this.labelspeed.Name = "labelspeed";
            this.labelspeed.Size = new System.Drawing.Size(61, 13);
            this.labelspeed.TabIndex = 41;
            this.labelspeed.Tag = "";
            this.labelspeed.Text = "Speed : {0}";
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(97, 144);
            this.trackBarSpeed.Maximum = 32;
            this.trackBarSpeed.Minimum = 2;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(410, 45);
            this.trackBarSpeed.TabIndex = 40;
            this.trackBarSpeed.TabStop = false;
            this.trackBarSpeed.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarSpeed.Value = 8;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // textBoxNumFrames
            // 
            this.textBoxNumFrames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNumFrames.Location = new System.Drawing.Point(206, 106);
            this.textBoxNumFrames.MaxLength = 128;
            this.textBoxNumFrames.Name = "textBoxNumFrames";
            this.textBoxNumFrames.Size = new System.Drawing.Size(129, 20);
            this.textBoxNumFrames.TabIndex = 39;
            this.textBoxNumFrames.Text = "10000";
            this.textBoxNumFrames.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumFrames_Validating);
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(15, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(504, 44);
            this.label4.TabIndex = 38;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(346, 336);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 61;
            this.label7.Text = "Priority:";
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPriority.Location = new System.Drawing.Point(347, 353);
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownPriority.TabIndex = 60;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.index;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(292, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(146, 23);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Launch indexing";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(346, 384);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(83, 13);
            this.label33.TabIndex = 64;
            this.label33.Text = "Output storage :";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(347, 400);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(190, 21);
            this.comboBoxStorage.TabIndex = 63;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label34.Location = new System.Drawing.Point(301, 9);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(240, 24);
            this.label34.TabIndex = 65;
            this.label34.Text = "Azure Media Hyperlapse";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(-2, 484);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 48);
            this.panel1.TabIndex = 66;
            // 
            // moreinfoprofilelink
            // 
            this.moreinfoprofilelink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoprofilelink.AutoSize = true;
            this.moreinfoprofilelink.Location = new System.Drawing.Point(452, 36);
            this.moreinfoprofilelink.Name = "moreinfoprofilelink";
            this.moreinfoprofilelink.Size = new System.Drawing.Size(85, 13);
            this.moreinfoprofilelink.TabIndex = 70;
            this.moreinfoprofilelink.TabStop = true;
            this.moreinfoprofilelink.Text = "More information";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // Hyperlapse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(553, 532);
            this.Controls.Add(this.moreinfoprofilelink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownPriority);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelAssetName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxoutputassetname);
            this.Controls.Add(this.processorlabel);
            this.Name = "Hyperlapse";
            this.Text = "Media Indexing";
            this.Load += new System.EventHandler(this.Hyperlapse_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textboxoutputassetname;
        private System.Windows.Forms.Label processorlabel;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxStartFrame;
        public System.Windows.Forms.Label labelAssetName;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxJobName;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        public System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel moreinfoprofilelink;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        public System.Windows.Forms.TextBox textBoxNumFrames;
        private System.Windows.Forms.Label labelspeed;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}