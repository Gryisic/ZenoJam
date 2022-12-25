using ZenoJam.Common;

namespace ZenoJam.Infrastructure.Interfaces
{
    public interface IStateSitcher 
    {
        public void ChangeState<T>() where T : State;
    }
}
