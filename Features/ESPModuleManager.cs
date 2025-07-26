// Neural v3.1 — ESPModuleManager.cs
using JaysAi.Finale.Data;
using JaysAi.Finale.Modules;
using JaysAi.Finale.Overlay;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Utility;
using JaysAi.Finale.Visuals;
using System;
using System.Collections.Generic;
using System.Timers;

namespace JaysAi.Finale.Features
{
    public class ESPModuleManager : IModule
    {
        private bool _isEnabled;
        private int _toggleCount;
        private DateTime _lastToggle;
        private readonly System.Timers.Timer _optimizationTimer;

        public string Name => "ESP";

        public bool IsEnabled => _isEnabled;

        // Events for UI binding or advanced AI logic
        public event Action<bool> OnStateChanged;
        public event Action<string, bool> OnAutoOptimized;

        public ESPModuleManager()
        {
            _toggleCount = 0;
            _lastToggle = DateTime.Now;

            _optimizationTimer = new System.Timers.Timer(5000); // Check every 5 seconds
            _optimizationTimer.Elapsed += (_, _) => AnalyzeUsage();
            _optimizationTimer.AutoReset = true;
        }

        public void Initialize()
        {
            ESP.Initialize();
            SetEnabled(UserSettings.Current.EnableESP);
        }

        public void Enable()
        {
            if (!_isEnabled)
            {
                SetEnabled(true);
                _toggleCount++;
                _lastToggle = DateTime.Now;
                _optimizationTimer.Start();
            }
        }

        public void Disable()
        {
            if (_isEnabled)
            {
                SetEnabled(false);
                ESP.Clear(); // Clear tracked ESP objects
                _optimizationTimer.Stop();
            }
        }

        public void ToggleESP(bool enabled)
        {
            SetEnabled(enabled);
            Logger.Log($"ESP toggled {(enabled ? "on" : "off")}");
        }

        private void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
            UserSettings.Current.EnableESP = enabled;
            ESP.SetEnabled(enabled);
            OnStateChanged?.Invoke(enabled);
        }

        private void AnalyzeUsage()
        {
            if (_toggleCount > 7 && (DateTime.Now - _lastToggle).TotalSeconds < 20)
            {
                SetEnabled(false);
                OnAutoOptimized?.Invoke(Name, false);
                Logger.Log("ESP auto-disabled due to rapid toggling.");
                _optimizationTimer.Stop();
            }
        }

        public void Update()
        {
            if (!_isEnabled) return;

            List<Enemy> enemies = EntityCache.GetVisibleEnemies();
            ESP.UpdateObjects(enemies);
        }

        public void OnGUI()
        {
            if (!_isEnabled) return;

            ESPDrawer.RenderAll();
        }

        public void Shutdown()
        {
            SetEnabled(false);
            ESP.Clear();
        }

        public bool Active => _isEnabled;
    }
}
