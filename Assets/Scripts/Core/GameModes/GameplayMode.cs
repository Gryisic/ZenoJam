using System;
using ZenoJam.Common;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class GameplayMode : IGameMode, IResetable, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private GameContext _gameContext;
        private SceneContext _sceneContext;
        private UIContext _uiContext;
        private SceneChangeTrigger _sceneChangeTrigger;
        private SceneCamera _sceneCamera;
        private SceneSwitcher _sceneSwitcher;
        private Input _input;
        private Player _player;
        private TriggersContainer _triggersContainer;

        private bool _isConstructed = false;

        public GameplayMode(GameContext context)
        {
            _gameContext = context;
            _sceneSwitcher = context.Resolve<SceneSwitcher>();
            _input = context.Resolve<Input>();
        }

        public void Activate(GameModeArgs args)
        {
            if (_isConstructed == false)
            {
                _sceneContext = _gameContext.Resolve<SceneContext>();
                _uiContext = _sceneContext.Resolve<UIContext>();
                _sceneCamera = _sceneContext.Resolve<SceneCamera>();
                _sceneChangeTrigger = _sceneContext.Resolve<SceneChangeTrigger>();
                _player = _sceneContext.Resolve<Player>();
                _triggersContainer = _sceneContext.Resolve<TriggersContainer>();

                _isConstructed = true;
            }

            SubscribeToEvents();
            AttachInput();

            _sceneCamera.StartFollowing(_player.transform);
        }

        public void Deactivate()
        {
            DeattachInput();
            UnsubscribeToEvents();
        }

        public void Reset()
        {
            _sceneContext = null;

            _isConstructed = false;
        }

        private void AttachInput()
        {
            _input.Player.Space.performed += _player.Actions.Space;
            _input.Player.LMB.performed += _player.Actions.LMB;
            _input.Player.RMB.performed += _player.Actions.RMB;
            _input.Player.Movement.started += _player.Actions.StartMovement;
            _input.Player.Movement.canceled += _player.Actions.EndMovement;
            _input.Player.Pause.performed += ActivatePauseMode;

            _input.Enable();
        }

        private void DeattachInput()
        {
            _input.Disable();

            _input.Player.Pause.performed -= ActivatePauseMode;
            _input.Player.Space.performed -= _player.Actions.Space;
            _input.Player.LMB.performed -= _player.Actions.LMB;
            _input.Player.RMB.performed -= _player.Actions.RMB;
            _input.Player.Movement.started -= _player.Actions.StartMovement;
            _input.Player.Movement.canceled -= _player.Actions.EndMovement;
        }

        private void SubscribeToEvents()
        {
            foreach (var trigger in _triggersContainer.Triggers)
            {
                if (trigger is DialogueTrigger dialogue)
                    dialogue.DialogueInitiated += ActivateDialogueMode;
                else if (trigger is CutsceneTrigger cutscene)
                    cutscene.CutsceneInitiated += ActivateCutsceneMode;
            }

            _sceneChangeTrigger.Triggered += ActivateSceneSwitcMode;
            _sceneChangeTrigger.Triggered += _sceneSwitcher.ChangeScene;

            _player.Death += ReloadCurrentScene;
        }

        private void UnsubscribeToEvents()
        {
            foreach (var trigger in _triggersContainer.Triggers)
            {
                if (trigger is DialogueTrigger dialogue)
                    dialogue.DialogueInitiated -= ActivateDialogueMode;
                else if (trigger is CutsceneTrigger cutscene)
                    cutscene.CutsceneInitiated -= ActivateCutsceneMode;
            }

            _sceneChangeTrigger.Triggered -= ActivateSceneSwitcMode;
            _sceneChangeTrigger.Triggered -= _sceneSwitcher.ChangeScene;

            _player.Death -= ReloadCurrentScene;
        }

        private void ReloadCurrentScene() 
        {
            _sceneCamera.Focus(_player.transform);
            _sceneSwitcher.ReloadCurrentScene();
            Finished?.Invoke(new GameModeArgs(GameModeType.SceneSwitchMode));
        }

        private void ActivateDialogueMode(DialogueProvider provider) => 
            Finished?.Invoke(new GameModeArgs(GameModeType.DialogueMode, provider));

        private void ActivateSceneSwitcMode(SceneType type) => 
            Finished?.Invoke(new GameModeArgs(GameModeType.SceneSwitchMode));

        private void ActivateCutsceneMode(CutsceneProvider provider) =>
            Finished?.Invoke(new GameModeArgs(GameModeType.CutsceneMode, provider));

        private void ActivatePauseMode(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
            Finished?.Invoke(new GameModeArgs(GameModeType.PauseMode));
    }
}