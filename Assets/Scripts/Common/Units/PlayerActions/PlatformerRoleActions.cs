using UnityEngine;
using UnityEngine.InputSystem;
using ZenoJam.Infrastructure.Interfaces;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using ZenoJam.Utils;

namespace ZenoJam.Common
{
    public class PlatformerRoleActions : IPlayerRoleActions
    {
        public event Action LightJump;
        public event Action Jump;

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private JumpAction _jumpAction;
        private DashAction _dashAction;
        private CheckInteractableAction _checkInteractableAction;

        private CustomAnimator _animator;

        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;

        private Vector2 _movementDirection;

        private bool _isInvincible = false;
        private int _health;

        public bool CanMove { get; private set; } = true;

        public Vector2 MovementDirection 
        {
            get 
            {
                if (CanMove || _dashAction.CanDash == false)
                    return _movementDirection;

                return Vector2.zero;
            }
        }

        public PlatformerRoleActions(BasePlayerData data, GroundCheckData checkData) 
        {
            _collider = data.Collider;
            _animator = data.Animator;
            _health = data.Config.Health;
            _rigidbody = data.Rigidbody;

            _jumpAction = new JumpAction(data.Rigidbody, checkData);
            _dashAction = new DashAction(data.Rigidbody);
            _checkInteractableAction = new CheckInteractableAction();

            TickAsync(_tokenSource.Token).Forget();
        }

        public void RaiseLightJump() => LightJump?.Invoke();

        public void RaiseJump() => Jump?.Invoke();

        public void Enable() 
        {
            SubscribeToEvents();
        }

        public void Disable() 
        {
            UnsubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeToEvents();

            _tokenSource.Cancel();

            _tokenSource.Dispose();
        }

        public void StartMovement(InputAction.CallbackContext context)
        {
            _movementDirection = context.ReadValue<Vector2>();
            _movementDirection = new Vector2(MovementDirection.x, 0);
        }

        public void EndMovement(InputAction.CallbackContext context)
        {
            _movementDirection = Vector2.zero;
        }

        public void LMB(InputAction.CallbackContext context) => _checkInteractableAction.Check(_collider);

        public void RMB(InputAction.CallbackContext context)
        {
            if (_movementDirection != Vector2.zero)
                _dashAction.Execute(MovementDirection);
        }

        public void Space(InputAction.CallbackContext context) => _jumpAction.Execute();

        public void ApplyDamage(int damage, Transform from, out bool isDead)
        {
            if (_isInvincible == false)
            {
                _animator.StartTakingDamage();

                _health -= damage;

                ApplyDamageAsync(from).Forget();

                if (_health <= 0)
                {
                    _health = 0;

                    _animator.Death();

                    isDead = true;

                    return;
                }
            }

            isDead = false;
        }

        private void EnableMovement() => CanMove = true;

        private void DisableMovement() => CanMove = false;

        private void SubscribeToEvents() 
        {
            _jumpAction.InAir += _animator.InAir;

            _dashAction.DashStarted += DisableMovement;
            _dashAction.DashStarted += _animator.Dash;
            _dashAction.DashEnded += EnableMovement;

            _jumpAction.LightJumpp += LightJump;
            _jumpAction.Jump += Jump;
        }

        private void UnsubscribeToEvents()
        {
            _jumpAction.InAir -= _animator.InAir;

            _dashAction.DashStarted -= DisableMovement;
            _dashAction.DashStarted -= _animator.Dash;
            _dashAction.DashEnded -= EnableMovement;

            _jumpAction.LightJumpp -= LightJump;
            _jumpAction.Jump -= Jump;
        }

        private async UniTask TickAsync(CancellationToken token = default) 
        {
            while (token.IsCancellationRequested == false) 
            {
                _jumpAction.Update(token);

                await UniTask.WaitForFixedUpdate();
            }
        }

        private async UniTask ApplyDamageAsync(Transform from, CancellationToken token = default)
        {
            CanMove = false;
            _isInvincible = true;

            _rigidbody.velocity = new Vector2().DamageImpact(_rigidbody, from);

            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));

            _animator.EndTakingDamage();
            CanMove = true;

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            _isInvincible = false;
        }

        private async UniTask AwaitCancellation(CancellationToken token = default) =>
            await UniTask.WaitUntil(() => token.IsCancellationRequested);
    }
}
