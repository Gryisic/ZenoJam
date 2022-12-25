using System.Collections.Generic;
using UnityEngine;

namespace ZenoJam.Common
{
    [System.Serializable]
    public class EnemySpawner 
    {
        [SerializeField] private List<SpawnMarker> _spawnMarkers;

        private EnemyFactory _factory = new EnemyFactory();

        public void Initialize() 
        {
            if (_spawnMarkers != null && _spawnMarkers.Count > 0)
                _spawnMarkers.ForEach(m => _factory.Load(m.Type));
        }

        public void SpawnAll() 
        {
            if (_spawnMarkers != null && _spawnMarkers.Count > 0)
                _spawnMarkers.ForEach(m => _factory.Create(m.transform.position)); 
        }
    }
}
