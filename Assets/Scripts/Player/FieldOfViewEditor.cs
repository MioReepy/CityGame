using UnityEditor;
using UnityEngine;

namespace PlayerSpace
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            FieldOfView fieldOfView = (FieldOfView)target;
            Handles.color = Color.green;
            float thickness = 2.0f;

            Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward,
                360, fieldOfView.viewRadius, thickness);

            Vector3 viewAngleA = fieldOfView.DirectionFromAngle(-fieldOfView.viewAngle / 2, false);
            Vector3 viewAngleB = fieldOfView.DirectionFromAngle(fieldOfView.viewAngle / 2, false);

            Handles.color = Color.blue;
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position
                                                             + viewAngleA * fieldOfView.viewRadius);
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position
                                                             + viewAngleB * fieldOfView.viewRadius);

            Handles.color = Color.red;

            foreach (var visibleTarget in fieldOfView.VisibleTarget)
            {
                Handles.DrawLine(fieldOfView.transform.position, visibleTarget.position, thickness);
            }
        }
    }
}
