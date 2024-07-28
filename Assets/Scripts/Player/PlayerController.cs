using System;
using UnityEngine;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Bullet
        
        [SerializeField] private Transform _barrel;
        [SerializeField] private Transform _gunTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _bulletHitMiss = 25f;
        
        #endregion

        private CharacterController _characterController;
        private Vector3 _moveInput;
        private Vector3 _move;
        private Vector2 _currentBlendAnim;
        private Vector2 _animVelosity;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private float _animSmoothTime = 0.2f;
        
        public Vector2 MoveInput
        {
            set
            {
                _moveInput.x = value.x;
                _moveInput.y = value.y;
            }
        }

        public bool isJump;
        
        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            MovePlayer();
            RotateToDirection();
        }

        private void MovePlayer()
        {
            _currentBlendAnim = Vector2.SmoothDamp(_currentBlendAnim, _moveInput, ref _animVelosity, _animSmoothTime);
            _move = new Vector3(_currentBlendAnim.x, 0f, _currentBlendAnim.y);
            _move = _cameraTransform.right * _moveInput.x + _cameraTransform.forward * _moveInput.y;
            _move.y = 0f;
            _characterController.Move(_move * _playerSpeed * Time.deltaTime);
        }

        private void RotateToDirection()
        {
            if (_moveInput != Vector3.zero)
            {
                Quaternion rotation = Quaternion.Euler(0f, _cameraTransform.eulerAngles.y, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            }
        }

        public void ShootGun()
        {
            GameObject bullet = ObjectPool.SharedInstance.GetPoolesObject();

            if (bullet != null)
            {
                bullet.transform.parent = _barrel;
                bullet.transform.position = _gunTransform.position;
                bullet.transform.rotation = _gunTransform.rotation;
                bullet.SetActive(true);
            }

            BulletController bulletController = GetComponent<BulletController>();

            RaycastHit hit;

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, Mathf.Infinity))
            {
                bulletController.Target = hit.point;
                bulletController.Hit = true;
            }
            else
            {
                bulletController.Target = _cameraTransform.position + _cameraTransform.forward * _bulletHitMiss;
                bulletController.Hit = false;
            }
        }
    }
}