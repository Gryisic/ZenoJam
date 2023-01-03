using System;
using ZenoJam.Common;
using ZenoJam.Infrastructure.Interfaces;
using static UnityEngine.InputSystem.InputAction;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class DialogueMode : IGameMode, IResetable, IDeactivatable
    {
        public event Action<GameModeArgs> Finished;

        private GameContext _gameContext;
        private UIContext _uiContext;
        private SceneCamera _sceneCamera;
        private CinematicBorders _borders;
        private DialogueWindow _dialogueWindow;
        private Input _input;

        private Dialogue _dialogue = new Dialogue();

        private bool _isConstructed = false;

        public DialogueMode(GameContext context) 
        {
            _gameContext = context;
            _input = _gameContext.Resolve<Input>();
        }

        public void Activate(GameModeArgs args)
        {
            if (_isConstructed == false)
            {
                _uiContext = _gameContext.Resolve<SceneContext>().Resolve<UIContext>();
                _sceneCamera = _gameContext.Resolve<SceneContext>().Resolve<SceneCamera>();
                _borders = _uiContext.Resolve<CinematicBorders>();
                _dialogueWindow = _uiContext.Resolve<DialogueWindow>();

                _isConstructed = true;
            }

            _dialogue.DialogueEnded += RaiseOnFinishedEvent;
            _dialogue.NamePrinted += _dialogueWindow.UpdateName;
            _dialogue.LetterPrinted += _dialogueWindow.UpdateSentence;
            _dialogue.SentencePrinting += _dialogueWindow.ToggleNextSentenceImage;

            _dialogueWindow.Clear();
            AttachInput();
            ToggleElementsActivity(true);

            _dialogue.Initiate(args.DialogueProvider);
            _sceneCamera.Focus(args.DialogueProvider.Transform);
        }

        public void Deactivate()
        {
            _dialogue.SentencePrinting -= _dialogueWindow.ToggleNextSentenceImage;
            _dialogue.NamePrinted -= _dialogueWindow.UpdateName;
            _dialogue.LetterPrinted -= _dialogueWindow.UpdateSentence;
            _dialogue.DialogueEnded -= RaiseOnFinishedEvent;

            DeattachInput();
            ToggleElementsActivity(false);
        }

        public void Reset() => _isConstructed = false;

        private void AttachInput() 
        {
            _input.Player.LMB.performed += ChangeSentence;
            _input.Player.RMB.performed += ChangeSentence;
            _input.Player.Space.performed += ChangeSentence;

            _input.Enable();
        }

        private void DeattachInput()
        {
            _input.Disable();

            _input.Player.LMB.performed -= ChangeSentence;
            _input.Player.RMB.performed -= ChangeSentence;
            _input.Player.Space.performed -= ChangeSentence;
        }

        private void ChangeSentence(CallbackContext context) => _dialogue.NextSentence();

        private void RaiseOnFinishedEvent() => Finished?.Invoke(new GameModeArgs(GameModeType.GameplayMode));

        private void ToggleElementsActivity(bool value) 
        {
            switch (value)
            {
                case true:
                    _borders.gameObject.SetActive(value);
                    _dialogueWindow.gameObject.SetActive(value);

                    _borders.Activate();
                    _dialogueWindow.Activate();
                    break;

                case false:
                    _borders.Deactivate();
                    _dialogueWindow.Deactivate();
                    break;
            }
        }
    }
}