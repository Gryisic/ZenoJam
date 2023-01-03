using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using ZenoJam.Utils;

namespace ZenoJam.Common
{
    public class JumpAction 
    {
        public event Action<float, bool> InAir;

        private GroundCheckData _groundCheckData;
        private Rigidbody2D _rigidbody;

        private bool _canJump;

        public JumpAction(Rigidbody2D rigidbody, GroundCheckData groundCheckData) 
        {
            _groundCheckData = groundCheckData;
            _rigidbody = rigidbody;
        }

        public void Execute()
        {
            if (_canJump)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Constants.JUMP_FORCE);
            }
        }

        public void Update(CancellationToken token) 
        {
            CheckJumpPossibility(token);

            if (_rigidbody.velocity.y < 0)
            {
                InAir?.Invoke(_rigidbody.velocity.y, true);

                CheckForEnemies();
            }
            else if (_rigidbody.velocity.y > 0)
            {
                InAir?.Invoke(_rigidbody.velocity.y, true);
            }
            else 
            {
                InAir?.Invoke(_rigidbody.velocity.y, false);
            }
        }

        public void LightJump()
        {
            if (IsGrounded() == false)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Constants.JUMP_FORCE / 2);
            }
        }

        private bool IsGrounded() =>
            Physics2D.OverlapCircle(_groundCheckData.Transform.position, _groundCheckData.Radius, _groundCheckData.GroundMask);

        private void CheckForEnemies() 
        {
            var collider = Physics2D.OverlapCircle(_groundCheckData.Transform.position, _groundCheckData.Radius, _groundCheckData.EnemyMask);

            if (collider && collider.TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(Constants.JUMP_ATTACK_DAMAGE, _rigidbody.transform);

                LightJump();
            }
        }

        private void CheckJumpPossibility(CancellationToken token = default)
        {
            bool isGrounded = IsGrounded();

            if (isGrounded == false && _canJump)
                CoyoteTimeAsync(token).Forget();

            else if (isGrounded && _canJump == false)
                _canJump = true;
        }

        private async UniTask CoyoteTimeAsync(CancellationToken token = default)
        {
            var delayTask = UniTask.Delay(TimeSpan.FromSeconds(Constants.COYOTE_TIME));

            await UniTask.WhenAny(AwaitCancellation(token), delayTask);

            _canJump = false;
        }

        private async UniTask AwaitCancellation(CancellationToken token = default) =>
            await UniTask.WaitUntil(() => token.IsCancellationRequested);
    }
}
