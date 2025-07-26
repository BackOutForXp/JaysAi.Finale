// neural v3.0
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using JaysAi.Finale.SystemLogic;
using System;
using System.Collections.Generic;

namespace JaysAi.Finale.AI
{
    public class CaptureEngine
    {
        public event Action<FrameSnapshot> OnFrameCaptured;
        private readonly IEnemyProvider _enemyProvider;
        private readonly IFrameSource _frameSource;

        public CaptureEngine(IEnemyProvider enemyProvider, IFrameSource frameSource)
        {
            _enemyProvider = enemyProvider;
            _frameSource = frameSource;
        }

        public void CaptureNextFrame()
        {
            var snapshot = new FrameSnapshot
            {
                Timestamp = DateTime.UtcNow,
                Enemies = _enemyProvider.GetVisibleEnemies(),
                PlayerPosition = GameMemory.GetLocalPlayerPosition(),
                PlayerViewAngles = GameMemory.GetViewAngles(),
                CameraFOV = GameMemory.GetFieldOfView(),
                FrameData = _frameSource.CaptureFrame()
            };

            OnFrameCaptured?.Invoke(snapshot);
        }
    }
}
