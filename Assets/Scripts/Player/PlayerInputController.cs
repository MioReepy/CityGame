using System;
using CameraSpace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSpace
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerController _playerController;
        private PlayerAnimator _playerAnimator;
        // [SerializeField] private AimCamera _aimCamera;
        
        #region InputAction

        private PlayerInput _playerInputController;
        private InputAction _actionMove;
        private InputAction _actionJump;
        private InputAction _actionShoot;
        private InputAction _actionAim;

        #endregion

        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInput>();
            _playerController = GetComponent<PlayerController>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            
            _actionMove = _playerInputController.actions["Move"];
            _actionJump = _playerInputController.actions["Jump"];
            _actionShoot = _playerInputController.actions["Shoot"];
            _actionAim = _playerInputController.actions["Aim"];

            Cursor.lockState = CursorLockMode.Locked;
        }

        // private void Start()
        // {
        //     _aimCamera = GetComponent<AimCamera>();
        // }

        private void OnEnable()
        {
            _actionShoot.performed += _ => Shooting();
            _actionAim.performed += _ => StartAim();
            _actionAim.canceled += _ => CancelAim();
        }

        private void Shooting()
        {
            _playerController.ShootGun();
        }

        private void Update()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            Vector2 input = _actionMove.ReadValue<Vector2>();
            _playerController.MoveInput = input;
        }

        private void Jump()
        {
            if (_actionJump.triggered)
            {
                _playerController.isJump = true;
            }
        }
        
        private void StartAim()
        {
            _playerAnimator.StartAim();
            // _aimCamera.StartAim();
        }

        private void CancelAim()
        {
            _playerAnimator.CancelAim();
            // _aimCamera.CancelAim();
        }
        
        private void OnDisable()
        {
            _actionShoot.performed -= _ => Shooting();
            _actionAim.performed += _ => StartAim();
            _actionAim.canceled += _ => CancelAim();
        }
    }
}