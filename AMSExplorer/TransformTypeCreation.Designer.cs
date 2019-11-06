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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonImport
            // 
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.errorProvider1.SetError(this.buttonImport, resources.GetString("buttonImport.Error"));
            this.errorProvider1.SetIconAlignment(this.buttonImport, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("buttonImport.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.buttonImport, ((int)(resources.GetObject("buttonImport.IconPadding"))));
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.UseVisualStyleBackColor = true;
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
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonImport);
            this.errorProvider1.SetError(this.panel1, resources.GetString("panel1.Error"));
            this.errorProvider1.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
            this.panel1.Name = "panel1";
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.errorProvider1.SetError(this.labelTitle, resources.GetString("labelTitle.Error"));
            this.labelTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.errorProvider1.SetIconAlignment(this.labelTitle, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelTitle.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelTitle, ((int)(resources.GetObject("labelTitle.IconPadding"))));
            this.labelTitle.Name = "labelTitle";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // radioButtonEncoding
            // 
            resources.ApplyResources(this.radioButtonEncoding, "radioButtonEncoding");
            this.radioButtonEncoding.Checked = true;
            this.errorProvider1.SetError(this.radioButtonEncoding, resources.GetString("radioButtonEncoding.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonEncoding, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonEncoding.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonEncoding, ((int)(resources.GetObject("radioButtonEncoding.IconPadding"))));
            this.radioButtonEncoding.Name = "radioButtonEncoding";
            this.radioButtonEncoding.TabStop = true;
            this.radioButtonEncoding.UseVisualStyleBackColor = true;
            // 
            // radioButtonAVAnalyze
            // 
            resources.ApplyResources(this.radioButtonAVAnalyze, "radioButtonAVAnalyze");
            this.errorProvider1.SetError(this.radioButtonAVAnalyze, resources.GetString("radioButtonAVAnalyze.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonAVAnalyze, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonAVAnalyze.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonAVAnalyze, ((int)(resources.GetObject("radioButtonAVAnalyze.IconPadding"))));
            this.radioButtonAVAnalyze.Name = "radioButtonAVAnalyze";
            this.radioButtonAVAnalyze.UseVisualStyleBackColor = true;
            // 
            // radioButtonFaceDetection
            // 
            resources.ApplyResources(this.radioButtonFaceDetection, "radioButtonFaceDetection");
            this.errorProvider1.SetError(this.radioButtonFaceDetection, resources.GetString("radioButtonFaceDetection.Error"));
            this.errorProvider1.SetIconAlignment(this.radioButtonFaceDetection, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("radioButtonFaceDetection.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.radioButtonFaceDetection, ((int)(resources.GetObject("radioButtonFaceDetection.IconPadding"))));
            this.radioButtonFaceDetection.Name = "radioButtonFaceDetection";
            this.radioButtonFaceDetection.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.errorProvider1.SetError(this.pictureBox2, resources.GetString("pictureBox2.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBox2, ((int)(resources.GetObject("pictureBox2.IconPadding"))));
            this.pictureBox2.Image = global::AMSExplorer.Bitmaps.encoding;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.errorProvider1.SetError(this.pictureBox1, resources.GetString("pictureBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBox1, ((int)(resources.GetObject("pictureBox1.IconPadding"))));
            this.pictureBox1.Image = global::AMSExplorer.Bitmaps.face_detector;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.errorProvider1.SetError(this.pictureBox3, resources.GetString("pictureBox3.Error"));
            this.errorProvider1.SetIconAlignment(this.pictureBox3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.pictureBox3, ((int)(resources.GetObject("pictureBox3.IconPadding"))));
            this.pictureBox3.Image = global::AMSExplorer.Bitmaps.index;
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // labelNoAssetFilter
            // 
            resources.ApplyResources(this.labelNoAssetFilter, "labelNoAssetFilter");
            this.errorProvider1.SetError(this.labelNoAssetFilter, resources.GetString("labelNoAssetFilter.Error"));
            this.labelNoAssetFilter.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.errorProvider1.SetIconAlignment(this.labelNoAssetFilter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("labelNoAssetFilter.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.labelNoAssetFilter, ((int)(resources.GetObject("labelNoAssetFilter.IconPadding"))));
            this.labelNoAssetFilter.Name = "labelNoAssetFilter";
            // 
            // TransformTypeCreation
            // 
            this.AcceptButton = this.buttonImport;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
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
            this.DpiChanged += new System.Windows.Forms.DpiChangedEventHandler(this.TransformTypeCreation_DpiChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
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
    }
}