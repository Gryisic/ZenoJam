using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace ZenoJam.Common
{
    public class SceneTransition : UIElement
    {
        private RectTransform _rectTransform;

        private Vector2 _outPosition = new Vector2(2000, 0);

        public override void Activate() => animator.Slide(_rectTransform, Vector2.zero);

        public override void Deactivate()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            animator.Slide(_rectTransform, _outPosition);

            ReturnToInitialPositionAsync().Forget();
        }

        private async UniTask ReturnToInitialPositionAsync() 
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

           gameObject.SetActive(false);
            _rectTransform.anchoredPosition = _outPosition * -1;
        }
    }
}
