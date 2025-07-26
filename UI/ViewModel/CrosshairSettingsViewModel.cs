// neural v3.0
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JaysAi.Finale.Settings;

namespace JaysAi.Finale.UI.ViewModel
{
    public class CrosshairSettingsViewModel : INotifyPropertyChanged
    {
        private bool _showCrosshair;
        private string _crosshairColor = "#FF0000";
        private double _crosshairSize = 6.0;

        public bool ShowCrosshair
        {
            get => _showCrosshair;
            set
            {
                if (_showCrosshair != value)
                {
                    _showCrosshair = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CrosshairColor
        {
            get => _crosshairColor;
            set
            {
                if (_crosshairColor != value)
                {
                    _crosshairColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public double CrosshairSize
        {
            get => _crosshairSize;
            set
            {
                if (_crosshairSize != value)
                {
                    _crosshairSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public void LoadFrom(UserSettings settings)
        {
            ShowCrosshair = settings.ShowCrosshair;
            CrosshairColor = settings.CrosshairColor;
            CrosshairSize = settings.CrosshairSize;
        }

        public void ApplyTo(UserSettings settings)
        {
            settings.ShowCrosshair = ShowCrosshair;
            settings.CrosshairColor = CrosshairColor;
            settings.CrosshairSize = CrosshairSize;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
