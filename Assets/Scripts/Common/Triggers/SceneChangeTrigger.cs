using System;
using UnityEngine;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class SceneChangeTrigger : Trigger
    {
        public event Action<SceneType> Triggered;

        [SerializeField] private SceneType _sceneType;
        [SerializeField] private EndSceneDoor _door;

        protected override void Execute()
        {
            _door.Close();
            Triggered?.Invoke(_sceneType);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player)) 
            {
                Execute();
            }
        }
    }
}
