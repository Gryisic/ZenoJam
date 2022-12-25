using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    public abstract class State 
    {
        protected IStateSitcher switcher;

        public abstract void Enter(IStateSitcher switcher);

        public abstract void Update();

        public abstract void Exit();
    }
}
