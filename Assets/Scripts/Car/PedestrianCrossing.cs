using UnityEngine;

namespace CarSpace
{
    public class PedestrianCrossing : MonoBehaviour
    {
        internal bool canMove;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Pedestrian")
            {
                canMove = false;
            }        
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Pedestrian")
            {
                canMove = true;
            }
        }
    }
}