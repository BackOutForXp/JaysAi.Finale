//monarch v1.0
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace JaysAi.Finale.Utility
{
    public class INIFile
    {
        private readonly string _path;

        public INIFile(string iniPath)
        {
            _path = iniPath;
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder returnValue, int size, string filePath);

        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        public string ReadValue(string section, string key, string defaultValue = "")
        {
            var returnValue = new StringBuilder(512);
            GetPrivateProfileString(section, key, defaultValue, returnValue, returnValue.Capacity, _path);
            return returnValue.ToString();
        }

        public void DeleteKey(string section, string key)
        {
            WriteValue(section, key, null);
        }

        public void DeleteSection(string section)
        {
            WriteValue(section, null, null);
        }

        public bool KeyExists(string section, string key)
        {
            return !string.IsNullOrEmpty(ReadValue(section, key));
        }
    }
}
