using System;
using UnityEngine;

namespace ZenoJam.Common
{
    public class NeutralPlayer : Player
    {
        public override event Action Death;

        protected override void Initialize()
        {
            actions = new NeutralRoleActions(m_collider);
        }

        public override void Dispose()
        {
            actions.Dispose();
        }

        public override void Move(Vector2 destination)
        {
            if (destination != Vector2.zero)
            {
                animator.StartMovement();

                sprite.flipX = destination.x < 0;

                m_rigidbody.MovePosition(m_rigidbody.position + destination * config.MovementSpeed * Time.fixedDeltaTime);
            }
            else
            {
                animator.EndMovement();
            }
        }
    }
}
