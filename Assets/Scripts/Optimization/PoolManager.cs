using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Optimization
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;

        public Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public GameObject GetObjectFromPool(string poolKey, GameObject spawnObject, Vector2 position, Quaternion rotation)
        {
            if (!pools.ContainsKey(poolKey))
            {
                pools.Add(poolKey, new List<GameObject>());
            }

            if (pools[poolKey].Count > 0)
            {
                GameObject pooledObject = pools[poolKey][0];
                pools[poolKey].RemoveAt(0);
                pooledObject.transform.position = position;
                pooledObject.transform.rotation = rotation;
                pooledObject.SetActive(true);
                return pooledObject;
            }
            else
            {
                Debug.LogWarning($"Pool for {poolKey} is empty. Creating a new object.");
                GameObject newObject = Instantiate(spawnObject, position, rotation);
                pools[poolKey].Add(newObject);
                return newObject;
            }
        }

        public void ReturnObjectToPool(string poolKey, GameObject objectToReturn)
        {
            objectToReturn.SetActive(false);

            if (!pools.ContainsKey(poolKey))
            {
                pools.Add(poolKey, new List<GameObject>());
            }

            pools[poolKey].Add(objectToReturn);
        }
    }
}
