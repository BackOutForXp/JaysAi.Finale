// Neural v3.1 — WeaponDataManager.cs
using JaysAi.Finale.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JaysAi.Finale.Helpers
{
    public static class WeaponDataManager
    {
        private static readonly string _weaponDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "weapons.json");
        private static readonly Dictionary<string, WeaponStat> _weapons = new();

        public static void Load()
        {
            if (!File.Exists(_weaponDataPath))
            {
                LogManager.Warn($"Weapon data file not found at {_weaponDataPath}.");
                return;
            }

            try
            {
                string json = File.ReadAllText(_weaponDataPath);
                var list = JsonSerializer.Deserialize<List<WeaponStat>>(json);

                _weapons.Clear();

                if (list != null)
                {
                    foreach (var weapon in list)
                    {
                        if (!string.IsNullOrWhiteSpace(weapon.Name))
                            _weapons[weapon.Name] = weapon;
                    }

                    LogManager.Log($"Loaded {list.Count} weapon profiles.");
                }
            }
            catch (Exception ex)
            {
                LogManager.Error($"Failed to load weapon data: {ex.Message}");
            }
        }

        public static WeaponStat? Get(string weaponName)
        {
            return _weapons.TryGetValue(weaponName, out var stat) ? stat : null;
        }

        public static IReadOnlyDictionary<string, WeaponStat> GetAll() => _weapons;
    }
}
