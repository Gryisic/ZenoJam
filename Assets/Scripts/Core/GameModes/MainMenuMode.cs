using System;
using UnityEngine;
using UnityEngine.Audio;
using ZenoJam.Common;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class MainMenuMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private GameContext _gameContext;
        private SceneContext _sceneContext;
        private StartButton _startButton;
        private ExitButton _exitButton;
        private SceneSwitcher _sceneSwitcher;
        private VolumeSlider _volumeSlider;

        public MainMenuMode(GameContext gameContext) 
        {
            _gameContext = gameContext;
            _sceneSwitcher = _gameContext.Resolve<SceneSwitcher>();
        }

        public void Activate(GameModeArgs args)
        {
            _sceneContext = _gameContext.Resolve<SceneContext>();
            _startButton = _sceneContext.Resolve<StartButton>();
            _exitButton = _sceneContext. Resolve<ExitButton>();
            _volumeSlider = _sceneContext.Resolve<VolumeSlider>();

            _volumeSlider.SetMixer(_gameContext.Resolve<AudioMixer>());

            _startButton.Clicked += OnStartButtonClicked;
            _exitButton.Clicked += OnExitButtonClicked;
        }

        public void Deactivate() 
        {
            _startButton.Clicked -= OnStartButtonClicked;
            _exitButton.Clicked -= OnExitButtonClicked;
        }

        private void OnStartButtonClicked()
        {
            _sceneSwitcher.ChangeScene(SceneType.Prologue);
            Finished?.Invoke(new GameModeArgs(GameModeType.SceneSwitchMode));
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}