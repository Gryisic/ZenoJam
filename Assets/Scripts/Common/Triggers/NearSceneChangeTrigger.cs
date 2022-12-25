using UnityEngine;

namespace ZenoJam.Common
{
    public class NearSceneChangeTrigger : Trigger
    {
        [SerializeField] private EndSceneDoor _door;

        private Collider2D _playerCollision;

        protected override void Execute() { }

        private bool IsPlayerTriggered(Collider2D collision) 
        {
            if (_playerCollision == collision) 
            {
                return true;
            }
            else if (_playerCollision != collision)
            {
                if (collision.TryGetComponent(out Player player))
                {
                    _playerCollision = collision;

                    return true;
                }
            }

            return false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsPlayerTriggered(collision))
                _door.Open();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsPlayerTriggered(collision))
                _door.Close();
        }
    }
}
