using UnityEngine;
using DG.Tweening;

namespace ZenoJam.Common
{
    public class UIAnimator 
    {
        public void Slide(RectTransform transform, Vector2 to) 
        {
            transform.DOAnchorPos(to, 1f, false).SetEase(Ease.InOutQuint);
        }
    }
}
