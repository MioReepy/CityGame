using UnityEngine;
using UnityEngine.InputSystem;

namespace CarSpace
{
    public class SelectedCar : MonoBehaviour
    {
        #region CarParameters

        private PlayerInput _playerInput;
        private CarController _carController;
        private CarInputController _carInputController;

        #endregion

        #region Player

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _playerPlace;

        #endregion

        #region Events

        public delegate void Drive();
        public static Drive OnGetIntoTheCar;

        #endregion

        private void OnEnable()
        {
            CarInputController.OnGetOutOfTheCar += ExitCar;
        }

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
            OnGetIntoTheCar?.Invoke();
        }

        private void ExitCar()
        {
            _playerInput.enabled = false;
            _carController.enabled = false;
            _carInputController.enabled = false;
            _player.transform.position = _playerPlace.transform.position;
            _player.transform.rotation = _playerPlace.transform.rotation;
            _player.gameObject.SetActive(true);

        }
        
        private void OnDisable()
        {
            CarInputController.OnGetOutOfTheCar -= ExitCar;
        }
    }
}