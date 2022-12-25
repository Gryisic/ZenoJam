using UnityEngine;
using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;

namespace ZenoJam.Common
{
    public class MenuButton : MonoBehaviour 
    {
        [SerializeField] private Vector2 _inVector;
        [SerializeField] private Vector2 _outVector;

        private void Awake()
        {
            Begin().Forget();
        }

        public void AnimateIn()
        {
            var rectTransform = GetComponent<RectTransform>();

            rectTransform.DOAnchorPos(_inVector, 1, false).SetEase(Ease.InOutQuad);
        }

        public void AnimateOut() 
        {
            var rectTransform = GetComponent<RectTransform>();

            rectTransform.DOAnchorPos(_outVector, 1, false).SetEase(Ease.InOutQuad);
        }

        private async UniTask Begin()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            AnimateIn();
        }
    }
}
