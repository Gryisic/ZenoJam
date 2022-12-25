using System;
using ZenoJam.Core;

namespace ZenoJam.Infrastructure.Interfaces
{
    public interface IGameMode 
    {
        public event Action<GameModeArgs> Finished;

        public void Activate(GameModeArgs args);
    }
}
