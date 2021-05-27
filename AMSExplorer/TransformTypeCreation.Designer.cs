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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformTypeCreation));
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.radioButtonEncoding = new System.Windows.Forms.RadioButton();
            this.radioButtonAVAnalyze = new System.Windows.Forms.RadioButton();
            this.radioButtonFaceDetection = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.labelNoAssetFilter = new System.Windows.Forms.Label();
            this.radioButtonCustomJson = new System.Windows.Forms.RadioButton();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonImport);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelTitle.Name = "labelTitle";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // radioButtonEncoding
            // 
            resources.ApplyResources(this.radioButtonEncoding, "radioButtonEncoding");
            this.radioButtonEncoding.Checked = true;
            this.radioButtonEncoding.Name = "radioButtonEncoding";
            this.radioButtonEncoding.TabStop = true;
            this.radioButtonEncoding.UseVisualStyleBackColor = true;
            // 
            // radioButtonAVAnalyze
            // 
            resources.ApplyResources(this.radioButtonAVAnalyze, "radioButtonAVAnalyze");
            this.radioButtonAVAnalyze.Name = "radioButtonAVAnalyze";
            this.radioButtonAVAnalyze.TabStop = true;
            this.radioButtonAVAnalyze.UseVisualStyleBackColor = true;
            // 
            // radioButtonFaceDetection
            // 
            resources.ApplyResources(this.radioButtonFaceDetection, "radioButtonFaceDetection");
            this.radioButtonFaceDetection.Name = "radioButtonFaceDetection";
            this.radioButtonFaceDetection.TabStop = true;
            this.radioButtonFaceDetection.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.encoding;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.face_detector;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.index;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // labelNoAssetFilter
            // 
            this.labelNoAssetFilter.ForeColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.labelNoAssetFilter, "labelNoAssetFilter");
            this.labelNoAssetFilter.Name = "labelNoAssetFilter";
            // 
            // radioButtonCustomJson
            // 
            resources.ApplyResources(this.radioButtonCustomJson, "radioButtonCustomJson");
            this.radioButtonCustomJson.Name = "radioButtonCustomJson";
            this.radioButtonCustomJson.TabStop = true;
            this.radioButtonCustomJson.UseVisualStyleBackColor = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::AMSExplorer.Bitmaps.rename;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // TransformTypeCreation
            // 
            this.AcceptButton = this.buttonImport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.radioButtonCustomJson);
            this.Controls.Add(this.labelNoAssetFilter);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.radioButtonFaceDetection);
            this.Controls.Add(this.radioButtonAVAnalyze);
            this.Controls.Add(this.radioButtonEncoding);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.Name = "TransformTypeCreation";
            this.Load += new System.EventHandler(this.TransformTypeCreation_Load);
            this.Shown += new System.EventHandler(this.TransformTypeCreation_Shown);
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.TransformTypeCreation_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RadioButton radioButtonFaceDetection;
        private System.Windows.Forms.RadioButton radioButtonAVAnalyze;
        private System.Windows.Forms.RadioButton radioButtonEncoding;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelNoAssetFilter;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RadioButton radioButtonCustomJson;
    }
}