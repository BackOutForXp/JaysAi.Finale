//monarch v2.1
using SkiaSharp;
using JaysAi.AI;

namespace JaysAi.Finale.Visuals
{
    public class ESPSettings
    {
        public bool ShowBoxes { get; set; } = true;
        public bool ShowSnaplines { get; set; } = true;
        public bool ShowHealthBars { get; set; } = true;
        public bool ShowIcons { get; set; } = true;

        public bool ShowEnemies { get; set; } = true;
        public bool ShowSquad { get; set; } = false;
        public bool ShowTeammates { get; set; } = false;

        public float ScreenCenterX { get; set; }
        public float ScreenCenterY { get; set; }

        public bool ShouldDisplay(EntityType type)
        {
            return type switch
            {
                EntityType.Enemy => ShowEnemies,
                EntityType.Squad => ShowSquad,
                EntityType.Teammate => ShowTeammates,
                _ => false
            };
        }

        public SKColor GetColorForType(EntityType type)
        {
            return type switch
            {
                EntityType.Enemy => SKColors.Red,
                EntityType.Squad => SKColors.Orange,
                EntityType.Teammate => SKColors.Blue,
                _ => SKColors.White
            };
        }
    }
}
