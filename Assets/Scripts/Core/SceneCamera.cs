using System.Linq;
using UnityEngine;
using Cinemachine;
using ZenoJam.Common;
using CameraType = ZenoJam.Common.CameraType;

namespace ZenoJam.Core
{
    public class SceneCamera : MonoBehaviour
    {
        [SerializeField] private CameraType[] _cameras;

        private CinemachineVirtualCamera _activeCamera;
        private CinemachineVirtualCamera _followCamera;
        private CinemachineVirtualCamera _focusCamera;
        private CinemachineVirtualCamera _staticCamera;

        private void Awake()
        {
            Initialize();
        }

        public void Focus(Transform focusTransform) 
        {
            _focusCamera.Follow = focusTransform;

            if (_activeCamera != _focusCamera)
                ChangeActiveCamera(_focusCamera);
        }

        public void StartFollowing(Transform transform) 
        {
            _followCamera.Follow = transform;

            if (_activeCamera != _followCamera)
                ChangeActiveCamera(_followCamera);
        }

        public void StopFollowing() => _followCamera.Follow = null;

        public void ChangeToExternalCamera(CinemachineVirtualCamera camera) => ChangeActiveCamera(camera);

        private void Initialize()
        {
            _activeCamera = _cameras.First(c => c is StaticCamera).Camera;
            _followCamera = _cameras.First(c => c is FollowCamera).Camera;
            _focusCamera = _cameras.First(c => c is FocusCamera).Camera;
        }

        private void ChangeActiveCamera(CinemachineVirtualCamera newCamera) 
        {
            _activeCamera = newCamera;

            _activeCamera.Priority = 10;

            _cameras.Where(c => c.Camera != _activeCamera).ToList().ForEach(x => x.Camera.Priority = 0);
        }
    }
}