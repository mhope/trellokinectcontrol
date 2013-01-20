using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrelloKinectControl.Kinect;

namespace TrelloKinectControl
{
    public partial class BrowserForm : Form
    {
        private readonly WebView webView;
        private KinectControl kinectControl;
        public BrowserForm()
        {
            InitializeComponent();

            var CefSettings = new CefSharp.Settings()
            {
                PackLoadingDisabled = true
            };

            do
            {
                CefSharp.CEF.Initialize(CefSettings);
            }
            while (!CefSharp.CEF.IsInitialized);

            webView = new WebView("https://trello.com/board/kinect-development/508d9bbfb3b2cd791f005e32", new BrowserSettings());
            webView.Dock = DockStyle.Fill;
            browserContainer.ContentPanel.Controls.Add(webView);

            kinectControl = new KinectControl();
        }

        private void onOffButton_Click(object sender, EventArgs e)
        {
            if (onOffButton.Text == "Start")
            {
                onOffButton.Text = "Stop";
                onOffButton.BackColor = Color.Red;
                kinectControl.Start();
            }
            else
            {
                onOffButton.Text = "Start";
                onOffButton.BackColor = Color.FromArgb(0, 192, 0);
                kinectControl.Stop();
            }
        }

    }
}
