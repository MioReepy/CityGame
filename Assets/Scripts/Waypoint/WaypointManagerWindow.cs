using UnityEditor;
using UnityEngine;

namespace WaypointSpase
{
    public class WaypointManagerWindow : EditorWindow
    {
        [MenuItem("Tools/Waipoint Editor")]
        public static void Open()
        {
            GetWindow<WaypointManagerWindow>();
        }

        public Transform wayPointRoot;
        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("wayPointRoot"));

            if (wayPointRoot == null)
            {
                EditorGUILayout.HelpBox("Root transform must be selected. Please, assign a root transform", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                DrawButton();
                EditorGUILayout.EndVertical();
            }

            obj.ApplyModifiedProperties();
        }

        private void DrawButton()
        {
            if (GUILayout.Button("Create"))
            {
                CreateWaipoint();
            }

            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
            {
                if (GUILayout.Button("Create Waipoint Before"))
                {
                    CreateWaipointBefore();
                }
                
                if (GUILayout.Button("Create Waipoint After"))
                {
                    CreateWaipointAfter();
                }
                
                if (GUILayout.Button("Remove Waipoint"))
                {
                    RemoveWaipoint();
                }
            }
        }

        private void CreateWaipoint()
        {
            GameObject waypointObject = new GameObject("Waypoint_" + wayPointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(wayPointRoot, false);
            Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

            if (wayPointRoot.childCount > 1)
            {
                waypoint.previousWaipoint =
                    wayPointRoot.GetChild(wayPointRoot.childCount - 2).GetComponent<Waypoint>();
                waypoint.previousWaipoint.nextWaipoint = waypoint;

                waypoint.transform.position = waypoint.previousWaipoint.transform.position;
            }

            Selection.activeGameObject = waypoint.gameObject;
        }

        private void CreateWaipointBefore()
        {
            GameObject waipointObject = new GameObject("Waypoint_" + wayPointRoot.childCount, typeof(Waypoint));
            waipointObject.transform.SetParent(wayPointRoot, false);
            Waypoint newWaypoint = waipointObject.GetComponent<Waypoint>();
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            waipointObject.transform.position = selectedWaypoint.transform.position;
            waipointObject.transform.forward = selectedWaypoint.transform.forward;

            if (selectedWaypoint.previousWaipoint != null)
            {
                newWaypoint.previousWaipoint = selectedWaypoint.previousWaipoint;
                selectedWaypoint.previousWaipoint.nextWaipoint = newWaypoint;
            }

            newWaypoint.nextWaipoint = selectedWaypoint;
            selectedWaypoint.previousWaipoint = newWaypoint;
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
            Selection.activeGameObject = newWaypoint.gameObject;
        }

        private void CreateWaipointAfter()
        {
            GameObject waipointObject = new GameObject("Waypoint_" + wayPointRoot.childCount, typeof(Waypoint));
            waipointObject.transform.SetParent(wayPointRoot, false);
            Waypoint newWaypoint = waipointObject.GetComponent<Waypoint>();
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            waipointObject.transform.position = selectedWaypoint.transform.position;
            waipointObject.transform.forward = selectedWaypoint.transform.forward;
            
            if (selectedWaypoint.nextWaipoint != null)
            {
                newWaypoint.nextWaipoint = selectedWaypoint.nextWaipoint;
                selectedWaypoint.nextWaipoint.previousWaipoint = newWaypoint;
            }
            
            newWaypoint.previousWaipoint = selectedWaypoint;
            selectedWaypoint.nextWaipoint = newWaypoint;
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
            Selection.activeGameObject = newWaypoint.gameObject;
        }

        private void RemoveWaipoint()
        {
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            if (selectedWaypoint.nextWaipoint != null)
            {
                selectedWaypoint.nextWaipoint.previousWaipoint = selectedWaypoint.previousWaipoint;
            }
            
            if (selectedWaypoint.previousWaipoint != null)
            {
                selectedWaypoint.previousWaipoint.nextWaipoint = selectedWaypoint.nextWaipoint;
                Selection.activeGameObject = selectedWaypoint.previousWaipoint.gameObject;
            }
            
            DestroyImmediate(selectedWaypoint.gameObject);
        }
    }
} 