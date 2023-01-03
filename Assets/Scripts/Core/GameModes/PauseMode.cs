using System;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class PauseMode : IGameMode, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private GameContext _gameContext;
        private Input _input;

        public PauseMode(GameContext context) 
        {
            _gameContext = context;
            _input = _gameContext.Resolve<Input>();
        }

        public void Activate(GameModeArgs args)
        {
            _input.Player.Pause.performed += OnPausePressed;

            _input.Enable();
        }

        public void Deactivate()
        {
            _input.Disable();

            _input.Player.Pause.performed -= OnPausePressed;
        }

        private void OnPausePressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Finished?.Invoke(new GameModeArgs(GameModeType.GameplayMode));
        }
    }
}