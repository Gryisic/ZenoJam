using System;
using UnityEngine;
using UnityEngine.Audio;
using ZenoJam.Common;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class PauseMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private GameContext _gameContext;
        private SceneContext _sceneContext;
        private Input _input;
        private ExitButton _exitButton;
        private VolumeSlider _volumeSlider;

        public PauseMode(GameContext context) 
        {
            _gameContext = context;
            _input = _gameContext.Resolve<Input>();
        }

        public void Activate(GameModeArgs args)
        {
            _sceneContext = _gameContext.Resolve<SceneContext>();
            _exitButton = _sceneContext.Resolve<ExitButton>();
            _volumeSlider = _sceneContext.Resolve<VolumeSlider>();

            _volumeSlider.SetMixer(_gameContext.Resolve<AudioMixer>());

            _input.Player.Pause.performed += OnPausePressed;
            _exitButton.Clicked += OnExitButtonClicked;

            _input.Enable();

            _volumeSlider.gameObject.SetActive(true);
            _volumeSlider.AnimateIn();
            _exitButton.gameObject.SetActive(true);
            _exitButton.AnimateIn();
        }

        public void Deactivate()
        {
            _input.Disable();

            _volumeSlider.AnimateOut();
            _exitButton.AnimateOut();

            _input.Player.Pause.performed -= OnPausePressed;
            _exitButton.Clicked -= OnExitButtonClicked;
        }

        private void OnPausePressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Finished?.Invoke(new GameModeArgs(GameModeType.GameplayMode));
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}