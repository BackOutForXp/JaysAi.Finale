// File: Modules/BoneVisualizer.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.Settings;
using JaysAi.Finale.Visuals;
using System.Collections.Generic;
using System.Numerics;

namespace JaysAi.Finale.Modules
{
    public class BoneVisualizer
    {
        private readonly AppSettings _settings;
        private readonly ESPDrawer _drawer;

        public bool IsEnabled => _settings.EnableBoneESP;

        public BoneVisualizer(AppSettings settings, ESPDrawer drawer)
        {
            _settings = settings;
            _drawer = drawer;
        }

        public void Draw(List<Enemy> enemies)
        {
            if (!IsEnabled) return;

            _drawer.Clear();

            foreach (var enemy in enemies)
            {
                if (!enemy.IsVisible || enemy.Bones == null || !enemy.Bones.HasValidBones)
                    continue;

                BoneData bones = enemy.Bones;

                // Draw lines between bones
                DrawLine(bones.Head, bones.Chest);
                DrawLine(bones.Chest, bones.Stomach);
                DrawLine(bones.Stomach, bones.Feet);
            }

            _drawer.Render();
        }

        private void DrawLine(Vector3 from, Vector3 to)
        {
            var from2D = new Vector2(from.X, from.Y);
            var to2D = new Vector2(to.X, to.Y);

            _drawer.DrawLine(from2D, to2D, _settings.BoneColor);
        }
    }
}
