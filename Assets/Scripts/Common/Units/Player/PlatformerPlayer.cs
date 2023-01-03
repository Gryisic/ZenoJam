using System;
using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    public class PlatformerPlayer : Player, IDamagable
    {
        public override event Action Death;
        public event Action DamageTaken;

        [SerializeField] private GroundCheckData _groundCheckData;

        private PlayerPhysics _physics;

        protected override void Initialize()
        {
            actions = new PlatformerRoleActions(new BasePlayerData(animator, m_collider, m_rigidbody, config), 
                                                _groundCheckData);

            _physics = new PlayerPhysics(m_rigidbody);

            _physics.Update();
            actions.Enable();
        }

        public override void Dispose()
        {
            var act = actions as PlatformerRoleActions;

            _physics.Dispose();
            actions.Dispose();
        }

        public override void Move(Vector2 destination)
        {
            if (actions.CanMove)
                m_rigidbody.velocity = new Vector2(destination.x * config.MovementSpeed, m_rigidbody.velocity.y);

            if (destination != Vector2.zero)
            {
                animator.StartMovement();
                sprite.flipX = destination.x < 0;
            }
            else
            {
                animator.EndMovement();
            }
        }

        public void ApplyDamage(int damage, Transform from)
        {
            actions.ApplyDamage(damage, from, out bool isDead);

            if (isDead) 
            {
                actions.Disable();

                Death?.Invoke();
            }
        }
    }
}
