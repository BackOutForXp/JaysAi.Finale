// neural v3.0
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JaysAi.Finale.UI.ViewModels
{
    public class LoaderViewModel : INotifyPropertyChanged
    {
        private string _version = "v0.0";
        private bool _isInjected;
        private string _status = "Idle";

        public string Version
        {
            get => _version;
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsInjected
        {
            get => _isInjected;
            set
            {
                if (_isInjected != value)
                {
                    _isInjected = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Reset()
        {
            Version = "v0.0";
            IsInjected = false;
            Status = "Idle";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
