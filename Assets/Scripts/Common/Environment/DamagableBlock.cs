using System;
using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    public class DamagableBlock : MonoBehaviour, IDamagable, IDestroyable
    {
        public event Action DamageTaken;

        [SerializeField] private int _health;

        public void ApplyDamage(int damage, Transform from)
        {
            _health -= damage;

            if (_health <= 0)
                Destroy(gameObject);
        }

        public void Destroy()
        {
            Debug.Log("Destroyed");
        }
    }
}
