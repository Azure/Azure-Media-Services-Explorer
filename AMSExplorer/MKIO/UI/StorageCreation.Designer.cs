namespace AMSExplorer
{
    partial class StorageCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageCreation));
            buttonCancel = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            labelNewAsset = new System.Windows.Forms.Label();
            textBoxDescription = new System.Windows.Forms.TextBox();
            lblDescription = new System.Windows.Forms.Label();
            lblOptional = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            pictureBox1 = new System.Windows.Forms.PictureBox();
            textBoxStorage = new System.Windows.Forms.TextBox();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblStorage = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            numericUpDownSASValidity = new System.Windows.Forms.NumericUpDown();
            textBoxRegion = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBoxAccessKey = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSASValidity).BeginInit();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(buttonOk, "buttonOk");
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Name = "buttonOk";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonOk);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // labelNewAsset
            // 
            resources.ApplyResources(labelNewAsset, "labelNewAsset");
            labelNewAsset.ForeColor = System.Drawing.Color.DarkBlue;
            labelNewAsset.Name = "labelNewAsset";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(textBoxDescription, "textBoxDescription");
            textBoxDescription.Name = "textBoxDescription";
            // 
            // lblDescription
            // 
            resources.ApplyResources(lblDescription, "lblDescription");
            lblDescription.Name = "lblDescription";
            // 
            // lblOptional
            // 
            resources.ApplyResources(lblOptional, "lblOptional");
            lblOptional.ForeColor = System.Drawing.SystemColors.WindowFrame;
            lblOptional.Name = "lblOptional";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Bitmaps.mk_io_blue;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // textBoxStorage
            // 
            resources.ApplyResources(textBoxStorage, "textBoxStorage");
            textBoxStorage.Name = "textBoxStorage";
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
            // lblStorage
            // 
            resources.ApplyResources(lblStorage, "lblStorage");
            lblStorage.Name = "lblStorage";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // numericUpDownSASValidity
            // 
            resources.ApplyResources(numericUpDownSASValidity, "numericUpDownSASValidity");
            numericUpDownSASValidity.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
            numericUpDownSASValidity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSASValidity.Name = "numericUpDownSASValidity";
            numericUpDownSASValidity.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // textBoxRegion
            // 
            resources.ApplyResources(textBoxRegion, "textBoxRegion");
            textBoxRegion.Name = "textBoxRegion";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // textBoxAccessKey
            // 
            resources.ApplyResources(textBoxAccessKey, "textBoxAccessKey");
            textBoxAccessKey.Name = "textBoxAccessKey";
            textBoxAccessKey.UseSystemPasswordChar = true;
            textBoxAccessKey.TextChanged += textBoxAccessKey_TextChanged;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // StorageCreation
            // 
            AcceptButton = buttonOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(textBoxAccessKey);
            Controls.Add(label3);
            Controls.Add(textBoxRegion);
            Controls.Add(label2);
            Controls.Add(numericUpDownSASValidity);
            Controls.Add(label1);
            Controls.Add(textBoxStorage);
            Controls.Add(lblStorage);
            Controls.Add(pictureBox1);
            Controls.Add(lblOptional);
            Controls.Add(textBoxDescription);
            Controls.Add(lblDescription);
            Controls.Add(labelNewAsset);
            Controls.Add(panel1);
            Name = "StorageCreation";
            Load += StorageCreation_Load;
            Shown += StorageCreation_Shown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSASValidity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblOptional;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxStorage;
        public System.Windows.Forms.Label labelNewAsset;
        private System.Windows.Forms.NumericUpDown numericUpDownSASValidity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.TextBox textBoxRegion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAccessKey;
        private System.Windows.Forms.Label label3;
    }
}