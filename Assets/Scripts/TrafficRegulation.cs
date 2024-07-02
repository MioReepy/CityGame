using UnityEngine;
using WaypointSpase;
using Random = UnityEngine.Random;

namespace TraficSpace
{
    public class TrafficRegulation : MonoBehaviour
    {
        [SerializeField] private bool _isActive;
        [Range(1f, 20f)] [SerializeField] private float _timeInterval;
        [SerializeField] private GameObject _greenLight;
        [SerializeField] private GameObject _redLight;
        [SerializeField] private Waypoint _waypointFront;
        [SerializeField] private Waypoint _waypointBack;
        
        private bool _isCanGo;
        private float _time;

        private void Start()
        {
            if (_isActive)
            {
                _time = _timeInterval;
                _isCanGo = Random.value > 0.5;

                ChangeLight();
            }
        }

        private void Update()
        {
            if (_isActive)
            {
                _time -= Time.deltaTime;

                if (_time <= 0)
                {
                    _isCanGo = !_isCanGo;
                    ChangeLight();
                    _time = _timeInterval;
                }
            }
        }

        private void ChangeLight()
        {
            if (_isCanGo)
            {
                _greenLight.SetActive(false);
                _redLight.SetActive(true);
                _waypointFront._isStop = true;
                _waypointBack._isStop = true;
            }
            else
            {
                _redLight.SetActive(false);
                _greenLight.SetActive(true);
                _waypointFront._isStop = false;
                _waypointBack._isStop = false;
            }
        }
    }
}