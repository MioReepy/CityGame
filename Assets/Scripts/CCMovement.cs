using UnityEngine;

namespace Character
{
    public class CCMovement : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _mouseSensivity = 100f;
        [SerializeField] private float _gravity = 9.81f;
        [SerializeField] private float _border = 50f;

        private CharacterController _characterController;
        private Vector3 _velosity;
        private float _rotationX = 0f;
        private float _rotationY = 0f;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            RotationCharacter();
            ApplyGravitation();
        }

        private void MoveCharacter()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            _characterController.Move(move * _speed * Time.deltaTime);
        }

        private void RotationCharacter()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

            _rotationY += mouseX;
            transform.localRotation = Quaternion.Euler(0f, _rotationY, 0f);

            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, -_border, _border);
            _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        }

        private void ApplyGravitation()
        {
            if (_characterController.isGrounded && _velosity.y < 0)
            {
                _velosity.y = -2f;
            }
        }
    }
}
