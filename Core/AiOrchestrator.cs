//monarch v2.0
using System.Collections.Generic;
using JaysAi.AI;
using JaysAi.SystemLogic;
using JaysAi.Finale.AI;
using JaysAi.Finale.Input;
using JaysAi.Finale.Visuals;

namespace JaysAi.Core
{
    public class AiOrchestrator
    {
        private readonly ModelLoader _modelLoader;
        private readonly YOLOBridge _yoloBridge;
        private readonly ESPModule _espModule;
        private readonly PredictionEngine _predictionEngine;
        private readonly InputInjector _inputInjector;
        private readonly ControllerInputState _inputState;
        private readonly OverlayDrawer _overlayDrawer;

        public AiOrchestrator(
            ModelLoader modelLoader,
            YOLOBridge yoloBridge,
            ESPModule espModule,
            PredictionEngine predictionEngine,
            InputInjector inputInjector,
            ControllerInputState inputState,
            OverlayDrawer overlayDrawer)
        {
            _modelLoader = modelLoader;
            _yoloBridge = yoloBridge;
            _espModule = espModule;
            _predictionEngine = predictionEngine;
            _inputInjector = inputInjector;
            _inputState = inputState;
            _overlayDrawer = overlayDrawer;
        }

        public void Tick(byte[] screenFrame)
        {
            if (!_modelLoader.ModelReady)
                return;

            var rawDetections = _modelLoader.RunDetection(screenFrame);
            var parsedTargets = _yoloBridge.ParseDetections(rawDetections);

            _espModule.UpdateTargets(parsedTargets);
            var bestTarget = _predictionEngine.GetBestTarget(_espModule.GetTargets());

            _overlayDrawer.Clear();
            foreach (var target in parsedTargets)
            {
                _overlayDrawer.AddBox(target.ScreenX, target.ScreenY, target.Width, target.Height, target.Label);
            }

            if (bestTarget != null && _inputState.IsADS)
            {
                var predicted = _predictionEngine.PredictAim(bestTarget);
                _inputInjector.InjectOffset(predicted.X, predicted.Y);
            }
            else
            {
                _inputInjector.Reset();
            }
        }
    }
}
