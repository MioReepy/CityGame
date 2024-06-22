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
                CreateWaypoint();
            }

            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
            {
                if (GUILayout.Button("Create Waipoint Before"))
                {
                    CreateWaypointBefore();
                }
                
                if (GUILayout.Button("Create Waipoint After"))
                {
                    CreateWaypointAfter();
                }
                
                if (GUILayout.Button("Remove Waipoint"))
                {
                    RemoveWaypoint();
                }

                if (GUILayout.Button("Create branch Waipoint"))
                {
                    CreateBranchWaypoint();
                }
            }
        }

        private void CreateWaypoint()
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

        private void CreateWaypointBefore()
        {
            GameObject waypointObject = new GameObject("Waypoint_" + wayPointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(wayPointRoot, false);
            Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            waypointObject.transform.position = selectedWaypoint.transform.position;
            waypointObject.transform.forward = selectedWaypoint.transform.forward;

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

        private void CreateWaypointAfter()
        {
            GameObject waypointObject = new GameObject("Waypoint_" + wayPointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(wayPointRoot, false);
            Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            waypointObject.transform.position = selectedWaypoint.transform.position;
            waypointObject.transform.forward = selectedWaypoint.transform.forward;
            
            if (selectedWaypoint.nextWaipoint != null)
            {
                newWaypoint.nextWaipoint = selectedWaypoint.nextWaipoint;
                selectedWaypoint.nextWaipoint.previousWaipoint = newWaypoint;
            }
            
            newWaypoint.previousWaipoint = selectedWaypoint;
            selectedWaypoint.nextWaipoint = newWaypoint;
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex() + 1);
            Selection.activeGameObject = newWaypoint.gameObject;
        }

        private void RemoveWaypoint()
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

        private void CreateBranchWaypoint()
        {
            GameObject waypointGameObject = new GameObject("Waypoint_" + wayPointRoot.childCount, typeof(Waypoint));
            waypointGameObject.transform.SetParent(wayPointRoot, false);
            Waypoint waypoint = waypointGameObject.GetComponent<Waypoint>();
            Waypoint branchesFrom = Selection.activeGameObject.GetComponent<Waypoint>();
            branchesFrom.Branches.Add(waypoint);
            waypoint.transform.position = branchesFrom.transform.position;
            waypoint.transform.forward = branchesFrom.transform.forward;
            
            Selection.activeGameObject = waypoint.gameObject;
        }
    }
} 