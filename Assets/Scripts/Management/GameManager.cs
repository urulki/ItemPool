using System.Collections.Generic;
using Instacied;
using Management.S.O;
using Pool;
using UnityEngine;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        public SpawnPref Pref;

        public float SpawnInterval;
        private float _timer;
        
        public List<Spawner> Spawners = new List<Spawner>();
        public List<Entity> ActiveEntity = new List<Entity>();

        private GameObject _instance1;
        private GameObject _instance2;

        private bool finished;
        
        void Awake()
        {
            
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

        private void CheckActiveEntity()
        {
            for (int i = 0; i < ActiveEntity.Count - 1; i++)
            {
                if (ActiveEntity[i].gameObject.activeInHierarchy) ActiveEntity.Remove(ActiveEntity[i]);
            }
        }

        private void Spawner()
        {
            
            _timer -= SpawnInterval;
            if (!finished)
            {
                _instance1 = ObjectPool.SharedInstance.OutOfPool().gameObject;
                SetupInstance(_instance1.GetComponent<Entity>());
                ActiveEntity.Add(_instance1.GetComponent<Entity>());


                _instance2 = ObjectPool.SharedInstance.OutOfPool().gameObject;
                SetupInstance(_instance2.GetComponent<Entity>());
                ActiveEntity.Add(_instance2.GetComponent<Entity>());
            }
            CheckActiveEntity();
            SetTarget(_instance1);
            SetTarget(_instance2);
            finished = true;
        }

        void SetupInstance(Entity entity)
        {
            entity.transform.position = Spawners[entity.ID].transform.position;
            Spawners[entity.ID].LinkedEntity = entity;
            entity.gameObject.SetActive(true);
        }

        void SetTarget(GameObject entity)
        {
            int randSpawner = Random.Range(0, Spawners.Count - 1);
            while(randSpawner == entity.GetComponent<Entity>().ID)
            {
                randSpawner = Random.Range(0, Spawners.Count - 1);
            }
            entity.GetComponent<Entity>().Agent.SetDestination( Spawners[randSpawner].LinkedEntity.transform.position);
        }
    }
}
