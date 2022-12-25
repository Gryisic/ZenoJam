using UnityEngine;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    [System.Serializable]
    public class PlayerSpawner
    {
        [SerializeField] private PlayerRole _playerType;
        [SerializeField] private Transform _spawnPosition;

        private PlayerFactory _factory = new PlayerFactory();

        public void Initialize() =>  _factory.Load(_playerType);

        public Player Create() => _factory.Create(_spawnPosition.position);
    }
}
