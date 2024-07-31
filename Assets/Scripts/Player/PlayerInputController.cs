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
        
        #region InputAction

        private PlayerInput _playerInputController;
        private InputAction _actionMove;
        private InputAction _actionRun;
        private InputAction _actionJump;
        private InputAction _actionShoot;
        private InputAction _actionAim;

        #endregion

        public delegate Action Aim();

        public event Aim OnAim;
        
        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInput>();
            _playerController = GetComponent<PlayerController>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            
            _actionMove = _playerInputController.actions["Move"];
            _actionJump = _playerInputController.actions["Jump"];
            _actionShoot = _playerInputController.actions["Shoot"];
            _actionAim = _playerInputController.actions["Aim"];
            _actionRun = _playerInputController.actions["Run"];

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _actionShoot.performed += _ => Shooting();
            _actionAim.performed += _ => StartAim();
            _actionAim.canceled += _ => CancelAim();
            _actionRun.performed += _ => StartRun();
            _actionRun.canceled += _ => CancelRun();
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

            if (input != Vector2.zero)
            {
                _playerController.isWalk = true;
            }
            else
            {
                _playerController.isWalk = false;
            }
        }

        private void StartRun()
        {
            _playerController.isRun = true;
        }

        private void CancelRun()
        {
            _playerController.isRun = false;
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
            _playerController.isAim = true;
        }

        private void CancelAim()
        {
            _playerController.isAim = false;
        }
        
        private void OnDisable()
        {
            _actionShoot.performed -= _ => Shooting();
            _actionAim.performed -= _ => StartAim();
            _actionAim.canceled -= _ => CancelAim();
            _actionRun.performed -= _ => StartRun();
            _actionRun.canceled -= _ => CancelRun();
        }
    }
}