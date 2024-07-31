using UnityEngine;

namespace PlayerSpace
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
        private PlayerAnimator _playerAnimator;
        private Vector3 _moveInput;
        private Vector3 _move;
        internal Vector2 currentBlendAnim;
        private Vector2 _animVelosity;
        internal bool isGroung;
        internal bool isJump;
        [SerializeField] private float _jumpHeight = 1f;
        private float _gravityValue = -9.81f;
        private Vector3 _playerVelosity;
        [SerializeField] private LayerMask _ignoreMask;
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
        
        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _playerAnimator = GetComponent<PlayerAnimator>();
        }

        private void Update()
        {
            GroundCheck();
            MovePlayer();
            JumpPlayer();
            RotateToDirection();
        }

        private void GroundCheck()
        {
            isGroung = _characterController.isGrounded;

            if (!isGroung && _playerVelosity.y < 0)
            {
                _playerVelosity.y = 0;
                isJump = false;
            }
        }

        private void JumpPlayer()
        {
            if (isGroung && isJump)
            {
                _playerVelosity.y = Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
                _playerAnimator.JumpAnimation();
            }
            
            _playerVelosity.y += _gravityValue * 2 * Time.deltaTime;
            _characterController.Move(_playerVelosity * Time.deltaTime);
        }

        private void MovePlayer()
        {
            currentBlendAnim = Vector2.SmoothDamp(currentBlendAnim, _moveInput, ref _animVelosity, _animSmoothTime);
            _move = new Vector3(currentBlendAnim.x, 0f, currentBlendAnim.y);
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
            GameObject bullet = ObjectPool.SharedInstance.GetPoolesBullet();

            if (bullet != null)
            {
                bullet.transform.parent = _barrel;
                bullet.transform.position = _gunTransform.position;
                bullet.transform.rotation = _gunTransform.rotation;
                bullet.SetActive(true);

                BulletController bulletController = bullet.GetComponent<BulletController>();
                    
                Ray ray = new Ray(transform.position, _cameraTransform.forward);
            
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _ignoreMask)) 
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

        public void AimGun()
        {
            throw new System.NotImplementedException();
        }
    }
}