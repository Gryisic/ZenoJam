using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using ZenoJam.Utils;

namespace ZenoJam.Common
{
    public class PlayerPhysics : IDisposable
    {
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private Vector2 _gravityVector;
        private Rigidbody2D _rigidbody;

        public PlayerPhysics(Rigidbody2D rigidbody) 
        {
            _rigidbody = rigidbody;
            _gravityVector = new Vector2(0, -Physics2D.gravity.y);
        }

        public void Dispose()
        {
            _tokenSource.Cancel();

            _tokenSource.Dispose();
        }

        public void Update() 
        {
            UpdateAsync(_tokenSource.Token).Forget();
        }

        private async UniTask UpdateAsync(CancellationToken token = default) 
        {
            while (token.IsCancellationRequested == false) 
            {
                if (_rigidbody.velocity.y < 0)
                {
                    _rigidbody.velocity -= _gravityVector * Constants.FALL_MULTIPLIER * Time.fixedDeltaTime;
                }
                else if (_rigidbody.velocity.y > 0)
                {
                    _rigidbody.velocity *= Constants.LINEAR_VELOCITY_SLOWDOWN_SPEED;
                }

                await UniTask.WaitForFixedUpdate();
            }
        }
    }
}