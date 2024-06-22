using System;
using UnityEngine;

public class CharacterNavigatorController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _stopDistance = 1f;
    [SerializeField] private string _name;
    
    private Vector3 _destination;
    public bool isReachedDestination;

    private void Update()
    {
        if (transform.position != _destination)
        {
            Vector3 destinationDirection = _destination - transform.position;
            destinationDirection.y = 0f;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= _stopDistance)
            {
                isReachedDestination = true;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
            }
            else
            {
                isReachedDestination = false;
            }
        }
    }

    internal void SetDestination(Vector3 pointPosition, string pointName)
    {
        _destination = pointPosition;
        _name = pointName;
        isReachedDestination = false;
    }
}
