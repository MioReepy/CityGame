using UnityEngine;
using UnityEngine.InputSystem;

namespace CarSpace
{
    public class CarInputController : MonoBehaviour
    {
        #region InputActions

        private InputAction _actionMove;
        private InputAction _actionBreak;
        private InputAction _actionExitCar;
        private PlayerInput _carInputController;
        
        #endregion

        #region CarController

        private CarController _carController;

        #endregion

        #region Events

        public delegate void Interact();
        public static event Interact OnGetOutOfTheCar;

        #endregion

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
            _carController.isBreak = true;
        }

        private void CancelBreak()
        {
            _carController.isBreak = false;
        }

        private void ExitCar()
        {
            OnGetOutOfTheCar?.Invoke();
        }

        private void OnDisable()
        {
            _actionBreak.performed -= _ => StartBreak();
            _actionBreak.canceled -= _ => CancelBreak();
            _actionExitCar.performed -= _ => ExitCar();
        }
    }
}