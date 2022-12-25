using System;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    public abstract class Player : Unit
    {
        public abstract event Action Death;

        protected IPlayerRoleActions actions;

        public IPlayerRoleActions Actions => actions;

        private void FixedUpdate()
        {
            Move(actions.MovementDirection);
        }
    }
}
