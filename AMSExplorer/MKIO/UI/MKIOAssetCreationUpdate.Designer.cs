﻿namespace AMSExplorer
{
    partial class MKIOAssetCreationUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MKIOAssetCreationUpdate));
            buttonCancel = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            labelNewAsset = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            textBoxAssetName = new System.Windows.Forms.TextBox();
            textBoxDescription = new System.Windows.Forms.TextBox();
            lblDescription = new System.Windows.Forms.Label();
            textBoxContainer = new System.Windows.Forms.TextBox();
            lblContainer = new System.Windows.Forms.Label();
            lblOptional = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            pictureBox1 = new System.Windows.Forms.PictureBox();
            textBoxStorage = new System.Windows.Forms.TextBox();
            lblStorage = new System.Windows.Forms.Label();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            textBoxInstructions = new System.Windows.Forms.TextBox();
            checkBoxCloneClearLocator = new System.Windows.Forms.CheckBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            checkBoxRecreateKeys = new System.Windows.Forms.CheckBox();
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
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // textBoxAssetName
            // 
            resources.ApplyResources(textBoxAssetName, "textBoxAssetName");
            textBoxAssetName.Name = "textBoxAssetName";
            toolTip1.SetToolTip(textBoxAssetName, resources.GetString("textBoxAssetName.ToolTip"));
            textBoxAssetName.TextChanged += TextBoxAssetName_TextChanged;
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(textBoxDescription, "textBoxDescription");
            textBoxDescription.Name = "textBoxDescription";
            toolTip1.SetToolTip(textBoxDescription, resources.GetString("textBoxDescription.ToolTip"));
            // 
            // lblDescription
            // 
            resources.ApplyResources(lblDescription, "lblDescription");
            lblDescription.Name = "lblDescription";
            // 
            // textBoxContainer
            // 
            resources.ApplyResources(textBoxContainer, "textBoxContainer");
            textBoxContainer.Name = "textBoxContainer";
            textBoxContainer.TextChanged += TextBoxContainer_TextChanged;
            // 
            // lblContainer
            // 
            resources.ApplyResources(lblContainer, "lblContainer");
            lblContainer.Name = "lblContainer";
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
            pictureBox1.Image = Bitmaps.MKIO_Default;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // textBoxStorage
            // 
            resources.ApplyResources(textBoxStorage, "textBoxStorage");
            textBoxStorage.Name = "textBoxStorage";
            // 
            // lblStorage
            // 
            resources.ApplyResources(lblStorage, "lblStorage");
            lblStorage.Name = "lblStorage";
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
            // textBoxInstructions
            // 
            textBoxInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(textBoxInstructions, "textBoxInstructions");
            textBoxInstructions.Name = "textBoxInstructions";
            textBoxInstructions.ReadOnly = true;
            // 
            // checkBoxCloneClearLocator
            // 
            resources.ApplyResources(checkBoxCloneClearLocator, "checkBoxCloneClearLocator");
            checkBoxCloneClearLocator.Checked = true;
            checkBoxCloneClearLocator.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxCloneClearLocator.Name = "checkBoxCloneClearLocator";
            toolTip1.SetToolTip(checkBoxCloneClearLocator, resources.GetString("checkBoxCloneClearLocator.ToolTip"));
            checkBoxCloneClearLocator.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecreateKeys
            // 
            resources.ApplyResources(checkBoxRecreateKeys, "checkBoxRecreateKeys");
            checkBoxRecreateKeys.Name = "checkBoxRecreateKeys";
            toolTip1.SetToolTip(checkBoxRecreateKeys, resources.GetString("checkBoxRecreateKeys.ToolTip"));
            checkBoxRecreateKeys.UseVisualStyleBackColor = true;
            // 
            // MKIOAssetCreationUpdate
            // 
            AcceptButton = buttonOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(checkBoxRecreateKeys);
            Controls.Add(checkBoxCloneClearLocator);
            Controls.Add(textBoxStorage);
            Controls.Add(lblStorage);
            Controls.Add(pictureBox1);
            Controls.Add(lblOptional);
            Controls.Add(textBoxContainer);
            Controls.Add(lblContainer);
            Controls.Add(textBoxDescription);
            Controls.Add(lblDescription);
            Controls.Add(textBoxAssetName);
            Controls.Add(label1);
            Controls.Add(labelNewAsset);
            Controls.Add(panel1);
            Controls.Add(textBoxInstructions);
            Name = "MKIOAssetCreationUpdate";
            Load += AssetCreationUpdate_Load;
            Shown += AssetCreationUpdate_Shown;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAssetName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox textBoxContainer;
        private System.Windows.Forms.Label lblContainer;
        private System.Windows.Forms.Label lblOptional;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxStorage;
        private System.Windows.Forms.Label lblStorage;
        public System.Windows.Forms.Label labelNewAsset;
        private System.Windows.Forms.TextBox textBoxInstructions;
        private System.Windows.Forms.CheckBox checkBoxCloneClearLocator;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxRecreateKeys;
    }
}