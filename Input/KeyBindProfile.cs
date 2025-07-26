// neural v3.0
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaysAi.Finale.Input
{
    public class KeybindProfile
    {
        [JsonPropertyName("profileName")]
        public string ProfileName { get; set; } = "Default";

        [JsonPropertyName("bindings")]
        public Dictionary<string, InputBinding> Bindings { get; set; } = new();

        public KeybindProfile() { }

        public KeybindProfile(string profileName)
        {
            ProfileName = profileName;
        }

        public void AddOrUpdateBinding(string actionName, InputBinding binding)
        {
            if (Bindings.ContainsKey(actionName))
                Bindings[actionName] = binding;
            else
                Bindings.Add(actionName, binding);
        }

        public bool TryGetBinding(string actionName, out InputBinding binding)
        {
            return Bindings.TryGetValue(actionName, out binding!);
        }

        public void RemoveBinding(string actionName)
        {
            Bindings.Remove(actionName);
        }

        public void ClearBindings()
        {
            Bindings.Clear();
        }

        public KeybindProfile Clone()
        {
            return new KeybindProfile(ProfileName + "_Copy")
            {
                Bindings = new Dictionary<string, InputBinding>(Bindings)
            };
        }
    }
}
