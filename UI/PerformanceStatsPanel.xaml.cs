// Neural v3.0 — PerformanceStatsPanel.xaml.cs
using System.Windows.Controls;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.UI
{
    public partial class PerformanceStatsPanel : UserControl
    {
        public OverlayDebugState DebugState { get; set; }

        public PerformanceStatsPanel()
        {
            InitializeComponent();
            DebugState = new OverlayDebugState();
            this.DataContext = this;
        }

        public void UpdateDebugInfo(OverlayDebugState newState)
        {
            DebugState.IsRendering = newState.IsRendering;
            DebugState.IsConnected = newState.IsConnected;
            DebugState.CurrentFPS = newState.CurrentFPS;
            DebugState.FrameBufferSize = newState.FrameBufferSize;
            DebugState.StatusMessage = newState.StatusMessage;
        }
    }
}
