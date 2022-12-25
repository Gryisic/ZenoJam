using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace ZenoJam.Common
{
    public class StartButton : MenuButton 
    {
        public event Action Clicked;

        [SerializeField] private List<MenuButton> _buttons;
        [SerializeField] private PlayableDirector _director;

        public void Execute() => Begin().Forget();

        private async UniTask Begin() 
        {
            _buttons.ForEach(button => button.AnimateOut());

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            var duration = _director.duration;
            _director.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(duration));

            Clicked?.Invoke();
        }
    }
}
