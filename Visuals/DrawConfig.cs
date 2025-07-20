//monarch v2.1
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.Visuals
{
    public class DrawConfig
    {
        public bool DrawBoxes { get; set; } = true;
        public bool DrawNames { get; set; } = true;
        public bool DrawHealthBars { get; set; } = true;
        public bool DrawDistance { get; set; } = false;
        public bool DrawSnapLines { get; set; } = false;

        public string EnemyBoxColorHex { get; set; } = "#FF0000"; // Red
        public string AllyBoxColorHex { get; set; } = "#00FF00"; // Green
        public string SquadBoxColorHex { get; set; } = "#0000FF"; // Blue

        public float LineThickness { get; set; } = 2.0f;
        public float BoxAlpha { get; set; } = 1.0f; // 1 = solid, 0 = invisible
        public float FontSize { get; set; } = 14.0f;

        public Dictionary<string, bool> AdvancedToggles { get; set; } = new()
        {
            { "ShowOnlyVisible", true },
            { "FadeDistantTargets", false },
            { "HighlightLowestHealth", false },
        };

        public string GetColorByTeam(string team)
        {
            return team switch
            {
                "enemy" => EnemyBoxColorHex,
                "ally" => AllyBoxColorHex,
                "squad" => SquadBoxColorHex,
                _ => "#FFFFFF"
            };
        }
    }
}
