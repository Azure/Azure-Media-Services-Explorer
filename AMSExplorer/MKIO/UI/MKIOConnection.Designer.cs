namespace AMSExplorer
{
    partial class MKIOConnection
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MKIOConnection));
            buttonDoNotConnectMKIO = new System.Windows.Forms.Button();
            buttonConnectMKIO = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            labelNewAsset = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            textSubscriptionName = new System.Windows.Forms.TextBox();
            textMKToken = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            pictureBoxMKIO = new System.Windows.Forms.PictureBox();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            textInstructions = new System.Windows.Forms.TextBox();
            linkLabelMKIO = new System.Windows.Forms.LinkLabel();
            linkLabelMigration = new System.Windows.Forms.LinkLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMKIO).BeginInit();
            SuspendLayout();
            // 
            // buttonDoNotConnectMKIO
            // 
            resources.ApplyResources(buttonDoNotConnectMKIO, "buttonDoNotConnectMKIO");
            buttonDoNotConnectMKIO.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonDoNotConnectMKIO.Name = "buttonDoNotConnectMKIO";
            buttonDoNotConnectMKIO.UseVisualStyleBackColor = true;
            // 
            // buttonConnectMKIO
            // 
            resources.ApplyResources(buttonConnectMKIO, "buttonConnectMKIO");
            buttonConnectMKIO.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonConnectMKIO.Name = "buttonConnectMKIO";
            buttonConnectMKIO.UseVisualStyleBackColor = true;
            buttonConnectMKIO.Click += buttonOk_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonDoNotConnectMKIO);
            panel1.Controls.Add(buttonConnectMKIO);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // labelNewAsset
            // 
            resources.ApplyResources(labelNewAsset, "labelNewAsset");
            labelNewAsset.ForeColor = System.Drawing.Color.DarkBlue;
            labelNewAsset.Name = "labelNewAsset";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textSubscriptionName
            // 
            resources.ApplyResources(textSubscriptionName, "textSubscriptionName");
            textSubscriptionName.Name = "textSubscriptionName";
            textSubscriptionName.TextChanged += textSubscriptionName_TextChanged;
            // 
            // textMKToken
            // 
            resources.ApplyResources(textMKToken, "textMKToken");
            textMKToken.Name = "textMKToken";
            textMKToken.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // pictureBoxMKIO
            // 
            resources.ApplyResources(pictureBoxMKIO, "pictureBoxMKIO");
            pictureBoxMKIO.Image = Bitmaps.mk_io_blue;
            pictureBoxMKIO.Name = "pictureBoxMKIO";
            pictureBoxMKIO.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // textInstructions
            // 
            resources.ApplyResources(textInstructions, "textInstructions");
            textInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textInstructions.Name = "textInstructions";
            textInstructions.ReadOnly = true;
            // 
            // linkLabelMKIO
            // 
            resources.ApplyResources(linkLabelMKIO, "linkLabelMKIO");
            linkLabelMKIO.Name = "linkLabelMKIO";
            linkLabelMKIO.TabStop = true;
            linkLabelMKIO.LinkClicked += linkLabelMKIO_LinkClicked;
            // 
            // linkLabelMigration
            // 
            resources.ApplyResources(linkLabelMigration, "linkLabelMigration");
            linkLabelMigration.Name = "linkLabelMigration";
            linkLabelMigration.TabStop = true;
            linkLabelMigration.LinkClicked += linkLabelMKIO_LinkClicked;
            // 
            // MKIOConnection
            // 
            AcceptButton = buttonConnectMKIO;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonDoNotConnectMKIO;
            Controls.Add(linkLabelMigration);
            Controls.Add(linkLabelMKIO);
            Controls.Add(textInstructions);
            Controls.Add(pictureBoxMKIO);
            Controls.Add(textMKToken);
            Controls.Add(label2);
            Controls.Add(textSubscriptionName);
            Controls.Add(label1);
            Controls.Add(labelNewAsset);
            Controls.Add(panel1);
            Name = "MKIOConnection";
            Load += MKIOConnection_Load;
            Shown += MKIOConnection_Shown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMKIO).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.Button buttonConnectMKIO;
        public System.Windows.Forms.Button buttonDoNotConnectMKIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSubscriptionName;
        private System.Windows.Forms.TextBox textMKToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox pictureBoxMKIO;
        public System.Windows.Forms.Label labelNewAsset;
        private System.Windows.Forms.TextBox textInstructions;
        private System.Windows.Forms.LinkLabel linkLabelMKIO;
        private System.Windows.Forms.LinkLabel linkLabelMigration;
    }
}