using UnityEngine;

namespace ZenoJam.Common
{
    [CreateAssetMenu(menuName = "Gameplay/Configs/Units/Config", fileName = "Unit")]
    public class UnitConfig : ScriptableObject 
    {
        [SerializeField] private int _health = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _movementSpeed = 1;

        public int Health => _health;
        public int Damage => _damage;
        public float MovementSpeed => _movementSpeed;
    }
}
