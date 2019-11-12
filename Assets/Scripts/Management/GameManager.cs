using System.Collections.Generic;
using Instatied;
using Management.S.O;
using Pool;
using UnityEngine;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        public SpawnPref Pref;

        public float SpawnInterval;
        public float _timer;
        
        public List<Spawner> Spawners = new List<Spawner>();
        

        private GameObject _instance1;
        private GameObject _instance2;

        public bool finished;
        
        void Awake()
        {
            finished = false;
           ObjectPool.GetPool(Pref);
            
        }

        private void FixedUpdate()
        {
            _timer += Time.deltaTime;
            if (_timer >= SpawnInterval)
            {
                finished = false;
                Spawner();
            }
        }

        private void Spawner()
        {
            
            _timer -= SpawnInterval;
            if (!finished)
            {
                _instance1 = ObjectPool.SharedInstance.OutOfPool().gameObject;
                SetupInstance(_instance1.GetComponent<Entity>());


                _instance2 = ObjectPool.SharedInstance.OutOfPool().gameObject;
                SetupInstance(_instance2.GetComponent<Entity>());
            }
            finished = true;
        }

        void SetupInstance(Entity entity)
        {
            entity.transform.position = Spawners[Random.Range(0,3)].transform.position;
            entity.gameObject.SetActive(true);
        }

        
    }
}
