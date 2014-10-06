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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSelFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.checkBoxUseQueue = new System.Windows.Forms.CheckBox();
            this.checkBoxDeleteFile = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(111, 392);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 32);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Apply";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(245, 392);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(113, 32);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 39);
            this.label1.TabIndex = 35;
            this.label1.Text = "Specify a watch folder.\r\nAny file copied to this folder will be uploaded to Azure" +
    " Media Services as a new asset.\r\nYou must keep the application opened.";
            // 
            // radioButtonON
            // 
            this.radioButtonON.AutoSize = true;
            this.radioButtonON.Location = new System.Drawing.Point(35, 29);
            this.radioButtonON.Name = "radioButtonON";
            this.radioButtonON.Size = new System.Drawing.Size(41, 17);
            this.radioButtonON.TabIndex = 44;
            this.radioButtonON.Text = "ON";
            this.radioButtonON.UseVisualStyleBackColor = true;
            // 
            // radioButtonOFF
            // 
            this.radioButtonOFF.AutoSize = true;
            this.radioButtonOFF.Checked = true;
            this.radioButtonOFF.Location = new System.Drawing.Point(35, 53);
            this.radioButtonOFF.Name = "radioButtonOFF";
            this.radioButtonOFF.Size = new System.Drawing.Size(45, 17);
            this.radioButtonOFF.TabIndex = 46;
            this.radioButtonOFF.TabStop = true;
            this.radioButtonOFF.Text = "OFF";
            this.radioButtonOFF.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.radioButtonOFF);
            this.groupBox4.Controls.Add(this.radioButtonON);
            this.groupBox4.Location = new System.Drawing.Point(17, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(453, 86);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Activation";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.watch_folder;
            this.pictureBox1.Location = new System.Drawing.Point(13, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // buttonSelFolder
            // 
            this.buttonSelFolder.Location = new System.Drawing.Point(22, 81);
            this.buttonSelFolder.Name = "buttonSelFolder";
            this.buttonSelFolder.Size = new System.Drawing.Size(113, 23);
            this.buttonSelFolder.TabIndex = 44;
            this.buttonSelFolder.Text = "Select folder...";
            this.buttonSelFolder.UseVisualStyleBackColor = true;
            this.buttonSelFolder.Click += new System.EventHandler(this.buttonSelFolder_Click);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Enabled = false;
            this.textBoxFolder.Location = new System.Drawing.Point(142, 83);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(324, 20);
            this.textBoxFolder.TabIndex = 45;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.textBoxFolder_TextChanged);
            // 
            // checkBoxUseQueue
            // 
            this.checkBoxUseQueue.AutoSize = true;
            this.checkBoxUseQueue.Location = new System.Drawing.Point(52, 235);
            this.checkBoxUseQueue.Name = "checkBoxUseQueue";
            this.checkBoxUseQueue.Size = new System.Drawing.Size(192, 17);
            this.checkBoxUseQueue.TabIndex = 48;
            this.checkBoxUseQueue.Text = "One upload at a time (use a queue)";
            this.checkBoxUseQueue.UseVisualStyleBackColor = true;
            this.checkBoxUseQueue.CheckedChanged += new System.EventHandler(this.checkBoxParallel_CheckedChanged);
            // 
            // checkBoxDeleteFile
            // 
            this.checkBoxDeleteFile.AutoSize = true;
            this.checkBoxDeleteFile.Location = new System.Drawing.Point(52, 258);
            this.checkBoxDeleteFile.Name = "checkBoxDeleteFile";
            this.checkBoxDeleteFile.Size = new System.Drawing.Size(147, 17);
            this.checkBoxDeleteFile.TabIndex = 49;
            this.checkBoxDeleteFile.Text = "Delete file once uploaded";
            this.checkBoxDeleteFile.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.delete;
            this.pictureBox2.Location = new System.Drawing.Point(22, 258);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // WatchFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(490, 441);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.checkBoxDeleteFile);
            this.Controls.Add(this.checkBoxUseQueue);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.buttonSelFolder);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WatchFolder";
            this.Text = "Watch Folder";
            this.Load += new System.EventHandler(this.WatchFolder_Load);
            this.Shown += new System.EventHandler(this.WatchFolder_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonON;
        private System.Windows.Forms.RadioButton radioButtonOFF;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonSelFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.CheckBox checkBoxUseQueue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBoxDeleteFile;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
    }
}