using UnityEngine;
using UnityEngine.InputSystem;

namespace CarSpace
{
    public class CarInputController : MonoBehaviour
    {
        private PlayerInput _carInputController;
        private CarController _carController;
        private InputAction _actionMove;
        private InputAction _actionBreak;
        private InputAction _actionExitCar;
        
        public delegate void Interact();
        public static event Interact OnExit;

        private void Awake()
        {
            _carInputController = GetComponent<PlayerInput>();
            _carController = GetComponent<CarController>();
            _actionMove = _carInputController.actions["Move"];
            _actionBreak = _carInputController.actions["Break"];
            _actionExitCar = _carInputController.actions["Exit"];
        }

        private void OnEnable()
        {
            _actionBreak.performed += _ => StartBreak();
            _actionBreak.canceled += _ => CancelBreak();
            _actionExitCar.performed += _ => ExitCar();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 input = _actionMove.ReadValue<Vector2>();
            _carController.MoveInput = input;
        }

        private void StartBreak()
        {
            _carController._isBreak = true;
            
        }

        private void CancelBreak()
        {
            _carController._isBreak = false;
        }

        private void ExitCar()
        {
            OnExit?.Invoke();
        }

        private void OnDisable()
        {
            _actionBreak.performed -= _ => StartBreak();
            _actionBreak.canceled -= _ => CancelBreak();
            _actionExitCar.performed -= _ => ExitCar();
        }
    }
}