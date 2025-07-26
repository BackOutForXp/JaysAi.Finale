//neural v3.0
using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Integration
{
    public sealed class TitanHelper : IDisposable
    {
        private SerialPort? _serialPort;
        private readonly object _lock = new();
        private bool _connected;

        public bool IsConnected => _connected;

        public TitanHelper(string portName = "COM3", int baudRate = 115200)
        {
            try
            {
                _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
                {
                    Handshake = Handshake.None,
                    Encoding = Encoding.ASCII,
                    ReadTimeout = 500,
                    WriteTimeout = 500
                };

                _serialPort.DataReceived += OnDataReceived;
                _serialPort.Open();

                _connected = true;
                Logger.Info($"[TitanHelper] Connected to Titan Two on {portName}.");
            }
            catch (Exception ex)
            {
                Logger.Error($"[TitanHelper] Connection failed: {ex.Message}");
                _connected = false;
            }
        }

        public void SendCommand(string command)
        {
            if (!_connected || _serialPort == null)
                return;

            lock (_lock)
            {
                try
                {
                    _serialPort.WriteLine(command);
                    Logger.Debug($"[TitanHelper] Sent: {command}");
                }
                catch (Exception ex)
                {
                    Logger.Warn($"[TitanHelper] Send failed: {ex.Message}");
                }
            }
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var line = _serialPort?.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Logger.Debug($"[TitanHelper] Received: {line.Trim()}");
                    // You can route this to another event system or trigger logic
                }
            }
            catch (Exception ex)
            {
                Logger.Warn($"[TitanHelper] Data read failed: {ex.Message}");
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_serialPort != null)
                {
                    try
                    {
                        _serialPort.DataReceived -= OnDataReceived;
                        if (_serialPort.IsOpen)
                            _serialPort.Close();

                        _serialPort.Dispose();
                        Logger.Info("[TitanHelper] Disconnected.");
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn($"[TitanHelper] Dispose failed: {ex.Message}");
                    }

                    _serialPort = null;
                }

                _connected = false;
            }
        }
    }
}
