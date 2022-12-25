using System;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Core
{
    public class SceneSwitchMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private SceneSwitcher _sceneSwitcher;

        public SceneSwitchMode(GameContext context) => _sceneSwitcher = context.Resolve<SceneSwitcher>();

        public void Activate(GameModeArgs args) => _sceneSwitcher.SceneChanged += OnSceneChanged;

        public void Deactivate() => _sceneSwitcher.SceneChanged -= OnSceneChanged;

        private void OnSceneChanged(SceneContext obj) => 
            Finished?.Invoke(new GameModeArgs(Utils.Enums.GameModeType.GameplayMode));
    }
}