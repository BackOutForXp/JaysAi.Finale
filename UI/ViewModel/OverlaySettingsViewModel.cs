// neural v3.0
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JaysAi.Finale.UI.ViewModels
{
    public class OverlaySettingsViewModel : INotifyPropertyChanged
    {
        private bool _isOverlayVisible = true;
        private float _overlayOpacity = 0.85f;
        private string _activeTheme = "Dark";

        public bool IsOverlayVisible
        {
            get => _isOverlayVisible;
            set
            {
                if (_isOverlayVisible != value)
                {
                    _isOverlayVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public float OverlayOpacity
        {
            get => _overlayOpacity;
            set
            {
                if (_overlayOpacity != value)
                {
                    _overlayOpacity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ActiveTheme
        {
            get => _activeTheme;
            set
            {
                if (_activeTheme != value)
                {
                    _activeTheme = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Reset()
        {
            IsOverlayVisible = true;
            OverlayOpacity = 0.85f;
            ActiveTheme = "Dark";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
