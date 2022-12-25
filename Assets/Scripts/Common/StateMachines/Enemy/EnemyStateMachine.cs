using Cysharp.Threading.Tasks;
using System.Threading;

namespace ZenoJam.Common
{
    public class EnemyStateMachine : StateMachine 
    {
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public EnemyStateMachine() 
        {
            Initialize();
        }

        public override void Dispose()
        {
            _tokenSource.Cancel();

            _tokenSource.Dispose();
        }

        private void Initialize() 
        {
            states = new State[] 
            {
                
            };

            Tick(_tokenSource.Token).Forget();
        }

        private async UniTask Tick(CancellationToken token = default) 
        {
            while (token.IsCancellationRequested == false) 
            {
                await UniTask.WaitForFixedUpdate();

                currentState.Update();
            }
        }
    }
}
