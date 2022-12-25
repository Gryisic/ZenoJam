using System;
using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public abstract class Enemy : Unit, IDamagable
    {
        public abstract event Action DamageTaken;
        public abstract event Action<Vector2> Dead;

        [SerializeField] protected MoveType moveType;

        protected bool isAlive = true;

        protected int health;
        protected int damage;

        public abstract void ApplyDamage(int damage, Transform from);

        protected abstract void Die();

        public void Restore() 
        {
            health = config.Health;
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
