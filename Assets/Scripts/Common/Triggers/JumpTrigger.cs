using UnityEngine;

namespace ZenoJam.Common
{
    public class JumpTrigger : Trigger
    {
        [SerializeField] private float _force;

        protected override void Execute()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlatformerPlayer player))
            {
                player.ApplyForce(_force);
            }
        }
    }
}
