using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ObjectPool : MonoBehaviour
    {
        private PooledObject prefab;
        public List<PooledObject> availableObjects = new List<PooledObject>();
        
        public static ObjectPool GetPool(PooledObject prefab)
        {
            GameObject obj;
            ObjectPool pool;
            // Si les pool ont été créées manuellement set la pool comme etant ces objets.
            if (Application.isEditor)
            {
                obj = GameObject.Find(prefab.name + " Pool");
                if (obj)
                {
                    pool = obj.GetComponent<ObjectPool>();
                    if (pool) return pool;
                }
            }
            //sinon créé des objets en tant que pool
            obj = new GameObject(prefab.name + " Pool");
            pool = obj.AddComponent<ObjectPool>();
            pool.prefab = prefab;
            return pool;
        }
        public PooledObject GetObject()
        {
            PooledObject obj;
            int lastAvailableIndex = availableObjects.Count - 1;
            if (lastAvailableIndex >= 0)
            {
                obj = availableObjects[lastAvailableIndex];
                availableObjects.RemoveAt(lastAvailableIndex);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Instantiate(prefab);
                obj.transform.SetParent(transform,false);
                obj.Pool = this;
            }
            return obj;
        }
        public void AddObject(PooledObject po)
        {
            po.gameObject.SetActive(false);
            availableObjects.Add(po);
        }
    }
}
