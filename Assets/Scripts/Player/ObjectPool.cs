using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;
        
        private List<GameObject> _poolObjects;
        private List<GameObject> _decalObjects;
        [SerializeField] private GameObject _bulletToPool;
        [SerializeField] private GameObject _barrelToBullet;
        [SerializeField] private GameObject _decalToPool;
        [SerializeField] private GameObject _barrelToDecal;
        [SerializeField] private int _amountToPool;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            _poolObjects = new List<GameObject>();
            _decalObjects = new List<GameObject>();
            GameObject poolTemp;

            for (int i = 0; i < _amountToPool; i++)
            {
                poolTemp = Instantiate(_bulletToPool, _barrelToBullet.transform);
                poolTemp.SetActive(false);
                _poolObjects.Add(poolTemp);
            }
            
            for (int i = 0; i < _amountToPool; i++)
            {
                poolTemp = Instantiate(_decalToPool, _barrelToDecal.transform);
                poolTemp.SetActive(false);
                _decalObjects.Add(poolTemp);
            }
        }

        public GameObject GetPoolesBullet()
        {
            for (int i = 0; i < _amountToPool; i++)
            {
                if (!_poolObjects[i].activeInHierarchy)
                {
                    return _poolObjects[i];
                }
            }

            return null;
        }
        
        public GameObject GetPoolesDecals()
        {
            for (int i = 0; i < _amountToPool; i++)
            {
                if (!_decalObjects[i].activeInHierarchy)
                {
                    return _poolObjects[i];
                }
            }

            return null;
        }
    }
}