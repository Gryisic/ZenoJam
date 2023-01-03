using System.Collections.Generic;
using UnityEngine;
using ZenoJam.Utils;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class EnemyFactory 
    {
        private List<Enemy> _prefabs = new List<Enemy>();

        public void Load(UnitType type) => 
            _prefabs.Add(Resources.Load<Enemy>($"{Constants.ENEMY_PREFABS_PATH}/{type}"));

        public void Create(Vector2 at) 
        {
            foreach (var prefab in _prefabs)
                GameObject.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}
