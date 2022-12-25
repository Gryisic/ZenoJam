using UnityEngine;

namespace ZenoJam.Common
{
    public abstract class UIElement : MonoBehaviour 
    {
        protected UIAnimator animator = new UIAnimator();

        public abstract void Activate();

        public abstract void Deactivate();
    }
}
