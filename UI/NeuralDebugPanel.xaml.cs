using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;
using JaysAi.Finale.AI;

namespace JaysAi.Finale.UI
{
    public partial class NeuralDebugPanel : UserControl
    {
        private readonly DispatcherTimer _refreshTimer;
        private readonly ObservableCollection<TargetProfile> _targetProfiles = new();
        private readonly TargetProfileManager _profileManager;

        public NeuralDebugPanel(TargetProfileManager profileManager)
        {
            InitializeComponent();
            _profileManager = profileManager;

            TargetList.ItemsSource = _targetProfiles;

            _refreshTimer = new DispatcherTimer
            {
                Interval = System.TimeSpan.FromMilliseconds(1000)
            };

            _refreshTimer.Tick += (s, e) => RefreshProfiles();
            _refreshTimer.Start();
        }

        private void RefreshProfiles()
        {
            _targetProfiles.Clear();

            foreach (var profile in _profileManager.GetAll())
            {
                _targetProfiles.Add(profile);
            }
        }
    }
}
