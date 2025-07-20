// Monarch v1.0 – ESPCheckboxBinder.cs
// ✅ Monarch Fix Checklist
// [x] Monitors WPF checkbox state
// [x] Starts/stops ESP overlay window
// [x] Safe toggle and thread guard

using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace JaysAi.Finale.Visuals
{
    public class ESPCheckboxBinder
    {
        private readonly CheckBox _espCheckbox;
        private Thread _espThread;
        private OverlayHost _overlayHost;

        public ESPCheckboxBinder(CheckBox checkbox)
        {
            _espCheckbox = checkbox;
            _espCheckbox.Checked += ToggleESP;
            _espCheckbox.Unchecked += ToggleESP;
        }

        private void ToggleESP(object sender, EventArgs e)
        {
            if (_espCheckbox.IsChecked == true)
            {
                StartESP();
            }
            else
            {
                StopESP();
            }
        }

        private void StartESP()
        {
            if (_espThread != null && _espThread.IsAlive) return;

            _espThread = new Thread(() =>
            {
                _overlayHost = new OverlayHost();
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    _overlayHost.Show();
                });
                System.Windows.Threading.Dispatcher.Run();
            });

            _espThread.SetApartmentState(ApartmentState.STA);
            _espThread.IsBackground = true;
            _espThread.Start();
        }

        private void StopESP()
        {
            if (_overlayHost != null)
            {
                _overlayHost.Invoke(new Action(() =>
                {
                    _overlayHost.Close();
                }));
            }

            _espThread?.Interrupt();
            _espThread = null;
            _
