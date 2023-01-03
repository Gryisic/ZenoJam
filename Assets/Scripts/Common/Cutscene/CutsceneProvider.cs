using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

namespace ZenoJam.Common
{
    [System.Serializable]
    public class CutsceneProvider 
    {
        [SerializeField] private PlayableDirector _director;
        [SerializeField] private CinemachineVirtualCamera _camera;

        public PlayableDirector Director => _director;
        public CinemachineVirtualCamera Camera => _camera;

        public bool IsSeen { get; private set; } = true;

        public void SetSeen() => IsSeen = true;
    }
}