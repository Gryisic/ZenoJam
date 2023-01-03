using Cysharp.Threading.Tasks;
using System;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class GameInitMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private SceneSwitcher _sceneSwitcher;

        public GameInitMode(GameContext context) 
        {
            _sceneSwitcher = context.Resolve<SceneSwitcher>();
        }

        public void Activate(GameModeArgs args)
        {
            ChangeSceneAsync().Forget();

            _sceneSwitcher.SceneChanged += OnSceneChanged;
        }

        public void Deactivate() 
        {
            _sceneSwitcher.SceneChanged -= OnSceneChanged;
        }

        private void OnSceneChanged(SceneContext obj)
        {
            Finished?.Invoke(new GameModeArgs(GameModeType.GameplayMode));
        }

        private async UniTask ChangeSceneAsync() 
        {
            await _sceneSwitcher.ChangeSceneAsync(SceneType.Prologue);
        }
    }
}