using CarSpace;
using Cinemachine;
using PlayerSpace;
using UnityEngine;

namespace CameraSpace
{
    public class ChangeCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _aimCamera;
        [SerializeField] private CinemachineVirtualCamera _driverCamera;

        [SerializeField] private PlayerController _playerController;

        private void OnEnable()
        {
            PlayerInputController.OnStratAim += ActiveAimCamera;
            PlayerInputController.OnCancelAim += ActiveMainCamera;
            SelectedCar.OnDrive += ActiveDriveCamera;
            CarInputController.OnExit += ActiveMainCamera;
        }

        private void Start()
        {
            _playerController.GetComponent<PlayerController>();
            ActiveMainCamera();
        }

        private void ActiveMainCamera()
        {
            _mainCamera.enabled = true;
            _aimCamera.enabled = false;
            _driverCamera.enabled = false;
        }

        private void ActiveAimCamera()
        {
            _aimCamera.enabled = true;
            _mainCamera.enabled = false;
            _driverCamera.enabled = false;
        }

        internal void ActiveDriveCamera()
        {
            _driverCamera.enabled = true;
            _mainCamera.enabled = false;
            _aimCamera.enabled = false;
        }

        private void OnDisable()
        {
            PlayerInputController.OnStratAim -= ActiveAimCamera;
            PlayerInputController.OnCancelAim -= ActiveMainCamera;
            SelectedCar.OnDrive -= ActiveDriveCamera;
            CarInputController.OnExit -= ActiveMainCamera;

        }
    }
}