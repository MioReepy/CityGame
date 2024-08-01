using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class FieldOfView : MonoBehaviour
    {
        #region Layers

        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _enviromentMask;

        #endregion

        #region AreaSettings

        [Range(0, 360)] [SerializeField] internal float viewAngle = 5f;
        [Range(0, 50)] [SerializeField] internal float viewRadius = 5f;
        [SerializeField] private float _delayTime = 0.2f;
        public List<Transform> VisibleTarget;

        #endregion

        private void Start()
        {
            StartCoroutine("FindTarget", _delayTime);
        }

        IEnumerator FindTarget(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTarget();
                DeleteInVisibleTarger();
            }
        }

        private void FindVisibleTarget()
        {
            Collider[] targetInRadius = Physics.OverlapSphere(transform.position, viewRadius, _targetMask);

            for (int i = 0; i < targetInRadius.Length; i++)
            {
                Transform target = targetInRadius[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                
                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    if (Physics.Raycast(transform.position, directionToTarget, _targetMask) && !Physics.Raycast(transform.position, 
                            directionToTarget, viewRadius, _enviromentMask))
                    {
                        VisibleTarget.Add(target);
                    }
                }
            }
        }

        private void DeleteInVisibleTarger()
        {
            for (int i = 0; i < VisibleTarget.Count; i++)
            {
                Vector3 directionToTarget = (VisibleTarget[i].position - transform.position).normalized;

                if (viewRadius > directionToTarget.magnitude || Vector3.Angle(transform.forward, directionToTarget) > viewAngle / 2 || 
                    Physics.Raycast(transform.position, directionToTarget, viewRadius, _enviromentMask))
                {
                    VisibleTarget.Remove(VisibleTarget[i]);
                }
            }
        }
        
        internal Vector3 DirectionFromAngle(float angleDegrees, bool isAngleGlobal)
        {
            if (!isAngleGlobal)
            {
                angleDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
        }
    }
}