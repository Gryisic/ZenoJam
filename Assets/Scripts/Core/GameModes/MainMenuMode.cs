using System;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class MainMenuMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private GameContext _gameContext;
        private SceneSwitcher _sceneSwitcher;

        public MainMenuMode(GameContext gameContext) 
        {
            _gameContext = gameContext;
            _sceneSwitcher = _gameContext.Resolve<SceneSwitcher>();
        }

        public void Activate(GameModeArgs args)
        {

        }

        public void Deactivate() 
        {

        }

        private void OnStartButtonClicked()
        {
            _sceneSwitcher.ChangeScene(SceneType.Prologue);
            Finished?.Invoke(new GameModeArgs(GameModeType.SceneSwitchMode));
        }
    }
}