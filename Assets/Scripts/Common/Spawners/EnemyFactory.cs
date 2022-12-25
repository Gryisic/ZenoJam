using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using ZenoJam.Utils;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class EnemyFactory 
    {
        private List<Enemy> _prefabs = new List<Enemy>();

        private Dictionary<Vector2, Enemy> _enemies = new Dictionary<Vector2, Enemy>();

        public void Load(UnitType type) => 
            _prefabs.Add(Resources.Load<Enemy>($"{Constants.ENEMY_PREFABS_PATH}/{type}"));

        public void Create(Vector2 at) 
        {
            foreach (var prefab in _prefabs)
            {
                if (_enemies.ContainsKey(at) == false)
                {
                    _enemies.Add(at, GameObject.Instantiate(prefab, at, Quaternion.identity));

                    _enemies[at].Dead += Respawn;
                }
            }
        }

        private void Respawn(Vector2 at) 
        {
            RespawnAsync(at).Forget();
        }

        private async UniTask RespawnAsync(Vector2 at) 
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3f));

            _enemies[at].gameObject.SetActive(true);
            _enemies[at].Restore();
        }
    }
}
