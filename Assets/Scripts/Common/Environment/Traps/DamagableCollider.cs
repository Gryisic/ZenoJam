using UnityEngine;
using ZenoJam.Infrastructure.Interfaces;

namespace ZenoJam.Common
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamagableCollider : MonoBehaviour 
    {
        [SerializeField] private int _damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out IDamagable damagable)) 
            {
                damagable.ApplyDamage(_damage, transform);
            }
        }
    }
}
