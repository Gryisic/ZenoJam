using UnityEngine;
using ZenoJam.Infrastructure;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private IGameMode _currentMode;
        private IGameMode[] _gameModes;

        private void Awake()
        {
            _gameContext.Construct();

            _gameModes = new IGameMode[]
            {
                new GameInitMode(_gameContext),
                new GameplayMode(_gameContext),
                new DialogueMode(_gameContext),
                new SceneSwitchMode(_gameContext),
                new CutsceneMode(_gameContext),
                new PauseMode(_gameContext),
                new MainMenuMode(_gameContext)
            };
        }

        private void Start()
        {
            ChangeActiveGameMode(new GameModeArgs(GameModeType.GameInitMode));
        }

        private void OnEnable()
        {
            foreach (var mode in _gameModes) 
            {
                mode.Finished += OnGameModeFinished;
            }
        }

        private void OnDisable()
        {
            foreach (var mode in _gameModes)
            {
                mode.Finished -= OnGameModeFinished;
            }
        }

        private void OnDestroy()
        {
            foreach (var mode in _gameModes)
            {
                mode.Dispose();
            }

            _gameContext.Dispose();
        }

        private void OnGameModeFinished(GameModeArgs args) 
        {
            ChangeActiveGameMode(args);
        }

        private void ChangeActiveGameMode(GameModeArgs args) 
        {
            _currentMode?.Deactivate();
            _currentMode?.Reset();
            _currentMode = _gameModes[(int) args.NewMode];
            _currentMode.Activate(args);
        }
    }
}