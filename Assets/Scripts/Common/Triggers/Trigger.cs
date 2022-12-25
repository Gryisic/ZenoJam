using UnityEngine;

namespace ZenoJam.Common
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Trigger : MonoBehaviour 
    {
        private void Awake()
        {
            Initialize();
        }

        protected abstract void Execute();

        private void Initialize()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
