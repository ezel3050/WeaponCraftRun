using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera startCamera;
        [SerializeField] private CinemachineVirtualCamera inGameCamera;
        
        public static CameraManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
        
        public void SetCameraFollow(Transform t)
        {
            startCamera.Follow = t;
            startCamera.LookAt = t;
            inGameCamera.Follow = t;
            inGameCamera.LookAt = t;
        }
        
        public void TurnStartCameraOn()
        {
            TurnAllCamerasOff();
            startCamera.enabled = true;
        }

        public void TurnInGameCameraOn()
        {
            TurnAllCamerasOff();
            inGameCamera.enabled = true;
        }
        
        private void TurnAllCamerasOff()
        {
            startCamera.enabled = false;
            inGameCamera.enabled = false;
        }
    }
}