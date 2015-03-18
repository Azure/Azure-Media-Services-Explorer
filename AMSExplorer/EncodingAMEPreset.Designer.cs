namespace AMSExplorer
{
    partial class EncodingAMEPreset
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
            this.label = new System.Windows.Forms.Label();
            this.textBoxJobName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listbox = new System.Windows.Forms.ListBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.moreinfopresetslink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.outputassetname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.comboBoxProcessor = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moreinfoame = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Location = new System.Drawing.Point(32, 32);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(665, 20);
            this.label.TabIndex = 0;
            this.label.Text = "label1";
            // 
            // textBoxJobName
            // 
            this.textBoxJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJobName.Location = new System.Drawing.Point(35, 287);
            this.textBoxJobName.Name = "textBoxJobName";
            this.textBoxJobName.Size = new System.Drawing.Size(353, 20);
            this.textBoxJobName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // listbox
            // 
            this.listbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listbox.FormattingEnabled = true;
            this.listbox.Location = new System.Drawing.Point(35, 116);
            this.listbox.Name = "listbox";
            this.listbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listbox.Size = new System.Drawing.Size(662, 121);
            this.listbox.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(622, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Job(s) name :";
            // 
            // moreinfopresetslink
            // 
            this.moreinfopresetslink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moreinfopresetslink.AutoSize = true;
            this.moreinfopresetslink.Location = new System.Drawing.Point(32, 240);
            this.moreinfopresetslink.Name = "moreinfopresetslink";
            this.moreinfopresetslink.Size = new System.Drawing.Size(137, 13);
            this.moreinfopresetslink.TabIndex = 7;
            this.moreinfopresetslink.TabStop = true;
            this.moreinfopresetslink.Text = "More information on presets";
            this.moreinfopresetslink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.moreinfoprofilelink_LinkClicked);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output asset(s) name :";
            // 
            // outputassetname
            // 
            this.outputassetname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputassetname.Location = new System.Drawing.Point(35, 335);
            this.outputassetname.Name = "outputassetname";
            this.outputassetname.Size = new System.Drawing.Size(353, 20);
            this.outputassetname.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Priority:";
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPriority.Location = new System.Drawing.Point(416, 288);
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownPriority.TabIndex = 35;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::AMSExplorer.Bitmaps.encoding;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(476, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(140, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Launch encoding";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(32, 56);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 13);
            this.label32.TabIndex = 49;
            this.label32.Text = "Processor :";
            // 
            // comboBoxProcessor
            // 
            this.comboBoxProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProcessor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessor.FormattingEnabled = true;
            this.comboBoxProcessor.Location = new System.Drawing.Point(35, 72);
            this.comboBoxProcessor.Name = "comboBoxProcessor";
            this.comboBoxProcessor.Size = new System.Drawing.Size(662, 21);
            this.comboBoxProcessor.TabIndex = 48;
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(413, 319);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(83, 13);
            this.label33.TabIndex = 55;
            this.label33.Text = "Output storage :";
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(416, 335);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(281, 21);
            this.comboBoxStorage.TabIndex = 54;
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(128)))), ((int)(((byte)(171)))));
            this.label34.Location = new System.Drawing.Point(483, 8);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(214, 24);
            this.label34.TabIndex = 62;
            this.label34.Text = "Azure Media Encoder";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(513, 240);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(184, 13);
            this.label12.TabIndex = 63;
            this.label12.Text = "(Use ctrl or shift to multiselect presets)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Location = new System.Drawing.Point(2, 415);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 48);
            this.panel1.TabIndex = 65;
            // 
            // moreinfoame
            // 
            this.moreinfoame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moreinfoame.AutoSize = true;
            this.moreinfoame.Location = new System.Drawing.Point(612, 52);
            this.moreinfoame.Name = "moreinfoame";
            this.moreinfoame.Size = new System.Drawing.Size(85, 13);
            this.moreinfoame.TabIndex = 66;
            this.moreinfoame.TabStop = true;
            this.moreinfoame.Text = "More information";
            // 
            // EncodingAMEPreset
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(735, 463);
            this.Controls.Add(this.moreinfoame);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.comboBoxProcessor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownPriority);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputassetname);
            this.Controls.Add(this.moreinfopresetslink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxJobName);
            this.Controls.Add(this.label);
            this.Name = "EncodingAMEPreset";
            this.Text = "AME System Preset Encoding";
            this.Load += new System.EventHandler(this.EncodingPreset_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label;
        public System.Windows.Forms.TextBox textBoxJobName;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox listbox;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel moreinfopresetslink;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox outputassetname;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        public System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox comboBoxProcessor;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        public System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel moreinfoame;

    }
}