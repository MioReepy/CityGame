using System.Collections.Generic;
using UnityEngine;

namespace WaypointSpase
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] internal Waypoint previousWaipoint;
        [SerializeField] internal Waypoint nextWaipoint;
        [Range(0, 5)] [SerializeField] internal float weight = 1f;
        [Range(0, 1)] [SerializeField] internal float branchRatio = 0.5f;

        public List<Waypoint> Branches;
        public Vector3 GetPosition()
        {
            Vector3 minBounds = transform.position + transform.right * weight / 2;
            Vector3 maxBounds = transform.position - transform.right * weight / 2;

            return Vector3.Lerp(minBounds, maxBounds, Random.Range(0f, 1f));
        }
    }
}