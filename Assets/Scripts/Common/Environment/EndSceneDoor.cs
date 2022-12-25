using UnityEngine;
using ZenoJam.Utils;

namespace ZenoJam.Common
{
    [RequireComponent(typeof(Animator))]
    public class EndSceneDoor : MonoBehaviour 
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Close() => _animator.SetBool(Constants.SCENE_CHANGE_DOOR_STATE, false);

        public void Open() => _animator.SetBool(Constants.SCENE_CHANGE_DOOR_STATE, true);
    }
}
