
namespace AMSExplorer.AMSLogin
{
    partial class EmbeddedBrowserFormUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmbeddedBrowserFormUI));
            this.webBrowser = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowser)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.CreationProperties = null;
            this.webBrowser.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ZoomFactor = 1D;
            this.webBrowser.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webBrowser_CoreWebView2InitializationCompleted);
            this.webBrowser.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.webBrowser_NavigationStarting);
            // 
            // EmbeddedBrowserFormUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser);
            this.Name = "EmbeddedBrowserFormUI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmbeddedBrowserUI_FormClosed);
            this.Load += new System.EventHandler(this.EmbeddedBrowserUI_Load);
            this.Shown += new System.EventHandler(this.EmbeddedBrowserUI_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.webBrowser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webBrowser;
    }
}