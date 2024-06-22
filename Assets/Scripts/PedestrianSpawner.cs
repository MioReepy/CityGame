using System.Collections;
using UnityEngine;
using WaypointSpase;

namespace Character
{
    public class PedestrianSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _pedestrianPrefab;
        [SerializeField] private int _pedestrianCount = 1;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            int count = 0;

            while (count < _pedestrianCount)
            {
                GameObject obj = Instantiate(_pedestrianPrefab);
                Transform child = transform.GetChild(Random.Range(0, transform.childCount));
                obj.GetComponent<WaipointNavigator>()._currentWaypoint = child.GetComponent<Waypoint>();
                obj.transform.position = child.position;

                yield return new WaitForEndOfFrame();
                
                count++;
            }
        }
    }
}