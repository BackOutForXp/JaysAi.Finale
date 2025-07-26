// neural v3.0
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JaysAi.Finale.Settings;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.UI.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private string _statusMessage = "Initializing...";
        private string _currentGame = "None";
        private bool _loaderReady;
        private readonly ObservableCollection<string> _recentLogs = new();

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CurrentGame
        {
            get => _currentGame;
            set
            {
                if (_currentGame != value)
                {
                    _currentGame = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool LoaderReady
        {
            get => _loaderReady;
            set
            {
                if (_loaderReady != value)
                {
                    _loaderReady = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> RecentLogs => _recentLogs;

        public void AddLog(string message)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (_recentLogs.Count > 50)
                    _recentLogs.RemoveAt(0);

                _recentLogs.Add($"[{DateTime.Now:T}] {message}");
            });
        }

        public void SyncWithSystemState(SystemStatus status)
        {
            StatusMessage = status.LoaderState;
            CurrentGame = status.DetectedGame;
            LoaderReady = status.IsReady;
        }

        public void ClearLogs()
        {
            App.Current.Dispatcher.Invoke(() => _recentLogs.Clear());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
