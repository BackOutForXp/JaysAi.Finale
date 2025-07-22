// heavenly v3.0 – Visual ESP Module (Hitbox, Box, Health, Name)
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.Features
{
    public class ESP
    {
        public bool EnableBoxESP { get; set; } = true;
        public bool EnableHealthBar { get; set; } = true;
        public bool EnableNameTag { get; set; } = true;

        public Brush BoxColor { get; set; } = Brushes.Red;
        public Brush HealthColor { get; set; } = Brushes.Green;
        public Brush TextColor { get; set; } = Brushes.White;

        public void DrawEntities(DrawingContext dc, List<ESPObject> entities)
        {
            foreach (var entity in entities)
            {
                if (EnableBoxESP)
                    DrawBoundingBox(dc, entity);

                if (EnableHealthBar)
                    DrawHealthBar(dc, entity);

                if (EnableNameTag)
                    DrawNameTag(dc, entity);
            }
        }

        private void DrawBoundingBox(DrawingContext dc, ESPObject entity)
        {
            Pen boxPen = new Pen(BoxColor, 1.5);
            Rect box = new Rect(entity.Position.X, entity.Position.Y, entity.Width, entity.Height);
            dc.DrawRectangle(null, boxPen, box);
        }

        private void DrawHealthBar(DrawingContext dc, ESPObject entity)
        {
            double healthHeight = entity.Height * (entity.Health / 100.0);
            Rect bar = new Rect(entity.Position.X - 6, entity.Position.Y + (entity.Height - healthHeight), 4, healthHeight);
            dc.DrawRectangle(HealthColor, null, bar);
        }

        private void DrawNameTag(DrawingContext dc, ESPObject entity)
        {
            FormattedText text = OverlayTextBuilder.Build(entity.Name, TextColor, 12);
            dc.DrawText(text, new Point(entity.Position.X + entity.Width / 2 - text.Width / 2, entity.Position.Y - 16));
        }
    }
}
