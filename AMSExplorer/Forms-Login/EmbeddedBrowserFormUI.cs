using Microsoft.Identity.Client.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Microsoft.Identity.Client;
using Microsoft.Web.WebView2.WinForms;

namespace AMSExplorer.AMSLogin
{

    public partial class EmbeddedBrowserFormUI : Form
    {
        private readonly Uri _authorizationUri;
        private readonly Uri _redirectUri;
        private readonly TaskCompletionSource<Uri> _taskCompletionSource;
        private readonly CancellationToken _cancellationToken;
        private CancellationTokenRegistration _token;

        public EmbeddedBrowserFormUI(Uri authorizationUri, Uri redirectUri, TaskCompletionSource<Uri> taskCompletionSource, CancellationToken cancellationToken)
        {
            InitializeComponent();
            _authorizationUri = authorizationUri;
            _redirectUri = redirectUri;
            _taskCompletionSource = taskCompletionSource;
            _cancellationToken = cancellationToken;
        }

        private void EmbeddedBrowserUI_Load(object sender, EventArgs e)
        {
            _token = _cancellationToken.Register(() => _taskCompletionSource.SetCanceled());

            //webBrowser.Source = _authorizationUri;
            // navigating to an uri that is entry point to authorization flow.
            //

        }

        private void EmbeddedBrowserUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            _taskCompletionSource.TrySetCanceled();
            webBrowser.Dispose();
            _token.Dispose();
        }



        private async void EmbeddedBrowserUI_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);

            _token = _cancellationToken.Register(() => _taskCompletionSource.SetCanceled());
            // navigating to an uri that is entry point to authorization flow.

            await webBrowser.EnsureCoreWebView2Async();
            webBrowser.Source = _authorizationUri;
        }


        private void webBrowser_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            //webBrowser.CoreWebView2.Navigate(_authorizationUri.ToString());
            // if (init)
            // webBrowser.NavigateToString(_authorizationUri.ToString());
        }

        private void webBrowser_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            Uri eUri = new(e.Uri);

            if (!e.Uri.ToString().StartsWith(_redirectUri.ToString()))
            {
                // not redirect uri case
                return;
            }

            // parse query string
            var query = HttpUtility.ParseQueryString(eUri.Query);
            if (query.AllKeys.Any(x => x == "code"))
            {
                // It has a code parameter.
                if (_taskCompletionSource.Task.Status != TaskStatus.RanToCompletion)
                    _taskCompletionSource.SetResult(eUri);
            }
            else
            {
                // error.
                _taskCompletionSource.SetException(
                    new MsalException(
                        $"An error occurred, error: {query.Get("error")}, error_description: {query.Get("error_description")}"));
            }

            this.Hide();
            this.Close();
        }

        // ref code https://techcommunity.microsoft.com/t5/windows-dev-appconsult/how-to-use-embedded-web-ui-of-msal-net-on-wpf-on-net-core/ba-p/1315024

    }
}
