using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class PlatformerEnemy : Enemy
    {
        public override event Action DamageTaken;
        public override event Action<Vector2> Dead;

        [SerializeField] private PlayerCheckData _playerCheckData;

        private MovingStrategy _movingStrategy = new MovingStrategy();
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private CustomAnimator _animator;

        private bool _canMove = true;

        protected override void Initialize()
        {
            health = config.Health;
            damage = config.Damage;

            _animator = new CustomAnimator(GetComponent<Animator>());

            if (moveType != MoveType.None)
                IdleAsync(_tokenSource.Token).Forget();

            CheckPlayerAsync(_tokenSource.Token).Forget();
        }

        public override void Dispose()
        {
            _tokenSource.Cancel();

            _tokenSource.Dispose();
        }

        public override void Move(Vector2 destination) 
        {
            transform.Translate(destination * config.MovementSpeed * Time.fixedDeltaTime);
        }

        public override void ApplyDamage(int damage, Transform from)
        {
            ApplyDamageAsync(damage, _tokenSource.Token).Forget();
        }

        protected override void Die()
        {
            isAlive = false;

            Dead?.Invoke(transform.position);
        }

        private async UniTask IdleAsync(CancellationToken token = default) 
        {
            while (isAlive) 
            {
                if (token.IsCancellationRequested)
                    break;

                Vector2 destination = _movingStrategy.GetDestination(moveType, transform.position);

                if (destination != Vector2.zero)
                    await MoveAsync(destination, token);

                await UniTask.Delay(TimeSpan.FromSeconds(2f));
            }
        }

        private async UniTask MoveAsync(Vector2 destination, CancellationToken token = default) 
        {
            _animator.StartMovement();

            while (token.IsCancellationRequested == false && isAlive)
            {
                float distance = (destination - (Vector2) transform.position).sqrMagnitude;

                if (distance < 0.5f || _canMove == false)
                {
                    _animator.EndMovement();
                    break;
                }

                Vector2 direction = (destination - (Vector2)transform.position).normalized;
                direction.y = transform.position.y;

                Move(direction);

                await UniTask.WaitForFixedUpdate();
            }
        }

        private async UniTask ApplyDamageAsync(int damage, CancellationToken token = default) 
        {
            _canMove = false;
            health -= damage;

            if (health <= 0)
            {
                health = 0;

                isAlive = false;
                animator.Death();
                _animator.EndMovement();

                await UniTask.Delay(TimeSpan.FromSeconds(1f));

                Dead?.Invoke(transform.position);

                gameObject.SetActive(false);

                return;
            }

            animator.StartTakingDamage();

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            animator.EndTakingDamage();

            _canMove = true;
        }

        private async UniTask CheckPlayerAsync(CancellationToken token = default)
        {
            while (token.IsCancellationRequested == false && isAlive)
            {
                var collider = Physics2D.OverlapCircle(_playerCheckData.Transform.position, _playerCheckData.Radius, 
                                                       _playerCheckData.Mask);

                if (collider && collider.TryGetComponent(out IDamagable damagable)) 
                {
                    damagable.ApplyDamage(damage, transform);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
        }
    }
}
