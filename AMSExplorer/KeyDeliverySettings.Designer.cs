namespace AMSExplorer
{
    partial class KeyDeliverySettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyDeliverySettings));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonAttach = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewIP = new System.Windows.Forms.DataGridView();
            this.labelAssetCopy = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelStreamingAllowedIP = new System.Windows.Forms.Panel();
            this.buttonAddIP = new System.Windows.Forms.Button();
            this.buttonDelIP = new System.Windows.Forms.Button();
            this.checkBoxUseIpList = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).BeginInit();
            this.panelStreamingAllowedIP.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAttach
            // 
            resources.ApplyResources(this.buttonAttach, "buttonAttach");
            this.buttonAttach.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAttach.Name = "buttonAttach";
            this.toolTip1.SetToolTip(this.buttonAttach, resources.GetString("buttonAttach.ToolTip"));
            this.buttonAttach.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonAttach);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Name = "button1";
            this.toolTip1.SetToolTip(this.button1, resources.GetString("button1.ToolTip"));
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewIP
            // 
            resources.ApplyResources(this.dataGridViewIP, "dataGridViewIP");
            this.dataGridViewIP.AllowUserToAddRows = false;
            this.dataGridViewIP.AllowUserToDeleteRows = false;
            this.dataGridViewIP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewIP.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewIP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewIP.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewIP.Name = "dataGridViewIP";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewIP.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewIP.RowHeadersVisible = false;
            this.dataGridViewIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip1.SetToolTip(this.dataGridViewIP, resources.GetString("dataGridViewIP.ToolTip"));
            // 
            // labelAssetCopy
            // 
            resources.ApplyResources(this.labelAssetCopy, "labelAssetCopy");
            this.labelAssetCopy.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelAssetCopy.Name = "labelAssetCopy";
            this.toolTip1.SetToolTip(this.labelAssetCopy, resources.GetString("labelAssetCopy.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // panelStreamingAllowedIP
            // 
            resources.ApplyResources(this.panelStreamingAllowedIP, "panelStreamingAllowedIP");
            this.panelStreamingAllowedIP.Controls.Add(this.dataGridViewIP);
            this.panelStreamingAllowedIP.Controls.Add(this.buttonAddIP);
            this.panelStreamingAllowedIP.Controls.Add(this.buttonDelIP);
            this.panelStreamingAllowedIP.Controls.Add(this.checkBoxUseIpList);
            this.panelStreamingAllowedIP.Name = "panelStreamingAllowedIP";
            this.toolTip1.SetToolTip(this.panelStreamingAllowedIP, resources.GetString("panelStreamingAllowedIP.ToolTip"));
            // 
            // buttonAddIP
            // 
            resources.ApplyResources(this.buttonAddIP, "buttonAddIP");
            this.buttonAddIP.Name = "buttonAddIP";
            this.toolTip1.SetToolTip(this.buttonAddIP, resources.GetString("buttonAddIP.ToolTip"));
            this.buttonAddIP.UseVisualStyleBackColor = true;
            this.buttonAddIP.Click += new System.EventHandler(this.buttonAddIP_Click);
            // 
            // buttonDelIP
            // 
            resources.ApplyResources(this.buttonDelIP, "buttonDelIP");
            this.buttonDelIP.Name = "buttonDelIP";
            this.toolTip1.SetToolTip(this.buttonDelIP, resources.GetString("buttonDelIP.ToolTip"));
            this.buttonDelIP.UseVisualStyleBackColor = true;
            this.buttonDelIP.Click += new System.EventHandler(this.buttonDelIP_Click);
            // 
            // checkBoxUseIpList
            // 
            resources.ApplyResources(this.checkBoxUseIpList, "checkBoxUseIpList");
            this.checkBoxUseIpList.Name = "checkBoxUseIpList";
            this.toolTip1.SetToolTip(this.checkBoxUseIpList, resources.GetString("checkBoxUseIpList.ToolTip"));
            this.checkBoxUseIpList.UseVisualStyleBackColor = true;
            this.checkBoxUseIpList.CheckedChanged += new System.EventHandler(this.checkBoxUseIpList_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // KeyDeliverySettings
            // 
            this.AcceptButton = this.buttonAttach;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelStreamingAllowedIP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelAssetCopy);
            this.Controls.Add(this.panel1);
            this.Name = "KeyDeliverySettings";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.KeyDeliverySettings_Load);
            this.Shown += new System.EventHandler(this.AttachStorage_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.AttachStorage_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIP)).EndInit();
            this.panelStreamingAllowedIP.ResumeLayout(false);
            this.panelStreamingAllowedIP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAttach;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelAssetCopy;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelStreamingAllowedIP;
        private System.Windows.Forms.DataGridView dataGridViewIP;
        private System.Windows.Forms.Button buttonAddIP;
        private System.Windows.Forms.Button buttonDelIP;
        private System.Windows.Forms.CheckBox checkBoxUseIpList;
        private System.Windows.Forms.Label label1;
    }
}