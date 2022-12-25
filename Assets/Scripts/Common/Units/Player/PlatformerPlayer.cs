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
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _lightJumpSound;
        [SerializeField] private AudioSource _audioSource;

        private PlayerPhysics _physics;

        private bool _isAlive = true;

        protected override void Initialize()
        {
            actions = new PlatformerRoleActions(new BasePlayerData(animator, m_collider, m_rigidbody, config), 
                                                _groundCheckData);

            _physics = new PlayerPhysics(m_rigidbody);

            var act = actions as PlatformerRoleActions;

            act.Jump += PlayJump;
            act.LightJump += PlayLJ;

            _physics.Update();
            actions.Enable();
        }

        public override void Dispose()
        {
            var act = actions as PlatformerRoleActions;

            act.Jump -= PlayJump;
            act.LightJump -= PlayLJ;

            _physics.Dispose();
            actions.Dispose();
        }

        private void PlayJump() 
        {
            _audioSource.clip = _jumpSound;
            _audioSource.Play();
        }

        private void PlayLJ() 
        {
            _audioSource.clip = _lightJumpSound;
            _audioSource.Play();
        }

        public override void Move(Vector2 destination)
        {
            if (_isAlive)
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
        }

        public void ApplyDamage(int damage, Transform from)
        {
            actions.ApplyDamage(damage, from, out bool isDead);

            if (isDead) 
            {
                _isAlive = false;

                actions.Disable();
                animator.EndMovement();
                animator.InAir(0, false);

                Death?.Invoke();
            }
        }

        public void ApplyForce(float force) 
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, force);
        }

        public void SetPosition(Vector2 position) 
        {
            transform.position = position;
        }
    }
}
