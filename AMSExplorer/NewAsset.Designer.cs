namespace AMSExplorer
{
    partial class NewAsset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAsset));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStorage = new System.Windows.Forms.Label();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.labelNewAsset = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAssetName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAltId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxContainer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorProvider1.SetError(this.buttonCancel, resources.GetString("buttonCancel.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonCancel.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonCancel, ((int)(resources.GetObject("buttonCancel.IconPadding"))));
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonOk, resources.GetString("buttonOk.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonOk, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonOk.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonOk, ((int)(resources.GetObject("buttonOk.IconPadding"))));
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOk);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            // 
            // labelStorage
            // 
            resources.ApplyResources(this.labelStorage, "labelStorage");
            this.errorProvider1.SetError(this.labelStorage, resources.GetString("labelStorage.Error"));
            this.errorProvider1.SetIconAlignment(this.labelStorage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelStorage.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelStorage, ((int)(resources.GetObject("labelStorage.IconPadding"))));
            this.labelStorage.Name = "labelStorage";
            // 
            // comboBoxStorage
            // 
            resources.ApplyResources(this.comboBoxStorage, "comboBoxStorage");
            this.comboBoxStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.comboBoxStorage, resources.GetString("comboBoxStorage.Error"));
            this.comboBoxStorage.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.comboBoxStorage, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("comboBoxStorage.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.comboBoxStorage, ((int)(resources.GetObject("comboBoxStorage.IconPadding"))));
            this.comboBoxStorage.Name = "comboBoxStorage";
            // 
            // labelNewAsset
            // 
            resources.ApplyResources(this.labelNewAsset, "labelNewAsset");
            this.errorProvider1.SetError(this.labelNewAsset, resources.GetString("labelNewAsset.Error"));
            this.labelNewAsset.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.labelNewAsset, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelNewAsset.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelNewAsset, ((int)(resources.GetObject("labelNewAsset.IconPadding"))));
            this.labelNewAsset.Name = "labelNewAsset";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // textBoxAssetName
            // 
            resources.ApplyResources(this.textBoxAssetName, "textBoxAssetName");
            this.errorProvider1.SetError(this.textBoxAssetName, resources.GetString("textBoxAssetName.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAssetName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAssetName.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAssetName, ((int)(resources.GetObject("textBoxAssetName.IconPadding"))));
            this.textBoxAssetName.Name = "textBoxAssetName";
            this.textBoxAssetName.TextChanged += new System.EventHandler(this.TextBoxAssetName_TextChanged);
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.errorProvider1.SetError(this.textBoxDescription, resources.GetString("textBoxDescription.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxDescription.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxDescription, ((int)(resources.GetObject("textBoxDescription.IconPadding"))));
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // textBoxAltId
            // 
            resources.ApplyResources(this.textBoxAltId, "textBoxAltId");
            this.errorProvider1.SetError(this.textBoxAltId, resources.GetString("textBoxAltId.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxAltId, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxAltId.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxAltId, ((int)(resources.GetObject("textBoxAltId.IconPadding"))));
            this.textBoxAltId.Name = "textBoxAltId";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // textBoxContainer
            // 
            resources.ApplyResources(this.textBoxContainer, "textBoxContainer");
            this.errorProvider1.SetError(this.textBoxContainer, resources.GetString("textBoxContainer.Error"));
            this.errorProvider1.SetIconAlignment(this.textBoxContainer, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("textBoxContainer.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.textBoxContainer, ((int)(resources.GetObject("textBoxContainer.IconPadding"))));
            this.textBoxContainer.Name = "textBoxContainer";
            this.textBoxContainer.TextChanged += new System.EventHandler(this.TextBoxContainer_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider1.SetError(this.label4, resources.GetString("label4.Error"));
            this.errorProvider1.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider1.SetError(this.label5, resources.GetString("label5.Error"));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider1.SetError(this.label6, resources.GetString("label6.Error"));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.errorProvider1.SetError(this.label7, resources.GetString("label7.Error"));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
            this.label7.Name = "label7";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // NewAsset
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxContainer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxAltId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxAssetName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelNewAsset);
            this.Controls.Add(this.labelStorage);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.panel1);
            this.Name = "NewAsset";
            this.Load += new System.EventHandler(this.NewAsset_Load);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.NewAsset_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStorage;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.Label labelNewAsset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAltId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxContainer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}