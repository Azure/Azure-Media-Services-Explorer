namespace AMSExplorer
{
    partial class TransformTypeCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformTypeCreation));
            buttonImport = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            radioButtonEncoding = new System.Windows.Forms.RadioButton();
            radioButtonAVAnalyze = new System.Windows.Forms.RadioButton();
            pictureBoxEncoding = new System.Windows.Forms.PictureBox();
            pictureBoxAVAnalyze = new System.Windows.Forms.PictureBox();
            labelNoAssetFilter = new System.Windows.Forms.Label();
            radioButtonCustomJson = new System.Windows.Forms.RadioButton();
            pictureBoxCustomJson = new System.Windows.Forms.PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEncoding).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAVAnalyze).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCustomJson).BeginInit();
            SuspendLayout();
            // 
            // buttonImport
            // 
            resources.ApplyResources(buttonImport, "buttonImport");
            buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonImport.Name = "buttonImport";
            buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(buttonCancel, "buttonCancel");
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Name = "buttonCancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.Control;
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonImport);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // labelTitle
            // 
            resources.ApplyResources(labelTitle, "labelTitle");
            labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            labelTitle.Name = "labelTitle";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // radioButtonEncoding
            // 
            resources.ApplyResources(radioButtonEncoding, "radioButtonEncoding");
            radioButtonEncoding.Checked = true;
            radioButtonEncoding.Name = "radioButtonEncoding";
            radioButtonEncoding.TabStop = true;
            radioButtonEncoding.UseVisualStyleBackColor = true;
            // 
            // radioButtonAVAnalyze
            // 
            resources.ApplyResources(radioButtonAVAnalyze, "radioButtonAVAnalyze");
            radioButtonAVAnalyze.Name = "radioButtonAVAnalyze";
            radioButtonAVAnalyze.TabStop = true;
            radioButtonAVAnalyze.UseVisualStyleBackColor = true;
            // 
            // pictureBoxEncoding
            // 
            resources.ApplyResources(pictureBoxEncoding, "pictureBoxEncoding");
            pictureBoxEncoding.Name = "pictureBoxEncoding";
            pictureBoxEncoding.TabStop = false;
            // 
            // pictureBoxAVAnalyze
            // 
            resources.ApplyResources(pictureBoxAVAnalyze, "pictureBoxAVAnalyze");
            pictureBoxAVAnalyze.Name = "pictureBoxAVAnalyze";
            pictureBoxAVAnalyze.TabStop = false;
            // 
            // labelNoAssetFilter
            // 
            labelNoAssetFilter.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(labelNoAssetFilter, "labelNoAssetFilter");
            labelNoAssetFilter.Name = "labelNoAssetFilter";
            // 
            // radioButtonCustomJson
            // 
            resources.ApplyResources(radioButtonCustomJson, "radioButtonCustomJson");
            radioButtonCustomJson.Name = "radioButtonCustomJson";
            radioButtonCustomJson.TabStop = true;
            radioButtonCustomJson.UseVisualStyleBackColor = true;
            // 
            // pictureBoxCustomJson
            // 
            resources.ApplyResources(pictureBoxCustomJson, "pictureBoxCustomJson");
            pictureBoxCustomJson.Name = "pictureBoxCustomJson";
            pictureBoxCustomJson.TabStop = false;
            // 
            // TransformTypeCreation
            // 
            AcceptButton = buttonImport;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = buttonCancel;
            Controls.Add(pictureBoxCustomJson);
            Controls.Add(radioButtonCustomJson);
            Controls.Add(labelNoAssetFilter);
            Controls.Add(pictureBoxAVAnalyze);
            Controls.Add(pictureBoxEncoding);
            Controls.Add(radioButtonAVAnalyze);
            Controls.Add(radioButtonEncoding);
            Controls.Add(labelTitle);
            Controls.Add(panel1);
            Name = "TransformTypeCreation";
            Load += TransformTypeCreation_Load;
            Shown += TransformTypeCreation_Shown;
            DpiChanged += TransformTypeCreation_DpiChanged;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEncoding).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAVAnalyze).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCustomJson).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RadioButton radioButtonAVAnalyze;
        private System.Windows.Forms.RadioButton radioButtonEncoding;
        private System.Windows.Forms.PictureBox pictureBoxAVAnalyze;
        private System.Windows.Forms.PictureBox pictureBoxEncoding;
        private System.Windows.Forms.Label labelNoAssetFilter;
        private System.Windows.Forms.PictureBox pictureBoxCustomJson;
        private System.Windows.Forms.RadioButton radioButtonCustomJson;
    }
}