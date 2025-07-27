// Neural v3.1 — OverlayModuleToggle.cs
namespace JaysAi.Finale.Overlay
{
    public class OverlayModuleToggle
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public IOverlayRenderer Renderer { get; set; }

        public OverlayModuleToggle(string name, IOverlayRenderer renderer, bool isEnabled = true)
        {
            Name = name;
            Renderer = renderer;
            IsEnabled = isEnabled;
        }
    }
}
