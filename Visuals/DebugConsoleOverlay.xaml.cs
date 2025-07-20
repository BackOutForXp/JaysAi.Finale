using System.Text;
using System.Windows.Controls;

namespace JaysAi.Finale.Visuals
{
    public partial class DebugConsoleOverlay : UserControl
    {
        private readonly StringBuilder _logBuilder = new();

        public DebugConsoleOverlay()
        {
            InitializeComponent();
        }

        public void Log(string message)
        {
            if (_logBuilder.Length > 2000)
                _logBuilder.Clear(); // prevent overflow

            _logBuilder.AppendLine($"[{System.DateTime.Now:HH:mm:ss}] {message}");
            LogText.Text = _logBuilder.ToString();
        }
    }
}
