//monarch v2.1 – Dynamic Snap-to-Target Module
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Utility;
using System.Windows;

namespace JaysAi.Finale.Aimbot
{
    public static class SnapAssist
    {
        public static void LockOn(DetectionObject target)
        {
            if (target == null) return;

            var targetCenter = new Point(
                target.X + target.Width / 2,
                target.Y + target.Height / 2
            );

            var screenCenter = new Point(
                ScreenHelper.CenterX,
                ScreenHelper.CenterY
            );

            var offsetX = targetCenter.X - screenCenter.X;
            var offsetY = targetCenter.Y - screenCenter.Y;

            // Sensitivity-adjusted snapping
            var moveX = (int)(offsetX * ConfigManager.SnapSensitivity);
            var moveY = (int)(offsetY * ConfigManager.SnapSensitivity);

            InputInjector.MoveMouseRelative(moveX, moveY);
        }
    }
}
