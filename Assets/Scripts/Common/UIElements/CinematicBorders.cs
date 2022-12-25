using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZenoJam.Common
{
    public class CinematicBorders : UIElement 
    {
        private Image[] _borders;

        private Vector2 _upperIn = new Vector2(0, 1000);
        private Vector2 _upperOut = new Vector2(0, 1150);
        private Vector2 _lowerIn = new Vector2(0, -900);
        private Vector2 _lowerOut = new Vector2(0, -1050);

        public override void Activate()
        {
            if (_borders == null)
                _borders = GetComponentsInChildren<Image>();

            animator.Slide(_borders[0].rectTransform, _upperIn);
            animator.Slide(_borders[1].rectTransform, _lowerIn);
        }

        public override void Deactivate()
        {
            animator.Slide(_borders[0].rectTransform, _upperOut);
            animator.Slide(_borders[1].rectTransform, _lowerOut);

            DeactivateSelfAsync().Forget();
        }

        private async UniTask DeactivateSelfAsync() 
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            gameObject.SetActive(false);
        }
    }
}
