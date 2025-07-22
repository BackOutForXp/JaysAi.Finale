// heavenly v3.0
using JaysAi.Finale.Loader;
using JaysAi.Finale.SystemLogic;
using JaysAi.Loader;
using System;
using System.Windows;
using System.Windows.Input;

namespace JaysAi.Finale
{
    public partial class MainWindow : Window
    {
        private readonly MainControlBridge _mainBridge;

        public MainWindow()
        {
            InitializeComponent();

            _mainBridge = new MainControlBridge(MainContentGrid, StatusLabel);
            _mainBridge.LoadDefaultUI();

            this.MouseLeftButtonDown += Window_MouseLeftButtonDown;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void UpdateStatus(string status, bool isError = false)
        {
            Dispatcher.Invoke(() =>
            {
                StatusLabel.Text = status;
                StatusLabel.Foreground = isError ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.Lime;
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            AppState.Shutdown();
        }
    }
}
