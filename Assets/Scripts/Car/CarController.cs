using PlayerSpace;
using UnityEngine;

namespace CarSpace
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private WheelCollider _fl_Wheel_Collider;
        [SerializeField] private WheelCollider _fr_Wheel_Collider;
        [SerializeField] private WheelCollider _bl_Wheel_Collider;
        [SerializeField] private WheelCollider _br_Wheel_Collider;

        [SerializeField] private float _acceleration = 500f;
        [SerializeField] private float _breakForce = 400f;
        [SerializeField] private float _slowingForce = 50f;
        [SerializeField] private float _maxTurnAngle = 15f;

        private float _currentAcceleration;
        private float _currentBreakeForce;
        private float _currentTurnAngle;
        internal bool _isBreak;

        private Rigidbody _rigidbody;
        private PlayerInputController _playerInputController;

        private Vector3 _moveInput;

        public Vector2 MoveInput
        {
            set
            {
                _moveInput.x = value.x;
                _moveInput.y = value.y;
            }
        }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = Vector3.zero;
        }

        private void FixedUpdate()
        {
            MoveCar();
            RotateCar();
        }

        private void MoveCar()
        {
            _currentAcceleration = _moveInput.y * _acceleration;

            if (_isBreak)
            {
                _currentBreakeForce = _breakForce;
                // Debug.Log(_isBreak);
            }
            else if (_moveInput.y == 0f)
            {
                _currentBreakeForce = _breakForce / _slowingForce;
                // Debug.Log(_isBreak);
            }
            else
            {
                _currentBreakeForce = 0f;
                // Debug.Log(_isBreak);
            }

            _fl_Wheel_Collider.motorTorque = _currentAcceleration;
            _fr_Wheel_Collider.motorTorque = _currentAcceleration;

            _fr_Wheel_Collider.brakeTorque = _currentBreakeForce;
            _fl_Wheel_Collider.brakeTorque = _currentBreakeForce;
            _br_Wheel_Collider.brakeTorque = _currentBreakeForce;
            _bl_Wheel_Collider.brakeTorque = _currentBreakeForce;
        }

        private void RotateCar()
        {
            _currentTurnAngle = _moveInput.x * _maxTurnAngle;
            
            _fl_Wheel_Collider.steerAngle = _currentTurnAngle;
            _fr_Wheel_Collider.steerAngle = _currentTurnAngle;
        }
    }
}