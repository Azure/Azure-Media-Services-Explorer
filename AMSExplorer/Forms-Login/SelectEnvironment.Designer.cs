namespace AMSExplorer.Forms_Login
{
    partial class SelectEnvironment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectEnvironment));
            label6 = new System.Windows.Forms.Label();
            panelEnv = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            comboBoxAADMappingList = new System.Windows.Forms.ComboBox();
            panel1 = new System.Windows.Forms.Panel();
            buttonCancel = new System.Windows.Forms.Button();
            buttonNext = new System.Windows.Forms.Button();
            panelEnv.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.ForeColor = System.Drawing.Color.DarkBlue;
            label6.Name = "label6";
            // 
            // panelEnv
            // 
            panelEnv.Controls.Add(label1);
            panelEnv.Controls.Add(comboBoxAADMappingList);
            resources.ApplyResources(panelEnv, "panelEnv");
            panelEnv.Name = "panelEnv";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // comboBoxAADMappingList
            // 
            comboBoxAADMappingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxAADMappingList.FormattingEnabled = true;
            resources.ApplyResources(comboBoxAADMappingList, "comboBoxAADMappingList");
            comboBoxAADMappingList.Name = "comboBoxAADMappingList";
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonNext);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            resources.ApplyResources(buttonNext, "buttonNext");
            buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonNext.Name = "buttonNext";
            buttonNext.UseVisualStyleBackColor = true;
            // 
            // SelectEnvironment
            // 
            AcceptButton = buttonNext;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            Controls.Add(panel1);
            Controls.Add(panelEnv);
            Controls.Add(label6);
            Name = "SelectEnvironment";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            Load += SelectEnvironment_Load;
            panelEnv.ResumeLayout(false);
            panelEnv.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelEnv;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBoxAADMappingList;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonNext;
    }
}