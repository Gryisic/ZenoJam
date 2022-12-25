using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using ZenoJam.Utils;

namespace ZenoJam.Common
{
    public class DashAction 
    {
        public event Action DashStarted;
        public event Action DashEnded;

        private Rigidbody2D _rigidbody;

        public bool CanDash { get; private set; } = true;

        public DashAction(Rigidbody2D rigidbody) 
        {
            _rigidbody = rigidbody;
        }

        public void Execute(Vector2 movementDirection) 
        {
            DashAsync(movementDirection).Forget();
        }

        private async UniTask DashAsync(Vector2 movementDirection)
        {
            var cashedVelocity = _rigidbody.velocity;
            var cashedConstraints = _rigidbody.constraints;

            DashStarted?.Invoke();

            CanDash = false;

            _rigidbody.velocity = new Vector2(20f * movementDirection.x, _rigidbody.velocity.y);

            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            _rigidbody.freezeRotation = true;

            await UniTask.Delay(TimeSpan.FromSeconds(Constants.DASH_DURATION));

            cashedVelocity.y = 0;

            _rigidbody.constraints = cashedConstraints;
            _rigidbody.velocity = cashedVelocity;

            DashEnded?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(Constants.DASH_COOLDOWN));

            CanDash = true;
        }
    }
}
