// neural v3.0
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace JaysAi.Finale.Utility
{
    public sealed class IniFile
    {
        private readonly string _filePath;
        private readonly Dictionary<string, Dictionary<string, string>> _data;

        public IniFile(string filePath)
        {
            _filePath = filePath;
            _data = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

            if (File.Exists(filePath))
                Load();
        }

        private void Load()
        {
            string? currentSection = null;
            foreach (var line in File.ReadLines(_filePath))
            {
                var trimmed = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith(";") || trimmed.StartsWith("#"))
                    continue;

                if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                {
                    currentSection = trimmed[1..^1];
                    if (!_data.ContainsKey(currentSection))
                        _data[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                else if (currentSection != null)
                {
                    var match = Regex.Match(trimmed, @"^([^=]+)=(.*)$");
                    if (match.Success)
                    {
                        var key = match.Groups[1].Value.Trim();
                        var value = match.Groups[2].Value.Trim();
                        _data[currentSection][key] = value;
                    }
                }
            }
        }

        public string? Read(string section, string key, string? defaultValue = null)
        {
            if (_data.TryGetValue(section, out var sectionDict) &&
                sectionDict.TryGetValue(key, out var value))
                return value;

            return defaultValue;
        }

        public void Write(string section, string key, string value)
        {
            if (!_data.ContainsKey(section))
                _data[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            _data[section][key] = value;
        }

        public void Save()
        {
            var sb = new StringBuilder();
            foreach (var section in _data)
            {
                sb.AppendLine($"[{section.Key}]");
                foreach (var kvp in section.Value)
                {
                    sb.AppendLine($"{kvp.Key}={kvp.Value}");
                }
                sb.AppendLine();
            }

            File.WriteAllText(_filePath, sb.ToString());
        }

        public bool SectionExists(string section) => _data.ContainsKey(section);

        public bool KeyExists(string section, string key) =>
            _data.TryGetValue(section, out var sectionDict) && sectionDict.ContainsKey(key);
    }
}
