using Character;
using UnityEngine;

namespace WaypointSpase
{
    public class WaipointNavigator : MonoBehaviour
    {
        [SerializeField] private CharacterNavigatorController _controller;
        [SerializeField] internal Waypoint _currentWaypoint;

        private int _direction;

        private void Awake()
        {
            _controller.GetComponent<CharacterNavigatorController>();
            _direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        }

        private void Start()
        {
            _controller.SetDestination(_currentWaypoint.GetPosition());
        }

        private void Update()
        {
            if (!_controller.isReachedDestination)
            {
                bool shoulBranch = false;

                if (_currentWaypoint.Branches != null && _currentWaypoint.Branches.Count > 0)
                {
                    float tempBranch = Random.Range(0f, 1f);
                    Debug.Log(tempBranch);
                    shoulBranch = tempBranch <= _currentWaypoint.branchRatio ? true : false;
                    Debug.Log(shoulBranch);
                }

                if (shoulBranch)
                {
                    _currentWaypoint = _currentWaypoint.Branches[Random.Range(0, _currentWaypoint.Branches.Count - 1)];
                }
                else
                {
                    if (_direction == 0)
                    {
                        if (_currentWaypoint.nextWaipoint != null)
                        {
                            _currentWaypoint = _currentWaypoint.nextWaipoint;
                        }
                        else
                        {
                            _currentWaypoint = _currentWaypoint.previousWaipoint;
                            _direction = 1;
                        }
                    }
                    else if (_direction == 1)
                    {
                        if (_currentWaypoint.previousWaipoint != null)
                        {
                            _currentWaypoint = _currentWaypoint.previousWaipoint;
                        }
                        else
                        {
                            _currentWaypoint = _currentWaypoint.nextWaipoint;
                            _direction = 0;
                        }
                    }
                    _controller.SetDestination(_currentWaypoint.GetPosition());
                }
            }
        }
    }
}
