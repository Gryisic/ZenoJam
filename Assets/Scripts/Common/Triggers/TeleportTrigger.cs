using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace ZenoJam.Common
{
    public class TeleportTrigger : Trigger 
    {
        [SerializeField] private TeleportTrigger _link;

        private bool _passed = false;

        protected override void Execute()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlatformerPlayer player) && _passed == false)
            {
                _link.TogglePassed();
                player.SetPosition(_link.transform.position);
                TogglePassed();

                Untoggle().Forget();
            }
        }

        public void TogglePassed() 
        {
            _passed = !_passed;
        }

        private async UniTask Untoggle() 
        {
            await UniTask.Delay(TimeSpan.FromSeconds(20f));

            _link.TogglePassed();
            TogglePassed();
        }
    }
}
