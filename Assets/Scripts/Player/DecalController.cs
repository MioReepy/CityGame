using System.Collections;
using UnityEngine;


namespace PlayerSpace
{
    public class DecalController : MonoBehaviour
    {
        [SerializeField] private float _bulletDestroyTime = 5f;

        private void OnEnable()
        {
            StartCoroutine(DeactivateDecalAfterDelay(gameObject));
        }
        private IEnumerator DeactivateDecalAfterDelay(GameObject obj)
        {
            yield return new WaitForSeconds(_bulletDestroyTime);
            
            obj.SetActive(false);
        }
    }
}