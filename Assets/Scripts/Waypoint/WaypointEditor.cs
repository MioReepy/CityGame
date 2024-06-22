using UnityEditor;
using UnityEngine;

namespace WaypointSpase
{
    [InitializeOnLoad()]
    public class WaipointEditor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
        internal static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
        {
            float waipointRadius = 0.15f;
            
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.cyan;
            }
            else
            {
                Gizmos.color = Color.cyan * 0.5f;
            }
            
            Gizmos.DrawSphere(waypoint.transform.position, waipointRadius);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.weight / 2f), 
                waypoint.transform.position - (waypoint.transform.right * waypoint.weight / 2f));

            if (waypoint.previousWaipoint != null)
            {
                Gizmos.color = Color.blue;
                Vector3 offset = waypoint.transform.right * waypoint.weight / 2f;
                Vector3 offsetTo = waypoint.previousWaipoint.transform.right * waypoint.previousWaipoint.weight / 2f;
                Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaipoint.transform.position + offsetTo);
            }

            if (waypoint.nextWaipoint != null)
            {
                Gizmos.color = Color.magenta;
                Vector3 offset = waypoint.transform.right * -waypoint.weight / 2f;
                Vector3 offsetTo = waypoint.nextWaipoint.transform.right * -waypoint.nextWaipoint.weight / 2f;
                Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.nextWaipoint.transform.position + offsetTo);
            }

            if (waypoint.Branches != null)
            {
                foreach (Waypoint branch in waypoint.Branches)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
                }
            }
        }
        
    }
}
