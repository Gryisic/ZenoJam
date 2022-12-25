using System;
using UnityEngine;

namespace ZenoJam.Infrastructure.Interfaces
{
    public interface IDamagable 
    {
        public event Action DamageTaken;

        public void ApplyDamage(int damage, Transform from);
    }
}
