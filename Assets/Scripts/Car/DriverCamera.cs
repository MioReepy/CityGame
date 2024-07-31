using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CarSpace
{
    public class DriverCamera : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private int _priorityBoost = 15;
        private InputAction _inputAction;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _inputAction = _playerInput.actions["Drive"];
        }

        private void OnEnable()
        {
            _inputAction.performed += _ => DriveCamera();
        }

        private void DriveCamera()
        {
            if (SelectedCar.canDrive)
            {
                _virtualCamera.Priority += _priorityBoost;
            }

        } 

        private void OnDisable()
        {
            _inputAction.performed -= _ => DriveCamera();
        }
    }
}