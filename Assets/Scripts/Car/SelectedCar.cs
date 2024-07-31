using UnityEngine;
using UnityEngine.InputSystem;

namespace CarSpace
{
    public class SelectedCar : MonoBehaviour
    {
        [SerializeField] private GameObject _placeForDriver;
        private PlayerInput _playerInput;
        private CarController _carController;
        private CarInputController _carInputController;
        internal static bool canDrive;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _carController = GetComponent<CarController>();
            _carInputController = GetComponent<CarInputController>();
        }
        internal void DriveCar()
        {
            _playerInput.enabled = true;
            _carController.enabled = true;
            _carInputController.enabled = true;
            canDrive = true;
        }
    }
}