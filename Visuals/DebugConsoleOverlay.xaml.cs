// Neural v3.0 — DebugConsoleOverlay.xaml.cs
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JaysAi.Finale.Overlay
{
    public partial class DebugConsoleOverlay : UserControl
    {
        public DebugConsoleOverlay()
        {
            InitializeComponent();
            SetVisibility(true);
            AppendText(">> Debug Console Initialized");
        }

        public void AppendText(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            string timestamp = $"[{DateTime.Now:HH:mm:ss}] ";
            DebugText.Text += $"{timestamp}{message}\n";

            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            if (VisualTreeHelper.GetChild(this, 0) is Grid grid &&
                grid.Children[0] is Border border &&
                border.Child is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToEnd();
            }
        }

        public void SetVisibility(bool visible)
        {
            this.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public void ClearLog()
        {
            DebugText.Text = string.Empty;
        }

        public bool IsVisible => this.Visibility == Visibility.Visible;
    }
}
