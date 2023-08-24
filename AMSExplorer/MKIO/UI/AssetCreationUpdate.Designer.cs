namespace AMSExplorer
{
    partial class AssetCreationUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetCreationUpdate));
            buttonCancel = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            labelNewAsset = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            textBoxAssetName = new System.Windows.Forms.TextBox();
            textBoxDescription = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBoxContainer = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            pictureBox1 = new System.Windows.Forms.PictureBox();
            textBoxStorage = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textBoxAssetName
            // 
            resources.ApplyResources(textBoxAssetName, "textBoxAssetName");
            textBoxAssetName.Name = "textBoxAssetName";
            textBoxAssetName.TextChanged += TextBoxAssetName_TextChanged;
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(textBoxDescription, "textBoxDescription");
            textBoxDescription.Name = "textBoxDescription";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // textBoxContainer
            // 
            resources.ApplyResources(textBoxContainer, "textBoxContainer");
            textBoxContainer.Name = "textBoxContainer";
            textBoxContainer.TextChanged += TextBoxContainer_TextChanged;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label7.Name = "label7";
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
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
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
            // AssetCreationUpdate
            // 
            AcceptButton = buttonOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(textBoxStorage);
            Controls.Add(label5);
            Controls.Add(pictureBox1);
            Controls.Add(label7);
            Controls.Add(textBoxContainer);
            Controls.Add(label4);
            Controls.Add(textBoxDescription);
            Controls.Add(label2);
            Controls.Add(textBoxAssetName);
            Controls.Add(label1);
            Controls.Add(labelNewAsset);
            Controls.Add(panel1);
            Name = "AssetCreationUpdate";
            Load += AssetCreationUpdate_Load;
            Shown += AssetCreationUpdate_Shown;
            DpiChanged += AssetCreationUpdate_DpiChanged;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelNewAsset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxContainer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxStorage;
        private System.Windows.Forms.Label label5;
    }
}