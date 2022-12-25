using UnityEngine;

namespace ZenoJam.Common
{
    [System.Serializable]
    public struct GroundCheckData 
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _enemyMask;

        public Transform Transform => _transform;
        public float Radius => _radius;
        public LayerMask GroundMask => _groundMask;
        public LayerMask EnemyMask => _enemyMask;
    }
}
