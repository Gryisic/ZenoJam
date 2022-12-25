using System;
using UnityEngine;

namespace ZenoJam.Common
{
    public class CutsceneTrigger : Trigger
    {
        public event Action<CutsceneProvider> CutsceneInitiated;

        [SerializeField] private CutsceneProvider _provider;

        protected override void Execute()
        {
            _provider.SetSeen();
            CutsceneInitiated?.Invoke(_provider);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player) && _provider.IsSeen == false)
            {
                Execute();
            }
        }
    }
}
