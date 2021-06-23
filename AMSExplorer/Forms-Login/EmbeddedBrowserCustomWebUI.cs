using Microsoft.Identity.Client.Extensibility;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer.AMSLogin
{
    public class EmbeddedBrowserCustomWebUI : ICustomWebUi
    {
        public const int DefaultWindowWidth = 600;
        public const int DefaultWindowHeight = 800;

        private readonly Form _owner;
        private readonly string _title;
        private readonly int _windowWidth;
        private readonly int _windowHeight;
        private readonly FormStartPosition _windowStartupLocation;

        public EmbeddedBrowserCustomWebUI(Form owner,
            string title = "Sign in",
            int windowWidth = DefaultWindowWidth,
            int windowHeight = DefaultWindowHeight,
            FormStartPosition windowStartupLocation = FormStartPosition.CenterParent)
        {
            //_owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _title = title;
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
            _windowStartupLocation = windowStartupLocation;
            _owner = owner;
        }


        public Task<Uri> AcquireAuthorizationCodeAsync(Uri authorizationUri, Uri redirectUri, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<Uri>();

            _owner.Invoke((Action)(() =>
            {
                new EmbeddedBrowserFormUI(authorizationUri,
                  redirectUri,
                  tcs,
                  cancellationToken)
                {
                    Text = _title,
                    Width = _windowWidth,
                    Height = _windowHeight,
                    StartPosition = _windowStartupLocation
                }.ShowDialog();
            }));

            /*
            _owner.Dispatcher.Invoke(() =>
            {
                new EmbeddedBrowserUI(authorizationUri,
                    redirectUri,
                    tcs,
                    cancellationToken)
                {
                    Owner = _owner,
                    Text = _title,
                    Width = _windowWidth,
                    Height = _windowHeight,
                    StartPosition = _windowStartupLocation
                }.ShowDialog();
            });
            */

            /*

            new EmbeddedBrowserFormUI(authorizationUri,
                redirectUri,
                tcs,
                cancellationToken)
            {
                Text = _title,
                Width = _windowWidth,
                Height = _windowHeight,
                StartPosition = _windowStartupLocation
            }.ShowDialog();

            */


            return tcs.Task;
        }
    }
}
