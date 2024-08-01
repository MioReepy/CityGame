using UnityEngine;
using UnityEngine.Splines;

namespace CarSpace
{
    public class CarTrafic : MonoBehaviour
    {
        private SplineAnimate _splineAnimate;
        
        private float _currentSpeed;
        private bool _isDrive;

        private void Awake()
        {
            _splineAnimate = GetComponent<SplineAnimate>();
        }

        private void Start()
        {
            _splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "PedestrianCrossing")
            {
                if (!other.GetComponent<PedestrianCrossing>().canMove)
                {
                    _splineAnimate.enabled = false;
                }
                else
                {
                    _splineAnimate.enabled = true;
                }
            }
        }
    }
}