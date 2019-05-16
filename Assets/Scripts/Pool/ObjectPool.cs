using System;
using System.Collections.Generic;
using Instacied;
using Management.S.O;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pool
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;
        public List<Entity> PooledObjects = new List<Entity>();
        public List<Entity> ObjectsToPool = new List<Entity>();
        public int AmountOfInstance;
        
        private void Awake()
        {
            SharedInstance = this;
            
        }

        private void Start()
        {
            for (int i = 0; i < AmountOfInstance; i++)
            {
                PooledObjects.Add(Instantiate(ObjectsToPool[Random.Range(0,ObjectsToPool.Count-1)],this.transform));
                PooledObjects[i].ID = i;
            }
        }

        public static ObjectPool GetPool(SpawnPref pref)
        {
            GameObject obj;
            ObjectPool pool;
            if (Application.isEditor)
            {
                obj = GameObject.Find("Pool");
                if (obj)
                {
                    pool = obj.GetComponent<ObjectPool>();
                    if (pool) return pool;
                    else
                    {
                        obj.AddComponent<ObjectPool>();
                        pool = obj.GetComponent<ObjectPool>();
                        pool = SetNewPool(pref,pool);
                        return pool;
                    }
                }
            }
            obj = new GameObject("Pool");
            obj.AddComponent<ObjectPool>();
            pool = obj.GetComponent<ObjectPool>();
            if(!pool) throw new NotImplementedException("No Pool Set");
            pool = SetNewPool(pref,pool);
            return pool;
        }

        public static ObjectPool SetNewPool(SpawnPref pref, ObjectPool pool)
        {
            pool.ObjectsToPool = pref.ObjectsToPool;
            pool.AmountOfInstance = pref.AmountOfInstance;
            return pool;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        public Entity OutOfPool()
        {
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                if (!PooledObjects[i].gameObject.activeInHierarchy)
                {
                    return PooledObjects[i];
                }
            }
            return null;
        }
    }
}
