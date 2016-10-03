namespace AMSExplorer
{
    partial class AddDynamicEncryptionFrame3_ExistingPolicies
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxAuthPol = new System.Windows.Forms.GroupBox();
            this.TextBoxAutPolicyId = new System.Windows.Forms.TextBox();
            this.buttonUseExistingAutpolicy = new System.Windows.Forms.Button();
            this.groupBoxKeyAcqUrl = new System.Windows.Forms.GroupBox();
            this.textBoxDelPolId = new System.Windows.Forms.TextBox();
            this.buttonExistingDelPol = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBoxAuthPol.SuspendLayout();
            this.groupBoxKeyAcqUrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(386, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(176, 27);
            this.buttonOk.TabIndex = 17;
            this.buttonOk.Text = "Next";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(569, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 27);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 42);
            this.label1.TabIndex = 49;
            this.label1.Text = "Step 3\r\nSelect existing policies";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Location = new System.Drawing.Point(-1, 688);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 55);
            this.panel1.TabIndex = 51;
            // 
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Controls.Add(this.TextBoxAutPolicyId);
            this.groupBoxAuthPol.Controls.Add(this.buttonUseExistingAutpolicy);
            this.groupBoxAuthPol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAuthPol.Location = new System.Drawing.Point(14, 96);
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.Size = new System.Drawing.Size(645, 144);
            this.groupBoxAuthPol.TabIndex = 52;
            this.groupBoxAuthPol.TabStop = false;
            this.groupBoxAuthPol.Text = "Content Key Authorization Policy";
            // 
            // TextBoxAutPolicyId
            // 
            this.TextBoxAutPolicyId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxAutPolicyId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxAutPolicyId.Location = new System.Drawing.Point(16, 70);
            this.TextBoxAutPolicyId.Multiline = true;
            this.TextBoxAutPolicyId.Name = "TextBoxAutPolicyId";
            this.TextBoxAutPolicyId.ReadOnly = true;
            this.TextBoxAutPolicyId.Size = new System.Drawing.Size(612, 27);
            this.TextBoxAutPolicyId.TabIndex = 96;
            this.TextBoxAutPolicyId.Text = "(no policy selected)";
            // 
            // buttonUseExistingAutpolicy
            // 
            this.buttonUseExistingAutpolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUseExistingAutpolicy.Location = new System.Drawing.Point(16, 37);
            this.buttonUseExistingAutpolicy.Name = "buttonUseExistingAutpolicy";
            this.buttonUseExistingAutpolicy.Size = new System.Drawing.Size(146, 27);
            this.buttonUseExistingAutpolicy.TabIndex = 95;
            this.buttonUseExistingAutpolicy.Text = "Select policy...";
            this.buttonUseExistingAutpolicy.UseVisualStyleBackColor = true;
            this.buttonUseExistingAutpolicy.Click += new System.EventHandler(this.buttonUseExistingAutpolicy_Click);
            // 
            // groupBoxKeyAcqUrl
            // 
            this.groupBoxKeyAcqUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeyAcqUrl.Controls.Add(this.textBoxDelPolId);
            this.groupBoxKeyAcqUrl.Controls.Add(this.buttonExistingDelPol);
            this.groupBoxKeyAcqUrl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxKeyAcqUrl.Location = new System.Drawing.Point(14, 260);
            this.groupBoxKeyAcqUrl.Name = "groupBoxKeyAcqUrl";
            this.groupBoxKeyAcqUrl.Size = new System.Drawing.Size(645, 125);
            this.groupBoxKeyAcqUrl.TabIndex = 78;
            this.groupBoxKeyAcqUrl.TabStop = false;
            this.groupBoxKeyAcqUrl.Text = "Delivery Policy";
            // 
            // textBoxDelPolId
            // 
            this.textBoxDelPolId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDelPolId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDelPolId.Location = new System.Drawing.Point(16, 65);
            this.textBoxDelPolId.Multiline = true;
            this.textBoxDelPolId.Name = "textBoxDelPolId";
            this.textBoxDelPolId.ReadOnly = true;
            this.textBoxDelPolId.Size = new System.Drawing.Size(612, 27);
            this.textBoxDelPolId.TabIndex = 98;
            this.textBoxDelPolId.Text = "(no policy selected)";
            // 
            // buttonExistingDelPol
            // 
            this.buttonExistingDelPol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExistingDelPol.Location = new System.Drawing.Point(16, 32);
            this.buttonExistingDelPol.Name = "buttonExistingDelPol";
            this.buttonExistingDelPol.Size = new System.Drawing.Size(146, 27);
            this.buttonExistingDelPol.TabIndex = 97;
            this.buttonExistingDelPol.Text = "Select policy...";
            this.buttonExistingDelPol.UseVisualStyleBackColor = true;
            this.buttonExistingDelPol.Click += new System.EventHandler(this.buttonExistingDelPol_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddDynamicEncryptionFrame3_ExistingPolicies
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(681, 741);
            this.Controls.Add(this.groupBoxKeyAcqUrl);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddDynamicEncryptionFrame3_ExistingPolicies";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamic Encryption - Step 3";
            this.Load += new System.EventHandler(this.SetupDynEnc_Load);
            this.panel1.ResumeLayout(false);
            this.groupBoxAuthPol.ResumeLayout(false);
            this.groupBoxAuthPol.PerformLayout();
            this.groupBoxKeyAcqUrl.ResumeLayout(false);
            this.groupBoxKeyAcqUrl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxAuthPol;
        private System.Windows.Forms.GroupBox groupBoxKeyAcqUrl;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox TextBoxAutPolicyId;
        private System.Windows.Forms.Button buttonUseExistingAutpolicy;
        private System.Windows.Forms.TextBox textBoxDelPolId;
        private System.Windows.Forms.Button buttonExistingDelPol;
    }
}