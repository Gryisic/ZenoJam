using Cinemachine;
using UnityEngine;

namespace ZenoJam.Common
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public abstract class CameraType : MonoBehaviour 
    {
        private CinemachineVirtualCamera _camera;

        public CinemachineVirtualCamera Camera 
        {
            get 
            {
                if (_camera == null)
                    _camera = GetComponent<CinemachineVirtualCamera>();

                return _camera;
            }
        }
    }
}