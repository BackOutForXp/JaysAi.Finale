// Monarch v1.0 – ClipboardManager.cs
using System;
using System.Windows;

namespace JaysAi.Finale.Utility
{
    public static class ClipboardManager
    {
        public static void CopyText(string text)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    Clipboard.SetText(text);
                }
            }
            catch (Exception ex)
            {
                // Clipboard is locked or unavailable
                Logger.Log($"Clipboard copy failed: {ex.Message}");
            }
        }

        public static string GetText()
        {
            try
            {
                return Clipboard.ContainsText() ? Clipboard.GetText() : string.Empty;
            }
            catch (Exception ex)
            {
                Logger.Log($"Clipboard read failed: {ex.Message}");
                return string.Empty;
            }
        }

        public static void Clear()
        {
            try
            {
                Clipboard.Clear();
            }
            catch (Exception ex)
            {
                Logger.Log($"Clipboard clear failed: {ex.Message}");
            }
        }
    }
}
