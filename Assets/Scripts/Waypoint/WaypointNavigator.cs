using System;
using UnityEngine;

namespace WaypointSpase
{
    public class WaipointNavigator : MonoBehaviour
    {
        [SerializeField] private CharacterNavigatorController _controller;
        [SerializeField] private Waypoint _currentWaypoint;

        private void Awake()
        {
            _controller.GetComponent<CharacterNavigatorController>();
        }

        private void Start()
        {
            _controller.SetDestination(_currentWaypoint.GetPosition(), _currentWaypoint.name);
        }

        private void Update()
        {
            if (!_controller.isReachedDestination)
            {
                _currentWaypoint = _currentWaypoint.nextWaipoint;
                _controller.SetDestination(_currentWaypoint.GetPosition(), _currentWaypoint.name);
            }
        }
    }
}
