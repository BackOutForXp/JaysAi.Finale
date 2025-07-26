// Neural v3.0 — OverlayConsole.cs
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace JaysAi.Finale.Overlay
{
    public class OverlayConsole
    {
        private readonly TextBlock _targetTextBlock;
        private readonly DispatcherTimer _flushTimer;
        private readonly ConcurrentQueue<string> _logBuffer;
        private readonly StringBuilder _currentText;

        public bool IsVisible { get; private set; } = true;
        public int MaxLines { get; set; } = 100;

        public OverlayConsole(TextBlock targetTextBlock)
        {
            _targetTextBlock = targetTextBlock ?? throw new ArgumentNullException(nameof(targetTextBlock));

            _logBuffer = new ConcurrentQueue<string>();
            _currentText = new StringBuilder();

            _flushTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _flushTimer.Tick += FlushLogs;
            _flushTimer.Start();
        }

        public void Log(string message)
        {
            string timestamp = $"[{DateTime.Now:HH:mm:ss}] ";
            _logBuffer.Enqueue($"{timestamp}{message}");
        }

        private void FlushLogs(object sender, EventArgs e)
        {
            while (_logBuffer.TryDequeue(out string line))
            {
                _currentText.AppendLine(line);

                // Trim old lines
                string[] lines = _currentText.ToString().Split('\n');
                if (lines.Length > MaxLines)
                {
                    _currentText.Clear();
                    int start = lines.Length - MaxLines;
                    for (int i = start; i < lines.Length; i++)
                        _currentText.AppendLine(lines[i]);
                }
            }

            _targetTextBlock.Text = _currentText.ToString();
        }

        public void Clear()
        {
            _currentText.Clear();
            _targetTextBlock.Text = string.Empty;
        }

        public void SetVisibility(bool visible)
        {
            IsVisible = visible;
            _targetTextBlock.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public void ToggleVisibility() => SetVisibility(!IsVisible);
    }
}
