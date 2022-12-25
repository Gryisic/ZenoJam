using ZenoJam.Infrastructure.Interfaces;
using System.Linq;
using System;

namespace ZenoJam.Common
{
    public abstract class StateMachine : IStateSitcher, IDisposable
    {
        protected State currentState;
        protected State[] states;

        public abstract void Dispose();

        public void ChangeState<T>() where T : State
        {
            currentState?.Exit();
            currentState = states.First(s => s is T);
            currentState.Enter(this);
        }
    }
}
