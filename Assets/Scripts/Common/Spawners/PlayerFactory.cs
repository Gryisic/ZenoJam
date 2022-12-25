using UnityEngine;
using ZenoJam.Utils;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class PlayerFactory 
    {
        private Player _player;

        public void Load(PlayerRole role) =>
            _player = Resources.Load<Player>($"{Constants.PLAYER_PREFABS_PATH}/{role}");

        public Player Create(Vector2 at) => GameObject.Instantiate(_player, at, Quaternion.identity);
    }
}
