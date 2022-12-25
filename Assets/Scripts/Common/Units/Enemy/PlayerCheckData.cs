using UnityEngine;

namespace ZenoJam.Common
{
    [System.Serializable]
    public struct PlayerCheckData 
    {
        [SerializeField] private Transform _playerCheck;
        [SerializeField] private float _checkRadius;
        [SerializeField] private LayerMask _playerMask;

        public Transform Transform => _playerCheck;
        public float Radius => _checkRadius;
        public LayerMask Mask => _playerMask;
    }
}
