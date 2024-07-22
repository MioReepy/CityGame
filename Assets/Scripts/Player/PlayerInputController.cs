using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerController _playerController;
        
        #region InputAction

        private PlayerInput _playerInputController;
        private InputAction _actionMove;
        private InputAction _actionJump;
        private InputAction _actionShoot;

        #endregion

        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInput>();
            _playerController = GetComponent<PlayerController>();
            _actionMove = _playerInputController.actions["Move"];
            _actionJump = _playerInputController.actions["Jump"];
            _actionShoot = _playerInputController.actions["Shoot"];

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _actionShoot.performed += _ => Shooting();
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
        
        private void OnDisable()
        {
            _actionShoot.performed -= _ => Shooting();
        }
    }
}