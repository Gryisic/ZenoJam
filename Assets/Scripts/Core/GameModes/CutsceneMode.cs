using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Playables;
using ZenoJam.Common;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Core
{
    public class CutsceneMode : IGameMode, IDeactivatable, IResetable
    {
        public event Action<GameModeArgs> Finished;

        private GameContext _gameContext;
        private SceneContext _sceneContext;
        private UIContext _uiContext;
        private SceneCamera _sceneCamera;
        private CinematicBorders _borders;
        private Player _player;

        private bool _isConstructed = false;

        public CutsceneMode(GameContext context) 
        {
            _gameContext = context;
        }

        public void Activate(GameModeArgs args)
        {
            if (_isConstructed == false)
            {
                _sceneContext = _gameContext.Resolve<SceneContext>();
                _uiContext = _sceneContext.Resolve<UIContext>();
                _sceneCamera = _sceneContext.Resolve<SceneCamera>();
                _player = _sceneContext.Resolve<Player>();
                _borders = _uiContext.Resolve<CinematicBorders>();

                _isConstructed = true;
            }

            ToggleElementsActivity(true);
            PlayCutsceneAsync(args.CutsceneProvider).Forget();
        }

        public void Deactivate() => ToggleElementsActivity(false);

        public void Reset() => _isConstructed = false;

        private void RaiseOnFinishedEvent() => Finished?.Invoke(new GameModeArgs(GameModeType.GameplayMode));

        private async UniTask PlayCutsceneAsync(CutsceneProvider provider) 
        {
            PlayableDirector director = provider.Director;

            director.Play();
            _sceneCamera.ChangeToExternalCamera(provider.Camera);
            _player.gameObject.SetActive(false);

            await UniTask.Delay(TimeSpan.FromSeconds(director.duration));

            director.Stop();
            _player.gameObject.SetActive(true);
            RaiseOnFinishedEvent();
        }

        private void ToggleElementsActivity(bool value)
        {
            switch (value)
            {
                case true:
                    _borders.gameObject.SetActive(value);

                    _borders.Activate();
                    break;

                case false:
                    _borders.Deactivate();
                    break;
            }
        }
    }
}