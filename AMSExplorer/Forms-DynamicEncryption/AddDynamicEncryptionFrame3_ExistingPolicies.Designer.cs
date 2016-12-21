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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDynamicEncryptionFrame3_ExistingPolicies));
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
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Name = "panel1";
            // 
            // groupBoxAuthPol
            // 
            this.groupBoxAuthPol.Controls.Add(this.TextBoxAutPolicyId);
            this.groupBoxAuthPol.Controls.Add(this.buttonUseExistingAutpolicy);
            resources.ApplyResources(this.groupBoxAuthPol, "groupBoxAuthPol");
            this.groupBoxAuthPol.Name = "groupBoxAuthPol";
            this.groupBoxAuthPol.TabStop = false;
            // 
            // TextBoxAutPolicyId
            // 
            resources.ApplyResources(this.TextBoxAutPolicyId, "TextBoxAutPolicyId");
            this.TextBoxAutPolicyId.Name = "TextBoxAutPolicyId";
            this.TextBoxAutPolicyId.ReadOnly = true;
            // 
            // buttonUseExistingAutpolicy
            // 
            resources.ApplyResources(this.buttonUseExistingAutpolicy, "buttonUseExistingAutpolicy");
            this.buttonUseExistingAutpolicy.Name = "buttonUseExistingAutpolicy";
            this.buttonUseExistingAutpolicy.UseVisualStyleBackColor = true;
            this.buttonUseExistingAutpolicy.Click += new System.EventHandler(this.buttonUseExistingAutpolicy_Click);
            // 
            // groupBoxKeyAcqUrl
            // 
            resources.ApplyResources(this.groupBoxKeyAcqUrl, "groupBoxKeyAcqUrl");
            this.groupBoxKeyAcqUrl.Controls.Add(this.textBoxDelPolId);
            this.groupBoxKeyAcqUrl.Controls.Add(this.buttonExistingDelPol);
            this.groupBoxKeyAcqUrl.Name = "groupBoxKeyAcqUrl";
            this.groupBoxKeyAcqUrl.TabStop = false;
            // 
            // textBoxDelPolId
            // 
            resources.ApplyResources(this.textBoxDelPolId, "textBoxDelPolId");
            this.textBoxDelPolId.Name = "textBoxDelPolId";
            this.textBoxDelPolId.ReadOnly = true;
            // 
            // buttonExistingDelPol
            // 
            resources.ApplyResources(this.buttonExistingDelPol, "buttonExistingDelPol");
            this.buttonExistingDelPol.Name = "buttonExistingDelPol";
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
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBoxKeyAcqUrl);
            this.Controls.Add(this.groupBoxAuthPol);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "AddDynamicEncryptionFrame3_ExistingPolicies";
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